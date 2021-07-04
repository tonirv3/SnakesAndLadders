using System;
using SnakesAndLadders.ClassLibrary.Configs;
using SnakesAndLadders.ClassLibrary.Contracts;

namespace SnakesAndLadders.ClassLibrary.Impl
{
    public class Dice : IDice
    {
        public DiceConfig Config { get; set; }

        public Dice(DiceConfig config)
        {
            if (config is null)
                throw new ArgumentNullException();


            this.Config = config;
        }

        public int Roll()
        {
            return new Random().Next(Config.MinValue, Config.MaxValue);
        }
    }
}
