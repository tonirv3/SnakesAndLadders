using SnakesAndLadders.ClassLibrary.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakesAndLadders.ClassLibrary.Contracts
{
    public interface IDice
    {
        int Roll();
        void AddConfig(DiceConfig diceConfig);
    }
}
