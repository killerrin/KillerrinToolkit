using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.Core.Models
{
    public struct ProgressData<T>
    {
        public int Progress { get; }
        public T Value { get; }

        public ProgressData(int progress, T value) {
            Progress = progress;
            Value = value;
        }
    }
}
