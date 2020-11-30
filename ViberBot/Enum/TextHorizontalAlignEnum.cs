using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ViberBot.Enum
{
    /// <summary>
    /// Horizontal align of the text.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TextHorizontalAlignEnum
    {
        /// <summary>
        /// Align 'left'.
        /// </summary>
        [EnumMember(Value = "left")]
        Left = 1,

        /// <summary>
        /// Align 'center'.
        /// </summary>
        [EnumMember(Value = "center")]
        Center = 2,

        /// <summary>
        /// Align 'right'.
        /// </summary>
        [EnumMember(Value = "right")]
        Right = 3
    }
}