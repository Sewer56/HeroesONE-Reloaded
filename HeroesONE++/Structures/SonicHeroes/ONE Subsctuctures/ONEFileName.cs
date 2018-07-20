using System.Text;

namespace HeroesONE_R.Structures.SonicHeroes.ONE_Subsctuctures
{
    /// <summary>
    /// Defines the file name of an individual file inside a .ONE archive.
    /// </summary>
    public unsafe struct ONEFileName
    {
        /// <summary>
        /// Contains the length of each filename.
        /// </summary>
        public const int FileNameLength = 64;

        /// <summary>
        /// Contains the actual filename of the file as ASCII encoded bytes.
        /// </summary>
        public fixed byte Name[FileNameLength];

        /// <summary>
        /// Constructor which copies a string into the fixed length buffer.
        /// </summary>
        /// <param name="name">The name to use for this ONE file.</param>
        public ONEFileName(string name)
        {
            // Get the name as ASCII bytes.
            byte[] asciiName = Encoding.ASCII.GetBytes(name);

            // Copy them over to structure.
            fixed (byte* localName = Name)
            {
                for (int x = 0; x < asciiName.Length; x++)
                {
                    localName[x] = asciiName[x];
                }
            }
        }

        /// <summary>
        /// Gets the name of the current ONE file as a string.
        /// </summary>
        /// <param name="array">The individual byte array to convert to string.</param>
        /// <returns></returns>
        public override string ToString()
        {
            // Get the file name.
            fixed (byte* fileName = this.Name)
            {
                // Calculate length before first null terminator.
                int fileNameLength = 0;
                while (true)
                {
                    if (fileName[fileNameLength] == 0) { break; }
                    fileNameLength += 1;
                }

                // Assign name.
                return Encoding.ASCII.GetString(fileName, fileNameLength);
            }
        }
    }
}
