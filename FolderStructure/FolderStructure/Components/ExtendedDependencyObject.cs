using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FolderStructure.Annotations;

namespace FolderStructure.Components
{
    public class ExtendedDependencyObject : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}