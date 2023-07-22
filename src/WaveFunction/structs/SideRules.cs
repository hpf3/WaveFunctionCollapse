using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveFunction.structs
{
    public readonly struct SideRules
    {
        public readonly int[] compatableSides;
        public readonly int mySide;
        public SideRules(int[] compatableSides, int mySide)
        {
            this.compatableSides = compatableSides;
            this.mySide = mySide;
        }
        public static bool IsCompatible(SideRules sideA, SideRules sideB)
        {
            return sideA.compatableSides.Contains(sideB.mySide) && sideB.compatableSides.Contains(sideA.mySide);
        }
    }
}