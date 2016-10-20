using System;
using System.Collections.ObjectModel;

namespace DragAndDrop
{
    public static class Helpers
    {
        public static GameObject Add(this ObservableCollection<GameObject> collection, string name, Action<GameObject> after = null)
        {
            var g = new GameObject(name);
            after?.Invoke(g);

            collection.Add(g);

            return g;
        }
        public static GameObject Add(this GameObject gameObject, string name, Action<GameObject> after = null)
        {
            var g = gameObject.Children.Add(name, after);
            g.Parent = gameObject;

            return g;
        }
    }
}