using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpBasic.DesignPattern.Test
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =true)]
    public class TestCaseAttribute : Attribute
    {
        public bool Enabled { get; private set; }

        public TestCaseAttribute(bool enabled = true)
        {
            Enabled = enabled;
        }
    }
}
