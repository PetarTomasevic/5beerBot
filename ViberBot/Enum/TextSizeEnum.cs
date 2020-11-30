using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ViberBot.Enum
{
    /// <summary>
    /// Text size.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TextSizeEnum
    {
        /// <summary>
        /// Size 'small'.
        /// </summary>
        [EnumMember(Value = "small")]
        Small = 1,

        /// <summary>
        /// Size 'regular'.
        /// </summary>
        [EnumMember(Value = "regular")]
        Regular = 2,

        /// <summary>
        /// Size 'medium'.
        /// </summary>
        [EnumMember(Value = "medium")]
        Medium = 3,

        /// <summary>
        /// Size 'large'.
        /// </summary>
        [EnumMember(Value = "large")]
        Large = 4
    }
}