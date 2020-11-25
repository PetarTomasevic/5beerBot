using Newtonsoft.Json;
using System;
using ViberBot.Enum;

namespace ViberBot.Model
{
    public abstract class MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBase"/> class.
        /// </summary>
        /// <param name="type">Message type.</param>
        /// , UserInfo sender, String receiverId)
        protected MessageBase(MessageTypeEnum type/*, UserBase sender, String receiverId*/)
        {
            Type = type;
            //Sender = sender;
            //Receiver = receiverId;
        }

        /// <summary>
        /// Unique Viber user id.
        /// </summary>
        [JsonProperty("receiver")]
        public string Receiver { get; set; }

        /// <summary>
        /// Message type.
        /// </summary>
        [JsonProperty("type")]
        public MessageTypeEnum Type { get; }

        /// <summary>
        /// Sender of the message.
        /// </summary>
        [JsonProperty("sender")]
        public User Sender { get; set; }

        /// <summary>
        /// Allow the account to track messages and user’s replies. Sent tracking_data value will be passed back with user’s reply.
        /// </summary>
        [JsonProperty("tracking_data")]
        public string TrackingData { get; set; }

        /// <summary>
        /// Minimal API version required by clients for this message (default 1).
        /// </summary>
        [JsonProperty("min_api_version")]
        public double? MinApiVersion { get; set; }

        [JsonIgnore]
        public Boolean IsBroadcasting { get; }
    }
}