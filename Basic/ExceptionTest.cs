
using System;

namespace CsharpBasic.Basic 
{
    public class ExceptionTest : CsharpBasic.Test.ITest
    {
        public void Test(){

            TestApp();

            try
            {
                int x=0;
                int y = 5;

                int z = y/x;

                Console.WriteLine("TRY");
                return;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine($"DivideByZeroException {e.Message}");
                Console.WriteLine("return");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            finally
            {
                Console.WriteLine("Finally");
            }

        }

        public static void TestApp(){
            AppDomain app = AppDomain.CurrentDomain;

            app.UnhandledException += new UnhandledExceptionEventHandler(myHandler);

            try{
                throw new Exception("1");
            }
            catch (Exception e){
                Console.WriteLine($"exeception:{e.Message}");
            }

            throw new Exception("2");
        }

        public static void Test1()
        {
            try {
                int a=6,b=0;

                Console.WriteLine($"Exception:{a/b}");
            }
            finally
            {
                Console.WriteLine("Divided by zero error");
            }

            Console.ReadLine();
        }

        static void myHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception) args.ExceptionObject;
            Console.WriteLine($"Myhandler caught :{e.Message}");
            Console.WriteLine($"Runtime terminating:{args.IsTerminating}");
        }
    }
}