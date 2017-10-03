using System;
using System.Collections.Generic;
using static TheRack.PlayGround.FTypes;

namespace TheRack.PlayGround
{
    class Data
    {
        public int I { get; set; }
    }

    class FTypes
    {
        public delegate dynamic GetStuff<T>(T data);
        public delegate void DoStuff<P>(P data, dynamic value);
    }

    abstract class BaseCaller<T, P>
    {
        

        public BaseCaller(/*Dictionary<string, GetStuff> G, Dictionary<string, DoStuff> S*/)
        {
            //this.G = G;
            //this.S = S;
        }

        public abstract Dictionary<string, GetStuff<T>> G { get; }
        public abstract Dictionary<string, DoStuff<P>>  S { get; }

        public void Do(T t, P p)
        {
            var v = G["ID"](t);
            dynamic d = 123;
            S["ID"](p, d);
        }
    }

    class Caller : BaseCaller<Data, Data>
    {
        
        public Caller()
        {

        }

        public override Dictionary<string, GetStuff<Data>> G
        {
            get
            {
                return new Dictionary<string, GetStuff<Data>>
                {
                    ["ID"] = (data) => data.I
                };
            }
        }
        public override Dictionary<string, DoStuff<Data>> S
        {
            get
            {
                return new Dictionary<string, DoStuff<Data>>
                {
                    ["ID"] = (data, value) => Console.WriteLine(value)
                };
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Caller c = new Caller();
            Data t = new Data();
            Data p = new Data();
            c.Do(t, p);
            
            Console.ReadLine();
        }
    }
}
