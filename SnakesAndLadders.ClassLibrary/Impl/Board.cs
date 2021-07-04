using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnakesAndLadders.ClassLibrary.Configs;
using SnakesAndLadders.ClassLibrary.Contracts;

namespace SnakesAndLadders.ClassLibrary.Impl
{
    public class Board : IBoard
    {

        public IDice Dice { get; set; }
        public List<Token> TokenPlayers { get; set; } = new List<Token>();
        public BoardConfig BoardConfig { get; set; }
        public Token? Winner { get; set; }

        public Board(IDice dice)
        {
            this.Dice = dice;
        }

        public void InitializeNewGame(List<Token> players, DiceConfig diceConfig, BoardConfig boardConfig)
        {
            this.BoardConfig = boardConfig;

            this.Dice = new Dice(diceConfig);

            if (!players.Any() || players.Count() < 2)
            {
                throw new ArgumentException("To initialize the game players must contains at least 2 items ");
            }

            foreach (var player in players)
            {                
                player.CurrentPosition = BoardConfig.InitialPosition;                
                this.TokenPlayers.Add(player);
            }            
        }

        public bool HasWon(Token player)
        {
            return player.CurrentPosition == this.BoardConfig.LastPosition;
        }
        public void PlayerMovement(Token player)
        {
            var movements = Dice.Roll();
            player.Movement(movements, this.BoardConfig.LastPosition);
        }
    }
}
