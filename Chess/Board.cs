using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess
{
    internal static class Board
    {
        public static Square[][] Squares;
        public static Rectangle SelectedRectangle;
        public static Piece SelectedPiece;
        public static Square CurrentlySelectedSquare;
        public static string CurrentPlayerColor;
        public static bool APieceHasBeenPicked;
        public static List<Piece> CapturedPieces;

        public static void SetupBoard()
        {
            Squares = new Square[9][];
            CurrentPlayerColor = "White";
            CapturedPieces = new List<Piece>();

            for(int row = 1; row <= 8; row++)
            {
                Squares[row] = new Square[9];

                for(int col = 1; col <= 8; col++)
                {
                    Squares[row][col] = new Square(row, col);
                }
            }
        }
        public static void ClickOnTargetSquare(object sender, MouseButtonEventArgs e)
        {
            if(APieceHasBeenPicked == true)
            {
                int col, row;
                Square selectedSquare;
                Rectangle selectedRectangle;

                // Translates the clicked on rectangle to the right square.
                GetSelectedSquare(sender, out col, out row, out selectedSquare, out selectedRectangle);

                Square from = CurrentlySelectedSquare;
                Square to = selectedSquare;

                if(SelectedPiece.GetType() == typeof(King) &&
                   SelectedPiece.regularlMoves.Contains(to) &&
                   (to.Col == SelectedPiece.Square.Col + 2 || to.Col == SelectedPiece.Square.Col - 3))
                {
                    if(to.Col == SelectedPiece.Square.Col + 2) (SelectedPiece as King).Castle("Short");
                    else (SelectedPiece as King).Castle("Long");
                }
                // Check if the move is legal for the selected piece.
                else if(SelectedPiece.IsMoveLeagel(from, to) == true)
                {
                    // If move is leagel move the piece to the selected square
                    //  and clear all Board flags.
                    SelectedPiece.Move(from, to);

                    // Switch turn to other player.
                    SwitchPlayer();
                }
            }
        }


        public static void SwitchPlayer()
        {
            ClearBoardFlags();
            Board.CurrentPlayerColor = Board.CurrentPlayerColor == "White" ? "Black" : "White";
        }
        public static void GameOver()
        {
            string winner = CurrentPlayerColor;
            TextBlock tb = new TextBlock { Text = $"Game Over {winner} Wins" };

            Grid.SetColumn(tb, 3);
            Grid.SetColumnSpan(tb, 4);
            Grid.SetRow(tb, 3);
            Grid.SetRowSpan(tb, 3);

            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.FontSize = 30;
            tb.Foreground = new SolidColorBrush(Colors.Black);
            tb.Background = new SolidColorBrush(Colors.White);

            Grid gridBoard = (Application.Current.MainWindow as MainWindow).FindName("gridBoard") as Grid;
            gridBoard.Children.Add(tb);
        }
        public static void UpdateMovesAndThreats()
        {
            ClearThreates();

            for(int i = 1; i < Squares.Length; i++)
                for(int j = 1; j < Squares[i].Length; j++)
                    if(Squares[i][j].HasPieceOn) Squares[i][j].PieceOnSquare.updateLeagelMoves();
        }
        public static void CheckIfKingIsChecked()
        {
            // Scan board for the other player's king,
            // then check if he is mated.
            for(int i = 1; i < 9; i++)
                for(int j = 1; j < 9; j++)
                    if(Squares[i][j].PieceOnSquare is King &&
                       Squares[i][j].PieceOnSquare.Color != CurrentPlayerColor)
                    {
                        (Squares[i][j].PieceOnSquare as King).CheckIfMated();
                    }
        }

        private static void ClearThreates()
        {
            for(int i = 1; i < Squares.Length; i++)
                for(int j = 1; j < Squares[i].Length; j++)
                {
                    Squares[i][j].ThreatenedBy.Clear();
                    Squares[i][j].IsThreatened = false;
                }
        }
        private static void GetSelectedSquare(object sender, out int col, out int row, out Square selectedSquare, out Rectangle selectedRectangle)
        {
            // Get the piece image's board coordinates.
            col = Grid.GetColumn(sender as FrameworkElement);
            row = Grid.GetRow(sender as FrameworkElement);

            // Get the square and the rectangle currently selected.
            selectedSquare = Board.Squares[row][col];

            string squareName = ((char)(64 + col)).ToString() + (9 - row).ToString();
            Grid gridBoard = (Grid)(Application.Current.MainWindow as MainWindow).FindName("gridBoard");
            selectedRectangle = (Rectangle)gridBoard.FindName(squareName);
        }
        private static void ClearBoardFlags()
        {
            Board.SelectedRectangle.Stroke = new SolidColorBrush(Colors.Transparent);

            Board.APieceHasBeenPicked = false;
            Board.SelectedPiece = null;
            Board.SelectedRectangle = null;
            Board.CurrentlySelectedSquare = null;
        }
    }
}