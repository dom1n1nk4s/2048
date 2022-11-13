using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public class Table
    {
        public Block[,] Map { get; set; }
        private static Block _emptyBlock = new Block(Constants.EmptyBlockValue);
        private static Random _random = new Random();
        private int _width, _height;
        public Table(int width, int height)
        {
            Map = new Block[height, width];
            _width = width;
            _height = height;

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Map[x, y] = _emptyBlock;
                }
            }

            GenerateNewBlock();
            GenerateNewBlock();

        }

        private bool IsMapFull()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (Map[x, y].Value == Constants.EmptyBlockValue)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool GenerateNewBlock()
        {
            if (IsMapFull())
            {
                return false;
            }

            int x, y;
            do
            {
                x = _random.Next(_width);
                y = _random.Next(_height);
            } while (Map[x, y].Value != Constants.EmptyBlockValue);

            Map[x, y] = new(2);

            return true;
        }
        private MoveResultEnum MoveTableBasedOnDirection(DirectionEnum directionEnum) => directionEnum switch
        {
            DirectionEnum.DOWN => MoveTableDown(),
            DirectionEnum.LEFT => MoveTableLeft(),
            DirectionEnum.RIGHT => MoveTableRight(),
            DirectionEnum.UP => MoveTableUp(),
        };

        private bool IsOutOfBounds(int x, int y)
        =>
            x >= _width ||
            y >= _height ||
            x < 0 ||
            y < 0;

        private MoveResultEnum MoveTableUniversally(int x, int y, (int x, int y) directionTuple)
        {
            var result = MoveResultEnum.NO_MOVE_DONE;

            var currentElement = Map[x, y];

            if (currentElement.Value == Constants.EmptyBlockValue)
            {
                return MoveResultEnum.NO_MOVE_DONE;
            }

            int index = 1;
            while (!IsOutOfBounds(x + directionTuple.x * index, y + directionTuple.y * index))
            {
                var nextElement = Map[x + directionTuple.x * index, y + directionTuple.y * index];
                if (nextElement.Value == Constants.EmptyBlockValue)
                {
                    result = MoveResultEnum.MOVE_DONE;

                    Map[x + directionTuple.x * index, y + directionTuple.y * index] =
                        Map[x + directionTuple.x * (index - 1), y + directionTuple.y * (index - 1)];

                    Map[x + directionTuple.x * (index - 1), y + directionTuple.y * (index - 1)] = _emptyBlock;
                }
                else
                {
                    if (currentElement.Value == nextElement.Value)
                    {
                        result = MoveResultEnum.MOVE_DONE;

                        nextElement.Value *= 2;
                        Map[x + directionTuple.x * (index - 1), y + directionTuple.y * (index - 1)] = _emptyBlock;

                        if (nextElement.Value == Constants.GameWinConditionValue)
                        {
                            result = MoveResultEnum.GAMEOVER_WIN;
                        }
                    }
                    break;
                }

                index++;
            }

            return result;
        }

        private MoveResultEnum MoveTableDown()
        {
            var result = MoveResultEnum.NO_MOVE_DONE;
            (int x, int y) directionTuple = (0, 1);
            for (int x = 0; x < _width; x++)
            {
                for (int y = _height - 1; y >= 0; y--)
                {
                    var moveResult = MoveTableUniversally(x, y, directionTuple);
                    result = ResultHelper.ReturnGreaterResult(result, moveResult);
                }
            }

            return result;
        }

        private MoveResultEnum MoveTableUp()
        {
            var result = MoveResultEnum.NO_MOVE_DONE;
            (int x, int y) directionTuple = (0, -1);
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var moveResult = MoveTableUniversally(x, y, directionTuple);
                    result = ResultHelper.ReturnGreaterResult(result, moveResult);
                }
            }

            return result;
        }

        private MoveResultEnum MoveTableLeft()
        {
            var result = MoveResultEnum.NO_MOVE_DONE;
            (int x, int y) directionTuple = (-1, 0);
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var moveResult = MoveTableUniversally(x, y, directionTuple);
                    result = ResultHelper.ReturnGreaterResult(result, moveResult);
                }
            }

            return result;
        }

        private MoveResultEnum MoveTableRight()
        {
            var result = MoveResultEnum.NO_MOVE_DONE;
            (int x, int y) directionTuple = (1, 0);
            for (int y = 0; y < _height; y++)
            {
                for (int x = _width - 1; x >= 0; x--)
                {
                    var moveResult = MoveTableUniversally(x, y, directionTuple);
                    result = ResultHelper.ReturnGreaterResult(result, moveResult);
                }
            }

            return result;
        }

        public MoveResultEnum Move(DirectionEnum direction)
        {
            var result = MoveTableBasedOnDirection(direction);

            if (IsMapFull() && result == MoveResultEnum.NO_MOVE_DONE)
            {
                return MoveResultEnum.GAMEOVER_FAIL;
            }

            if (result == MoveResultEnum.MOVE_DONE)
            {
                GenerateNewBlock();
            }

            return result;
        }


        public override string? ToString()
        {
            var result = new StringBuilder();
            for (int i = 0; i < _width * 5 + 1; i++)
            {
                result.Append('-');
            }

            result.Append(System.Environment.NewLine);

            for (int y = 0; y < _height; y++)
            {
                result.Append('|');
                for (int x = 0; x < _width; x++)
                {
                    result.Append($"{Map[x, y].Value,4}|");
                }

                result.Append(System.Environment.NewLine);
            }


            for (int i = 0; i < _width * 5 + 1; i++)
            {
                result.Append('-');
            }

            return result.ToString();
        }
    }
}