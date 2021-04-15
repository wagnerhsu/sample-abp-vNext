using System.Net.Http.Json;
using Newtonsoft.Json;

namespace BasicAspNetCoreApplication.Infrastructure
{
    public static class ObjectExtensions
    {
        public static string AsJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        
    }
}