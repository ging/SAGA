using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Threading;
//using IWshRuntimeLibrary;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.Globalization;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Runtime.InteropServices;
using System.Drawing;
using System.ComponentModel;

namespace plato_saga
{
    public partial class Form1 : Form
    {
        // list of video devices
        int file_n = 0;
        FilterInfoCollection videoDevices;
        // stop watch for measuring fps
        Boolean start_up = true;
        private Stopwatch stopWatch = null;
        String update_file = String.Empty;
        Boolean obs_run = false;
        Boolean bad_pass = true;
        String pass_txt = String.Empty;
        String path = String.Empty;
        String path2 = String.Empty;
        TextBox passwd = new TextBox();
        Form frmInfo = new Form();
        public Boolean locked = false;
        String path_combo = String.Empty;
        String scene_global = String.Empty;
        String scene_global_name = String.Empty;
        TextBox file_name_clone = new TextBox();
        String scene_clone = String.Empty;
        String num_delay = "0";
        Boolean first_run = true;
        private Boolean duplicating = false;
        public Boolean cancel_count = false;
        Boolean bad_col = false;
        float peak_audio = 0;
        float threshold_aud = 0.001f;
        Boolean obs_launched = false;
        String aud_sel_ID = String.Empty;
        Boolean aud_match = false;
        Boolean mon_audio = true;
        Boolean changed_lang = false;
        Form11 frm_11 = new Form11();
        public string language = plato_saga.Properties.Settings.Default.app_lang;
        public string obs_path = plato_saga.Properties.Settings.Default.obs_path;
        public bool show_timer = plato_saga.Properties.Settings.Default.show_timer;
        public bool auto_updates = plato_saga.Properties.Settings.Default.auto_updates;
        public bool show_keep_file = plato_saga.Properties.Settings.Default.show_keep_file;
        public bool close_obs_auto = plato_saga.Properties.Settings.Default.close_obs_auto;
        public int silence_level = plato_saga.Properties.Settings.Default.silence_level;
        public bool show_devs = plato_saga.Properties.Settings.Default.show_panel;
        public Font pr_font = plato_saga.Properties.Settings.Default.pr_font;
        public Color pr_color = plato_saga.Properties.Settings.Default.pr_color;
        public Boolean to_pr = plato_saga.Properties.Settings.Default.to_prompt;
        public int pr_speed = plato_saga.Properties.Settings.Default.prompt_sp;
        public String pr_text = plato_saga.Properties.Settings.Default.prompt_txt;
        public Boolean pr_delay = plato_saga.Properties.Settings.Default.pr_delay;
        public int pr_delay_val = plato_saga.Properties.Settings.Default.pr_delay_val;

        WebClient cli = new WebClient();
        String obs_exec = String.Empty;
        plato_saga.Form6 form_intro = new plato_saga.Form6();
        int valid_vids = 0;
        plato_saga.Form4 frm_pedal = new plato_saga.Form4();
        Boolean closed_ok = false;
        Boolean recording = false;
        Boolean testing_pedal = false;
        Boolean loading_obs = false;
        int time_record = 0;

        plato_saga.Form5 frm_cuenta = new plato_saga.Form5();
        plato_saga.Form8 frm_timer = new plato_saga.Form8();
        public plato_saga.Form12 frm_tele = new plato_saga.Form12();
        plato_saga.Form13 frm_prompt = new plato_saga.Form13();

        List<string> previewed_scenes = new List<string>();
        String dest_update = String.Empty;
        DateTime _startedAt;
        Rectangle resolution = Screen.PrimaryScreen.Bounds;
        String def_aud = String.Empty;
        MMDevice device;
        ComboBox Combo_scene = new ComboBox();
        String last_file = String.Empty;
        String base_update_server = "https://github.com/ging/platosaga/releases/download";
        Boolean download_progressing = false;
        String passwd_access = "";
        ToolTip T00 = new ToolTip();
        ToolTip T01 = new ToolTip();
        ToolTip T02 = new ToolTip();
        
        private bool headOnly;
        public bool HeadOnly
        {
            get { return headOnly; }
            set { headOnly = value; }
        }

        public struct Rect1
        {
            public int W_Left { get; set; }
            public int W_Top { get; set; }
            public int W_Right { get; set; }
            public int W_Bottom { get; set; }
        }

        public String num_delay_string
        {
            get { return num_delay; }
            set { num_delay = value; }
        }     

        //Estado de ventana
        //[DllImport("user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect1 rectangle);

        //Tamaño OBS
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        // P/Invoke
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
           int Y, int cx, int cy, uint uFlags);

