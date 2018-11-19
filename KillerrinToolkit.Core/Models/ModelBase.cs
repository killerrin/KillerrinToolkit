using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Killerrin.Toolkit.Core.Models
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName]string property = "")
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }
            catch (Exception) { }
        }
    }
}
