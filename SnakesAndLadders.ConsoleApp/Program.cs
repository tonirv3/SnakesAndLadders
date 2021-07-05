using Microsoft.Extensions.DependencyInjection;
using SnakesAndLadders.ClassLibrary.Contracts;
using SnakesAndLadders.ClassLibrary.Enums;
using SnakesAndLadders.ClassLibrary.Impl;
using System;
using System.Collections.Generic;

namespace SnakesAndLadders.ConsoleApp
{

    class Program
    {
        protected static IBoard _board { get; set; }
        static void Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<IBoard, Board>()
                .AddSingleton<IDice, Dice>()
                .BuildServiceProvider();

            _board = serviceProvider.GetService<IBoard>();


            Console.WriteLine("Starting New Game");

            Console.WriteLine("How many players?");

            var players = Console.ReadLine();

            int.TryParse(players, out int numPlayers);

            List<Token> playerTokens = new List<Token>();

            for (int i = 0; i < numPlayers; i++)
            {
                playerTokens.Add(new Token());
            }

            _board.InitializeNewGame(playerTokens, new ClassLibrary.Configs.DiceConfig(1, 6), new ClassLibrary.Configs.BoardConfig(1, 100));

            var gameStatus = GameStatus.InProgress;

            while (gameStatus == GameStatus.InProgress)
            {
                foreach (var item in _board.TokenPlayers)
                {
                    gameStatus = _board.PlayerMovement(item);

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
                Console.WriteLine($"Player {_board.Winner?.Id} has won the game !!");
        }

    } 

}
