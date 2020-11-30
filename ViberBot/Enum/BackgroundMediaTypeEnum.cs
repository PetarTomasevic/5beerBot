using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ViberBot.Enum
{
    /// <summary>
    /// Type of the background media.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BackgroundMediaTypeEnum
    {
        /// <summary>
        /// JPEG and PNG files.
        /// </summary>
        [EnumMember(Value = "picture")]
        Picture = 1,

        /// <summary>
        /// GIF files.
        /// </summary>
        [EnumMember(Value = "gif")]
        Gif = 2
    }
}