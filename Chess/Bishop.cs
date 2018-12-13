using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Chess
{
    internal class Bishop : Piece
    {
        public Bishop(Square square, string color) : base(square, color)
        {
            Uri uri;
            uri = color == "White" ?
                new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\white\WhiteBishop.png") :
                new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\black\BlackBishop.png");
            BitmapImage bmi = new BitmapImage(uri);
            Source = bmi;
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

            // Set NE moves.
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            if(c < 8 && r > 1)
            {
                // Add empty squares
                while(++c < 9 && --r > 0)
                {
                    s = Board.Squares[r][c];

                    if(s.HasPieceOn)
                    {
                        if(s.PieceOnSquare.Color != Color)
                        {
                            captureMoves.Add(s);
                            s.IsThreatened = true;
                            s.ThreatenedBy = Color;
                        }
                        break;
                    }

                    regularlMoves.Add(s);
                }
            }
            // Set SE moves.
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            if(c < 8 && r < 8)
            {
                // Add empty squares
                while(++c < 9 && ++r < 9)
                {
                    s = Board.Squares[r][c];

                    if(s.HasPieceOn)
                    {
                        if(s.PieceOnSquare.Color != Color)
                        {
                            captureMoves.Add(s);
                            s.IsThreatened = true;
                            s.ThreatenedBy = Color;
                        }
                        break;
                    }

                    regularlMoves.Add(s);
                }
            }
            // Set SW moves.
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            if(c > 0 && r < 8)
            {
                // Add empty squares
                while(--c > 0 && ++r < 9)
                {
                    s = Board.Squares[r][c];

                    if(s.HasPieceOn)
                    {
                        if(s.PieceOnSquare.Color != Color)
                        {
                            captureMoves.Add(s);
                            s.IsThreatened = true;
                            s.ThreatenedBy = Color;
                        }
                        break;
                    }

                    regularlMoves.Add(s);
                }
            }
            // Set NW moves.
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            if(c > 1 && r > 1)
            {
                // Add empty squares
                while(--c > 0 && --r > 0)
                {
                    s = Board.Squares[r][c];

                    if(s.HasPieceOn)
                    {
                        if(s.PieceOnSquare.Color != Color)
                        {
                            captureMoves.Add(s);
                            s.IsThreatened = true;
                            s.ThreatenedBy = Color;
                        }
                        break;
                    }

                    regularlMoves.Add(s);
                }
            }
        }
        private static void ResetRowAndColumn(out int r, out int c, out Square s, int R, int C, Square S)
        {
            r = R;
            c = C;
            s = S;
        }
    }
}
