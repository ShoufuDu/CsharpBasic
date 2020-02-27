using System;

namespace CsharpBasic.Basic
{
    public class OOTest
    {
        public int A
        {
            set
            {
                _a = value;
            }
            get
            {
                return _a;
            }
        }
        public int _a;

        public OOTest()
        {
            _a = 0;
        }

        public OOTest(int a)
        {
            _a = a;
        }

        public virtual void F_Virtual()
        {
        }

        public static void F_Static() { }

        public virtual void Test()
        {
            OOTest o = new OOTest();
            Console.WriteLine($"GetType={o.GetType()}");
            Console.WriteLine($"GetHashCode={o.GetHashCode()}");
        }
    }

    public abstract class OOTestAbstract
    {
        public abstract void F_Abstract();
        public virtual void F_Virtual()
        {

        }

        public void F_Common() { }

        public abstract int A { set; get; }
    }

    interface OOTestInterface
    {
        int A { set; get; }
        void F_Common();
    }

    public class OOTestChild : OOTest, Test.ITest
    {
        private int _b = 0;
        public OOTestChild(int a) : base(a)
        {
            _b = a;
        }

        public override void Test()
        {
            var o = new OOTestChild(5);
        }
    }
}