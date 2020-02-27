using System.Linq;
using System.IO;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpBasic.Basic
{
    public class SynchronizeCache{
        private Dictionary<int,string> syncCache = new Dictionary<int,string>();
        private ReaderWriterLockSlim sl = new ReaderWriterLockSlim();
        public int Count{
            get{
            return syncCache.Count;
        }
        }

        public void Add(int key,string value)
        {
            sl.EnterWriteLock();
            try{
                syncCache.Add(key,value);
            }
            finally
            {
                sl.ExitWriteLock();
            }
        }

        public string Read(int key)
        {
            sl.EnterReadLock();
            try{
                return syncCache[key];
            }
            finally{
                sl.ExitReadLock();
            }
        }

        public bool AddWithTimeout(int key,string value,int timeout)
        {
            if (sl.TryEnterWriteLock(timeout))
            {
                try{
                    syncCache.Add(key,value);
                    return true;
                }
                finally
                {
                    sl.ExitWriteLock();
                }
            }
            else
            {
                return false;
            }
        }

        public AddOrUpdateStatus AddOrUpdate(int key,string value)
        {
            sl.EnterUpgradeableReadLock();

            string val;
            try{
                if(syncCache.TryGetValue(key,out val))
                {
                        if(val == value)
                        {
                            return AddOrUpdateStatus.Unchanged;
                        }
                        else
                        {
                            sl.EnterWriteLock();
                            try{
                                syncCache[key] = value;
                                return AddOrUpdateStatus.Updated;
                            }
                            finally
                            {
                                sl.ExitWriteLock();
                            }
                        }
                }
                else
                {
                    sl.EnterWriteLock();
                    try{
                        syncCache.Add(key,value);
                        return AddOrUpdateStatus.Add;
                    }
                    finally
                    {
                        sl.ExitWriteLock();
                    }
                }
            }
            finally{
                sl.ExitUpgradeableReadLock();
            }
        }

        public void Delete(int key)
        {
            sl.EnterWriteLock();
            try{
                syncCache.Remove(key);
            }
            finally{
                sl.ExitWriteLock();
            }
        }

        ~ SynchronizeCache()
        {
            sl.Dispose();
        }

        public enum AddOrUpdateStatus{
            Add=1,
            Updated,
            Unchanged
        }
    }
    public class Ex1LockTest : CsharpBasic.Test.ITest
    {
        public void Test(){
            var tasks = new List<Task>();
            SynchronizeCache sc = new SynchronizeCache();
            int itemLength = 0;

            tasks.Add(Task.Run(()=>{
                string[] vegetable = new string[]{"Apple","Orange","Cabbage","Pear","Peach","Flower","Cucumber","boccoli"};
                for (int i=0;i<vegetable.Length;i++)
                {
                    sc.Add(i+1,vegetable[i]);
                }

                itemLength = vegetable.Length;
                Console.WriteLine($"Task:{Task.CurrentId} wrote {itemLength} items");
                }
            ));

            for(int i=0;i<2;i++)
            {
                bool desc = Convert.ToBoolean(i);
                tasks.Add(Task.Run(()=>
                {
                    int start,last,step;
                    int items;
                    do{
                        String output = String.Empty;
                        items = sc.Count;
                        if (desc)
                        {
                            start = 0;
                            last = itemLength -1;
                            step=1;
                        }
                        else
                        {
                            start = itemLength-1;
                            last = 0;
                            step = -1;
                        }

                        for(int j= start;desc?j<=last:j>=last;j+=step)
                            output += String.Format("{0} ",sc.Read(j+1));

                        Console.WriteLine($"{output}");
                    }while(items<itemLength|itemLength==0);
                }));
            }

            tasks.Add(Task.Run(()=>{
                Thread.Sleep(300);
                for(int i=0;i<sc.Count;i++)
                {
                    string val = sc.Read(i+1);
                    if (val == "Apple")
                    {
                        if (sc.AddOrUpdate(i+1,"Good") != SynchronizeCache.AddOrUpdateStatus.Unchanged)
                            Console.WriteLine($"task:{Task.CurrentId} update key:{i+1},val:{val} to 'Good' success");
                    }
                }
            }));

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("synchronized cache");
            for(int i=0;i<sc.Count;i++)
                Console.WriteLine($"Key:{i+1},Val:{sc.Read(i+1)}");
        }
    }
}