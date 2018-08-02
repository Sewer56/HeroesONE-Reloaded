using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using HeroesONE_R.Structures.Common;
using HeroesONE_R.Structures.ShadowTheHedgehog;
using HeroesONE_R.Structures.SonicHeroes;
using HeroesONE_R.Structures.SonicHeroes.ONE_Subsctuctures;
using HeroesONE_R.Structures.Subsctructures;
using HeroesONE_R.Utilities;

namespace HeroesONE_R.Structures
{
    /// <summary>
    /// Defines an individual archive in a format that is specifically optimised for importing and exporting
    /// files.
    /// </summary>
    public class Archive
    {
        /// <summary>
        /// Stores the RenderWare version assigned to this .ONE file.
        /// </summary>
        public RWVersion RwVersion;

        /// <summary>
        /// Array of the actual file structures used within the game.
        /// Equivalent to the file count.
        /// </summary>
        public List<ArchiveFile> Files;

        private Archive()
        {
            Files = new List<ArchiveFile>();
        }

        /// <summary>
        /// Initiates a new Archive with a specified RenderWare version ID.
        /// </summary>
        /// <param name="rwVersion">The RenderWare version to use.</param>
        public Archive(RWVersion rwVersion) : this()
        {
            RwVersion = rwVersion;
        }

        /// <summary>
        /// Initiates a new Archive with a specified RenderWare version ID.
        /// </summary>
        /// <param name="commonRenderwareVersion">The commonly used RenderWare version to use.</param>
        public Archive(CommonRWVersions commonRenderwareVersion) : this()
        {
            RwVersion.RwVersion = (uint)commonRenderwareVersion;
        }

        /// <summary>
        /// Parses a Sonic Heroes/Shadow ONE Archive from the supplied byte array and returns
        /// you an instance of Archive.
        /// </summary>
        /// <returns></returns>
        public static unsafe Archive FromONEFile(ref byte[] oneArchive)
        {
            // Know if we're dealing with Shadow050 or Shadow060
            ONEArchiveType archiveType = ONEArchiveTester.GetArchiveType(ref oneArchive);

            if (archiveType == ONEArchiveType.Heroes) { return new ONEArchive(ref oneArchive).GetArchive(); }
            else { return new ONEShadowArchive(ref oneArchive).GetArchive(); }
        }

        /// <summary>
        /// Generates a Heroes ONE archive from the provided HeroesONE Archive structure.
        /// </summary>
        public List<byte> BuildHeroesONEArchive()
        {
            // Note: The file name section has 2 blank entries at file name index 0 and 1, we need to skip those.
            // Populate some data before we write it.
            ONEHeader fileHeader = new ONEHeader();
            ONEFileNameSectionHeader fileNameSectionHeader = new ONEFileNameSectionHeader();

            // Header
            fileHeader.RenderWareVersion = RwVersion;
            fileHeader.FileSize = 0;
            fileHeader.Null = 0;

            // Name Header
            fileNameSectionHeader.RenderWareVersion = RwVersion;
            fileNameSectionHeader.Unknown = 1;
            fileNameSectionHeader.FileNameSectionLength = ((Files.Count + 2) * ONEFileName.FileNameLength);

            // Populate ArchiveFile names
            List<ONEFileName> fileNames = new List<ONEFileName>(ONEArchive.MAX_FILE_COUNT);

            // Insert blank entries
            fileNames.Add(new ONEFileName(""));
            fileNames.Add(new ONEFileName(""));

            for (int x = 0; x < Files.Count; x++)
            { fileNames.Add(new ONEFileName(Files[x].Name)); }

            // Populate ArchiveFile Structure
            List<ONEFile> oneFiles = new List<ONEFile>(Files.Count);

            for (int x = 0; x < Files.Count; x++)
            {
                ONEFile oneFile = new ONEFile();
                oneFile.CompressedData = Files[x].CompressedData;
                oneFile.ONEFileHeader.FileNameIndex = x + 2; // ArchiveFile names always start after two blank entries, for some reason...
                oneFile.ONEFileHeader.FileSize = oneFile.CompressedData.Length;
                oneFile.ONEFileHeader.RwVersion = Files[x].RwVersion;
                oneFiles.Add(oneFile);
            }

            // Calculate file size.
            fileHeader.FileSize += Marshal.SizeOf(fileNameSectionHeader);
            fileHeader.FileSize += (Marshal.SizeOf<ONEFileName>() * fileNames.Count);

            for (int x = 0; x < oneFiles.Count; x++)
            {
                fileHeader.FileSize += Marshal.SizeOf(oneFiles[x].ONEFileHeader);
                fileHeader.FileSize += oneFiles[x].CompressedData.Length;
            }

            // Generate ONE file.
            List<byte> file = new List<byte>(fileHeader.FileSize + Marshal.SizeOf<ONEFileHeader>());
            file.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(ref fileHeader));
            file.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(ref fileNameSectionHeader));

