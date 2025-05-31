using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lib_dominio.Nucleo
{
    public static class JsonConversor
    {
        
        public static Dictionary<string, object> ConvertirAObjeto(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return new Dictionary<string, object>();

            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            var values2 = new Dictionary<string, object>();

            foreach (var item in values!)
            {
                if (item.Value is JObject jObject)
                    values2.Add(item.Key, ConvertirAObjeto(jObject.ToString()));
                else
                    values2.Add(item.Key, item.Value!);
            }

            return values2;
        }

       
        public static string ConvertirAString(object data, bool ignore = true)
        {
            return JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ignore ? ReferenceLoopHandling.Ignore : ReferenceLoopHandling.Error,
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        
        public static T ConvertirAObjeto<T>(string data, bool cleanQuotes = false)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException("El dato JSON de entrada está vacío o nulo.");

            if (cleanQuotes)
                data = data.Replace("\"", "");

            return JsonConvert.DeserializeObject<T>(data)!;
        }
    }
}
