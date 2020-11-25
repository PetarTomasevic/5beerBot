using Newtonsoft.Json;

namespace ViberBot.Service
{
    internal class SendMessageResponse : BaseApiResponse
    {
        /// <summary>
        /// Unique id of the message.
        /// </summary>
        [JsonProperty("message_token")]
        public long MessageToken { get; set; }
    }
}