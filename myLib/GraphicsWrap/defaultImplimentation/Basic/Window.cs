using System.Data.SqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDL2;

namespace GraphicsWrap.defaultImplimentation.Basic;

public class Window: Interfaces.Basic.IWindow
{
    private readonly Interfaces.Services.RendererFlags _RenderFlags;
    private readonly nint _window;
    public Interfaces.Services.IRenderer Renderer { get; }
     public Window(string Title, int Width, int Height, Interfaces.Services.RendererFlags renderFlags=(Interfaces.Services.RendererFlags.RENDERER_ACCELERATED | Interfaces.Services.RendererFlags.RENDERER_PRESENTVSYNC))
     {
         _window = SDL.SDL_CreateWindow(Title, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, Width, Height, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
         _RenderFlags = renderFlags;
          Renderer = new Services.Renderer(_window, _RenderFlags);
     }

        public void Dispose()
        {
            Renderer.Dispose();
            SDL.SDL_DestroyWindow(_window);
        }
}
