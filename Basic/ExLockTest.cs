using System;
using System.Threading;

namespace CsharpBasic.Basic
{
    public class ExLockTest : CsharpBasic.Test.ITest
    {
        static private object myLock = new object ();

        static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim ();
        public static int resource = 0;
        static public int reads = 0;
        static public int writes = 0;
        static public int readTimeout = 0;
        static public int writeTimeout = 0;
        static public int threadNum = 26;
        static public bool running = true;
        static public Random rnd = new Random();

        void ThreadProc () {

            while(running)
            {
                double action = rnd.NextDouble();
                if(action<.8)
                {
                    ReadResource(10);
                }
                else if (action<.81)
                {
                    ReleaseRestore(50);
                }
                else if (action<.90)
                {
                    UpgradeDowngrade(100);
                }
                else
                {
                    WriteResource(100);
                }
            }

            Display("Thread:"+Thread.CurrentThread.Name+" exited");
        }

        public void Test()
        {
            MainThreadsTest();
        }

        public void MainThreadsTest () {
            Thread[] ts = new Thread[threadNum];

            for (int i = 0; i < ts.Length; i++) {
                ts[i] = new Thread (new ThreadStart (ThreadProc));
                ts[i].Name = new String (Convert.ToString (i));
                ts[i].Start ();
                if (i > 10)
                    Thread.Sleep (300);
            }

            running = false;

            for(int i=0; i<threadNum; i++)
                ts[i].Join();

            Console.WriteLine($"read:{reads},writes:{writes},readtimeout:{readTimeout},writeTimeout:{writeTimeout}");
            // Console.ReadLine();
        }

        void ReadResource (int timeout) {
                if(rwl.TryEnterReadLock(timeout)){
                try{
                    Display("Read resource:"+resource);
                    Interlocked.Increment(ref reads);
                }
                finally
                {
                    rwl.ExitReadLock();
                }
                }
            }

        void WriteResource (int timeout) {

                if (rwl.TryEnterWriteLock(timeout))
                {
                    try{
                        resource=rnd.Next(500);
                        Display("write resource:"+resource);
                        Interlocked.Increment(ref writes);
                    }
                    finally
                    {
                        rwl.ExitWriteLock();
                    }
                }
        }

        void ReleaseRestore (int timeout) {

        }

        void UpgradeDowngrade (int timeout) {

                rwl.EnterUpgradeableReadLock();
                try{
                    Display("Read resource:"+resource);
                    Interlocked.Increment(ref reads);
                       if(rwl.TryEnterWriteLock(timeout))
                        {
                            try{
                                resource = rnd.Next(500);
                                Display("Write resource:"+resource);
                                Interlocked.Increment(ref writes);
                            }
                            finally
                            {
                                rwl.ExitWriteLock();
                            }
                        }
                }
                finally{
                    rwl.ExitUpgradeableReadLock();
                }
        }

        static void Display(string msg)
        {
            Console.WriteLine($"Thread:{Thread.CurrentThread.Name},Msg:{msg}");
        }
    }
}