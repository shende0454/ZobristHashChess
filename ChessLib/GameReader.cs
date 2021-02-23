using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChessLib
{
    public class GameReader
    {
        public GameReader(string filename)
        {
            _reader = new StreamReader(filename);
        }

        bool GetNextLine()
        {
            bool result = true;
            string thingRead = "";
            char[] delimiters = " \t".ToCharArray();
            if (!_reader.EndOfStream)
            {
                thingRead = _reader.ReadLine();
                _moves = thingRead.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                _nextMoveIndex = 0;
            }
            else
            {
                result = false;
            }
            return result;
        }
        
        public bool IsResult(string maybeResult)
        {
            return maybeResult == "1-0" ||
                   maybeResult == "1/2-1/2" ||
                   maybeResult == "0-1";
        }

        bool IsPromotion(string maybePromotion, 
            out string sourceSquareName, out string destinationSquareName,
            out char promotionChar)
        {
            sourceSquareName = "";
            destinationSquareName = "";
            promotionChar = '\0';
            bool isPromotion = false;
            if (maybePromotion != null && 
                maybePromotion.Length >= 5 &&
                IsSquare(maybePromotion.Substring(0,2)) &&
                IsSquare(maybePromotion.Substring(2,2)) &&
                Piece.IsPiece(maybePromotion[4]))
            {
                sourceSquareName = maybePromotion.Substring(0, 2);
                destinationSquareName = maybePromotion.Substring(2, 2);
                promotionChar = maybePromotion[4];
                if (destinationSquareName[1] == '1')
                    promotionChar = Char.ToLower(promotionChar);
                isPromotion = true;
            }
            return isPromotion;
        }

        public bool ParseMove(out string sourceSquareName, out string destinationSquareName,
            out bool gameOver, out char promotionChar, out string parsedString)
        {
            destinationSquareName = "";
            sourceSquareName = "";
            gameOver = false;
            promotionChar = '\0';
            parsedString = "";
            bool canParse = true;
            if (_moves != null && _nextMoveIndex < _moves.Count())
            {
                char[] delimiter = "-".ToCharArray();
                parsedString = _moves[_nextMoveIndex];
                string[] squares = _moves[_nextMoveIndex].Split(delimiter,
                    StringSplitOptions.RemoveEmptyEntries);
                canParse = squares.Count() >= 2 &&
                           IsSquare(squares[0]) && IsSquare(squares[1]);

                if (canParse)
                {
                    sourceSquareName = squares[0];
                    destinationSquareName = squares[1];
                }
                else
                {
                    canParse = IsPromotion(_moves[_nextMoveIndex],
                        out sourceSquareName, out destinationSquareName,
                        out promotionChar);
                }

                if (!canParse)
                {
                    canParse = IsResult(_moves[_nextMoveIndex]);
                    if (canParse) gameOver = true;
                }
                else
                {
                    sourceSquareName = sourceSquareName.Substring(0,2);
                    destinationSquareName = destinationSquareName.Substring(0,2);
                }
            }
            return canParse;
        }

        public bool IsSquare(string maybeSquare)
        {
            return (maybeSquare.Length >= 2 &&
                    maybeSquare[0] >= 'a' &&
                    maybeSquare[0] <= 'h' &&
                    maybeSquare[1] >= '1' &&
                    maybeSquare[1] <= '8');
        }

        // Return false when end of file
        public bool GetNextMove(out string sourceSquareName, 
            out string destinationSquareName, out bool gameOver, 
            out char promotionChar, out string parsedString)
        {
            bool eof = false;
            bool gotMove = false;
            parsedString = "";
            sourceSquareName = "";
            destinationSquareName = "";
            promotionChar = '\0';
            gameOver = false;
            while (!eof && !gotMove)
            {
                while (!eof && _nextMoveIndex >= _moves.Length)
                {
                    eof = !GetNextLine();
                }

                gotMove = ParseMove(out sourceSquareName, out destinationSquareName,
                    out gameOver, out promotionChar, out parsedString);

                if (!gotMove)
                {
                    gameOver = true;
                    gotMove = true;
                }

                ++_nextMoveIndex;
            }

            return eof;
        }

        StreamReader _reader;
        int _nextMoveIndex;
        string[] _moves = new string[0];

    }
}
