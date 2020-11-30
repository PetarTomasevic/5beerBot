using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ViberBot.Enum;
using ViberBot.Helpers;
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
        private IMemoryCache _cache;

        /// <summary>
        ///
        /// </summary>
        /// <param name="viberBot"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="httpClientFactory"></param>
        public ViberBotController(IMemoryCache memoryCache, IViberBot viberBot, ILogger<ViberBotController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _viberBot = viberBot;
            _cache = memoryCache;
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
                    Keyboard keyboard;
                    keyboard = MainKeyboard();
                    SendTextMessage(callbackData, "Welcome to 5beer BOT", keyboard);
                }
                if (callbackData.Message is TextMessage)
                {
                    Keyboard keyboard;
                    keyboard = MainKeyboard();
                    TextMessage getUserMessage = (TextMessage)callbackData.Message;

                    SendTextMessage(callbackData, getUserMessage.Text, keyboard);
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
        ///
        /// </summary>
        /// <param name="callbackData"></param>
        /// <param name="userResponse"></param>
        [NonAction]
        public void SendTextMessage(CallbackData callbackData, string userResponse, Keyboard keyboard)
        {
            try
            {
                User user = new User();

                user = callbackData.Sender;
                String context = callbackData.Context;
                var messageText = "Welcome Message in 5 beer BOT";

                if (_cache.TryGetValue(user.Id, out List<TrackingDataItem> trackItems))
                {
                    var inList = trackItems.FindIndex(x => x.QuestionNumber == userResponse);
                    if (inList != -1)
                    {
                        bool success = Int32.TryParse(trackItems[inList].QuestionNumber, out int getQuestion);
                        if (success)
                        {
                            trackItems[inList].UserId = user.Id;
                            trackItems[inList].QuestionNumber = trackItems[inList].QuestionNumber;
                            trackItems[inList].Counter = trackItems[inList].Counter + 1;
                            // trackItems.Add(trackingAlready);
                            _cache.Set(user.Id, trackItems, new MemoryCacheEntryOptions()
                                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15)));

                            messageText = SendEinsteinQuote(Convert.ToInt32(getQuestion), trackItems[inList].Counter).Result;
                            _logger.LogError(getQuestion, "odabrani broj");
                        }
                        else
                        {
                            messageText = SendInfoMessage(userResponse).Result;
                        }
                    }
                    else
                    {
                        bool success = Int32.TryParse(userResponse, out int getQuestion);
                        if (success)
                        {
                            TrackingDataItem newTrackingItem = new TrackingDataItem()
                            {
                                Counter = 1,
                                QuestionNumber = userResponse,
                                UserId = user.Id
                            };

                            trackItems.Add(newTrackingItem);
                            _cache.Set(user.Id, trackItems, new MemoryCacheEntryOptions()
                           .SetAbsoluteExpiration(TimeSpan.FromMinutes(15)));
                            messageText = SendEinsteinQuote(Convert.ToInt32(getQuestion), newTrackingItem.Counter).Result;
                            _logger.LogError(getQuestion, "odabrani broj");
                        }
                        else
                        {
                            messageText = SendInfoMessage(userResponse).Result;
                        }
                    }
                }
                else
                {
                    bool success = Int32.TryParse(userResponse, out int getQuestion);
                    if (success)
                    {
                        TrackingDataItem newTrackingItem = new TrackingDataItem()
                        {
                            Counter = 1,
                            QuestionNumber = userResponse,
                            UserId = user.Id
                        };
                        List<TrackingDataItem> trackIt = new List<TrackingDataItem>();

                        messageText = SendEinsteinQuote(getQuestion, newTrackingItem.Counter).Result;
                        trackIt.Add(newTrackingItem);
                        _cache.Set(user.Id, trackIt, new MemoryCacheEntryOptions()
                          .SetAbsoluteExpiration(TimeSpan.FromMinutes(15)));
                    }
                    else
                    {
                        messageText = SendInfoMessage(userResponse).Result;
                    }
                }
                ExcecuteMessaging(callbackData, messageText, user.Id, keyboard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "FUBAR here");
                ex.ToString();
            }
        }

        /// <summary>
        ///  Execute event whatever event is
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="messageText"></param>
        /// <param name="userId"></param>
        /// <param name="keyboard"></param>
        [NonAction]
        public void ExcecuteMessaging(CallbackData callback, string messageText, string userId, Keyboard keyboard)
        {
            switch (callback.Event)
            {
                case EventTypeEnum.Message:

                    if (!string.IsNullOrEmpty(messageText))
                    {
                        TextMessage message = new TextMessage();
                        message.Receiver = userId;
                        message.Sender = new User()
                        {
                            Name = "Simon Says"
                        };
                        message.Text = messageText;
                        message.Keyboard = keyboard;

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

        /// <summary>
        /// quote selection
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        [NonAction]
        public Task<string> SendEinsteinQuote(int selection, int counter)
        {
            var returnMessage = "";

            switch (selection)
            {
                case 1:
                    returnMessage = $"Logic will get you from A to Z. Imagination will get you everywhere. (Albert Einstein) ({counter})"; ;
                    break;

                case 2:
                    returnMessage = $"Never memorize something that you can look up.  (Albert Einstein) ({counter})";
                    break;

                case 3:
                    returnMessage = $"I believe in intuitions and inspirations. I sometimes feel that I am right. I do not know that I am.  (Albert Einstein) ({counter})";
                    break;

                case 4:
                    returnMessage = $"A table, a chair, a bowl of fruit and a violin; what else does a man need to be happy?  (Albert Einstein) ({counter})";
                    break;

                case 5:
                    returnMessage = $"The important thing is not to stop questioning. Curiosity has its own reason for existence.  (Albert Einstein) ({counter})";
                    break;

                case 6:
                    returnMessage = $"Don't force it; get a larger hammer.  (Murphy's Law) ({counter})";
                    break;

                case 7:
                    returnMessage = $"Complex problems have simple, easy to understand wrong answers. (Murphy's Law) ({counter})";
                    break;

                case 8:
                    returnMessage = $"If at first you don't succeed, destroy all evidence that you have tried.  (Murphy's Law) ({counter})";
                    break;

                case 9:
                    returnMessage = $"If you can't learn to do it well, you should learn to enjoy doing it badly. (Murphy's Law)  ({counter})";
                    break;

                case 10:
                    returnMessage = $"For instance, on the planet Earth, man had always assumed that he was more intelligent than dolphins because he had achieved so much—the wheel, New York, wars and so on—whilst all the dolphins had ever done was muck about in the water having a good time. But conversely, the dolphins had always believed that they were far more intelligent than man—for precisely the same reasons. (Douglas Adams)  ({counter})";
                    break;

                case 11:
                    returnMessage = $"Time is an illusion. Lunchtime doubly so. (Douglas Adams)   ({counter})";
                    break;

                case 12:
                    returnMessage = $"A common mistake that people make when trying to design something completely foolproof is to underestimate the ingenuity of complete fools. (Douglas Adams)   ({counter})";
                    break;

                case 13:
                    returnMessage = $"For a moment, nothing happened. Then, after a second or so, nothing continued to happen. (Douglas Adams)   ({counter})";
                    break;

                case 14:
                    returnMessage = $"Don't you understand that we need to be childish in order to understand? Only a child sees things with perfect clarity, because it hasn't developed all those filters which prevent us from seeing things that we don't expect to see. (Douglas Adams)   ({counter})";
                    break;

                case 15:
                    returnMessage = $"Human beings, who are almost unique in having the ability to learn from the experience of others, are also remarkable for their apparent disinclination to do so. (Douglas Adams)  ({counter})";
                    break;

                default:
                    returnMessage = $"You miserably failed to follow instruction.";
                    break;
            };

            return Task.FromResult(returnMessage);
        }

        /// <summary>
        /// buton actions selection
        /// </summary>
        /// <param name="messageAction"></param>
        /// <returns></returns>
        [NonAction]
        public Task<string> SendInfoMessage(string messageAction)
        {
            var returnMessage = "";

            switch (messageAction)
            {
                case "GetEinstein":
                    returnMessage = $"Enter number between 1 and 5"; ;
                    break;

                case "GetMurphy":
                    returnMessage = $"Enter number between 6 and 9";
                    break;

                case "GetAdams":
                    returnMessage = $"Enter number between 10 and 15";
                    break;

                default:
                    returnMessage = $"whoopsie-daisy something is wrong! Try with buttons.";
                    break;
            };

            return Task.FromResult(returnMessage);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public Keyboard MainKeyboard()
        {
            string bgColor = "#1998bf";
            string bgColor1 = "#4a4a4a";
            string bgColor2 = "#1998bf";

            var buttons = new List<KeyboardButton>();

            buttons.AddRange(new KeyboardButton[]
               {
               GetKeyboardButton(2, bgColor, "Einstein", "GetEinstein"),
               GetKeyboardButton(2, bgColor1, "Murphy", "GetMurphy"),
                GetKeyboardButton(2, bgColor2, "Douglas Adams", "GetAdams")
               });

            Keyboard keyboard = new Keyboard()
            {
                Buttons = buttons.ToArray()
            };
            return keyboard;
        }

        [NonAction]
        private KeyboardButton GetKeyboardButton(int cols, string bgColor, string label, string action, bool silent = false, KeyboardActionTypeEnum actionType = KeyboardActionTypeEnum.Reply, InternalBrowser browser = null, OpenUrlTypeEnum openURLType = OpenUrlTypeEnum.Internal, TextSizeEnum textSize = TextSizeEnum.Regular, TextHorizontalAlignEnum textHAlign = TextHorizontalAlignEnum.Center, TextVerticalAlignEnum textVAlign = TextVerticalAlignEnum.Middle)
        {
            return GetKeyboardButton(1, cols, bgColor, label, action, null, silent, actionType, browser, openURLType, textSize, textHAlign, textVAlign);
        }

        [NonAction]
        private KeyboardButton GetKeyboardButton(int rows, int cols, string bgColor, string label, string action, string imageUrl = null, bool silent = false, KeyboardActionTypeEnum actionType = KeyboardActionTypeEnum.Reply, InternalBrowser browser = null, OpenUrlTypeEnum openURLType = OpenUrlTypeEnum.Internal, TextSizeEnum textSize = TextSizeEnum.Regular, TextHorizontalAlignEnum textHAlign = TextHorizontalAlignEnum.Center, TextVerticalAlignEnum textVAlign = TextVerticalAlignEnum.Middle)
        {
            string bgndCol = bgColor, frameCol = bgColor;
            return new KeyboardButton()
            {
                Columns = cols,
                Rows = rows,
                Text = $"<font color='#000000'>{label}</font>",
                TextSize = textSize,
                TextHorizontalAlign = textHAlign,
                TextVerticalAlign = textVAlign,
                ActionType = actionType,
                ActionBody = action,
                BackgroundColor = bgndCol,
                Silent = silent,
                InternalBrowser = browser,
                UrlOpenType = openURLType,
                TextShouldFit = true,
                Frame = new KeyboardButtonFrame()
                {
                    BorderColor = frameCol,
                    BorderWidth = 5,
                    CornerRadius = 2
                },
                Image = imageUrl
            };
        }
    }
}