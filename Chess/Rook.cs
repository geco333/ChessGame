using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Chess
{
    internal class Rook : Piece
    {
        public bool HasMoved = false;

        public Rook(Square square, string color) : base(square, color)
        {
            Uri uri;
            uri = color == "White" ?
                new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\white\WhiteRook.png") :
                new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\black\BlackRook.png");
            BitmapImage bmi = new BitmapImage(uri);
            Source = bmi;
        }
        // N moves.
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
                // Add empty squares
                while(--r > 0)
                {
                    s = Board.Squares[r][c];

                    if(s.HasPieceOn)
                    {
                        if(s.PieceOnSquare.Color != Color)
                        {
                            captureMoves.Add(s);
                            s.IsThreatened = true;
                            s.ThreatenedBy.Add(Square);
                        }
                        break;
                    }

                    regularlMoves.Add(s);
                }
            }
            // Set E moves.
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            if(c < 8)
            {
                // Add empty squares
                while(++c < 9)
                {
                    s = Board.Squares[r][c];

                    if(s.HasPieceOn)
                    {
                        if(s.PieceOnSquare.Color != Color)
                        {
                            captureMoves.Add(s);
                            s.IsThreatened = true;
                            s.ThreatenedBy.Add(Square);
                        }
                        break;
                    }

                    regularlMoves.Add(s);
                }
            }
            // Set S moves.
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            if(r < 8)
            {
                s = S;

                // Add empty squares
                while(++r < 9)
                {
                    s = Board.Squares[r][c];

                    if(s.HasPieceOn)
                    {
                        if(s.PieceOnSquare.Color != Color)
                        {
                            captureMoves.Add(s);
                            s.IsThreatened = true;
                            s.ThreatenedBy.Add(Square);
                        }
                        break;
                    }

                    regularlMoves.Add(s);
                }
            }
            // Set W moves.
            ResetRowAndColumn(out r, out c, out s, R, C, S);
            if(c > 1)
            {
                // Add empty squares
                while(--c > 0)
                {
                    s = Board.Squares[r][c];

                    if(s.HasPieceOn)
                    {
                        if(s.PieceOnSquare.Color != Color)
                        {
                            captureMoves.Add(s);
                            s.IsThreatened = true;
                            s.ThreatenedBy.Add(Square);
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
