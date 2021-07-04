using System;
using System.Collections.Generic;
using System.Text;

namespace SnakesAndLadders.ClassLibrary.Configs
{
    public class BoardConfig
    {
        public int InitialPosition { get; set; }
        public int LastPosition { get; set; }

        public BoardConfig(int initialPosition, int lastPosition)
        {
            this.InitialPosition = initialPosition;
            this.LastPosition = lastPosition;
        }

    }
}
