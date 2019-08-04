using System;
namespace CsharpBasic.Basic {
    public class DelegateTest {
        public Action<string> myAction;

        public void CallAction (string s) {
            myAction (s);
        }

        public void CallInvoke (string s) {
            myAction.Invoke (s);
        }
    }

    public class DelegateTest1 {

        public void TestInvoke () {
            DelegateTest a = new DelegateTest ();

            a.myAction += Test1;

            a.CallAction ("Call Action directly");

            a.CallAction ("Call Action directly");

            a.myAction += Test2;
            a.CallAction ("what about +");
        }

        public void TestPlus () {
            DelegateTest a = new DelegateTest();
            a.myAction += Plus1;
            a.myAction += Plus2;
            a.myAction += Plus3;

            a.CallAction("Plus test");
        }

        private void Test1 (string s) {
            Console.WriteLine ($"This is a Invoke test with {s}");
        }

        private void Test2 (string s) {
            Console.WriteLine ($"This is a + test with {s}");
        }

        private void Plus1 (string s) {
            Console.WriteLine ($"Plus1 {s}");
        }

        private void Plus2 (string s) {
            Console.WriteLine ($"Plus2 {s}");
        }

        private void Plus3 (string s) {
            Console.WriteLine ($"Plus3 {s}");
        }
    }
}