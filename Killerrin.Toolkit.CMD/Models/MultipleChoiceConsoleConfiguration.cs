using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Models
{
    public class MultipleChoiceConsoleConfiguration
    {
        public int StartX { get; set; } = 15;
        public int StartY { get; set; } = 8;
        public int OptionsPerLine { get; set; } = 3;
        public int SpacingPerLine { get; set; } = 14;

        public ConsoleColor SelectedColor { get; set; } = ConsoleColor.Red;
        public string SelectedPrefix { get; set; } = "";
        public string SelectedPostfix { get; set; } = "";

        public List<ConsoleKey> ExitKeys { get; set; } = new List<ConsoleKey>();
        public List<ConsoleKey> SelectKeys { get; set; } = new List<ConsoleKey>();
    }
}
