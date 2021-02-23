using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ChessLib;
using ZobristLib;

// You will need to use your HashLib from the previous lab.
// using HashLib;

namespace ZobristTests
{
    [TestClass]
    public class UnitTest1
    {
        // This should already work
        // It demonstrates how to get the index associated with each piece.
        [TestMethod]
        public void T001_GetIndex()
        {
            Assert.AreEqual((int)Piece.Index.WhitePawn,
                (int)Piece.GetIndex('P'));
            Assert.AreEqual((int)Piece.Index.WhiteKnight,
                (int)Piece.GetIndex('N'));
            Assert.AreEqual((int)Piece.Index.WhiteBishop,
                (int)Piece.GetIndex('B'));
            Assert.AreEqual((int)Piece.Index.WhiteRook,
                (int)Piece.GetIndex('R'));
            Assert.AreEqual((int)Piece.Index.WhiteQueen,
                (int)Piece.GetIndex('Q'));
            Assert.AreEqual((int)Piece.Index.WhiteKing,
                (int)Piece.GetIndex('K'));

            Assert.AreEqual((int)Piece.Index.BlackPawn,
                (int)Piece.GetIndex('p'));
            Assert.AreEqual((int)Piece.Index.BlackKnight,
                (int)Piece.GetIndex('n'));
            Assert.AreEqual((int)Piece.Index.BlackBishop,
                (int)Piece.GetIndex('b'));
            Assert.AreEqual((int)Piece.Index.BlackRook,
                (int)Piece.GetIndex('r'));
            Assert.AreEqual((int)Piece.Index.BlackQueen,
                (int)Piece.GetIndex('q'));
            Assert.AreEqual((int)Piece.Index.BlackKing,
                (int)Piece.GetIndex('k'));
        }
        // end T001_GetIndex()


        // This should also work
        // It demonstrates how to get the index of a particular square.
        [TestMethod]
        public void T002_GetSquareIndex()
        {
            Assert.AreEqual(0, Board.GetSquareIndex("a1"));
            Assert.AreEqual(1, Board.GetSquareIndex("b1"));
            Assert.AreEqual(2, Board.GetSquareIndex("c1"));
            Assert.AreEqual(3, Board.GetSquareIndex("d1"));
            Assert.AreEqual(4, Board.GetSquareIndex("e1"));
            Assert.AreEqual(5, Board.GetSquareIndex("f1"));
            Assert.AreEqual(6, Board.GetSquareIndex("g1"));
            Assert.AreEqual(7, Board.GetSquareIndex("h1"));

            Assert.AreEqual(56, Board.GetSquareIndex("a8"));
            Assert.AreEqual(57, Board.GetSquareIndex("b8"));
            Assert.AreEqual(58, Board.GetSquareIndex("c8"));
            Assert.AreEqual(59, Board.GetSquareIndex("d8"));
            Assert.AreEqual(60, Board.GetSquareIndex("e8"));
            Assert.AreEqual(61, Board.GetSquareIndex("f8"));
            Assert.AreEqual(62, Board.GetSquareIndex("g8"));
            Assert.AreEqual(63, Board.GetSquareIndex("h8"));
        }
        // end T002_GetSquareIndex()


        // The zobrist hash is initially zero, but this
        // is with nothing on the board (not even empty squares!)
        [TestMethod]
        public void T003_emptyBoardHashesToZero()
        {
            int seed = 3432211;
            ZobristHash zobrist = new ZobristHash(seed);
            Assert.AreEqual(0UL, zobrist.Hash);
        }
        // end T003_emptyBoardHashesToZero()


        // Put empty squares on the board.
        [TestMethod]
        public void T004_clearBoard()
        {
            int seed = 3432211;
            ZobristHash zobrist = new ZobristHash(seed);
            ulong oldHash = zobrist.Hash;
            int pieceIndex = Piece.GetIndex(' ');

            const int nSquares = 64;
            for (int i = 0; i < nSquares; ++i)
            {
                zobrist.PlacePiece(pieceIndex, i);
            }

            // The chessHash has an instance of the ZobristHash 
            // class as a data member.
            ChessHash chessHash = new ChessHash(seed);
            chessHash.ClearBoard();
            Assert.IsTrue(zobrist.Hash == chessHash.Hash);
            Assert.IsFalse(0UL == chessHash.Hash);
        }

