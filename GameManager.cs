using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2048
{
    public static class GameManager
    {
        static Table _table;

        public static void Start()
        {
            Console.WriteLine("INPUT WIDTH AND HEIGHT COMMA SEPARATED (ex. 4,4)");
            var input = Console.ReadLine()?.Split(',');
            if (!int.TryParse(input?.ElementAtOrDefault(0), out var width) && !int.TryParse(input?.ElementAtOrDefault(1), out var height))
            {
                throw new Exception("Failed to parse width/height");
            }
        }
    }
}