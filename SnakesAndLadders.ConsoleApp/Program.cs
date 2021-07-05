using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SnakesAndLadders.ClassLibrary.Contracts;
using SnakesAndLadders.ClassLibrary.Impl;
using SnakesAndLadders.ClassLibrary.Enums;
using System;
using System.Collections.Generic;
using SnakesAndLadders.ClassLibrary.Configs;

namespace SnakesAndLadders.ConsoleApp
{

    class Program
    {
        protected static IDice _dice { get; set; }
        static void Main(string[] args)
        {

            Console.WriteLine("Starting New Game");

            Console.WriteLine("How many players?");

            var players = Console.ReadLine();

            int.TryParse(players, out int numPlayers);

            List<Token> playerTokens = new List<Token>();

            for (int i = 0; i < numPlayers; i++)
            {
                playerTokens.Add(new Token());
            }

            var board = new Board(_dice);

            board.InitializeNewGame(playerTokens, new ClassLibrary.Configs.DiceConfig(1, 6), new ClassLibrary.Configs.BoardConfig(1, 100));

            var gameStatus = GameStatus.InProgress;

            while (gameStatus == GameStatus.InProgress)
            {
                foreach (var item in board.TokenPlayers)
                {
                    gameStatus = board.PlayerMovement(item);

                    if (gameStatus == GameStatus.Finished)
                    {
                        break;
                    }

                    Console.WriteLine($"Player {item.Id} is in {item.CurrentPosition} square");
                }

                Console.WriteLine("Next Movement, press any key...");
                Console.ReadLine();
            }

            if (gameStatus == GameStatus.Finished)
                Console.WriteLine($"Player {board.Winner?.Id} has won the game !!");
        }

    } 

}
