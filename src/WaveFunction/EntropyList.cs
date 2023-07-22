using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveFunction
{
    /// <summary>
    ///list of WaveCells sorted by entropy, acting as a priority queue
    /// </summary>
    public class EntropyList
    {
        //backing list
        private readonly List<WaveCell> _list = new();
        public int Count => _list.Count;

        //add a cell to the list
        public void Add(WaveCell cell)
        {
            _list.Add(cell);
            InsertCellInOrder(_list, _list.Count - 1);
        }
        //remove a cell from the list
        public void Remove(WaveCell cell)
        {
            _list.Remove(cell);
            Console.WriteLine($"Removed cell at {cell.X}, {cell.Y} List size: {_list.Count}");
        }
        //get the cell with the lowest entropy
        public WaveCell GetLowestEntropy()
        {
            return _list[0];
        }
        //get the cell with the highest entropy
        public WaveCell GetHighestEntropy()
        {
            return _list[^1];
        }
        //update the entropy of a cell
        public void UpdateEntropy(WaveCell cell)
        {
            var index = _list.IndexOf(cell);
            InsertCellInOrder(_list, index);
        }

        //sorting function, using a modified insertion sort
        private static void InsertCellInOrder(List<WaveCell> cells, int index)
        {
            while (index > 0 && cells[index].Entropy < cells[index - 1].Entropy)
            {
                // Swap cells[index] and cells[index - 1]
                (cells[index - 1], cells[index]) = (cells[index], cells[index - 1]);
                index--;
            }

            while (index < cells.Count - 1 && cells[index].Entropy > cells[index + 1].Entropy)
            {
                // Swap cells[index] and cells[index + 1]
                (cells[index + 1], cells[index]) = (cells[index], cells[index + 1]);
                index++;
            }
        }
    }
}