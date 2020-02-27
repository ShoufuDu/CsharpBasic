using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpBasic.Test
{
    public partial class Customer
    {
        public static Customer Instance = new Customer();

        public List<ITest> TestCases = new List<ITest>();

         public void Execute()
        {
            foreach(var tc in TestCases)
            {
                tc.Test();
            }
        }
    }
}
