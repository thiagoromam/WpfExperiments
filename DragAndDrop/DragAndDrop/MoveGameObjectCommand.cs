using System;
using System.Collections.Generic;
using System.Windows.Input;
using DragAndDrop.Components;

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
            var args = (DropEventArgs)parameter;

            return args.Data is GameObject &&
                   (args.DropType == DropType.Normal || args.Target is GameObject);
        }

        public void Execute(object parameter)
        {
            var args = (DropEventArgs)parameter;
            var gameObject = (GameObject)args.Data;
            var target = args.Target as GameObject;
            
            switch (args.DropType)
            {
                case DropType.Top: 
                    ChangeIndex(gameObject, target, 0);
                    break;
                case DropType.Normal:
                    MoveTo(gameObject, target);
                    break;
                case DropType.Bottom:
                    ChangeIndex(gameObject, target, 1);
                    break;
            }
        }
        
        private void MoveTo(GameObject gameObject, GameObject target)
        {
            (gameObject.Parent?.Children ?? SceneData.GameObjects).Remove(gameObject);
            gameObject.Parent = target;
            (target?.Children ?? SceneData.GameObjects).Add(gameObject);
        }
        private void ChangeIndex(GameObject gameObject, GameObject target, int increment)
        {
            //var collection = target?.Parent.Children ?? SceneData.GameObjects;

            //if (!collection.Contains(gameObject))
            //{
            //    MoveTo(gameObject, target?.Parent);
            //    ChangeIndex(gameObject, target, increment);
            //    return;
            //}

            //var oldIndex = Math.Max(collection.IndexOf(gameObject), 0);
            //var newIndex = Math.Min(collection.IndexOf(target) + increment, collection.Count);
            
            //collection.Move(oldIndex, newIndex);
        }
    }
}