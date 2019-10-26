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
                RaisePropertyChanging();
                m_value = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Whether the DataBind contains a value
        /// </summary>
        public bool HasValue
        {
            get { return m_value != null; }
        }

        /// <summary>
        /// Checks if the Value is null, and if the value is default(T)
        /// </summary>
        public bool IsDefault
        {
            get
            {
                if (!HasValue) return true;
                return m_value.Equals(default(T));
            }
        }

        public DataBind() { }
        public DataBind(T value) { Value = value; }
        public DataBind(T value, PropertyChangedEventHandler handler) { Value = value; PropertyChanged = handler; }
        public DataBind(DataBind<T> data)
        {
            Value = data.Value;
            PropertyChanging = data.PropertyChanging;
            PropertyChanged = data.PropertyChanged;
        }

        public static implicit operator T(DataBind<T> data) { return data.Value; }
        public static implicit operator DataBind<T>(T data) { return new DataBind<T>(data); }

        public override string ToString() { return Value.ToString(); }
        public override bool Equals(object obj) { return Value.Equals(obj); }
        public override int GetHashCode() { return Value.GetHashCode(); }

        /// <summary>
        /// Event Handler for Modelbinding on Property Changing. 
        /// </summary>
        /// <remarks>
        /// This event handler fires multiple times in a row for all <see cref="Value"/>, <see cref="HasValue" and <see cref="NotDefault"/>.
        /// </remarks>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Event Handler for Modelbinding on Property Changed. 
        /// </summary>
        /// <remarks>
        /// This event handler fires multiple times in a row for all <see cref="Value"/>, <see cref="HasValue" and <see cref="NotDefault"/>.
        /// </remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Special Event Handler that only triggers on <see cref="Value"/> Change.
        /// </summary>
        /// <remarks>
        /// This event is only fired once.
        /// </remarks>
        public event PropertyChangedEventHandler ValueChanged;

        /// <summary>
        /// Raises all the property changing events
        /// </summary>
        public void RaisePropertyChanging()
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(Value)));
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(HasValue)));
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(IsDefault)));
        }

        /// <summary>
        /// Raises all the property changed events
        /// </summary>
        public void RaisePropertyChanged()
        {
            ValueChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasValue)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDefault)));
        }
    }
}
