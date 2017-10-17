using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Dynamic;

namespace BarNone.TheRack.Demo
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {

            var p = new Person
            {
                Name = "Test",
                Age = 55
            };

            var s = JsonConvert.SerializeObject(55);
            //var j = JObject.Parse();
            //Console.WriteLine(j["Age"]);

            Func<int, bool> IsGreaterThen0 = (i) => i > 0;
            Func<int, bool> IsEvent = (i) => i % 2 == 0;

            var r = Func<int, bool>.Combine(IsGreaterThen0, IsEvent);

            int value = 0;
            Console.WriteLine(r.DynamicInvoke(value));
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
