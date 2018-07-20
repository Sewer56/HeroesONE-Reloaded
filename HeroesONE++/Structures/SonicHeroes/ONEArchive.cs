using System;
using System.Collections.Generic;
using System.Linq;
using HeroesONE_R.Structures.SonicHeroes.ONE_Subsctuctures;
using HeroesONE_R.Utilities;

namespace HeroesONE_R.Structures.SonicHeroes
{
    public unsafe struct ONEArchive
    {
        /// <summary>
        /// The maximum number of files, and default in file name section used.
        /// </summary>
        public const int MAX_FILE_COUNT = 256;

        /// <summary>
        /// The header for the ONE file.
        /// </summary>
        public ONEHeader FileHeader;

        /// <summary>
        /// Contains the header which describes the upcoming file name section.
        /// </summary>
        public ONEFileNameSectionHeader FileNameSectionHeader;

        /// <summary>
        /// Stores an array of usernames containing all of the individual names of 
        /// files inside ONE archives.
        /// </summary>
        public ONEFileName[] FileNames;

        /// <summary>
        /// Array of the actual file structures used within the game.
        /// Equivalent to the file count.
        /// </summary>
        public List<ONEFile> Files;

        public ONEArchive(ref byte[] file)
        {
            this = ParseONEFile(ref file);
        }

        /// <summary>
        /// Parses a Sonic Heroes ONE file from a byte array and returns a ONE file structure back.
        /// </summary>
        /// <returns>The structure of a ONE Archive.</returns>
        // ReSharper disable once InconsistentNaming
        public static ONEArchive ParseONEFile(ref byte[] file)
        {
            ONEArchive oneArchive = new ONEArchive();

            // Stores a pointer, increasing as we read the individual file structures.
            int pointer = 0;

            // Parse the header and individual sections.
            oneArchive.FileHeader = StructUtilities.ArrayToStructureUnsafe<ONEHeader>(ref file, pointer, ref pointer);
            oneArchive.FileNameSectionHeader = StructUtilities.ArrayToStructureUnsafe<ONEFileNameSectionHeader>(ref file, pointer, ref pointer);

            // Parse all of the filenames.
            int fileNameCount = oneArchive.FileNameSectionHeader.GetNameCount();
            oneArchive.FileNames = new ONEFileName[fileNameCount];

            for (int x = 0; x < fileNameCount; x++)
                oneArchive.FileNames[x] = StructUtilities.ArrayToStructureUnsafe<ONEFileName>(ref file, pointer, ref pointer);

            // Parse all of the files.
            oneArchive.Files = new List<ONEFile>(fileNameCount);
            int fileCount = oneArchive.FileNames.Count(x => x.ToString() != "");

            // Some ONE files have been padded at the end of file, thus reading until the end of file may fail - only take as many files as we have filenames.
            while ((pointer < file.Length) && (oneArchive.Files.Count < fileCount))
            {
                // Create ONE ArchiveFile
                ONEFile oneFile = new ONEFile();

                // Parse the header.
                oneFile.ONEFileHeader = StructUtilities.ArrayToStructureUnsafe<ONEFileHeader>(ref file, pointer, ref pointer);

                // Now parse the data.
                oneFile.CompressedData = new byte[oneFile.ONEFileHeader.FileSize];
                Array.Copy(file, pointer, oneFile.CompressedData, 0, oneFile.CompressedData.Length);
                pointer += oneFile.CompressedData.Length;

                oneArchive.Files.Add(oneFile);
            }

            return oneArchive;
        }
    }
}
