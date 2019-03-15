namespace HeroesONE_R.Structures.Nights
{
    /// <summary>
    /// Contains the individual header of a Nights ONE Archive.
    /// </summary>
    public unsafe struct ONENightsHeader
    {
        public ONENightsFileIdentifier  Identifier  { get; set; }
        public int                      Null        { get; set; } // 204
        public ONENightsArchiveTypeIdentifier ArchiveIdentifier { get; set; }
        public int                      NumberOfFiles { get; set; }
        public int                      FileAlignment { get; set; } // Presumably alignment of files in archive.
        public fixed byte               Padding[128];
    }
}
