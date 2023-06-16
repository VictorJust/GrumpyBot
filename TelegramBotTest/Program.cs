﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bots.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.ReplyMarkups;

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

                if (message.Text == "/start")
                {
                    // Create an instance of InlineKeyboardMarkup
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Start", "/start"),
                    InlineKeyboardButton.WithCallbackData("Author info", "/authorinfo"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("What a bot", "/whatabot"),
                    InlineKeyboardButton.WithCallbackData("Call the manager", "option4 / default message"),
                }
            });

                    // Send the inline keyboard as a reply to the user's message
                    await botClient.SendTextMessageAsync(message.Chat, "You don't even have to write:", replyMarkup: inlineKeyboard);
                }
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                var callbackQuery = update.CallbackQuery;
                var data = callbackQuery.Data;

                // Handle the callback query based on the data received
                switch (data)
                {
                    case "/start":
                        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, " Good day, or bad one. Don't care, actually." +
                            "\n If you don't like buttons, boomer, you can use oldschool queries:" +
                            "\n /authorinfo - guess what it is about" +
                            "\n /whatabot - why you are here, why I am here");
                        break;
                    case "/authorinfo":
                        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, " Why not, who wants privacy? Here we go. " +
                            "\n HH: https://hh.ru/resume/cc62699eff0bc63f340039ed1f63564c593858" +
                            "\n LinkedIn: https://www.linkedin.com/in/victor-evseenko-99218b25b/" +
                            "\n GitHub: https://github.com/VictorJust");
                        break;
                    case "/whatabot":
                        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, " In previous life I was cursed for... None of you business actually." +
                            "\n And now I am a bot manager. So ask me anything. I won't be surprised, won't care and most likely won't be helpful.");
                        break;
                    default:
                        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, "I wasn't waiting for anyone. But whatever, feel yourself at home.");
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