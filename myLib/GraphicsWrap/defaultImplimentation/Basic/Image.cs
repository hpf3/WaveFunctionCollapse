using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDL2;

namespace GraphicsWrap.defaultImplimentation.Basic
{
    internal class Image : Interfaces.Basic.Iimage
    {
        public readonly nint pointer;
        private readonly nint formatPointer;
        public readonly Interfaces.Basic.IPixelMap pixelMap;
        public Interfaces.Basic.IPixelMap PixelMap => pixelMap;

        public Image(nint surface)
        {
            this.pointer = surface;
            if (surface == IntPtr.Zero) throw new ArgumentNullException(nameof(surface));
            #pragma warning disable CS8605 // Unboxing a possibly null value.
            SDL.SDL_Surface surfaceStruct = (SDL.SDL_Surface)System.Runtime.InteropServices.Marshal.PtrToStructure(surface, typeof(SDL.SDL_Surface));
            #pragma warning restore CS8605 // Unboxing a possibly null value.
            pixelMap = new PixelMap(surfaceStruct);
            formatPointer = surfaceStruct.format;
        }
        public int Width
        {
            get
            {
                if (Nullcheck()) return 0;
                return pixelMap.Width;
            }
        }
        public int Height
        {
            get
            {
                if (Nullcheck()) return 0;
                return pixelMap.Height;
            }
        }
        private bool Nullcheck()
        {
            return pointer == IntPtr.Zero;
        }
        public void fillRect(int x, int y, int width, int height, byte r, byte g, byte b, byte a)
        {
            SDL2.SDL.SDL_Rect rect = new SDL.SDL_Rect();
            rect.x = x;
            rect.y = y;
            rect.w = width;
            rect.h = height;
            SDL2.SDL.SDL_FillRect(pointer, ref rect, SDL2.SDL.SDL_MapRGBA(formatPointer, r, g, b, a));
        }
        public void fillRect(int x, int y, int width, int height, System.Drawing.Color color)
        {
            fillRect(x, y, width, height, color.R, color.G, color.B, color.A);
        }
        ~Image()
        {
            SDL.SDL_FreeSurface(pointer);
        }
    }
}