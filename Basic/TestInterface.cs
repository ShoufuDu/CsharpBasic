using System;
using System.Collections.Generic;
using System.Text;

using CsharpBasic.Basic.Interface;

namespace CsharpBasic.Basic.Test
{
    class InterfaceTest : ITest
    {
        public void Api()
        {
            Console.WriteLine("Api");
        }

        public ITest Get()
        {
            return new InterfaceTest();
        }
    }

}