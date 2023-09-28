using System.Text;

namespace HeroesONE_R.Utilities
{
    public static unsafe class StringUtilities
    {
        /// <summary>
        /// Converts a fixed array of null terminated chars into a string instance.
        /// </summary>
        /// <param name="fileName">Pointer to the filename to be deciphered and returned.</param>
        /// <returns></returns>
        public static string CharPointerToString(byte* fileName)
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

        /// <summary>
        /// Writes a string to a specified char pointer in ASCII format.
        /// </summary>
        /// <param name="text">The text to write to the pointer.</param>
        /// <param name="pointer">The pointer to write to.</param>
        public static void StringToCharPointer(string text, byte* pointer)
        {
            // Get the name as ASCII bytes.
            byte[] asciiText = Encoding.ASCII.GetBytes(text);

            // Copy them over to structure.
            for (int x = 0; x < asciiText.Length; x++)
            {
                pointer[x] = asciiText[x];
            }
        }
    }
}
