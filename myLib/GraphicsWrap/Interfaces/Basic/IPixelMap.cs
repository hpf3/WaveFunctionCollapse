using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicsWrap.Interfaces.Basic
{
    public interface IPixelMap
    {
        public int Width { get; }
        public int Height { get; }
        public int Pitch { get; }
        public void SetValue(structs.PixelChannel channel, int x, int y, byte value);
        public byte GetValue(structs.PixelChannel channel, int x, int y);
    }
}