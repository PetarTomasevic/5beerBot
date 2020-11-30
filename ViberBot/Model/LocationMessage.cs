using Newtonsoft.Json;
using ViberBot.Enum;

namespace ViberBot.Model
{
    /// <summary>
    /// Location message
    /// </summary>
    public class LocationMessage : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationMessage"/> class.
        /// </summary>
        public LocationMessage()
            : base(MessageTypeEnum.Location)
        {
        }

        /// <summary>
        /// Location data.
        /// </summary>
        [JsonProperty("location")]
        public Location Location { get; set; }
    }
}