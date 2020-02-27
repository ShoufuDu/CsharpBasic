using System;

namespace CsharpBasic.DesignPattern.Factory
{
    public partial class TestFactory : CsharpBasic.Test.ITest
    {
        public void Test() {
            TestMyFactory();
            TestSimpleFactory();
            TestAbstractFactory();
        }
    }
}