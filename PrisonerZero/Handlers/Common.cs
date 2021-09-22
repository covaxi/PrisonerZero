using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrisonerZero.Handlers
{
    public class Common
    {
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(botClient, update.Message),
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
            Console.WriteLine($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return;

            var texts = message.Text?.Split(' ').ToArray();
            var action = texts?.First()?.ToLower();
            
            if (texts.Length >= 1 && (action == "/w" 
                    || action == "!w"
                    || action == "!weather"
                    || action == "/weather"
                    || action == "/погода"
                    || action == "!погода"
                    || action == "!п"
                    || action == "/п"
                    || action == "!в"
                    || action == "/в"))
            {
                var sentMessage = await Reply(botClient, message, await Weather.GetWeather(texts.Length > 1 ? texts[1] : null));
                Console.WriteLine($"The message was sent with id: {sentMessage.MessageId}");
            }

            static async Task<Message> Reply(ITelegramBotClient botClient, Message message, string text)
            {
                return await botClient.SendTextMessageAsync(message.Chat.Id, text, replyToMessageId: message.MessageId);
            }
        }

        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }
    }
}
