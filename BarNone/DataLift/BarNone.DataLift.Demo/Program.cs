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
            var r = await TokenManager.Authorize("admin", "adminf");
            Console.WriteLine(r);

            var g = await DataManager.Lifts.GetAll();
            Console.WriteLine(g);

            Console.ReadLine();
        }
    }
}
