using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ViberBot.Enum
{
    /// <summary>
    ///  Should the browser’s footer will be displayed (default) or not (hidden)
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InternalBrowserFooterTypeEnum
    {
        /// <inheritdoc />
        [EnumMember(Value = "default")]
        Default = 1,

        /// <inheritdoc />
        [EnumMember(Value = "hidden")]
        Hidden = 2,
    }
}