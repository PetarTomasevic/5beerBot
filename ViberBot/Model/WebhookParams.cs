using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using ViberBot.Enum;

namespace ViberBot.Model
{
    public class WebhookParams
    {
        public WebhookParams(String callbackUrl)
        {
            if (callbackUrl == null)
                throw new ArgumentNullException(nameof(callbackUrl));

            CallbackUrl = callbackUrl;
        }

        [JsonProperty("url")]
        public String CallbackUrl { get; }

        [JsonProperty("event_types")]
        public IEnumerable<EventTypeEnum> EventTypes { get; set; }

        [JsonProperty("send_name")]
        public Boolean SendUserName { get; set; }

        [JsonProperty("send_photo")]
        public Boolean SendUserPhoto { get; set; }
    }
}