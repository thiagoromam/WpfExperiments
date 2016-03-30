using FolderStructure.Components;
using FolderStructure.Models;

namespace FolderStructure
{
    public static class Data
    {
        public static readonly IObservableCollection<ISpriteSheetNode> SpriteSheets;

        static Data()
        {
            SpriteSheets = new ExtendedObservableCollection<ISpriteSheetNode>();
        }
    }
}