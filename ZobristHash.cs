using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZobristLib
{
    public class ZobristHash
    {
        public ZobristHash(int seed, int nPieces = 13, int nSquares = 64)
        {
        }

        public void PlacePiece(int pieceIndex, int squareIndex)
        {
        }



        public ulong Hash { get; private set; } = 0UL;

        ulong[] _squareState;
        ulong[,] _pieceSquareCode;
    }
}
