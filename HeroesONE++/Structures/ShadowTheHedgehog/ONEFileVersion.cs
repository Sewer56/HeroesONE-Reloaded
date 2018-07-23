using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesONE_R.Utilities;

namespace HeroesONE_R.Structures.ShadowTheHedgehog
{
    /// <summary>
    /// Simple wrapper for the ONE File version; used simply to make parsing code easier.
    /// </summary>
    public unsafe struct ONEFileVersion
    {
        /// <summary>
        /// Either "One Ver 0.60" or "One Ver 0.50"
        /// Ends with a null terminator.
        /// </summary>
        public fixed byte OneVersion[16];

        /// <summary>
        /// Constructor which copies a string into the fixed length buffer.
        /// </summary>
        /// <param name="text">The text to use for this ONE File Version identifier.</param>
        public ONEFileVersion(string text)
        {
            fixed (byte* fileNamePointer = OneVersion)
            {
                StringUtilities.StringToCharPointer(text, fileNamePointer);
            }
        }

        /// <summary>
        /// Gets the name of the current ONE file as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            fixed (byte* fileName = OneVersion)
            {
                return StringUtilities.CharPointerToString(fileName);
            }
        }
    }
}
