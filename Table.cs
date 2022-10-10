using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2048
{
    public class Table
    {
        public Block[,] Map { get; set; }
        public Table(int height, int width)
        {
            Map = new Block[height, width];
        }

        public MoveResultEnum Move(DirectionEnum direction)
        {

        }
    }
}