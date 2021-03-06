﻿using System.Collections.Generic;

namespace Chess
{
    internal class Square
    {
        public bool HasPieceOn;
        public Piece PieceOnSquare;
        public int Col { get; }
        public int Row { get; }
        public bool IsThreatened;
        public List<Square> ThreatenedBy;

        public Square(int row, int col)
        {
            Col = col;
            Row = row;
            HasPieceOn = false;
            IsThreatened = false;
            ThreatenedBy = new List<Square>();
        }
    }
}
