using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using GraphicsWrap.structs;

namespace GraphicsWrap.defaultImplimentation.Basic
{
    public readonly struct PixelMap : Interfaces.Basic.IPixelMap
    {
        private readonly nint pointer;
        public int Width { get; }
        public int Height { get; }
        public int Pitch { get; }
        public PixelMap(SDL2.SDL.SDL_Surface surfaceStruct)
        {
            Width = surfaceStruct.w;
            Height = surfaceStruct.h;
            Pitch = surfaceStruct.pitch;
            pointer = surfaceStruct.pixels;
        }
        public readonly void SetValue(structs.PixelChannel channel, int x, int y, byte value)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height) return;
            var pixel = pointer + (y * Pitch) + (x * 4);
            int channelOffset = (int)channel;
            Marshal.WriteByte(pixel, channelOffset, value);

            // Add some debugging statements
            Console.WriteLine($"SetValue called for channel {channel} at ({x}, {y}) with value {value}");
            Console.WriteLine($"Pixel value at ({x}, {y}): R={GetValue(PixelChannel.R, x, y)}, G={GetValue(PixelChannel.G, x, y)}, B={GetValue(PixelChannel.B, x, y)}, A={GetValue(PixelChannel.A, x, y)}");
        }
        public readonly byte GetValue(structs.PixelChannel channel, int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height) return 0;
            var pixel = pointer + (y * Pitch) + (x * 4);
            int channelOffset = (int)channel;
            return Marshal.ReadByte(pixel, channelOffset);
        }
    }
}