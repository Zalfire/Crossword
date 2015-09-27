using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Builder
{
    //Todo: summary CrosswordGrid and sub summary
    class CrosswordGrid
    {
        #region Properties
        private BidimensionalArray<Char> _grid;

        public List<PlacedWord> WordsAndBlanks; 

        public readonly char Blank;
        #endregion

        public CrosswordGrid(int columnCount, int rowCount, char blank)
        {
            Blank = blank;

            _grid = new BidimensionalArray<char>(columnCount, rowCount);

            WordsAndBlanks = new List<PlacedWord>();
        }

        private void Add(Word word, Word.Placement placement)
        {
            _grid.Insert(word.Value.ToCharArray(), placement.Column, placement.Row, placement.IsHorizontal);
            WordsAndBlanks.Add(new PlacedWord(placement, word));

            Word.Placement blankPlacement = (placement.IsHorizontal)
                ? new Word.Placement(placement.Column + word.Length, placement.Row)
                : new Word.Placement(placement.Column, placement.Row + word.Length);

            if (_grid.IsInGrid(blankPlacement.Column, blankPlacement.Row))
            {
                _grid.Insert(Blank, blankPlacement.Column, blankPlacement.Row);
                WordsAndBlanks.Add(new PlacedWord(placement, null));
            }

        }
    }
}
