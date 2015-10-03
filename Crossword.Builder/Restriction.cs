using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Crossword.Builder
{
    /// <summary>
    /// Represent a restriction, the legnth a word should have and which characters are predetermined.
    /// </summary>
    public class Restriction
    {
        #region Propreties
        /// <summary>Collection of the predetermined characters and their index.</summary>
        public readonly IEnumerable<RestrictedCharacter> RestrictedCharacters;

        /// <summary>The included  minimum lenght a <see cref="Word"/> can have.</summary>
        public readonly int MimimumLenght;

        /// <summary>The included maximum lenght a <see cref="Word"/> can have.</summary>
        public readonly int MaximumLenght;
        #endregion

        /// <summary>
        /// Initialize a new instance of the Restriction class.
        /// </summary>
        /// <param name="mimimumLenght">The minimum lenght a <see cref="Word"/> can have.</param>
        /// <param name="maximumLenght">The included maximum lenght a <see cref="Word"/> can have.</param>
        /// <param name="characters">Collection of <see cref="RestrictedCharacter"/.></param>
        public Restriction(int mimimumLenght, int maximumLenght, IEnumerable<RestrictedCharacter> restrictedCharacters = null)
        {
            if (maximumLenght < mimimumLenght && maximumLenght != 0)
                throw new Exception("Maximum cannot be smaller than minimum.");
            if (maximumLenght < 0)
                throw new Exception("Maximum cannot be smaller than 0");
            if (mimimumLenght < 1)
                throw new Exception("Minimum cannot be smaller than 1");
            if (restrictedCharacters != null) {
                if(restrictedCharacters.Select(w => w.Index).Distinct().Count() != restrictedCharacters.Count())
                    throw new Exception("Index needs to be unique.");
                if (maximumLenght != 0 && restrictedCharacters.Any(c => c.Index >= maximumLenght))
                    throw new Exception("Cannot have characters outside of range.");
            }

            MimimumLenght = mimimumLenght;
            MaximumLenght = maximumLenght;
            RestrictedCharacters = restrictedCharacters;
        }
    }

    // Todo: RestrictedCharacter summary and sub summary
    public class RestrictedCharacter
    {
        #region Propreties
        public readonly int Index;

        public readonly Char Value;
        #endregion

        public RestrictedCharacter(Char value, int index)
        {
            Index = index;
            Value = value;
        }
    }
}
