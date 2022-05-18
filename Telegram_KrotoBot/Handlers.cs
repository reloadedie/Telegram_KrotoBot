using System;
using System.Collections.Generic;
using System.Globalization;
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
            listUsers.Add("Penttix");
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

            Console.WriteLine($"Получено новое сообщение, его тип: {message.Type}" +
                $"его длина {new StringInfo(message.Text).LengthInTextElements}");
            if (message.Type != MessageType.Text)
                return;

            bool yesSpam = listSpam.Any(ls => message.Text.ToLower().Contains(ls));
            bool yesUser = listUsers.Any(lu => message.From.ToString().Contains(lu));
            if (yesSpam | yesUser)
            {
                await botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                Console.WriteLine($"Удалено спам - сообщение типа {message.Type}");
            }
        }

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

    }
}
