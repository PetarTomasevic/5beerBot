using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ViberBot.Enum
{
    /// <summary>
    /// Action button in internal’s browser navigation bar.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InternalBrowserActionButtonEnum
    {
        /// <summary>
        /// forward (default) - will open the forward via Viber screen and share current URL or predefined URL.
        /// </summary>
        [EnumMember(Value = "forward")]
        Forward = 1,

        /// <summary>
        /// send - sends the currently opened URL as an URL message, or predefined URL if property ActionPredefinedURL is not empty.
        /// </summary>
        [EnumMember(Value = "send")]
        Send = 2,

        /// <summary>
        /// open-externally - opens external browser with the current URL.
        /// </summary>
        [EnumMember(Value = "open-externally")]
        OpenExternally = 3,

        /// <summary>
        /// send-to-bot - (api level 6) sends reply data in msgInfo to bot in order to receive message.
        /// </summary>
        [EnumMember(Value = "send-to-bot")]
        SendToBot = 4,

        /// <summary>
        /// none - will not display any button.
        /// </summary>
        [EnumMember(Value = "none")]
        None = 5,
    }
}