using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram_KrotoBot
{
    public class Handlers
    {
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type
            switch
            {
                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!),
                UpdateType.EditedMessage => BotOnMessageReceived(botClient, update.EditedMessage!),
               // UpdateType.ChatJoinRequest => BotOnChatReceived(botClient, update),
                _ => UnknownUpdateHandlerAsync(botClient, update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            List<string> listUsers = new List<string>();
            #region
            listUsers.Add("someuser");
            //listUsers.Add("Penttix");
            listUsers.Add("studentkrot_bot");
            #endregion

            List<string> listSpam = new List<string>();
            #region
            listSpam.Add("sometext");
            listSpam.Add("123");
            listSpam.Add("1 2 3");
            listSpam.Add("http");
            listSpam.Add("https");
            listSpam.Add("www");
            listSpam.Add("man");
            listSpam.Add("vostretsova");
            listSpam.Add("вострецова");
            listSpam.Add("dfgdg");
            #endregion

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Получено новое сообщение, его тип: {message.Type};" +
                $" его длина {new StringInfo(message.Text).LengthInTextElements}");
            Console.ResetColor();
            if (message.Type != MessageType.Text)
                return;

            bool yesUser = listUsers.Any(listU => message.From.ToString().Contains(listU));
            if (yesUser)
            {
                await botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Удалено сообщение (тип {message.Type}), {message.From}" +
                    $" находится в чёрном списке");
                Console.ResetColor();
                await botClient.SendTextMessageAsync(message.Chat.Id, message.Text =
                    $"{message.From} ты в бане!");
               
                Thread deleteBotThread = new Thread(() =>
                {
                    BotDeleteMethod(botClient, message);
                });
                deleteBotThread.Start();
            }

            bool yesSpam = listSpam.Any(listS => message.Text.ToLower().Contains(listS));
            if (yesSpam)
            {
                await botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Удалено спам - сообщение (тип {message.Type}),"
                   + $" написал его {message.From}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($" сообщение с бан-словом: {message.Text}");
                Console.ResetColor();
                await botClient.SendTextMessageAsync(message.Chat.Id, message.Text =
                    $"{message.From} Эээ нехорошо пишешь!");

                Thread deleteBotThread = new Thread(() =>
                {
                    BotDeleteMethod(botClient, message);
                });
                deleteBotThread.Start();

            }
            /*
            for (int i = 1; i < 11; i++)
            {
                //тут удаление сообщения
                //
                if (i > 10)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id,
                        message.Text = $"{message.From}, ты сейчас будешь забанен!");
                    i = 0;
                    break;
                }
            }
           */
        }
        
        private static void BotDeleteMethod(ITelegramBotClient botClient, Message message)
        {
            Thread.Sleep(5000);
            botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
        }
        //unkowns + errors nadlers
        #region
        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Неизвестный тип сообщения: {update.Type}");
            return Task.CompletedTask;
        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception
            switch
            {
                ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        #endregion
    }
}
