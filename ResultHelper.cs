using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2048
{
    public static class ResultHelper
    {
        public static MoveResultEnum ReturnGreaterResult(MoveResultEnum originalResult, MoveResultEnum newResult)
        => originalResult > newResult ? originalResult : newResult;

    }
}