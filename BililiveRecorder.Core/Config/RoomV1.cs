using Newtonsoft.Json;

namespace BililiveRecorder.Core.Config
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class RoomV1
    {
        [JsonProperty("id")]
        public int Roomid { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("notify")]
        public bool Notify { get; set; }

        [JsonProperty("fav")]
        public bool Fav { get; set; }
    }
}
