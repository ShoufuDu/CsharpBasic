using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpBasic.Basic
{
    class TaskTest
    {

        private static TaskTest Instance = new TaskTest();

        public int Sign { set; get; }

        public TaskTest()
        {
            Console.WriteLine("TaskTest initial");

            MultipleTask1();
        }

        static public TaskTest GetInstance()
        {
            return Instance;
        }

        class BankAccount
        {
            public int Balance { set; get; }
        }

        public void MultipleTask1()
        {
            BankAccount account = new BankAccount();

            Task<int>[] tasks = new Task<int>[10];

            ThreadLocal<int> tls = new ThreadLocal<int>(
            () =>
            {
                Console.WriteLine("Value factory called for value:{0}", account.Balance);
                return account.Balance;
            });

            for (int i =0; i<10; i++)
            {
                tasks[i] = new Task<int>((stateObj) =>
                {
                    int init = (int)stateObj;
                    Console.WriteLine($"Task:{init} get initial value:{tls.Value}, thredId:{Thread.CurrentThread.ManagedThreadId}");

                    for (int j = 0; j < 1000; j++)
                    {
                        tls.Value++;
                    }

                    return tls.Value;
                }, i);

                tasks[i].Start();
            }

            for (int i = 0; i< 10; i++)
            {
                account.Balance += tasks[i].Result;
            }

            Console.WriteLine($"Expected value:10000, Balance:{account.Balance}");

        }

        class ThreadPara
        {
            public int Index { set; get; }
            public int Balance { set; get; }
        }

        public void MultipleTask2()
        {
            BankAccount account = new BankAccount();

            Task<int>[] tasks = new Task<int>[10];

            ThreadLocal<int> tls = new ThreadLocal<int>();

            for (int i = 0; i < 10; i++)
            {
                tasks[i] = new Task<int>((stateObj) =>
                {
                    tls.Value = ((ThreadPara)stateObj).Balance;
                    var init = ((ThreadPara)stateObj).Index;

                    Console.WriteLine($"Task:{init} get initial value:{tls.Value}, thredId:{Thread.CurrentThread.ManagedThreadId}");

                    for (int j = 0; j < 1000; j++)
                    {
                        tls.Value++;
                    }

                    return tls.Value;
                }, new ThreadPara{ Index = i, Balance = account.Balance });

                tasks[i].Start();
            }

            for (int i = 0; i < 10; i++)
            {
                account.Balance += tasks[i].Result;
            }

            Console.WriteLine($"Expected value:10000, Balance:{account.Balance}");

        }
    }

    class CustomData
    {
        public long CreationTime;
        public int Index;
        public int? TaskId;
        public int ThreadId;

        public CustomData() { }

        public CustomData(CustomData c)
        {
            this.CreationTime = c.CreationTime;
            this.Index = c.Index;
            this.TaskId = c.TaskId;
            this.ThreadId = c.ThreadId;
        }
    }

    public class Example : CsharpBasic.Test.ITest
    {
        public void Test()
        {
            Task[] taskArray = new Task[10];


            Console.WriteLine("=================== Correct ways to bring parameters int the lambda express ================");

            CustomData para = null;

            for (int i = 0; i < taskArray.Length; i++)
            {
                para = new CustomData() { Index = i, CreationTime = DateTime.Now.Ticks };

                taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {

                    Thread.Sleep(1000);
                    CustomData data = obj as CustomData; // will be changed to transfer by value, not necessary to new as new CustomData(obj as CustomData)

                    if (data == null)
                        return;
                    data.TaskId = Task.CurrentId;
                    data.ThreadId = Thread.CurrentThread.ManagedThreadId;
                }, para);
            }

            Task.WaitAll(taskArray);

            foreach (var task in taskArray)
            {
                var data = task.AsyncState as CustomData;
                if (data != null)
                    Console.WriteLine("Task #{0}, id #{1} created at {2}, ran on thread #{3}.",
                                      data.Index, data.TaskId, data.CreationTime, data.ThreadId);
            }
            
            Console.WriteLine("===================== Wrong ways to bring parameters int the lambda express ===============");

            CustomData data1 = null;
            for (int i = 0; i < taskArray.Length; i++)
            {
                data1 = new CustomData() { Index = i, CreationTime = DateTime.Now.Ticks };

                taskArray[i] = Task.Factory.StartNew(() => {

                    Thread.Sleep(2000);

                    if (data1 == null)
                    {
                        return;
                    }

                    data1.TaskId = Task.CurrentId;

                    data1.ThreadId = Thread.CurrentThread.ManagedThreadId;

                    Console.WriteLine("Task #{0}, id #{1} created at {2}, ran on thread #{3}.",
                                      data1.Index, data1.TaskId, data1.CreationTime, data1.ThreadId);
                });
            }
            Task.WaitAll(taskArray);

            Console.WriteLine("=================== Another correct ways to bring parameters int the lambda express ================");
            for (int i = 0; i < taskArray.Length; i++)
            {
                var data2 = new CustomData() { Index = i, CreationTime = DateTime.Now.Ticks };

                taskArray[i] = Task.Factory.StartNew(() => {

                    Thread.Sleep(2000);

                    if (data2 == null)
                    {
                        return;
                    }

                    data2.TaskId = Task.CurrentId;

                    data2.ThreadId = Thread.CurrentThread.ManagedThreadId;

                    Console.WriteLine("Task #{0}, id #{1} created at {2}, ran on thread #{3}.",
                                      data2.Index, data2.TaskId, data2.CreationTime, data2.ThreadId);
                });
            }

            Task.WaitAll(taskArray);


            Console.WriteLine("=================== Another wrong ways to bring parameters int the lambda express ================");

            CustomData data3 = null;
            for (int i = 0; i < taskArray.Length; i++)
            {
                data3 = new CustomData() { Index = i, CreationTime = DateTime.Now.Ticks };

                taskArray[i] = Task.Factory.StartNew(() => {
                    var data4 = data3;

                    Thread.Sleep(2000);

                    if (data4 == null)
                    {
                        return;
                    }

                    data4.TaskId = Task.CurrentId;

                    data4.ThreadId = Thread.CurrentThread.ManagedThreadId;

                    Console.WriteLine("Task #{0}, id #{1} created at {2}, ran on thread #{3}.",
                                      data4.Index, data4.TaskId, data4.CreationTime, data4.ThreadId);
                });
            }

            Task.WaitAll(taskArray);


            Console.WriteLine("=================== Another wrong ways to bring parameters int the lambda express ================");

            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((obj) => {
                    var data5 = new CustomData() { Index = i, CreationTime = DateTime.Now.Ticks };

                    Thread.Sleep(2000);

                    if (data5 == null)
                    {
                        return;
                    }

                    data5.TaskId = Task.CurrentId;

                    data5.ThreadId = Thread.CurrentThread.ManagedThreadId;

                    Console.WriteLine("Task #{0}, id #{1} created at {2}, ran on thread #{3}.",
                                      data5.Index, data5.TaskId, data5.CreationTime, data5.ThreadId);
                },i);
            }

            Task.WaitAll(taskArray);

            Console.WriteLine("=================== Another Correct ways to bring parameters int the lambda express ================");

            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((obj) => {
                    var data5 = new CustomData() { Index = (int)obj, CreationTime = DateTime.Now.Ticks };

                    Thread.Sleep(2000);

                    if (data5 == null)
                    {
                        return;
                    }

                    data5.TaskId = Task.CurrentId;

                    data5.ThreadId = Thread.CurrentThread.ManagedThreadId;

                    Console.WriteLine("Task #{0}, id #{1} created at {2}, ran on thread #{3}.",
                                      data5.Index, data5.TaskId, data5.CreationTime, data5.ThreadId);
                }, i);
            }

            Task.WaitAll(taskArray);

        }
    }

}
