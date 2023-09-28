namespace HeroesONE_R.Structures.SonicHeroes.ONE_Substructures
{
    public struct ONEFileHeader
    {
        /// <summary>
        /// Contains an index into the array of individual file names for files.
        /// Declares the file name for the individual file.
        /// </summary>
        public int FileNameIndex;

        /// <summary>
        /// Contains the size of the file in question.
        /// </summary>
        public int FileSize;

        /// <summary>
        /// Stores the RenderWare version of the individual file inside the ONE.
        /// </summary>
        public RWVersion RwVersion;
    }
}
