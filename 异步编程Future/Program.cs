using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 异步编程Future
{
    class Program
    {
        public static void Main(string[] args)
        {
            People A = new People();
            new Task(async ()=>await A.DoSomethingAsync()).Start();
            OutputMsg("------------------------------------");
            Console.ReadLine();
        }
        public static void OutputMsg(string msg)
        {
            Console.WriteLine($"当前线程ID[{Thread.CurrentThread.ManagedThreadId.ToString()}] {msg}");
        }
    }
    public class People
    {
        public async Task DoSomethingAsync()
        {
            OutputMsg("DoSomethingAsync_Begin");
            await Task.Delay(3000);
            OutputMsg("DoSomethingAsync_Completed_1");
            await Task.Delay(4000).ConfigureAwait(false);
            OutputMsg("DoSomethingAsync_Completed_2");
        }
        public void OutputMsg(string msg)
        {
            Console.WriteLine($"当前线程ID[{Thread.CurrentThread.ManagedThreadId.ToString()}] {msg}");
        }
    }
   
}
