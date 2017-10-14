using BarNone.DataLift.APIRequest;
using BarNone.Shared.DataTransfer.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
            Console.ReadLine();
        }

        static async Task Run()
        {
            var auth = await TokenManager.Authorize("admin", "admin");
            Console.WriteLine(auth.Access_Token);
            var users = await DataManager.Users.GetAll();
            users.ForEach(u => Console.WriteLine(u.UserName));

            var nu = await DataManager.Users.Post(new Shared.DataTransfer.UserDTO
            {
                Name = "Test",
                UserName = "Test444",
                Password = "123"
            });
            Console.WriteLine(nu.ID);
            Console.ReadLine();
        }
    }
}
