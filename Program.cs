using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Diagnostics;
using System.Text;
using System;
using System.Linq.Expressions;
using CsharpBasic.DesignPattern.Factory;
using System.Collections.Generic;
using CsharpBasic.Basic;

using CsharpBasic.Basic.Interface;
using CsharpBasic.Basic.Test;

using CsharpBasic.DesignPattern.Test;
using System.Reflection;

namespace CsharpBasic {
    class Program {

        static void ExecuteTest()
        {
            var methods = typeof(Customer).GetMethods();

            var customer = new Customer();

            Console.WriteLine("Customer Test start...");

            foreach (var m in methods)
            {
                var attributes = m.GetCustomAttributes(typeof(TestCaseAttribute),false);

                foreach(var attr in attributes)
                {
                    var a = attr as TestCaseAttribute;
                    if(a.Enabled)
                    {
                        m.Invoke(customer, null);
                    }
                }
            }

            customer.Execute();

            Console.WriteLine("Customer Test end");

            return;
        }
         
        static void Main (string[] args) {
            AppDomain app = AppDomain.CurrentDomain;
            app.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            // TestAction ();

            // TestDelegate ();

            // TestFunc ();

            // LambadaExprssion.Test ();

            // TestExpression ();

            // TestPropertyInit ();

            // TestFactory.TestAll ();

            // TestReverseAndCapitalise();

            // LinqTest.Test1();

            //LinqTest.TestCallRecords();

            // DelegateTest1 dt = new DelegateTest1();
            // dt.TestInvoke();
            // dt.TestPlus();

            // // TestJaggedArray();

            // SeriableTest st = new SeriableTest();

            // st.TestSerialize();
            // st.TestDeserialize();

            // TestGarbageCollector();

            // OOTest.Test();

            //ExceptionTest.Test();

            // ExceptionTest.Test1();

            // LockTest lt = new LockTest();
            // lt.MainThreadsTest();

            // ExLockTest lt = new ExLockTest();
            // lt.MainThreadsTest();

            // Ex1LockTest.Test();

            // FileTest.TestDir();

            // FileTest.TestRW();

            // TestStructAndClass();

            //var featureTest = new CSharpNewFeature();
            //featureTest.Test();

            //Customer.ExecuteTest();

            //TestReflection.Test();

            ExecuteTest();
        }



        private static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine($"Exception:{e.Message}, Programm is terminating:{args.IsTerminating}");
        }

    static void TestJaggedArray()
    {
        int[][] a = new int[5][];

        a[0] = new int[5]{5,6,7,8,9};

        a[1] = new int[3]{1,2,3};

        a[2] = new int[2]{1,3};

        a[3] = new int[3]{2,3,5};

        a[4] = new int[4]{1,2,3,4};

        Console.WriteLine($"a.Rank={a.Rank}");
        for(int i=0;i<a.Length;i++)
            {
                Console.WriteLine($"i={i}");
                for(int j=0;j<a[i].Length;j++)
                    Console.Write($"{a[i][j]} ");
                Console.WriteLine();
            }

        int[,] b = new int[4,5];
        Console.WriteLine($"b.Rank={b.Rank}");
}


static void spicyChicken(int min, int max) {

        if (max < min)
            return;

        string[] str = new string[3]{"Spicy","Chicken","SpicyChicken!"};

        for(int i=min;i<=max;i++)
        {
            if(i%3==0)
                Console.WriteLine(str[0]);
            else if (i%5 == 0&&i%15!=0)
                Console.WriteLine(str[1]);
            else if( i%15==0)
                Console.WriteLine(str[2]);
            else
                Console.WriteLine();
        }

}

// public readonly char[] symArray= new char[]{'!',',','.','?'};

public readonly char[] symArray = {'!',',','.','?'};


static void TestReverseAndCapitalise()
{
    const string test        = "Hey there, Alan";
            const string testOut     = "Nala, ereht Yeh";

            string outstr = reverseAndCapitalise(test);

            Debug.Assert(String.Compare(outstr,testOut)==0);

            Console.WriteLine(outstr);
}

static string reverseAndCapitalise(string sentenceIn) {
            HashSet<char> sym = new HashSet<char>();
            sym.Add('!');
            sym.Add(',');
            sym.Add('.');
            sym.Add('?');

            StringBuilder output = new StringBuilder();

            int i = sentenceIn.Length-1;

            sentenceIn = sentenceIn.ToLower();

            int index = -1;

            int lastBlank = -1;
            while(i>=0)
            {
                index++;

                if (i==sentenceIn.Length-1)
                    {
                        output.Append(sentenceIn[i].ToString().ToUpper());
                        i--;
                        continue;
                    }

                if(output[output.Length-1] ==' '&&sym.Contains(sentenceIn[i]))
                {
                    char temp = output[output.Length-1];
                    output[output.Length-1] = sentenceIn[i];
                    output.Append(temp);
                    lastBlank = index;
                    i--;
                    continue;
                }

                output.Append(sentenceIn[i]);
                if(sentenceIn[i]==' ')
                    lastBlank = index;
                i--;
            }

            output[lastBlank+1]= Convert.ToChar(output[lastBlank+1].ToString().ToUpper());

            return output.ToString();

    }

