using System;
using System.Collections.ObjectModel;
using DragAndDrop.Components;

namespace DragAndDrop
{
    public class ViewModel : IDropable
    {
        public ViewModel()
        {
            GameObjects = SceneData.GameObjects;
        }

        public Type DataType => typeof (GameObject);
        public ObservableCollection<GameObject> GameObjects { get; }

        public void Drop(object data, int index = -1)
        {
            GameObjects.Add((GameObject)data);
        }
    }
}