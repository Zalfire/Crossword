using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Builder
{
    class CrosswordGrid
    {
        #region Properties
        private TwoDimensionArray<Char> _grid;

        public readonly Dictionary<Word.Placement, Word> Words;
        public readonly List<Point> Blanks;

        public readonly char Blank;
        #endregion

        public CrosswordGrid(int columnCount, int rowCount, char blank)
        {
            Blank = blank;

            _grid = new TwoDimensionArray<char>(columnCount, rowCount);

            Words = new Dictionary<Word.Placement, Word>();
            Blanks = new List<Point>();
        }

        private void Insert(Word word, Word.Placement placement) =>
            _grid.Insert(word.Value.ToCharArray(), placement.Column, placement.Row, placement.IsHorizontal);

    }
}
