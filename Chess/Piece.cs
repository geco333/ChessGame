using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess
{
    internal abstract class Piece : Image
    {
        public List<Square> regularlMoves;
        public List<Square> captureMoves;

        public string Color { get; }
        public Square Square { get; private set; }

        public abstract void updateLeagelMoves();

        public Piece(Square square, string color)
        {
            Square = square;
            Color = color;

            MouseDown += ClickOnPiece;
        }
        public bool IsMoveLeagel(Square from, Square to)
        {
            // Checks if to square is in the pieces moves lists.

            if(to.HasPieceOn && to.PieceOnSquare.Color != Color)
            {
                if(captureMoves.Contains(to))
                {
                    return true;
                }
            }

            if(regularlMoves.Contains(to))
            {
                return true;
            }

            return false;
        }
        public void Move(Square from, Square to)
        {
            if(GetType() == typeof(Pawn))
            {
                if((this as Pawn).firstMove)
                {
                    (this as Pawn).firstMove = false;
                }

                if((this as Pawn).queenRow == to.Row)
                {
                    (this as Pawn).QueenAPawn(to);
                    return;
                }
            }
            else if(GetType() == typeof(Rook))
            {
                (this as Rook).HasMoved = true;
            }

            Grid.SetColumn(this, to.Col);
            Grid.SetRow(this, to.Row);

            from.PieceOnSquare = null;
            from.HasPieceOn = false;

            to.HasPieceOn = true;
            to.PieceOnSquare = this;

            Square = to;
            Board.UpdateMovesAndThreats();
            Board.CheckIfKingIsChecked();
        }
        public void ClickOnPiece(object sender, RoutedEventArgs e)
        {
            // If the current player clicked on his own piece.
            if(Color == Board.CurrentPlayerColor)
            {
                if(this != Board.SelectedPiece)
                {
                    if(Board.APieceHasBeenPicked == true)
                    {
                        Board.SelectedRectangle.Stroke = new SolidColorBrush(Colors.Transparent);
                    }

                    HighlighSelectedRectangle(Square);

                    Board.APieceHasBeenPicked = true;
                    Board.SelectedPiece = this;
                    Board.CurrentlySelectedSquare = Square;
                }
            }
            // If the current player clicked on a piece of the opposite color.
            else if(Board.SelectedPiece != null && Board.SelectedPiece != this)
            {
                // Get the from square and to square.
                Square from = Board.SelectedPiece.Square;
                Square to = Square;

                if(Board.SelectedPiece.IsMoveLeagel(from, to))
                {
                    CapturePiece();
                    Board.SwitchPlayer();
                }
            }
        }
        private void CapturePiece()
        {
            Piece capturingPiece = Board.SelectedPiece;

            Source = null;

            if(GetType() == typeof(King))
            {
                Board.GameOver();
            }

            capturingPiece.Move(capturingPiece.Square, Square);

            if(GetType() == typeof(King))
            {
                Board.GameOver();
            }
        }
        private void HighlighSelectedRectangle(Square square)
        {
            string name = ((char)(64 + Square.Col)).ToString() + (9 - Square.Row).ToString();
            Rectangle selectedRectangle = (Application.Current.MainWindow as MainWindow).FindName(name) as Rectangle;
            selectedRectangle.StrokeThickness = 3;
            selectedRectangle.Stroke = new SolidColorBrush(Colors.Red);

            Board.SelectedRectangle = selectedRectangle;
        }
    }
}