        /* #region  GabageCollector */
        public class A {
            private B _b;
            public A () {
                _b = new B (this);
            }

            ~A()
            {
                Console.WriteLine("A destructed");
            }
        }

        public class B {
            private A _a;
            public B (A a) {
                _a = a;
            }

            ~ B(){
                Console.WriteLine("B destructed");
            }
        }

        public class C {
            public void CreateA () {
                new A ();
            }

            ~C(){
                Console.WriteLine("C destructed");
            }
        }
        static void TestGarbageCollector () {
            C c = new C ();
            c.CreateA ();

            GC.Collect();
        }
        /* #endregion */

        /* #region  Test Struct and Class */
        class MyClass { // switch struct with class
            public MyClass (int value) {
                Value = value;
            }
            public int Value { get; set; }
        }

        struct MyStruct { // switch struct with class
            public MyStruct (int value) {
                Value = value;
            }
            public int Value { get; set; }
        }

        static void TestStructAndClass () {
            MyStruct d1 = new MyStruct (10);
            MyStruct d2 = new MyStruct (20);
            MyStruct d3 = d1;
            d2 = d3;
            d3.Value = 30;

            Console.WriteLine ($"d1={d1.Value},d2={d2.Value},d3={d3.Value}");

            MyClass c1 = new MyClass(10);
            MyClass c2 = new MyClass(20);
            MyClass c3 = c1;
            c2 = c3;
            c3.Value = 30;
            Console.WriteLine ($"d1={c1.Value},d2={c2.Value},d3={c3.Value}");
        }
        /* #endregion */

        /* #region Action */
        static public bool ActionF<T> (Action<T> atcion, T t) {
            atcion (t);

            return true;
        }

        static void ActionF1 (string s) {
            Console.WriteLine ($"I'm ActionF1 {s}");
        }

        static void ActionF2 (int n) {
            Console.WriteLine ($"I'm ActionF2 {n}");
        }

        static void TestAction () {
            ActionF (ActionF1, "good");
            ActionF (ActionF2, 5);
        }

        /* #endregion */

        /* #region delegate */
        public delegate int AddDelegat (int a, int b);
        static void TestDelegate () {

            AddDelegat f = add;
            Console.WriteLine ($"I'm delegate:{f(3,4)}");
        }

        static private int add (int a, int b) {
            return a + b;
        }
        /* #endregion */

        /* #region Funct */
        static void TestFunc () {
            Console.WriteLine ($"I'm test Func {FuncTest(func1,3,4)}");
            Console.WriteLine ($"I'm test Func {FuncTest(func2,3.5,4.9)}");
        }

        static T FuncTest<T> (Func<T, T, T> func, T a, T b) {
            return func (a, b);
        }

        static int func1 (int a, int b) {
            return a + b;
        }

        static double func2 (double a, double b) {
            return a + b;
        }
        /* #endregion */

        /* #region  Expression */
        static void TestExpression () {

            ExpTest (PredicatFun1, 4);

        }

        static private void ExpTest<T> (Predicate<T> f, T a) {
            if (f (a))
                Console.WriteLine ("I'm Predicat true");
            else
                Console.WriteLine ("I'm Predicat false");
        }

        static private void ExpTest1<T> (Expression<Func<T, bool>> f) {
            // if (f (a))
            //     Console.WriteLine ("I'm Predicat true");
            // else
            //     Console.WriteLine ("I'm Predicat false");
        }

        static private bool PredicatFun1 (int a) {
            if (a > 0)
                return true;
            else
                return false;
        }
        /* #endregion */

        /* #region  Property Constructor */
        public class PropertyInit {
            public int A { set; get; }

            public int B { set; get; }

            public int C { get; set; }

            public PropertyInit () { }

            public PropertyInit (PropertyInit Tp) {
                this.A = Tp.A;
                this.B = Tp.B;
            }
        }

        static void TestPropertyInit () {

            PropertyInit a = new PropertyInit ();
            a.A = 1;
            a.B = 2;
            a.C = 3;

            PropertyInit b = new PropertyInit (a) { C = 100 };

            Console.WriteLine ($"b.A={b.A},b.B={b.B},b.C={b.C}");
        }
        /* #endregion */

        // TestFactory.TestSimpleFactory();

        /* #region  Test Lambda expression body */
        public class LambadaExprssion {
            private int n;

            public int A () => n + 100;

            public bool Larger => n > 100;

            public int A1 => n + 100;

            public int A2(int x,int y)=>x+y+n;

            public LambadaExprssion (int a) {
                n = a;
            }

            static public void Test () {
                LambadaExprssion le = new LambadaExprssion (90);

                Console.WriteLine (le.A ());

                Console.WriteLine (le.A1);

                Console.WriteLine (le.A2(20,30));

                Console.WriteLine (le.Larger);
            }
        }
        /* #endregion */
    }
}