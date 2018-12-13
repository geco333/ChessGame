using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Chess
{
    internal class Knight : Piece
    {
        public Knight(Square square, string color) : base(square, color)
        {
            Uri uri;
            uri = color == "White" ?
                new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\white\WhiteKnight.png") :
                new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\black\BlackKnight.png");
            BitmapImage bmi = new BitmapImage(uri);
            Source = bmi;
        }
        public override void updateLeagelMoves()
        {
            regularlMoves = new List<Square>();
            captureMoves = new List<Square>();

            List<Square> S = new List<Square>();
            int r = Square.Row;
            int c = Square.Col;

            // x <
            //   ^
            //   ^
            if(Square.Row - 2 > 0 && Square.Col - 1 > 0)
            {
                AddSquareToList(S, -2, -1);
            }
            //   > x
            //   ^
            //   ^
            if(Square.Row - 2 > 0 && Square.Col + 1 < 9)
            {
                AddSquareToList(S, -2, 1);
            }
            // x
            // ^ < <
            if(Square.Row - 1 > 0 && Square.Col - 2 > 0)
            {
                AddSquareToList(S, -1, -2);
            }
            // v < <
            // x
            if(Square.Row + 1 < 9 && Square.Col - 2 > 0)
            {
                AddSquareToList(S, 1, -2);
            }
            //   v
            //   v
            // x <
            if(Square.Row + 2 < 9 && Square.Col - 1 > 0)
            {
                AddSquareToList(S, 2, -1);
            }
            //   v
            //   v
            //   > x
            if(Square.Row + 2 < 9 && Square.Col + 1 < 9)
            {
                AddSquareToList(S, 2, 1);
            }
            //     x
            // > > ^
            if(Square.Row - 1 > 0 && Square.Col + 2 < 9)
            {
                AddSquareToList(S, -1, 2);
            }
            // > > v
            //     x
            if(Square.Row + 1 < 9 && Square.Col + 2 < 9)
            {
                AddSquareToList(S, 1, 2);
            }
        }

        private void AddSquareToList(List<Square> S, int r, int c)
        {
            Square s = Board.Squares[Square.Row + r][Square.Col + c];

            if(s.HasPieceOn)
            {
                if(s.PieceOnSquare.Color != Color)
                {
                    captureMoves.Add(s);
                    s.IsThreatened = true;
                    s.ThreatenedBy = Color;
                }
            }
            else
            {
                regularlMoves.Add(s);
            }
        }
    }
}
