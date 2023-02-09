using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using System.Xml;
using Microsoft.VisualBasic;

namespace TelegramBot
{
    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("6249665060:AAFNRFlI-pLFSV04Fa2vmqPmcf6vNvhqF1k");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
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
                        await botClient.SendPhotoAsync(message.Chat, photo: "https://ibb.co/CJfL3Dh");
                        await botClient.SendTextMessageAsync(message.Chat, "Расписание на " + DateTime.Now.ToShortDateString());
                    }
                    await botClient.SendTextMessageAsync(message.Chat, "Короче, лучше замутайте бота, он отвечает, только когда я зафиксил баги:)");
                }
                catch
                {
                    Console.WriteLine("Ошибка отправки сообщения");
                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

                bot.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync
                    );
                Console.ReadLine();

        }
    }
}


