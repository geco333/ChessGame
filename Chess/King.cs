using System;
using System.Collections.Generic;
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

            Square s, S;
            int r, c, R, C;
            r = R = Square.Row;
            c = C = Square.Col;
            S = Board.Squares[Square.Row][Square.Col];

            // Set N moves.
            if(r > 1)
            {
                s = Board.Squares[--r][c];

                if(s.HasPieceOn)
                {
                    if(s.PieceOnSquare.Color != Color)
                    {
                        captureMoves.Add(s);
                    }
                    else
                    {
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                }
                else if(!s.IsThreatened ||
                        (s.IsThreatened && s.ThreatenedBy == Color))
                {
                    regularlMoves.Add(s);
                }
            }
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            // Set NE moves.
            if(r > 1 && c < 8)
            {
                s = Board.Squares[--r][++c];

                if(s.HasPieceOn)
                {
                    if(s.PieceOnSquare.Color != Color)
                    {
                        captureMoves.Add(s);
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                    else
                    {
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                }
                else if(!s.IsThreatened || (s.IsThreatened && s.ThreatenedBy == Color))
                {
                    regularlMoves.Add(s);
                }

            }
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            // Set E moves.
            if(c < 8)
            {
                s = Board.Squares[r][++c];

                if(s.HasPieceOn)
                {
                    if(s.PieceOnSquare.Color != Color)
                    {
                        captureMoves.Add(s);
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                    else
                    {
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                }
                else if(!s.IsThreatened || (s.IsThreatened && s.ThreatenedBy == Color))
                {
                    regularlMoves.Add(s);
                }
            }
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            // Set SE moves.
            if(r < 8 && c < 8)
            {
                s = Board.Squares[++r][++c];

                if(s.HasPieceOn)
                {
                    if(s.PieceOnSquare.Color != Color)
                    {
                        captureMoves.Add(s);
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                    else
                    {
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                }
                else if(!s.IsThreatened || (s.IsThreatened && s.ThreatenedBy == Color))
                {
                    regularlMoves.Add(s);
                }
            }
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            // Set S moves.
            if(r < 8)
            {
                s = Board.Squares[++r][c];

                if(s.HasPieceOn)
                {
                    if(s.PieceOnSquare.Color != Color)
                    {
                        captureMoves.Add(s);
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                    else
                    {
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                }
                else if(!s.IsThreatened || (s.IsThreatened && s.ThreatenedBy == Color))
                {
                    regularlMoves.Add(s);
                }
            }
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            // Set SW moves.
            if(r < 8 && c > 1)
            {
                s = Board.Squares[++r][--c];

                if(s.HasPieceOn)
                {
                    if(s.PieceOnSquare.Color != Color)
                    {
                        captureMoves.Add(s);
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                    else
                    {
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                }
                else if(!s.IsThreatened || (s.IsThreatened && s.ThreatenedBy == Color))
                {
                    regularlMoves.Add(s);
                }
            }
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            // Set W moves.
            if(c > 1)
            {
                s = Board.Squares[r][--c];

                if(s.HasPieceOn)
                {
                    if(s.PieceOnSquare.Color != Color)
                    {
                        captureMoves.Add(s);
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                    else
                    {
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                }
                else if(!s.IsThreatened || (s.IsThreatened && s.ThreatenedBy == Color))
                {
                    regularlMoves.Add(s);
                }
            }
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            // Set NW moves.
            if(r > 1 && c > 1)
            {
                s = Board.Squares[--r][--c];

                if(s.HasPieceOn)
                {
                    if(s.PieceOnSquare.Color != Color)
                    {
                        captureMoves.Add(s);
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                    else
                    {
                        s.IsThreatened = true;
                        s.ThreatenedBy = Color;
                    }
                }
                else if(!s.IsThreatened || (s.IsThreatened && s.ThreatenedBy == Color))
                {
                    regularlMoves.Add(s);
                }
            }
            // Set castle moves.
            if(!HasMoved)
            {
                if(Square.Col < 7 && Square.Col > 3)
                {
                    // Short castle.
                    if(!Board.Squares[Square.Row][Square.Col + 1].HasPieceOn)
                    {
                        if(!Board.Squares[Square.Row][Square.Col + 1].IsThreatened ||
                            (Board.Squares[Square.Row][Square.Col + 1].IsThreatened &&
                              Board.Squares[Square.Row][Square.Col + 1].ThreatenedBy == Color))
                        {
                            if(!Board.Squares[Square.Row][Square.Col + 2].HasPieceOn)
                            {
                                if(!Board.Squares[Square.Row][Square.Col + 2].IsThreatened ||
                                    (Board.Squares[Square.Row][Square.Col + 2].IsThreatened &&
                                      Board.Squares[Square.Row][Square.Col + 2].ThreatenedBy == Color))
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
                            (Board.Squares[Square.Row][Square.Col - 1].IsThreatened &&
                              Board.Squares[Square.Row][Square.Col - 1].ThreatenedBy == Color))
                        {
                            if(!Board.Squares[Square.Row][Square.Col - 2].HasPieceOn)
                            {
                                if(!Board.Squares[Square.Row][Square.Col - 2].IsThreatened ||
                                    (Board.Squares[Square.Row][Square.Col - 2].IsThreatened &&
                                      Board.Squares[Square.Row][Square.Col - 2].ThreatenedBy == Color))
                                {
                                    if(!Board.Squares[Square.Row][Square.Col - 3].HasPieceOn)
                                    {
                                        if(!Board.Squares[Square.Row][Square.Col - 3].IsThreatened ||
                                            (Board.Squares[Square.Row][Square.Col - 3].IsThreatened &&
                                              Board.Squares[Square.Row][Square.Col - 3].ThreatenedBy == Color))
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

        private void ResetRowAndColumn(out int r, out int c, out Square s, int R, int C, Square S)
        {
            r = R;
            c = C;
            s = S;
        }
    }
}
