using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Builder
{
    //Todo: summary CrosswordGrid and sub summary
    public class CrosswordGrid
    {
        #region Properties
        private BidimensionalArray<Char> _grid;

        //public List<PlacedWord> WordsAndBlanks; 
        public Dictionary<Word.Placement, Word> WordsAndBlanks;

        private readonly WordsDictionary _Words;

        public readonly char Blank;
        #endregion

        public CrosswordGrid(int columnCount, int rowCount, char blank)
        {
            Blank = blank;

            _Words = new WordsDictionary();
            _grid = new BidimensionalArray<char>(columnCount, rowCount);

            //WordsAndBlanks = new List<PlacedWord>();
            WordsAndBlanks = new Dictionary<Word.Placement, Word>();
        }

        public void BuildGrid()
        {
            Word.Placement firstPlacement = new Word.Placement(0, 0);
            Restriction firstRestriction = new Restriction(1, _grid.ColumnCount + 1);
            AddWordUntilFull(firstPlacement, firstRestriction);
        }

        private void AddWordUntilFull(Word.Placement placement, Restriction restriction)
        {
            var words = _Words.GetWords(restriction).Where(w => !WordsAndBlanks.ContainsValue(w));
            int count = words.Count();
            foreach (Word word in words)
            {
                Add(word, placement);

                Word.Placement nextPlacement = GetNextPlacement(placement, word.Length);
                if (nextPlacement == null)
                    return;

                Restriction nextRestriction = GetRestriction(nextPlacement);
                if (nextRestriction == null)
                {
                    nextPlacement = placement.IsHorizontal
                        ? new Word.Placement(nextPlacement.Column, 1)
                        : new Word.Placement(1, nextPlacement.Row);
                    nextRestriction = GetRestriction(nextPlacement);
                }

                AddWordUntilFull(nextPlacement, nextRestriction);
            }
        }

        private void Add(Word word, Word.Placement placement)
        {
            _grid.Insert(word.Value.ToCharArray(), placement.Column, placement.Row, placement.IsHorizontal);
            if (WordsAndBlanks.ContainsKey(placement))
                WordsAndBlanks[placement] = word;
            else
                WordsAndBlanks.Add(placement, word);

            Word.Placement blankPlacement = (placement.IsHorizontal)
                ? new Word.Placement(placement.Column + word.Length, placement.Row)
                : new Word.Placement(placement.Column, placement.Row + word.Length);

            if (_grid.IsInGrid(blankPlacement.Column, blankPlacement.Row))
            {
                _grid.Insert(Blank, blankPlacement.Column, blankPlacement.Row);
                if (!WordsAndBlanks.ContainsKey(blankPlacement))
                    WordsAndBlanks.Add(blankPlacement, null);//Todo: remove blanks when writing over
            }
        }

        private Word.Placement GetNextPlacement(Word.Placement currentPlacement, int wordLengthAdded)
        {
            if (currentPlacement.IsHorizontal)
            {
                if (currentPlacement.Column + wordLengthAdded + 1 >= _grid.ColumnCount)
                {
                    if (currentPlacement.Row > _grid.ColumnCount - 1) {
                        if (currentPlacement.Row + 1 > _grid.RowCount - 1)
                            //grid is full
                            return null;

                        //all columns are full, go next row
                        return new Word.Placement(0, currentPlacement.Row + 1);
                    }

                    //Change direction (horizontal OR vertical)
                    return new Word.Placement(currentPlacement.Row, 0, false);
                }

                //LIne isn't finished, go after the blank
                return new Word.Placement(currentPlacement.Column + wordLengthAdded + 1, currentPlacement.Row);
            }
            else
            {
                if (currentPlacement.Row + wordLengthAdded + 1 >= _grid.RowCount)
                {
                    if (currentPlacement.Column > _grid.RowCount - 1)
                    {
                        if (currentPlacement.Column + 1 > _grid.ColumnCount - 1)
                            //grid is full
                            return null;

                        //all rows are full, go next column
                        return new Word.Placement(currentPlacement.Column + 1, 0, false);
                    }

                    //Change direction (horizontal OR vertical) and adds one
                    return new Word.Placement(0, currentPlacement.Column + 1);
                }

                //Line isn't finished, go after the blank
                return new Word.Placement(currentPlacement.Column, currentPlacement.Row + wordLengthAdded + 1, false);
            }
        }

        private Restriction GetRestriction(Word.Placement placement, int min = 1)
        {
            #region variables
            List<RestrictedCharacter> restrictedCharacters = new List<RestrictedCharacter>();
            int count;
            int index;
            if (placement.IsHorizontal)
            {
                index = placement.Column;
                count = _grid.ColumnCount;
            }
            else
            {
                index = placement.Row;
                count = _grid.RowCount;
            }
            int max = count - index + 1;
            Char character;
            #endregion
            while (index < count)
            {
                character = placement.IsHorizontal
                    ? _grid[index, placement.Row]
                    : _grid[placement.Column, index];

                if (_grid.IsDefault(character))
                {
                    if (character == Blank)
                    {
                        max = index + 1;
                        break;
                    }
                    restrictedCharacters.Add(new RestrictedCharacter(character, index));
                }

                index++;
            }        
            if (max == 0) // blank on first index
                return null;

            return new Restriction(min, max, restrictedCharacters);
        }

    }
}
