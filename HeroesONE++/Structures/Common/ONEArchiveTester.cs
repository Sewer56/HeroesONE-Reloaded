using HeroesONE_R.Structures.SonicHeroes.ONE_Substructures;
using HeroesONE_R.Utilities;

namespace HeroesONE_R.Structures.Common
{
    public unsafe struct ONEArchiveTester
    {
        /// <summary>
        /// The header for the ONE file.
        /// </summary>
        public ONEHeader FileHeader;

        /// <summary>
        /// Either "One Ver 0.60" or "One Ver 0.50"
        /// Ends with a null terminator.
        /// </summary>
        public fixed byte OneVersion[16];

        /// <summary>
        /// Checks the type of archive used by comparing the individual version strings within the supplied
        /// archive set of bytes and returns an enumerable stating which .ONE Archive type the supplied archive is.
        /// </summary>
        /// <param name="file">The individual array of bytes which contains a .ONE file.</param>
        /// <returns></returns>
        public static ONEArchiveType GetArchiveType(ref byte[] file)
        {
            // Parse the array of bytes into the tester structure and compare the version string used.
            // If neither of the Shadow formats match, assume Heroes format.
            ONEArchiveTester oneArchiveTester = StructUtilities.ArrayToStructureUnsafe<ONEArchiveTester>(ref file);

            // Dump string.
            string oneVersion = StringUtilities.CharPointerToString(oneArchiveTester.OneVersion);

            switch (oneVersion)
            {
                case "One Ver 0.50":
                    return ONEArchiveType.Shadow050;

                case "One Ver 0.60":
                    return ONEArchiveType.Shadow060;

                default:
                    return ONEArchiveType.Heroes;
            }
        }
    }

    /// <summary>
    /// Defines the individual archive types that the .ONE file may belong to.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public enum ONEArchiveType
    {
        /// <summary>
        /// The default
        /// </summary>
        Heroes,

        /// <summary>
        /// One Ver 0.50
        /// </summary>
        Shadow050,

        /// <summary>
        /// One Ver 0.60
        /// </summary>
        Shadow060
    }
}
