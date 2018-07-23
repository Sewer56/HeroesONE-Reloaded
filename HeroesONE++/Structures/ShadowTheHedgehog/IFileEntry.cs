namespace HeroesONE_R.Structures.ShadowTheHedgehog
{
    /// <summary>
    /// Defines the individual set assignable fields of an individual Shadow The Edgehog file entry.
    /// </summary>
    public interface IFileEntry
    {
        int EndOfBlock { get; set; }
        int FileOffset { get; set; }
        int FileSize { get; set; }
        string ToString();
    }
}