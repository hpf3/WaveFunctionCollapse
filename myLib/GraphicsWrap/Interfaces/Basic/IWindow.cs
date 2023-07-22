using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicsWrap.Interfaces.Basic;

public interface IWindow: IDisposable
{
    public Services.IRenderer Renderer { get; }
}
