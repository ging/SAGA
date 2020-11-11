using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plato_saga
{
    public partial class Form9 : Form
    {
        Boolean keep_file = true;
        public Boolean keep_file_p
        {
            get { return keep_file; }
            set { keep_file = value; }
        }        

        public Form9()
        {            
            InitializeComponent();      
        }


        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture);
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form9));
            RefreshResources(this, resources);            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            keep_file = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            keep_file = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            keep_file = true;
            this.Close();
        }
    }
}
