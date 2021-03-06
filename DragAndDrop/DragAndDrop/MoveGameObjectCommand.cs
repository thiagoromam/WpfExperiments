﻿using System;
using System.Linq;
using System.Windows.Input;
using DragAndDrop.Unity;

namespace DragAndDrop
{
    public class MoveGameObjectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        public bool CanExecute(object parameter)
        {
            var args = (DropEventArgs) parameter;

            return IsDataValid(args) && IsHierarchyValid(args);
        }
        public void Execute(object parameter)
        {
            var args = (DropEventArgs)parameter;
            var gameObject = (GameObject)args.Data;
            var target = args.Target as GameObject;

            if (args.DropType == DropType.InsideOnTop)
            {
                target = target.Children.First();
                args.DropType = DropType.Above;
            }

            switch (args.DropType)
            {
                case DropType.Above: 
                    ChangeIndex(gameObject, target);
                    break;
                case DropType.Inside:
                    MoveTo(gameObject, target);
                    break;
                case DropType.Bellow:
                    ChangeIndex(gameObject, target, 1);
                    break;
            }
        }

        private static bool IsDataValid(DropEventArgs args)
        {
            return args.Data is GameObject;
        }
        private static bool IsHierarchyValid(DropEventArgs args)
        {
            var parent = args.Target as GameObject;
            while (parent != null && parent != args.Data)
                parent = parent.Parent;

            return parent == null;
        }

        private void MoveTo(GameObject gameObject, GameObject target)
        {
            (gameObject.Parent?.Children ?? SceneData.GameObjects).Remove(gameObject);
            gameObject.Parent = target;
            (target?.Children ?? SceneData.GameObjects).Add(gameObject);
        }
        private void ChangeIndex(GameObject gameObject, GameObject target, int increment = 0)
        {
            var collection = target?.Parent?.Children ?? SceneData.GameObjects;

            if (!collection.Contains(gameObject))
            {
                MoveTo(gameObject, target?.Parent);
                ChangeIndex(gameObject, target, increment);
                return;
            }
            
            var oldIndex = Math.Max(collection.IndexOf(gameObject), 0);
            var newIndex = Math.Min(collection.IndexOf(target) + increment, collection.Count);

            if (newIndex > oldIndex)
                newIndex--;

            collection.Move(oldIndex, newIndex);
        }
    }
}