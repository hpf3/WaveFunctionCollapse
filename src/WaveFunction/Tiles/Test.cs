using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphicsWrap.Interfaces.Services;
using GraphicsWrap.Interfaces.Basic;
using WaveFunction.structs;

namespace WaveFunction.Tiles
{
    public class Test : Interfaces.ITile
    {
        private Iimage? image;
        private IRenderer? renderer;
        public int Width => Interfaces.ITile.DefaultWidth;

        public int Height => Interfaces.ITile.DefaultHeight;

        public SideRules[] SideRules => throw new NotImplementedException();

        public void Draw(int x, int y)
        {
            if (image is null || renderer is null)
            {
                throw new NullReferenceException("image or renderer is null");
            }
            renderer.DrawImage(image, x, y);
        }

        public void Init(IRenderer renderer)
        {
            image = renderer.CreateImage(Width, Height);
            this.renderer = renderer;

            //red
            image.fillRect(0, 0, image.Width/2, image.Height/2, 255, 0, 0, 255);

            //green
            image.fillRect(image.Width/2, 0, image.Width/2, image.Height/2, 0, 255, 0, 255);

            //blue
            image.fillRect(0, image.Height/2, image.Width/2, image.Height/2, 0, 0, 255, 255);

            //white
            image.fillRect(image.Width/2, image.Height/2, image.Width/2, image.Height/2, 255, 255, 255, 255);
        }
    }
}