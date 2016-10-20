using System;
using System.Collections.ObjectModel;
using DragAndDrop.Components;

namespace DragAndDrop
{
    public class GameObject : IDraggable, IDroppable
    {
        public GameObject(string name)
        {
            Name = name;
            Children = new ObservableCollection<GameObject>();
        }

        public string Name { get; }
        public GameObject Parent { get; set; }
        public Type DataType => typeof(GameObject);
        public ObservableCollection<GameObject> Children { get; }

        public void Drag()
        {
            (Parent?.Children ?? SceneData.GameObjects).Remove(this);
            Parent = null;
        }
        public void Drop(object data, int index = -1)
        {
            var gameObject = (GameObject)data;

            Children.Add(gameObject);
            gameObject.Parent = this;
        }
    }
}