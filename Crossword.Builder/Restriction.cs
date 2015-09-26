using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Builder
{
    /// <summary>
    /// Represent a restriction, the legnth a word should have and which characters are predetermined.
    /// </summary>
    class Restriction
    {
        #region Propreties
        /// <summary>Collection of the predetermined characters and their index.</summary>
        public readonly RestrictedCharactersDictionary Characters;

        /// <summary>The minimum lenght a <see cref="Word"/> can have.</summary>
        public readonly int MimimumLenght;

        /// <summary>The maximum lenght a <see cref="Word"/> can have.</summary>
        public readonly int MaximumLenght;
        #endregion

        /// <summary>
        /// Initialize a new instance of the Restriction class.
        /// </summary>
        /// <param name="mimimumLenght">The minimum lenght a <see cref="Word"/> can have.</param>
        /// <param name="maximumLenght">The maximum lenght a <see cref="Word"/> can have.</param>
        /// <param name="characters">Collection of the predetermined characters and their index.</param>
        public Restriction(int mimimumLenght, int maximumLenght, RestrictedCharactersDictionary characters = null)
        {
            MimimumLenght = mimimumLenght;
            MaximumLenght = maximumLenght;
            Characters = characters;
        }

        // Todo: RestrictedCharactersDictionary summary and sub summary
        internal class RestrictedCharactersDictionary
        {

            private readonly Dictionary<int, Char> _restrictedCharacters;

            /// <summary>
            /// Get the <see cref="Char"/> associated with the specified index or <c>\0</c> if there is no char associated.
            /// </summary>
            /// <param name="index">the index of the char to get.</param>
            /// <returns>returns '\0' if the index doesn't exist.</returns>
            public char this[int index] => _restrictedCharacters.ContainsKey(index)
                ? _restrictedCharacters[index]
                : '\0';

            /// <summary>
            /// Initialize a new instance of the <see cref="RestrictedCharactersDictionary"/> class from a dictionary of characters and their indexes>
            /// </summary>
            /// <param name="restrictedCharacters"></param>
            public RestrictedCharactersDictionary(Dictionary<int, Char> restrictedCharacters)
            {
                _restrictedCharacters = restrictedCharacters;
            }
        }
    }
}
