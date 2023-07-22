using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using WaveFunction.structs;
using WaveFunction.Interfaces;

namespace WaveFunction.Tiles
{
    /// <summary>
    /// an advanced tile that can be used to create mazes and sets its texture based on its constraints
    /// </summary>
    public class Maze : Interfaces.ITile
    {
        private readonly bool[] sides = new bool[4];
        private GraphicsWrap.Interfaces.Basic.Iimage? image;
        private GraphicsWrap.Interfaces.Services.IRenderer? renderer;
        public int Width => Interfaces.ITile.DefaultWidth;

        public int Height => Interfaces.ITile.DefaultHeight;

        SideRules[] ITile.SideRules => SideRules;

        public readonly structs.SideRules[] SideRules = new structs.SideRules[4];

        public void Draw(int x, int y)
        {
            if (renderer is null)
            {
                return;
            }
            if (image is null)
            {
                //draw the missing tile
                TileRegistry.Missing.Draw(x, y);
                return;
            }
            renderer.DrawImage(image, x, y);
        }
        public void Init(GraphicsWrap.Interfaces.Services.IRenderer renderer)
        {
            this.renderer = renderer;
            //create the image
            ColorTile(Color.Black, Color.White);
        }
        public void ColorTile(Color Wall, Color Path){
            //if the renderer is null, then we can't do anything
            if (renderer is null) return;
            //create the image if it doesn't exist
            image ??= renderer.CreateImage(Width, Height);
            //set the wall color
            image.fillRect(0, 0, image.Width, image.Height, Wall);
            //if there are no paths, then we can just return
            if (!sides.Contains(true)) return;

            //we want a 1/5th border between paths and tile edges
            //first we need to draw the center, since all variants have a center
            image.fillRect(image.Width / 5, image.Height / 5, image.Width * 3 / 5, image.Height * 3 / 5, Path);
            //now we need to draw the paths
            if (sides[0])
            {
                //draw the top path
                image.fillRect(image.Width / 5, 0, image.Width * 3 / 5, image.Height / 5, Path);
            }
            if (sides[1])
            {
                //draw the right path
                image.fillRect(image.Width * 4 / 5, image.Height / 5, image.Width / 5, image.Height * 3 / 5, Path);
            }
            if (sides[2])
            {
                //draw the bottom path
                image.fillRect(image.Width / 5, image.Height * 4 / 5, image.Width * 3 / 5, image.Height / 5, Path);
            }
            if (sides[3])
            {
                //draw the left path
                image.fillRect(0, image.Height / 5, image.Width / 5, image.Height * 3 / 5, Path);
            }
        }

        //constructor
        public Maze(bool up, bool right, bool down, bool left)
        {
            //set the sides
            sides[0] = up;
            sides[1] = right;
            sides[2] = down;
            sides[3] = left;

            //set the side rules, for the maze tile, 0 is a wall, 1 is a path
            SideRules[0] = MakeRule(up);
            SideRules[1] = MakeRule(right);
            SideRules[2] = MakeRule(down);
            SideRules[3] = MakeRule(left);
        }
        //helper function to make a rule
        private static structs.SideRules MakeRule(bool isWall)
        {
            if (isWall)
                return new structs.SideRules(new int[] { 0 }, 0);
            else
                return new structs.SideRules(new int[] { 1 }, 1);
        }

        //the maze tile is special and needs to be registered differently, so it has its own register function
        public static void Register()
        {
            //register each possible variant
            TileRegistry.RegisterTile("MAZE-NNNN", new Maze(false, false, false, false));
            TileRegistry.RegisterTile("MAZE-NNNY", new Maze(false, false, false, true));
            TileRegistry.RegisterTile("MAZE-NNYN", new Maze(false, false, true, false));
            TileRegistry.RegisterTile("MAZE-NNYY", new Maze(false, false, true, true));
            TileRegistry.RegisterTile("MAZE-NYNN", new Maze(false, true, false, false));
            TileRegistry.RegisterTile("MAZE-NYNY", new Maze(false, true, false, true));
            TileRegistry.RegisterTile("MAZE-NYYN", new Maze(false, true, true, false));
            TileRegistry.RegisterTile("MAZE-NYYY", new Maze(false, true, true, true));
            TileRegistry.RegisterTile("MAZE-YNNN", new Maze(true, false, false, false));
            TileRegistry.RegisterTile("MAZE-YNNY", new Maze(true, false, false, true));
            TileRegistry.RegisterTile("MAZE-YNYN", new Maze(true, false, true, false));
            TileRegistry.RegisterTile("MAZE-YNYY", new Maze(true, false, true, true));
            TileRegistry.RegisterTile("MAZE-YYNN", new Maze(true, true, false, false));
            TileRegistry.RegisterTile("MAZE-YYNY", new Maze(true, true, false, true));
            TileRegistry.RegisterTile("MAZE-YYYN", new Maze(true, true, true, false));
            TileRegistry.RegisterTile("MAZE-YYYY", new Maze(true, true, true, true));
        }
    }
}