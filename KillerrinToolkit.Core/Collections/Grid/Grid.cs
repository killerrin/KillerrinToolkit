using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace KillerrinToolkit.Core.Collections.Grid
{
    public class Grid<T> : IEnumerable<GridCellNode<T>>
    {
        private List<List<GridCellNode<T>>> m_grid;

        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public Grid(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;

            // Create the grid using columns/rows;
            m_grid = new List<List<GridCellNode<T>>>(Columns);

            // Then create the Columns
            for (int i = 0; i < Columns; i++) {
                m_grid.Add(new List<GridCellNode<T>>(Rows));
            }

            // Finally, populate them with an ID'd GridCell
            int x = 0;
            foreach (var i in m_grid)
            {
                for (int y = 0; y < Rows; y++)
                {
                    i.Add(new GridCellNode<T>(Guid.NewGuid(), new Point(x, y)));
                }
                x++;
            }
        }

        public bool InGrid(Point point)
        {
            return InGrid(point.X, point.Y);
        }
        public bool InGrid(int column, int row)
        {
            return (column >= 0) &&
                   (column < Columns) &&
                   (row >= 0) &&
                   (row < Rows);
        }



        #region Enumerations
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        public IEnumerator<GridCellNode<T>> GetEnumerator()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++ )
                {
                    yield return m_grid[x][y];
                }
            }
        }

        public GridCellNode<T> GetGridCellNode(Point gridPos) { return m_grid[gridPos.X][gridPos.Y]; }
        public GridCellNode<T> GetGridCellNode(int column, int row) { return m_grid[column][row]; }

        public Guid GetIDFromColumnRow(Point gridPos) { return m_grid[gridPos.X][gridPos.Y].ID; }
        public Guid GetIDFromColumnRow(int column, int row) { return m_grid[column][row].ID; }

        public Point GetColumnRowFromID(Guid id)
        {
            int x, y; 
            GetColumnRowFromID(id, out x, out y);
            return new Point(x, y);
        }
        public void GetColumnRowFromID(Guid id, out int X, out int Y)
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    if (m_grid[x][y].ID == id)
                    {
                        X = x;
                        Y = y;
                        return;
                    }
                }
            }

            throw new IndexOutOfRangeException();
        }

        public T this [Guid gridID]
        {
            get {
                Point p = GetColumnRowFromID(gridID);
                return this[p.X, p.Y];
            }
            set
            {
                Point p = GetColumnRowFromID(gridID);
                this[p.X, p.Y] = value;
            }
        }

        public T this[Point gridPos] { get { return this[gridPos.X, gridPos.Y]; } set { this[gridPos.X, gridPos.Y] = value; } }
        public T this[int column, int row]
        {
            get { return m_grid[column][row].Data; }
            set {
                var data = m_grid[column][row];
                data.Data = value;
            }
        }
        #endregion
    }
}
