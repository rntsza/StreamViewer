using System;

namespace GauPoints
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitchChatBot bot = new TwitchChatBot();
            TelegramBetBot betBot = new TelegramBetBot();

            bot.Connect();
            betBot.Messages();

            Console.ReadLine();

            bot.Disconnect();
        }
    }
}