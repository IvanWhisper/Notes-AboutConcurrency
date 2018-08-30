using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RX_Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnFirstEventMode.Click += btnFirstEventMode_Click;

            //得到了Button的Click事件流。
            var clickedStream = Observable.FromEventPattern<EventArgs>(btnFirstReactiveMode, "Click");
            //在事件流上注册了一个观察者。 
            clickedStream
                .Throttle(TimeSpan.FromSeconds(3))
                .Subscribe(e => MessageBox.Show("Hello world"));

            var increasedEventStream = Observable.FromEventPattern<EventArgs>(btnIncreasement,"Click")
                .Select(_ => 1);
            var decreasedEventStream =Observable.FromEventPattern<EventArgs>(btnDecrement, "Click")
                .Select(_ => -1);

            increasedEventStream
                .Merge(decreasedEventStream)
                .Scan(0, (result, s) => result + s)
                .Subscribe(x => lblResult.Text = x.ToString());
        }


        private void btnFirstEventMode_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hello world");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
