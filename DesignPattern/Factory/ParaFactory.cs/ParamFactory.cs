using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpBasic.DesignPattern.Factory.ParaFactory
{
    interface IProduct
    {
        void Work();
    }

    class ProductA : IProduct
    {
        public void Work()
        {
            Console.WriteLine("Product A work!");
        }
    }

    class ProductB : IProduct
    {
        public void Work()
        {
            Console.WriteLine("Produc B work!");
        }
    }

    class ProductWork
    {
        protected virtual IProduct CreateProduct(int type)
        {
            IProduct product = null;

            if (type == 1)
            {
                product = new ProductA();
            }
            else if (type == 2)
            {
                product = new ProductB();
            }

            return product;
        }

        public void WorkFrom(int type, string txt)
        {
            IProduct p = CreateProduct(type);

            p.Work();

            Console.WriteLine(txt);
        }
    }

    class ProductC : IProduct
    {
        public void Work()
        {
            Console.WriteLine("Product C work!");
        }
    }

    class ProductWorkEx : ProductWork
    {
        protected override IProduct CreateProduct(int type)
        {
            if (type == 3)
            {
                return new ProductC();
            }
            else
            {
                return base.CreateProduct(type);
            }
        }
    }

    class TestParaFactory : ITest
    {
        public void Test()
        {
            var productWork = new ProductWork();

            productWork.WorkFrom(1, "Good");

            var productWorkEx = new ProductWorkEx();
            productWorkEx.WorkFrom(1, "Good");

            productWorkEx.WorkFrom(3, "Good");
        }
    }
}


namespace CsharpBasic.DesignPattern
{
    using CsharpBasic.DesignPattern.Factory.ParaFactory;

    public partial class Customer
    {
        public void AddParaFactory()
        {
            TestCases.Clear();

            TestCases.Add(new TestParaFactory());
        }
    }
}