        [TestMethod]
        // Put a piece on the board.
        // Remember, when we put something new on the board
        // we need to remove whatever was there before, even
        // if it is just an empty square.
        public void T005_PlacePiece()
        {
            int seed = 3432211;
            ZobristHash zobrist = new ZobristHash(seed);
            ulong oldHash = zobrist.Hash;
            int pieceIndex = Piece.GetIndex('N');
            int squareIndex = Board.GetSquareIndex("f3");
            zobrist.PlacePiece(pieceIndex, squareIndex);

            ChessHash chessHash = new ChessHash(seed);
            chessHash.PlacePiece('N', "f3");
            Assert.IsTrue(zobrist.Hash == chessHash.Hash);
            Assert.IsTrue(0UL != chessHash.Hash);
        }
        // end T005_PlacePiece()


        [TestMethod]
        public void T006_SetupBoard()
        {
            int seed = 3432211;
            ChessHash chessHash = new ChessHash(seed);

            chessHash.ClearBoard();

            // Place the rooks
            chessHash.PlacePiece('R', "h1");
            chessHash.PlacePiece('r', "h8");
            chessHash.PlacePiece('R', "a1");
            chessHash.PlacePiece('r', "a8");

            // Place the knights
            chessHash.PlacePiece('N', "g1");
            chessHash.PlacePiece('n', "g8");
            chessHash.PlacePiece('N', "b1");
            chessHash.PlacePiece('n', "b8");

            // Place the bishops
            chessHash.PlacePiece('B', "f1");
            chessHash.PlacePiece('b', "f8");
            chessHash.PlacePiece('B', "c1");
            chessHash.PlacePiece('b', "c8");

            // Place the king and queen
            chessHash.PlacePiece('K', "e1");
            chessHash.PlacePiece('k', "e8");
            chessHash.PlacePiece('Q', "d1");
            chessHash.PlacePiece('q', "d8");

            // Place the pawns
            chessHash.PlacePiece('P', "a2");
            chessHash.PlacePiece('P', "b2");
            chessHash.PlacePiece('P', "c2");
            chessHash.PlacePiece('P', "d2");
            chessHash.PlacePiece('P', "e2");
            chessHash.PlacePiece('P', "f2");
            chessHash.PlacePiece('P', "g2");
            chessHash.PlacePiece('P', "h2");

            chessHash.PlacePiece('p', "a7");
            chessHash.PlacePiece('p', "b7");
            chessHash.PlacePiece('p', "c7");
            chessHash.PlacePiece('p', "d7");
            chessHash.PlacePiece('p', "e7");
            chessHash.PlacePiece('p', "f7");
            chessHash.PlacePiece('p', "g7");
            chessHash.PlacePiece('p', "h7");

            ulong setupHash = chessHash.Hash;

            //  Move a knight out
            chessHash.PlacePiece('N', "f3");

            // Put an empty square where the knight was.
            chessHash.PlacePiece(' ', "g1");

            // Move the knight back
            chessHash.PlacePiece('N', "g1");

            // Put an empty square where the knight was.
            chessHash.PlacePiece(' ', "f3");

            Assert.IsTrue(0UL != setupHash);
            Assert.IsTrue(setupHash == chessHash.Hash);

            // Make this easier in the ChessHash class
            ChessHash betterChessHash = new ChessHash(seed);
            betterChessHash.SetupBoard();
            Assert.IsTrue(betterChessHash.Hash == setupHash);

            // Make sure that it works if we Setup the board again.
            betterChessHash.SetupBoard();
            Assert.IsTrue(betterChessHash.Hash == setupHash);
        }
        // end T006_SetupBoard()


        [TestMethod]
        // Make sure that move order doen't matter to the hash.
        public void T007_MoveOrder()
        {
            int seed = 3432211;
            ChessHash zobrist = new ChessHash(seed);
            zobrist.SetupBoard();
            zobrist.MakeMove('P', "e2", "e4");
            zobrist.MakeMove('p', "e7", "e5");
            zobrist.MakeMove('N', "g1", "f3");
            zobrist.MakeMove('n', "b8", "c6");

            ChessHash moveOrder = new ChessHash(seed);
            moveOrder.SetupBoard();
            moveOrder.MakeMove('N', "g1", "f3");
            moveOrder.MakeMove('n', "b8", "c6");
            moveOrder.MakeMove('P', "e2", "e4");
            moveOrder.MakeMove('p', "e7", "e5");

            Assert.IsTrue(zobrist.Hash == moveOrder.Hash);
            Assert.IsTrue(0UL != moveOrder.Hash);
        }
        // end T007_MoveOrder()


