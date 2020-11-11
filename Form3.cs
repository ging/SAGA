using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plato_saga
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btn_abort.Enabled = true;
        }

        private void btn_abort_Click(object sender, EventArgs e)
        {
            btn_abort.Enabled = false;
            pic.Enabled = false;
            int num = 0;
            Process[] localByName = Process.GetProcessesByName("obs64");
            num = localByName.Length;

            if (num > 0)
            {
                foreach (Process p in localByName)
                {
                    try
                    {
                        Task t = Task.Run(() =>
                        {
                            WindowHelper.BringProcessToFront(p);
                            p.CloseMainWindow();
                            p.WaitForExit();

                        });
                        if (!t.Wait(3000))
                        {
                            p.Kill();
                        }
                    }
                    catch
                    {
                        Form1 frm1 = new Form1();
                        if (frm1.language == "es") MessageBox.Show("Error al cerrar OBS Studio. Debe cerrarlo para continuar: " + p.Id, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (frm1.language == "en") MessageBox.Show("An error occurred while closing OBS Studio. You need to close it to continue: " + p.Id, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                              
                        this.Enabled = true;
                        return;
                    }
                }
            }            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            RefreshResources(this, resources);
            timer1.Start();
        }

        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture);
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }
    }
}
