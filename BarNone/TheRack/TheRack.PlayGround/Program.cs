using System;
using System.Collections.Generic;

namespace TheRack.PlayGround
{

    class Program
    {
        static void Main(string[] args)
        {
            var data = new Dictionary<int, string>();
            data[1] = "test1";
            data[2] = "test2";
            data.Add(1, "test3");
            Console.ReadLine();
        }
    }
}
