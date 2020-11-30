using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ViberBot.Enum
{
    /// <summary>
    /// Determine the open-url action result, in app or external browser.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpenUrlTypeEnum
    {
        /// <summary>
        /// Type 'internal'.
        /// </summary>
        [EnumMember(Value = "internal")]
        Internal = 1,

        /// <summary>
        /// Type 'external'.
        /// </summary>
        [EnumMember(Value = "external")]
        External = 2
    }
}