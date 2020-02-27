using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CsharpBasic.Test;

namespace CsharpBasic.Basic
{
    class AsyncTask : ITest
    {
        public void Print(string message)
        {
            Console.WriteLine(string.Format($"{message} in threadId:{Thread.CurrentThread.ManagedThreadId}"));
        }
        public int TestContinueWith()
        {
            int va = 1;

            var task = Task.Run(() =>
            {
                var result = "The task is continuewith";

                Thread.Sleep(10000);

                Print("In TestContinueWith task");

                va++;

                return result + " test";
            });

            task.ContinueWith((t) =>
            {
                Print(t.Result);

                va++;
            });

            Print("Return from TestContinueWith");

            return va;
        }

        public async Task<int> TestAwait1()
        {
            int b = 2;

            var task = Task.Run(() =>
            {
                var result = "The task is TestAwait1";

                //Thread.Sleep(10000);

                Print("In TestAwait1 task");

                b++;

                return result + " test";
            });

            await task.ContinueWith((t) =>
            {
                b++;

                Print(t.Result);
            });

            Thread.Sleep(3000);

            Print("Return from TestAwait1");

            return b;
        }

        public async Task<int> TestAwait2()
        {
            int b = 2;

            var task = await Task.Run(() =>
            {
                var result = "The task is TestAwait2";

                Thread.Sleep(10000);

                Print("In TestAwait2 task");

                b++;

                return result + " test";
            });
            
            Print(task);

            Print("Return from TestAwait2");

            return b;
        }

        public void Entry()
        {
            int re;

            //Print("begin TestContinueWith");
            //re = TestContinueWith();
            //Print("end TestContinueWith");

            Print("begin TestAwait1");
            TestAwait1();
            Print("end TestAwait1");

            //Print("begin TestAwait2");
            //TestAwait2();
            //Print("end TestAwait2");

            Console.WriteLine("Exit from main thread");
            //Console.ReadLine();
        }

        [Test(true)]
        public void Test()
        {
            Entry();
        }
    }
}
