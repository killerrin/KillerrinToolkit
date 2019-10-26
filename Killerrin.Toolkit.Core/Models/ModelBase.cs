using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Killerrin.Toolkit.Core.Models
{
    public interface IModelBase : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// Raises the property changing event
        /// </summary>
        /// <param name="property">The property that is changing</param>
        void RaisePropertyChanging([CallerMemberName]string property = "");

        /// <summary>
        /// Raises the property changed event
        /// </summary>
        /// <param name="property">The property that changed</param>
        void RaisePropertyChanged([CallerMemberName]string property = "");
    }

    public abstract class ModelBase : IModelBase, INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        void IModelBase.RaisePropertyChanging(string property) { RaisePropertyChanging(property); }
        protected void RaisePropertyChanging([CallerMemberName]string property = "")
        {
            try
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(property));
            }
            catch (Exception) { }
        }

        void IModelBase.RaisePropertyChanged(string property) { RaisePropertyChanged(property); }
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
