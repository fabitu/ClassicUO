using ClassicUO.Game.GameObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicUO.Custom.Model
{
    public class CustomItens
    {
        [JsonProperty(Order = 0)]
        public string Type { get; set; }
        [JsonProperty(Order = 1)]
        public string Description { get; set; }
        [JsonProperty(Order = 2)]
        public string Flags { get; set; }
        [JsonProperty(Order = 3)]
        public ushort ReplaceToGraphic { get; set; }
        [JsonProperty(Order = 4)]
        public List<ushort> ToReplaceGraphicArray = [];
    }
}
