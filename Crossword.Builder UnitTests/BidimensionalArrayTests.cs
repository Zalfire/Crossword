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
    public class TwoDimensionArrayTests
    {
        [TestMethod()]
        public void TwoDimensionArrayTest()
        {
            Assert.IsNotNull(new BidimensionalArray<bool>(1, 1));
        }

        [TestMethod()]
        public void TwoDimensionArrayTest1()
        {
            Assert.IsNotNull(new[] {true});
        }

        [TestMethod()]
        public void IsInGridTest()
        {
            const int column = 2;
            const int row = 3;

            BidimensionalArray<bool> testArray = new BidimensionalArray<bool>(column, row);
            Assert.IsTrue(
                testArray.IsInGrid(0,0)
                && testArray.IsInGrid(column - 1, row - 1)
                && !testArray.IsInGrid(column, 0)
                && !testArray.IsInGrid(0, row)
                && !testArray.IsInGrid(-1, 0)
                && !testArray.IsInGrid(0, -1));
        }

        [TestMethod()]
        public void InsertTest()
        {
            const int column = 5;
            const int row = 6;

            BidimensionalArray<int> testArray = new BidimensionalArray<int>(column, row);

            testArray.Insert(new []{ 1, 2, 3, 4, 5}, 0, 0, true);
            testArray.Insert(new []{ 6, 7, 8, 9, 10, 11}, 0, 0, false);
            testArray.Insert(new []{12, 13, 14, 15, 16, 17}, column - 1, 0, false);
            testArray.Insert(new []{ 18, 19, 20, 21, 22}, 0, row - 1, true);

            for (int i = 0; i < column; i++) {
                Assert.IsTrue(testArray[i, 0] == new [] {6, 2, 3, 4, 12}[i]);
                Assert.IsTrue(testArray[0, i] == new[] {6, 7, 8, 9, 10, 18}[i]);
                Assert.IsTrue(testArray[column - 1, i] == new[] {12, 13, 14, 15, 16, 22}[i]);
                Assert.IsTrue(testArray[i, row - 1] == new [] {18, 19, 20, 21, 22}[i]);
            }
        }

        [TestMethod()]
        public void InsertTest1()
        {
            const int column = 2;
            const int row = 3;

            BidimensionalArray<int> testArray = new BidimensionalArray<int>(column, row);

            testArray.Insert(1, 0, 0);
            Assert.IsTrue(testArray[0,0] == 1);

            testArray.Insert(2, column - 1, row - 1);
            Assert.IsTrue(testArray[column - 1, row -1] == 2);
        }

        [TestMethod()]
        public void ClearTest()
        {
            const int column = 5;
            const int row = 6;

            BidimensionalArray<int> testArray = new BidimensionalArray<int>(column, row);

            testArray.Insert(Enumerable.Range(0, 5).ToArray(), 0, 0, true);
            testArray.Insert(Enumerable.Range(5, 6).ToList(), 0, 0, false);
            testArray.Insert(Enumerable.Range(12, 6).ToArray(), column - 1, 0, false);
            testArray.Insert(Enumerable.Range(18, 5).ToArray(), 0, row - 1, true);

            testArray.Clear();

            Assert.IsTrue(
                testArray != null
                && testArray[0,0] == default(int)
                && testArray[column - 1, row - 1] == default(int));
        }

        [TestMethod()]
        public void CloneTest()
        {            
            Assert.Inconclusive();
        }
    }
}