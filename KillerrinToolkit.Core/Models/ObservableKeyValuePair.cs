using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerrinToolkit.Core.Models
{
    public class ObservableKeyValuePair<K, V> : ModelBase
    {
        private K m_key = default(K);
        public K Key
        {
            get { return m_key; }
            set
            {
                m_key = value;
                RaisePropertyChanged(nameof(Key));
            }
        }

        private V m_value = default(V);
        public V Value
        {
            get { return m_value; }
            set
            {
                m_value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }

        public ObservableKeyValuePair() : this(default(K), default(V)) { }
        public ObservableKeyValuePair(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
            //return $"{Key} = {Value}";
        }
    }
}
