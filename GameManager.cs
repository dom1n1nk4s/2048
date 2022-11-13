using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2048
{
    public class GameManager
    {
        private static Table _table;
        private static GameManager instance;

        private GameManager() { }

        public static GameManager Start()
        {
            Console.WriteLine("INPUT WIDTH AND HEIGHT COMMA SEPARATED (ex. 4,4)");
            var input = Console.ReadLine()?.Split(',');
            if (!int.TryParse(input?.ElementAtOrDefault(0), out var width) || !int.TryParse(input?.ElementAtOrDefault(1), out var height))
            {
                throw new Exception("FAILED TO PARSE WIDTH/HEIGHT");
            }

            _table = new(width, height);
            instance = new();

            return instance;
        }

        public void Run()
        {
            var roundIndex = 1;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"ROUND {roundIndex}");
                Console.WriteLine(_table);
                Console.WriteLine("INPUT DIRECTION TO MOVE: (UP - U, RIGHT - R, LEFT - L, DOWN - D)");

                var directionChar = Console.ReadLine()?.ToUpper()?.FirstOrDefault();

                var directionEnum = DirectionHelper.GetDirectionEnumByChar(directionChar);

                if (directionEnum == DirectionEnum.UNKNOWN)
                {
                    Console.WriteLine("INVALID DIRECTION INPUT.");
                    continue;
                }

                var result = _table.Move(directionEnum);

                if (result == MoveResultEnum.NO_MOVE_DONE)
                {
                    Console.WriteLine("NO MOVE DONE.");
                    continue;
                }

                if (result == MoveResultEnum.GAMEOVER_WIN || result == MoveResultEnum.GAMEOVER_FAIL)
                {
                    Console.WriteLine($"GAME OVER. YOU {(result == MoveResultEnum.GAMEOVER_WIN ? "WIN" : "FAIL")}.");
                    break;
                }

                roundIndex++;
            }
        }
    }
}