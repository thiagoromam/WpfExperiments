using FolderStructure.Components;

namespace FolderStructure.Models
{
    public class SpriteSheet : NotificationObject, ISpriteSheetNode
    {
        private string _name;

        public SpriteSheet(string name)
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