using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using LearningApp.Classes;

namespace LearningApp
{
    public partial class MainPage : ContentPage
    {
        List<List<SquareButton>> buttons = new List<List<SquareButton>>();

        ImageButton ChosenSquare;
        Color ChosenSquareColor;

        public MainPage()
        {
            InitializeComponent();
            Padding = new Thickness(5, 20, 5, 20);
            StackLayout mainLayout = new StackLayout()
            {
                Margin = new Thickness(0, 0, 0, 0),
            };

            Grid squaresBoard = new Grid()
            {
                ColumnSpacing = 0,
                RowSpacing = 0
            };

            for (int i = 0; i < 8; i++)
            {
                squaresBoard.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(44, GridUnitType.Absolute)});
                squaresBoard.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(44, GridUnitType.Absolute) });

                buttons.Add(new List<SquareButton>());
            }

            bool isDark = false;
            for (int height = 0; height < 8; height++)
            {
                for (int width = 0; width < 8; width++)
                {
                    SquareButton button = new SquareButton()
                    {
                        Padding = new Thickness(0, 0, 0, 0),
                        Margin = new Thickness(0, 0, 0, 0),
                        CornerRadius = 0                        
                    };

                    button.Clicked += OnSquareClick;

                    if (isDark)
                        button.BackgroundColor = Color.MediumSeaGreen;
                    else
                        button.BackgroundColor = Color.WhiteSmoke;

                    buttons[height].Add(button);
                    squaresBoard.Children.Add(button, width, height);

                    isDark = !isDark;
                }
                isDark = !isDark;
            }
            mainLayout.Children.Add(squaresBoard);

            for (int i = 0; i < 8; i++)
            {
                buttons[0][i].SetPiece(PieceColor.Black, Piece.Pawn);
                buttons[6][i].SetPiece(PieceColor.White, Piece.Pawn);
            }

            var row = buttons[1];
            Piece[] pieces = { Piece.Rook, Piece.Knight, Piece.Bishop, Piece.Queen, Piece.King, Piece.Bishop, Piece.Knight, Piece.Rook};

            for (int i = 0; i < 2; i++)
            {
                for (int ind = 0; ind < 8; ind++)
                {
                    row[ind].SetPiece((PieceColor)i, pieces[ind]);
                }
                row = buttons[7];
            }

            Content = mainLayout;
        }

        private void OnSquareClick(object sender, EventArgs e)
        {
            SquareButton temp = (SquareButton)sender;

            if (ChosenSquare != null)
            {
                ChosenSquare.BackgroundColor = ChosenSquareColor;
                ChosenSquare = null;
                return;
            }

            if (temp.Source == null)
                return;

            ChosenSquareColor = temp.BackgroundColor;
            if (temp.BackgroundColor == Color.WhiteSmoke)
            {
                temp.BackgroundColor = Color.Yellow;
            }
            else if (temp.BackgroundColor == Color.MediumSeaGreen)
            {
                temp.BackgroundColor = Color.Gold;
            }

            ChosenSquare = temp;

            var coords = CoordinatesOf(buttons, temp);
            switch (temp.Type)
            {
                case Piece.Pawn:
                    break;
                case Piece.Bishop:
                    break;
                case Piece.Knight:
                    KnightMoves(coords.Item1, coords.Item2, temp.Color);
                    break;
                case Piece.Rook:
                    break;
                case Piece.Queen:
                    break;
                case Piece.King:
                    break;
            }
        }

        private void KnightMoves(int height, int width, PieceColor color)
        {
            if (CheckSquare(height - 2, width + 1, color))
                buttons[height - 2][width + 1].BackgroundColor = Color.Black;
        }
        
        private bool CheckSquare(int height, int width, PieceColor colorToCheck)
        {
            if (height > buttons.Count || height < 0)
                return false;

            if (width > buttons.Count || width < 0)
                return false;

            if (buttons[height][width].Type == Piece.None)
                return true;

            return buttons[height][width].Color != colorToCheck;
        }

        private Tuple<int, int> CoordinatesOf(List<List<SquareButton>> matrix, SquareButton value)
        {
            int h = matrix.Count; // width
            int w = matrix[0].Count; // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x][y].Equals(value))
                        return Tuple.Create(x, y);
                }
            }

            return Tuple.Create(-1, -1);
        }
    }
}
