using System;
using System.Windows.Input;

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
            GameObject gameObject, target;
            GetValues(parameter, out gameObject, out target);

            return gameObject != null ;
        }
        public void Execute(object parameter)
        {
            GameObject gameObject, target;
            GetValues(parameter, out gameObject, out target);

            (gameObject.Parent?.Children ?? SceneData.GameObjects).Remove(gameObject);
            gameObject.Parent = target;
            (target?.Children ?? SceneData.GameObjects).Add(gameObject);
        }

        private static void GetValues(object parameter, out GameObject gameObject, out GameObject target)
        {
            var values = (object[])parameter;

            gameObject = values[0] as GameObject;
            target = values[1] as GameObject;
        }
    }
}