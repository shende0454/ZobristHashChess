using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    public class GameState
    {
        public GameState(int nRows=8, int nColumns=8)
        {
            _squares = new char[nRows * nColumns];
            SetupBoard();
        }

        public void SetupBoard()
        {
            for(int i=0;i<_squares.Length;++i)
            {
                SetSquareOccupant(' ', i);
            }

            // Place the rooks
            SetSquareOccupant('R', "h1");
            SetSquareOccupant('r', "h8");
            SetSquareOccupant('R', "a1");
            SetSquareOccupant('r', "a8");

            // Place the knights
            SetSquareOccupant('N', "g1");
            SetSquareOccupant('n', "g8");
            SetSquareOccupant('N', "b1");
            SetSquareOccupant('n', "b8");

            // Place the bishops
            SetSquareOccupant('B', "f1");
            SetSquareOccupant('b', "f8");
            SetSquareOccupant('B', "c1");
            SetSquareOccupant('b', "c8");

            // Place the king and queen
            SetSquareOccupant('K', "e1");
            SetSquareOccupant('k', "e8");
            SetSquareOccupant('Q', "d1");
            SetSquareOccupant('q', "d8");

            // Place the pawns
            SetSquareOccupant('P', "a2");
            SetSquareOccupant('P', "b2");
            SetSquareOccupant('P', "c2");
            SetSquareOccupant('P', "d2");
            SetSquareOccupant('P', "e2");
            SetSquareOccupant('P', "f2");
            SetSquareOccupant('P', "g2");
            SetSquareOccupant('P', "h2");

            SetSquareOccupant('p', "a7");
            SetSquareOccupant('p', "b7");
            SetSquareOccupant('p', "c7");
            SetSquareOccupant('p', "d7");
            SetSquareOccupant('p', "e7");
            SetSquareOccupant('p', "f7");
            SetSquareOccupant('p', "g7");
            SetSquareOccupant('p', "h7");
        }

        public void SetSquareOccupant(char pieceSymbol, string squareName)
        {
            SetSquareOccupant(pieceSymbol, Board.GetSquareIndex(squareName));
        }

        public void SetSquareOccupant(char pieceSymbol, int squareIndex)
        {
            _squares[squareIndex] = pieceSymbol;
        }

        bool IsCastling(string sourceSquare, string destinationSquare, char piece)
        {
            bool castling = false;
            if (piece == 'K' && sourceSquare == "e1" && destinationSquare == "g1")
            {
                SetSquareOccupant('K', "g1");
                SetSquareOccupant(' ', "e1");
                SetSquareOccupant('R', "f1");
                SetSquareOccupant(' ', "h1");
                castling = true;
            }

            if (piece == 'K' && sourceSquare == "e1" && destinationSquare == "c1")
            {
                SetSquareOccupant('K', "c1");
                SetSquareOccupant(' ', "e1");
                SetSquareOccupant('R', "d1");
                SetSquareOccupant(' ', "a1");
                castling = true;
            }

            if (piece == 'k' && sourceSquare == "e8" && destinationSquare == "g8")
            {
                SetSquareOccupant('k', "g8");
                SetSquareOccupant(' ', "e8");
                SetSquareOccupant('r', "f8");
                SetSquareOccupant(' ', "h8");
                castling = true;
            }

            if (piece == 'k' && sourceSquare == "e8" && destinationSquare == "c8")
            {
                SetSquareOccupant('k', "c8");
                SetSquareOccupant(' ', "e8");
                SetSquareOccupant('r', "d8");
                SetSquareOccupant(' ', "a8");
                castling = true;
            }
            return castling;
        }
        public void MakeMove(string sourceSquare, string destinationSquare)
        {
            if (Board.IsSquare(sourceSquare) && Board.IsSquare(destinationSquare))
            {
                char piece = GetSquareOccupant(sourceSquare);
                if (!IsCastling(sourceSquare, destinationSquare, piece))
                {
                    SetSquareOccupant(piece, destinationSquare);
                    SetSquareOccupant(' ', sourceSquare);
                }
            }
            else throw new Exception("Bad move squares.");


        }

        public char GetSquareOccupant(string squareName)
        {
            return GetSquareOccupant(Board.GetSquareIndex(squareName));
        }

        public char GetSquareOccupant(int squareIndex)
        {
            return _squares[squareIndex];
        }

        char[] _squares;
    }
}
