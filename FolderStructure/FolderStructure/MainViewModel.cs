using FolderStructure.Components;
using FolderStructure.Models;

namespace FolderStructure
{
    public class MainViewModel : NotificationObject
    {
        public MainViewModel()
        {
            SpriteSheets = new ExtendedObservableCollection<ISpriteSheetNode>();
            Nodes = new SpriteSheetsCollection(SpriteSheets);
        }

        public ExtendedObservableCollection<ISpriteSheetNode> SpriteSheets { get; }
        public SpriteSheetsCollection Nodes { get; }
    }
}