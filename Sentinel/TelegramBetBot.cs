using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace GauPoints
{
    class TelegramBetBot
    {
        static ITelegramBotClient botClient;
        
        internal void Messages()
        {
            botClient = new TelegramBotClient("1038255736:AAFv-JZ9Y_pONvhXq63h-brZ2BGH3x5m7a4");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text == null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: "You said:\n" + e.Message.Text
                );
            }
            if (e.Message.Text.Contains("!bet"))
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}, and the message is {e.Message.Text}.");
                TwitchChatBot.Client_OnMessageSent(e.Message.Text);
            }
        }

    }
}
