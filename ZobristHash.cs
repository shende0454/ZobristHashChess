using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZobristLib
{ 
    public class ZobristHash
    {
        private int seed;
        private int nPieces;
        private int nSquares;
        public ZobristHash(int seed, int nPieces = 13, int nSquares = 64)
        {
            this.seed = seed;
            this.nPieces = nPieces;
            this.nSquares = nSquares;
            _pieceSquareCode = new ulong[nPieces, nSquares];
            _squareState = new ulong[nSquares];
            RandomUlong rand = new RandomUlong(seed);
            for (int i = 0; i < nPieces; i++)
            {
                for (int j = 0; j < nSquares; j++)
                {
                    _pieceSquareCode[i,j] = rand.Next();
                }
                _squareState[i] = 0;
            }
            
        }
        public void ClearBoard()
        {
            Hash = 0;
            for (int i = 0; i < nSquares; i++)
            {
                _squareState[i] = _pieceSquareCode[0,i];
                Hash = Hash ^ _pieceSquareCode[0,i];
            }
        }
        
        public void PlacePiece(int pieceIndex, int squareIndex)
        {
            
            //Remove the current state from the Hash with xor
            Hash = Hash ^ _squareState[squareIndex]; 

            //Add the new state to the Hash
            Hash = Hash ^ _pieceSquareCode[pieceIndex, squareIndex];
            //Remember what was there 
            _squareState[squareIndex] = _pieceSquareCode[pieceIndex, squareIndex];


        }
        public void MakeMove(char piece, string sourceSquare, string destinationSquare)
        {

            //Add the new state to the Hash
            Hash = Hash ^ _squareState[GetSquareIndex(sourceSquare)];
            //Remember what was there 
            Hash = Hash ^ _pieceSquareCode[0, GetSquareIndex(sourceSquare)];

            _squareState[GetSquareIndex(sourceSquare)] = _pieceSquareCode[0, GetSquareIndex(sourceSquare)];
            Hash = Hash ^ _squareState[GetSquareIndex(destinationSquare)];
            Hash = Hash ^ _pieceSquareCode[GetPieceIndex(piece), GetSquareIndex(destinationSquare)];
            _squareState[GetSquareIndex(destinationSquare)] = _pieceSquareCode[GetPieceIndex(piece), GetSquareIndex(destinationSquare)];


        }

        public static int GetSquareIndex(string squareName)
        {
            const int nRows = 8;
            return (squareName[1] - '1') * nRows +
            (squareName[0] - 'a');
        }
        public static int GetPieceIndex(char pieceSymbol, bool IsBlack = false)
        {
            if (IsBlack) pieceSymbol = char.ToLower(pieceSymbol);
            string pieceString = " PRNBQKprnbqk";
            if (!pieceString.Contains(pieceSymbol))
            {
                throw new Exception($"{pieceSymbol} is not in {pieceString}.");
            }
            return pieceString.IndexOf(pieceSymbol);
        }
        public ulong Hash { get; private set; } = 0UL;

        
        ulong[] _squareState;
        ulong[,] _pieceSquareCode;
       
       
    }
}
