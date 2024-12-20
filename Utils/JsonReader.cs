using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Util
{
    public class JsonReader
    {       
        public JsonReader() { }
        public string extracData(String token)
        {
            String myJsonString = File.ReadAllText("DataTest.json");
            var jsonObject = JToken.Parse(myJsonString);
            Console.WriteLine(jsonObject.SelectToken(token).Value<string>());
            return jsonObject.SelectToken(token).Value<string>();
        }
    }
 
}