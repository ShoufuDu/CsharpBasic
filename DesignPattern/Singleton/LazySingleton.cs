using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpBasic.DesignPattern.Singleton
{
    public sealed class LazySingletonThreadUnsafe
    {
        private static LazySingletonThreadUnsafe _instance = null;

        public static LazySingletonThreadUnsafe Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LazySingletonThreadUnsafe();
                    return _instance;
                }
                else
                {
                    return _instance;
                }
            }
        }
    }


    public sealed class LazySingletonThreadSafeUnderminePerformance
    {
        private static LazySingletonThreadSafeUnderminePerformance _instance = null;

        private static object _locker = new object();

        public static LazySingletonThreadSafeUnderminePerformance Instance
        {
            get
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new LazySingletonThreadSafeUnderminePerformance();
                        
                    }
                    
                    return _instance;
                }
            }
        }
    }

    /// <summary>
    /// Double check style
    /// </summary>
    public sealed class LazySingletonThreadSafeHighPerformance
    {
        private LazySingletonThreadSafeHighPerformance _instance = null;
        private static object locker = new object();

        public LazySingletonThreadSafeHighPerformance Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new LazySingletonThreadSafeHighPerformance(); 
                        }
                    }
                }

                return _instance;
            }
        }
    }

    public sealed class LazySingletonNestedStatic
    {
        private LazySingletonNestedStatic() { }

        public static LazySingletonNestedStatic Instance
        {
            get
            {
                return NestedSingleton._instance;
            }
        }

        private static class NestedSingleton
        {
            static NestedSingleton() { }

            public static LazySingletonNestedStatic _instance = new LazySingletonNestedStatic();
        }
    }
}
