using System;
using System.Collections.Generic;
using System.Linq;

namespace Crossword.Builder
{
    public class BidimensionalArray<T>
    {
        #region Properties

        private volatile T[,] _array;

        /// <summary>Gets the 32-bit integer that represents the number of columns in the specified dimensions of the <see cref="BidimensionalArray{T}"/>.</summary>
        public readonly int ColumnCount;
        /// <summary>Gets the 32-bit integer that represents the number of rows in the specified dimensions of the <see cref="BidimensionalArray{T}"/>.</summary>
        public readonly int RowCount;

        /// <summary>Gets the 32-bit integer that represents the total number of elements in the <see cref="BidimensionalArray{T}"/>.</summary>
        public int Count => _array.Length;
        #endregion

        #region Initialization
        /// <summary>
        /// Initialize a new instance of the <see cref="BidimensionalArray{T}"/> class from an existing two dimensional array.
        /// </summary>
        /// <param name="array"></param>
        public BidimensionalArray(T[,] array)
        {
            _array = array;

            //Dimensions
            ColumnCount = array.GetLength(0);
            RowCount = array.GetLength(1);
        }
        /// <summary>
        /// Initialize a new instance of the <see cref="BidimensionalArray{T}"/> class from specified dimensions.
        /// </summary>
        /// <param name="columnCount">Number of columns of the <see cref="Array"/>.</param>
        /// <param name="rowCount">Number of rows of the <see cref="Array"/>.</param>
        public BidimensionalArray(int columnCount, int rowCount)
        {
            if (columnCount < 1 || rowCount < 1)
                throw new Exception("Cannot initialize BidimensionalArray with negative or null dimensions"); //TOdo: TwoDimensionArray.new trows execption

            _array = new T[columnCount, rowCount];

            //Dimensions
            ColumnCount = columnCount;
            RowCount = rowCount;
        }
        #endregion

        public Boolean IsInGrid(int columnIndex, int rowIndex)
            => (columnIndex >= 0 && columnIndex < ColumnCount) //Column
            && (rowIndex >= 0 && rowIndex < RowCount); //Row

        #region Insert
        public void Insert(IList<T> elements, int columnIndex, int rowIndex, bool isHorizontal = true)
        {
            if (!IsInGrid(columnIndex, rowIndex))
                throw new IndexOutOfRangeException("Out of the grid, during the insert"); //Todo: TwoDimensionalArray.Insert throw execption

            if (isHorizontal)
            {
                //Insert Honrizontally
                if (!IsInGrid(columnIndex + elements.Count - 1, rowIndex))
                    throw new IndexOutOfRangeException("Out of the grid, during the insert"); //Todo: TwoDimensionalArray.Insert throw execption

                for (int i = 0; i < elements.Count; i++)
                    _array[columnIndex + i, rowIndex] = elements[i];
            }
            else
            {
                //Insert Vertically
                if (!IsInGrid(columnIndex, rowIndex + elements.Count - 1))
                    throw new IndexOutOfRangeException("Out of the grid, during the insert"); //Todo: TwoDimensionalArray.Insert throw execption

                for (int i = 0; i < elements.Count; i++)
                    _array[columnIndex, rowIndex + i] = elements[i];
            }
        }

        public void Insert(T element, int columnIndex, int rowIndex)
        {
            if (!IsInGrid(columnIndex, rowIndex))
                throw new IndexOutOfRangeException("Out of the grid, during the Insert"); //Todo: TwoDimensionalArray.Insert throw execption

            _array[columnIndex, rowIndex] = element;
        }
        #endregion

        #region Get
        public T this[int column, int row]
            => _array[column, row];

        public T GetElement(int column, int row)
            => _array[column, row];

        public IEnumerable<T> GetColumn(int column, int startIndex = 0)
        {
            if (column >= ColumnCount || column < 0)
                throw new IndexOutOfRangeException("Column out of the grid");
            if (startIndex >= ColumnCount || startIndex < 0)
                throw new IndexOutOfRangeException("Start index out of the grid");

            for (int index = startIndex; index < ColumnCount; index++)
                yield return this[column, index];       
        }

        public IEnumerable<T> GetRow(int row, int startIndex = 0)
        {
            if (row >= RowCount || row < 0)
                throw new IndexOutOfRangeException("Row out of the grid");
            if (startIndex >= ColumnCount || startIndex < 0)
                throw new IndexOutOfRangeException("Start index out of the grid");

            for (int index = startIndex; index < RowCount; index++)
                yield return this[index, row];
        } 

        #endregion

        public bool Contains(T element)
        {
            foreach (T arrayElement in _array)
                if (element.Equals(arrayElement))
                    return true;
            
            return false;
        }

        public bool ContainsDefault(int columnIndex, int rowIndex)
            => this[columnIndex, rowIndex].Equals(default(T));

        public void Clear() 
            => Array.Clear(this._array, 0, this._array.Length);
        
        public BidimensionalArray<T> Clone() 
            => new BidimensionalArray<T>(this._array); 
    }
}