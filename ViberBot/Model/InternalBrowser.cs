using Newtonsoft.Json;
using System;
using ViberBot.Enum;

namespace ViberBot.Model
{
    /// <summary>
    /// Internal browser object.
    /// </summary>
    public class InternalBrowser
    {
        /// <summary>
        /// optional (api level 3). Action button in internal’s browser navigation bar.
        /// </summary>
        [JsonProperty("ActionButton")]
        public InternalBrowserActionButtonEnum? ActionButton { get; set; }

        /// <summary>
        /// optional (api level 3). If ActionButton is send or forward then the value from this property will be used to be sent as message, otherwise ignored
        /// </summary>
        [JsonProperty("ActionPredefinedURL")]
        public string ActionPredefinedURL { get; set; }

        /// <summary>
        /// optional (api level 3). Type of title for internal browser if has no CustomTitle field. default means the content in the page’s <OG:title> element or in <title> tag. domain means the top level domain
        /// </summary>
        ///
        [JsonProperty("TitleType")]
        public InternalBrowserTitleTypeEnum? TitleType { get; set; }

        private string customTitle;

        /// <summary>
        /// optional (api level 3). Custom text for internal’s browser title, TitleType will be ignored in case this key is presented
        /// </summary>
        /// <remarks>Max.length 15 chars</remarks>
        [JsonProperty("CustomTitle")]
        public string CustomTitle { get => customTitle; set => customTitle = (value == null ? null : value.Substring(0, Math.Min(value.Length, 15))); }

        /// <summary>
        /// optional (api level 3). Indicates that browser should be opened in a full screen or in partial size (50% of screen height). Full screen mode can be with orientation lock (both orientations supported, only landscape or only portrait)
        /// </summary>
        [JsonProperty("Mode")]
        public InternalBrowserModeEnum? Mode { get; set; }

        /// <summary>
        /// optional (api level 3). Should the browser’s footer will be displayed (default) or not (hidden)
        /// </summary>
        [JsonProperty("FooterType")]
        public InternalBrowserFooterTypeEnum? FooterType { get; set; }

        /// <summary>
        /// optional (api level 6). Custom reply data for send-to-bot action that will be resent in msgInfo
        /// </summary>
        [JsonProperty("ActionReplyData")]
        public string ActionReplyData { get; set; }
    }
}