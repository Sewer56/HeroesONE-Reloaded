using HeroesONE_R.Utilities;

namespace HeroesONE_R.Structures.Nights
{
    /// <summary>
    /// Stores the 16 byte ONE file identifier as a structure.
    /// </summary>
    public unsafe struct ONENightsFileIdentifier
    {
        /// <summary>
        /// Constant string "ThisIsOneFile".
        /// </summary>
        public fixed byte OneIdentifier[16];

        /// <summary>
        /// Constructor which copies a string into the fixed length buffer.
        /// </summary>
        /// <param name="text">The text to use for this ONE File Identifier.</param>
        public ONENightsFileIdentifier(string text)
        {
            fixed (byte* identifierPointer = OneIdentifier)
            {
                StringUtilities.StringToCharPointer(text, identifierPointer);
            }
        }

        /// <summary>
        /// Gets the name of the identifier as a string.
        /// </summary>
        public override string ToString()
        {
            fixed (byte* identifierPointer = OneIdentifier)
            {
                return StringUtilities.CharPointerToString(identifierPointer);
            }
        }
    }
}
