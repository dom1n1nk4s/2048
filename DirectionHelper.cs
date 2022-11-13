using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2048
{
    public static class DirectionHelper
    {
        public static DirectionEnum GetDirectionEnumByChar(char? input) => input switch
        {
            'U' => DirectionEnum.UP,
            'R' => DirectionEnum.RIGHT,
            'L' => DirectionEnum.LEFT,
            'D' => DirectionEnum.DOWN,
            _ => DirectionEnum.UNKNOWN
        };
    }
}