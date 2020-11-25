using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using ViberBot.Enum;

namespace ViberBot.Model
{
    /// <summary>
    /// URL message
    /// </summary>
    public class UrlMessage : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlMessage"/> class.
        /// </summary>
        public UrlMessage()
            : base(MessageTypeEnum.Url)
        {
        }

        /// <summary>
        /// URL, max 2000 characters.
        /// </summary>
        [JsonProperty("media")]
        public string Media { get; set; }
    }
}