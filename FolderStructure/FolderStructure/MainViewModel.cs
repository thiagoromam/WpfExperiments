using System.Windows.Input;
using FolderStructure.Commands;
using FolderStructure.Components;
using FolderStructure.Models;

namespace FolderStructure
{
    public class MainViewModel : NotificationObject
    {
        public MainViewModel()
        {
            SpriteSheets = Data.SpriteSheets;
            Nodes = new SpriteSheetsCollection(Data.SpriteSheets);
            OpenOddsWindowCommand = new DelegateCommand(OpenOddsWindow);
        }

        public IObservableCollection<ISpriteSheetNode> SpriteSheets { get; }
        public SpriteSheetsCollection Nodes { get; }
        public ICommand OpenOddsWindowCommand { get; }

        private void OpenOddsWindow(object parameter)
        {
            new OddsWindow().Show();
        }
    }
}