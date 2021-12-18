using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LearningApp.Classes
{
    class SquareButton : ImageButton
    {
        public bool HaveMoved = false;

        private Piece type = Piece.None;
        public Piece Type
        {
            get => type;
            set
            {
                type = value;
                Source = color.ToString() + type.ToString();
            }
        }

        private PieceColor color;
        public PieceColor Color
        {
            get => color;
            set
            {
                color = value;
                Source = color.ToString() + type.ToString();
            }
        }

        public void SetPiece(PieceColor color, Piece piece)
        {
            Type = piece;
            Color = color;
        }
    }

    public enum Piece
    {
        Pawn,
        Bishop,
        Knight,
        Rook,
        Queen,
        King,
        None
    }

    public enum PieceColor
    {
        Black,
        White
    }
}
