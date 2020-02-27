using System;
using System.Collections.Generic;
using System.Text;


namespace CsharpBasic.Test
{
    class TestDictionary : ITest
    {
        public void Test()
        {
            var dic = new Dictionary<string, int>();

            dic["a"] = 15;

            Console.WriteLine($"key:a, value:{dic["a"]}");

            dic["a"] = 16;
            Console.WriteLine($"key:a, value:{dic["a"]}");
        }
    }

    public partial class Customer
    {
        [Test(false)]
        public void TestDictionary()
        {
            TestCases.Add(new TestDictionary());
        }
    }
}
