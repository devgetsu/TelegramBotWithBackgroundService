
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using TelegramBotWithBackgroundService.Bot.Models;

namespace TelegramBotWithBackgroundService.Bot.Services.BackgroundServices
{
    public class ConfigureWebhook : BackgroundService
    {
        private readonly BotConfiguration _configuration;
        private readonly ITelegramBotClient _botClient;

        public ConfigureWebhook(
            IConfiguration configuration,
            ITelegramBotClient botClient)
        {
            _configuration = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
            _botClient = botClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var webhookAddress = $"{_configuration.HostAddress}/bot/{_configuration.Token}";

            await _botClient.SendTextMessageAsync(
                chatId: _configuration.MyChatId,
                text: "Start weebhook");

            await _botClient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: stoppingToken);
        }
    }
}
