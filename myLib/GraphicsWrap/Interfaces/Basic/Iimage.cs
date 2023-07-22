using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicsWrap.Interfaces.Basic
{
    public interface Iimage
    {
        public int Width { get; }
        public int Height { get; }
        public IPixelMap PixelMap { get; }
        public void fillRect(int x, int y, int width, int height, byte r, byte g, byte b, byte a);
        public void fillRect(int x, int y, int width, int height, System.Drawing.Color color);
        //TODO: add methods
    }
}