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
    public class CrosswordGridTests
    {
        [TestMethod()]
        public void CrosswordGridTest()
        {
            Assert.IsNotNull(new CrosswordGrid(1, 1, ' '));
        }

        [TestMethod()]
        public void BuildGridTest()
        {
            var grid = new CrosswordGrid(2, 2, ' ');
            grid.BuildGrid();
        }
    }
}