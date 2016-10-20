using System;

namespace DragAndDrop.Components
{
    public interface IDraggable
    {
        Type DataType { get; }

        void Drag();
    }
}