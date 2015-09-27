using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Builder
{
    class WordsDictionary
    {
        public readonly List<Word> Words;

#if DEBUG
        public Word GetWord_Debug()
        {
            return null
        }
#endif
    }
}
