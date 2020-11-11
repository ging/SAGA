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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.P)
            {
                lbl_parada.Text = "Pulsación de pedal detectada";
                MessageBox.Show("Pedal funciona correctamente");
            }
        }

        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture);
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            RefreshResources(this, resources);
            pic.Image = imgl.Images[1];
            pic2.Image = imgl.Images[1];
            pic3.Image = imgl.Images[1];
            pic4.Image = imgl.Images[1];
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.P)
            {
                lbl_parada.Text = "Pedal detectado";
                pic.Image = imgl.Images[0];
            }

            if (e.Control && e.Shift && e.KeyCode == Keys.M)
            {
                lbl_ponente.Text = "Pedal detectado";
                pic2.Image = imgl.Images[0];
            }
            if (e.Control && e.Shift && e.KeyCode == Keys.F)
            {
                lbl_1v_pres.Text = "Pedal detectado";
                pic3.Image = imgl.Images[0];
            }
            if (e.Control && e.Shift && e.KeyCode == Keys.R)
            {
                lbl_solo_pres.Text = "Pedal detectado";
                pic4.Image = imgl.Images[0];
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.P)
            {
                lbl_parada.Text = "Pedal detectado";
                pic.Image = imgl.Images[0];
            }

            if (e.Control && e.Shift && e.KeyCode == Keys.M)
            {
                lbl_ponente.Text = "Pedal detectado";
                pic2.Image = imgl.Images[0];
            }
            if (e.Control && e.Shift && e.KeyCode == Keys.F)
            {
                lbl_1v_pres.Text = "Pedal detectado";
                pic3.Image = imgl.Images[0];
            }
            if (e.Control && e.Shift && e.KeyCode == Keys.R)
            {
                lbl_solo_pres.Text = "Pedal detectado";
                pic4.Image = imgl.Images[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }
    }
}
