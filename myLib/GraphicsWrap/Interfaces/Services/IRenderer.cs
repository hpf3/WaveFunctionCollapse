using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicsWrap.Interfaces.Services;

public interface IRenderer:IDisposable
{
    //render settings
    public void SetDrawColor(int r, int g, int b, int a);
    public void SetDrawColor(int r, int g, int b);

    //Object creation
    public Interfaces.Basic.Iimage CreateImage(int width, int height);
    public Interfaces.Basic.Iimage CreateImage(string path);

    //rendering
    public void Clear();
    public void Present();
    public void DrawImage(Interfaces.Basic.Iimage image, int x, int y);
}
#pragma warning disable RCS1135 // Declare enum member with zero value (when enum has FlagsAttribute).
[Flags]
public enum RendererFlags : uint
{
    RENDERER_SOFTWARE = 0x00000001,
    RENDERER_ACCELERATED = 0x00000002,
    RENDERER_PRESENTVSYNC = 0x00000004,
    RENDERER_TARGETTEXTURE = 0x00000008
}
#pragma warning restore RCS1135 // Declare enum member with zero value (when enum has FlagsAttribute).