            for (int x = 0; x < fileNames.Count; x++)
            {
                ONEFileName fileName = fileNames[x];
                file.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(ref fileName));
            }

            for (int x = 0; x < oneFiles.Count; x++)
            {
                ONEFileHeader localFileHeader = oneFiles[x].ONEFileHeader;
                file.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(ref localFileHeader));
                file.AddRange(oneFiles[x].CompressedData);
            }

            return file;
        }

        /// <summary>
        /// Generates a Shadow ONE Archive from the current Archive instance.
        /// </summary>
        /// <param name="isShadow60Archive">Set to false to save in Shadow ONE Ver 0.50 archive format; true for ONE Ver 0.60 archive format.</param>
        /// <returns></returns>
        public unsafe List<byte> BuildShadowONEArchive(bool isShadow60Archive)
        {
            // Note: The file name section has 2 blank entries at file name index 0 and 1, we need to skip those.
            // Populate some data before we write it.
            ONEHeader fileHeader = new ONEHeader();
            ONEFileVersion fileVersion;
            int fileCount;
            ONEPadding padding;
            List<IFileEntry> localFiles = new List<IFileEntry>();
            List<byte> fileData = new List<byte>(5000 * 1000); // 5MB default buffer size.

            // Header
            fileHeader.RenderWareVersion = RwVersion;
            fileHeader.FileSize = 0;

            // Set One Version String 
            fileVersion = isShadow60Archive ? new ONEFileVersion("One Ver 0.60") : new ONEFileVersion("One Ver 0.50");

            // The amount of files present within the archive.
            fileCount = this.Files.Count;

            // Generate the generic padding used.
            for (int x = 1; x < ONEPadding.PADDING_LENGTH; x++)
            {
                padding.Padding[x] = 0xCD;
            }

            // Insert file definitions.
            // Dummies first!
            ONE50FileEntry dummmyEntry = new ONE50FileEntry();

            // Actual files.
            for (int x = 0; x < fileCount; x++)
            {
                // Set file entry;
                IFileEntry fileEntry;
                if (isShadow60Archive) fileEntry = new ONE60FileEntry(Files[x].Name);
                else fileEntry = new ONE50FileEntry(Files[x].Name);

                // Set file size.
                fileEntry.FileSize = Prs.Decompress(ref Files[x].CompressedData).Length;
                localFiles.Add(fileEntry);
            }

            // Calculate offsets and total file size.
            int filePointer = 0;

            // 0xC header ignored purposefully, it is only metadata after actual file is created.
            filePointer += Unsafe.SizeOf<ONEFileVersion>();
            filePointer += Unsafe.SizeOf<int>();
            filePointer += Unsafe.SizeOf<ONEPadding>();

            // Offset for dummy files
            filePointer += Unsafe.SizeOf<ONE50FileEntry>() * (2);
            filePointer += Unsafe.SizeOf<ONE50FileEntry>() * (fileCount);

            // Note: Both file entry types have equal length.
            for (int x = 0; x < fileCount; x++) 
            {
                localFiles[x].FileOffset = filePointer; // Offset for dummy files.
                filePointer += Files[x].CompressedData.Length;
            }

            fileHeader.FileSize = filePointer;

            // Start writing through file data.
            fileData.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(ref fileHeader));
            fileData.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(ref fileVersion));
            fileData.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(ref fileCount));
            fileData.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(ref padding));

            // Add dummies
            fileData.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(ref dummmyEntry));
            fileData.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(ref dummmyEntry));

            // File entries
            for (int x = 0; x < localFiles.Count; x++)
            {
                if (isShadow60Archive)
                    fileData.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe((ONE60FileEntry)localFiles[x]));
                else
                    fileData.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe((ONE50FileEntry)localFiles[x]));
            }

            // File data
            for (int x = 0; x < Files.Count; x++)
            { fileData.AddRange(Files[x].CompressedData); }

            return fileData;
        }
    }
}
