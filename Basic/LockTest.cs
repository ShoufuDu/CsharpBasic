using System;
using System.Threading;

namespace CsharpBasic.Basic {
    public class LockTest : CsharpBasic.Test.ITest
    {
        static private object myLock = new object ();

        static ReaderWriterLock rwl = new ReaderWriterLock ();
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
            try{
                rwl.AcquireReaderLock(timeout);
                try{
                    Display("Read resource:"+resource);
                    Interlocked.Increment(ref reads);
                }
                finally
                {
                    rwl.ReleaseReaderLock();
                }
            }
            catch (ApplicationException)
            {
                Interlocked.Increment(ref readTimeout);
            }
        }

        void WriteResource (int timeout) {

            try{
                rwl.AcquireWriterLock(timeout);
                try{
                    resource=rnd.Next(500);
                    Display("write resource:"+resource);
                    Interlocked.Increment(ref writes);
                }
                finally
                {
                    rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
                Interlocked.Increment(ref writeTimeout);
            }

        }

        void ReleaseRestore (int timeout) {
            int lastWriter;

            try{
                rwl.AcquireReaderLock(timeout);
                try {
                    Display("read resource:"+resource);
                    Interlocked.Increment(ref reads);

                    int resourceValue = resource;

                    lastWriter = rwl.WriterSeqNum;

                    LockCookie lc = rwl.ReleaseLock();

                    Thread.Sleep(rnd.Next(250));

                    rwl.RestoreLock(ref lc);

                    if (rwl.AnyWritersSince(lastWriter))
                    {
                        resourceValue = resource;
                        Interlocked.Increment(ref reads);
                        Display("resouce has changed:"+resourceValue);
                    }
                    else
                    {
                        Display("resouce has not changed:"+resourceValue);
                    }
                }
                catch (ApplicationException)
                {
                    rwl.ReleaseReaderLock();
                }
            }
            catch(ApplicationException)
            {
                    Interlocked.Increment(ref readTimeout);
            }
        }

        void UpgradeDowngrade (int timeout) {
            try{
                rwl.AcquireReaderLock(timeout);
                try{
                    Display("Read resource:"+resource);
                    Interlocked.Increment(ref reads);

                    try{
                        LockCookie lc = rwl.UpgradeToWriterLock(timeout);
                        try{
                            resource = rnd.Next(500);
                            Display("Write resource:"+resource);
                            Interlocked.Increment(ref writes);
                        }
                        finally
                        {
                            rwl.DowngradeFromWriterLock(ref lc);
                        }
                    }
                    catch (ApplicationException)
                    {
                        Interlocked.Increment(ref writeTimeout);
                    }

                    Display("Read resource:"+resource);
                    Interlocked.Increment(ref reads);
                }
                finally{
                    rwl.ReleaseReaderLock();
                }

            }
            catch(ApplicationException)
            {
                Interlocked.Increment(ref readTimeout);
            }
        }

        static void Display(string msg)
        {
            Console.WriteLine($"Thread:{Thread.CurrentThread.Name},Msg:{msg}");
        }
    }
}