using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphicsWrap.Interfaces.Services;
using GraphicsWrap.Interfaces.Basic;

namespace WaveFunction.Tiles
{
    public class Missing: Interfaces.ITile
    {
        private Iimage? image;
        private IRenderer? renderer;
        private const string path = "/question-mark.png";
        public int Width => image?.Width ?? Interfaces.ITile.DefaultWidth;

        public int Height => image?.Height ?? Interfaces.ITile.DefaultHeight;

        public structs.SideRules[] SideRules => throw new NotImplementedException();

        public void Draw(int x, int y)
        {
            if (image is null || renderer is null)
            {
                return;
            }
            renderer.DrawImage(image, x, y);
        }

        public void Init(IRenderer renderer)
        {
            //convert relative path to absolute path
            var absolutePath = Main.ProgramPath+path;
            image = renderer.CreateImage(absolutePath);
            this.renderer = renderer;
        }
    }
}