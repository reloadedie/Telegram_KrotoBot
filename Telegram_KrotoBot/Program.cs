using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace Telegram_KrotoBot
{
    public class Program
    {
        private static TelegramBotClient Bot;
        private static string token { get; set; } = "5350946846:AAEAiWh-z0hnfQB_ALFxEqdNjLEAtCraudU";
        static void Main(string[] args)
        {
            Bot = new TelegramBotClient(token);
            using var cancellationToken = new CancellationTokenSource();

            ReceiverOptions receiverOptions = new() { AllowedUpdates = { } }; //была ошибка на платформе NetCore 3.1,
                                                                              //поэтому пришлось пересоздавать проект на 5.0
                                                                              //прослушивание новых сообщений. 
            Bot.StartReceiving(Handlers.HandleUpdateAsync,
                               Handlers.HandleErrorAsync,
                               receiverOptions,
                               cancellationToken.Token);

            Console.WriteLine($"Бот запущен и ждет сообщения...");
            Console.ReadLine();
            cancellationToken.Cancel();
        }
    }
}
