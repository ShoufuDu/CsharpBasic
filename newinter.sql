1. .net framework
1.1 GC
https://dotnetinter.livejournal.com/92200.html

	Garbage collector is a feature of CLR which cleans unused managed (it does not clean unmanaged objects) objects and reclaims memory. It’s a back ground thread which runs continuously and at specific intervals it checks if there are any unused objects whose memory can be claimed.

	What are generations in Garbage collector (Gen 0, 1 and 2)?

	Generations defines age of the object.  There are three generations:-

	Gen 0:- When application creates fresh objects they are marked as Gen 0.

	Gen 1:- When GC is not able to clear the objects from Gen 0  in first round it moves them to Gen 1 bucket.

	Gen 2:- When GC visits Gen 1 objects and he is not able to clear them he moves them gen 2.

Generations are created to improve GC performance. Garbage collector will spend more time on Gen 0 objects rather than Gen 1 and Gen 2 thus improving performance.


1.2 CLR
	CLR provides an environment to execute .net applications on target machines.

	Automatic Memory Management
	CLR invokes multiple built in functions of .net framework to allocate and deallocate memory of .net objects.

	Garbage Collection
	Prevents memory leaks during program execution

	Code Access Security
	Imposes security and restrictions during execution of programs

	Code Verification
	Specifies that CLR enforces type safety and prevents a source code from performing illegal operations

	JIT Compilation of .net code
	Loads MSIL code on target machine for execution.

1.3 differences between finalize and dispose
	Dispose																							 Finalize
It is used to free unmanaged resources at any time.	 | It can be used to free unmanaged resources held by an object before that object is destroyed.
It is called by user code and the class which is implementing dispose method, must has to implement IDisposable interface. | It is called by Garbage Collector and cannot be called by user code.
It is implemented by implementing IDisposable interface Dispose() method.	| It is implemented with the help of Destructors
There is no performance costs associated with Dispose method.	| There is performance costs associated with Finalize method since it doesn’t clean the memory immediately and called by GC automatically.

1.4 difference between struct and class
	https://www.c-sharpcorner.com/blogs/difference-between-struct-and-class-in-c-sharp

1.5 exception handle
    AppDomain.UnhandledExcption += new UnhandledExceptionEventHandler(MyHandler);
       
    void MyHandle(object sender, UnhandledExceptionEventArgs args){
    
    		Exception e = (Exception) args.ExceptionObject;
    		bool isTermainating = args.IsTerminating;
    }
    
2. C#
2.1 whatis the difference of interface and class? how to implement interface.

https://www.geeksforgeeks.org/difference-between-abstract-class-and-interface-in-c-sharp/

2.2 Exeception hanle

2.3. thread safety
2.3.1 lock

			static readonly object lockObj = new object();
			
			lock(lockObj){}
			
2.3.2 read lock and write lock
	https://docs.microsoft.com/en-us/dotnet/api/system.threading.readerwriterlockslim?view=netframework-4.8
			ReaderWriterLockSlim rwl
			
			rwl.EnterReadLock
			rwl.ExitReadLock
			
			rwl.EnterWriteLock
			rwl.ExitWriteLock
			
			TryEnterWriteLock
			
			rwl.EnterUpgradableReadLock()
			rwl.ExitUpgradableReadLock()

2.4 linq
		https://github.com/ShoufuDu/CsharpBasic/tree/master/Basic

2.5 io operations
    list dir
    list file
    list subdir
    
    Directory.EnumerateFiles(path,"*.txt",SearchOption.AllDirectories)
    Directory.EnumerateDirectories(path,pattern,SearchOption.AllDirectories)
    																							SearchOption.TopDirectoryOnly
    
    txt:
    StreamWriter fw = File.CreateText(path)
    fw.WriteLine(str);
    
    StreamReader fr = File.OpenText(path)
    string s = fr.ReadLine();
    
    bin:
    FileStream fw = File.OpenWrite(path);
    fw.Write(array,0,len)
    
    FileStream fr = File.OpenRead(path);
    len = fr.Read(a,0,maxLen)
    
    Append:
    FileStream fs = File.Open(path,FileMode.Append,FileAccess.Write)
    File.Seek(0,SeekOrigin.Begin)
    File.Write()
    File.Read()
    
3 hashcode of class
	https://stackoverflow.com/questions/20604039/non-readonly-fields-referenced-in-gethashcode
	must be readonly variables
    
4. webapi
4.1 what the difference of get/post, put/post
	https://blog.mwaysolutions.com/2014/06/05/10-best-practices-for-better-restful-api/
4.2 whatis the restfulapi?
	https://www.guru99.com/rest-api-interview-question-answers.html

5. linq querry for sql
   group by

6. query optimise


7. what's the difference of clustered index and nonclustered index

https://www.quora.com/What-is-the-difference-between-clustered-and-non-clustered-index

8. draw the architecture of large scale website

   firewall/reverse proxy/cdn/db/...
