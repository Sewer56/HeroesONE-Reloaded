using HeroesONE_R.Utilities;

namespace HeroesONE_R.Structures.ShadowTheHedgehog
{
    public unsafe struct ONE60FileEntry : IFileEntry
    {
        /// <summary>
        /// Stores the name of the individual file. Ends with a null terminator.
        /// </summary>
        public fixed byte FileName[44];

        /// <summary>
        /// Contains the size of the file in question (uncompressed!)
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// The offset of the individual file within this archive.
        /// Note: In Sanic Teem tools, the file is probably generated then the 0x0C header is 
        /// prepended before saving as with the other .ONE formats.
        /// Thus the offsets pointed by this format, if read, will be short 0x0C bytes.
        /// </summary>
        public int FileOffset { get; set; }
        
        /// Always 01
        public int EndOfBlock { get; set; }

        // The size of a compressed file may be calculated by the 
        // difference of the offset of this file and the next file.

        /// <summary>
        /// Constructor which copies a string into the fixed length buffer.
        /// </summary>
        /// <param name="name">The name to use for this ONE file.</param>
        public ONE60FileEntry(string name)
        {
            fixed (byte* fileNamePointer = FileName)
            {
                StringUtilities.StringToCharPointer(name, fileNamePointer);
            }

            FileSize = 0;
            FileOffset = 0;
            EndOfBlock = 1;
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
