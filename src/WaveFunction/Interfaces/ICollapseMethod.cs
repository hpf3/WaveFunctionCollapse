using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveFunction.Interfaces
{
    public interface ICollapseMethod
    {
        /// <summary>
        /// Performs a single collapse step
        /// </summary>
        /// <param name="map">the Wavemap to collapse</param>
        public void Collapse(WaveMap map);
    }
}