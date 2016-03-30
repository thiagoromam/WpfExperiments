using FolderStructure.Models;

namespace FolderStructure.Commands
{
    public class ChangeToNextNameCommand : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is SpriteSheet;
        }

        public override void Execute(object parameter)
        {
            ((SpriteSheet)parameter).Name = AddItemCommand.NextName();
        }
    }
}