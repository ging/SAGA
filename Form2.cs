using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plato_saga
{
    public partial class Form2 : Form
    {
        static byte[] s_aditionalEntropy = { 7, 7, 5, 5, 1 };
        public string language = plato_saga.Properties.Settings.Default.app_lang;
        Boolean bad_pass = true;
        Form frmInfo = new Form();
        TextBox passwd = new TextBox();

        public Form2()
        {
            InitializeComponent();            
            
        }
        private Boolean obs_run = false;
        private void check_obs()
        {
            Process[] pname = Process.GetProcessesByName("obs64");
            if (pname.Length != 0)
            {
                MessageBox.Show("OBS Studio se está ejecutando, debe cerrarlo para continuar", "OBS Studio en ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error);
                obs_run = true;
            }
            else
            {
                obs_run = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (comboBox2.SelectedIndex == 0)
            {
                if (language == "es") MessageBox.Show("Seleccione primero la entidad a la que asociar el mensaje,", "Falta la entidad", MessageBoxButtons.OK);
                if (language == "en") MessageBox.Show("Please select your organization name.", "Organization missing", MessageBoxButtons.OK);
                comboBox2.Focus();
                return;
            }

            if (textBox5.Text == "")
            {
                if (language == "es") MessageBox.Show("El email de respuesta está en blanco.", "Falta email", MessageBoxButtons.OK);
                if (language == "en") MessageBox.Show("Reply email is blank.", "Email missing", MessageBoxButtons.OK);
                textBox5.Focus();
                return;
            }
            try
            {
                var eMailValidator = new System.Net.Mail.MailAddress(textBox5.Text);
            }
            catch
            {
                if (language == "es") MessageBox.Show("La dirección de email de respuesta no es válida", "Email no válido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("Reply email address is invalid.", "Invalid email address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return;
            }
            if (textBox1.Text == "")
            {
                if (language == "es") MessageBox.Show("El cuerpo del mensaje está en blanco.", "Falta el texto del mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("Email body is blank.", "Email contents blank", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }

            check_obs();
            if (obs_run == true)
            {
                return;
            }

            if (!Directory.Exists(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "obs-studio") + "\\" + "logs" + "\\" + "rep" + "\\"))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "obs-studio") + "\\" + "logs" + "\\" + "rep" + "\\");
            }

            var a = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (a == false)
            {
                MessageBox.Show("No se detectó conexión a Internet.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Create and send email
            
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("informesplatos@gmail.com", "Isabelenan00**");
            SmtpServer.EnableSsl = true;

            mail.From = new MailAddress("informesplatos@gmail.com");
            mail.To.Add("abel.carril@upm.es");
            if (language == "es")
            {
                mail.Subject = "Solicitud de asistencia del plató SAGA UPM " + comboBox2.Text;
                mail.Body = "Solicitud de soporte del plató" + Environment.NewLine + "Enviado por: " + textBox5.Text + Environment.NewLine + Environment.NewLine + textBox1.Text;
                button7.Text = "Enviando mensaje, espere por favor...";
            }
            if (language == "en")
            {
                mail.Subject = "Support request for SAGA-UPM " + comboBox2.Text;
                mail.Body = "Solicitud de soporte del plató" + Environment.NewLine + "Enviado por: " + textBox5.Text + Environment.NewLine + Environment.NewLine + textBox1.Text;
                button7.Text = "Sending message, please wait...";
            }            

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment(path);
            //mail.Attachments.Add(attachment);
            //attachment = new System.Net.Mail.Attachment(path2);
            //mail.Attachments.Add(attachment);
            
            button7.Enabled = false;
            try
            {
                SmtpServer.SendMailAsync(mail);
            }
            catch (Exception err)
            {
                if (language == "es")
                {
                    MessageBox.Show("Se produjo un error de red al enviar el mensaje." + Environment.NewLine + Environment.NewLine + err, "Error al enviar email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button7.Text = "Enviar solicitud de asistencia";
                }
                if (language == "en")
                {
                    MessageBox.Show("An error ocurred while sending message." + Environment.NewLine + Environment.NewLine + err, "Error al enviar email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button7.Text = "Send support request";
                }
            }
            button7.Enabled = true;
            
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            //String token = (string)e.UserState;

            if (e.Cancelled)
            {
                MessageBox.Show("[{0}] Envío cancelado.");

            }
            if (e.Error != null)
            {
                MessageBox.Show("Error al enviar el mensaje: " + e.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Solicitud de asistencia enviada correctamente. En breve recibirá respuesta en el email indicado.", "Mensaje enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActiveForm.Close();
            }

            button7.Text = "Enviar solicitud de asistencia";
            button7.Enabled = true;
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            var a = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (a == false)
            {
                MessageBox.Show("No se detectó conexión a Internet.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

        private void Form2_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            RefreshResources(this, resources);            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            check_obs();
            if (obs_run == true) return;            
            
            if (language == "es")
            {
                frmInfo.Name = "Acceso a función protegida";
                frmInfo.Text = "Acceso a función protegida";
            }
            if (language == "en")
            {
                frmInfo.Name = "Protected feature";
                frmInfo.Text = "Protected feature";
            }
            frmInfo.Icon = this.Icon;
            frmInfo.Icon = this.Icon;
            frmInfo.Height = 120;
            frmInfo.Width = 335;
            frmInfo.FormBorderStyle = FormBorderStyle.Fixed3D;
            frmInfo.MaximizeBox = false;
            frmInfo.MinimizeBox = false;

            Label lbl_titulo = new Label();
            lbl_titulo.Parent = frmInfo;
            lbl_titulo.Top = 20;
            lbl_titulo.Left = 14;
            lbl_titulo.Width = 290;
            if (language == "es") lbl_titulo.Text = "Introduzca la contraseña de acceso:";
            if (language == "en") lbl_titulo.Text = "Access password:";

            
            passwd.Parent = frmInfo;
            passwd.Top = 45;
            passwd.Left = 14;
            passwd.Width = 230;
            passwd.TabIndex = 0;
            passwd.UseSystemPasswordChar = true;
            passwd.BorderStyle = BorderStyle.Fixed3D;
            passwd.Text = String.Empty;

            Button boton_ok = new Button();

            boton_ok.Parent = frmInfo;
            boton_ok.Left = 247;
            boton_ok.Top = 44;
            boton_ok.Width = 60;
            boton_ok.Height = 22;
            boton_ok.Text = "Aceptar";
            boton_ok.Click += new EventHandler(boton_ok_Click);

            frmInfo.FormClosed += new FormClosedEventHandler(frmInfo_FormClosed);
            frmInfo.StartPosition = FormStartPosition.CenterScreen;
            frmInfo.ShowDialog();

            if (bad_pass == true) return;

            unlock_read_only();
            System.Threading.Thread.Sleep(150);

            String msg = "";
            String msg2 = "";
            if (language == "es")
            {
                msg = "¿Desea restaurar el sistema a valores de fábrica? NOTA: Las colecciones de escenas no exportadas se perderán.";
                msg2 = "Restaurar valores de fábrica";
            }
            if (language == "en")
            {
                msg = "Would you like to try performing a factory defaults reset? NOTE: All scene collections not previously exported will be lost.";
                msg2 = "Reset to factory defaults";
            }
            DialogResult a = MessageBox.Show(msg, msg2, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (a == DialogResult.No || a == DialogResult.Cancel)
            {
                Form2 frm_contacto = new Form2();
                frm_contacto.Icon = this.Icon;
                frm_contacto.ShowDialog();
            }
            else
            {
                reset_factory();
            }
        }

        private void boton_ok_Click(object sender, EventArgs e)
        {            
            Form.ActiveForm.Close();            
        }

        private void reset_factory()
        {
            String obs_prof_saved = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio-saved");
            String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Directory.Delete(obs_prof_or, true);
                new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(obs_prof_saved, obs_prof_or, true);
                if (language == "es") MessageBox.Show("Restauración a perfil de fábrica completada con éxito. Reinicie la aplicación para que los cambios surtan efecto.", "Restauración realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (language == "en") MessageBox.Show("Factory settings restore was successful. Please restart application for the new configuration to be loaded.", "Restore complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();

            }
            catch (Exception excpt)
            {
                if (language == "es") MessageBox.Show("Se produjo un error al restaurar el perfil de fábrica del plató. Contacte con soporte técnico." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error al restaurar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("An error ocurred restoring factory settings. Please contact support." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Arrow;
        }

        private void frmInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            bad_pass = true;
            if (passwd.Text != "carrito")
            {
                if (language == "es") MessageBox.Show("Contraseña incorrecta. Contacte con soporte audiovisual.", "Acceso a función no autorizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("Invalid password. Please contact support.", "Protected feature", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bad_pass = true;
            }
            else
            {
                bad_pass = false;
            }
        }

        private void unlock_read_only()
        {
            String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio")))
                try
                {
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio"));
                }
                catch (Exception excpt)
                {
                    if (language == "es") MessageBox.Show("No se puede crear la carpeta de la aplicación del plató, contacte con soporte técnico." + Environment.NewLine + excpt.Message, "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (language == "en") MessageBox.Show("No se puede crear la carpeta de la aplicación del plató, contacte con soporte técnico." + Environment.NewLine + excpt.Message, "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                        
            foreach (String dir in Directory.GetDirectories(obs_prof_or))
            {
                var di = new DirectoryInfo(dir);
                di.Attributes &= FileAttributes.Normal;
            }

            System.IO.File.SetAttributes(obs_prof_or, System.IO.FileAttributes.Normal);

            foreach (String file in Directory.GetFiles(obs_prof_or, "*.ini", SearchOption.AllDirectories))
            {
                try
                {
                    System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);
                }
                catch (Exception excpt)
                {
                 
                    if (language == "es")
                    {
                        MessageBox.Show("Error al desbloquear: " + file + Environment.NewLine + Environment.NewLine + excpt.Message);
                        
                    }
                    if (language == "en")
                    {
                        
                        MessageBox.Show("Error unlocking: " + file + Environment.NewLine + Environment.NewLine + excpt.Message);
                    }                    
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process proc = new Process();

            if (System.IO.File.Exists(Path.Combine(Application.StartupPath, "Soporte_Plato_UPM.pdf")))
            {

                foreach (Control ct in this.Controls)
                {
                    ct.Enabled = false;
                }

                if (language == "es") proc.StartInfo.FileName = (Path.Combine(Application.StartupPath, "Soporte_Plato_UPM.pdf"));
                if (language == "en") proc.StartInfo.FileName = (Path.Combine(Application.StartupPath, "Soporte_Plato_UPM_EN.pdf"));
                proc.Start();

                foreach (Control ct in this.Controls)
                {
                    ct.Enabled = true;
                }                
            }
            else
            {                
                if (language == "es") MessageBox.Show("No se encontró el programa de configuración de la capturadora de vídeo Blackmagic.", "No se encontró la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("Blackmagic capture device application not found.", "Blackmagic application not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
