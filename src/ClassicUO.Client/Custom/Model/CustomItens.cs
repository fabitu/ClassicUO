using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClassicUO.Custom.Model
{
    public class CustomItens
    {
        [JsonPropertyOrder(0)]
        public string Type { get; set; }
        [JsonPropertyOrder(1)]
        public string Description { get; set; }
        [JsonPropertyOrder(2)]
        public string Flags { get; set; }
        [JsonPropertyOrder(3)]
        public ushort ReplaceToGraphic { get; set; }
        [JsonPropertyOrder(4)]
        public List<ushort> ToReplaceGraphicArray = [];
    }
}
