using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpBasic.Basic
{
    public interface ExplicitInterface
    {
        int Sign { get; }
    }

    class ExplicitInterfaceTest : ExplicitInterface
    {
        int ExplicitInterface.Sign
        {
            get
            {
                return 5;
            }
        }
    }

    public interface IA
    {
        void Show();
    }

    public interface IB
    {
        void Show();
    }

    public abstract class AB
    {
        public abstract void Show1();
    }

    public class D : AB, IA, IB, CsharpBasic.Test.ITest
    {
        public void Show()
        {
            Console.WriteLine("concrete show");
        }

        void IA.Show()
        {
            Console.WriteLine("implement IA");
        }

        void IB.Show()
        {
            Console.WriteLine("implement IB");
        }

        public override void Show1()
        {
            Console.WriteLine("D implement AB class");
        }

        public void Test()
        {
            var test = new ExplicitInterfaceTest();

            if (test is ExplicitInterface ti)
            {
                Console.WriteLine(ti.Sign);
            }

            var d = new D();
            d.Show();

            ((IA)d).Show();

            ((IB)d).Show();

            var day = new DateTime(2020, 1, 29);
            var day1 = day.AddMonths(1);
            var day2 = day.AddMonths(2);

            var day3 = day.AddDays(100);
        }
    }

}
