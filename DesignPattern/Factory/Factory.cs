using System;

/* #region  Factory 1*/
// Generally, Factory doesn't return the Product Objects to clients,
// so Clients just generate concrete Factory to call the concrete Product's methods;

// Another way to use Factory is to return a Product object, which is composed of a few concrete other Products;
// Like Factory2 below

namespace CsharpBasic.DesignPattern.Factory {
    /* #region  Factory */
    public abstract class Factory1 {
        protected abstract IProductX Create ();
        virtual public void Work () {
            IProductX p = Create ();
            p.Work ();
        }

    }

    public class Factory1_A : Factory1 {
        protected override IProductX Create () {
            return new ProductX_A ();
        }
    }

    public class Factory1_B : Factory1 {
        protected override IProductX Create () {
            return new ProductX_B ();
        }
    }
    /* #endregion */

    /* #region  Product */
    public interface IProductX {
        void Work ();
    }

    public class ProductX_A : IProductX {
        public void Work () {
            Console.WriteLine ("ProductX_A work");
        }
    }

    public class ProductX_B : IProductX {
        public void Work () {
            Console.WriteLine ("ProductX_B work");
        }
    }
    /* #endregion */

    /* #region  Test */
    public static partial class TestFactory {
        public static void TestMyFactory () {

            Factory1_A a = new Factory1_A ();
            a.Work ();

            Factory1_B b = new Factory1_B ();
            b.Work ();

        }
    }
    /* #endregion */

}
/* #endregion */