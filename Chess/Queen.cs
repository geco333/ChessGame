using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Chess
{
    internal class Queen : Piece
    {
        public Queen(Square square, string color) : base(square, color)
        {
            Uri uri;
            uri = color == "White" ?
                new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\white\WhiteQueen.png") :
                new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\black\BlackQueen.png");
            BitmapImage bmi = new BitmapImage(uri);

            Source = bmi;
        }
        public override void updateLeagelMoves()
        {
            regularlMoves = new List<Square>();
            captureMoves = new List<Square>();

            int r = Square.Row;
            int c = Square.Col;

            // Set N moves.
            if(r > 1) SetMoveInDirection(r, c, -1, 0, (x, y) => x > 0);
            // Set NE moves.
            if(c < 8 && r > 1) SetMoveInDirection(r, c, -1, 1, (x, y) => y < 9 && x > 0);
            // Set E moves.
            if(c < 8) SetMoveInDirection(r, c, 0, 1, (x, y) => y < 9);
            // Set SE moves.
            if(c < 8 && r < 8) SetMoveInDirection(r, c, 1, 1, (x, y) => x < 9 && y < 9);
            // Set S moves.
            if(r < 8) SetMoveInDirection(r, c, 1, 0, (x, y) => x < 9);
            // Set SW moves.
            if(c > 0 && r < 8) SetMoveInDirection(r, c, 1, -1, (x, y) => x < 9 && y > 0);
            // Set W moves.
            if(c > 1) SetMoveInDirection(r, c, 0, -1, (x, y) => y > 0);
            // Set NW moves.
            if(c > 1 && r > 1) SetMoveInDirection(r, c, -1, -1, (x, y) => x > 0 && y > 0);
        }

        private void SetMoveInDirection(int r, int c, int factorR, int factorC, Func<int, int, bool> whileCheck)
        {
            // Add empty squares.
            // If a square has a piece of opposite color add it to noves and return.
            while(whileCheck(r, c))
            {
                Square s = Board.Squares[r][c];

                if(s.HasPieceOn &&
                   s.PieceOnSquare.Color != Color)
                {
                    captureMoves.Add(s);
                    s.IsThreatened = true;
                    s.ThreatenedBy.Add(Square);

                    return;
                }

                regularlMoves.Add(s);
                r += factorR;
                c += factorC;
            }
        }
    }
}