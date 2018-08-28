using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace 数据流编程TPLDataFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            new DataFlowTest().Running() ;
            Console.ReadLine();
        }
    }
    public class DataFlowTest
    {
        private int count1;
        private int count2;
        private int count3;
        private int count4;
        private int count5;
        private int rcount1;
        private int rcount2;
        public void Running()
        {
            var bufferBlock = new BufferBlock<int>();
            var post01 = Task.Run(() =>
            {
                for (int i = 1; i < 1000; i++)
                {
                    bufferBlock.Post(i);
                    count1++;
                    Console.WriteLine("Count1次数：" + count1);
                    Task.Delay(9);
                }
            });
            var receive1 = Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine(bufferBlock.Receive());
                    rcount1++;
                }
            });
            var receive2 = Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine(bufferBlock.Receive());
                    rcount2++;
                }
            });
            var post2 = Task.Run(() =>
            {
                for (int i = 1; i < 1000; i++)
                {
                    bufferBlock.Post(i);
                    count2++;
                    Console.WriteLine("Count2次数:" + count2);
                    Task.Delay(7);
                }
            });
            var post3 = Task.Run(() =>
            {
                for (int i = 1; i < 1000; i++)
                {
                    bufferBlock.Post(i);
                    count3++;
                    Console.WriteLine("Count3次数:" + count3);
                    Task.Delay(8);
                }
            });
            var post4 = Task.Run(() =>
            {
                for (int i = 1; i < 1000; i++)
                {
                    bufferBlock.Post(i);
                    count4++;
                    Console.WriteLine("Count4次数:" + count4);
                    Task.Delay(6);
                }
            });
            var post5 = Task.Run(() =>
            {
                for (int i = 1; i < 1000; i++)
                {
                    bufferBlock.Post(i);
                    count5++;
                    Console.WriteLine("Count5次数:" + count5);
                    Task.Delay(10);
                }
            });

            Task.WhenAll(post01, receive1, receive2, post2, post3, post4, post5);
        }

        }
}
