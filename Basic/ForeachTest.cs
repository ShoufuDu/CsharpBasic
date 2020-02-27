using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpBasic.Basic
{
    class ForeachTest : CsharpBasic.Test.ITest
    {
        int[] array = new int[] { 1, 2, 3, 4, 5 };

        int index = -1;
        public ForeachTest GetEnumerator() { return this; }

        public int Current { 
            get {
                return array[index];
            }
        }

        public bool MoveNext() { index++; return index < array.Length ? true : false; }

        public void Test()
        {
            var a = new ForeachTest();
            foreach (var s in a)
            {
                Console.WriteLine(s);
            }

            var alist = new List<int> { 6, 7, 8, 9, 10 };

            foreach (var i in alist)
            {
                Console.WriteLine(i);
            }

            foreach (var i in alist)
            {
                Console.WriteLine(i);
            }
        }
    }
}
