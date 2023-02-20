using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using System.Xml;

namespace TelegramBot
{
    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient(System.IO.File.ReadAllText("tokens.txt"));
        public static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                try
                {
                    var message = update.Message;
                    if (message.Text == "/start")
                    {
                            await botClient.SendTextMessageAsync(message.Chat, "watashi wa kurapika");
                        return;
                    }
                    if (message.Text != null && message.Text.ToLower() == "расписание")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Расписание на " + DateTime.Now.ToShortDateString());
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка отправки сообщения");
                }
            }
        }

        public static async Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

                bot.StartReceiving(
                    Update,
                    Error
                    );
                Console.ReadLine();

        }
    }
}


