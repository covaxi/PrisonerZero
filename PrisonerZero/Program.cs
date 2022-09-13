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
        static async Task Main()
        {
            Console.WriteLine($"Using token {Configuration.BotToken}");
            Bot = new TelegramBotClient(Configuration.BotToken);
            Telegram.Bot.Types.User me = null;
            try
            {
                me = await Bot.GetMeAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine($"me = {me?.Username}");
            using var cts = new CancellationTokenSource();
            var options = new Telegram.Bot.Extensions.Polling.ReceiverOptions()
            {
                AllowedUpdates = new[] {UpdateType.Message},
                ThrowPendingUpdates = true
            };
            Bot.StartReceiving(new DefaultUpdateHandler(Common.HandleUpdateAsync, Common.HandleErrorAsync), options, cts.Token);
            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }
    }
}
