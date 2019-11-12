using System;
using TwitchLib.Client.Models;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Api.V5.Models.Users;

namespace GauPoints
{
    internal class TwitchChatBot
    {
        ConnectionCredentials credentials = new ConnectionCredentials(Config.Default.BotUsername, Config.Default.BotToken);
        public static TwitchClient client;
        TwitchAPI api = new TwitchAPI();
        public static string Bet;
        
        public TwitchChatBot()
        {

        }

        internal void Connect()
        {
            Console.WriteLine("Connecting");
            client = new TwitchClient();

            client.Initialize(credentials, Config.Default.ChannelName);
            client.OnLog += Client_OnLog;
            client.OnConnectionError += Client_OnConnectionError;
            // Until here, thats all ok

            // 
            client.OnMessageReceived += Client_OnMessageReceived;
            //

            client.Connect();

            //ClientId = Config.Default.ClientID;

           
        }


        //Não sei qual dos dois está funcionando, mas está funcionando, tenho que dormir.
        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message != null)
            {
                Console.WriteLine("Working");
            }
            else if (e.ChatMessage.Message.StartsWith("!bet", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(Config.Default.ChannelName, Bet);
            }
        }
        public static void Client_OnMessageSent(string bet)
        {
            if (bet.StartsWith("!bet", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Bet chegou");
                client.SendMessage(Config.Default.ChannelName, $"{bet.ToString()}");
            }
        }
        //

        TimeSpan? GetUpTime()
        {
            string userId = GetUserId(Config.Default.ChannelName);
            if(userId == null)
            {
                return null;
            }

            return api.V5.Streams.GetUptimeAsync(userId).Result;
        }

        string GetUserId(string username)
        {
            User[] userList = api.V5.Users.GetUserByNameAsync(username).Result.Matches;
            if(userList == null || userList.Length == 0)
            {
                return null;
            }
            return userList[0].Id;
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine(e.Data);
        }
        private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            Console.WriteLine($"Erro! : {e.Error}");
        }

        internal void Disconnect()
        {
            Console.WriteLine("Disconnecting");
        }


    }
}