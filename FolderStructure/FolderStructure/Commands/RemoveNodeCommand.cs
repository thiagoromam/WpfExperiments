using FolderStructure.Components;
using FolderStructure.Models;

namespace FolderStructure.Commands
{
    public class RemoveNodeCommand : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var array = parameter as object[];
            if (array == null)
                return false;

            return array.Length == 2 &&
                   array[0] is IObservableCollection<ISpriteSheetNode> &&
                   array[1] is ISpriteSheetNode;
        }

        public override void Execute(object parameter)
        {
            var array = (object[])parameter;
            var collection = (IObservableCollection<ISpriteSheetNode>)array[0];
            var item = (ISpriteSheetNode)array[1];

            collection.Remove(item);
        }
    }
}