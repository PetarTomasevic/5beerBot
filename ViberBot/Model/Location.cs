using Newtonsoft.Json;

namespace ViberBot.Model
{
    public class Location
    {
        /// <summary>
        /// Longitude of the <see cref="Location"/>.
        /// </summary>
        [JsonProperty("lon")]
        public double Lon { get; set; }

        /// <summary>
        /// Latitude of the <see cref="Location"/>.
        /// </summary>
        [JsonProperty("lat")]
        public double Lat { get; set; }
    }
}