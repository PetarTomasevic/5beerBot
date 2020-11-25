using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using ViberBot.Enum;

namespace ViberBot.Model
{
    public class CallbackData
    {
        /// <summary>
        /// Callback type - which event triggered the callback.
        /// </summary>
        [JsonProperty("event")]
        public EventTypeEnum Event { get; set; }

        /// <summary>
        /// Unique ID of the message.
        /// </summary>
        [JsonProperty("message_token")]
        public long MessageToken { get; set; }

        /// <summary>
        /// Time of the event that triggered the callback (epoch time).
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Unique Viber user id.
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// Viber user.
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }

        /// <summary>
        /// Indicated whether a user is already subscribed.
        /// </summary>
        [JsonProperty("subscribed")]
        public bool? Subscribed { get; set; }

        /// <summary>
        /// Any additional parameters added to the deep link used to access the conversation passed as a string.
        /// </summary>
        /// <remarks>
        /// See deep link section for additional information: https://developers.viber.com/docs/tools/deep-links.
        /// </remarks>
        [JsonProperty("context")]
        public string Context { get; set; }

        /// <summary>
        /// A string describing the failure.
        /// </summary>
        [JsonProperty("desc")]
        public string Description { get; set; }

        /// <summary>
        /// Viber user.
        /// </summary>
        [JsonProperty("sender")]
        public User Sender { get; set; }

        /// <summary>
        /// Message object.
        /// </summary>
        [JsonIgnore]
        public MessageBase Message { get; set; }

        /// <summary>
        /// Message object.
        /// </summary>
        [JsonProperty("message")]
        private JObject message
        {
            set
            {
                var messageType = value.Property("type").Value.ToObject<MessageTypeEnum>();
                Type type;
                switch (messageType)
                {
                    case MessageTypeEnum.Text:
                        type = typeof(TextMessage);
                        break;

                    case MessageTypeEnum.Picture:
                        type = typeof(PictureMessage);
                        break;

                    case MessageTypeEnum.Video:
                        type = typeof(VideoMessage);
                        break;

                    case MessageTypeEnum.File:
                        type = typeof(FileMessage);
                        break;

                    case MessageTypeEnum.Location:
                        type = typeof(LocationMessage);
                        break;

                    case MessageTypeEnum.Contact:
                        type = typeof(ContactMessage);
                        break;

                    case MessageTypeEnum.Sticker:
                        type = typeof(StickerMessage);
                        break;

                    case MessageTypeEnum.CarouselContent:
                        throw new NotImplementedException();
                    case MessageTypeEnum.Url:
                        type = typeof(UrlMessage);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Message = (MessageBase)value.ToObject(type);
            }
        }
    }
}