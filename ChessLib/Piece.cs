using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib
{
    public class Piece
    {
        public enum Index 
        {
            EmptySquare,
            WhitePawn, WhiteRook, WhiteKnight, WhiteBishop, WhiteQueen, WhiteKing,
            BlackPawn, BlackRook, BlackKnight, BlackBishop, BlackQueen, BlackKing
        }

        public static bool IsPiece(char piece)
        {
            return "PRNBQKprnbqk".Contains(piece);
        }

        public static int GetIndex(char pieceSymbol, bool IsBlack = false)
        {
            if (IsBlack) pieceSymbol = char.ToLower(pieceSymbol);
            string pieceString = " PRNBQKprnbqk";
            if (!pieceString.Contains(pieceSymbol))
            {
                throw new Exception($"{pieceSymbol} is not in {pieceString}.");
            }

            return pieceString.IndexOf(pieceSymbol);
        }

    }
}
