using System.Collections.ObjectModel;

namespace DragAndDrop
{
    public static class SceneData
    {
        public static readonly ObservableCollection<GameObject> GameObjects;

        static SceneData()
        {
            GameObjects = new ObservableCollection<GameObject>
            {
                "Camera",
                {
                    "Layers", layers =>
                    {
                        layers.Add("Background");
                    }
                },
                "Player",
                {
                    "Main", main =>
                    {
                        main.Add("Platforms", platforms =>
                        {
                            platforms.Add("Platform 1");
                            platforms.Add("Platform 2");
                        });
                    }
                },
                "Foreground",
                {
                    "Mountains", mountains =>
                    {
                        mountains.Add("Mountain 1");
                        mountains.Add("Mountain 2");
                        mountains.Add("Mountain 3");
                        mountains.Add("Mountain 4");
                    }
                }
            };
        }
    }
}