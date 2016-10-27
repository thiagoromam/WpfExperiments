using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace DragAndDrop
{
    public static class Helpers
    {
        // For test
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

        // Behavior
        public static T GetBehavior<T>(this DependencyObject element) where T : Behavior
        {
            var behaviors = Interaction.GetBehaviors(element);
            return behaviors.OfType<T>().SingleOrDefault();
        }
        public static T FindBehaviorInChildren<T>(this DependencyObject parent) where T : Behavior
        {
            foreach (var child in parent.GetChildren())
            {
                var behavior = child.GetBehavior<T>() ?? child.FindBehaviorInChildren<T>();
                if (behavior != null)
                    return behavior;
            }

            return null;
        }

        // DependencyObject
        public static T ParentsUntil<T>(this DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            return parentObject as T ?? ParentsUntil<T>(parentObject);
        }
        public static T FindChild<T>(this DependencyObject parent, string childName) where T : FrameworkElement
        {
            foreach (var child in parent.GetChildren())
            {
                if ((child as T)?.Name == childName)
                    return (T)child;

                var childOfChild = FindChild<T>(child, childName);
                if (childOfChild != null)
                    return childOfChild;
            }

            return null;
        }
        public static IEnumerable<DependencyObject> GetChildren(this DependencyObject element)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
                yield return VisualTreeHelper.GetChild(element, i);
        }

        // Application
        public static T FindResource<T>(string name)
        {
            return (T)Application.Current.FindResource(name);
        }
    }
}