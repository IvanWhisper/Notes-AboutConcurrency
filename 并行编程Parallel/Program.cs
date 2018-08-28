using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 并行编程Parallel
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<People> ps = new List<People>() {
                new People(),
                new People(),
                new People(),
                new People(),
                new People(),
                new People(),
                new People(),
                new People(),
                new People(),
                new People(),
            };
            Parallel.ForEach(ps,new ParallelOptions() { MaxDegreeOfParallelism = 20 },
            async x =>await x.DoSomethingAsync());
            //new Task(async () => await A.DoSomethingAsync()).Start();
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
        public People()
        {
        }
        public async Task DoSomethingAsync()
        {
            OutputMsg("DoSomethingAsync_Begin");
            await Task.Delay(4000);
            OutputMsg("DoSomethingAsync_Completed_1");
            await Task.Delay(2000).ConfigureAwait(false);
            OutputMsg("DoSomethingAsync_Completed_2");
        }
        public void OutputMsg(string msg)
        {
            Console.WriteLine($"当前线程ID[{Thread.CurrentThread.ManagedThreadId.ToString()}] {msg}");
        }
    }
}
