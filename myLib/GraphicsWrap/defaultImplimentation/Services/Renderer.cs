using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDL2;
namespace GraphicsWrap.defaultImplimentation.Services
{
    public class Renderer : Interfaces.Services.IRenderer
    {
        private readonly nint _renderer;
        //constructor
        public Renderer(nint window, Interfaces.Services.RendererFlags renderFlags=(Interfaces.Services.RendererFlags.RENDERER_ACCELERATED | Interfaces.Services.RendererFlags.RENDERER_PRESENTVSYNC))
        {
            _renderer = SDL.SDL_CreateRenderer(window, -1, (SDL.SDL_RendererFlags)renderFlags);
        }
        //render settings
        public void SetDrawColor(int r, int g, int b, int a) => SDL.SDL_SetRenderDrawColor(_renderer, (byte)r, (byte)g, (byte)b, (byte)a);
        public void SetDrawColor(int r, int g, int b) => SDL.SDL_SetRenderDrawColor(_renderer, (byte)r, (byte)g, (byte)b, 255);

        //Object creation
        public Interfaces.Basic.Iimage CreateImage(int width, int height)
        {
            var surface = SDL.SDL_CreateRGBSurface(0, width, height, 32, 0xFF000000, 0x00FF0000, 0x0000FF00, 0x000000FF);
            return new Basic.Image(surface);
        }
        //create image from file
        public Interfaces.Basic.Iimage CreateImage(string path)
        {
            //make sure the file exists
            if (!System.IO.File.Exists(path)) throw new System.IO.FileNotFoundException("File not found", path);
            var surface = SDL2.SDL_image.IMG_Load(path);
            return new Basic.Image(surface);
        }

        //rendering
        public void Clear() => SDL.SDL_RenderClear(_renderer);
        public void Present() => SDL.SDL_RenderPresent(_renderer);
        public void DrawImage(Interfaces.Basic.Iimage image, int x, int y)
        {
            var surface = (Basic.Image)image;
            SDL.SDL_Rect dest = new()
            {
                x = x,
                y = y,
                w = surface.Width,
                h = surface.Height
            };
            var texture = SDL.SDL_CreateTextureFromSurface(_renderer, surface.pointer);
            SDL.SDL_RenderCopy(_renderer, texture, IntPtr.Zero, ref dest);
            SDL.SDL_DestroyTexture(texture);
        }

        //disposal
        public void Dispose()
        {
            SDL.SDL_DestroyRenderer(_renderer);
        }
    }
}