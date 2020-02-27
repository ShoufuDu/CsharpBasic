using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpBasic.Basic
{
    class FlagClass1
    {
        public FlagClass1()
        {
            Console.WriteLine("FlagClass1 created");
        }
    }

    class FlagClass2
    {
        public FlagClass2()
        {
            Console.WriteLine("FlagClass2 created");
        }
    }

    public class StaticConstructor : CsharpBasic.Test.ITest
    {
        private static int count = 2;

        private static FlagClass1 FlagClass1 = new FlagClass1();

        private static FlagClass2 FlagClass2;

        public string Name { set; get; }

        static StaticConstructor()
        {
            count = 1;

            FlagClass2 = new FlagClass2();
        }

        public void Test()
        {
            var obj = new StaticConstructor();

            obj.Show();

            Console.WriteLine($"A.X={A.X}, B.Y={B.Y}");

            Console.WriteLine($"Time is {DateTime.Now.ToString("yyyy-MM-dd")}");
        }

        public void Show()
        {
            Console.WriteLine($"count is {count}");
        }
    }

    class A
    {
        public static int X;

        static A()
        {
            Console.WriteLine("Call A static constructor");

            Console.WriteLine($"B.Y={B.Y}");

            X = B.Y + 1;
        }
    }

    class B
    {
        public static int Y;

        static B()
        {
            Console.WriteLine("Call B static constructor");

            Console.WriteLine($"A.x={A.X}");

            Y = A.X + 1;
        }
    }
}
