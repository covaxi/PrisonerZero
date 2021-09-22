using PrisonerZero.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;

namespace PrisonerZero
{
    internal class Program
    {
        internal static TelegramBotClient Bot;
        static async Task Main(string[] args)
        {
            Bot = new TelegramBotClient(Configuration.BotToken);
            var me = await Bot.GetMeAsync();

            Console.Title = me.Username;
            using var cts = new CancellationTokenSource();
            var options = new Telegram.Bot.Extensions.Polling.ReceiverOptions()
            {
                AllowedUpdates = new[] {UpdateType.Message},
                ThrowPendingUpdates = true
            };
            Bot.StartReceiving(new DefaultUpdateHandler(Common.HandleUpdateAsync, Common.HandleErrorAsync), options, cts.Token);
            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }
    }
}
