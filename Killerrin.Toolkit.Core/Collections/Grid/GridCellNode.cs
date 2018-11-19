using System;
using System.Collections;
using System.Drawing;

namespace Killerrin.Toolkit.Core.Collections.Grid
{
    public class GridCellNode<T>
    {
        public readonly Guid ID;
        public readonly Point GridPosition;
        public T Data;

        public GridCellNode(Guid id, Point gridPosition)
        {
            ID = id;
            GridPosition = gridPosition;

            Data = default(T);
        }

        public GridCellNode(Guid id, Point gridPosition, T data)
        {
            ID = id;
            GridPosition = gridPosition;

            Data = data;
        }
        public override string ToString() { return "" + ID; }
    }
}