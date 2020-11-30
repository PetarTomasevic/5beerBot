using Newtonsoft.Json;

namespace ViberBot.Model
{
    /// <summary>
    /// Keyboard button frame JSON Object. Draw frame above the background on the button, the size will be equal the size of the button
    /// </summary>
    public class KeyboardButtonFrame
    {
        /// <summary>
        /// optional (api level 6). Width of border
        /// </summary>
        /// <remarks>Possible values: 0..10 (default = 1)</remarks>
        [JsonProperty("BorderWidth")]
        public int BorderWidth { get; set; }

        /// <summary>
        /// optional (api level 6). Color of border
        /// </summary>
        [JsonProperty("BorderColor")]
        public string BorderColor { get; set; }

        /// <summary>
        /// optional (api level 6). The border will be drawn with rounded corners
        /// </summary>
        /// <remarks>Possible values: 0..10 (default = 0)</remarks>
        [JsonProperty("CornerRadius")]
        public int CornerRadius { get; set; }
    }
}