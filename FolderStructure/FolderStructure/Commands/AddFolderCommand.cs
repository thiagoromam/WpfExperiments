using FolderStructure.Components;
using FolderStructure.Models;

namespace FolderStructure.Commands
{
    public class AddFolderCommand : BaseCommand
    {
        private static int _numer = 1;

        public override bool CanExecute(object parameter)
        {
            return parameter is IObservableCollection<ISpriteSheetNode>;
        }

        public override void Execute(object parameter)
        {
            var collection = (IObservableCollection<ISpriteSheetNode>)parameter;
            collection.Add(new SpriteSheetFolder($"Folder {_numer++}"));
        }
    }
}