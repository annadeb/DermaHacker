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

        [JsonProperty(PropertyName = "coordinateXY")]
        public int[] CoordinateXY { get; set; }


        [JsonProperty(PropertyName = "matlab")]
        public FromMatlab Matlab { get; set; }

        [JsonProperty(PropertyName = "base64")]
        public string Base64 { get; set; }

        [JsonProperty(PropertyName = "status")]
        public bool Status { get; set; }

       // public Guid guid = new Guid();

      
    }
    public class FromMatlab
    {

        public string Width { get; set; }

        public string Height { get; set; }

        public string Arena { get; set; }
        public string MatrixC1 { get; set; }
        public string MatrixC2 { get; set; }
        public string MatrixC3 { get; set; }
    }
}