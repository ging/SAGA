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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();            
        }

        public string num_val;
        public Boolean cancelado = false;
        Int32 interval = 0;
        
        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Start();
            interval = Convert.ToInt32(num_val);
            label2.Text = interval.ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            interval = interval - 1;
             label2.Text = interval.ToString();
            label2.Refresh();
            if (interval == 0)
            {
                timer1.Stop();
                ActiveForm.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cancelado = true;
            timer1.Stop();
            ActiveForm.Close();
        }
    }
}
