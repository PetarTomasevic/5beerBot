using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using ViberBot.Enum;

namespace ViberBot.Service
{
    internal abstract class BaseApiResponse
    {
        /// <summary>
        /// Action result
        /// </summary>
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusCodeEnum Status { get; set; }

        /// <summary>
        /// Ok or failure reason
        /// </summary>
        [JsonProperty("status_message")]
        public String StatusMessage { get; set; }
    }
}