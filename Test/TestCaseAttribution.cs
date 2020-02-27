using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpBasic.Test
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =true)]
    public class TestAttribute : Attribute
    {
        public bool Enabled { get; private set; }

        public TestAttribute(bool enabled = true)
        {
            Enabled = enabled;
        }
    }
}
