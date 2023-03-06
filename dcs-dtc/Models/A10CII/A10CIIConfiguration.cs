using DTC.Models.A10CII.Waypoints;
using DTC.Models.Base;
using Newtonsoft.Json;

namespace DTC.Models.A10CII
{
    public class A10CIIConfiguration : IConfiguration
    {
        public WaypointSystem Waypoints = new WaypointSystem();
        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }

        public string ToCompressedString()
        {
            var json = ToJson();
            return StringCompressor.CompressString(json);
        }
        public static A10CIIConfiguration FromJson(string s)
        {
            try
            {
                var cfg = JsonConvert.DeserializeObject<A10CIIConfiguration>(s);
                cfg.AfterLoadFromJson();
                return cfg;
            }
            catch
            {
                return null;
            }
        }
        public void AfterLoadFromJson()
        {

        }

        public static A10CIIConfiguration FromCompressedString(string s)
        {
            try
            {
                var json = StringCompressor.DecompressString(s);
                var cfg = FromJson(json);
                return cfg;
            }
            catch
            {
                return null;
            }
        }

        public void CopyConfiguration(A10CIIConfiguration cfg)
        {
            if (cfg.Waypoints != null)
            {
                Waypoints = cfg.Waypoints;
            }
        }

        public A10CIIConfiguration Clone()
        {
            var json = ToJson();
            var cfg = FromJson(json);
            return cfg;
        }

        IConfiguration IConfiguration.Clone()
        {
            return Clone();
        }
    }
}