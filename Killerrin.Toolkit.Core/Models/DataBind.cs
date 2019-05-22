using System.ComponentModel;

namespace Killerrin.Toolkit.Core.Models
{
    public class DataBind<T> : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private T m_value = default(T);
        public T Value
        {
            get { return m_value; }
            set
            {
                m_value = value;
                RaisePropertyChanged();
            }
        }

        public DataBind() { }
        public DataBind(T value) { Value = value; }
        public DataBind(T value, PropertyChangedEventHandler handler) { Value = value;  PropertyChanged = handler; }
        public DataBind(DataBind<T> data) { Value = data.Value;  PropertyChanged = data.PropertyChanged; }

        public static implicit operator T(DataBind<T> data) { return data.Value; }
        public static implicit operator DataBind<T>(T data) { return new DataBind<T>(data); }

        public override string ToString() { return Value.ToString(); }
        public override bool Equals(object obj) { return Value.Equals(obj); }
        public override int GetHashCode() { return Value.GetHashCode(); }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanging()
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(Value)));
        }
        public void RaisePropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }
}
