using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2048
{
    public class Block
    {
        public Block(ulong value)
        {
            Value = value;
        }

        public ulong Value { get; set; } = 2;
    }
}