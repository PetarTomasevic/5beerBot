using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using ViberBot.Enum;

namespace ViberBot.Model
{
    public class ContactMessage : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactMessage"/> class.
        /// </summary>
        public ContactMessage()
            : base(MessageTypeEnum.Contact)
        {
        }

        /// <summary>
        /// Contact object.
        /// </summary>
        [JsonProperty("contact")]
        public Contact Contact { get; set; }
    }
}