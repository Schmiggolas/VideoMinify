using System.Linq.Expressions;
using Newtonsoft.Json;

namespace VideoMinify
{
    public class Config
    {
        [JsonProperty("suffix")]
        public string Suffix { get; set; } = "_minified";
        [JsonProperty("use_multithreading")]
        public bool UseMultithreading { get; set; } = true;
        [JsonProperty("constant_rate_factor")]
        public int ConstantRateFactor { get; set; } = 28;
    }
}