using System;
using System.Collections.Generic;
using System.Text;
using SnakesAndLadders.ClassLibrary.Configs;
using SnakesAndLadders.ClassLibrary.Enums;
using SnakesAndLadders.ClassLibrary.Impl;

namespace SnakesAndLadders.ClassLibrary.Contracts
{
    interface IBoard
    {
        bool HasWon(Token player);
        GameStatus PlayerMovement(Token player);
        void InitializeNewGame(List<Token> players, DiceConfig diceConfig, BoardConfig boardConfig);
    }
}
