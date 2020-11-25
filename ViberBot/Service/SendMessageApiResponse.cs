using Newtonsoft.Json;
using System;

namespace ViberBot.Service
{
    internal sealed class SendMessageApiResponse : BaseApiResponse
    {
        /// <summary>
        /// Unique ID of the message
        /// </summary>
        [JsonProperty("message_token")]
        public Int64 MessageToken { get; set; }
    }
}