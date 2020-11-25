using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ViberBot.Enum;
using ViberBot.Interface;
using ViberBot.Model;

namespace The5beerBot.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ViberBotController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IViberBot _viberBot;

        /// <summary>
        ///
        /// </summary>
        /// <param name="viberBot"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="httpClientFactory"></param>
        public ViberBotController(IViberBot viberBot, ILogger<ViberBotController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _viberBot = viberBot;
        }

        /// <summary>
        /// SetWebhookAsync
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SetWebhookAsync()
        {
            try
            {
                WebhookParams p = new WebhookParams(_configuration["HttpsForWeebHook"]);
                p.EventTypes = ((IEnumerable<EventTypeEnum>)Enum.GetValues(typeof(EventTypeEnum)));
                p.SendUserName = true;
                p.SendUserPhoto = true;
                var retVal = await _viberBot.SetWebhookAsync(p);
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "FUBAR here");
                return null;
            }
        }

        /// <summary>
        /// Remove WebHook
        /// </summary>
        /// <returns></returns>
        [HttpGet("remove-hook")]
        public async Task<IActionResult> RemoveWebhookAsync()
        {
            await _viberBot.RemoveWebhookAsync();
            return Ok("Webhook removed");
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="json"></param>
        [HttpPost]
        public void Post([FromBody] JObject json)
        {
            try
            {
                var callbackData = json.ToObject<CallbackData>();

                if (callbackData.Subscribed == true)
                {
                    // SendTextMessage(callbackData);
                }
                if (callbackData.Message is TextMessage)
                {
                    TextMessage getUserMessage = (TextMessage)callbackData.Message;
                    var getText = getUserMessage.Text;

                    if (getText != "1")
                    {
                        SendTextMessage(callbackData, 2);
                    }
                    else
                    {
                        SendTextMessage(callbackData, 1);
                    }
                }

                //if (callbackData.Message != null)
                //{
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), nameof(Post) + "ups we have some FUBAR here");
            }
            _logger.LogInformation("callback from viber succedded");
        }

        /// <summary>
        /// SendTextMessage
        /// </summary>
        /// <param name="callbackData"></param>
        /// <param name="step"></param>
        [NonAction]
        public void SendTextMessage(CallbackData callbackData, int step)
        {
            try
            {
                User user = new User();
                user = callbackData.Sender;
                String context = callbackData.Context;
                string messageText = $"Logic will get you from A to Z. Imagination will get you everywhere. (Albert Einstein)";
                if (step == 1)
                {
                    messageText = $"Never memorize something that you can look up.  (Albert Einstein)";
                }
                switch (callbackData.Event)
                {
                    case EventTypeEnum.Message:

                        if (!string.IsNullOrEmpty(messageText))
                        {
                            TextMessage message = new TextMessage();
                            message.Receiver = user.Id;
                            message.Sender = new User()
                            {
                                Name = "BiberTest"
                            };
                            message.Text = messageText;

                            _viberBot.SendMessageAsync(message);
                        }
                        break;

                    case EventTypeEnum.Subscribed:
                        break;

                    case EventTypeEnum.Action:
                        break;

                    case EventTypeEnum.ClientStatus:
                        break;

                    case EventTypeEnum.Delivered:
                        break;

                    case EventTypeEnum.Failed:
                        break;

                    case EventTypeEnum.Unsubscribed:
                        break;

                    case EventTypeEnum.ConversationStarted:
                        break;

                    default:
                        break;
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "FUBAR here");
                ex.ToString();
            }
        }
    }
}