        //Tecla global de parada ha sido pulsada

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        public Form1()
        {
            InitializeComponent();
            

            if (language == "es") form_intro.label_intro.Text = "Cargando dispositivos, espere por favor...";
            if (language == "en") form_intro.label_intro.Text = "Loading capture devices, please wait...";
            form_intro.Show();
            form_intro.Refresh();
            int id = 0;     // The id of the hotkey.            
            RegisterHotKey(this.Handle, id, (int)KeyModifier.Control + (int)KeyModifier.Shift, Keys.P.GetHashCode());       // Register Shift + A as global hotkey.
            
            int id1 = 2;
            int id2 = 3;
            int id3 = 4;
            RegisterHotKey(this.Handle, id2, (int)KeyModifier.Control, Keys.Space.GetHashCode());       // Register Shift + A as global hotkey.
            RegisterHotKey(this.Handle, id2, (int)KeyModifier.Control + (int)KeyModifier.Shift, Keys.Up.GetHashCode());       // Register Shift + A as global hotkey.
            RegisterHotKey(this.Handle, id3, (int)KeyModifier.Control + (int)KeyModifier.Shift, Keys.Down.GetHashCode());       // Register Shift + A as global hotkey.
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                /* Note that the three lines below are not needed if you only want to register one hotkey.
                 * The below lines are useful in case you want to register multiple keys, which you can use a switch with the id as argument, or if you want to know which key/modifier was pressed for some particular reason. */

                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.
                
                if (modifier == KeyModifier.Control && key == Keys.Space)
                {
                    try
                    {
                        frm_tele.Invoke(new MethodInvoker(delegate
                        {

                            if (frm_tele.t_scroll.Enabled == false)
                            {
                                frm_tele.t_scroll.Enabled = true;
                            }
                            else
                            {
                                frm_tele.t_scroll.Enabled = false;
                            }
                        }));
                    }
                    catch { }
                    return;
                }

                if (key == Keys.Up)
                {
                    if (frm_tele.t_scroll.Interval > 30)
                    {
                        try
                        {
                            frm_tele.Invoke(new MethodInvoker(delegate
                            {
                                frm_tele.t_scroll.Interval = frm_tele.t_scroll.Interval - 10;
                            }));
                        }
                        catch { }
                    }
                    return;
                }
                if (key == Keys.Down)
                    {

                    if (frm_tele.t_scroll.Interval < 210)
                    {

                        try
                        {
                            frm_tele.Invoke(new MethodInvoker(delegate
                                {
                                    frm_tele.t_scroll.Interval = frm_tele.t_scroll.Interval + 10;
                                }));
                        }
                        catch { }
                    }
                    return;
                }

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.Name == "Form9")
                    {
                        frm.Invoke(new MethodInvoker(delegate
                        {
                            frm.Close();
                        }));

                        return;
                    }
                }
                frm_timer.timer_recorded.Stop();

                if (testing_pedal == false && loading_obs == false && frm_11.chk_auto_close_obs.CheckState == CheckState.Checked)
                {
                    
                    Process[] localByName = Process.GetProcessesByName("obs64");
                    int num = localByName.Length;

                    if (num > 0)
                    {
                        this.Enabled = false;
                        Form3 frm_prog = new Form3();

                        new System.Threading.Thread(() =>
                        {
                            System.Threading.Thread.CurrentThread.IsBackground = true;
                            frm_prog.Show();
                            frm_prog.TopMost = true;
                            frm_prog.TopMost = false;
                            frm_prog.Refresh();

                        }).Start();

                        lbl_obs_running.Text = "Saliendo de OBS Studio, espere por favor...";
                        lbl_obs_running.Refresh();
                        foreach (Process p in localByName)
                        {
                            try
                            {
                                Task t = Task.Run(() =>
                                {
                                    if (p.Id == proc.Id)
                                    {

                                        int recorders = 1;                                        
                                        while (recorders > 0)
                                        {
                                            Process[] obs_recording2 = Process.GetProcessesByName("obs-ffmpeg-mux");
                                            recorders = obs_recording2.Length;
                                            Thread.Sleep(500);
                                        }
                                        Thread.Sleep(1000);
                                        WindowHelper.BringProcessToFront(p);
                                        p.CloseMainWindow();
                                        p.WaitForExit();
                                        if (p.HasExited == true)
                                        {
                                            closed_ok = true;
                                        }
                                        else
                                        {
                                            Process[] localByName2 = Process.GetProcessesByName("obs64");
                                            int num2 = localByName.Length;

                                            if (num2 > 0)
                                            {
                                                foreach (Process p2 in localByName2)
                                                {
                                                    WindowHelper.BringProcessToFront(p2);
                                                    p2.CloseMainWindow();
                                                    p2.WaitForExit();
                                                }
                                            }
                                        }
                                    }

                                });
                                if (!t.Wait(9000))
                                {
                                    if (language == "es")
                                    {
                                        DialogResult a = MessageBox.Show("No se pudo salir de OBS Studio. ¿Desea forzar su cierre?", "OBS studio no responde", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                        if (a == DialogResult.Yes) p.Kill();
                                        else
                                        {
                                            MessageBox.Show("No se puede cerrar OBS Studio. Intente cerrarlo manualmente o reinicie el equipo.", "OBS studio no respondió", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    if (language == "en")
                                    {
                                        DialogResult a = MessageBox.Show("OBS Studio could not be properly closed. ¿Do you want to force quit it?", "OBS studio not responding", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                        if (a == DialogResult.Yes) p.Kill();
                                        else
                                        {
                                            MessageBox.Show("OBS Studio could not be closed. Try closing it manually or restart computer.", "OBS not responding", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }

                                    closed_ok = false;
                                }
                            }
                            catch
                            {
                                closed_ok = false;
                                if (language == "es") MessageBox.Show("Error al cerrar OBS Studio. Debe cerrarlo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (language == "en") MessageBox.Show("Error closing OBS Studio. You need to close it to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Cursor = Cursors.Arrow;
                            }
                        }
                        
                            frm_prog.Invoke(new MethodInvoker(delegate
                            {
                                frm_prog.Dispose();
                            }));                      
                        this.Enabled = true;
                    }
                    else
                    {
                        if (ApplicationIsActivated() == false)
                        {
                            this.Activate();
                            if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
                        }
                        else
                        {
                            Boolean prev = false;
                            foreach (String str in previewed_scenes)
                            {
                                if (str == combo_scenes.SelectedItem.ToString())
                                {
                                    prev = true;
                                    break;
                                }
                            }
                            if (prev == true) btn_start_record.PerformClick();
                            else btn_preview.PerformClick();
                        }
                    }
                }
                else
                {
                    if (language == "es") frm_pedal.lbl_parada.Text = "Pedal detectado";
                    if (language == "en") frm_pedal.lbl_parada.Text = "Switch detected";
                    frm_pedal.pic.Image = frm_pedal.imgl.Images[0];
                }
            }
        }       

        private void get_cams()
        {
            // Show Video device list
            try
            {
                // enumerate video devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                {
                    //if (language == "es") MessageBox.Show("No se detectó ninguna capturadora de vídeo.", "No hay dispositivos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //if (language == "en") MessageBox.Show("No video capture devices detected.", "No video capture devices", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Camera1Combo.Enabled = false;

                }

                for (int i = 1, n = videoDevices.Count; i <= n; i++)
                {
                    string cameraName = i + " : " + videoDevices[i - 1].Name;

                    Camera1Combo.Items.Add(cameraName);
                }

                // check cameras count
                String path_vid = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "sel_vid.ini";
                if (videoDevices.Count > 0)
                {
                    if (System.IO.File.Exists(path_vid))
                    {
                        Camera1Combo.SelectedIndex = Camera1Combo.FindString(System.IO.File.ReadAllText(path_vid));
                    }
                    else
                    {
                        Camera1Combo.SelectedIndex = 0;
                    }

                }
                else
                {

                }
            }
            catch
            {

            }
        }

        private void get_obs_path()
        {
            if (Directory.Exists(obs_path))
            {
                obs_exec = obs_path + "\\" + "obs64.exe";
                if (System.IO.File.Exists(obs_exec)) return;
            }

            if (!System.IO.File.Exists(obs_exec))
            {
                obs_exec = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + "obs-studio" + "\\" + "bin" + "\\" + "64bit" + "\\" + "obs64.exe";
                obs_path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + "obs-studio" + "\\" + "bin" + "\\" + "64bit";

                if (!System.IO.File.Exists(obs_exec))
                {
                    obs_exec = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + "obs-studio" + "\\" + "bin" + "\\" + "64bit" + "\\" + "obs64.exe";
                    obs_path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + "obs-studio" + "\\" + "bin" + "\\" + "64bit";

                    if (!System.IO.File.Exists(obs_exec))

                    {
                        if (language == "es")
                        {
                            DialogResult a = MessageBox.Show("No se encontró OBS Studio instalado en la ruta por defecto. Puede localizar el ejecutable si está instalado en otra localización.", "Falta OBS", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (a == DialogResult.Cancel)
                            {
                                Application.Exit();
                                return;
                            }
                        }
                        if (language == "en")
                        {
                            DialogResult a = MessageBox.Show("OBS Studio was not found on the default path. You can browse for the executable file in case it is installed elsewhere.", "OBS Studio not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (a == DialogResult.Cancel)
                            {
                                Application.Exit();
                                return;
                            }
                        }


                        else
                        {
                            OpenFileDialog fd = new OpenFileDialog();
                            fd.Filter = "OBS ejecutable obs64.exe|obs64.exe";
                            if (fd.ShowDialog() == DialogResult.OK)
                            {
                                obs_exec = fd.FileName;
                                obs_path = Path.GetDirectoryName(fd.FileName);

                                plato_saga.Properties.Settings.Default.obs_path = obs_path;
                                plato_saga.Properties.Settings.Default.Save();
                            }

                            else
                            {
                                Application.Exit();
                                return;
                            }
                        }
                    }
                    else
                    {
                        plato_saga.Properties.Settings.Default.obs_path = obs_path;
                        plato_saga.Properties.Settings.Default.Save();
                    }

                }
                else

                {
                    plato_saga.Properties.Settings.Default.obs_path = obs_path;
                    plato_saga.Properties.Settings.Default.Save();
                }
            }
        }

        private void obs_is_recording()
        {
            Process[] obs_recording = Process.GetProcesses("obs-ffmpeg-mux");
            int num = obs_recording.Length;

            while (num > 0)
            {
                Thread.Sleep(250);
            }
        }

        private void close_obs_start()
        {
            Process[] localByName = Process.GetProcessesByName("obs64");
            int num = localByName.Length;

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
                            closed_ok = true;

                        });
                        if (!t.Wait(8000))
                        {
                            if (language == "es")
                            {
                                DialogResult a = MessageBox.Show("No se pudo salir de OBS Studio. ¿Desea forzar su cierre?", "OBS studio no responde", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (a == DialogResult.Yes) p.Kill();
                                else
                                {
                                    MessageBox.Show("No se puede cerrar OBS Studio. Intente cerrarlo manualmente o reinicie el equipo.", "OBS studio no respondió", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            if (language == "en")
                            {
                                DialogResult a = MessageBox.Show("OBS Studio could not be closed. ¿Do you wish to force quit it?", "OBS not responding", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (a == DialogResult.Yes) p.Kill();
                                else
                                {
                                    MessageBox.Show("OBS Studio could not be closed. Try closing it manually or restart computer.", "OBS not responding", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            closed_ok = false;
                        }
                    }
                    catch
                    {
                        closed_ok = false;
                        if (language == "es") MessageBox.Show("Error al cerrar OBS Studio. Debe cerrarlo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (language == "en") MessageBox.Show("Error closing OBS Studio. You need to close to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Arrow;
                        Application.Exit();
                        return;
                    }
                }
            }
        }

        //private void lock_read_only()
        //{
        //    String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio" + "\\" + "basic" + "\\" + "scenes");
        //    String obs_prof_or2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");

        //    if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio")))
        //        try
        //        {
        //            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio"));
        //        }
        //        catch
        //        {
        //            if (language == "es") MessageBox.Show("No se puede crear la carpeta de la aplicación del plató, contacte con soporte técnico.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            if (language == "en") MessageBox.Show("Studio application folder can't be created, please contact technical support.", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //    System.IO.File.SetAttributes(obs_prof_or2, System.IO.FileAttributes.ReadOnly);
        //    foreach (String file in Directory.GetFiles(obs_prof_or2, "*.ini", SearchOption.AllDirectories))
        //    {
        //        try
        //        {
        //            if (Path.GetDirectoryName(file) != obs_prof_or)
        //            {
        //                System.IO.File.SetAttributes(file, System.IO.FileAttributes.ReadOnly);
        //            }
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Error desconocido");
        //        }
        //    }

        //    btn_lock.Image = imgs.Images[0];
        //    if (language == "es") btn_lock.Text = "Plató está protegido";
        //    if (language == "en") btn_lock.Text = "Studio is protected";
        //    locked = true;
        //}

        //private void unlock_read_only()
        //{
        //    String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");

        //    if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio")))
        //        try
        //        {
        //            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio"));
        //        }
        //        catch (Exception excpt)
        //        {
        //            if (language == "es") MessageBox.Show("No se puede crear la carpeta de la aplicación del plató, contacte con soporte técnico." + Environment.NewLine + excpt.Message, "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            if (language == "en") MessageBox.Show("No se puede crear la carpeta de la aplicación del plató, contacte con soporte técnico." + Environment.NewLine + excpt.Message, "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //    btn_lock.Image = imgs.Images[1];
        //    if (language == "es") btn_lock.Text = "Plató está desbloqueado";
        //    if (language == "en") btn_lock.Text = "Studio is unprotected";

        //    locked = false;
        //    foreach (String dir in Directory.GetDirectories(obs_prof_or))
        //    {
        //        var di = new DirectoryInfo(dir);
        //        di.Attributes &= FileAttributes.Normal;
        //    }

        //    System.IO.File.SetAttributes(obs_prof_or, System.IO.FileAttributes.Normal);

        //    foreach (String file in Directory.GetFiles(obs_prof_or, "*.ini", SearchOption.AllDirectories))
        //    {
        //        try
        //        {
        //            System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);
        //        }
        //        catch (Exception excpt)
        //        {
        //            btn_lock.Image = imgs.Images[0];
        //            if (language == "es")
        //            {
        //                MessageBox.Show("Error al desbloquear: " + file + Environment.NewLine + Environment.NewLine + excpt.Message);
        //                btn_lock.Text = "Plató está protegido";
        //            }
        //            if (language == "en")
        //            {
        //                btn_lock.Text = "Studio is protected";
        //                MessageBox.Show("Error unlocking: " + file + Environment.NewLine + Environment.NewLine + excpt.Message);
        //            }
        //            locked = true;
        //        }
        //    }

        //}

        private void create_record()
        {
            //WshShell shell2 = new WshShell();
            //string shortcutAddress2 = Path.Combine(fd1.SelectedPath, scene_global_name) + @"\Record_" + scene_global_name + ".lnk";
            //IWshShortcut shortcut2 = (IWshShortcut)shell2.CreateShortcut(shortcutAddress2);
            //if (language == "es") shortcut2.Description = "Grabar escena SAGA";
            //if (language == "en") shortcut2.Description = "Record SAGA scene";
            //shortcut2.IconLocation = Application.StartupPath + "\\" + "record-no-intro.ico";
            //shortcut2.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + "obs-studio" + "\\" + "bin" + "\\" + "64bit";
            //shortcut2.TargetPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + "obs-studio" + "\\" + "bin" + "\\" + "64bit" + "\\" + "obs64.exe";

            //shortcut2.Arguments = "--collection " + '\u0022' + scene_global + '\u0022' + " --scene Pre-Intro" + " --startrecording";
            //shortcut2.Save();
        }

        private void refresh_scenes()
        {
            combo_scenes.Items.Clear();
            //Cargar escenas obs
            String obs_prof_or_2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");
            obs_prof_or_2 = obs_prof_or_2 + "\\" + "basic" + "\\" + "scenes";
            foreach (String file in Directory.GetFiles(obs_prof_or_2))
            {
                String item = System.IO.Path.GetExtension(file);
                String name = System.IO.Path.GetFileNameWithoutExtension(file);
                if (language == "es")
                {
                    if (item == ".json" && name != "Por_defecto" && name != "Comenzar_con_video" && name != "Comenzar_con_mixto" && name != "Comenzar_con_pc")
                    {
                        combo_scenes.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                    }
                }
                if (language == "en")
                {
                    if (item == ".json" && name != "Default" && name != "Start_with_video" && name != "Start_with_mix" && name != "Start_with_pc")
                    {
                        combo_scenes.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                    }
                }
            }
            if (combo_scenes.Items.Count > 0) combo_scenes.SelectedIndex = 0;
            else
            {
                if (language == "es") MessageBox.Show("No se encontraron escenas en la aplicación de los platós. Restaure una configuración válida, o contacte con soporte técnico.", "No se encontraron escenas validas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("No scenes were found in studio configuration. Please contact technical support, or restore a valid configuration.", "No valid scenes found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActiveForm.Close();
                return;
            }
            combo_scenes.Items.Insert(0, "---------------------");
            foreach (String file in Directory.GetFiles(obs_prof_or_2))
            {
                if (language == "es")
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Comenzar_con_video") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Comenzar_con_mixto") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Comenzar_con_pc") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Por_defecto") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                }
                if (language == "en")
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Start_with_video") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Start_with_mix") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Start_with_pc") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Default") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                }
            }
            //Fin cargar escenas obs
        }

        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture);
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }

        private void refresh_lang()
        {
            if (frm_11.combo_lang.SelectedIndex == 0)
            {
                plato_saga.Properties.Settings.Default.app_lang = "es";
                language = "es";
                plato_saga.Properties.Settings.Default.Save();

            }
            if (frm_11.combo_lang.SelectedIndex == 1)
            {
                plato_saga.Properties.Settings.Default.app_lang = "en";
                language = "en";
                plato_saga.Properties.Settings.Default.Save();
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form11));
            RefreshResources(this, resources);
            changed_lang = true;            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            get_obs_path();
            get_cams();
            pic_mute.Image = img_audio_2.Images[1];
            //Config
            if (plato_saga.Properties.Settings.Default.show_timer == true)
            {
                frm_11.chk_crono.CheckState = CheckState.Unchecked;
            }
            else
            {
                frm_11.chk_crono.CheckState = CheckState.Checked;
            }

            if (plato_saga.Properties.Settings.Default.auto_updates == false)
            {
                frm_11.chk_updates.CheckState = CheckState.Unchecked;
            }
            else
            {
                frm_11.chk_updates.CheckState = CheckState.Checked;
            }

            if (plato_saga.Properties.Settings.Default.show_keep_file == true)
            {
                frm_11.chk_show_keep.CheckState = CheckState.Unchecked;
            }
            else
            {
                frm_11.chk_show_keep.CheckState = CheckState.Checked;
            }

            if (plato_saga.Properties.Settings.Default.close_obs_auto == false)
            {
                frm_11.chk_auto_close_obs.CheckState = CheckState.Unchecked;
            }
            else
            {
                frm_11.chk_auto_close_obs.CheckState = CheckState.Checked;
            }

            if (plato_saga.Properties.Settings.Default.validate_scene == false)
            {
                frm_11.chk_validate.CheckState = CheckState.Unchecked;
            }
            else
            {
                frm_11.chk_validate.CheckState = CheckState.Checked;
            }

            if (plato_saga.Properties.Settings.Default.max_obs == false)
            {
                frm_11.chk_max_obs.CheckState = CheckState.Unchecked;
            }
            else
            {
                frm_11.chk_max_obs.CheckState = CheckState.Checked;
            }

            if ((int)plato_saga.Properties.Settings.Default.rtx_height < frm_prompt.n_h_rtx.Minimum) frm_prompt.n_h_rtx.Value = 129;
            else frm_prompt.n_h_rtx.Value = (int)plato_saga.Properties.Settings.Default.rtx_height;
            
            //End config

            pic_title.Focus();
            check_obs_run_init();
            num_delay_string = n_delay.Value.ToString();

            frmInfo.FormClosed += new FormClosedEventHandler(frmInfo_FormClosed);
            if (plato_saga.Properties.Settings.Default.app_lang == "es")
            {
                frm_11.combo_lang.SelectedIndex = 0;
            }
            if (plato_saga.Properties.Settings.Default.app_lang == "en")
            {
                frm_11.combo_lang.SelectedIndex = 1;

            }
            else frm_11.combo_lang.SelectedIndex = 0;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            RefreshResources(this, resources);

            //Leer escena guardada
            path_combo = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "sel_scene.ini";
            if (!Directory.Exists(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga")))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga"));
                System.IO.File.WriteAllText(path_combo, String.Empty);
            }
            String path_count = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "count_scene.ini";
            if (!System.IO.File.Exists(path_count))
            {
                System.IO.File.WriteAllText(path_count, String.Empty);
            }
            else
            {
                if (System.IO.File.ReadAllText(path_count) != String.Empty)
                    try
                    {
                        n_delay.Value = Convert.ToDecimal(System.IO.File.ReadAllText(path_count));
                    }
                    catch { }
            }

            //Monitor audio
            String path_mon_aud = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "mon_aud.ini";
            if (!System.IO.File.Exists(path_mon_aud))
            {
                mon_audio = true;
                chk_mon_audio.CheckState = CheckState.Checked;
            }
            else
            {
                mon_audio = false;
                combo_audio.Enabled = false;
                chk_mon_audio.CheckState = CheckState.Unchecked;
            }

            //End monitor audio

            fd1.ShowNewFolderButton = true;

            if (language == "es")
            {
                if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Escenas")))
                {
                    fd1.Description = "Seleccione la ruta para guardar los iconos de acceso directo a su colección personalizada.";
                    fd1.SelectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Escenas");
                }
            }
            if (language == "en")
            {
                if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Scenes")))
                {
                    fd1.Description = "Select path to save shortcuts to your selected scene collection.";
                    fd1.SelectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Scenes");
                }
            }

            //Cargar escenas obs
            String obs_prof_or_2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");
            obs_prof_or_2 = obs_prof_or_2 + "\\" + "basic" + "\\" + "scenes";
            foreach (String file in Directory.GetFiles(obs_prof_or_2))
            {
                String item = System.IO.Path.GetExtension(file);
                String name = System.IO.Path.GetFileNameWithoutExtension(file);
                if (language == "es")
                {
                    if (item == ".json" && name != "Por_defecto" && name != "Comenzar_con_video" && name != "Comenzar_con_mixto" && name != "Comenzar_con_pc")
                    {
                        combo_scenes.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                    }
                }
                if (language == "en")
                {
                    if (item == ".json" && name != "Default" && name != "Start_with_video" && name != "Start_with_mix" && name != "Start_with_pc")
                    {
                        combo_scenes.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                    }
                }
         
            }
            if (combo_scenes.Items.Count > 0) combo_scenes.SelectedIndex = 0;
            else
            {
                if (language == "es") MessageBox.Show("No se encontraron escenas en la aplicación de los platós. Restaure una configuración válida, o contacte con soporte técnico.", "No se encontraron escenas validas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("No scenes were found in studio configuration. Please contact technical support, or restore a valid configuration.", "No valid scenes found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActiveForm.Close();
                return;
            }
            combo_scenes.Items.Insert(0, "---------------------");
            foreach (String file in Directory.GetFiles(obs_prof_or_2))
            {
                if (language == "es")
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Por_defecto") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Comenzar_con_video") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Comenzar_con_mixto") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Comenzar_con_pc") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));

                }
                if (language == "en")
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Default") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Start_with_video") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Start_with_mix") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Start_with_pc") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                }
            }
            //Fin cargar escenas obs
            
            init_audio_devs();
            track_silence.Value = silence_level;
            get_silence();                       
            
            create_tips();
            check_pass();
        }

        private void check_pass()
        {
            String pass_file = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "pass_file";
            if (!File.Exists(pass_file)) File.WriteAllText(pass_file, "carrito");
            passwd_access = File.ReadAllText(pass_file);
        }

        private void init_audio_devs()
        {
            //Micrófonos
            MMDeviceEnumerator en = new MMDeviceEnumerator();
            NAudio.CoreAudioApi.MMDeviceCollection devices = en.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            var Array_dev = devices.ToArray();

            combo_audio.Items.AddRange(devices.ToArray());

            if (combo_audio.Items.Count > 0)
            {
                combo_audio.Items.Add("Default");
                timer1.Start();
                String path_aud = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "sel_aud.ini";
                String read_aud = String.Empty;
                if (!System.IO.File.Exists(path_aud))
                {
                    System.IO.File.WriteAllText(path_aud, String.Empty);
                }
                else
                {
                    if (System.IO.File.ReadAllText(path_aud) != String.Empty)
                    {
                        read_aud = System.IO.File.ReadAllText(path_aud);
                        combo_audio.SelectedIndex = combo_audio.FindString(read_aud);
                    }
                    else
                    {
                        combo_audio.SelectedIndex = 0;
                    }
                }
                var enumerator = new MMDeviceEnumerator();
                def_aud = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console).ToString();
            }
        }

        private void init_audio()
        {
            NAudio.Wave.WaveIn sourceStream = new NAudio.Wave.WaveIn();
            NAudio.Wave.WaveOutEvent waveOut = new WaveOutEvent();

            if (combo_audio.SelectedItem != null)
            {
                if (combo_audio.SelectedIndex == combo_audio.FindString("Default"))
                {
                    device = (MMDevice)combo_audio.Items[combo_audio.FindString(def_aud)];
                    sourceStream.DeviceNumber = combo_audio.FindString(def_aud);
                }
                else
                {
                    device = (MMDevice)combo_audio.SelectedItem;
                    sourceStream.DeviceNumber = combo_audio.SelectedIndex;
                }
            }

            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, 1);
            sourceStream.NumberOfBuffers = 4;
            NAudio.Wave.WaveInProvider waveIn = new NAudio.Wave.WaveInProvider(sourceStream);

            if (chk_mon_audio.CheckState == CheckState.Checked)
            {
                try
                {
                    waveOut.Init(waveIn);
                    waveOut.Volume = 0f;
                    sourceStream.StartRecording();
                    waveOut.Play();
                }
                catch (Exception excpt)
                {

                    if (language == "es") MessageBox.Show("No se pudo inicializar la monitorización de audio: " + excpt.Message);
                    if (language == "en") MessageBox.Show("Audio monitoring coult not be initialized: " + excpt.Message);
                }
            }
        }

        private void no_default_backup()
        {
            DialogResult a = DialogResult.None;
            if (language == "es") a = MessageBox.Show("No se encontró la copia de seguridad de la configuración de fábrica del Plató. ¿Desea crearla ahora utilizando la configuración actual?", "Falta configuración de fábrica", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (language == "en") a = MessageBox.Show("Factory profile backup was not found. ¿Do you want to create it now using current configuration?", "Factory profile not saved", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (a == DialogResult.Yes)
            {
                String obs_prof_saved = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio-saved");
                String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");
                try
                {
                    new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(obs_prof_or, obs_prof_saved, true);
                    if (language == "es") MessageBox.Show("Copia de seguridad de fábrica completada con éxito", "Backup realizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (language == "en") MessageBox.Show("Backup successfully completed", "Backup finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Arrow;
                }
                catch (Exception excpt)
                {
                    if (language == "es") MessageBox.Show("Se produjo un error al crear la copia de seguridad del perfil de fábrica del plató." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error al restaurar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (language == "en") MessageBox.Show("An error occurred while creating factory backup." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        private void boton_ok_Click(object sender, System.EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
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
                        btn_refresh.PerformClick();
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

        private void button3_Click(object sender, EventArgs e)
        {
            Process proc = new Process();

            if (System.IO.File.Exists(Path.Combine(Application.StartupPath, "Soporte_Plato_UPM_ES.pdf")))
            {

                foreach (Control ct in this.Controls)
                {
                    ct.Enabled = false;
                }

                if (language == "es") proc.StartInfo.FileName = (Path.Combine(Application.StartupPath, "Soporte_Plato_UPM_ES.pdf"));
                if (language == "en") proc.StartInfo.FileName = (Path.Combine(Application.StartupPath, "Soporte_Plato_UPM_EN.pdf"));
                proc.Start();

                foreach (Control ct in this.Controls)
                {
                    ct.Enabled = true;
                }
                pic_title.Focus();
            }
            else
            {

                pic_title.Focus();
                if (language == "es") MessageBox.Show("No se encontró el manual de usuario. Reinstale la aplicación.", "No se encontró el fichero", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("Quick guide was not found. Please reinstall application.", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("https://innovacioneducativa.upm.es/saga/plato-saga");
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

        private void check_obs_run()
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
        private void check_obs_run_init()
        {
            Process[] pname = Process.GetProcessesByName("obs64");
            if (pname.Length != 0)
            {
                DialogResult a = DialogResult.None;
                if (language == "es") a = MessageBox.Show("OBS Studio se está ejecutando, debe cerrarlo para usar la aplicación. ¿Desea hacerlo ahora?", "OBS Studio en ejecución", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (language == "en") a = MessageBox.Show("OBS Studio is running, you need to close it to use the application. ¿Do you want to close it now?", "OBS Studio running", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Yes) close_obs_start();
                else
                {
                    Application.Exit();
                    return;
                }

            }
        }

        private void btn_video1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/playlist?list=PLo4CW_btA6oYEjwJyw-Rsc1yr0ATENi56");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //pictureBox1.Focus();
            //check_obs();
            //if (obs_run == true) return;
            //if (System.IO.Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio" + "\\" + "basic")) == false)
            //{
            //if (language == "es") MessageBox.Show("No se encontró la aplicación de los platós en el equipo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //if (language == "en") MessageBox.Show("Studio application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //return;
            //}
            //Form3 form_accesos = new Form3();
            //form_accesos.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.StartPosition = FormStartPosition.CenterScreen;
            frm2.ShowDialog();
        }        

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frm_pedal.StartPosition = FormStartPosition.CenterParent;
            testing_pedal = true;

            frm_pedal.ShowDialog();
            testing_pedal = false;
        }

   

        private void btn_lock_Click(object sender, EventArgs e)
        {
            //if (locked == false)
            //{
            //    DialogResult a = DialogResult.Yes;
            //    if (language == "es")
            //    {
            //        a = MessageBox.Show("Si bloquea el plató no podrá hacer cambios en la configuración sin la contraseña de acceso a la aplicación." + Environment.NewLine + Environment.NewLine + "¿Está seguro?", "Confirmación de bloqueo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //    }

            //    if (language == "en")
            //    {
            //        a = MessageBox.Show("You will not be able to change studio settings once locked unless you provide the required password." + Environment.NewLine + Environment.NewLine + "Are you sure?", "Confirmación de bloqueo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //    }

            //    if (a != DialogResult.Yes) return;
            //    lock_read_only();
            //    if (language == "es") MessageBox.Show("La configuración general del plató está bloqueada. Solo es posible modificar las escenas.", "Configuración protegida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (language == "en") MessageBox.Show("Studio configuration is locked. Only scenes configuration can be edited.", "Configuration is locked", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    return;
            //}

            //frmInfo.Name = "Acceso a función protegida";
            //if (language == "es") frmInfo.Text = "Acceso a función protegida";
            //if (language == "en") frmInfo.Text = "Access to protected option";
            //frmInfo.Icon = this.Icon;
            //frmInfo.Icon = this.Icon;
            //frmInfo.Height = 120;
            //frmInfo.Width = 335;
            //frmInfo.FormBorderStyle = FormBorderStyle.Fixed3D;
            //frmInfo.MaximizeBox = false;
            //frmInfo.MinimizeBox = false;

            //Label lbl_titulo = new Label();
            //lbl_titulo.Parent = frmInfo;
            //lbl_titulo.Top = 20;
            //lbl_titulo.Left = 14;
            //lbl_titulo.Width = 290;
            //if (language == "es") lbl_titulo.Text = "Introduzca la contraseña de acceso:";
            //if (language == "en") lbl_titulo.Text = "Please write required password:";


            //passwd.Parent = frmInfo;
            //passwd.Top = 45;
            //passwd.Left = 14;
            //passwd.Width = 230;
            //passwd.TabIndex = 0;
            //passwd.UseSystemPasswordChar = true;
            //passwd.BorderStyle = BorderStyle.Fixed3D;
            //passwd.Text = String.Empty;

            //Button boton_ok = new Button();

            //boton_ok.Parent = frmInfo;
            //boton_ok.Left = 247;
            //boton_ok.Top = 44;
            //boton_ok.Width = 60;
            //boton_ok.Height = 22;
            //if (language == "es") boton_ok.Text = "Aceptar";
            //if (language == "en") boton_ok.Text = "OK";

            //boton_ok.Click += new EventHandler(boton_ok_Click);

            //frmInfo.StartPosition = FormStartPosition.CenterScreen;
            //frmInfo.ShowDialog();

            //if (bad_pass == true)
            //{
            //    return;
            //}

            //unlock_read_only();
            //if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio-saved")))
            //{
            //    no_default_backup();
            //}
        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {
        
        }

        private void btn_lock_MouseHover(object sender, EventArgs e)
        {

        }

        private void btn_escenas_Click(object sender, EventArgs e)
        {
            //String ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Escenas");
            //if (!Directory.Exists(ruta))
            //{
            //  MessageBox.Show("No se encontró la carpeta de escenas por defecto en el escritorio (Escenas). Debe crearla para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //return;
            //}
            //Process.Start("explorer.exe", ruta);
        }

        private void btn_videos_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            frm_timer.Top = 37;
            frm_timer.Left = (resolution.Width - 175);
            lbl_thr.Text = track_silence.Value.ToString() + " dB";

            if (Camera1Combo.Items.Count <= 0)
            {
                btn_preview_camera.Enabled = false;
                btn_stop_cam.Enabled = false;
                btn_video_prop.Enabled = false;
            }

            if (locked == false)
            {
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio-saved")))
                {
                    no_default_backup();
                }
            }

            String[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Count() > 1)
            {
                if (arguments[1] == "en")
                {
                    frm_11.combo_lang.SelectedIndex = 1;
                    plato_saga.Properties.Settings.Default.app_lang = "en";
                    plato_saga.Properties.Settings.Default.Save();
                }
            }

            form_intro.Dispose();
            if (chk_mon_audio.CheckState == CheckState.Checked)
            {
                mon_audio = true;
                timer_startup.Start();
            }
            else
            {
                mon_audio = false;
            }

            foreach (String item in combo_scenes.Items)
            {
                Combo_scene.Items.Add(item);
            }

            frm_11.btn_update.Text = "Version " + Application.ProductVersion;
            if (frm_11.chk_updates.CheckState == CheckState.Checked)
            {       
                    check_updates();
            }
            else
            {
                start_up = false;
            }
            show_devs = plato_saga.Properties.Settings.Default.show_panel;
            if (show_devs == false)
            {                
                chk_panel_dev.Checked = false;
            }
            else
            {                
                chk_panel_dev.Checked = true;
            }
            show_devs_panel();
            refresh_lang();
            //refresh_scenes();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (language == "es") Process.Start("https://innovacioneducativa.upm.es/saga/plato-saga");
            if (language == "en") Process.Start("https://innovacioneducativa.upm.es/saga/plato-saga#saga_english");
        }

        private void get_real_scene()
        {
            if (combo_scenes.SelectedIndex == -1) return;
            String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");
            obs_prof_or = obs_prof_or + "\\" + "basic" + "\\" + "scenes";
            Boolean scene_exist = false;
            List<string> file_lines = new List<string>();
            String name_internal = String.Empty;
            Boolean audio_captured_present = false;
            
            foreach (String file in Directory.GetFiles(obs_prof_or))
            {                
                if (System.IO.File.ReadAllText(file).Contains('\u0022' + "name" + '\u0022' + ":" + '\u0022' + combo_scenes.SelectedItem.ToString() + '\u0022'))
                {
                    scene_exist = true;
                    scene_global = combo_scenes.SelectedItem.ToString();
                    scene_global_name = scene_global;
                    String Pre_Intro = System.IO.File.ReadAllText(file);
                    foreach (String line in System.IO.File.ReadLines(file))
                    {
                        if (line.ToLower().Contains("wasapi_input_capture"))
                        {
                            audio_captured_present = true;
                        }

                        if (audio_captured_present == true)
                        {                            
                            if (line.ToLower().Contains(aud_sel_ID.ToLower()) == true)
                            {
                                aud_match = true;                                
                                break;
                            }
                        }
                        else continue;
                    }
                    
                    if (Pre_Intro.Contains('\u0022' + "name" + '\u0022' + ":" + '\u0022' + "Pre-Intro" + '\u0022') == false) bad_col = true;
                    
                    break;
                }

                else if (System.IO.File.ReadAllText(file).Contains('\u0022' + "name" + '\u0022' + ":" + '\u0022' + combo_scenes.SelectedItem.ToString().Replace("_", " ") + '\u0022'))
                {
                    scene_exist = true;
                    scene_global = combo_scenes.SelectedItem.ToString().Replace("_", " ");
                    scene_global_name = scene_global;
                    String Pre_Intro = System.IO.File.ReadAllText(file);

                    foreach (String line in System.IO.File.ReadLines(file))
                    {
                        if (line.ToLower().Contains("wasapi_input_capture"))
                        {
                            audio_captured_present = true;
                        }
                        if (audio_captured_present == true)
                        {
                            
                            if (line.ToLower().Contains(aud_sel_ID.ToLower()) == true)
                            {
                                aud_match = true;                                
                                break;
                            }
                        }
                        else continue;
                    }

                    if (Pre_Intro.Contains('\u0022' + "name" + '\u0022' + ": " + '\u0022' + "Pre-Intro" + '\u0022') == false) bad_col = true;
                    //if (Pre_Intro.Contains('\u0022' + "device_id" + '\u0022' + ": " + '\u0022' + aud_sel_ID.ToLower() + '\u0022')) aud_match = true;
                    break;
                }
                else
                {
                    int i = 0;
                    Boolean found_key = false;
                    Boolean match = false;
                    String name_found = String.Empty;
                    foreach (String line in System.IO.File.ReadLines(file))
                    {
                        if (System.IO.Path.GetFileNameWithoutExtension(file) != combo_scenes.SelectedItem.ToString()) continue;
                        if (line.Contains('\u0022' + "name" + '\u0022' + ": ") && found_key == true)
                        {
                            match = true;
                            name_found = line.Substring(13, line.Length - 15);
                            scene_global = name_found;

                            String Pre_Intro = System.IO.File.ReadAllText(file);

                            foreach (String line2 in System.IO.File.ReadLines(file))
                            {
                                if (line.ToLower().Contains("wasapi_input_capture"))
                                {
                                    audio_captured_present = true;
                                }
                                if (audio_captured_present == true)
                                {
                                    
                                    if (line2.ToLower().Contains(aud_sel_ID.ToLower()) == true)
                                    {
                                        aud_match = true;                                    
                                        break;
                                    }
                                }
                                else continue;
                            }

                            if (Pre_Intro.Contains('\u0022' + "name" + '\u0022' + ": " + '\u0022' + "Pre-Intro" + '\u0022') == false || Pre_Intro.Contains('\u0022' + "name" + '\u0022' + ": " + '\u0022' + "Salida" + '\u0022') == false) bad_col = true;
                            //if (Pre_Intro.Contains('\u0022' + "device_id" + '\u0022' + ": " + '\u0022' + aud_sel_ID.ToLower() + '\u0022')) aud_match = true;

                            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

                            foreach (char c in invalid)
                            {
                                name_found = name_found.Replace(c.ToString(), "");
                            }

                            if (System.IO.Path.GetFileNameWithoutExtension(file) == name_found.Replace(" ", "_"))
                            {
                                scene_exist = true;
                                scene_global_name = name_found.Replace(" ", "_");
                                break;
                            }
                        }
                        else
                        {
                            match = false;
                        }

                        if (line.Contains("},"))
                        {
                            found_key = true;
                        }
                        else
                        {
                            found_key = false;
                        }
                    }
                }
            }
            this.Cursor = Cursors.Arrow;
            if (scene_exist == false)
            {
                if (language == "es") MessageBox.Show("Se produjo un error al utilizar la colección de escenas seleccionada." + Environment.NewLine + Environment.NewLine + "Puede solucionarlo clonando la colección de escenas y usando un nombre sin espacios en blanco.", "Nombre de escena incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (language == "en") MessageBox.Show("An error occurred using selected collection." + Environment.NewLine + Environment.NewLine + "You may solve it by cloning the scene without blank spaces.", "Nombre de escena incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (bad_col == true && plato_saga.Properties.Settings.Default.validate_scene == true)
            {
                DialogResult a = DialogResult.None;
                if (language == "es") a = MessageBox.Show("La colección seleccionada no contiene una escena requerida. Los resultados pueden ser inesperados.", "Falta la escena requerida", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (language == "en") a = MessageBox.Show("Selected collection lacks a required scene. Results are unpredictible.", "A required scene is missing", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (a == DialogResult.OK) bad_col = false;
                else bad_col = true;
            }
        }

        private void get_device_aud_ID()
        {
            aud_sel_ID = String.Empty;
            MMDeviceEnumerator en = new MMDeviceEnumerator();
            var devices = en.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            var Array_dev = devices.ToArray();
            ComboBox combo_temp = new ComboBox();
            combo_temp.Items.AddRange(devices.ToArray());

            if (combo_temp.Items.Count == 0 || combo_audio.Items.Count == 0)
            {
                mon_audio = false;
                chk_mon_audio.CheckState = CheckState.Unchecked;
                return;
            }
            if (combo_audio.SelectedIndex == -1) return;
            combo_temp.Items.Add("Default");
            foreach (NAudio.CoreAudioApi.MMDevice item in combo_temp.Items)
            {
                if (item.ToString() == combo_audio.SelectedItem.ToString())
                {
                    aud_sel_ID = item.ID;
                    break;
                }
                if (combo_audio.SelectedItem.ToString() == "Default")
                {
                    aud_sel_ID = "Default";
                    break;
                }
            }
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            if (combo_scenes.SelectedItem == null)
            {
                if (language == "es") MessageBox.Show("No se ha seleccionado ninguna colección de escenas.", "No hay escena seleccionada", MessageBoxButtons.OK);
                if (language == "en") MessageBox.Show("No scene collection was selected.", "No scene collection selected", MessageBoxButtons.OK);
                return;
            }

            loading_obs = true;
            recording = false;
            pic_title.Focus();
            btn_stop_cam.PerformClick();
            this.Enabled = false;
            if (btn_preview_camera.Enabled == false) btn_stop_cam.PerformClick();
            if (peak_audio < threshold_aud && combo_audio.SelectedItem != null && mon_audio == true)
            {
                if (language == "es") MessageBox.Show("No se detectó sonido en el micrófono seleccionado. Revise que el micrófono tiene pilas cargadas y que no está en posición mute.", "No se detectó sonido de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (language == "en") MessageBox.Show("No sound was detected on selected microphone. Please check batteries are charged and mute is not selected.", "No sound detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            aud_match = false;
            get_device_aud_ID();
            get_real_scene();

            if (aud_match == false && chk_mon_audio.CheckState == CheckState.Checked)
            {
                if (language == "es") MessageBox.Show("El dispositivo de audio seleccionado con coincide con el que está configurado en la escena.", "Audio de escena no coincide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (language == "en") MessageBox.Show("Selected audio capture device does not match collection audio device.", "Selected audio device not found on scene", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.Cursor = Cursors.WaitCursor;
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
                        if (!t.Wait(8000))
                        {
                            p.Kill();
                        }
                    }
                    catch
                    {
                        if (language == "es") MessageBox.Show("Error al cerrar OBS Studio. Debe cerrarlo para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (language == "en") MessageBox.Show("Error trying to quit OBS Studio. You need to close it to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        loading_obs = false;
                        this.Enabled = true;
                        this.Cursor = Cursors.Arrow;
                    }
                }
            }
            bad_col = false;
            this.Cursor = Cursors.Arrow;
            Thread.Sleep(100);

            if (bad_col == true)
            {
                this.Enabled = true;
                return;
            }
            
            obs_launched = false;
            previewed_scenes.Add(combo_scenes.SelectedItem.ToString());            
            plato_saga.Form7 frm_load_obs = new plato_saga.Form7();
            if (language == "es")
            {
                if (combo_scenes.SelectedItem.ToString().Contains("Comenzar_con_"))
                {
                    MessageBox.Show("Ha seleccionado una colección básica, que no debería ser modificada." + Environment.NewLine + Environment.NewLine + "Para crear y modificar su propia colección de escenas utilice el botón Clonar.", "Colección básica seleccionada",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                frm_load_obs.textBox1.Text = "Iniciando previsualización";
                frm_load_obs.label2.Text = combo_scenes.SelectedItem.ToString();
            }
            if (language == "en")
            {
                if (combo_scenes.SelectedItem.ToString().Contains("Start_with_"))
                {
                    MessageBox.Show("A basic collection was selected, which should not be modified." + Environment.NewLine + Environment.NewLine + "In order to use and customize it, please create your own by pressing the button Clone.","Default collection selected",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                frm_load_obs.textBox1.Text = "Starting preview";
                frm_load_obs.label2.Text = combo_scenes.SelectedItem.ToString();
            }
            frm_load_obs.Show();

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;

                //String shortcutAddress = fd1.SelectedPath + @"\Preview_" + scene_global_name + ".lnk";
                proc.StartInfo.FileName = obs_exec;
                proc.StartInfo.WorkingDirectory = obs_path;
                if (Properties.Settings.Default.max_obs == true) proc.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                else proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                proc.StartInfo.Arguments = "--profile Plato --collection " + '\u0022' + scene_global + '\u0022' + " --scene Pre-Intro";

                obs_launched = true;
                proc.Start();
                frm_load_obs.Invoke(new MethodInvoker(delegate
                {
                    frm_load_obs.Activate();
                }));

                while (string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    System.Threading.Thread.Sleep(150);
                    proc.Refresh();
                }
                Thread.Sleep(100);
                if (proc.MainWindowTitle.ToLower().Contains("obs 24") == false && proc.MainWindowTitle.ToLower().Contains("obs 25") == false && proc.MainWindowTitle.ToLower().Contains("obs 26") == false && proc.MainWindowTitle.ToLower().Contains("obs 27") == false)
                {
                    if (language == "es") MessageBox.Show("Versión de OBS studio no soportada. Pueden producirse comportamientos inesperados. Instale OBS Studio versión 24 ó superior para solucionarlo.");
                    if (language == "en") MessageBox.Show("Current OBS Studio version is not supported. Some features may not work. Please install OBS Studio 24 or newer for best results.");
                }

                this.Invoke(new MethodInvoker(delegate
                {
                    this.Enabled = true;
                }));

                Disable_controls();
                frm_load_obs.Invoke(new MethodInvoker(delegate
                {
                    frm_load_obs.Close();
                }));

                lbl_obs_running.Invoke(new MethodInvoker(delegate
                {
                    if (language == "es") lbl_obs_running.Text = "OBS Studio ha sido lanzado para previsualización";
                    if (language == "en") lbl_obs_running.Text = "OBS Studio was launched for preview";

                }));
                loading_obs = false;

                //Tamaño de ventana
                if (Properties.Settings.Default.max_obs == false)
                {
                    int left = 0;
                    int top = 0;
                    int width = resolution.Width;
                    int height = resolution.Height;

                    if (Properties.Settings.Default.pr_location == 1)
                    {
                        left = 0;
                        top = Properties.Settings.Default.rtx_height - 50;
                        width = resolution.Width;
                        height = resolution.Height - Properties.Settings.Default.rtx_height + 50;
                    }

                    if (Properties.Settings.Default.pr_location == 2)
                    {
                        left = 0;
                        top = 0;
                        width = resolution.Width;
                        height = resolution.Height - Properties.Settings.Default.rtx_height;
                    }

                    if (Properties.Settings.Default.pr_location == 3)
                    {
                        left = Screen.PrimaryScreen.Bounds.Width / 3 + 15;
                        top = 0;
                        width = resolution.Width - left;
                        height = resolution.Height;
                    }
                    if (Properties.Settings.Default.pr_location == 4)
                    {
                        left = 0;
                        top = 0;
                        width = resolution.Width - (Screen.PrimaryScreen.Bounds.Width / 3 + 15);
                        height = resolution.Height;
                    }

                    SetWindowPos(proc.MainWindowHandle,
                            HWND_TOP,
                            left, top, width, height, 0);                
                }

                if (plato_saga.Properties.Settings.Default.to_prompt == true)
                {                    
                    new System.Threading.Thread(() =>
                    {
                        System.Threading.Thread.CurrentThread.IsBackground = true;
                        Thread.Sleep(1000);
                        
                        try
                        {
                            frm_tele.Invoke(new MethodInvoker(delegate
                            {
                                if (plato_saga.Properties.Settings.Default.rem_lines == false)
                                {
                                    frm_tele.rtx1.Text = Environment.NewLine + Environment.NewLine + pr_text;
                                }
                                else frm_tele.rtx1.Text = pr_text;
                                frm_tele.ShowDialog();
                            }));
                        }
                        catch
                        {
                            if (plato_saga.Properties.Settings.Default.rem_lines == false)
                            {
                                frm_tele.rtx1.Text = Environment.NewLine + Environment.NewLine + pr_text;
                            }
                            else frm_tele.rtx1.Text = pr_text;

                            try
                            {
                                frm_tele.ShowDialog();
                            }
                            catch { }
                        }

                    }).Start();
                }

                proc.WaitForExit();
                Enable_Controls();

                if (plato_saga.Properties.Settings.Default.to_prompt == true)
                {
                    try
                    {
                        frm_tele.Invoke(new MethodInvoker(delegate
                        {
                            frm_tele.Close();
                        }));
                    }
                    catch { }
                }

                lbl_obs_running.Invoke(new MethodInvoker(delegate
                {
                    lbl_obs_running.Text = String.Empty;
                }));
                this.Invoke(new MethodInvoker(delegate
                {
                    this.TopMost = true;
                    this.TopMost = false;
                }));
                obs_launched = false;
                btn_refresh.Invoke(new MethodInvoker(delegate
                {
                    btn_refresh.PerformClick();
                }));

            }).Start();
        }

        private void Disable_controls()
        {
            foreach (Control ct in this.Controls)
            {
                if (ct.Name != "groupBox1")
                {
                    ct.Invoke(new MethodInvoker(delegate
                     {
                         ct.Enabled = false;
                     }));
                }
            }

            foreach (Control ct in groupBox1.Controls)
            {
                ct.Invoke(new MethodInvoker(delegate
                {
                    ct.Enabled = false;
                }));
            }
            //btn_start_record.Invoke(new MethodInvoker(delegate
            ////  btn_start_record.Enabled = true;
            // }));

            n_delay.Invoke(new MethodInvoker(delegate
            {
                n_delay.Enabled = true;
            }));

            lbl_obs_running.Invoke(new MethodInvoker(delegate
            {
                lbl_obs_running.Enabled = true;
            }));

            btn_aud_dev.Invoke(new MethodInvoker(delegate
            {
                btn_aud_dev.Enabled = true;

            }));
            progressBar1.Invoke(new MethodInvoker(delegate
            {
                progressBar1.Enabled = true;
                progressBar1.Focus();

            }));
        }

        private void Enable_Controls()
        {
            foreach (Control ct in this.Controls)
            {
                ct.Invoke(new MethodInvoker(delegate
                   {
                       ct.Enabled = true;
                   }));
            }
            if (mon_audio == false)
            {
                combo_audio.Invoke(new MethodInvoker(delegate
            {
                combo_audio.Enabled = false;
            }));
            }

            foreach (Control ct in groupBox1.Controls)
            {
                ct.Invoke(new MethodInvoker(delegate
                {
                    ct.Enabled = true;
                }));
            }
            btn_stop_cam.Invoke(new MethodInvoker(delegate
            {
                btn_stop_cam.Enabled = false;
            }));

        }

        private void btn_start_record_Click(object sender, EventArgs e)
        {
            aud_match = false;            

            get_device_aud_ID();
            get_real_scene();
            if (aud_match == false && chk_mon_audio.CheckState == CheckState.Checked && frm_11.chk_validate.CheckState == CheckState.Unchecked)
            {
                if (language == "es") MessageBox.Show("El dispositivo de audio seleccionado no coincide con el de la escena." + Environment.NewLine + Environment.NewLine + "Seleccione otro dispositivo, o use la previsualización para verificar que el dispositivo seleccionado es el correcto.", "Audio no está configurado para la escena", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("Selected audio capture device does not match collection audio device.", "Selected audio device not found on scene", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                loading_obs = false;
                this.Enabled = true;
                return;
            }

            if (bad_col == true && frm_11.chk_validate.CheckState == CheckState.Unchecked)
            {
                this.Enabled = true;
                return;
            }
            if (combo_scenes.SelectedItem == null)
                {
                    if (language == "es") MessageBox.Show("No se ha seleccionado ninguna colección de escenas.", "No hay escena", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (language == "en") MessageBox.Show("No scene collection was selected.", "No scene collection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            if (btn_preview_camera.Enabled == false) btn_stop_cam.PerformClick();

            if (peak_audio < threshold_aud && combo_audio.SelectedItem != null && mon_audio == true)
            {
                if (language == "es") MessageBox.Show("No se detectó sonido en el micrófono seleccionado. Revise que el micrófono tiene pilas cargadas y que no está en posición mute.", "No se detectó sonido de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("No sound was detected on selected microphone. Please check batteries are charged and mute is not selected.", "No sound detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                loading_obs = false;
                this.Enabled = true;
                return;

            }
            this.Enabled = false;
            if (n_delay.Value > 0)
            {
                num_delay_string = n_delay.Value.ToString();
                frm_cuenta.cancelado = false;
                frm_cuenta.num_val = n_delay.Value.ToString();
                if (language == "es")
                {
                    frm_cuenta.label1.Text = "La grabación se iniciará en";
                    frm_cuenta.button1.Text = "Cancelar";
                }
                if (language == "en")
                {
                    frm_cuenta.label1.Text = "     Recording will start in";
                    frm_cuenta.button1.Text = "Cancel";
                }
                frm_cuenta.ShowDialog();
                if (frm_cuenta.cancelado == true)
                {
                    loading_obs = false;
                    lbl_obs_running.Text = "";
                    this.Enabled = true;
                    this.Cursor = Cursors.Arrow;
                    return;
                }
            }
            pic_title.Focus();
            this.Cursor = Cursors.WaitCursor;
            BG_rec.RunWorkerAsync();
        }

        private void move_video_discarded()
        {
            file_n = file_n + 1;
            var directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            var LastFile = (from f in directory.GetFiles("*.m??")
                            orderby f.LastAccessTime descending
                            select f).First();
                        
            if (language == "es")
            {
                if (!Directory.Exists(LastFile.DirectoryName + "\\" + "Descartados"))
                {
                    try
                    {
                        Directory.CreateDirectory(LastFile.DirectoryName + "\\" + "Descartados");
                    }
                    catch (Exception excpt)
                    {
                        MessageBox.Show("No se pudo crear la carpeta Descartados. El fichero seguirá en su ubicación inicial: " + excpt.Message, "Error al mover", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                try
                {
                    
                    System.IO.File.Move(LastFile.FullName, LastFile.DirectoryName + "\\" + "Descartados" + "\\" + LastFile + "_" + file_n.ToString() + LastFile.Extension);
                }
                catch (Exception excpt)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Enabled = true;
                        this.Cursor = Cursors.Arrow;
                    }));
                    MessageBox.Show("No se pudo borrar el fichero, es posible que esté en uso. " + Environment.NewLine + Environment.NewLine + excpt.Message);
                }
            }
            if (language == "en")
            {
                if (!Directory.Exists(LastFile.DirectoryName + "\\" + "Discarded"))
                {
                    try
                    {
                        Directory.CreateDirectory(LastFile.DirectoryName + "\\" + "Discarded");
                    }
                    catch (Exception excpt)
                    {
                        MessageBox.Show("Discarded folder could not be created. Video file will remain at default location: " + excpt.Message, "Error moving file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                try
                {
                    System.IO.File.Move(LastFile.FullName, LastFile.DirectoryName + "\\" + "Discarded" + "\\" + LastFile + "_" + file_n.ToString() + LastFile.Extension);
                }
                catch (Exception excpt)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Enabled = true;
                        this.Cursor = Cursors.Arrow;
                    }));
                    MessageBox.Show("File could not be moved, it may be locked by another application. " + Environment.NewLine + Environment.NewLine + excpt.Message);
                }
            }
        }

        private void ask_to_keep_video()
        {
            var directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            var LastFile = (from f in directory.GetFiles("*.m??")
                            orderby f.LastAccessTime descending
                            select f).First();
            DialogResult a = DialogResult.None;

            if (language == "es") a = MessageBox.Show("¿Desea conservar el fichero " + LastFile.Name + " ?", "Confirmar acción", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (language == "en") a = MessageBox.Show("¿Do you want to keep recording video " + LastFile.Name + " ?", "Confirm action", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (a == DialogResult.Yes || a == DialogResult.Cancel)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    this.Enabled = true;
                    this.Cursor = Cursors.Arrow;
                }));

                return;
            }
            else
            {
                if (language == "es")
                {
                    if (!Directory.Exists(LastFile.DirectoryName + "\\" + "Descartados"))
                    {
                        try
                        {
                            Directory.CreateDirectory(LastFile.DirectoryName + "\\" + "Descartados");
                        }
                        catch (Exception excpt)
                        {
                            MessageBox.Show("No se pudo crear la carpeta Descartados. El fichero seguirá en su ubicación inicial: " + excpt.Message, "Error al mover", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    try
                    {
                        System.IO.File.Move(LastFile.FullName, LastFile.DirectoryName + "\\" + "Descartados" + "\\" + LastFile);
                    }
                    catch (Exception excpt)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Enabled = true;
                            this.Cursor = Cursors.Arrow;
                        }));
                        MessageBox.Show("No se pudo borrar el fichero, es posible que esté en uso. " + Environment.NewLine + Environment.NewLine + excpt.Message);
                    }
                }
                if (language == "en")
                {
                    if (!Directory.Exists(LastFile.DirectoryName + "\\" + "Discarded"))
                    {
                        try
                        {
                            Directory.CreateDirectory(LastFile.DirectoryName + "\\" + "Discarded");
                        }
                        catch (Exception excpt)
                        {
                            MessageBox.Show("Discarded folder could not be created. Video file will remain at default location: " + excpt.Message, "Error moving file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    try
                    {
                        System.IO.File.Move(LastFile.FullName, LastFile.DirectoryName + "\\" + "Discarded" + "\\" + LastFile);
                    }
                    catch (Exception excpt)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Enabled = true;
                            this.Cursor = Cursors.Arrow;
                        }));
                        MessageBox.Show("File could not be moved, it may be locked by another application. " + Environment.NewLine + Environment.NewLine + excpt.Message);
                    }
                }
            }
        }

        private void create_icons()
        {
            //if (combo_scenes.SelectedIndex == -1)
            //{
            //    if (language == "es") MessageBox.Show("No se ha especificado el nombre de la escena.");
            //    if (language == "en") MessageBox.Show("Scene name is missing.");
            //    return;
            //}

            //String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");
            //obs_prof_or = obs_prof_or + "\\" + "basic" + "\\" + "scenes";
            //Boolean scene_exist = false;
            //List<string> file_lines = new List<string>();
            //String name_internal = String.Empty;

            //foreach (String file in Directory.GetFiles(obs_prof_or))
            //{
            //    if (System.IO.File.ReadAllText(file).Contains('\u0022' + "name" + '\u0022' + ": " + '\u0022' + combo_scenes.SelectedItem.ToString() + '\u0022'))
            //    {
            //        scene_exist = true;
            //        scene_global = combo_scenes.SelectedItem.ToString();
            //        scene_global_name = scene_global;
            //        break;
            //    }

            //    else if (System.IO.File.ReadAllText(file).Contains('\u0022' + "name" + '\u0022' + ": " + '\u0022' + combo_scenes.SelectedItem.ToString().Replace("_", " ") + '\u0022'))
            //    {
            //        scene_exist = true;
            //        scene_global = combo_scenes.SelectedItem.ToString().Replace("_", " ");
            //        scene_global_name = scene_global;
            //        break;
            //    }
            //    else
            //    {
            //        int i = 0;
            //        Boolean found_key = false;
            //        Boolean match = false;
            //        String name_found = String.Empty;
            //        foreach (String line in System.IO.File.ReadLines(file))
            //        {
            //            if (System.IO.Path.GetFileNameWithoutExtension(file) != combo_scenes.SelectedItem.ToString()) continue;
            //            if (line.Contains('\u0022' + "name" + '\u0022' + ": ") && found_key == true)
            //            {
            //                match = true;
            //                name_found = line.Substring(13, line.Length - 15);
            //                scene_global = name_found;
            //                string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            //                foreach (char c in invalid)
            //                {
            //                    name_found = name_found.Replace(c.ToString(), "");
            //                }

            //                if (System.IO.Path.GetFileNameWithoutExtension(file) == name_found.Replace(" ", "_"))
            //                {
            //                    scene_exist = true;
            //                    scene_global_name = name_found.Replace(" ", "_");
            //                    break;
            //                }
            //            }
            //            else
            //            {
            //                match = false;
            //            }

            //            if (line.Contains("},"))
            //            {
            //                found_key = true;
            //            }
            //            else
            //            {
            //                found_key = false;
            //            }
            //        }
            //    }
            //}

            //if (scene_exist == false)
            //{
            //    if (language == "es") MessageBox.Show("Se produjo un error al utilizar la colección de escenas seleccionada." + Environment.NewLine + Environment.NewLine + "Puede solucionarlo duplicando la colección de escenas y usando un nombre sin espacios en blanco.", "Nombre de escena incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    if (language == "en") MessageBox.Show("There was an error trying to use the selected collection." + Environment.NewLine + Environment.NewLine + "You may solve it by cloning it using a new name without blank spaces.", "Scene name error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            //if (fd1.ShowDialog() == DialogResult.OK)
            //{
            //    create_preview();
            //    create_record();
            //    if (language == "es") MessageBox.Show("Iconos de acceso directo creados correctamente.", "Iconos creados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (language == "en") MessageBox.Show("Shortcuts succesfully created.", "Shortcuts created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Process.Start("explorer.exe", Path.Combine(fd1.SelectedPath, scene_global_name));
            //    ActiveForm.Close();
            //}
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            create_icons();
        }

        private void create_preview()
        {
            //if (!Directory.Exists(Path.Combine(fd1.SelectedPath, scene_global_name)))
            //    {
            //    Directory.CreateDirectory(Path.Combine(fd1.SelectedPath, scene_global_name));
            //    }
            //WshShell shell = new WshShell();
            //string shortcutAddress = Path.Combine(fd1.SelectedPath, scene_global_name) + @"\Preview_" + scene_global_name + ".lnk";
            //IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            //shortcut.Description = "Previsualizar escena SAGA";
            //shortcut.IconLocation = Application.StartupPath + "\\" + "Preview-OBS.ico";
            //shortcut.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + "obs-studio" + "\\" + "bin" + "\\" + "64bit";
            //shortcut.TargetPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + "obs-studio" + "\\" + "bin" + "\\" + "64bit" + "\\" + "obs64.exe";
            //shortcut.Arguments = "--profile Plato --collection " + '\u0022' + scene_global + '\u0022' + " --scene Pre-intro";
            //shortcut.Save();
        }


        private void btn_clone_Click(object sender, EventArgs e)
        {
            check_obs_run();
            if (combo_scenes.SelectedIndex == -1)
            {
                if (language == "es") MessageBox.Show("No se ha seleccionado ninguna escena.");
                if (language == "en") MessageBox.Show("No scene collection was selected.");
                return;
            }
            Form frmName = new Form();

            if (language == "es")
            {
                frmName.Name = "Duplicar colección de escenas existente";
                frmName.Text = "Duplicar colección de escenas existente";
            }
            if (language == "en")
            {
                frmName.Name = "Clone existing scene collection";
                frmName.Text = "Clone existing scene collection";
            }
            frmName.Icon = this.Icon;
            frmName.Icon = this.Icon;
            frmName.Height = 120;
            frmName.Width = 335;
            frmName.FormBorderStyle = FormBorderStyle.Fixed3D;
            frmName.MaximizeBox = false;
            frmName.MinimizeBox = false;
            frmName.FormClosed += new FormClosedEventHandler(frmName_FormClosed);

            Label lbl_titulo = new Label();
            lbl_titulo.Parent = frmName;
            lbl_titulo.Top = 20;
            lbl_titulo.Left = 14;
            lbl_titulo.Width = 290;
            lbl_titulo.Text = "Seleccione el nombre de la nueva escena";
            if (language == "es") lbl_titulo.Text = "Seleccione el nombre de la nueva escena";
            if (language == "en") lbl_titulo.Text = "Select new scene collection file name";

            file_name_clone.Parent = frmName;
            file_name_clone.Top = 45;
            file_name_clone.Left = 14;
            file_name_clone.Width = 230;
            file_name_clone.TabIndex = 0;
            file_name_clone.BorderStyle = BorderStyle.Fixed3D;
            file_name_clone.Text = combo_scenes.SelectedItem.ToString() + "_2";

            Button boton_ok_clone = new Button();

            boton_ok_clone.Parent = frmName;
            boton_ok_clone.Left = 247;
            boton_ok_clone.Top = 44;
            boton_ok_clone.Width = 60;
            boton_ok_clone.Height = 22;
            if (language == "es") boton_ok_clone.Text = "Aceptar";
            if (language == "en") boton_ok_clone.Text = "OK";
            boton_ok_clone.Click += new EventHandler(boton_ok_clone_Click);

            frmName.StartPosition = FormStartPosition.CenterScreen;
            duplicating = true;
            frmName.ShowDialog();            
            refresh_scenes();
            duplicating = false;
            btn_refresh.Invoke(new MethodInvoker(delegate
            {
                btn_refresh.PerformClick();
            }));
            
            combo_scenes.SelectedIndex = combo_scenes.FindString(file_name_clone.Text);
            
            if (combo_scenes.SelectedIndex == -1)
            {
                combo_scenes.Items.Clear();
                foreach (String item in Combo_scene.Items)
                {
                    if (item.ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        combo_scenes.Items.Add(item);
                    }
                }
                if (combo_scenes.Items.Count > 0) combo_scenes.SelectedIndex = 0;
            }
        }

        private void boton_ok_clone_Click(object sender, EventArgs e)
        {
            scene_clone = file_name_clone.Text;
            if (scene_clone.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                if (language == "es") MessageBox.Show("El nombre elegido contiene caracteres no válidos. Utilice otro nombre.");
                if (language == "en") MessageBox.Show("Slected name contains invalid characters. Please use a different name.");
                return;
            }

            String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");
            obs_prof_or = obs_prof_or + "\\" + "basic" + "\\" + "scenes" + "\\" + combo_scenes.SelectedItem.ToString() + ".json";

            String obs_prof_or_dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");
            obs_prof_or_dest = obs_prof_or_dest + "\\" + "basic" + "\\" + "scenes" + "\\" + scene_clone + ".json";


            try
            {
                new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyFile(obs_prof_or, obs_prof_or_dest, false);
            }
            catch
            {
                if (language == "es") MessageBox.Show("Error al copiar escena. Utilice otro nombre y pruebe de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                if (language == "en") MessageBox.Show("Error copying scene collection. Please use a different name and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            if (System.IO.File.Exists(obs_prof_or_dest))
            {
                if (locked == true) System.IO.File.SetAttributes(obs_prof_or_dest, System.IO.FileAttributes.Normal);
            }

            //Set duplicated scene internal name

            Boolean found_key = false;
            Boolean match = false;
            String name_found = String.Empty;

            List<string> list_lines = new List<string>();
            foreach (String line in System.IO.File.ReadLines(obs_prof_or_dest))
            {
                list_lines.Add(line);
            }
            StreamWriter SaveFile = new System.IO.StreamWriter(obs_prof_or_dest);

            foreach (String line in list_lines)
            {
                String to_write = line;
                if (line.Contains('\u0022' + "name" + '\u0022' + ": ") && found_key == true)
                {
                    to_write = '\u0022' + "name" + '\u0022' + ": " + '\u0022' + scene_clone + '\u0022' + ",";
                }
                else
                {
                    match = false;
                }

                if (line.Contains("},"))
                {
                    found_key = true;
                }
                else
                {
                    found_key = false;
                }
                SaveFile.WriteLine(to_write);
            }

            //End set internal scene name

            System.Threading.Thread.Sleep(150);
            combo_scenes.SelectedIndex = combo_scenes.FindString(file_name_clone.Text);

            if (System.IO.File.Exists(obs_prof_or_dest))
            {
                if (locked == true) System.IO.File.SetAttributes(obs_prof_or, System.IO.FileAttributes.ReadOnly);
            }
            SaveFile.Close();
            ActiveForm.Close();
        }

        private void frmName_FormClosed(object sender, FormClosedEventArgs e)
        {
            bad_pass = true;
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (combo_scenes.SelectedItem == null) return;
            String sel = combo_scenes.SelectedItem.ToString();
            if (sel == "Por_defecto" || sel == "Comenzar_con_video" || sel == "Comenzar_con_mixto" || sel == "Comenzar_con_pc" || sel == "Start_with_video" || sel == "Start_with_mix" || sel == "Start_with_pc" || sel == "Default")
            {
                if (language == "es") MessageBox.Show("No se puede eliminar una escena básica.");
                if (language == "en") MessageBox.Show("A basic scene cannot be removed");
                return;
            }
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

            Button boton_ok_2 = new Button();

            boton_ok_2.Parent = frmInfo;
            boton_ok_2.Left = 247;
            boton_ok_2.Top = 44;
            boton_ok_2.Width = 60;
            boton_ok_2.Height = 22;
            if (language == "es") boton_ok_2.Text = "Aceptar";
            if (language == "en") boton_ok_2.Text = "OK";
            boton_ok_2.Click += new EventHandler(boton_ok_2_Click);

            frmInfo.StartPosition = FormStartPosition.CenterScreen;
            frmInfo.ShowDialog();

            if (bad_pass == true) return;
            DialogResult a = DialogResult.None;
            if (language == "es") a = MessageBox.Show("¿Desea eliminar la colección de escenas " + combo_scenes.SelectedItem.ToString() + " ?", "Confirmar eliminación", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (language == "en") a = MessageBox.Show("¿Do you with to remove the scene collection " + combo_scenes.SelectedItem.ToString() + " ?", "Confirm action", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (a == DialogResult.Yes)
            {
                String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");
                String obs_prof_or2 = obs_prof_or;
                String obs_prof_or3 = obs_prof_or;
                obs_prof_or = obs_prof_or + "\\" + "basic" + "\\" + "scenes" + "\\" + combo_scenes.SelectedItem.ToString() + ".json";
                obs_prof_or2 = obs_prof_or2 + "\\" + "basic" + "\\" + "scenes" + "\\" + combo_scenes.SelectedItem.ToString() + ".json.bak";
                obs_prof_or3 = obs_prof_or2 + "\\" + "basic" + "\\" + "scenes" + "\\" + combo_scenes.SelectedItem.ToString() + ".json.bak.tmp";

                if (System.IO.File.Exists(obs_prof_or)) System.IO.File.SetAttributes(obs_prof_or, System.IO.FileAttributes.Normal);
                if (System.IO.File.Exists(obs_prof_or2)) System.IO.File.SetAttributes(obs_prof_or2, System.IO.FileAttributes.Normal);
                if (System.IO.File.Exists(obs_prof_or3)) System.IO.File.SetAttributes(obs_prof_or3, System.IO.FileAttributes.Normal);
                System.Threading.Thread.Sleep(200);

                if (System.IO.File.Exists(obs_prof_or))
                {
                    try
                    {
                        System.IO.File.Delete(obs_prof_or);
                        if (System.IO.File.Exists(obs_prof_or2)) System.IO.File.Delete(obs_prof_or2);
                        if (System.IO.File.Exists(obs_prof_or3)) System.IO.File.Delete(obs_prof_or3);

                    }
                    catch (Exception excpt)
                    {
                        if (language == "es") MessageBox.Show("Error al eliminar la colección seleccionada." + excpt.Message);
                        if (language == "en") MessageBox.Show("Error removing selected collection." + excpt.Message);
                    }
                    finally
                    {
                        refresh_scenes();
                    }
                }
            }
        }

        private void boton_ok_2_Click(object sender, System.EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            String current_item = "";
            if (combo_scenes.SelectedItem != null) current_item = combo_scenes.SelectedItem.ToString();
            String obs_prof_or_2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio");
            obs_prof_or_2 = obs_prof_or_2 + "\\" + "basic" + "\\" + "scenes";
            if (combo_scenes.Items.Count > 0)
            {
                combo_scenes.Items.Clear();
            }
            //Cargar escenas obs
            
            foreach (String file in Directory.GetFiles(obs_prof_or_2))
            {
                String item = System.IO.Path.GetExtension(file);
                String name = System.IO.Path.GetFileNameWithoutExtension(file);
                if (language == "es")
                {
                    if (item == ".json" && name != "Por_defecto" && name != "Comenzar_con_video" && name != "Comenzar_con_mixto" && name != "Comenzar_con_pc")
                    {
                        combo_scenes.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                    }
                }
                if (language == "en")
                {
                    if (item == ".json" && name != "Default" && name != "Start_with_video" && name != "Start_with_mix" && name != "Start_with_pc")
                    {
                        combo_scenes.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                    }
                }
            }
            if (combo_scenes.Items.Count > 0) combo_scenes.SelectedIndex = 0;
            else
            {
                if (language == "es") MessageBox.Show("No se encontraron escenas en la aplicación de los platós. Restaure una configuración válida, o contacte con soporte técnico.", "No se encontraron escenas validas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("No scenes were found in studio configuration. Please contact technical support, or restore a valid configuration.", "No valid scenes found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActiveForm.Close();
                return;
            }
            combo_scenes.Items.Insert(0, "---------------------");
            foreach (String file in Directory.GetFiles(obs_prof_or_2))
            {
                if (language == "es")
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Comenzar_con_video") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Comenzar_con_mixto") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Comenzar_con_pc") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Por_defecto") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));

                }
                if (language == "en")
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Start_with_video") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Start_with_mix") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Start_with_pc") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                    if (System.IO.Path.GetFileNameWithoutExtension(file) == "Default") combo_scenes.Items.Insert(0, System.IO.Path.GetFileNameWithoutExtension(file));
                }
            }
            //Fin cargar escenas obs      

        Boolean itemExists = false;
            foreach (String cbi in combo_scenes.Items)
            {
                itemExists = cbi.Equals(current_item);
                if (itemExists)
                {
                    combo_scenes.SelectedIndex = combo_scenes.FindString(current_item);
                    break;
                }
            }
            Combo_scene.Items.Clear();
            foreach (String item in combo_scenes.Items)
            {
                Combo_scene.Items.Add(item);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (first_run == true)
            {
                first_run = false;
                combo_scenes.SelectedIndex = combo_scenes.FindString(System.IO.File.ReadAllText(path_combo));
                return;
            }
            else
            {

                if (combo_scenes.SelectedItem == null) return;
                if (combo_scenes.SelectedItem.ToString().Contains("-----")) combo_scenes.SelectedIndex = combo_scenes.SelectedIndex + 1;
                try
                {
                    if (duplicating == false) System.IO.File.WriteAllText(path_combo, combo_scenes.SelectedItem.ToString());
                }
                catch { }
            }
        }

        private void n_delay_ValueChanged(object sender, EventArgs e)
        {
            String path_count = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "count_scene.ini";
            System.IO.File.WriteAllText(path_count, n_delay.Value.ToString());

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (device == null || chk_mon_audio.CheckState == CheckState.Unchecked)
            {
                timer1.Stop();
                return;
            }
            peak_audio = device.AudioMeterInformation.MasterPeakValue;
            progressBar1.Value = (int)(Math.Round(device.AudioMeterInformation.MasterPeakValue * 100));
            if (device.AudioMeterInformation.MasterPeakValue < 0.004) progressBar1.Value = 2;
            progressBar1.Refresh();
            if (peak_audio < threshold_aud) pic_mute.Image = img_audio_2.Images[1];
            else pic_mute.Image = img_audio_2.Images[0];

        }

        private void timer2_Tick(object sender, EventArgs e)
        {            
            this.Invoke(new MethodInvoker(delegate
            {
                        
            if (peak_audio < threshold_aud && combo_audio.SelectedItem != null && mon_audio == true)
            {
                timer3.Start();
            }
            else
            {
                timer3.Stop();
            }
            }));
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            String path_aud = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "sel_aud.ini";

            if (combo_audio.SelectedItem != null)
            {
                if (combo_audio.SelectedIndex == combo_audio.FindString("Default"))
                {
                    device = (MMDevice)combo_audio.Items[combo_audio.FindString(def_aud)];
                }
                else
                {
                    device = (MMDevice)combo_audio.SelectedItem;
                }
                if (first_run == true)
                {
                    first_run = false;
                    combo_audio.SelectedIndex = combo_audio.FindString(System.IO.File.ReadAllText(path_aud));
                    return;
                }
                else
                {
                    init_audio();
                    System.IO.File.WriteAllText(path_aud, combo_audio.SelectedItem.ToString());
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("obs64");
            if (pname.Length != 0) obs_run = true;
            else obs_run = false;

            if (obs_run == false) return;
            frm_11.btn_update.Invoke(new MethodInvoker(delegate
            {
            
            if (peak_audio < threshold_aud && combo_audio.SelectedItem != null && mon_audio == true)
            {
                this.TopMost = true;
                timer2.Stop();
                timer3.Stop();
                if (language == "es") MessageBox.Show("ATENCIÓN: No se detecta señal en el micrófono seleccionado." + Environment.NewLine + Environment.NewLine + "Revise que el micrófono tiene pilas cargadas y que no está en posición " + '\u0022' + "mute." + '\u0022' + ".", "No se detectó sonido de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (language == "en") MessageBox.Show("WARNING: No audio signal detected on selected microphone." + Environment.NewLine + Environment.NewLine + "Please check microphone batteries and mute selector." + '\u0022' + "mute." + '\u0022' + ".", "No input sound detected for too long", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.WindowState = FormWindowState.Minimized;
                this.TopMost = false;
            }
            }));
        }

        private void btn_aud_dev_Click(object sender, EventArgs e)
        {
            Process a = new Process();
            a.StartInfo.FileName = "control";
            a.StartInfo.Arguments = "mmsys.cpl,,1";
            a.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
            UnregisterHotKey(this.Handle, 0);       // Unregister hotkey with id 0 before closing the form. You might want to call this more than once with different id values if you are planning to register more than one hotkey.
            StopCameras();
            if (obs_launched == true)
            {
                DialogResult a = DialogResult.None;
                if (language == "es") a = MessageBox.Show("OBS ha sido lanzado y está en ejecución. Para salir primero debe cerrarlo." + Environment.NewLine + Environment.NewLine + "¿Desea salir de todos modos?", "Confirmar cierre del programa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (language == "en") a = MessageBox.Show("OBS was launched. You need to close it before exiting application." + Environment.NewLine + Environment.NewLine + "¿Do you want to close studio application anyway?", "Confirm application closing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Cancel || a == DialogResult.No) e.Cancel = true;
            }
            }));
        }

        private void chk_mon_audio_CheckedChanged(object sender, EventArgs e)
        {
            if (combo_audio.Items.Count == 0 && first_run == false)
            {
                chk_mon_audio.CheckState = CheckState.Unchecked;
            }

            String path_mon_aud = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "mon_aud.ini";
            if (chk_mon_audio.CheckState == CheckState.Unchecked)
            {
                mon_audio = false;
                combo_audio.Enabled = false;
                timer1.Stop();
                progressBar1.Value = 0;
                pic_mute.Image = img_audio_2.Images[1];
                System.IO.File.WriteAllText(path_mon_aud, String.Empty);
            }

            else
            {
                mon_audio = true;
                combo_audio.Enabled = true;
                timer1.Start();
                System.IO.File.Delete(path_mon_aud);
            }
        }

        private void combo_lang_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (frm_11.combo_lang.SelectedIndex == 0)
            {
                plato_saga.Properties.Settings.Default.app_lang = "es";
                language = "es";
                plato_saga.Properties.Settings.Default.Save();

            }
            if (frm_11.combo_lang.SelectedIndex == 1)
            {
                plato_saga.Properties.Settings.Default.app_lang = "en";
                language = "en";
                plato_saga.Properties.Settings.Default.Save();
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            RefreshResources(this, resources);
            create_tips();
            show_devs_panel();
            frm_11.btn_update.Text = "Version " + Application.ProductVersion;
        }

        private void create_tips()
        {            
            T00.AutoPopDelay = 5000;
            T00.InitialDelay = 1000;
            T00.ReshowDelay = 500;
            T00.ShowAlways = true;
            T01.AutoPopDelay = 5000;

            T01.InitialDelay = 1000;
            T01.ReshowDelay = 500;
            T01.ShowAlways = true;
            if (language == "es") T01.SetToolTip(this.btn_clone, "Clonar colección de escenas");
            if (language == "en") T01.SetToolTip(this.btn_clone, "Clone scene collection");
                        
            T02.AutoPopDelay = 5000;
            T02.InitialDelay = 1000;
            T02.ReshowDelay = 500;
            T02.ShowAlways = true;
            if (language == "es") T02.SetToolTip(this.btn_del, "Eliminar colección de escenas");
            if (language == "en") T02.SetToolTip(this.btn_del, "Remove scene collection");
        }


        private void button10_Click_1(object sender, EventArgs e)
        {
            if (Camera1Combo.SelectedIndex == -1) return;
            VideoCaptureDevice videoSource1 = new VideoCaptureDevice(videoDevices[Camera1Combo.SelectedIndex].MonikerString);
            IntPtr ptr = new IntPtr();
            
            try
            {
                videoSource1.DisplayPropertyPage(ptr);
            }
            catch (Exception excpt)
            {
                MessageBox.Show("Error: " + excpt.Message);
            }
        }

        private void StopCameras()
        {
            this.Width = 745;
            timer_cam.Stop();
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.WaitForStop();
        }

        private void timer_cam_Tick(object sender, EventArgs e)
        {
            IVideoSource videoSource1 = videoSourcePlayer1.VideoSource;

            int framesReceived1 = 0;
            int framesReceived2 = 0;

            // get number of frames for the last second
            if (videoSource1 != null)
            {
                framesReceived1 = videoSource1.FramesReceived;
            }

            if (stopWatch == null)
            {
                stopWatch = new Stopwatch();
                stopWatch.Start();
            }
            else
            {
                stopWatch.Stop();

                float fps1 = 1000.0f * framesReceived1 / stopWatch.ElapsedMilliseconds;
                float fps2 = 1000.0f * framesReceived2 / stopWatch.ElapsedMilliseconds;

                //camera1FpsLabel.Text = fps1.ToString("F2") + " fps";
                //camera2FpsLabel.Text = fps2.ToString("F2") + " fps";

                stopWatch.Reset();
                stopWatch.Start();
            }
        }
        
        private void btn_stop_cam_Click(object sender, EventArgs e)
        {
            StopCameras();
            videoSourcePlayer1.Text = "Previsualización detenida";
            btn_stop_cam.Enabled = false;
            btn_preview_camera.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Camera1Combo.SelectedItem != null)
            {
                btn_stop_cam.Enabled = true;
                btn_preview_camera.Enabled = false;
                this.Width = 1013;
                VideoCaptureDevice videoSource1 = new VideoCaptureDevice(videoDevices[Camera1Combo.SelectedIndex].MonikerString);

                //IntPtr ptr = new IntPtr();
                videoSourcePlayer1.VideoSource = videoSource1;
                videoSourcePlayer1.Start();

                // reset stop watch
                stopWatch = null;
                // start timer
                timer_cam.Start();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            combo_audio.Items.Clear();
            init_audio_devs();
            timer1.Start();
        }

        private void Camera1Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String path_vid = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "sel_vid.ini";
            System.IO.File.WriteAllText(path_vid, Camera1Combo.SelectedItem.ToString());
            if (btn_stop_cam.Enabled == true)
            {
                if (Camera1Combo.SelectedItem != null)
                {
                    VideoCaptureDevice videoSource1 = new VideoCaptureDevice(videoDevices[Camera1Combo.SelectedIndex].MonikerString);

                    videoSourcePlayer1.VideoSource = videoSource1;
                    videoSourcePlayer1.Start();

                    // reset stop watch
                    stopWatch = null;
                    // start timer
                    timer_cam.Start();
                }
            }
        }

        private void btn_refres_vid_Click(object sender, EventArgs e)
        {
            // Show Video device list
            try
            {
                // enumerate video devices
                Camera1Combo.Items.Clear();
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                {
                    throw new Exception();
                }

                for (int i = 1, n = videoDevices.Count; i <= n; i++)
                {
                    string cameraName = i + " : " + videoDevices[i - 1].Name;

                    Camera1Combo.Items.Add(cameraName);
                }

                // check cameras count
                String path_vid = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "platosaga") + "\\" + "sel_vid.ini";
                if (videoDevices.Count > 1)
                {
                    if (System.IO.File.Exists(path_vid))
                    {
                        Camera1Combo.SelectedIndex = Camera1Combo.FindString(System.IO.File.ReadAllText(path_vid));
                    }
                    else
                    {
                        Camera1Combo.SelectedIndex = 0;
                    }

                }
                else
                {
                    if (language == "es") MessageBox.Show("No se detectó ningún dispositivo de captura de vídeo.", "No hay dispositivos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (language == "en") MessageBox.Show("No video capture devices detected.", "No devices found ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

            }
        }

        public static bool ApplicationIsActivated()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;       // No window is currently activated
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }
        private void btn_import_Click(object sender, EventArgs e)
        {
            FileD.Filter = "Scene files (*.json)|*.json";
            FileD.ShowDialog();
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio" + "\\" + "basic" + "\\" + "scenes");
            fd1.ShowNewFolderButton = true;

            if (language == "es")
            {
                fd1.Description = "Seleccione la ruta para guardar la colección de escenas seleccionada.";
            }
            if (language == "en")
            {
                fd1.Description = "Select path to save the selected scene collection.";
            }

            if (fd1.ShowDialog() == DialogResult.OK)
            {
                btn_refresh.PerformClick();
                String file_sc = obs_prof_or + "\\" + combo_scenes.SelectedItem.ToString() + ".json";
                try
                {
                    System.IO.File.Copy(file_sc, fd1.SelectedPath + "\\" + combo_scenes.SelectedItem.ToString() + ".json");
                }
                catch (Exception excpt)
                {
                    MessageBox.Show("Error al guardar la escena." + Environment.NewLine + Environment.NewLine + excpt.Message);
                }
            }
        }

        private void FileD_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            String obs_prof_or = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio" + "\\" + "basic" + "\\" + "scenes");
            if (System.IO.File.Exists(obs_prof_or + "\\" + Path.GetFileName(FileD.FileName)))
            {

                DialogResult a = DialogResult.None;
                if (language == "es") a = MessageBox.Show("Fichero de escenas ya existe. ¿Sobreescribir?", "Confirmar acción", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (language == "en") a = MessageBox.Show("Scene collection file already exists. Overwrite it?", "Confirm action", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (a != DialogResult.Yes) return;
            }
            try
            {
                System.IO.File.Copy(FileD.FileName, obs_prof_or + "\\" + Path.GetFileName(FileD.FileName), true);
            }
            catch (Exception excpt)
            {
                if (language == "es") MessageBox.Show("Se produjo un error al importar la colección seleccionada: " + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("An error occurred importing selected collection: " + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    
        private void check_updates()
        {
            String current_ver = frm_11.btn_update.Text;
            frm_11.btn_update.Refresh();
            String content1 = String.Empty;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;

                try
                {
                    if (start_up == true) Thread.Sleep(250);
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
                        this.Enabled = true;
                    }));


                    if (language == "es") MessageBox.Show("Hubo un error al conectar al servidor de actualizaciones." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (language == "en") MessageBox.Show("An error occurred conecting to update service." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Enable_Controls();                    
                    
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
                            this.Invoke(new MethodInvoker(delegate
                            {
                                this.Activate();
                            }));

                            String downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
                            dest_update = downloadsPath + "\\" + "Setup_saga_" + content1.Substring(0, 5) + ".exe";

                            String down_link = base_update_server + "/" + content1.Substring(0, 5) + "/" + "Setup_saga_" + content1.Substring(0, 5) + ".exe";
                            String res = RemoteFileExists(down_link).ToString();

                            pg_download.Invoke(new MethodInvoker(delegate
                            {
                                pg_download.Visible = true;
                                pg_download.Style = ProgressBarStyle.Marquee;

                            }));
                            lbl_dowload.Invoke(new MethodInvoker(delegate
                            {
                                
                                lbl_dowload.Visible = true;
                                lbl_down_2.Visible = true;
                                lbl_dowload.Text = "";
                                if (language == "es") lbl_down_2.Text = "Actualizando...";
                                if (language == "en") lbl_down_2.Text = "Updating...";

                            }));

                            cli.DownloadFileAsync(new Uri(down_link), dest_update);
                            update_file = dest_update;
                            cli.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                            cli.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback2);
                            download_progressing = false;
                        }
                    }
                    else
                    {
                        if (start_up == false)
                        {
                            if (language == "es") MessageBox.Show("Está usando la versión más reciente.", "Aplicación está actualizada", MessageBoxButtons.OK);
                            if (language == "en") MessageBox.Show("You are using the latest version.", "No update found", MessageBoxButtons.OK);
                        }
                    }
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Activate();
                    }));
                }
                catch (Exception excpt)
                {
                    if (language == "es") MessageBox.Show("Se produjo un error al buscar actualizaciones." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (language == "en") MessageBox.Show("There was an error checking for updates." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                start_up = false;
                
                Enable_Controls();                

                this.Invoke(new MethodInvoker(delegate
                {
                    this.Enabled = true;
                }));               
                

            }).Start();
        }

        private void update_app()
        {         
            String current_ver = "Version " + Application.ProductVersion;
            String content1 = frm_11.content1;                    

            this.Invoke(new MethodInvoker(delegate
            {
                this.Activate();
            }));

            new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.CurrentThread.IsBackground = true;                   

                    try
                    {
                        if (Convert.ToInt16(content1.Replace(".", String.Empty).Substring(0, 3)) > Convert.ToInt16(Application.ProductVersion.Replace(".", String.Empty)))
                        {
                            DialogResult a = DialogResult.None;

                            this.Invoke(new MethodInvoker(delegate
                            {
                                this.Enabled = false;
                            }));

                                String downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
                                dest_update = downloadsPath + "\\" + "Setup_saga_" + content1.Substring(0, 5) + ".exe";

                                String down_link = base_update_server + "/" + content1.Substring(0, 5) + "/" + "Setup_saga_" + content1.Substring(0, 5) + ".exe";
                                String res = RemoteFileExists(down_link).ToString();

                                pg_download.Invoke(new MethodInvoker(delegate
                                {
                                    pg_download.Visible = true;
                                    pg_download.Style = ProgressBarStyle.Marquee;

                                }));
                                lbl_dowload.Invoke(new MethodInvoker(delegate
                                {
                                    lbl_dowload.Visible = true;
                                    lbl_down_2.Visible = true;
                                    lbl_dowload.Text = "";
                                    lbl_down_2.Text = "";

                                }));

                                cli.DownloadFileAsync(new Uri(down_link), dest_update);
                                update_file = dest_update;
                                cli.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                                cli.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback2);
                                download_progressing = false;                            
                        }                       
                    }
                    catch (Exception excpt)
                    {
                        if (language == "es") MessageBox.Show("Se produjo un error al buscar actualizaciones." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (language == "en") MessageBox.Show("There was an error checking for updates." + Environment.NewLine + Environment.NewLine + excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    start_up = false;
                    Enable_Controls();
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Enabled = true;
                    }));

                }).Start();            
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            check_updates();
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            pg_download.Invoke(new MethodInvoker(delegate
            {                
                pg_download.Value = e.ProgressPercentage;
                pg_download.Style = ProgressBarStyle.Continuous;

            }));

            if (_startedAt == default(DateTime))
            {
                _startedAt = DateTime.Now;
            }
            else
            {
                var timeSpan = DateTime.Now - _startedAt;
                
                if (timeSpan.TotalSeconds > 1)
                {
                    if (download_progressing == false)
                    {
                        pg_download.Invoke(new MethodInvoker(delegate
                        {
                            pg_download.Style = ProgressBarStyle.Continuous;
                            download_progressing = true;
                        }));
                    }

                    try
                    {
                        var bytesPerSecond = e.BytesReceived / 1048576 / (long)timeSpan.TotalSeconds;

                        Decimal speed = Math.Round((decimal)bytesPerSecond, 2);
                        lbl_dowload.Invoke(new MethodInvoker(delegate
                        {
                            lbl_dowload.Text = bytesPerSecond.ToString() + " MB/s";
                        }));
                    }
                    catch { }
                }
            }



            //Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...", (string)e.UserState, e.BytesReceived,e.TotalBytesToReceive, e.ProgressPercentage);
        }

        private void DownloadFileCallback2(object sender, AsyncCompletedEventArgs e)
        {
            Boolean quit = false;
            lbl_dowload.Invoke(new MethodInvoker(delegate
            {
                lbl_dowload.Text = String.Empty;
                lbl_down_2.Text = String.Empty;
            }));
            pg_download.Invoke(new MethodInvoker(delegate
            {
                pg_download.Visible = false;

            }));

            if (!System.IO.File.Exists(dest_update))
            {
                if (language == "es") MessageBox.Show("Error en la descarga");
                if (language == "en") MessageBox.Show("Download error");
                return;
            }
            
            DialogResult a = DialogResult.None;
            Thread.Sleep(500);
            
            if (!File.Exists(update_file))
            {
                if (language == "es") MessageBox.Show("Se produjo un error al descargar la actualización. No se encontró el archivo solicitado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("An error ocurred downloading update. File not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FileInfo fi = new FileInfo(update_file);
            if (fi.Length == 0)
            {
                if (language == "es") MessageBox.Show("Se produjo un error al descargar la actualización. No se encontró el archivo solicitado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (language == "en") MessageBox.Show("An error ocurred downloading update. File not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    File.Delete(update_file);
                }
                catch { }
                return;
            }

            if (language == "es") a = MessageBox.Show("Descarga completada. ¿Instalar actualización?", "Confirmar instalación", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (language == "en") a = MessageBox.Show("Download complete. Install update now?", "Confirm installation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            cli.Dispose();
            if (a != DialogResult.Yes)
            {                
                return;
            }
            else
            {
                try
                {
                    Process.Start(dest_update);
                    quit = true;                    
                }
                catch (Exception excpt)
                {
                    MessageBox.Show(excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (quit == true) Application.Exit();
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

        private void chk_crono_CheckedChanged(object sender, EventArgs e)
        {
            if (frm_11.chk_crono.CheckState == CheckState.Checked)
            {
                plato_saga.Properties.Settings.Default.show_timer = true;
            }
            else
            {
                plato_saga.Properties.Settings.Default.show_timer = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void chk_updates_CheckedChanged(object sender, EventArgs e)
        {
            if (frm_11.chk_updates.CheckState == CheckState.Checked)
            {
                plato_saga.Properties.Settings.Default.auto_updates = true;
            }
            else
            {
                plato_saga.Properties.Settings.Default.auto_updates = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void chk_show_keep_CheckedChanged(object sender, EventArgs e)
        {
            if (frm_11.chk_show_keep.CheckState == CheckState.Checked)
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
            if (frm_11.chk_auto_close_obs.CheckState == CheckState.Checked)
            {
                plato_saga.Properties.Settings.Default.close_obs_auto = true;
            }
            else
            {
                plato_saga.Properties.Settings.Default.close_obs_auto = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void get_silence()
        {
            String val = track_silence.Value.ToString();
            if (track_silence.Value == 0) threshold_aud = 0.001f;
            if (track_silence.Value < 0)
            {
                if (track_silence.Value == -1) threshold_aud = 0.002f;
                if (track_silence.Value == -2) threshold_aud = 0.003f;
                if (track_silence.Value == -3) threshold_aud = 0.005f;
                if (track_silence.Value == -4) threshold_aud = 0.006f;
                if (track_silence.Value == -5) threshold_aud = 0.008f;
                if (track_silence.Value == -6) threshold_aud = 0.009f;
                if (track_silence.Value == -7) threshold_aud = 0.010f;
                if (track_silence.Value == -8) threshold_aud = 0.012f;
                if (track_silence.Value == -9) threshold_aud = 0.015f;
                if (track_silence.Value == -10) threshold_aud = 0.02f;
            }
            if (track_silence.Value > 0)
            {
                if (track_silence.Value == 1) threshold_aud = 0.0009f;
                if (track_silence.Value == 2) threshold_aud = 0.0007f;
                if (track_silence.Value == 3) threshold_aud = 0.0005f;
                if (track_silence.Value == 4) threshold_aud = 0.0003f;
                if (track_silence.Value == 5) threshold_aud = 0.0002f;
                if (track_silence.Value == 6) threshold_aud = 0.0001f;
                if (track_silence.Value == 7) threshold_aud = 0.00009f;
                if (track_silence.Value == 8) threshold_aud = 0.00007f;
                if (track_silence.Value == 9) threshold_aud = 0.00005f;
                if (track_silence.Value == 10) threshold_aud = 0.00002f;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            threshold_aud = 0.001f;
            get_silence();
            plato_saga.Properties.Settings.Default.silence_level = track_silence.Value;
            lbl_thr.Text = track_silence.Value.ToString() + " dB";
            plato_saga.Properties.Settings.Default.Save();
        }

        private void timer_startup_Tick(object sender, EventArgs e)
        {
            if (combo_audio.Items.Count > 2 && combo_audio.SelectedIndex == combo_audio.FindString("Default") && chk_mon_audio.CheckState == CheckState.Checked)
            {
                timer_startup.Stop();
                timer_startup.Dispose();
                btn_refresh_audio.PerformClick();
            }
            else
            {
                timer_startup.Stop();
                timer_startup.Dispose();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "Buscar" || textBox1.Text == "Search") return;
            combo_scenes.Items.Clear();
            foreach (String item in Combo_scene.Items)
            {
                if (item.ToLower().Contains(textBox1.Text.ToLower()))
                {
                    combo_scenes.Items.Add(item);
                }
            }
            if (combo_scenes.Items.Count > 0) combo_scenes.SelectedIndex = 0;
        }

        private void chk_validate_CheckedChanged(object sender, EventArgs e)
        {
            if (frm_11.chk_validate.CheckState == CheckState.Checked)
            {
                plato_saga.Properties.Settings.Default.validate_scene = true;
            }
            else
            {
                plato_saga.Properties.Settings.Default.validate_scene = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void chk_mon_audio_MouseClick(object sender, MouseEventArgs e)
        {
            if (chk_mon_audio.CheckState == CheckState.Checked) btn_refresh_audio.PerformClick();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Buscar" || textBox1.Text == "Search")
            {
                textBox1.Text = "";
                textBox1.BackColor = Color.White;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                if (language == "es") textBox1.Text = "Buscar";
                if (language == "en") textBox1.Text = "Search";
                textBox1.BackColor = Control.DefaultBackColor;
            }
        }
        public bool RemoteFileExists(string url)
        {
            lbl_obs_running.Invoke(new MethodInvoker(delegate
            {                            
            if (language == "es") lbl_obs_running.Text = "Contactando con el servidor de actualización. Espere por favor...";
            if (language == "en") lbl_obs_running.Text = "Contacting updates server, please wait...";
            }));

            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 5000; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
                request.Method = "HEAD"; //Get only the header information -- no need to download any content

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    
                    if (statusCode >= 100 && statusCode < 400) //Good requests
                    {

                        lbl_obs_running.Invoke(new MethodInvoker(delegate
                        {
                            lbl_obs_running.Text = String.Empty;
                        }));
                        return true;
                    }
                    else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                    {
                        //log.Warn(String.Format("The remote server has thrown an internal error. Url is not valid: {0}", url));
                        Debug.WriteLine(String.Format("The remote server has thrown an internal error. Url is not valid: {0}", url));
                        
                        lbl_obs_running.Invoke(new MethodInvoker(delegate
                        {
                            lbl_obs_running.Text = String.Empty;
                        }));

                        return false;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {
                    lbl_obs_running.Invoke(new MethodInvoker(delegate
                    {
                        lbl_obs_running.Text = String.Empty;
                    }));                   
                    
                    return false;
                }
                else
                {
                    lbl_obs_running.Invoke(new MethodInvoker(delegate
                    {
                        lbl_obs_running.Text = String.Empty;
                    }));
                    MessageBox.Show(String.Format("Unhandled status [{0}] returned for url: {0}", ex.Status), ex.Message);
                    
                }
            }
            catch (Exception ex)
            {
                lbl_obs_running.Invoke(new MethodInvoker(delegate
                {
                    lbl_obs_running.Text = String.Empty;
                }));
                MessageBox.Show(String.Format("Could not test url {0}.", url), ex.Message);                
            }
            return false;
            
        }

        private void BG_rec_DoWork(object sender, DoWorkEventArgs e)
        {
            loading_obs = true;

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
                        if (!t.Wait(9000))
                        {
                            p.Kill();
                        }
                    }
                    catch
                    {
                        if (language == "es") MessageBox.Show("Error al cerrar OBS Studio. Debe cerrarlo para continuar: " + p.Id, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (language == "en") MessageBox.Show("An error occurred while closing OBS Studio. You need to close it to continue: " + p.Id, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        loading_obs = false;
                        this.Cursor = Cursors.Arrow;
                        this.Enabled = true;
                        return;
                    }
                }
            }
            combo_scenes.Invoke(new MethodInvoker(delegate
            {
                if (language == "es")
                {
                    if (combo_scenes.SelectedItem.ToString().Contains("Comenzar_con_"))
                    {
                        MessageBox.Show("Ha seleccionado una colección básica, que no debería ser modificada." + Environment.NewLine + Environment.NewLine + "Para crear y modificar su propia colección de escenas utilice el botón Clonar.", "Colección básica seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                if (language == "en")
                {
                    if (combo_scenes.SelectedItem.ToString().Contains("Start_with_"))
                    {
                        MessageBox.Show("A basic collection was selected, which should not be modified." + Environment.NewLine + Environment.NewLine + " In order to use and customize it, please create your own by pressing the button Clone.", "Default collection selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }));

            Form7 frm_load_obs = new Form7();

            this.Invoke(new MethodInvoker(delegate
            {
                previewed_scenes.Add(combo_scenes.SelectedItem.ToString());

                if (language == "es")
                {
                    frm_load_obs.textBox1.Text = "Iniciando grabación";
                    frm_load_obs.label2.Text = combo_scenes.SelectedItem.ToString();
                }
                if (language == "en")
                {
                    frm_load_obs.textBox1.Text = "Starting recording";
                    frm_load_obs.label2.Text = combo_scenes.SelectedItem.ToString();
                }
            }));

            
            if (frm_11.chk_crono.CheckState == CheckState.Checked)
            {
                new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.CurrentThread.IsBackground = true;
                    obs_launched = false;
                    frm_timer.TopMost = true;
                    frm_timer.Text = "Crono";
                    frm_timer.lbl_elapsed.Text = "00h:00m:00s";
                    frm_timer.timer_recorded.Start();
                    Thread.Sleep(3000);
                    frm_timer.ShowDialog();
                    Thread.Sleep(500);

                }).Start();
            }

            //String shortcutAddress = fd1.SelectedPath + @"\Preview_" + scene_global_name + ".lnk";
            proc.StartInfo.FileName = obs_exec;
            proc.StartInfo.WorkingDirectory = obs_path;
            proc.StartInfo.Arguments = "--profile Plato --collection " + '\u0022' + scene_global + '\u0022' + " --scene Pre-Intro" + " --startrecording";
            
            if (Properties.Settings.Default.max_obs == true) proc.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            else proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            
            obs_launched = true;
            recording = true;
            timer2.Start();
            proc.Start();

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                frm_load_obs.ShowDialog();
                frm_load_obs.Refresh();

            }).Start();

            if (plato_saga.Properties.Settings.Default.to_prompt == true)
            {
                new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.CurrentThread.IsBackground = true;
                    try
                    {
                        frm_tele.Invoke(new MethodInvoker(delegate
                        {
                            if (plato_saga.Properties.Settings.Default.rem_lines == false)
                            {
                                frm_tele.rtx1.Text = Environment.NewLine + Environment.NewLine + pr_text;
                            }
                            else frm_tele.rtx1.Text = pr_text;
                            if (plato_saga.Properties.Settings.Default.pr_delay == true)
                            {
                                Thread.Sleep(plato_saga.Properties.Settings.Default.pr_delay_val * 1000);
                            }

                            frm_tele.ShowDialog();
                        }));
                    }
                    catch
                    {
                        if (plato_saga.Properties.Settings.Default.rem_lines == false)
                        {
                            frm_tele.rtx1.Text = Environment.NewLine + Environment.NewLine + pr_text;
                        }
                        else frm_tele.rtx1.Text = pr_text;
                        Thread.Sleep(plato_saga.Properties.Settings.Default.pr_delay_val * 1000);
                        frm_tele.ShowDialog();
                    }

                }).Start();
            }

            while (string.IsNullOrEmpty(proc.MainWindowTitle))
            {
                System.Threading.Thread.Sleep(50);
                proc.Refresh();
            }
           
            Disable_controls();

            frm_load_obs.Invoke(new MethodInvoker(delegate
            {
                frm_load_obs.Close();
                //frm_timer_timer_recorded.Start();
            }));

            lbl_obs_running.Invoke(new MethodInvoker(delegate
            {
                if (language == "es") lbl_obs_running.Text = "Plató en grabación.";
                if (language == "en") lbl_obs_running.Text = "Studio is recording.";

            }));


            loading_obs = false;

            if (Properties.Settings.Default.max_obs == true)
            {
                // Detectar tamaño ventana para poner a pantalla completa
                IntPtr handle = proc.MainWindowHandle;
                Rect1 mspaintRect = new Rect1();
                GetWindowRect(handle, ref mspaintRect);
                if (resolution.Right != mspaintRect.W_Right || resolution.Bottom != mspaintRect.W_Bottom) SendKeys.SendWait("{F11}");
            }
            else
            {
                //Tamaño de ventana
                if (Properties.Settings.Default.max_obs == false)
                {
                    int left = 0;
                    int top = 0;
                    int width = resolution.Width;
                    int height = resolution.Height;

                    if (Properties.Settings.Default.pr_location == 1)
                    {
                        left = 0;
                        top = Properties.Settings.Default.rtx_height - 50;
                        width = resolution.Width;
                        height = resolution.Height - Properties.Settings.Default.rtx_height + 50;
                    }

                    if (Properties.Settings.Default.pr_location == 2)
                    {
                        left = 0;
                        top = 0;
                        width = resolution.Width;
                        height = resolution.Height - Properties.Settings.Default.rtx_height;
                    }

                    if (Properties.Settings.Default.pr_location == 3)
                    {
                        left = Screen.PrimaryScreen.Bounds.Width / 3 + 15;
                        top = 0;
                        width = resolution.Width - left;
                        height = resolution.Height;
                    }
                    if (Properties.Settings.Default.pr_location == 4)
                    {
                        left = 0;
                        top = 0;
                        width = resolution.Width - (Screen.PrimaryScreen.Bounds.Width / 3 + 15);
                        height = resolution.Height;
                    }

                    SetWindowPos(proc.MainWindowHandle,
                            HWND_TOP,
                            left, top, width, height, 0);
                }
            }

            proc.WaitForExit();

            if (frm_11.chk_crono.CheckState == CheckState.Checked)
            {
                frm_timer.Invoke(new MethodInvoker(delegate
                {
                    frm_timer.TopMost = false;
                    frm_timer.Close();
                    //timer_recorded.Stop();
                    time_record = 0;
                }));
            }

            if (plato_saga.Properties.Settings.Default.to_prompt == true)
            {
                try
                {
                    frm_tele.Invoke(new MethodInvoker(delegate
                    {
                        frm_tele.Close();

                    }));
                }
                catch { }
            }

            lbl_obs_running.Invoke(new MethodInvoker(delegate
            {
                lbl_obs_running.Text = String.Empty;
            }));

            lbl_obs_running.Invoke(new MethodInvoker(delegate
            {
                lbl_obs_running.Text = String.Empty;
            }));
            this.Invoke(new MethodInvoker(delegate
            {
                this.TopMost = true;
                this.TopMost = false;
            }));
            obs_launched = false;

        }

        private void BG_rec_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pic_title.Focus();

            if (closed_ok == true && frm_11.chk_show_keep.CheckState == CheckState.Checked)
            {
                var directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
                var LastFile = (from f in directory.GetFiles("*.m??")
                                orderby f.LastAccessTime descending
                                select f).First();

                this.Invoke(new MethodInvoker(delegate
                {
                    this.Enabled = false;
                    this.Activate();
                    this.Cursor = Cursors.Arrow;
                }));
                plato_saga.Form9 frm_save = new plato_saga.Form9();
                frm_save.StartPosition = FormStartPosition.CenterScreen;
                frm_save.text_filename.Text = LastFile.Name;

                frm_save.ShowDialog();
                if (frm_save.keep_file_p == false) move_video_discarded();
                this.Invoke(new MethodInvoker(delegate
                {
                    this.Enabled = true;
                    this.Activate();
                    this.Cursor = Cursors.Arrow;
                }));                
            }
            Enable_Controls();
            this.Enabled = true;
            this.Activate();            
            this.Cursor = Cursors.Arrow;
            pic_title.Focus();
        }
  
        private void btn_set_advanced_Click(object sender, EventArgs e)
        {
            frm_11.ShowDialog();
            if (frm_11.restored == true) btn_refresh.PerformClick();
            if (frm_11.update_now == true) update_app();
            language = plato_saga.Properties.Settings.Default.app_lang;
            if (frm_11.changed_lang == true)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
                RefreshResources(this, resources);                
                create_tips();
                show_devs_panel();
            }
        }

        private void show_devs_panel()
        {
            if (chk_panel_dev.Checked == true)
            {
                plato_saga.Properties.Settings.Default.show_panel = true;
                panel1.Height = 140;                
                this.Height = 654;
            }
            else
            {
                plato_saga.Properties.Settings.Default.show_panel = false;
                panel1.Height = 30;                
                this.Height = 554;
            }
            plato_saga.Properties.Settings.Default.Save();
            show_devs = plato_saga.Properties.Settings.Default.show_panel;            
        }

        private void chk_panel_dev_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_panel_dev.Checked == false) chk_panel_dev.BackColor = Control.DefaultBackColor;
            else chk_panel_dev.BackColor = Color.FromArgb(255,255,255);
            show_devs_panel();
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

        private void btn_prompter_Click(object sender, EventArgs e)
        {
            frm_prompt.chk_rem_lines.Checked = plato_saga.Properties.Settings.Default.rem_lines;
            frm_prompt.n_rem_timer.Value = plato_saga.Properties.Settings.Default.remove_timer / 1000;
            frm_prompt.n_rem_timer.Enabled = frm_prompt.chk_rem_lines.Checked;
            frm_prompt.ShowDialog();
            plato_saga.Properties.Settings.Default.pr_font = frm_prompt.rtx1.Font;
            plato_saga.Properties.Settings.Default.pr_color = frm_prompt.rtx1.ForeColor;
            plato_saga.Properties.Settings.Default.pr_back_color = frm_prompt.rtx1.BackColor;
            plato_saga.Properties.Settings.Default.to_prompt = frm_prompt.chk_tele.Checked;
            plato_saga.Properties.Settings.Default.prompt_sp = frm_prompt.tr_speed.Value;
            plato_saga.Properties.Settings.Default.prompt_txt = frm_prompt.rtx1.Text;
            plato_saga.Properties.Settings.Default.pr_delay = frm_prompt.chk_delay.Checked;
            plato_saga.Properties.Settings.Default.pr_delay_val = (int)frm_prompt.num_delay.Value;
            plato_saga.Properties.Settings.Default.pr_location = frm_prompt.cb_loc.SelectedIndex;
            plato_saga.Properties.Settings.Default.pr_transp = frm_prompt.chk_trans.Checked;
            plato_saga.Properties.Settings.Default.rtx_height = (int)frm_prompt.n_h_rtx.Value;
            plato_saga.Properties.Settings.Default.rem_lines = frm_prompt.chk_rem_lines.Checked;
            plato_saga.Properties.Settings.Default.Save();
            pr_text = frm_prompt.prompt_text;
        }

    }
}