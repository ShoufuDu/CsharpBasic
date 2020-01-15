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

    public class StaticConstructor
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

        public void Show()
        {
            Console.WriteLine($"count is {count}");
        }
    }
}


namespace CsharpBasic.DesignPattern.Test
{
    using CsharpBasic.Basic;

    class TestStaticConstructor : ITest
    {
        public void Test()
        {
            var obj = new StaticConstructor();

            obj.Show();
        }
    }

    public partial class Customer
    {
        [TestCase(false)]
        public void TestStaticConstructor()
        {
            TestCases.Add(new TestStaticConstructor());
        }
    }
}