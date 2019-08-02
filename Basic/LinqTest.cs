using System.Reflection.Metadata.Ecma335;
using System;
using System.Linq;

namespace CsharpBasic.Basic {
    public class LinqTest {
        static public readonly int[] seq = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

        public class Person {
            public int Id { set; get; }
            public String Name { set; get; }
            public String Gender { set; get; }

            public Person () { }

            public Person (int id, String name, String gender) {
                this.Id = id;
                this.Name = name;
                this.Gender = gender;
            }
        }

        public class CallLog {
            public int Id { set; get; }

            public int P_Id{set;get;}

            public string Service_Number { set; get; }

            public string Dialed_Number { set; get; }

            public string Exchange_ID { set; get; }

            public string Call_Status { set; get; }

            public CallLog (int id, int pid, string sn, string dn, string eid, string cs) {
                this.Id = id;
                this.P_Id = pid;
                this.Service_Number = sn;
                this.Dialed_Number = dn;
                this.Exchange_ID = eid;
                this.Call_Status = cs;
            }

            override public String ToString () {
                return $"{this.P_Id} {this.Service_Number} {this.Dialed_Number} {this.Call_Status}";
            }
        }

        public static readonly Person[] People = new Person[] {
            new Person { Id = 1, Name = "Tom", Gender = "M" },
            new Person { Id = 2, Name = "John", Gender = "M" },
            new Person (3, "Alice", "W"),
            new Person (4, "Eric", "M"),
            new Person (5, "Ingilic", "W"),
            new Person (6, "Sarvana", "M")
        };

        private const String S_C = "CONNECTED";
        private const String S_B = "BUSY";

        public static readonly CallLog[] CallRecords = new CallLog[] {
            new CallLog (1, 1, "0477276381", "0476234974", "X012", S_C),
            new CallLog (2, 3, "0477276383", "0476234974", "X012", S_C),
            new CallLog (3, 4, "0477276384", "0476234974", "X012", S_B),
            new CallLog (4, 2, "0477276382", "0476234974", "X012", S_C),
            new CallLog (5, 1, "0477276381", "0476234974", "X012", S_C),
            new CallLog (6, 5, "0477276385", "0476234974", "X012", S_C),
            new CallLog (7, 6, "0477276386", "0476234974", "X012", S_C),
            new CallLog (8, 2, "0477276382", "0476234974", "X012", S_B),
            new CallLog (9, 2, "0477276382", "0476234974", "X012", S_C),
            new CallLog (10, 4, "0477276384", "0476234974", "X012", S_C),
            new CallLog (11, 3, "0477276383", "0476234974", "X012", S_B),
            new CallLog (12, 4, "0477276384", "0476234974", "X012", S_C),
            new CallLog (13, 5, "0477276385", "0476234974", "X012", S_C)
        };

        // select c.Type ,c.ItemName,SUM(a.ConsumptionAmount)
        // from [dbo].[ConsumptionItem] a left join [dbo].[Membership] b on a.MembershipID=b.ID left join [dbo].[BillItem] c on a.BillItemID=c.ID where b.Name='小明' and c.Type=0
        // group by c.Type ,c.ItemName
        // 用lambda方法语法 怎么实现这条sql查询，求大神指教···

        //  ConsumptionItem.Join(Membership, x => x.MembershipID, x => x.ID, (a, b) => new { a, b })
        // .Join(BillItem, x => x.a.BillItemID, x => x.ID, (a, c) => new { a.a, a.b, c })
        // .Where(x => x.b.Name == "小明" && x.c.Type == 0)
        // .GroupBy(x => new { c.Type, c.ItemName })
        // .Select(x => x.Key.Type, x.Key.ItemName, sum = x.Sum(y => y.a.ConsumptionAmount));

        static public void TestCallRecords () {
            var records = CallRecords.OrderBy (x => x.P_Id);
            foreach (var r in records) {
                Console.WriteLine (r.ToString ());
            }

            var records_G = CallRecords.GroupBy (x => x.P_Id).Select (g => new { Name = g.Key, Count = g.Count () })
                .OrderByDescending (x => x.Name);

            foreach (var r in records_G) {
                Console.WriteLine ($"{r.Name} {r.Count}");
            }

            var records_1 = from p in People
            from r in CallRecords
            where p.Id == r.P_Id
            select new { Name = p.Name, CallFrom = r.Service_Number, CallTo = r.Dialed_Number, Status = r.Call_Status };

            foreach (var r in records_1) {
                Console.WriteLine ($"{r.Name} {r.CallFrom} {r.CallTo} {r.Status}");
            }


            foreach (var r in records_1) {
                Console.WriteLine ($"{r.Name} {r.CallFrom} {r.CallTo} {r.Status}");
            }

            var records_2 = from r1 in CallRecords
                            group r1 by r1.P_Id into g
                            select new {g.Key, Num=g.Count()};

            foreach(var r in records_2)
                Console.WriteLine($"{r.Key} {r.Num}");

            Console.WriteLine("");
            var records_3 = from r1 in CallRecords
                            group r1 by r1.P_Id;

            var records_4 = from r in records_3
                            from p in People
                            where r.Key == p.Id
                            select new{Name = p.Name,Id=r.Key, Num=r.Count()};

            foreach(var r in records_4)
                Console.WriteLine($"{r.Name} {r.Id} {r.Num}");

            var records_5 = from p in People
                            from r in (
                                from r1 in CallRecords
                                group r1 by r1.P_Id into g
                                select new{Id=g.Key,Num=g.Count()}
                            )
                            where p.Id == r.Id
                            select new{Id=p.Id, Name=p.Name, Num=r.Num};

            Console.WriteLine();
            foreach(var r in records_5)
                Console.WriteLine($"{r.Name} {r.Id} {r.Num}");


            var records_6 = from p in People
                            from r in (
                                CallRecords.GroupBy(x=>x.P_Id).Select(r=>new{Id=r.Key,Num=r.Count()})
                            )
                            where p.Id == r.Id
                            select new{Id=p.Id, Name=p.Name, Num=r.Num};

            Console.WriteLine();
            foreach(var r in records_6)
                Console.WriteLine($"{r.Name} {r.Id} {r.Num}");


            var records_7 = from r in CallRecords
                            orderby r.P_Id,r.Call_Status
                            select new{r.P_Id,r.Call_Status, r.Service_Number};
            foreach(var r in records_7)
                Console.WriteLine($"{r.P_Id},{r.Call_Status},{r.Service_Number}");

            Console.WriteLine();
            var records_8 = CallRecords.OrderBy(r=>r.P_Id).ThenBy(r=>r.Call_Status)
                            .Select(x=>new{x.P_Id,x.Call_Status,x.Service_Number});
            foreach(var r in records_7)
                Console.WriteLine($"{r.P_Id},{r.Call_Status},{r.Service_Number}");

        }


        static public void Test1 () {
            var t = seq.Where (x => x > 10).Select (x => x * 10);

            foreach (int i in t) {
                Console.Write (i + " ");
            }
        }

    }
}