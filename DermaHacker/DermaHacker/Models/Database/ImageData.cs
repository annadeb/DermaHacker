using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DermaHacker.Models.Database
{
    public class ImageData
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "dimension")]
        public double[] coordinateXY { get; set; }

        [JsonProperty(PropertyName = "base64")]
        public string Base64 { get; set; }

        [JsonProperty(PropertyName = "status")]
        public bool Status { get; set; }

       // public Guid guid = new Guid();

      
    }
}