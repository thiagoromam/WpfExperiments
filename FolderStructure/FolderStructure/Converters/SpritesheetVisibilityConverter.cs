using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using FolderStructure.Models;

namespace FolderStructure.Converters
{
    public class SpritesheetVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var folder = value as SpriteSheetFolder;
            if (folder != null && !HasAnyItemInTree(folder))
                return Visibility.Collapsed;

            var item = value as SpriteSheet;
            if (item != null && !IsItemValid(item))
                return Visibility.Collapsed;

            return Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static bool HasAnyItemInTree(SpriteSheetFolder folder)
        {
            if (folder.OfType<SpriteSheet>().Any(IsItemValid))
                return true;

            return folder.OfType<SpriteSheetFolder>().Any(HasAnyItemInTree);
        }
        private static bool IsItemValid(SpriteSheet item)
        {
            var match = Regex.Replace(item.Name, "[^0-9]", "");
            return System.Convert.ToInt32(match) % 2 == 1;
        }
    }
}