        // This should already work.
        // It demonstrates that you can find the piece on each square.
        // and the constructor sets the game up.
        [TestMethod]
        public void T008_GameState()
        {
            GameState gameState = new GameState();
            Assert.AreEqual('R', gameState.GetSquareOccupant("a1"));
            Assert.AreEqual('r', gameState.GetSquareOccupant("h8"));
            Assert.AreEqual(' ', gameState.GetSquareOccupant("e4"));
        }
        // end T008_GameState()


        // Demonstrates how to read a pgn file.
        // Read a single move
        [TestMethod]
        public void T009_readSingleMove()
        {
            string parseString;
            string path = System.Environment.GetEnvironmentVariable("HOMEPATH");
            GameReader reader = new GameReader(path + "\\chessGames\\cs242-oneGame.pgn");
            string sourceSquare;
            string destinationSquare;
            bool gameOver;
            char promotionChar;

            // GetNextMove parameters are all out (returned) values
            //   promotionChar returns the new piece char if a piece was promoted (0 otherwise)
            //   parseString lets you see what the move was that was parsed.

            reader.GetNextMove(out sourceSquare,
                out destinationSquare, out gameOver, out promotionChar, out parseString);
            Assert.AreEqual("e2", sourceSquare);
            Assert.AreEqual("e4", destinationSquare);
            Assert.IsFalse(gameOver);
        }
        // end T009_testName()


        // This demonstrates reading a whole game.
        [TestMethod]
        public void T010_readWholeGame()
        {
            string path = System.Environment.GetEnvironmentVariable("HOMEPATH");
            GameReader reader = new GameReader(path + "\\chessGames\\cs242-oneGame.pgn");
            string sourceSquare = "";
            string destinationSquare = "";
            bool gameOver = false;

            int moveCount = 0;
            string lastSource = "";
            string lastDest = "";
            string parseString;
            char promotionChar;
            while (!gameOver)
            {
                lastSource = sourceSquare;
                lastDest = destinationSquare;
                reader.GetNextMove(out sourceSquare,
                    out destinationSquare, out gameOver, out promotionChar, out parseString);
                ++moveCount;
            }

            // Last "move" counted is the result
            --moveCount;

            Assert.AreEqual("g2", lastSource);
            Assert.AreEqual("g4", lastDest);
            Assert.AreEqual(41, moveCount);
        }
        // end T010_readWholeGame()


        // Demonstrates reading multiple games.
        [TestMethod]
        public void T011_readTenGames()
        {
            string parseString;
            string path = System.Environment.GetEnvironmentVariable("HOMEPATH");
            GameReader reader = new GameReader(path + "\\chessGames\\cs242-tenGames.pgn");
            string sourceSquare = "";
            string destinationSquare = "";
            bool gameOver = false;

            int gameCount = 0;
            string lastSource = "";
            string lastDest = "";
            bool eof = false;
            char promotionChar;
            while (!eof)
            {
                eof = reader.GetNextMove(out sourceSquare, out destinationSquare, out gameOver,
                    out promotionChar, out parseString);
                if (gameOver) ++gameCount;
            }
            Assert.AreEqual(10, gameCount);
        }
        // end T011_readTenGames()

        // Read all the games in the largest pgn file.
        // You will probably want to comment this out after the first run.
        [TestMethod]
        public void T012_readAllGames()
        {
            string parseString;
            string path = System.Environment.GetEnvironmentVariable("HOMEPATH");
            GameReader reader = new GameReader(path + "\\chessGames\\cs242-allGames.pgn");
            string sourceSquare = "";
            string destinationSquare = "";
            bool gameOver = false;

            int gameCount = 0;
            string lastSource = "";
            string lastDest = "";
            bool eof = false;
            char promotionChar;
            while (!eof)
            {
                eof = reader.GetNextMove(out sourceSquare, out destinationSquare, out gameOver,
                    out promotionChar, out parseString);
                if (gameOver) ++gameCount;
            }
            Assert.AreEqual(1363050, gameCount);
        }
        // end T012_readAllGames()


