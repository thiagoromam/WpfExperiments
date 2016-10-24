using System;

namespace DragAndDrop.Components
{
    public interface IDroppable
    {
        Type DataType { get; }

        void Drop(object data, DropType dropType);
    }
}