using BarNone.DataLift.APIRequest;
using BarNone.Shared.DataTransfer.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.Demo
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public object Value { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Run();
            Console.ReadLine();
        }

        static async Task Run()
        {

            //var p = new Person
            //{
            //    Age = 55
            //};

            //var serializer = new JsonSerializer()
            //{
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //};

            //var j = JObject.FromObject(p, serializer);

            var p = JsonConvert.DeserializeObject<Person>("{\"value\": 55}");

            //Console.WriteLine(p.Age);
            //Console.WriteLine(j["age"]);

            //dynamic d = 55;
            Console.WriteLine(p.Value.GetType());

            Console.ReadLine();
        }
    }
}
