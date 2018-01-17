using BarNone.DataLift.APIRequest;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Auth;
using BarNone.Shared.DataTransfer.Flex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
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
            await TokenManager.Authorize("admin", "admin");
            var data = File.ReadAllBytes(@"C:\VideoDB\Demo1.mp4");
            var r = await DataManager.Flex.Post(new FlexDTO
            {
                Entities = new List<FlexEntityDTO>
                {
                    new FlexEntityDTO
                    {
                        Resource = "LIFT",
                        Entity = new LiftDTO
                        {
                            Name = "NewTest",
                            ParentID = 1,
                            BodyDataID = 55,
                            Details = new LiftDetailDTO
                            {
                                Video = new VideoDTO
                                {
                                    Data = data
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}
