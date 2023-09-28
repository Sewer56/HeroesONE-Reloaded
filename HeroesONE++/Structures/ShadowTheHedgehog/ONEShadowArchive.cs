using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HeroesONE_R.Structures.Common;
using HeroesONE_R.Structures.SonicHeroes.ONE_Substructures;
using HeroesONE_R.Structures.Substructures;
using HeroesONE_R.Utilities;

namespace HeroesONE_R.Structures.ShadowTheHedgehog
{
    /// <summary>
    /// Defines a Shadow The Hedgehog One Ver 0.50/0.60 archive structure.
    /// (The only difference between the two fornats is the file entry structure,
    /// defined by the delegate type).
    /// </summary>
    public unsafe struct ONEShadowArchive 
    {
        /// <summary>
        /// The header for the ONE file.
        /// </summary>
        public ONEHeader FileHeader;

        /// <summary>
        /// Simple wrapper for the ONE File version; used simply to make parsing code easier.
        /// Contains a fixed byte array of either "One Ver 0.60" or "One Ver 0.50"
        /// Ends with a null terminator.
        /// </summary>
        public ONEFileVersion OneVersion;

        /// <summary>
        /// States the amount of files present in this archive.
        /// </summary>
        public int FileCount;

        /// <summary>
        /// 1 byte padding of 0x00 and
        /// 31 byte padding of 0xCD
        /// </summary>
        public ONEPadding OnePadding;

        /// <summary>
        /// Starts with two completely empty file entries.
        /// </summary>
        public List<IFileEntry> Files;

        /// <summary>
        /// Stores all of the individual files' raw data to be decompressed.
        /// The length of the raw data is calculated by next file's - previous file's offset
        /// </summary>
        public List<byte[]> FileData;

        /// <summary>
        /// Instantiates a Shadow the Hedgehog ONE file; which can be of either version 0.50 or 0.60.
        /// </summary>
        /// <param name="file"></param>
        public ONEShadowArchive(ref byte[] file)
        {
            this = ParseONEFile(ref file);
        }

        /// <summary>
        /// Parses a Shadow The Hedgehog ONE file from a byte array and returns a ONE file structure back.
        /// </summary>
        /// <returns>The structure of a ONE Archive.</returns>
        // ReSharper disable once InconsistentNaming
        public static ONEShadowArchive ParseONEFile(ref byte[] file)
        {
            // Know if we're dealing with Shadow050 or Shadow060
            ONEArchiveType archiveType = ONEArchiveTester.GetArchiveType(ref file);

            // Do not accept Heroes archives.
            if (archiveType == ONEArchiveType.Heroes)
            { throw new ArgumentException("The supplied .ONE file does not appear to be a Shadow The Hedgehog .ONE file"); }

            // Instantiate either a Shadow 050 or Shadow 060 archive. 
            ONEShadowArchive oneShadowArchive = new ONEShadowArchive();
            
            // Pointer for our file.
            int pointer = 0;

            oneShadowArchive.FileHeader = StructUtilities.ArrayToStructureUnsafe<ONEHeader>(ref file, pointer, ref pointer);
            oneShadowArchive.OneVersion = StructUtilities.ArrayToStructureUnsafe<ONEFileVersion>(ref file, pointer, ref pointer);
            oneShadowArchive.FileCount  = StructUtilities.ArrayToStructureUnsafe<int>(ref file, pointer, ref pointer);
            oneShadowArchive.OnePadding = StructUtilities.ArrayToStructureUnsafe<ONEPadding>(ref file, pointer, ref pointer);

            // Populate the file list.
            oneShadowArchive.Files = new List<IFileEntry>();

            // Populate the individual files.
            // Here we will filter out our dummies by 
            for (int x = 0; x < oneShadowArchive.FileCount + 2; x++)
            {
                if (archiveType == ONEArchiveType.Shadow050)
                {
                    ONE50FileEntry entry = StructUtilities.ArrayToStructureUnsafe<ONE50FileEntry>(ref file, pointer, ref pointer);

                    if (entry.EndOfBlock == 1)
                        oneShadowArchive.Files.Add(entry);
                }
                else
                {
                    ONE60FileEntry entry = StructUtilities.ArrayToStructureUnsafe<ONE60FileEntry>(ref file, pointer, ref pointer);

                    if (entry.EndOfBlock == 1)
                        oneShadowArchive.Files.Add(entry);
                }
            }

            // Get individual file data.
            oneShadowArchive.FileData = new List<byte[]>(oneShadowArchive.FileCount);

            /*
                From observation we can deduce that the first 0xC length header present at the start of the file              
                is not technically part of the .ONE Archive structure itself for any of the .ONE variations but is
                instead, metadata preprended to each of the files once they have been generated. The first always empty
                integer is probably simply acting as a reserved value.

                Thus all of the offsets present inside the ONE Files are actually incorrect as they are read here due to this
                and offset from the true location inside the actual file by the length of the header in question.

                We must take this into consideration when obtaining the length of the last file and the offset of each of the individual files.
            */
            int headerSize = Marshal.SizeOf<ONEHeader>();
            int trueFileSize = file.Length - headerSize;

            for (int x = 0; x < oneShadowArchive.Files.Count; x++)
            {
                // Define our locals
                int length;
                int offset;
                byte[] compressedData;

                // Determine our locals.
                if (x != oneShadowArchive.Files.Count - 1) { length = oneShadowArchive.Files[x + 1].FileOffset - oneShadowArchive.Files[x].FileOffset; }
                else { length = trueFileSize - oneShadowArchive.Files[x].FileOffset; } // Last file needs to count from the end of file.

                // Set our offset.
                offset = oneShadowArchive.Files[x].FileOffset + headerSize;

                // Create managed byte array and copy into it.
                compressedData = new byte[length];
                Array.Copy(file, offset, compressedData, 0, length);

                // We're done here.
                oneShadowArchive.FileData.Add(compressedData);
            }

            return oneShadowArchive;
        }

        /// <summary>
        /// Parses a Sonic Heroes/Shadow ONE Archive and retrieves a HeroesONE archive,
        /// simpler and more optimized for manipulation of individual files.
        /// </summary>
        /// <returns>A HeroesONE archive that makes manipulating ONE archives easy!</returns>
        // ReSharper disable once InconsistentNaming
        public unsafe Archive GetArchive()
        {
            // New archive
            Archive heroesONEArchive = new Archive(FileHeader.RenderWareVersion);
            heroesONEArchive.Files = new List<ArchiveFile>(Files.Count);

            // Build the file list.
            for (int x = 0; x < Files.Count; x++)
            {
                // Get the file details.
                ArchiveFile heroesOneArchiveFile = new ArchiveFile();
                
                // Set the file details.
                heroesOneArchiveFile.RwVersion = new RWVersion();
                heroesOneArchiveFile.CompressedData = FileData[x];
                heroesOneArchiveFile.Name = Files[x].ToString();

                // Append the file to list.
                heroesONEArchive.Files.Add(heroesOneArchiveFile);
            }

            return heroesONEArchive;
        }
    }
}
