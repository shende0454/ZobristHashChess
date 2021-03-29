using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZobristLib;

namespace ChessLib
{
    public class ChessHash
    {
        public ChessHash(int seed)
        {
            _hash = new ZobristHash(seed);
        }

        // Make all the squares empty.
        public void ClearBoard()
        {
            _hash.ClearBoard();}

        public void PlacePiece(char pieceSymbol, string squareName)
        {
            _hash.PlacePiece(Piece.GetIndex(pieceSymbol), Board.GetSquareIndex(squareName));
        }

        public void MakeMove(char piece, string sourceSquare, string destinationSquare)
        {
            // 4 steps
            _hash.MakeMove(piece, sourceSquare, destinationSquare);
            
        }

        public void SetupBoard()
        {
            ClearBoard();

            // Place the rooks
            PlacePiece('R', "h1");
            PlacePiece('r', "h8");
            PlacePiece('R', "a1");
            PlacePiece('r', "a8");

            // Place the knights
            PlacePiece('N', "g1");
            PlacePiece('n', "g8");
            PlacePiece('N', "b1");
            PlacePiece('n', "b8");

            // Place the bishops
            PlacePiece('B', "f1");
            PlacePiece('b', "f8");
            PlacePiece('B', "c1");
            PlacePiece('b', "c8");

            // Place the king and queen
            PlacePiece('K', "e1");
            PlacePiece('k', "e8");
            PlacePiece('Q', "d1");
            PlacePiece('q', "d8");

            // Place the pawns
            PlacePiece('P', "a2");
            PlacePiece('P', "b2");
            PlacePiece('P', "c2");
            PlacePiece('P', "d2");
            PlacePiece('P', "e2");
            PlacePiece('P', "f2");
            PlacePiece('P', "g2");
            PlacePiece('P', "h2");

            PlacePiece('p', "a7");
            PlacePiece('p', "b7");
            PlacePiece('p', "c7");
            PlacePiece('p', "d7");
            PlacePiece('p', "e7");
            PlacePiece('p', "f7");
            PlacePiece('p', "g7");
            PlacePiece('p', "h7");

        }

        public ulong Hash
        {
            get { return _hash.Hash; }
        }

        ZobristHash _hash;

    }
}
