using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plato_saga
{
    public partial class Form10 : Form
    {
        String language = plato_saga.Properties.Settings.Default.app_lang;
        public String new_pass = String.Empty;
        public Boolean cancel = false;

        public Form10()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0)
            {
                if (language == "es") MessageBox.Show("Rellene todos los campos para continuar.");
                if (language == "en") MessageBox.Show("Please fill in all text fields.");
                return;
            }

            if (textBox1.Text.Length < 5 || textBox2.Text.Length < 5 || textBox3.Text.Length < 5)
            {
                if (language == "es") MessageBox.Show("La contraseña debe tener al menos 5 caracteres.");
                if (language == "en") MessageBox.Show("Password must be at least 5 characters in lenght.");
                return;
            }

            String pass_file =  System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "pass_file";
            String old_pass = "";
            if (!File.Exists(pass_file)) old_pass = "carrito";
            if (File.Exists(pass_file)) old_pass = File.ReadAllText(pass_file);

            if (textBox1.Text != old_pass)
            {
                if (language == "es") MessageBox.Show("La contraseña anterior es incorrecta.");
                if (language == "en") MessageBox.Show("Current password is incorrect.");
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                if (language == "es") MessageBox.Show("La contraseña nueva no coincide en ambos campos.");
                if (language == "en") MessageBox.Show("New passwords do not match.");
                return;
            }
            cancel = false;
            new_pass = textBox2.Text;
            File.WriteAllText(pass_file, new_pass);
            if (language == "es") MessageBox.Show("Contraseña modificada correctamente.","Contraseña cambiada",MessageBoxButtons.OK,MessageBoxIcon.Information);
            if (language == "en") MessageBox.Show("New password sucessfully set.", "Password changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture);
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }


        private void Form10_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form10));
            RefreshResources(this, resources);
            String pass_file = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "pass_file";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancel = true;
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 4) pic1.Visible = true;
            else pic1.Visible = false;

            if (textBox2.Text != textBox3.Text) pic2.Visible = false;
            else
            {
                pic1.Visible = true;
                pic2.Visible = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 4) pic2.Visible = true;
            else pic2.Visible = false;

            if (textBox3.Text != textBox2.Text) pic2.Visible = false;
            else
            {                
                pic2.Visible = true;
            }
        }
    }
}
