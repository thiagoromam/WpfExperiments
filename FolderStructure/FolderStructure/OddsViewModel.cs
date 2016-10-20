using System;
using FolderStructure.Components;
using FolderStructure.Models;

namespace FolderStructure
{
    public class OddsViewModel : NotificationObject, IDisposable
    {
        public OddsViewModel()
        {
            Nodes = new SpriteSheetsCollection(Data.SpriteSheets);
        }

        public SpriteSheetsCollection Nodes { get; }

        public void Dispose()
        {
            Nodes.Dispose();
        }
    }
}