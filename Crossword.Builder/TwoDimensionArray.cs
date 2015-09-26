using System;

namespace Crossword.Builder
{
    public class TwoDimensionArray<T>
    {
        #region Properties
        private T[,] _array;

        public readonly int ColumnCount;
        public readonly int RowCount;
        #endregion

        #region Initialization
        public TwoDimensionArray(T[,] array)
        {
            _array = array;

            //Dimensions
            ColumnCount = array.GetLength(0);
            RowCount = array.GetLength(1);
        }

        public TwoDimensionArray(int columnCount, int rowCount)
        {
            if (columnCount < 1 || rowCount < 1)
                throw new Exception("Cannot create initialize twoDimensionArray with negative or null dimensions"); //TOdo: TwoDimensionArray.new trows execption

            _array = new T[columnCount, rowCount];

            //Dimensions
            ColumnCount = columnCount;
            RowCount = rowCount;
        }
        #endregion

        public T this[int column, int row] => _array[column, row];

        public Boolean IsInGrid(int columnIndex, int rowIndex) =>
            (columnIndex >= 0 && columnIndex < ColumnCount) //Column
            && (rowIndex >= 0 && rowIndex < RowCount); //Row

        //Todo: test Insert
        public void Insert(T[] elements, int columnIndex, int rowIndex, bool isHorizontal = true)
        {
            if (!IsInGrid(columnIndex, rowIndex))
                throw new IndexOutOfRangeException("Outside of the grid, during the insert"); //Todo: TwoDimensionalArray.Insert throw execption

            if (isHorizontal)
            {
                //Insert Honrizontally

                if (!IsInGrid(columnIndex + elements.Length - 1, rowIndex))
                    throw new IndexOutOfRangeException("Outside of the grid, during the insert"); //Todo: TwoDimensionalArray.Insert throw execption

                for (int i = 0; i < elements.Length; i++)
                    _array[columnIndex + i, rowIndex] = elements[i];
            }
            else
            {
                //Insert Vertically

                if (!IsInGrid(columnIndex, rowIndex + elements.Length - 1))
                    throw new IndexOutOfRangeException("Outside of the grid, during the insert"); //Todo: TwoDimensionalArray.Insert throw execption

                for (int i = 0; i < elements.Length; i++)
                    _array[columnIndex, rowIndex + i] = elements[i];
            }
        }

        public void Insert(T element, int columnIndex, int rowIndex)
        {
            if (!IsInGrid(columnIndex, rowIndex))
                throw new IndexOutOfRangeException("Outside of the grid, during the Insert"); //Todo: TwoDimensionalArray.Insert throw execption

            _array[columnIndex, rowIndex] = element;
        }

        public void Clear() => Array.Clear(this._array, 0, this._array.Length);
        
        public TwoDimensionArray<T> Clone() => new TwoDimensionArray<T>(this._array); 
    }
}