using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.SonicHeroes;

namespace HeroesONE_R
{
    public class Test
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            byte[] oneFile = System.IO.File.ReadAllBytes("en_pawn.one");
            byte[] compressed = csharp_prs.Prs.Compress(ref oneFile, 0x1FFF);

            Console.WriteLine("ayy " + oneFile.Length + " " + compressed.Length);
            Console.ReadLine();
        }
    }
}
