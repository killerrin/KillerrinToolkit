using GalaSoft.MvvmLight.Command;
using Killerrin.Toolkit.Core.Helpers;
using Killerrin.Toolkit.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.WPF.Models
{
    public class NumberBinding<T> : ModelBase where T : IComparable<T>
    {
        public Type NumberType { get; } = typeof(T);

        private string m_raw = "0.0";
        public string Raw
        {
            get { return m_raw; }
            set
            {
                if (m_raw == value) return;
                m_raw = value;
                RaisePropertyChanged(nameof(Raw));
                TryConvert();
            }
        }

        private object m_valueRaw = 0.0;
        public T Value { get { return (T)ValueRaw; } set { ValueRaw = value; } }
        public object ValueRaw
        {
            get { return m_valueRaw; }
            set
            {
                if (m_valueRaw.Equals(value)) return;
                m_valueRaw = value;
                RaisePropertyChanged(nameof(ValueRaw));
                RaisePropertyChanged(nameof(Value));
            }
        }

        public NumberBinding() { }
        public NumberBinding(string raw) { Raw = raw; }
        public NumberBinding(T value) : this(value.ToString(), value) { }
        public NumberBinding(string raw, T value)
        {
            Raw = raw;
            Value = value;
        }


        public RelayCommand TryConvertCommand { get { return new RelayCommand(TryConvert); } }

        /// <summary>
        /// Tries to convert Raw string into its proper Value Type 
        /// </summary>
        public void TryConvert()
        {
            if (NumberType == typeof(float))
            {
                float i = 0;
                if (float.TryParse(Raw, out i)) { ValueRaw = i; }
            }
            else if (NumberType == typeof(double))
            {
                double i = 0.0;
                if (double.TryParse(Raw, out i)) { ValueRaw = i; }
            }
            else if (NumberType == typeof(int))
            {
                int i = 0;
                if (int.TryParse(Raw, out i)) { ValueRaw = i; }
            }
            else
            {
                try
                {
                    if (TypeHelpers.TryParse<T>(Raw, out T output))
                    {
                        Value = output;
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
