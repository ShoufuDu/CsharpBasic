using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CsharpBasic.Basic
{
    public class NewClass : CsharpBasic.Test.ITest
    {
        public string a;
        public int b;
        public string Name { set; get; }
        public int Age { set; get; }


        public NewClass()
        {

        }

        public NewClass(string m, int n)
        {
            a = m;
            b = n;
        }

        public void Show()
        {
            Console.WriteLine("Create a object ok " + a + " "+ b + " " + this.Name +" " + this.Age);
        }

        public void PShow(int a, NewClass b)
        {

        }

        public void Test()
        {
            NewClass nc = new NewClass();

            Type t = nc.GetType();

            ConstructorInfo[] cis = t.GetConstructors();

            int count = 0;
            foreach (var c in cis)
            {
                Console.WriteLine("constructor:" + count++);

                ParameterInfo[] ps = c.GetParameters();

                foreach (var p in ps)
                {
                    Console.WriteLine(p.ParameterType.ToString() + " " + p.Name);
                }
            }

            Type[] pt = new Type[2] { typeof(string), typeof(int) };

            ConstructorInfo cs = t.GetConstructor(pt);

            Object[] parms = new Object[2] { "5", 6 };

            object o = cs.Invoke(parms);

            ((NewClass)o).Show();


            object[] paras2 = new object[2] { "f1", 77 };

            var o1 = Activator.CreateInstance(t, paras2);
            ((NewClass)o1).Show();

            var o2 = Activator.CreateInstance(t, "fff", 12);
            ((NewClass)o2).Show();


            var o3 = Activator.CreateInstance(t);
            ((NewClass)o3).Show();

            PropertyInfo[] pros = t.GetProperties();
            foreach (var ps in pros)
            {
                Console.WriteLine("Pros: type:" + ps.PropertyType + " " + "name:" + ps.Name);
            }


            FieldInfo[] fields = t.GetFields();
            foreach (var fs in fields)
            {
                Console.WriteLine("Pros: type:" + fs.FieldType.ToString() + " " + "name:" + fs.Name);
            }

            MethodInfo[] methods = t.GetMethods();
            foreach (var m in methods)
            {
                Console.WriteLine(m.ReturnType.ToString() + " " + "method name:" + m.Name);

                var paras = m.GetParameters();
                foreach (var p in paras)
                {
                    Console.WriteLine("Params type:" + p.ParameterType + " parma name:" + p.Name);
                }
            }
        }
    }

}
