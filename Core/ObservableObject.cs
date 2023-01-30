using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Moosic.Core
{
    class ObservableObject : INotifyPropertyChanged
    {

#pragma warning disable CS8632
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS8632

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}