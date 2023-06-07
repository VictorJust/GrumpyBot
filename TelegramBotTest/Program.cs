using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bots.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;

namespace TelegramBotExperiments
{

    class Program
    {
        // Initialising a bot with personal key
        static ITelegramBotClient bot = new TelegramBotClient("5997377638:AAFDUUbP9hC4rPpm5BmOO-x7zckgQUg9le0");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Wait for a text from a user and/or send greetings message
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                switch (message.Text.ToLower())
                {
                    case "/start":
                        await botClient.SendTextMessageAsync(message.Chat, "Good day, or bad one. Don't care, actually.");
                        break;
                    case "/authorinfo":
                        await botClient.SendTextMessageAsync(message.Chat, "Why not, who wants privacy? Here we go. " +
                            "\n HH: https://hh.ru/resume/cc62699eff0bc63f340039ed1f63564c593858" +
                            "\n LinkedIn: https://www.linkedin.com/in/victor-evseenko-99218b25b/" +
                            "\n GitHub: https://github.com/VictorJust");
                        break;
                    default:
                        await botClient.SendTextMessageAsync(message.Chat, "I wasn't waiting for anyone. But whatever, feel yourself at home.");
                        break;
                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Throw excetion
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine($"Bot {bot.GetMeAsync().Result.FirstName} launched. Sure, noone cares.");

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}