using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crossword.Builder
{
    public class WordsDictionary
    {
        private const String _dictionaryName = @"ressources\Dictionary.txt";
        private readonly String _pathToDictionary = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dictionaryName);

        public readonly IEnumerable<Word> Words;

        public WordsDictionary()
        {
            Words = ReadDictionaryFile()
                .ConvertAll(l => new Word(l))
                .OrderBy(w => w.Length);
        }

        private List<String> ReadDictionaryFile()
        {
#if (DEBUG)
            return new List<String>(File.ReadAllLines(@"D:\Github\Crossword\Crossword.Builder\ressources\Dictionary.txt"));
#endif

            return new List<String>(File.ReadAllLines(_pathToDictionary)); //TODO: WordsDictionary test path.
        }

        public IEnumerable<Word> GetWords(Restriction restriction)
        {
            IEnumerable<Word> words = Words.Where(word =>
                word.Length >= restriction.MimimumLenght
                && (word.Length < restriction.MaximumLenght || restriction.MaximumLenght == 0));

            if (restriction.RestrictedCharacters == null)
                return words;

            words = words.Where(word => 
                !restriction.RestrictedCharacters.Any(c => c.Index == word.Length + 1 ||
                (word.Length > c.Index && word[c.Index] != c.Value)));

            return words;
        }

    }
}
