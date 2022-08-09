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
            Thread newThread = new Thread(BotMethod);
            newThread.Start();


        }

        private static void BotMethod()
        {
            Bot = new TelegramBotClient(token);
            var cancellationToken = new CancellationTokenSource();

            ReceiverOptions receiverOptions = new() { AllowedUpdates = { } }; //была ошибка на платформе NetCore 3.1,
                                                                              //поэтому пришлось пересоздавать проект на 5.0
            Bot.StartReceiving(Handlers.HandleUpdateAsync,//прослушивание новых сообщений. 
                               Handlers.HandleErrorAsync,
                               receiverOptions,
                               cancellationToken.Token);

            Console.WriteLine($"Бот запущен и ждет сообщения...");
            Console.WriteLine($"Для бота есть команды. подробнее /help");

            Console.ReadLine();
            cancellationToken.Cancel();

            while (true)
                switch (Console.ReadLine())
                {
                    case "/help":
                        Console.WriteLine("Команды для бота:");
                        Console.WriteLine("\t/help - вывод списка команд");
                        Console.WriteLine("\t/command - запустить какую-то команду");
                        Console.WriteLine("Ваши действия?");
                        break;

                    case "/command":
                        Console.WriteLine($"какая-то команда выполнена.");
                        break;

                    case "/continue":
                        Console.WriteLine($"продолжаем очистку");
                        break;

                    case "/addword":
                        Console.WriteLine($"добавляем слово...");
                        break;

                    case "/adduser":
                        Console.WriteLine($"добавляем пользователя...");
                        break;

                    case "/deleteword":
                        Console.WriteLine($"удаляем слово...");
                        break;

                    case "/deleteuser":
                        Console.WriteLine($"удаляем пользователя...");
                        break;
                }
        }
    }
}
