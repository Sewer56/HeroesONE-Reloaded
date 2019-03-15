using HeroesONE_R.Utilities;

namespace HeroesONE_R.Structures.Nights
{
    public unsafe struct ONENightsFileEntry
    {
        /// <summary>
        /// The order that the ONEFILE appears in the archive.
        /// </summary>
        public int FileIndex1 { get; set; }

        /// <summary>
        /// Size of the file after compression (in the ONE Archive).
        /// </summary>
        public int CompressedSize { get; set; }

        /// <summary>
        /// (Presumably: Did not test) The flag that determines whether the file is compressed.
        /// </summary>
        public int CompressedFlag { get; set; }

        /// <summary>
        /// Size of the file after decompression.
        /// </summary>
        public int DecompressedSize { get; set; }

        /// <summary>
        /// Appears to always be equal to <see cref="FileIndex1"/>
        /// </summary>
        public int FileIndex2 { get; set; }

        /// <summary>
        /// Offset of the file in the ONE Archive.
        /// Start of each file is 32 aligned.
        /// </summary>
        public int FileOffset { get; set; }

        /// <summary>
        /// Stores the name of the individual file. Ends with a null terminator.
        /// </summary>
        public fixed byte FileName[192];

        // The size of a compressed file may be calculated by the 
        // difference of the offset of this file and the next file.

        /// <summary>
        /// Constructor which copies a string into the fixed length buffer and sets index of file.
        /// </summary>
        /// <param name="name">The name to use for this ONE file.</param>
        /// <param name="index">Index of file (order in which file appears in archive).</param>
        public ONENightsFileEntry(string name, int index)
        {
            fixed (byte* fileNamePointer = FileName)
            {
                StringUtilities.StringToCharPointer(name, fileNamePointer);
            }

            FileIndex1 = index;
            FileIndex2 = index;
            CompressedFlag = 1;

            CompressedSize = 0;
            DecompressedSize = 0;
            FileOffset = 0;
        }

        /// <summary>
        /// Gets the name of the current ONE file as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            fixed (byte* fileName = FileName)
            {
                return StringUtilities.CharPointerToString(fileName);
            }
        }
    }
}
