using Newtonsoft.Json;
using ViberBot.Enum;

namespace ViberBot.Model
{
    public class StickerMessage : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StickerMessage"/> class.
        /// </summary>
        public StickerMessage()
            : base(MessageTypeEnum.Sticker)
        {
        }

        /// <summary>
        /// Unique Viber sticker ID.
        /// </summary>
        [JsonProperty("sticker_id")]
        public string StickerId { get; set; }
    }
}