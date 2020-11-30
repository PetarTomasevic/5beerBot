using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ViberBot.Enum
{
    /// <summary>
    /// Indicates that browser should be opened in a full screen or in partial size (50% of screen height).
    /// Full screen mode can be with orientation lock (both orientations supported, only landscape or only portrait)
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InternalBrowserModeEnum
    {
        /// <inheritdoc />
        [EnumMember(Value = "fullscreen")]
        Fullscreen = 1,

        /// <inheritdoc />
        [EnumMember(Value = "fullscreen-portrait")]
        Portrait = 2,

        /// <inheritdoc />
        [EnumMember(Value = "fullscreen-landscape")]
        Landscape = 3,

        /// <inheritdoc />
        [EnumMember(Value = "partial-size")]
        Partial = 4,
    }
}