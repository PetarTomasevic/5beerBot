using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViberBot.Enum;
using ViberBot.Model;

namespace ViberBot.Interface
{
    public interface IViberBot
    {
        /// <summary>
        /// Sets a webhook. This webhook will be used for receiving callbacks and user messages from Viber
        /// </summary>
        /// <param name="webHookParams">Webhook parameters</param>
        /// <returns></returns>
        Task<IEnumerable<EventTypeEnum>> SetWebhookAsync(WebhookParams webHookParams);

        /// <summary>
        /// Removes the webhook
        /// </summary>
        /// <returns></returns>
        Task RemoveWebhookAsync();

        /// <summary>
        /// Send Message to Bot
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<Int64?> SendMessageAsync(MessageBase message);
    }
}