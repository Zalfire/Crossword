using System;

namespace Crossword.Builder
{
    class TwoDimensionArray<T>
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
            _array = new T[columnCount, rowCount];

            //Dimensions
            ColumnCount = columnCount;
            RowCount = rowCount;
        }
        #endregion

        public T this[int column, int row] => _array[column, row];

        public Boolean IsInGrid(int column, int row) =>
            (column >= 0 && column < ColumnCount) //Column
            && (row >= 0 && row < RowCount); //Row

        //Todo: test Insert
        public void Insert(T[] elements, int column, int row, bool isHorizontal = true)
        {
            if (!IsInGrid(column, row))
                throw new IndexOutOfRangeException("Outside of the grid, during the insert"); //Todo: TwoDimensionalArray.Insert throw execption

            if (isHorizontal)
            {
                //Insert Honrizontally

                if (!IsInGrid(column + elements.Length - 1, row))
                    throw new IndexOutOfRangeException("Outside of the grid, during the insert"); //Todo: TwoDimensionalArray.Insert throw execption

                for (int i = 0; i < elements.Length; i++)
                    _array[column + i, row] = elements[i];
            }
            else
            {
                //Insert Vertically

                if (!IsInGrid(column, row + elements.Length - 1))
                    throw new IndexOutOfRangeException("Outside of the grid, during the insert"); //Todo: TwoDimensionalArray.Insert throw execption

                for (int i = 0; i < elements.Length; i++)
                    _array[column, row + i] = elements[i];
            }
        }

        public void Insert(T element, int column, int row)
        {
            if (!IsInGrid(column, row))
                throw new IndexOutOfRangeException("Outside of the grid, during the Insert"); //Todo: TwoDimensionalArray.Insert throw execption

            _array[column, row] = element;
        }

        public void Clear() => Array.Clear(this._array, 0, this._array.Length);
        
        public TwoDimensionArray<T> Clone() => new TwoDimensionArray<T>(this._array); 
    }
}