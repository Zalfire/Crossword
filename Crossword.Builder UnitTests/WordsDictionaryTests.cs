using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crossword.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Builder.Tests
{
    [TestClass()]
    public class WordsDictionaryTests
    {
        [TestMethod()]
        public void WordsDictionaryTest()
        {
            var dic = new WordsDictionary();
            Assert.IsNotNull(dic);
        }

        [TestMethod()]
        public void GetWordsTest()
        {
            var dic = new WordsDictionary();

            var words = dic.GetWords(new Restriction(1, 0));
            Assert.IsTrue(words.Count() == dic.Words.Count());

            words = dic.GetWords(new Restriction(1, 2));
            Assert.IsFalse(words.Any(w => w.Length >= 2));

            words = dic.GetWords(new Restriction(2, 0));
            Assert.IsFalse(words.Any(w => w.Length < 2));

            words = dic.GetWords(new Restriction(2, 2));
            Assert.IsFalse(words.Any(w => w.Length != 2));

            words = dic.GetWords(new Restriction(1, 0, 
                new RestrictedCharacter[] {
                new RestrictedCharacter('a', 0) }));
            Assert.IsFalse(words.Any(w => w[0] != 'a'));

            words = dic.GetWords(new Restriction(1, 0, 
                new RestrictedCharacter[] {
                   new RestrictedCharacter('a', 0),
                    new RestrictedCharacter('b', 1)}));
            Assert.IsFalse(words.Any(w => w[0] != 'a' && w[1] != 'b'));
        }
    }
}