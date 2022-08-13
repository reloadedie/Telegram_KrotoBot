using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace Telegram_KrotoBot
{
    public class Program : HelpClass 
    {
        private static TelegramBotClient Bot;
        private static string token { get; set; } = "5350946846:AAEAiWh-z0hnfQB_ALFxEqdNjLEAtCraudU";

        static void Main(string[] args)
        {
            Thread newBotMethodThread = new Thread(BotMethod);
            newBotMethodThread.Start();

            Thread newCaseMethodThread = new Thread(CaseMethod);
            newCaseMethodThread.Start();
        }
        
        private static void CaseMethod()
        {
            while (true)
                switch (Console.ReadLine())
                {
                    case "/command":
                        Console.Beep();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"какая-то команда выполнена.");
                        Console.ResetColor();
                        continue;

                    //cases 
                    #region
                    case "/help":
                        Console.Beep();
                        HelpClass.CaseHelpMethod();
                        continue;

                    case "/addword":
                        Console.WriteLine($"добавляем слово...");
                        HelpClass.CaseAddWordMethod();
                        continue;

                    case "/adduser":
                        Console.WriteLine($"добавляем пользователя...");
                        HelpClass.CaseAddUserMethod();
                        continue;

                    case "/deleteword":
                        Console.WriteLine($"удаляем слово...");
                        HelpClass.CaseDeleteWordMethod();
                        continue;

                    case "/deleteuser":
                        Console.WriteLine($"удаляем пользователя...");
                        HelpClass.CaseDeleteUserMethod();
                        continue;

                    case "/continue":
                        Console.WriteLine($"продолжаем очистку");
                        HelpClass.CaseContinueMethod();
                        continue;

                    case "/stop":
                        Console.WriteLine($"продолжаем очистку");
                        HelpClass.CaseStopMethod();
                        continue;

                    case "/clearconsole":
                        HelpClass.CaseClearConsoleMethod();
                        continue;

                    case "/exit":
                        HelpClass.CaseExitMethod();
                        continue;
                        #endregion
                }
        }

        private static void BotMethod()
        {
            Bot = new TelegramBotClient(token);
            var cancellationToken = new CancellationTokenSource();

            ReceiverOptions receiverOptions = new() { AllowedUpdates = { } }; //была ошибка на платформе NetCore 3.1,
                                                                              //поэтому пришлось пересоздавать проект на 5.0
            Bot.StartReceiving(Handlers.HandleUpdateAsync,//прослушивание новых сообщений. 
                               Handlers.HandleErrorAsync,//ошибки
                               receiverOptions,
                               cancellationToken.Token);
            Console.Title = "Telegram_KrotoBot";
            Console.WriteLine($"Бот запущен и ждет сообщения...");
            Console.WriteLine($"Для бота есть команды. подробнее /help");
            Console.ReadLine();
            //Console.ReadKey();
            cancellationToken.Cancel();

        }
    }
}
