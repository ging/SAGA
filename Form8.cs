using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plato_saga
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        int time_record = -1;
        private void Form8_Load(object sender, EventArgs e)
        {
            BackColor = Color.Black;
            TransparencyKey = Color.Black;
            timer_recorded.Start();
        }

        private void timer_recorded_Tick(object sender, EventArgs e)
        {
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                        
            time_record = time_record + 1;
            TimeSpan t = TimeSpan.FromSeconds(time_record);
            String tx_elapsed = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                t.Hours,
                t.Minutes,
                t.Seconds);
                this.Invoke(new MethodInvoker(delegate
                {
                    lbl_elapsed.Text = tx_elapsed;
                    lbl_elapsed.Refresh();
                    Text = tx_elapsed;
                }));
            }).Start();
        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            time_record = 0;
            timer_recorded.Stop();
        }
    }
}
