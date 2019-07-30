using System;
using System.Linq;

namespace CsharpBasic.Basic
{
    public class LinqTest
    {
        static public readonly int[] seq = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};

        public class Person{
            public int Id{set;get;}
            public String Name{set;get;}
            public String Gender{set;get;}

            public Person(){}

            public Person(int id,String name, String gender)
            {
                this.Id = id;
                this.Name = name;
                this.Gender = gender;
            }
        }

        public class CallLog{
            public int Id{set;get;}
            public int P_Id {set;get;}
            public string Service_Number{set;get;}

            public string Dialed_Number{set;get;}

            public string Exchange_ID{set;get;}

            public string Call_Status{set;get;}

            public CallLog(int id,int pid,string sn,string dn,string eid,string cs){
                this.Id = id;
                this.P_Id = pid;
                this.Service_Number = sn;
                this.Dialed_Number =dn;
                this.Exchange_ID = eid;
                this.Call_Status = cs;
            }

            override public String ToString()
            {
                return $"{this.P_Id} {this.Service_Number} {this.Dialed_Number} {this.Call_Status}";
            }
        }

        public static readonly Person[] People = new Person[]{
                new Person{Id=1,Name="Tom",Gender="M"},
                new Person{Id=2,Name="John",Gender="M"},
                new Person(3,"Alice","W"),
                new Person(4,"Eric","M"),
                new Person(5,"Ingilic","W"),
                new Person(6,"Sarvana","M")
            };

        private const String S_C = "CONNECTED";
        private const String S_B = "BUSY";

        public static readonly CallLog[] CallRecords= new CallLog[]{
            new CallLog(1,1,"0477276381","0476234974","X012",S_C),
            new CallLog(2,3,"0477276383","0476234974","X012",S_C),
            new CallLog(3,4,"0477276384","0476234974","X012",S_B),
            new CallLog(4,2,"0477276382","0476234974","X012",S_C),
            new CallLog(5,1,"0477276381","0476234974","X012",S_C),
            new CallLog(6,5,"0477276385","0476234974","X012",S_C),
            new CallLog(7,6,"0477276386","0476234974","X012",S_C),
            new CallLog(8,2,"0477276382","0476234974","X012",S_B),
            new CallLog(9,2,"0477276382","0476234974","X012",S_C),
            new CallLog(10,4,"0477276384","0476234974","X012",S_C),
            new CallLog(11,3,"0477276383","0476234974","X012",S_B),
            new CallLog(12,4,"0477276384","0476234974","X012",S_C),
            new CallLog(13,5,"0477276385","0476234974","X012",S_C)
        };

        static public void TestCallRecords()
        {
             var records = CallRecords.OrderBy(x=>x.P_Id);
             foreach(var r in records)
             {
                 Console.WriteLine(r.ToString());
             }

             var records_G = CallRecords.GroupBy(x=>x.P_Id).Select(g=>new{Name=g.Key,Count=g.Count()});

             foreach(int i=0; i<records_G.Count;i++)
             {
                    Console.Write(records_G[i].Name);
             }
        }
        static public void Test1()
        {
            var t = seq.Where(x=>x>10).Select(x=>x*10);

            foreach(int i in t)
            {
                Console.Write(i+" ");
            }
        }

    }
}