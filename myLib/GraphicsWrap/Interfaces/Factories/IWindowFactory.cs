using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphicsWrap.Interfaces.Basic;
namespace GraphicsWrap.Interfaces.Factories;

public interface IWindowFactory
{
    //variable setters
    public IWindowFactory SetTitle(string Title);
    public IWindowFactory SetWidth(int Width);
    public IWindowFactory SetHeight(int Height);
    public IWindowFactory SetRenderFlags(Interfaces.Services.RendererFlags RenderFlags);
    //builder
    public IWindow CreateWindow();
    public IWindow CreateWindow(string Title, int Width, int Height);
}
