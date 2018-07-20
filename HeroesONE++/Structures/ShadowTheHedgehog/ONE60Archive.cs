using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesONE_R.Structures.SonicHeroes.ONE_Subsctuctures;

namespace HeroesONE_R.Structures.ShadowTheHedgehog
{
    /// <summary>
    /// Defines a Shadow The Hedgehog One Ver 0.60 archive structure.
    /// </summary>
    public unsafe struct ONE60Archive
    {
        /// <summary>
        /// The header for the ONE file.
        /// </summary>
        public ONEHeader FileHeader;

        /// <summary>
        /// Either "One Ver 0.60" or "One Ver 0.50"
        /// </summary>
        public fixed char OneVersion[16];

        /// <summary>
        /// States the amount of files present in this archive.
        /// </summary>
        public int FileCount;
    }
}
