using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ViberBot.Enum
{
    /// <summary>
    /// Type of title for internal browser if has no CustomTitle field.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InternalBrowserTitleTypeEnum
    {
        /// <summary>
        /// default means the content in the page’s <OG:title> element or in <title> tag.
        /// </summary>
        [EnumMember(Value = "default")]
        Default = 1,

        /// <summary>
        /// domain means the top level domain.
        /// </summary>
        [EnumMember(Value = "domain")]
        Domain = 2,
    }
}