using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ViberBot.Enum
{
    /// <summary>
    /// Determine the url media type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpenUrlMediaTypeEnum
    {
        /// <summary>
        /// Force browser usage.
        /// </summary>
        [EnumMember(Value = "not-media")]
        NotMedia = 1,

        /// <summary>
        /// Will be opened via media player.
        /// </summary>
        [EnumMember(Value = "video")]
        Video = 2,

        /// <summary>
        /// Client will play the gif in full screen mode.
        /// </summary>
        [EnumMember(Value = "gif")]
        Gif = 3,

        /// <summary>
        /// Client will open the picture in full screen mode.
        /// </summary>
        [EnumMember(Value = "picture")]
        Picture = 4,
    }
}