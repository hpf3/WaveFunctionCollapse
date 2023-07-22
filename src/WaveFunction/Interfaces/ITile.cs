using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveFunction.Interfaces
{
    public interface ITile
    {
        public const int DefaultWidth = 20;
        public const int DefaultHeight = 20;
        public int Width { get; }
        public int Height { get; }
        //up, right, down, left
        public structs.SideRules[] SideRules { get; }
        public void Init(GraphicsWrap.Interfaces.Services.IRenderer renderer);
        public void Draw(int x, int y);
    }
}