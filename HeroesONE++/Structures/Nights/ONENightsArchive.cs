using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HeroesONE_R.Structures.Common;
using HeroesONE_R.Structures.ShadowTheHedgehog;
using HeroesONE_R.Structures.SonicHeroes.ONE_Subsctuctures;
using HeroesONE_R.Structures.Subsctructures;
using HeroesONE_R.Utilities;

namespace HeroesONE_R.Structures.Nights
{
    /// <summary>
    /// Tool to convert Dreams ONE archive format into <see cref="Archive"/>.
    /// </summary>
    public unsafe struct ONENightsArchive
    {
        /// <summary>
        /// The header of the Nights archive format.
        /// </summary>
        public ONENightsHeader Header;

        /// <summary>
        /// Stores all of the file entries for Nights Files.
        /// </summary>
        public List<ONENightsFileEntry> Files;

        /// <summary>
        /// Stores all of the individual files' raw data to be decompressed.
        /// </summary>
        public List<byte[]> FileData;

        /// <summary>
        /// Instantiates a Nights ONE file.
        /// </summary>
        public ONENightsArchive(ref byte[] file)
        {
            this = ParseONEFile(ref file);
        }

        /// <summary>
        /// Parses a Nights ONE file from a byte array and returns a ONE file structure back.
        /// </summary>
        /// <returns>The structure of a ONE Archive.</returns>
        // ReSharper disable once InconsistentNaming
        public static ONENightsArchive ParseONEFile(ref byte[] file)
        {
            // Know if we're dealing with a nights archive.
            ONEArchiveType archiveType = ONEArchiveTester.GetArchiveType(ref file);

            if (archiveType != ONEArchiveType.Nights)
                throw new ArgumentException("The supplied .ONE file does not appear to be a Nights: Journey Into Dreams .ONE file");

            ONENightsArchive oneNightsArchive = new ONENightsArchive();
            int pointer = 0;

            oneNightsArchive.Header = StructUtilities.ArrayToStructureUnsafe<ONENightsHeader>(ref file, pointer, ref pointer);

            // Populate the file list.
            oneNightsArchive.Files = new List<ONENightsFileEntry>();

            // Populate the individual files.
            for (int x = 0; x < oneNightsArchive.Header.NumberOfFiles; x++)
                oneNightsArchive.Files.Add(StructUtilities.ArrayToStructureUnsafe<ONENightsFileEntry>(ref file, pointer, ref pointer));

            oneNightsArchive.FileData = new List<byte[]>(oneNightsArchive.Header.NumberOfFiles);

            // Read files
            foreach (var archiveFile in oneNightsArchive.Files)
            {
                byte[] compressedData = new byte[archiveFile.CompressedSize];
                Array.Copy(file, archiveFile.FileOffset, compressedData, 0, archiveFile.CompressedSize);
                oneNightsArchive.FileData.Add(compressedData);
            }

            return oneNightsArchive;
        }

        /// <summary>
        /// Parses a Sonic Heroes/Shadow ONE Archive and retrieves a HeroesONE archive,
        /// simpler and more optimized for manipulation of individual files.
        /// </summary>
        /// <returns>A HeroesONE archive that makes manipulating ONE archives easy!</returns>
        // ReSharper disable once InconsistentNaming
        public unsafe Archive GetArchive()
        {
            Archive heroesOneArchive = new Archive(CommonRWVersions.Null);
            heroesOneArchive.Files   = new List<ArchiveFile>(Files.Count);

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
                heroesOneArchive.Files.Add(heroesOneArchiveFile);
            }

            return heroesOneArchive;
        }
    }
}
