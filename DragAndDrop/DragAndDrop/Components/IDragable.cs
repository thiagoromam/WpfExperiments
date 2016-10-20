using System;

namespace DragAndDrop.Components
{
    public interface IDragable
    {
        Type DataType { get; }

        void Drag();
    }
}