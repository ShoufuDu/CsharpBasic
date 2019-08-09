using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;

namespace CsharpBasic.Basic
{
    public class SeriableTest
    {
        public SeriableTest(){
            Person a = new Person("Tom",10);
            Person b = new Person("Simon",22);
            Person c = new Person("John",21);

            people.Add(a);
            people.Add(b);
            people.Add(c);
        }

        [Serializable]
        public class Person{
            public Person(){}
            public Person(string name,int age){
                this.name = name;
                this.age = age;
            }
            private string name;

            public string Name{
                set{
                    this.name = value;
                }
                get{
                    return this.name;
                }
            }

            // [NonSerialized]
            private int age;
            public int Age{
                set{
                    this.age=value;
                }
                get{
                    return this.age;
                }
            }

            public void sayHi(){
                Console.WriteLine($"Hello everyone, My name is {Name}, I'm {Age} years old");
            }
        }

        public List<Person> people = new List<Person>();

        public void TestSerialize()
        {
            using(FileStream fs = new FileStream("./people.bin",FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs,people);
            }
        }

        public void TestDeserialize()
        {
            using (FileStream fs = new FileStream("./people.bin",FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                List<Person> staff = bf.Deserialize(fs) as List<Person>;

                for(int i=0;i<staff.Count;i++)
                    staff[i].sayHi();
            }
        }
    }
}