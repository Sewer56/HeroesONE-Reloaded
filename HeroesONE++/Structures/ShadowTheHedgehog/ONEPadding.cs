namespace HeroesONE_R.Structures.ShadowTheHedgehog
{
    /// <summary>
    /// Stores the padding used in each Shadow The Hedgehog ONE file after the initial headers.
    /// </summary>
    public unsafe struct ONEPadding
    {
        public const int PADDING_LENGTH = 32;

        /// <summary>
        /// 1 byte padding of 0x00 and
        /// 31 byte padding of 0xCD
        /// </summary>
        public fixed byte Padding[PADDING_LENGTH];
    }
}
