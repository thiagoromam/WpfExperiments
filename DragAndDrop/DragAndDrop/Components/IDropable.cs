using System;

namespace DragAndDrop.Components
{
    public interface IDropable
    {
        Type DataType { get; }

        void Drop(object data, int index = -1);
    }
}