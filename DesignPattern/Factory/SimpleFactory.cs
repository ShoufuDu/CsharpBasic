using System.IO;
using System;

namespace CsharpBasic.DesignPattern.Factory {
    static public class SimpleFactory {
        public enum ProductType {
            TypeA = 1,
            TypeB,
            TypeC
        }

        public static IProduct CreateProduct (ProductType type) {
            switch (type) {
                case ProductType.TypeA:
                    {
                        return new ProductA ();
                    }
                case ProductType.TypeB:
                    return new ProductB ();
                default:
                    return new ProductA ();
            }
        }

    }

    public interface IProduct {
        void Work ();
    }

    public class ProductA : IProduct {
        private string name;
        public string Name {
            get {
                return "Product A";
            }
            set {
                name = value;
            }
        }

        public void Work () {
            Console.WriteLine ($"{Name} work");
        }
    }

    public class ProductB : IProduct {
        private string name;
        public string Name {
            get {
                return "Product B";
            }
            set {
                name = value;
            }
        }

        public void Work () {
            Console.WriteLine ($"{Name} work");
        }
    }

    public partial class TestFactory {
        public static void TestSimpleFactory () {

            IProduct a = SimpleFactory.CreateProduct (SimpleFactory.ProductType.TypeA);
            a.Work ();

            IProduct b = SimpleFactory.CreateProduct (SimpleFactory.ProductType.TypeB);
            b.Work ();

        }
    }
}