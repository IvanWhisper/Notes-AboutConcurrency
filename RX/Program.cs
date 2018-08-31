using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
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

            ScanAndWeigh();
            //EventTostream();
            //DifferenceBetweenSubscribeOnAndObserveOn();
            //TestWatcher();

            Console.ReadLine();

        }
        public static void ScanAndWeigh()
        {
            
            Weight wt = new Weight();
            var wtreport = Observable.FromEvent<Queue<double>>(handler => wt.Weigh += handler, handler => wt.Weigh -= handler);
            wtreport
                //转化器
                .Select(e=>Tuple.Create(e, Statistics.Variance(e)))
                //过滤器
                .Where(e=>e.Item2<0.02)
                //发布器
                .Subscribe(e => {
                    int i = 0;
                    foreach(var item in e.Item1)
                    {
                        Console.WriteLine($"{i}----{item}----{e.Item2}");
                        i++;
                    }
                });
            Task.Run(()=> {
                wt.OnWeigh(16.201);
                wt.OnWeigh(16.202);
                wt.OnWeigh(16.203);
                wt.OnWeigh(16.201);
                wt.OnWeigh(26.202);
                wt.OnWeigh(16.203);
                wt.OnWeigh(16.201);
                wt.OnWeigh(16.202);
                wt.OnWeigh(16.203);

                wt.OnWeigh(16.212);
                wt.OnWeigh(16.213);
                wt.OnWeigh(26.211);
                wt.OnWeigh(16.212);
                wt.OnWeigh(16.213);
                wt.OnWeigh(16.211);
                wt.OnWeigh(16.212);
                wt.OnWeigh(16.213);
                wt.OnWeigh(16.212);
                wt.OnWeigh(16.213);
                wt.OnWeigh(16.212);
                wt.OnWeigh(16.212);
                wt.OnWeigh(16.212);
                wt.OnWeigh(16.212);


            });
        }

        public static void EventTostream()
        {
            Speaker sp = new Speaker();
            var progressReports = Observable.FromEvent(hander => sp.Speak += hander, hander => sp.Speak -= hander);
            progressReports.Buffer(2).Subscribe(e=>Console.WriteLine(123));
            Task.Run(() => {
                sp.OnSpeak();
            });

        }

        public static void DifferenceBetweenSubscribeOnAndObserveOn()
        {
            Thread.CurrentThread.Name = "Main";

            IScheduler thread1 = new NewThreadScheduler(x => new Thread(x) { Name = "Thread1" });
            IScheduler thread2 = new NewThreadScheduler(x => new Thread(x) { Name = "Thread2" });

            Observable.Create<int>(o =>
            {
                Console.WriteLine("Subscribing on " + Thread.CurrentThread.Name);
                o.OnNext(1);
                return Disposable.Create(() => { });
            })
            //.SubscribeOn(thread1)
            //.ObserveOn(thread2)
            .Subscribe(x => Console.WriteLine("Observing '" + x + "' on " + Thread.CurrentThread.Name));
        }
        public static void UsingScheduler()
        {
            Console.WriteLine("Starting on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
            var source = Observable.Create<int>(
            o =>
            {
                Console.WriteLine("Invoked on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
                o.OnNext(1);
                o.OnNext(2);
                o.OnNext(3);
                o.OnCompleted();
                Console.WriteLine("Finished on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
                return Disposable.Empty;
            });
            source
            //.SubscribeOn(NewThreadScheduler.Default)
            //.SubscribeOn(ThreadPoolScheduler.Instance)
            .Subscribe(
            o => Console.WriteLine("Received {1} on threadId:{0}", Thread.CurrentThread.ManagedThreadId, o),
            () => Console.WriteLine("OnCompleted on threadId:{0}", Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Subscribed on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
        }
        public static void WatcherWithRX()
        {
            var watch =new FileSystemWatcher(@".\");
            watch.EnableRaisingEvents = true;

            Observable.FromEventPattern<FileSystemEventArgs>(watch, "Created")
                .Where(e => Path.GetExtension(e.EventArgs.FullPath).ToLower()==(".jpg"))
                .Subscribe(e =>
                {
                    Console.WriteLine(e.EventArgs.FullPath);
                });
        }
        public static void RXDemo01()
        {
            var list = Enumerable.Range(1, 10)
                .Where(x => x > 8)
                .Select(x => x.ToString())
                .First();
        }
        public static void TestWatcher()
        {
            var watch = new FileSystemWatcher(@".\");
            watch.Created += (s, e) =>
            {
                var fileType = Path.GetExtension(e.FullPath);
                if (fileType.ToLower().Equals(".jpg"))
                {
                    Console.WriteLine("done");
                }
            };
            watch.EnableRaisingEvents = true;
        }
        public static void TestSubject()
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
    public class Speaker
    {
        public event Action Speak;
        public void OnSpeak()
        {
            Speak();
        }
    }
    public class Weight
    {
        private Queue<double>datas = new Queue<double>();
        public event Action<Queue<double>> Weigh;
        public Weight()
        {
        }
        public void OnWeigh(double w)
        {
            datas.Enqueue(w);
            if (datas.Count >= 10)
            {
                Weigh(datas);
                datas.Dequeue();
            }
        }
    }
}