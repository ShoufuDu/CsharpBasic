using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CsharpBasic.Basic
{
    class LambdaLoopVariable : CsharpBasic.Test.ITest
    {
        public void Test()
        {
            var loops = new List<int>{ 1, 2, 3, 4 };

            Thread.Sleep(100);

            foreach (var i in loops)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Not parameters CurrentTaskId:{Task.CurrentId} output {i}");
                });
            }

            foreach(var i in loops)
            {
                Task.Factory.StartNew((para) =>
                {
                    //Thread.Sleep(100);
                    Console.WriteLine($"Parameters CurrentTaskId:{Task.CurrentId} output {(int)para}");
                },i);
            }
        }
        
    }
}
