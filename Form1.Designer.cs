namespace plato_saga
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pic_title = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_blackm = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_joiner = new System.Windows.Forms.Button();
            this.btn_pedal = new System.Windows.Forms.Button();
            this.btn_lock = new System.Windows.Forms.Button();
            this.imgs = new System.Windows.Forms.ImageList(this.components);
            this.btn_videos = new System.Windows.Forms.Button();
            this.btn_start_record = new System.Windows.Forms.Button();
            this.btn_preview = new System.Windows.Forms.Button();
            this.btn_clone = new System.Windows.Forms.Button();
            this.combo_scenes = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.fd1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_export = new System.Windows.Forms.Button();
            this.btn_import = new System.Windows.Forms.Button();
            this.n_delay = new System.Windows.Forms.NumericUpDown();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.lbl_scenes = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.combo_audio = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.btn_aud_dev = new System.Windows.Forms.Button();
            this.lbl_obs_running = new System.Windows.Forms.TextBox();
            this.chk_mon_audio = new System.Windows.Forms.CheckBox();
            this.combo_lang = new System.Windows.Forms.ComboBox();
            this.lbl_lang = new System.Windows.Forms.Label();
            this.pic_mute = new System.Windows.Forms.PictureBox();
            this.img_audio_2 = new System.Windows.Forms.ImageList(this.components);
            this.btn_video_prop = new System.Windows.Forms.Button();
            this.Camera1Combo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_preview_camera = new System.Windows.Forms.Button();
            this.timer_cam = new System.Windows.Forms.Timer(this.components);
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.btn_stop_cam = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_refresh_audio = new System.Windows.Forms.Button();
            this.proc = new System.Diagnostics.Process();
            this.process2 = new System.Diagnostics.Process();
            this.btn_refres_vid = new System.Windows.Forms.Button();
            this.FileD = new System.Windows.Forms.OpenFileDialog();
            this.btn_update = new System.Windows.Forms.Button();
            this.gr_settings = new System.Windows.Forms.GroupBox();
            this.chk_validate = new System.Windows.Forms.CheckBox();
            this.chk_auto_close_obs = new System.Windows.Forms.CheckBox();
            this.chk_show_keep = new System.Windows.Forms.CheckBox();
            this.chk_updates = new System.Windows.Forms.CheckBox();
            this.chk_crono = new System.Windows.Forms.CheckBox();
            this.pg_download = new System.Windows.Forms.ProgressBar();
            this.lbl_dowload = new System.Windows.Forms.Label();
            this.track_silence = new System.Windows.Forms.TrackBar();
            this.lbl_silence = new System.Windows.Forms.Label();
            this.timer_startup = new System.Windows.Forms.Timer(this.components);
            this.lbl_thr = new System.Windows.Forms.Label();
            this.BG_rec = new System.ComponentModel.BackgroundWorker();
            this.btn_settings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic_title)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_mute)).BeginInit();
            this.gr_settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_silence)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_title
            // 
            this.pic_title.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pic_title, "pic_title");
            this.pic_title.Name = "pic_title";
            this.pic_title.TabStop = false;
            this.pic_title.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label2.Name = "label2";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_blackm
            // 
            this.btn_blackm.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_blackm, "btn_blackm");
            this.btn_blackm.Name = "btn_blackm";
            this.btn_blackm.UseVisualStyleBackColor = true;
            this.btn_blackm.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // button7
            // 
            this.button7.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // button8
            // 
            this.button8.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button8, "button8");
            this.button8.Name = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_joiner);
            this.groupBox3.Controls.Add(this.btn_pedal);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.btn_blackm);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // btn_joiner
            // 
            this.btn_joiner.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_joiner, "btn_joiner");
            this.btn_joiner.Name = "btn_joiner";
            this.btn_joiner.UseVisualStyleBackColor = true;
            this.btn_joiner.Click += new System.EventHandler(this.btn_joiner_Click);
            // 
            // btn_pedal
            // 
            this.btn_pedal.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_pedal, "btn_pedal");
            this.btn_pedal.Name = "btn_pedal";
            this.btn_pedal.UseVisualStyleBackColor = true;
            this.btn_pedal.Click += new System.EventHandler(this.button7_Click);
            // 
            // btn_lock
            // 
            this.btn_lock.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_lock, "btn_lock");
            this.btn_lock.Name = "btn_lock";
            this.btn_lock.UseVisualStyleBackColor = true;
            this.btn_lock.Click += new System.EventHandler(this.btn_lock_Click);
            // 
            // imgs
            // 
            this.imgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgs.ImageStream")));
            this.imgs.TransparentColor = System.Drawing.Color.Transparent;
            this.imgs.Images.SetKeyName(0, "Lock-icon.png");
            this.imgs.Images.SetKeyName(1, "unlocked_2.png");
            // 
            // btn_videos
            // 
            this.btn_videos.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_videos, "btn_videos");
            this.btn_videos.Name = "btn_videos";
            this.btn_videos.UseVisualStyleBackColor = true;
            this.btn_videos.Click += new System.EventHandler(this.btn_videos_Click);
            // 
            // btn_start_record
            // 
            this.btn_start_record.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_start_record, "btn_start_record");
            this.btn_start_record.Name = "btn_start_record";
            this.btn_start_record.UseVisualStyleBackColor = true;
            this.btn_start_record.Click += new System.EventHandler(this.btn_start_record_Click);
            // 
            // btn_preview
            // 
            this.btn_preview.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_preview, "btn_preview");
            this.btn_preview.Name = "btn_preview";
            this.btn_preview.UseVisualStyleBackColor = true;
            this.btn_preview.Click += new System.EventHandler(this.btn_preview_Click);
            // 
            // btn_clone
            // 
            this.btn_clone.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_clone, "btn_clone");
            this.btn_clone.Name = "btn_clone";
            this.btn_clone.UseVisualStyleBackColor = true;
            this.btn_clone.Click += new System.EventHandler(this.btn_clone_Click);
            // 
            // combo_scenes
            // 
            this.combo_scenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.combo_scenes, "combo_scenes");
            this.combo_scenes.FormattingEnabled = true;
            this.combo_scenes.Name = "combo_scenes";
            this.combo_scenes.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button5
            // 
            this.button5.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // btn_del
            // 
            this.btn_del.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_del, "btn_del");
            this.btn_del.Name = "btn_del";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btn_export);
            this.groupBox1.Controls.Add(this.btn_import);
            this.groupBox1.Controls.Add(this.n_delay);
            this.groupBox1.Controls.Add(this.btn_refresh);
            this.groupBox1.Controls.Add(this.combo_scenes);
            this.groupBox1.Controls.Add(this.btn_del);
            this.groupBox1.Controls.Add(this.btn_start_record);
            this.groupBox1.Controls.Add(this.btn_videos);
            this.groupBox1.Controls.Add(this.btn_clone);
            this.groupBox1.Controls.Add(this.btn_preview);
            this.groupBox1.Controls.Add(this.lbl_scenes);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // btn_export
            // 
            this.btn_export.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_export, "btn_export");
            this.btn_export.Name = "btn_export";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // btn_import
            // 
            this.btn_import.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_import, "btn_import");
            this.btn_import.Name = "btn_import";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // n_delay
            // 
            resources.ApplyResources(this.n_delay, "n_delay");
            this.n_delay.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.n_delay.Name = "n_delay";
            this.n_delay.ValueChanged += new System.EventHandler(this.n_delay_ValueChanged);
            // 
            // btn_refresh
            // 
            this.btn_refresh.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_refresh, "btn_refresh");
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // lbl_scenes
            // 
            resources.ApplyResources(this.lbl_scenes, "lbl_scenes");
            this.lbl_scenes.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_scenes.Name = "lbl_scenes";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Step = 100;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // combo_audio
            // 
            this.combo_audio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_audio.FormattingEnabled = true;
            resources.ApplyResources(this.combo_audio, "combo_audio");
            this.combo_audio.Name = "combo_audio";
            this.combo_audio.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // timer3
            // 
            this.timer3.Interval = 6000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // btn_aud_dev
            // 
            this.btn_aud_dev.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_aud_dev, "btn_aud_dev");
            this.btn_aud_dev.Name = "btn_aud_dev";
            this.btn_aud_dev.UseVisualStyleBackColor = true;
            this.btn_aud_dev.Click += new System.EventHandler(this.btn_aud_dev_Click);
            // 
            // lbl_obs_running
            // 
            this.lbl_obs_running.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.lbl_obs_running, "lbl_obs_running");
            this.lbl_obs_running.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_obs_running.Name = "lbl_obs_running";
            this.lbl_obs_running.ReadOnly = true;
            // 
            // chk_mon_audio
            // 
            resources.ApplyResources(this.chk_mon_audio, "chk_mon_audio");
            this.chk_mon_audio.Checked = true;
            this.chk_mon_audio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_mon_audio.Name = "chk_mon_audio";
            this.chk_mon_audio.UseVisualStyleBackColor = true;
            this.chk_mon_audio.CheckedChanged += new System.EventHandler(this.chk_mon_audio_CheckedChanged);
            this.chk_mon_audio.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chk_mon_audio_MouseClick);
            // 
            // combo_lang
            // 
            this.combo_lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_lang.FormattingEnabled = true;
            this.combo_lang.Items.AddRange(new object[] {
            resources.GetString("combo_lang.Items"),
            resources.GetString("combo_lang.Items1")});
            resources.ApplyResources(this.combo_lang, "combo_lang");
            this.combo_lang.Name = "combo_lang";
            this.combo_lang.SelectedIndexChanged += new System.EventHandler(this.combo_lang_SelectedIndexChanged);
            // 
            // lbl_lang
            // 
            resources.ApplyResources(this.lbl_lang, "lbl_lang");
            this.lbl_lang.Name = "lbl_lang";
            // 
            // pic_mute
            // 
            resources.ApplyResources(this.pic_mute, "pic_mute");
            this.pic_mute.Name = "pic_mute";
            this.pic_mute.TabStop = false;
            // 
            // img_audio_2
            // 
            this.img_audio_2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img_audio_2.ImageStream")));
            this.img_audio_2.TransparentColor = System.Drawing.Color.Transparent;
            this.img_audio_2.Images.SetKeyName(0, "capture_icon_21.png");
            this.img_audio_2.Images.SetKeyName(1, "mute_red_21px.png");
            // 
            // btn_video_prop
            // 
            resources.ApplyResources(this.btn_video_prop, "btn_video_prop");
            this.btn_video_prop.Name = "btn_video_prop";
            this.btn_video_prop.UseVisualStyleBackColor = true;
            this.btn_video_prop.Click += new System.EventHandler(this.button10_Click_1);
            // 
            // Camera1Combo
            // 
            this.Camera1Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Camera1Combo.FormattingEnabled = true;
            resources.ApplyResources(this.Camera1Combo, "Camera1Combo");
            this.Camera1Combo.Name = "Camera1Combo";
            this.Camera1Combo.SelectedIndexChanged += new System.EventHandler(this.Camera1Combo_SelectedIndexChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btn_preview_camera
            // 
            resources.ApplyResources(this.btn_preview_camera, "btn_preview_camera");
            this.btn_preview_camera.Name = "btn_preview_camera";
            this.btn_preview_camera.UseVisualStyleBackColor = true;
            this.btn_preview_camera.Click += new System.EventHandler(this.button10_Click);
            // 
            // timer_cam
            // 
            this.timer_cam.Tick += new System.EventHandler(this.timer_cam_Tick);
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.videoSourcePlayer1.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.videoSourcePlayer1, "videoSourcePlayer1");
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            // 
            // btn_stop_cam
            // 
            resources.ApplyResources(this.btn_stop_cam, "btn_stop_cam");
            this.btn_stop_cam.Name = "btn_stop_cam";
            this.btn_stop_cam.UseVisualStyleBackColor = true;
            this.btn_stop_cam.Click += new System.EventHandler(this.btn_stop_cam_Click);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btn_refresh_audio
            // 
            this.btn_refresh_audio.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_refresh_audio, "btn_refresh_audio");
            this.btn_refresh_audio.Name = "btn_refresh_audio";
            this.btn_refresh_audio.UseVisualStyleBackColor = true;
            this.btn_refresh_audio.Click += new System.EventHandler(this.button11_Click);
            // 
            // proc
            // 
            this.proc.StartInfo.Domain = "";
            this.proc.StartInfo.LoadUserProfile = false;
            this.proc.StartInfo.Password = null;
            this.proc.StartInfo.StandardErrorEncoding = null;
            this.proc.StartInfo.StandardOutputEncoding = null;
            this.proc.StartInfo.UserName = "";
            this.proc.SynchronizingObject = this;
            // 
            // process2
            // 
            this.process2.StartInfo.Domain = "";
            this.process2.StartInfo.LoadUserProfile = false;
            this.process2.StartInfo.Password = null;
            this.process2.StartInfo.StandardErrorEncoding = null;
            this.process2.StartInfo.StandardOutputEncoding = null;
            this.process2.StartInfo.UserName = "";
            this.process2.SynchronizingObject = this;
            // 
            // btn_refres_vid
            // 
            this.btn_refres_vid.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_refres_vid, "btn_refres_vid");
            this.btn_refres_vid.Name = "btn_refres_vid";
            this.btn_refres_vid.UseVisualStyleBackColor = true;
            this.btn_refres_vid.Click += new System.EventHandler(this.btn_refres_vid_Click);
            // 
            // FileD
            // 
            this.FileD.RestoreDirectory = true;
            this.FileD.FileOk += new System.ComponentModel.CancelEventHandler(this.FileD_FileOk);
            // 
            // btn_update
            // 
            this.btn_update.FlatAppearance.BorderSize = 0;
            this.btn_update.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            resources.ApplyResources(this.btn_update, "btn_update");
            this.btn_update.Name = "btn_update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // gr_settings
            // 
            this.gr_settings.Controls.Add(this.chk_validate);
            this.gr_settings.Controls.Add(this.chk_auto_close_obs);
            this.gr_settings.Controls.Add(this.chk_show_keep);
            this.gr_settings.Controls.Add(this.chk_updates);
            this.gr_settings.Controls.Add(this.chk_crono);
            resources.ApplyResources(this.gr_settings, "gr_settings");
            this.gr_settings.Name = "gr_settings";
            this.gr_settings.TabStop = false;
            // 
            // chk_validate
            // 
            resources.ApplyResources(this.chk_validate, "chk_validate");
            this.chk_validate.Name = "chk_validate";
            this.chk_validate.UseVisualStyleBackColor = true;
            this.chk_validate.CheckedChanged += new System.EventHandler(this.chk_validate_CheckedChanged);
            // 
            // chk_auto_close_obs
            // 
            resources.ApplyResources(this.chk_auto_close_obs, "chk_auto_close_obs");
            this.chk_auto_close_obs.Name = "chk_auto_close_obs";
            this.chk_auto_close_obs.UseVisualStyleBackColor = true;
            this.chk_auto_close_obs.CheckedChanged += new System.EventHandler(this.chk_auto_close_obs_CheckedChanged);
            // 
            // chk_show_keep
            // 
            resources.ApplyResources(this.chk_show_keep, "chk_show_keep");
            this.chk_show_keep.Name = "chk_show_keep";
            this.chk_show_keep.UseVisualStyleBackColor = true;
            this.chk_show_keep.CheckedChanged += new System.EventHandler(this.chk_show_keep_CheckedChanged);
            // 
            // chk_updates
            // 
            resources.ApplyResources(this.chk_updates, "chk_updates");
            this.chk_updates.Name = "chk_updates";
            this.chk_updates.UseVisualStyleBackColor = true;
            this.chk_updates.CheckedChanged += new System.EventHandler(this.chk_updates_CheckedChanged);
            // 
            // chk_crono
            // 
            resources.ApplyResources(this.chk_crono, "chk_crono");
            this.chk_crono.Name = "chk_crono";
            this.chk_crono.UseVisualStyleBackColor = true;
            this.chk_crono.CheckedChanged += new System.EventHandler(this.chk_crono_CheckedChanged);
            // 
            // pg_download
            // 
            resources.ApplyResources(this.pg_download, "pg_download");
            this.pg_download.Name = "pg_download";
            // 
            // lbl_dowload
            // 
            resources.ApplyResources(this.lbl_dowload, "lbl_dowload");
            this.lbl_dowload.Name = "lbl_dowload";
            // 
            // track_silence
            // 
            resources.ApplyResources(this.track_silence, "track_silence");
            this.track_silence.Minimum = -10;
            this.track_silence.Name = "track_silence";
            this.track_silence.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lbl_silence
            // 
            resources.ApplyResources(this.lbl_silence, "lbl_silence");
            this.lbl_silence.Name = "lbl_silence";
            // 
            // timer_startup
            // 
            this.timer_startup.Interval = 1000;
            this.timer_startup.Tick += new System.EventHandler(this.timer_startup_Tick);
            // 
            // lbl_thr
            // 
            resources.ApplyResources(this.lbl_thr, "lbl_thr");
            this.lbl_thr.Name = "lbl_thr";
            // 
            // BG_rec
            // 
            this.BG_rec.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BG_rec_DoWork);
            this.BG_rec.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BG_rec_RunWorkerCompleted);
            // 
            // btn_settings
            // 
            this.btn_settings.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_settings, "btn_settings");
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.UseVisualStyleBackColor = true;
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.lbl_obs_running);
            this.Controls.Add(this.lbl_thr);
            this.Controls.Add(this.lbl_silence);
            this.Controls.Add(this.track_silence);
            this.Controls.Add(this.lbl_dowload);
            this.Controls.Add(this.pg_download);
            this.Controls.Add(this.gr_settings);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_refres_vid);
            this.Controls.Add(this.btn_refresh_audio);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_stop_cam);
            this.Controls.Add(this.videoSourcePlayer1);
            this.Controls.Add(this.btn_preview_camera);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.Camera1Combo);
            this.Controls.Add(this.btn_video_prop);
            this.Controls.Add(this.pic_mute);
            this.Controls.Add(this.lbl_lang);
            this.Controls.Add(this.combo_lang);
            this.Controls.Add(this.chk_mon_audio);
            this.Controls.Add(this.btn_aud_dev);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.combo_audio);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pic_title);
            this.Controls.Add(this.btn_lock);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pic_title)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_mute)).EndInit();
            this.gr_settings.ResumeLayout(false);
            this.gr_settings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_silence)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pic_title;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_blackm;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button btn_pedal;
        private System.Windows.Forms.Button btn_lock;
        private System.Windows.Forms.ImageList imgs;
        private System.Windows.Forms.Button btn_videos;
        private System.Windows.Forms.Button btn_start_record;
        private System.Windows.Forms.Button btn_preview;
        private System.Windows.Forms.Button btn_clone;
        private System.Windows.Forms.ComboBox combo_scenes;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.FolderBrowserDialog fd1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.NumericUpDown n_delay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox combo_audio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button btn_aud_dev;
        private System.Windows.Forms.TextBox lbl_obs_running;
        private System.Windows.Forms.CheckBox chk_mon_audio;
        private System.Windows.Forms.Label lbl_lang;
        private System.Windows.Forms.ComboBox combo_lang;
        private System.Windows.Forms.PictureBox pic_mute;
        private System.Windows.Forms.ImageList img_audio_2;
        private System.Windows.Forms.Button btn_video_prop;
        private System.Windows.Forms.ComboBox Camera1Combo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_preview_camera;
        private System.Windows.Forms.Timer timer_cam;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private System.Windows.Forms.Button btn_stop_cam;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_refresh_audio;
        private System.Diagnostics.Process proc;
        private System.Diagnostics.Process process2;
        private System.Windows.Forms.Button btn_refres_vid;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.OpenFileDialog FileD;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.GroupBox gr_settings;
        private System.Windows.Forms.CheckBox chk_updates;
        private System.Windows.Forms.CheckBox chk_crono;
        private System.Windows.Forms.CheckBox chk_show_keep;
        private System.Windows.Forms.ProgressBar pg_download;
        private System.Windows.Forms.Label lbl_dowload;
        private System.Windows.Forms.CheckBox chk_auto_close_obs;
        private System.Windows.Forms.TrackBar track_silence;
        private System.Windows.Forms.Label lbl_silence;
        private System.Windows.Forms.Timer timer_startup;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chk_validate;
        private System.Windows.Forms.Label lbl_thr;
        private System.Windows.Forms.Label lbl_scenes;
        private System.ComponentModel.BackgroundWorker BG_rec;
        private System.Windows.Forms.Button btn_joiner;
        private System.Windows.Forms.Button btn_settings;
    }
}

