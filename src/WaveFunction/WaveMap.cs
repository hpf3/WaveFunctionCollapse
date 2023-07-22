using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveFunction
{
    public class WaveMap
    {
        public int Width { get; }
        public int Height { get; }
        public int BaseSeed;
        public SharpNeatLib.Maths.FastRandom Random { get; }
        public WaveCell[,] Cells { get; }
        public EntropyList EntropyCells { get; }
        public WaveMap(int width, int height, int Seed=0)
        {
            BaseSeed = Seed;
            Random = new SharpNeatLib.Maths.FastRandom(Seed);
            Width = width;
            Height = height;
            Cells = new WaveCell[width, height];
            EntropyCells = new();
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height;y++)
                {
                    Cells[x, y] = new WaveCell(this, x, y, null);
                    EntropyCells.Add(Cells[x, y]);
                }
            }
            Console.WriteLine($"Created WaveMap with {Cells.GetLength(0)}x{Cells.GetLength(1)} cells ({Cells.Length} total) and {EntropyCells.Count} entropy cells");
        }
    }
    public class WaveCell
    {
                public WaveMap Map { get; }
        public int X { get; }
        public int Y { get; }
        private Interfaces.ITile? _tile;
        public Interfaces.ITile Tile { get{return _tile ?? TileRegistry.Missing;} private set{_tile = value;}}
        public WaveCell(WaveMap map, int x, int y, Interfaces.ITile? tile)
        {
            Map = map;
            X = x;
            Y = y;
            Tile = tile;
            if (tile is null)
            {
                InitPotentialTiles();
            }
        }
        //cache of tiles that could be placed here
        public List<Interfaces.ITile> PotentialTiles { get; } = new();
        public void InitPotentialTiles()
        {
            var tiles = TileRegistry.Tiles.Values.ToArray();
            PotentialTiles.AddRange(tiles);
            Entropy = tiles.Length;
        }
        //remove a tile from the list of potential tiles
        public void RemovePotentialTile(Interfaces.ITile tile)
        {
            //if entropy is 0, then there is no point in removing a tile
            if (Entropy == 0)
            {
                return;
            }
            if (PotentialTiles.Contains(tile))
            {
                PotentialTiles.Remove(tile);
                Entropy--;
                //check if there is only one tile left
                if (Entropy <= 1)
                {
                    //set the tile to the only tile left
                    SetTile(PotentialTiles[0]);
                }
            }
        }
        //add a tile to the list of potential tiles
        public void AddPotentialTile(Interfaces.ITile tile)
        {
            if (!PotentialTiles.Contains(tile))
            {
                PotentialTiles.Add(tile);
                Entropy++;
            }
        }
        //set the tile to a specific tile
        public void SetTile(Interfaces.ITile tile)
        {
            Tile = tile;
            PotentialTiles.Clear();
            Entropy = 0;
            Map.EntropyCells.Remove(this);
        }
        //set the tile to a random tile
        public void SetTileRandom()
        {
            if (Entropy == 0)
            {
                throw new Exception("Entropy is 0");
            }
            var index = Map.Random.Next(0, PotentialTiles.Count);
            SetTile(PotentialTiles[index]);
        }
        public int Entropy { get; private set; }
    }
}