using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DragAndDrop.Components;

namespace DragAndDrop
{
    public class ViewModel : IDroppable
    {
        public ViewModel()
        {
            GameObjects = SceneData.GameObjects;
            MoveGameObjectCommand = new MoveGameObjectCommand();
        }

        public Type DataType => typeof(GameObject);
        public ObservableCollection<GameObject> GameObjects { get; }
        public ICommand MoveGameObjectCommand { get; }

        public void Drop(object data, DropType dropType)
        {
            GameObjects.Add((GameObject)data);
        }
    }
}