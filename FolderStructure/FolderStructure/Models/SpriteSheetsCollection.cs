using System;
using System.Linq.Expressions;
using FolderStructure.Components;
using FolderStructure.Structures;

namespace FolderStructure.Models
{
    public class SpriteSheetsCollection : FolderStructureCollection<ISpriteSheetNode, SpriteSheetFolder, SpriteSheet>
    {
        private readonly IObservableCollection<ISpriteSheetNode> _source;

        public SpriteSheetsCollection(IObservableCollection<ISpriteSheetNode> source)
        {
            _source = source;
        }

        protected override IObservableCollection<ISpriteSheetNode> GetSource()
        {
            return _source;
        }
        protected override Expression<Func<ISpriteSheetNode, string>> GetNameExpression()
        {
            return n => n.Name;
        }
        protected override IObservableCollection<ISpriteSheetNode> GetChildrenFromFolder(SpriteSheetFolder folder)
        {
            return folder;
        }
    }
}