using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CreateBoard();
            SetPicesOnBoard();

            Board.UpdateMovesAndThreats();
        }

        private void CreateBoard()
        {
            Board.SetupBoard();

            // Create the side numbers and top letters in textblocks.
            for(int i = 1; i <= 8; i++)
            {
                TextBlock tbNumber = new TextBlock
                {
                    Text = (9 - i).ToString(),
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Right
                };
                TextBlock tbLetter = new TextBlock
                {
                    Text = ((char)(64 + i)).ToString(),
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Grid.SetColumn(tbNumber, 0);
                Grid.SetRow(tbNumber, i);
                Grid.SetColumn(tbLetter, i);
                Grid.SetRow(tbLetter, 0);

                gridBoard.Children.Add(tbNumber);
                gridBoard.Children.Add(tbLetter);
            }
            // Create the black and white squares.
            for(int i = 1; i <= 8; i++)
            {
                string c = i % 2 == 0 ? "SaddleBrown" : "White";

                for(int j = 1; j <= 8; j++)
                {
                    Rectangle rec = new Rectangle
                    {
                        Fill = new BrushConverter().ConvertFromString(c) as SolidColorBrush
                    };
                    string squareCoordinats = ((char)(64 + j)).ToString() + (9 - i).ToString();
                    rec.Name = squareCoordinats;
                    RegisterName(squareCoordinats, rec);
                    rec.MouseDown += Board.ClickOnTargetSquare;

                    Grid.SetColumn(rec, j);
                    Grid.SetRow(rec, i);

                    gridBoard.Children.Add(rec);

                    c = c == "White" ? "SaddleBrown" : "White";
                }
            }
        }
        private void SetPicesOnBoard()
        {
            string color = "White";
            int pawnsRow = 7;
            int row = 8;

            for(int i = 0; i < 2; i++)
            {
                // Set pawns.
                for(int j = 1; j <= 8; j++)
                {
                    Pawn pawn = new Pawn(Board.Squares[pawnsRow][j], color);
                    AddPieceToBoard(pawn, pawnsRow, j);
                }

                AddPieceToBoard(new Rook(Board.Squares[row][1], color), row, 1);
                AddPieceToBoard(new Rook(Board.Squares[row][8], color), row, 8);
                AddPieceToBoard(new Knight(Board.Squares[row][2], color), row, 2);
                AddPieceToBoard(new Knight(Board.Squares[row][7], color), row, 7);
                AddPieceToBoard(new Bishop(Board.Squares[row][3], color), row, 3);
                AddPieceToBoard(new Bishop(Board.Squares[row][6], color), row, 6);
                AddPieceToBoard(new Queen(Board.Squares[row][4], color), row, 4);
                AddPieceToBoard(new King(Board.Squares[row][5], color), row, 5);

                color = "Black";
                pawnsRow = 2;
                row = 1;
            }
        }
        private void AddPieceToBoard(Piece piece, int row, int col)
        {
            Grid.SetColumn(piece, col);
            Grid.SetRow(piece, row);

            Board.Squares[row][col].PieceOnSquare = piece;
            Board.Squares[row][col].HasPieceOn = true;

            gridBoard.Children.Add(piece);
        }
    }
}