        // Read a file with three games that are all the same.
        //[TestMethod]
        //public void T013_countThreeGames()
        //{
        //    string parseString;
        //    string path = System.Environment.GetEnvironmentVariable("HOMEPATH");
        //    GameReader reader = new GameReader(path + "\\chessGames\\cs242-threeSameGame.pgn");
        //    string sourceSquare = "";
        //    string destinationSquare = "";
        //    bool gameOver = false;
        //    int tableSize = 1024 * 1024 * 2 * 51;
        //    HashWithLinearProbe<ulong, int> hashTable = new HashWithLinearProbe<ulong, int>(tableSize);
        //    int seed = 3242351;
        //    ChessHash chessHash = new ChessHash(seed);
        //    chessHash.SetupBoard();
        //    GameState state = new GameState();
        //    state.SetupBoard();

        //    int gameCount = 0;
        //    string lastSource = "";
        //    string lastDest = "";
        //    bool eof = false;
        //    int gameNumber = 0;
        //    char promotionChar;
        //    while (!eof)
        //    {
        //        eof = reader.GetNextMove(out sourceSquare, out destinationSquare, out gameOver,
        //            out promotionChar, out parseString);
        //        if (!eof)
        //        {
        //            if (gameOver)
        //            {
        //                chessHash.SetupBoard();
        //                state.SetupBoard();
        //                ++gameNumber;
        //            }
        //            else
        //            {
        //                char piece = state.GetSquareOccupant(sourceSquare);
        //                chessHash.MakeMove(piece, sourceSquare, destinationSquare);
        //                state.MakeMove(sourceSquare, destinationSquare);
        //                int count = 0;
        //                if (hashTable.Contains(chessHash.Hash))
        //                {
        //                    count = hashTable.Get(chessHash.Hash);
        //                }
        //                Assert.AreEqual(gameNumber, count);
        //                hashTable.Put(chessHash.Hash, count + 1);
        //            }
        //        }
        //    }

        //    foreach (HashTableEntry<ulong, int> entry in hashTable)
        //    {
        //        Assert.AreEqual(3, entry.Payload);
        //        int count = entry.Payload;
        //    }
        //}
        //// end T013_countThreeGames()


        // Read all the games and place the counts in a hashtable
        //[TestMethod]
        //public void T014_hashAllGames()
        //{
        //    string path = System.Environment.GetEnvironmentVariable("HOMEPATH");
        //    GameReader reader = new GameReader(path + "\\chessGames\\cs242-allGames.pgn");
        //    string sourceSquare = "";
        //    string destinationSquare = "";
        //    bool gameOver = false;
        //    int tableSize = 1024 * 1024 * 2 * 97;
        //    HashWithLinearProbe<ulong, int> hashTable = new HashWithLinearProbe<ulong, int>(tableSize);
        //    int seed = 3242351;
        //    ChessHash chessHash = new ChessHash(seed);
        //    chessHash.SetupBoard();
        //    GameState state = new GameState();
        //    state.SetupBoard();

        //    int gameCount = 0;
        //    string lastSource = "";
        //    string lastDest = "";
        //    bool eof = false;
        //    int gameNumber = 0;
        //    char promotionChar;
        //    string parsedString;
        //    while (!eof)
        //    {
        //        eof = reader.GetNextMove(out sourceSquare, out destinationSquare, out gameOver,
        //            out promotionChar, out parsedString);
        //        if (!eof)
        //        {
        //            if (gameOver)
        //            {
        //                chessHash.SetupBoard();
        //                state.SetupBoard();
        //                ++gameNumber;
        //            }
        //            else
        //            {
        //                char piece = state.GetSquareOccupant(sourceSquare);
        //                if (promotionChar != 0) piece = promotionChar;
        //                chessHash.MakeMove(piece, sourceSquare, destinationSquare);
        //                state.MakeMove(sourceSquare, destinationSquare);
        //                int count = 0;
        //                if (hashTable.Contains(chessHash.Hash))
        //                {
        //                    count = hashTable.Get(chessHash.Hash);
        //                }
        //                hashTable.Put(chessHash.Hash, count + 1);
        //            }
        //        }
        //    }
        //}
        //// end T014_hashAllGames()


    }
}
