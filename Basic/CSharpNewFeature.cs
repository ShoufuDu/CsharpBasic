using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpBasic.Basic
{
    public class CSharpNewFeature
    {
        public int DefaultProperty { set; get; } = 1;

        public void Test()
        {
            Console.WriteLine($"DefaultProperty is {DefaultProperty}");

            Console.WriteLine("set value to 100");

            DefaultProperty = 100;

            Console.WriteLine($"DefaultProperty is {DefaultProperty}");
        }
    }
}
