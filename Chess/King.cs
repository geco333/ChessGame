using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    internal class King : Piece
    {
        public bool HasMoved = false;

        public King(Square square, string color) : base(square, color)
        {
            Uri uri;
            uri = color == "White" ?
                new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\white\Whiteking.png") :
                new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\black\BlackKing.png");
            BitmapImage bmi = new BitmapImage(uri);
            Source = bmi;

            Name = color == "White" ? "WhiteKing" : "BlackKing";
            (Application.Current.MainWindow.FindName("gridBoard") as Grid).RegisterName(Name, this);
        }
        public void Castle(string castle)
        {
            Rook rookToCastle;
            Square rookCastleMoveToSquare;

            // Short castle.
            if(castle == "Short")
            {
                rookToCastle = Board.Squares[Square.Row][8].PieceOnSquare as Rook;
                rookCastleMoveToSquare = Board.Squares[rookToCastle.Square.Row][rookToCastle.Square.Col - 2];

                rookToCastle.Move(rookToCastle.Square, rookCastleMoveToSquare);
                Move(Square, Board.Squares[Square.Row][Square.Col + 2]);
            }
            // Long castle.
            else
            {
                rookToCastle = Board.Squares[Square.Row][1].PieceOnSquare as Rook;
                rookCastleMoveToSquare = Board.Squares[rookToCastle.Square.Row][rookToCastle.Square.Col + 2];

                rookToCastle.Move(rookToCastle.Square, rookCastleMoveToSquare);
                Move(Square, Board.Squares[Square.Row][Square.Col - 3]);
            }

            Board.SwitchPlayer();
        }
        public override void updateLeagelMoves()
        {
            regularlMoves = new List<Square>();
            captureMoves = new List<Square>();

            int r = Square.Row;
            int c = Square.Col;

            // Set N moves.
            if(r > 1) SetMoveInDirection(r - 1, c);
            // Set NE moves.
            if(r > 1 && c < 8) SetMoveInDirection(r - 1, c + 1);
            // Set E moves.
            if(c < 8) SetMoveInDirection(r, c + 1);
            // Set SE moves.
            if(r < 8 && c < 8) SetMoveInDirection(r + 1, c + 1);
            // Set S moves.
            if(r < 8) SetMoveInDirection(r + 1, c);
            // Set SW moves.
            if(r < 8 && c > 1) SetMoveInDirection(r + 1, c - 1);
            // Set W moves.
            if(c > 1) SetMoveInDirection(r, c - 1);
            // Set NW moves.
            if(r > 1 && c > 1) SetMoveInDirection(r - 1, c - 1);
            // Set castle moves.
            if(!HasMoved)
            {
                if(Square.Col < 7 && Square.Col > 3)
                {
                    // Short castle.
                    if(!Board.Squares[Square.Row][Square.Col + 1].HasPieceOn)
                    {
                        if(!Board.Squares[Square.Row][Square.Col + 1].IsThreatened ||
                            Board.Squares[Square.Row][Square.Col + 1].ThreatenedBy.All(sq => sq.PieceOnSquare.Color == Color))
                        {
                            if(!Board.Squares[Square.Row][Square.Col + 2].HasPieceOn)
                            {
                                if(!Board.Squares[Square.Row][Square.Col + 2].IsThreatened ||
                            Board.Squares[Square.Row][Square.Col + 2].ThreatenedBy.All(sq => sq.PieceOnSquare.Color == Color))
                                {

                                    regularlMoves.Add(Board.Squares[Square.Row][Square.Col + 2]);
                                }
                            }
                        }
                    }
                    // Long castle.
                    if(!Board.Squares[Square.Row][Square.Col - 1].HasPieceOn)
                    {
                        if(!Board.Squares[Square.Row][Square.Col - 1].IsThreatened ||
                            Board.Squares[Square.Row][Square.Col - 1].ThreatenedBy.All(sq => sq.PieceOnSquare.Color == Color))
                        {
                            if(!Board.Squares[Square.Row][Square.Col - 2].HasPieceOn)
                            {
                                if(!Board.Squares[Square.Row][Square.Col - 2].IsThreatened ||
                            Board.Squares[Square.Row][Square.Col - 2].ThreatenedBy.All(sq => sq.PieceOnSquare.Color == Color))
                                {
                                    if(!Board.Squares[Square.Row][Square.Col - 3].HasPieceOn)
                                    {
                                        if(!Board.Squares[Square.Row][Square.Col - 3].IsThreatened ||
                            Board.Squares[Square.Row][Square.Col - 3].ThreatenedBy.All(sq => sq.PieceOnSquare.Color == Color))
                                        {
                                            regularlMoves.Add(Board.Squares[Square.Row][Square.Col - 3]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void CheckIfMated()
        {
            if(Square.IsThreatened &&
               Square.ThreatenedBy.Any(sq => sq.PieceOnSquare.Color == Color))
            {
                int movesCounter = regularlMoves.Count + captureMoves.Count;

                foreach(Square s in regularlMoves)
                    if(s.IsThreatened) movesCounter--;

                foreach(Square s in captureMoves)
                    if(s.IsThreatened) movesCounter--;

                if(movesCounter == 0) Board.GameOver();
            }
        }

        private void SetMoveInDirection(int r, int c)
        {
            // Target square.
            Square s = Board.Squares[r][c];

            // If target square is threatened by opponent don't add move.
            if(s.IsThreatened &&
               s.ThreatenedBy.Any(sq => sq.PieceOnSquare.Color != Color)) return;

            if(s.HasPieceOn &&
               s.PieceOnSquare.Color != Color)
            {
                captureMoves.Add(s);
                s.IsThreatened = true;
                s.ThreatenedBy.Add(Square);
            }
            else regularlMoves.Add(s);
        }
    }
}
