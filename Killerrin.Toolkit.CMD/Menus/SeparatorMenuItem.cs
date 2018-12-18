using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.CMD.Menus
{
    /// <summary>
    /// Blank MenuItem used to populate the Menu with Text
    /// </summary>
    public class SeparatorMenuItem : Menu
    {
        public const int DEFAULT_NUMBER_OF_TIMES = 40;
        public string SeparationString { get { return new string(Separator, NumberOfTimes); } }
        public char Separator { get; set; }
        public int NumberOfTimes { get; set; }

        public SeparatorMenuItem(string name, char seperator, int numberOfTimes = DEFAULT_NUMBER_OF_TIMES) : base(name)
        {
            Separator = seperator;
            NumberOfTimes = numberOfTimes;
        }


        public override void Run() { }
    }
}
