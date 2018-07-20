using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesONE_R.Structures
{
    /// <summary>
    /// A simple front-end for dlang-prs' front-end which is used to globally control the passed in
    /// search buffer size and automatically release memory.
    /// </summary>
    public static class Prs
    {
        /// <summary>
        /// Controls the size of the search buffer used for compressing files passed into this class.
        /// Range: 0xFF - 0x1FFF
        /// </summary>
        public static int SEARCH_BUFFER_SIZE = 0x1FFF;

        /// <summary>
        /// Decides the size of the search buffer on the size of the file to optimize for performance/quick operation.
        /// </summary>
        public static bool ADAPTIVE_SEARCH_BUFFER = true;

        /// <summary>
        /// Compresses the passed in byte array with the PRS Compression algorithm.
        /// Note that you may change the search buffer size to your liking to fine-tune between compression ratio and speed.
        /// </summary>
        /// <param name="data">Contains the data to be compressed.</param>
        /// <returns>Compressed copy of the passed in byte array.</returns>
        public static byte[] Compress(ref byte[] data)
        {
            // Yay mode switching!
            if (ADAPTIVE_SEARCH_BUFFER)
            {
                // An approximate size in kilobytes of the file, rounded down to lower 100
                int sizeKB = data.Length / 100;

                if (IsWithin(sizeKB, 0, 250))
                    return csharp_prs.Prs.Compress(ref data, 0x1FFF);

                if (IsWithin(sizeKB, 250, 500))
                    return csharp_prs.Prs.Compress(ref data, 0x1C00);

                if (IsWithin(sizeKB, 500, 750))
                    return csharp_prs.Prs.Compress(ref data, 0x1800);

                if (IsWithin(sizeKB, 750, 1000))
                    return csharp_prs.Prs.Compress(ref data, 0x1400);

                if (IsWithin(sizeKB, 1000, 1250))
                    return csharp_prs.Prs.Compress(ref data, 0x1000);

                if (IsWithin(sizeKB, 1250, 1500))
                    return csharp_prs.Prs.Compress(ref data, 0xC00);

                if (IsWithin(sizeKB, 2000, 3000))
                    return csharp_prs.Prs.Compress(ref data, 0x800);

                return csharp_prs.Prs.Compress(ref data, 0x400);
            }
            else
            {
                return csharp_prs.Prs.Compress(ref data, SEARCH_BUFFER_SIZE);
            }
        }

        /// <summary>
        /// Decompresses the passed in byte array with the PRS Compression algorithm.
        /// </summary>
        /// <param name="data">The data to be decompressed.</param>
        /// <returns>Decompressed copy of the data.</returns>
        public static byte[] Decompress(ref byte[] data)
        {
            return csharp_prs.Prs.Decompress(ref data);
        }

        /// <summary>
        /// Checks whether a number lies within a certain range of numbers.
        /// </summary>
        /// <param name="value">The number to compare.</param>
        /// <param name="minimum">The minimum value to check.</param>
        /// <param name="maximum">The maximum value to check.</param>
        /// <returns>The number</returns>
        public static bool IsWithin(this int value, int minimum, int maximum)
        {
            return value >= minimum && value <= maximum;
        }
    }
}
