using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyleeIsWrong
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = "The quick    brown dog";
            string r = "";
            char ch = ' ';
            int c = 0;
            for (var i = 0; i < data.Length; i++)  {
                ch = data[i];
                r = r.Insert(c, Convert.ToString(ch));
                c++;
                if (ch == ' ') c = 0;
            }
            Console.WriteLine(r);
            Console.ReadLine();
        }
    }
}
