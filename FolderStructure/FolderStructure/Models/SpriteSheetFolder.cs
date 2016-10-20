using FolderStructure.Components;

namespace FolderStructure.Models
{
    public class SpriteSheetFolder : NotificationCollectionWrapper<ISpriteSheetNode>, ISpriteSheetNode
    {
        private string _name;

        public SpriteSheetFolder(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }
    }
}