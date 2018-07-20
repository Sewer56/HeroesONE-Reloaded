using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
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
        public static unsafe Archive FromHeroesONE(ref byte[] oneArchive)
        {
            ONEArchive heroesONEArchive = new ONEArchive(ref oneArchive);
            return GetHeroesONEArchive(ref heroesONEArchive);
        }

        /// <summary>
        /// Parses a Sonic Heroes/Shadow ONE Archive and retrieves a HeroesONE archive,
        /// simpler and more optimized for manipulation of individual files.
        /// </summary>
        /// <param name="oneArchive">A Sonic Heroes ONE archive structure that has been parsed from a file.</param>
        /// <returns>A HeroesONE archive that makes manipulating ONE archives easy!</returns>
        public static unsafe Archive GetHeroesONEArchive(ref ONEArchive oneArchive)
        {
            Archive heroesONEArchive = new Archive();
            heroesONEArchive.Files = new List<ArchiveFile>(oneArchive.Files.Count);

            // Assign the RenderWare version.
            heroesONEArchive.RwVersion = oneArchive.FileHeader.RenderWareVersion;

            // Build the file list.
            foreach (ONEFile oneFile in oneArchive.Files)
            {
                // Get the file details.
                ArchiveFile heroesOneArchiveFile = new ArchiveFile();
                heroesOneArchiveFile.CompressedData = oneFile.CompressedData;
                heroesOneArchiveFile.RwVersion = oneFile.ONEFileHeader.RwVersion;

                // Just to simplify following line.
                int fileNameIndex = oneFile.ONEFileHeader.FileNameIndex;
                heroesOneArchiveFile.Name = oneArchive.FileNames[fileNameIndex].ToString();

                heroesONEArchive.Files.Add(heroesOneArchiveFile);
            }

            return heroesONEArchive;
        }

        /// <summary>
        /// Generates a Heroes ONE archive from the provided HeroesONE Archive structure.
        /// </summary>
        /// <param name="heroesONEArchive">The ONE archive to write.</param>
        public static List<byte> GenerateONEArchive(ref Archive heroesONEArchive)
        {
            // Note: The file name section has 2 blank entries at file name index 0 and 1, we need to skip those.
            // Populate some data before we write it.
            ONEHeader fileHeader = new ONEHeader();
            ONEFileNameSectionHeader fileNameSectionHeader = new ONEFileNameSectionHeader();

            // Header
            fileHeader.RenderWareVersion = heroesONEArchive.RwVersion;
            fileHeader.FileSize = 0;
            fileHeader.Null = 0;

            // Name Header
            fileNameSectionHeader.RenderWareVersion = heroesONEArchive.RwVersion;
            fileNameSectionHeader.Unknown = 1;
            fileNameSectionHeader.FileNameSectionLength = ((heroesONEArchive.Files.Count + 2) * ONEFileName.FileNameLength);

            // Populate ArchiveFile names
            List<ONEFileName> fileNames = new List<ONEFileName>(ONEArchive.MAX_FILE_COUNT);

            // Insert blank entries
            fileNames.Add(new ONEFileName(""));
            fileNames.Add(new ONEFileName(""));

            for (int x = 0; x < heroesONEArchive.Files.Count; x++)
            { fileNames.Add(new ONEFileName(heroesONEArchive.Files[x].Name)); }

            // Populate ArchiveFile Structure
            List<ONEFile> oneFiles = new List<ONEFile>(heroesONEArchive.Files.Count);

            for (int x = 0; x < heroesONEArchive.Files.Count; x++)
            {
                ONEFile oneFile = new ONEFile();
                oneFile.CompressedData = heroesONEArchive.Files[x].CompressedData;
                oneFile.ONEFileHeader.FileNameIndex = x + 2; // ArchiveFile names always start after two blank entries, for some reason...
                oneFile.ONEFileHeader.FileSize = oneFile.CompressedData.Length;
                oneFile.ONEFileHeader.RwVersion = heroesONEArchive.Files[x].RwVersion;
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
    }
}
