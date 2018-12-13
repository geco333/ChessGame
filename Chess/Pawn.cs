using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    internal class Pawn : Piece
    {
        public bool firstMove = true;
        public int queenRow;

        public Pawn(Square square, string color) : base(square, color)
        {
            Uri uri;
            uri = color == "White" ?
                    new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\white\WhitePawn.png") :
                    new Uri(@"C:\Users\Geco\Documents\Visual Studio 2017\Projects\Chess\Chess\png\black\BlackPawn.png");
            BitmapImage bmi = new BitmapImage(uri);
            Source = bmi;

            queenRow = Color == "White" ? 1 : 8;
        }
        public void QueenAPawn(Square to)
        {
            Source = null;

            Queen queen = new Queen(Square, Color);
            ((Application.Current.MainWindow as MainWindow).FindName("gridBoard") as Grid).Children.Add(queen);
            queen.Move(queen.Square, to);
        }
        public override void updateLeagelMoves()
        {
            regularlMoves = new List<Square>();
            captureMoves = new List<Square>();

            int colorFactor = Color == "White" ? -1 : 1;

            // Add normal one square forword move.
            if(Square.Row > 1)
            {
                if(!Board.Squares[Square.Row + colorFactor][Square.Col].HasPieceOn)
                {
                    regularlMoves.Add(Board.Squares[Square.Row + colorFactor][Square.Col]);
                }
            }
            // Add two moves if first turn.
            if(firstMove == true)
            {
                if(!Board.Squares[Square.Row + colorFactor * 2][Square.Col].HasPieceOn)
                {
                    regularlMoves.Add(Board.Squares[Square.Row + colorFactor * 2][Square.Col]);
                }
            }
            // Add capture moves limited to edge columns.
            if(Square.Col > 1)
            {
                captureMoves.Add(Board.Squares[Square.Row + colorFactor][Square.Col - 1]);
                Board.Squares[Square.Row + colorFactor][Square.Col - 1].IsThreatened = true;
                Board.Squares[Square.Row + colorFactor][Square.Col - 1].ThreatenedBy = Color;
            }
            if(Square.Col < 8)
            {
                captureMoves.Add(Board.Squares[Square.Row + colorFactor][Square.Col + 1]);
                Board.Squares[Square.Row + colorFactor][Square.Col + 1].IsThreatened = true;
                Board.Squares[Square.Row + colorFactor][Square.Col + 1].ThreatenedBy = Color;
            }
        }
    }
}
