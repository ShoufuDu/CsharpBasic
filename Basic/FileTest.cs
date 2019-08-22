using System.IO;
using System;
using System.Linq;

namespace CsharpBasic.Basic
{
    public class FileTest
    {
        public static void TestDir(){
            string dir = @"/Users/dusf/Downloads/csharp/myprj/CsharpBasic/a";

         var files = from file in Directory.EnumerateFiles(dir,"*",SearchOption.AllDirectories)
                from line in File.ReadAllLines(file)
                where line.Contains("dusf")
                select new {
                    File=file,
                    Line=line
                };
        foreach (var f in files)
            Console.WriteLine($"File:{f.File},Line:{f.Line}");


        var dirs = Directory.EnumerateDirectories(dir,"*",SearchOption.AllDirectories);
        foreach(var d in dirs)
            Console.WriteLine(d);
        }


    public static void TestRW(){
        string f = @"/Users/dusf/Downloads/csharp/myprj/CsharpBasic/a/1.txt";

        if (!File.Exists(f))
        {
                using(StreamWriter fw = File.CreateText(f))
                {
                        fw.WriteLine("Good Boy");
                        fw.WriteLine("Good Girl");
                        fw.WriteLine("Good people");
                }
        }

        using(StreamReader fr = File.OpenText(f))
        {
            string s;
            while((s=fr.ReadLine())!=null)
            {
                Console.WriteLine(s);
            }
        }

        string  f1 = @"/Users/dusf/Downloads/csharp/myprj/CsharpBasic/a/1a.txt";

        try{
                int maxLen = 100;
                // if(!File.Exists(f1))
                // {
                    using(FileStream fw = File.OpenWrite(f1))
                    {
                        Byte[] a = new Byte[]{66,67,68,69 };
                        fw.Write(a,0,a.Length);
                    }

                // }

                using(FileStream fr = File.OpenRead(f1))
                    {
                        Byte[] a = new Byte[maxLen];
                        int readLen = fr.Read(a,0,maxLen);
                        for(int i=0;i<readLen;i++)
                            Console.WriteLine((char)a[i]);
                    }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
    }
    }
}