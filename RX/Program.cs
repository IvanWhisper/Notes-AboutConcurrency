using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RX
{
    class Program
    {
        static void Main(string[] args)
        {
            Subject<int> subject = new Subject<int>();
            var subscription = subject.Subscribe(
                                     x => Console.WriteLine("Value published: {0}", x),
                                     () => Console.WriteLine("Sequence Completed."));
            var subscription2 = subject.Subscribe(
                         x => Console.WriteLine("2 Value published: {0}", x),
                         () => Console.WriteLine("2 Sequence Completed."));

            subject.OnNext(1);

            subject.OnNext(2);
            Task.Run(() => {
                int i = 0;
                while (true)
                {
                    Thread.Sleep(1000);
                    subject.OnNext(i);
                    i++;
                    if (i > 10)
                        break;
                }
            });
            subscription.Dispose();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            subject.OnCompleted();
            subscription.Dispose();

            Console.ReadLine();
        }
        public static void OutputMsg(string msg)
        {
            Console.WriteLine($"当前线程ID[{Thread.CurrentThread.ManagedThreadId.ToString()}] {msg}");
        }

    }
    public class People
    {
        public static int Index;
        static People()
        {
            Index++;
        }
        public People()
        {
            _ID = Index;
        }
        private int _ID;
        public int ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
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