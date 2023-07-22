using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphicsWrap.Interfaces.Basic;
using GraphicsWrap.Interfaces.Factories;

namespace GraphicsWrap.defaultImplimentation.Factories;
public class WindowFactory : IWindowFactory
{
    //window variables
    private string title;
    private int width;
    private int height;
    private Interfaces.Services.RendererFlags renderFlags=(Interfaces.Services.RendererFlags.RENDERER_ACCELERATED | Interfaces.Services.RendererFlags.RENDERER_PRESENTVSYNC);

    //constructor
    public WindowFactory(string Title, int Width, int Height)
    {
        title = Title;
        width = Width;
        height = Height;
    }
    //empty constructor
    public WindowFactory()
    {
        title = "default";
        width = 640;
        height = 480;
    }

    //variable setters
    public IWindowFactory SetTitle(string Title)
    {
        title = Title;
        return this;
    }
    public IWindowFactory SetWidth(int Width)
    {
        width = Width;
        return this;
    }
    public IWindowFactory SetHeight(int Height)
    {
        height = Height;
        return this;
    }
    public IWindowFactory SetRenderFlags(Interfaces.Services.RendererFlags RenderFlags)
    {
        renderFlags = RenderFlags;
        return this;
    }
    //builder
    public IWindow CreateWindow()
    {
        return new Basic.Window(title, width, height);
    }
    public IWindow CreateWindow(string Title, int Width, int Height)
    {
        return new Basic.Window(Title, Width, Height, renderFlags);
    }
}
