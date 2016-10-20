using System.ComponentModel;
using System.Runtime.CompilerServices;
using FolderStructure.Annotations;

namespace FolderStructure.Components
{
    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Set<T>(ref T variable, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(value, variable))
            {
                variable = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}