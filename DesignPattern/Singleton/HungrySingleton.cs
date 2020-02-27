using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CsharpBasic.DesignPattern.Singleton
{
    public sealed class HungrySingletonWithStaticConstructor
    {
        static private HungrySingletonWithStaticConstructor _instance = new HungrySingletonWithStaticConstructor();

        //explictly static constructor to tell c# complier not to mark type as beforefieldinit
        static HungrySingletonWithStaticConstructor() { }

        private HungrySingletonWithStaticConstructor()
        {
            Console.WriteLine($"Class contructor called for HungrySingletonWithStaticConstructor");
        }

        public HungrySingletonWithStaticConstructor Instance
        {
            get
            {
                return _instance;
            }
        }

        public static string Sign = EchoAndReturn("Init Sign");

        public static string EchoAndReturn(string str)
        {
            Console.WriteLine("Call EchoAndReturn" + str);

            return str;
        }

    }

    public sealed class HungrySingleton
    {
        static private HungrySingleton _instance = new HungrySingleton();

        //explictly static constructor to tell c# complier not to mark type as beforefieldinit
        //static HungrySingleton() { }

        public HungrySingleton Instance
        {
            get
            {
                return _instance;
            }
        }

        private HungrySingleton()
        {
            Console.WriteLine($"Class contructor called for HungrySingleton");
        }

        public static string Sign = EchoAndReturn("Init static property of Sign");

        public static string EchoAndReturn(string str)
        {
            Console.WriteLine("Call EchoAndReturn"+ str);

            return str;
        }
    }

    class TestHungrySingleton : CsharpBasic.Test.ITest
    {
        public void Test()
        {
            Console.WriteLine(" ****************** Without explict constructor *******************");

            Console.WriteLine("Before Test without explicit static constructor1");

            Console.WriteLine("Before Test without explicit static constructor2");

            int j = 0;
            for (int i = 0; i < 100; i++)
            {
                j++;
            }

            Console.WriteLine($"j:{j}");

            Console.WriteLine("Before Test without explicit static constructor3");

            Console.WriteLine($"HungrySingleton.Sign = {HungrySingleton.Sign}");

            Console.WriteLine("End test");

            Console.WriteLine(" ****************** With explict constructor *******************");

            Console.WriteLine("Before Test without explicit static constructor1");

            Console.WriteLine("Before Test without explicit static constructor2");

            j = 0;
            for (int i = 0; i < 100; i++)
            {
                j++;
            }

            Console.WriteLine($"j:{j}");

            Console.WriteLine("Before Test without explicit static constructor3");

            Console.WriteLine($"HungrySingleton.Sign = {HungrySingletonWithStaticConstructor.Sign}");

            Console.WriteLine("End test");
        }
    }

}


