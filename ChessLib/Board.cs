using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    public class Board
    {
        public static int GetSquareIndex(string squareName)
        {
            const int nRows = 8;
            return (squareName[1] - '1') * nRows +
            (squareName[0] - 'a');
        }


        public static bool IsSquare(string maybeSquare)
        {
            return (maybeSquare.Length >= 2 &&
                    maybeSquare[0] >= 'a' &&
                    maybeSquare[0] <= 'h' &&
                    maybeSquare[1] >= '1' &&
                    maybeSquare[1] <= '8');
        }
    }
}
