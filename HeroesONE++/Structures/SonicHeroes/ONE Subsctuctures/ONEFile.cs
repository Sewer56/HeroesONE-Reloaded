namespace HeroesONE_R.Structures.SonicHeroes.ONE_Subsctuctures
{
    public struct ONEFile
    {
        /// <summary>
        /// Stores the header for this individual ONE file.
        /// </summary>
        public ONEFileHeader ONEFileHeader;

        /// <summary>
        /// Stores the PRS compressed data for the individual file.
        /// </summary>
        public byte[] CompressedData;
    }
}
