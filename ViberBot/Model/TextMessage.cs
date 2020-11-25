using Newtonsoft.Json;
using ViberBot.Enum;

namespace ViberBot.Model
{
    /// <summary>
    /// Text message
    /// </summary>
    public class TextMessage : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextMessage"/> class.
        /// </summary>
        public TextMessage()
            : base(MessageTypeEnum.Text)
        {
        }

        /// <summary>
        /// The text of the message.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}