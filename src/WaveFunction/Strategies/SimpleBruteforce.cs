using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaveFunction.structs;
namespace WaveFunction.Strategies
{
    public class SimpleBruteforce : Interfaces.ICollapseMethod
    {
        private readonly Queue<WaveCell> cells = new();
            //debug counter
            private int itterations = 0;
        public void Collapse(WaveMap map)
        {
            itterations++;
            Console.WriteLine($"Itteration: {itterations}");
            if(map.EntropyCells.Count == 0)
            {
                Console.WriteLine("No more entropy cells");
                return;
            }
            //get the sorted list of cells
            var list = map.EntropyCells;
            //get the cell with the lowest entropy
            cells.Enqueue(list.GetLowestEntropy());
            while (cells.Count > 0)
            {
                //get the first cell in the queue
                var cell = cells.Dequeue();
                //get the x and y of the cell
                var x = cell.X;
                var y = cell.Y;
                //check if the cell has already been collapsed
                if (cell.Entropy != 0)
                {
                    //collapse the cell to a random possible tile
                    cell.SetTileRandom();
                }
                //get the neighbors of the cell
                var neighbors = GetNeighbors(map, x, y);
                //for each neighbor, determine if their potential tiles are still valid
                foreach (var neighbor in neighbors)
                {
                    //if the neighbor has already been collapsed, then skip it
                    if (neighbor.Entropy == 0)
                    {
                        continue;
                    }
                    //get the side of the neighbor that is adjacent to the current cell
                    var side = GetDirection(x, y, neighbor.X, neighbor.Y);
                    //track if the neighbor needs to be updated
                    var update = false;
                    //get the list of potential tiles for the neighbor
                    var neighborTiles = neighbor.PotentialTiles;
                    //for each potential tile of the neighbor cell check if it is valid
                    //using the tiles connection rules
                    for (var i = neighborTiles.Count - 1; i >= 0; i--)
                    {
                        //if the tile is not valid, then remove it from the list of potential tiles
                        if (!SideRules.IsCompatible(neighborTiles[i].SideRules[InvertDirection(side)], cell.Tile!.SideRules[side]))
                        {
                            neighbor.RemovePotentialTile(neighborTiles[i]);
                            update = true;
                        }
                    }
                    //if the neighbor needs to be updated, then update it on the map
                    if (update)
                    {
                        //check if the neighbor collapsed
                        if (neighbor.PotentialTiles.Count == 1)
                        {
                            //if the neighbor collapsed, then add it to the queue
                            cells.Enqueue(neighbor);
                        }
                        //update the neighbor on the map's sorted list
                        list.UpdateEntropy(neighbor);
                    }
                }
            }
        }
        private static WaveCell[] GetNeighbors(WaveMap map, int x, int y)
        {
            var neighbors = new List<WaveCell>();
            if (x > 0)
            {
                neighbors.Add(map.Cells[x - 1, y]);
            }
            if (x < (map.Width - 1))
            {
                neighbors.Add(map.Cells[x + 1, y]);
            }
            if (y > 0)
            {
                neighbors.Add(map.Cells[x, y - 1]);
            }
            if (y < (map.Height - 1))
            {
                neighbors.Add(map.Cells[x, y + 1]);
            }
            return neighbors.ToArray();
        }
        private static int GetDirection(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2)
            {
                if (y1 == y2)
                {
                    throw new Exception("Cells are the same");
                }
                return y1 > y2 ? 0 : 2;
            }
            if (y1 == y2)
            {
                return x1 > x2 ? 3 : 1;
            }
            throw new Exception("Cells are not adjacent");
        }
        private static int InvertDirection(int direction)
        {
            return (direction + 2) % 4;
        }
    }
}