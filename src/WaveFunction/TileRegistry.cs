using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveFunction
{
    public static class TileRegistry
    {
        public static System.Collections.Concurrent.ConcurrentDictionary<string, Interfaces.ITile> Tiles { get; } = new();
        public static readonly Interfaces.ITile Missing = new Tiles.Missing();
        public static void RegisterTile(string name, Interfaces.ITile tile)
        {
            Tiles.TryAdd(name, tile);
        }
        public static Interfaces.ITile GetTile(string name)
        {
            return Tiles[name];
        }
        public static void InitAll(GraphicsWrap.Interfaces.Services.IRenderer renderer)
        {
            Missing.Init(renderer);
            foreach (var tile in Tiles)
            {
                tile.Value.Init(renderer);
            }
        }

        public static void RegisterDefaultTiles()
        {
            //RegisterTile("test", new Tiles.Test());
            WaveFunction.Tiles.Maze.Register();
        }
    }
}