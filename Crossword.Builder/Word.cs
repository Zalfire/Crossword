using System;
using System.Collections.Generic;
using System.Linq;

namespace Crossword.Builder
{
    //Todo: Word summary
    public class Word
    {
        #region Propreties
        /// <summary><see cref="Guid"/> identification as <see cref="String"/>.</summary>
        public readonly String Id;

        /// <summary>The actual value of the class <see cref="Word"/>.</summary>
        public readonly String Value;

        private readonly Char[] _chars;

        /// <summary>Gets the number of characters in <see cref="Word.Value"/>.</summary>
        public int Length => Value.Length;

        /// <summary>Get the <see cref="Char"/> associated with the specified index in <see cref="Word.Value"/>.</summary>
        public Char this[int index] => Value[index];
        #endregion

        #region Initialization
        /// <summary>
        /// Initialize a new instance of the <see cref="Word"/> class from a <see cref="String"/>.
        /// </summary>
        /// <param name="value">The <see cref="Word.Value"/> of the <see cref="Word"/> class.</param>
        public Word(String value)
        {
            Id = Guid.NewGuid().ToString();

            Value = value.Trim().ToLower(); //Trims and Lower case value
            _chars = Value.ToCharArray();
        }
        #endregion

        public Char[] ToCharArray ()
            => this._chars;

        #region Equality members

        protected bool Equals(Word other)
        {
            return String.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return Value != null 
                ? Value.GetHashCode() 
                : 0;
        }

        #endregion

        //Todo: Placement summary and sub-summary
        public class Placement
        {
            #region Properties
            public readonly int Row;
            
            public readonly int Column;

            /// <summary>Gets if the <see cref="Word"/> should be represented horizontally</summary>
            public readonly bool IsHorizontal;
            #endregion

            public Placement(int column, int row, bool isHorizontal = true)
            {
                Row = row;
                Column = column;
                IsHorizontal = isHorizontal;
            }

            public class EqualityComparer : IEqualityComparer<Placement>
            {
                public bool Equals(Placement x, Placement y)
                {
                    return x.Row == y.Row
                           && x.Column == y.Column
                           && x.IsHorizontal == y.IsHorizontal;
                }

                public int GetHashCode(Placement obj)
                {
                    unchecked
                    {
                        var hashCode = obj.Row;
                        hashCode = (hashCode*397) ^ obj.Column;
                        hashCode = (hashCode*397) ^ obj.IsHorizontal.GetHashCode();
                        return hashCode;
                    }
                }
            }
        }
    }

    //Todo: PlacedWord summary and sub-sumary
    //public class PlacedWord
    //{
    //    public readonly Word Word;
    //    public readonly Word.Placement Placement;

    //    public PlacedWord(Word.Placement placement, Word word)
    //    {
    //        Placement = placement;
    //        Word = word;
    //    }
    //}
}