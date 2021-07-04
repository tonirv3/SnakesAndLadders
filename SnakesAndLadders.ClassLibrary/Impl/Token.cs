using System;

namespace SnakesAndLadders.ClassLibrary.Impl
{
    public class Token
    {

        public Guid Id { get; set; }
        public int CurrentPosition { get; set; }

        public Token()
        {
            this.Id = Guid.NewGuid();
        }

        public void Movement(int movements, int maxPosition)
        {

            if ((this.CurrentPosition + movements) > maxPosition)
                return;

            this.CurrentPosition += movements;

        }

    }
}
