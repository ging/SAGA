using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plato_saga
{
    public partial class Form11 : Form
    {
        public string language = plato_saga.Properties.Settings.Default.app_lang;
        Form frmInfo = new Form();
        Boolean bad_pass = true;
        TextBox passwd = new TextBox();
        Boolean obs_run = false;
        public Boolean restored = false;
        String passwd_access = "";
        public Boolean update_now = false;
        public String content1 = String.Empty;
        public Boolean changed_lang = false;

        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            restored = false;
            update_now = false;
            frmInfo.FormClosed += new FormClosedEventHandler(frmInfo_FormClosed);
            check_pass();

            btn_update.Invoke(new MethodInvoker(delegate
            {
                btn_update.Text = "Version " + Application.ProductVersion;
            }));
        }

        private void check_updates()
        {
            String current_ver = btn_update.Text;
            btn_update.Refresh();            

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;

                try
                {                    
                    WebClient client = new WebClientWithTimeout();
                    String lang_check_update = "";
                    if (language == "es") lang_check_update = "https://drive.upm.es/index.php/s/dIfk1LM0rYkVoBF/download";
                    if (language == "en") lang_check_update = "https://drive.upm.es/index.php/s/BIRLKz7kbLokLqg/download";
                    Stream stream = client.OpenRead(lang_check_update);
                    StreamReader reader = new StreamReader(stream);
                    String content = reader.ReadToEnd();
                    content1 = content;

                }
                catch (Exception excpt)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Enabled = false;
                    }));

                    if (language == "es") MessageBox.Show("Hubo un error al conectar al servidor de actualizaciones." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (language == "en") MessageBox.Show("An error occurred conecting to update service." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btn_update.Invoke(new MethodInvoker(delegate
                    {
                        btn_update.Text = current_ver;
                    }));                    
                    return;
                }

                try
                {
                    if (Convert.ToInt16(content1.Replace(".", String.Empty).Substring(0, 3)) > Convert.ToInt16(Application.ProductVersion.Replace(".", String.Empty)))
                    {
                        DialogResult a = DialogResult.None;

                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Enabled = false;
                        }));

                        if (language == "es") a = MessageBox.Show("Una nueva versión está disponible: " + content1.Substring(0, 5) + Environment.NewLine + content1.Substring(6, content1.Length - 6) + Environment.NewLine + Environment.NewLine + "¿Desea descargarla?", "Nueva versión disponible", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (language == "en") a = MessageBox.Show("A new version was found : " + content1.Substring(0, 5) + Environment.NewLine + content1.Substring(6, content1.Length - 6) + Environment.NewLine + Environment.NewLine + "Do you want to download it?", "New version found", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (a == DialogResult.Yes)
                        {
                            update_now = true;
                            this.Invoke(new MethodInvoker(delegate
                            {
                                this.Enabled = true;
                                this.Close();
                            }));                            
                        }
                        else
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                this.Enabled = true;
                                this.Activate();
                            }));
                        }
                    }
                    else
                    {
                        if (language == "es") MessageBox.Show("Está usando la versión más reciente.");
                        if (language == "en") MessageBox.Show("You are using the latest version.");
                    }
                }
                catch {
                    if (language == "es") MessageBox.Show("Ocurrió un error al buscar actualizaciones.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (language == "en") MessageBox.Show("An error ocurred while obtaining updates.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                

            }).Start();
        }

        private void check_pass()
        {
            String pass_file = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "pass_file";
            if (!File.Exists(pass_file)) File.WriteAllText(pass_file, "carrito");
            passwd_access = File.ReadAllText(pass_file);
        }

        private void chk_updates_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_updates.CheckState == CheckState.Checked)
            {
                plato_saga.Properties.Settings.Default.auto_updates = true;
            }
            else
            {
                plato_saga.Properties.Settings.Default.auto_updates = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void chk_crono_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_crono.CheckState == CheckState.Checked)
            {
                plato_saga.Properties.Settings.Default.show_timer = true;
            }
            else
            {
                plato_saga.Properties.Settings.Default.show_timer = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void chk_validate_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_validate.CheckState == CheckState.Checked)
            {
                plato_saga.Properties.Settings.Default.validate_scene = true;
            }
            else
            {
                plato_saga.Properties.Settings.Default.validate_scene = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void chk_show_keep_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_show_keep.CheckState == CheckState.Checked)
            {
                plato_saga.Properties.Settings.Default.show_keep_file = true;
            }
            else
            {
                plato_saga.Properties.Settings.Default.show_keep_file = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void chk_auto_close_obs_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_auto_close_obs.CheckState == CheckState.Checked)
            {
                plato_saga.Properties.Settings.Default.close_obs_auto = true;
            }
            else
            {
                plato_saga.Properties.Settings.Default.close_obs_auto = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void btn_blackm_Click(object sender, EventArgs e)
        {
            Process procfile = new Process();
            String path_black = String.Empty;
            if (System.IO.File.Exists(Path.Combine("C:\\Program Files (x86)\\Blackmagic Design\\Blackmagic Desktop Video", "BlackmagicDesktopVideoSetup.exe")))
            {
                path_black = Path.Combine("C:\\Program Files (x86)\\Blackmagic Design\\Blackmagic Desktop Video", "BlackmagicDesktopVideoSetup.exe");
            }
            if (System.IO.File.Exists(Path.Combine("C:\\Program Files\\Blackmagic Design\\Blackmagic Desktop Video", "BlackmagicDesktopVideoSetup.exe")))
            {
                path_black = Path.Combine("C:\\Program Files\\Blackmagic Design\\Blackmagic Desktop Video", "BlackmagicDesktopVideoSetup.exe");
            }
            if (System.IO.File.Exists(Path.Combine("C:\\Program Files\\Blackmagic Design\\Desktop Video\\Blackmagic", "DesktopVideoSetup.exe")))
            {
                path_black = Path.Combine("C:\\Program Files\\Blackmagic Design\\Desktop Video\\Blackmagic", "DesktopVideoSetup.exe");
            }
            if (path_black != String.Empty)
            {
                try
                {
                    Process.Start(path_black);
                    return;
                }
                catch
                {

                }
            }
            else
            {
                if (language == "es") MessageBox.Show("No se encontró el programa de configuración de la capturadora de vídeo Blackmagic.", "No se encontró la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("Blackmagic capture device application not found.", "Blackmagic application not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            check_obs();
            if (obs_run == true) return;
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio")))
            {
                if (language == "es") MessageBox.Show("No se encontró una configuración válida de plató o fue eliminada accidentalmente. Intente restaurar el perfil de fábrica." + Environment.NewLine + Environment.NewLine + "Si el problema no se soluciona, contacte con la asistencia técnica para reparar el equipo.", "No se encontró perfil válido en el sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("No valid studio configuration was found, or it was removed. You may try restoring factory settings." + Environment.NewLine + Environment.NewLine + "Si el problema no se soluciona, contacte con la asistencia técnica para reparar el equipo.", "No valid obs profile found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult a = DialogResult.None;
            if (language == "es") a = MessageBox.Show("La copia de seguridad debe hacerse desde un perfil que funcione correctamente. ¿Desea continuar?", "Advertencia sobre backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (language == "en") a = MessageBox.Show("Factory settings backup should be performed with a working profile. Do you want to continue?", "Factory settings backup warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (a == DialogResult.No) return;

            String obs_prof_bck = String.Empty;
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (language == "es") folder.Description = "Seleccione la carpeta en la que se guardará el backup";
            if (language == "en") folder.Description = "Select backup destination path";

            folder.ShowDialog();
            if (folder.SelectedPath != String.Empty)
            {
                obs_prof_bck = Path.Combine(folder.SelectedPath, "obs-studio");
            }
            else
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Directory.CreateDirectory(Path.Combine(folder.SelectedPath, "obs-studio"));
            }
            catch (Exception exc)
            {
                if (language == "es") MessageBox.Show("Se produjo un error al crear la carpeta de destino. " + Environment.NewLine + exc, "No se pudo crear carpeta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("An error occurred creating destination folder. " + Environment.NewLine + exc, "Error creating folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Arrow;
                return;
            }

            String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");

            foreach (Control ct in this.Controls)
            {
                ct.Enabled = false;
            }

            new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(obs_prof_or, obs_prof_bck, true);
            this.Cursor = Cursors.Arrow;
            foreach (Control ct in this.Controls)
            {
                ct.Enabled = true;
            }

            if (language == "es") MessageBox.Show("Backup realizado con éxito", "Respaldo completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (language == "en") MessageBox.Show("Backup successfully completed", "Backup complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void check_obs()
        {
            Process[] pname = Process.GetProcessesByName("obs64");
            if (pname.Length != 0)
            {
                if (language == "es") MessageBox.Show("OBS Studio se está ejecutando, debe cerrarlo para continuar", "OBS Studio en ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("OBS Studio is already running, you need to close it to continue.", "OBS Studio is running", MessageBoxButtons.OK, MessageBoxIcon.Error);
                obs_run = true;
            }
            else
            {
                obs_run = false;
            }
        }

        private void btn_restore_Click(object sender, EventArgs e)
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

            frmInfo.StartPosition = FormStartPosition.CenterScreen;
            frmInfo.ShowDialog();

            if (bad_pass == true) return;

            //unlock_read_only();
            System.Threading.Thread.Sleep(150);

            String obs_prof_saved = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio-saved");
            String obs_prof_bck = String.Empty;
            String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");

            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (language == "es") folder.Description = "Seleccione la carpeta que contiene el backup (obs-studio)";
            if (language == "en") folder.Description = "Select backup path (obs-studio)";

            folder.ShowDialog();
            if (folder.SelectedPath != String.Empty)
            {
                obs_prof_bck = folder.SelectedPath;
            }
            else
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            if (Directory.Exists(Path.Combine(folder.SelectedPath, "obs-studio")))
            {
                obs_prof_bck = Path.Combine(folder.SelectedPath, "obs-studio");
            }

            if (!System.IO.File.Exists(Path.Combine(obs_prof_bck, "global.ini")))
            {
                DialogResult a = DialogResult.None;
                if (language == "es") a = MessageBox.Show("No se encontró un backup válido en la carpeta seleccionada." + Environment.NewLine + Environment.NewLine + "¿Desea restaurar el perfil original guardado durante la puesta en marcha del sistema?", "Backup no encontrado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (language == "en") a = MessageBox.Show("No valid backup was found on selected folder." + Environment.NewLine + Environment.NewLine + "¿Do you want to restore to factory settings?. NOTE: This will remove all current scenes.", "Backup not found", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (a == DialogResult.No || a == DialogResult.Cancel)
                {
                    this.Cursor = Cursors.Arrow;
                    return;
                }
                else
                {
                    try
                    {
                        Directory.Delete(obs_prof_or, true);
                        new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(obs_prof_saved, obs_prof_or, true);
                        if (language == "es") MessageBox.Show("Restauración a perfil de fábrica completada con éxito", "Restauración realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (language == "en") MessageBox.Show("Factory settings restore was successful", "Restore complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Arrow;
                        restored = true;
                    }
                    catch (Exception excpt)
                    {
                        if (language == "es") MessageBox.Show("Se produjo un error al restaurar el perfil de fábrica del plató. Contacte con soporte técnico." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error al restaurar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (language == "en") MessageBox.Show("An error ocurred restoring factory settings. Please contact support." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Arrow;
                    }
                    return;
                }
            }

            foreach (Control ct in this.Controls)
            {
                ct.Enabled = false;
            }


            try
            {
                Directory.Delete(obs_prof_or, true);
                new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(obs_prof_bck, obs_prof_or, true);

                if (language == "es") MessageBox.Show("Restauración completada con éxito", "Restauración completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (language == "en") MessageBox.Show("Restore sucessfully completed", "Restore complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception excpt)
            {
                if (language == "es") MessageBox.Show("Se produjo un error al restaurar el perfil de OBS." + Environment.NewLine + excpt.Message, "Error al restaurar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("An error occurred while restoring OBS profile." + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Arrow;
            }
            finally
            {
                foreach (Control ct in this.Controls)
                {
                    ct.Enabled = true;
                }
            }
        }

        private void boton_ok_Click(object sender, System.EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void frmInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            bad_pass = true;
            if (passwd.Text != passwd_access)
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

        private void btn_update_Click(object sender, EventArgs e)
        {
            check_updates();
        }
        //END CODE

        public class WebClientWithTimeout : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest wr = base.GetWebRequest(address);
                wr.Timeout = 5000; // timeout in milliseconds (ms)
                return wr;
            }
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            Form10 frm10 = new Form10();
            frm10.ShowDialog();
            if (frm10.cancel == false) check_pass();
        }

        private void combo_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_lang.SelectedIndex == 0)
            {
                plato_saga.Properties.Settings.Default.app_lang = "es";
                language = "es";
                plato_saga.Properties.Settings.Default.Save();

            }
            if (combo_lang.SelectedIndex == 1)
            {
                plato_saga.Properties.Settings.Default.app_lang = "en";
                language = "en";
                plato_saga.Properties.Settings.Default.Save();
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form11));
            RefreshResources(this, resources);
            changed_lang = true;
            //create_tips();
            //show_devs_panel();
            btn_update.Text = "Version " + Application.ProductVersion;
        }

        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture);
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }

        private void btn_joiner_Click(object sender, EventArgs e)
        {
            String join = Application.StartupPath + "\\" + "FFBatch_UPM" + "\\" + "FFbatch_UPM.exe";

            if (!File.Exists(join))
            {
                if (language == "es") MessageBox.Show("No se encontró la aplicación para unir vídeos. Reinstale la aplicatión para solucionar el problema.", "Falta un archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("Concatenation video application was not found. Please reinstall application.", "File missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else
            {
                Process.Start(join);
            }
        }

        private void btn_joiner_Click_1(object sender, EventArgs e)
        {
            String join = Application.StartupPath + "\\" + "FFBatch_UPM" + "\\" + "FFbatch_UPM.exe";

            if (!File.Exists(join))
            {
                if (language == "es") MessageBox.Show("No se encontró la aplicación para unir vídeos. Reinstale la aplicatión para solucionar el problema.", "Falta un archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("Concatenation video application was not found. Please reinstall application.", "File missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else
            {
                Process.Start(join);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
