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

        /// <summary>Gets the number of characters in <see cref="Word.Value"/>.</summary>
        public int Length => Value.Length;

        /// <summary>Get the <see cref="Char"/> associated with the specified index in <see cref="Word.Value"/>.</summary>
        public Char this[int index] => Value[index];
        #endregion

        #region Initialization
        /// <summary>
        /// Initialize a new instance of the <see cref="Word"/> class.
        /// </summary>
        /// <param name="value">The <see cref="Word.Value"/> of the <see cref="Word"/> class.</param>
        public Word(String value)
        {
            Id = Guid.NewGuid().ToString();

            Value = value.Trim().ToLower(); //Trims and Lower case value
        }
        #endregion

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
            /// <summary><see cref="Guid"/> identification as <see cref="String"/>.</summary>
            public readonly String Id;

            public readonly int Row;
            
            public readonly int Column;

            /// <summary>Gets if the <see cref="Word"/> should be represented horizontally</summary>
            public readonly bool IsHorizontal;
            #endregion

            public Placement(int row, int column, bool isHorizontal)
            {
                Id = Guid.NewGuid().ToString();

                Row = row;
                Column = column;
                IsHorizontal = isHorizontal;
            }
            #region Equality members
            protected bool Equals(Placement other)
            {
                return Row == other.Row 
                    && Column == other.Column
                    && IsHorizontal == other.IsHorizontal;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Row;
                    hashCode = (hashCode*397) ^ Column;
                    hashCode = (hashCode*397) ^ IsHorizontal.GetHashCode();
                    return hashCode;
                }
            }
            #endregion
        }
    }
}