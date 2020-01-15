using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpBasic.DesignPattern.Test
{
    public partial class Customer
    {
        private List<ITest> TestCases = new List<ITest>();

         public void Execute()
        {
            foreach(var tc in TestCases)
            {
                tc.Test();
            }
        }
    }
}
