using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

            var commands = string.Join('|',WeatherCommand.Commands.Concat(RandomCommand.Commands));

            var match = Regex.Match(message.Text ?? "", $"^([/!](?'command'{commands}))(?'personal'@{Configuration.BotNick})?(\\s(?'payload'.*))?$");

            var command = match.Groups["command"].Value;
            var personal = match.Groups["personal"].Success;
            var payload = match.Groups["payload"].Value;
            if (match.Success && WeatherCommand.Commands.Contains(command) || message.Text == $"@{Configuration.BotNick}")
            {
                await Reply(botClient, message, await WeatherCommand.GetWeather(payload));
            }
            else if (match.Success && RandomCommand.Commands.Contains(command))
            {
                await Reply(botClient, message, await RandomCommand.GetRandom(payload));
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
