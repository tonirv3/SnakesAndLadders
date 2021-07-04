using System;
using System.Collections.Generic;
using System.Text;

namespace SnakesAndLadders.ClassLibrary.Configs
{
    public class DiceConfig
    {
        public short MinValue { get; set; }
        public short MaxValue { get; set; }

        public DiceConfig(short minValue, short maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;

        }
    }
}
