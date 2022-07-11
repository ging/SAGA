
namespace plato_saga
{
    partial class Form13
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form13));
            this.font1 = new System.Windows.Forms.FontDialog();
            this.color1 = new System.Windows.Forms.ColorDialog();
            this.rtx1 = new System.Windows.Forms.RichTextBox();
            this.rtx_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.chk_tele = new System.Windows.Forms.CheckBox();
            this.tr_speed = new System.Windows.Forms.TrackBar();
            this.btn_color = new System.Windows.Forms.Button();
            this.btn_fuente = new System.Windows.Forms.Button();
            this.btn_preview = new System.Windows.Forms.Button();
            this.chk_trans = new System.Windows.Forms.CheckBox();
            this.btn_clear_text = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.pic_back_color = new AForge.Controls.PictureBox();
            this.btn_Default = new System.Windows.Forms.Button();
            this.btn_fondo_color = new System.Windows.Forms.Button();
            this.pic_color_font = new AForge.Controls.PictureBox();
            this.btn_load = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openf = new System.Windows.Forms.OpenFileDialog();
            this.color2 = new System.Windows.Forms.ColorDialog();
            this.chk_delay = new System.Windows.Forms.CheckBox();
            this.num_delay = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_loc = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.n_h_rtx = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chk_rem_lines = new System.Windows.Forms.CheckBox();
            this.t_scroll = new System.Windows.Forms.Timer(this.components);
            this.n_rem_timer = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_sp_sc = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chk_max_obs = new System.Windows.Forms.CheckBox();
            this.lbl_deck_prof = new System.Windows.Forms.Label();
            this.btn_deck = new System.Windows.Forms.Button();
            this.rtx_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tr_speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_back_color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_color_font)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_h_rtx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_rem_timer)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtx1
            // 
            this.rtx1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rtx1.ContextMenuStrip = this.rtx_menu;
            resources.ApplyResources(this.rtx1, "rtx1");
            this.rtx1.Name = "rtx1";
            this.rtx1.TextChanged += new System.EventHandler(this.rtx1_TextChanged);
            // 
            // rtx_menu
            // 
            this.rtx_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_copy,
            this.menu_paste});
            this.rtx_menu.Name = "rtx_menu";
            resources.ApplyResources(this.rtx_menu, "rtx_menu");
            // 
            // menu_copy
            // 
            this.menu_copy.Name = "menu_copy";
            resources.ApplyResources(this.menu_copy, "menu_copy");
            this.menu_copy.Click += new System.EventHandler(this.menu_copy_Click);
            // 
            // menu_paste
            // 
            this.menu_paste.Name = "menu_paste";
            resources.ApplyResources(this.menu_paste, "menu_paste");
            this.menu_paste.Click += new System.EventHandler(this.menu_paste_Click);
            // 
            // chk_tele
            // 
            resources.ApplyResources(this.chk_tele, "chk_tele");
            this.chk_tele.Name = "chk_tele";
            this.chk_tele.UseVisualStyleBackColor = true;
            this.chk_tele.CheckedChanged += new System.EventHandler(this.chk_tele_CheckedChanged);
            // 
            // tr_speed
            // 
            resources.ApplyResources(this.tr_speed, "tr_speed");
            this.tr_speed.Maximum = 220;
            this.tr_speed.Minimum = 20;
            this.tr_speed.Name = "tr_speed";
            this.tr_speed.TickFrequency = 10;
            this.tr_speed.Value = 120;
            this.tr_speed.Scroll += new System.EventHandler(this.tr_speed_Scroll);
            // 
            // btn_color
            // 
            this.btn_color.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_color, "btn_color");
            this.btn_color.Name = "btn_color";
            this.btn_color.UseVisualStyleBackColor = true;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            // 
            // btn_fuente
            // 
            this.btn_fuente.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_fuente, "btn_fuente");
            this.btn_fuente.Name = "btn_fuente";
            this.btn_fuente.UseVisualStyleBackColor = true;
            this.btn_fuente.Click += new System.EventHandler(this.btn_fuente_Click);
            // 
            // btn_preview
            // 
            this.btn_preview.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_preview, "btn_preview");
            this.btn_preview.Name = "btn_preview";
            this.btn_preview.UseVisualStyleBackColor = true;
            this.btn_preview.Click += new System.EventHandler(this.button1_Click);
            // 
            // chk_trans
            // 
            resources.ApplyResources(this.chk_trans, "chk_trans");
            this.chk_trans.Name = "chk_trans";
            this.chk_trans.UseVisualStyleBackColor = true;
            this.chk_trans.CheckedChanged += new System.EventHandler(this.chk_trans_CheckedChanged);
            // 
            // btn_clear_text
            // 
            this.btn_clear_text.FlatAppearance.BorderSize = 0;
            this.btn_clear_text.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            resources.ApplyResources(this.btn_clear_text, "btn_clear_text");
            this.btn_clear_text.Name = "btn_clear_text";
            this.btn_clear_text.UseVisualStyleBackColor = true;
            this.btn_clear_text.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_exit
            // 
            this.btn_exit.FlatAppearance.BorderSize = 0;
            this.btn_exit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            resources.ApplyResources(this.btn_exit, "btn_exit");
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // pic_back_color
            // 
            this.pic_back_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pic_back_color, "pic_back_color");
            this.pic_back_color.Name = "pic_back_color";
            this.pic_back_color.TabStop = false;
            this.pic_back_color.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btn_Default
            // 
            this.btn_Default.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_Default, "btn_Default");
            this.btn_Default.Name = "btn_Default";
            this.btn_Default.UseVisualStyleBackColor = true;
            this.btn_Default.Click += new System.EventHandler(this.btn_Default_Click);
            // 
            // btn_fondo_color
            // 
            this.btn_fondo_color.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_fondo_color, "btn_fondo_color");
            this.btn_fondo_color.Name = "btn_fondo_color";
            this.btn_fondo_color.UseVisualStyleBackColor = true;
            this.btn_fondo_color.Click += new System.EventHandler(this.btn_fondo_color_Click);
            // 
            // pic_color_font
            // 
            this.pic_color_font.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pic_color_font, "pic_color_font");
            this.pic_color_font.Name = "pic_color_font";
            this.pic_color_font.TabStop = false;
            this.pic_color_font.Click += new System.EventHandler(this.pic_color_font_Click);
            // 
            // btn_load
            // 
            this.btn_load.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_load, "btn_load");
            this.btn_load.Name = "btn_load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // openf
            // 
            resources.ApplyResources(this.openf, "openf");
            this.openf.FileOk += new System.ComponentModel.CancelEventHandler(this.openf_FileOk);
            // 
            // chk_delay
            // 
            resources.ApplyResources(this.chk_delay, "chk_delay");
            this.chk_delay.Checked = true;
            this.chk_delay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_delay.Name = "chk_delay";
            this.chk_delay.UseVisualStyleBackColor = true;
            this.chk_delay.CheckedChanged += new System.EventHandler(this.chk_delay_CheckedChanged);
            // 
            // num_delay
            // 
            resources.ApplyResources(this.num_delay, "num_delay");
            this.num_delay.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.num_delay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_delay.Name = "num_delay";
            this.num_delay.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cb_loc
            // 
            this.cb_loc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_loc.FormattingEnabled = true;
            this.cb_loc.Items.AddRange(new object[] {
            resources.GetString("cb_loc.Items"),
            resources.GetString("cb_loc.Items1"),
            resources.GetString("cb_loc.Items2"),
            resources.GetString("cb_loc.Items3"),
            resources.GetString("cb_loc.Items4")});
            resources.ApplyResources(this.cb_loc, "cb_loc");
            this.cb_loc.Name = "cb_loc";
            this.cb_loc.SelectedIndexChanged += new System.EventHandler(this.cb_loc_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // n_h_rtx
            // 
            resources.ApplyResources(this.n_h_rtx, "n_h_rtx");
            this.n_h_rtx.Maximum = new decimal(new int[] {
            2160,
            0,
            0,
            0});
            this.n_h_rtx.Minimum = new decimal(new int[] {
            129,
            0,
            0,
            0});
            this.n_h_rtx.Name = "n_h_rtx";
            this.n_h_rtx.Value = new decimal(new int[] {
            129,
            0,
            0,
            0});
            this.n_h_rtx.ValueChanged += new System.EventHandler(this.n_h_rtx_ValueChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // chk_rem_lines
            // 
            resources.ApplyResources(this.chk_rem_lines, "chk_rem_lines");
            this.chk_rem_lines.Checked = true;
            this.chk_rem_lines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_rem_lines.Name = "chk_rem_lines";
            this.chk_rem_lines.UseVisualStyleBackColor = true;
            this.chk_rem_lines.CheckedChanged += new System.EventHandler(this.chk_rem_lines_CheckedChanged);
            // 
            // t_scroll
            // 
            this.t_scroll.Interval = 120;
            this.t_scroll.Tick += new System.EventHandler(this.t_scroll_Tick);
            // 
            // n_rem_timer
            // 
            resources.ApplyResources(this.n_rem_timer, "n_rem_timer");
            this.n_rem_timer.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.n_rem_timer.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.n_rem_timer.Name = "n_rem_timer";
            this.n_rem_timer.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.n_rem_timer.ValueChanged += new System.EventHandler(this.n_rem_timer_ValueChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_sp_sc);
            this.groupBox1.Controls.Add(this.tr_speed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chk_delay);
            this.groupBox1.Controls.Add(this.num_delay);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lbl_sp_sc
            // 
            resources.ApplyResources(this.lbl_sp_sc, "lbl_sp_sc");
            this.lbl_sp_sc.Name = "lbl_sp_sc";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtx1);
            this.groupBox2.Controls.Add(this.chk_trans);
            this.groupBox2.Controls.Add(this.btn_fuente);
            this.groupBox2.Controls.Add(this.btn_clear_text);
            this.groupBox2.Controls.Add(this.btn_load);
            this.groupBox2.Controls.Add(this.btn_color);
            this.groupBox2.Controls.Add(this.pic_color_font);
            this.groupBox2.Controls.Add(this.btn_exit);
            this.groupBox2.Controls.Add(this.btn_preview);
            this.groupBox2.Controls.Add(this.pic_back_color);
            this.groupBox2.Controls.Add(this.btn_fondo_color);
            this.groupBox2.Controls.Add(this.btn_Default);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chk_max_obs);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.n_h_rtx);
            this.groupBox3.Controls.Add(this.cb_loc);
            this.groupBox3.Controls.Add(this.n_rem_timer);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.chk_rem_lines);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // chk_max_obs
            // 
            resources.ApplyResources(this.chk_max_obs, "chk_max_obs");
            this.chk_max_obs.Checked = true;
            this.chk_max_obs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_max_obs.Name = "chk_max_obs";
            this.chk_max_obs.UseVisualStyleBackColor = true;
            this.chk_max_obs.CheckedChanged += new System.EventHandler(this.chk_max_obs_CheckedChanged);
            // 
            // lbl_deck_prof
            // 
            resources.ApplyResources(this.lbl_deck_prof, "lbl_deck_prof");
            this.lbl_deck_prof.Name = "lbl_deck_prof";
            // 
            // btn_deck
            // 
            resources.ApplyResources(this.btn_deck, "btn_deck");
            this.btn_deck.Name = "btn_deck";
            this.btn_deck.UseVisualStyleBackColor = true;
            this.btn_deck.Click += new System.EventHandler(this.btn_deck_Click);
            // 
            // Form13
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_deck);
            this.Controls.Add(this.lbl_deck_prof);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chk_tele);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form13";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form13_FormClosed);
            this.Load += new System.EventHandler(this.Form13_Load);
            this.Shown += new System.EventHandler(this.Form13_Shown);
            this.rtx_menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tr_speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_back_color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_color_font)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_h_rtx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_rem_timer)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FontDialog font1;
        private System.Windows.Forms.ColorDialog color1;
        private System.Windows.Forms.Button btn_color;
        private System.Windows.Forms.Button btn_fuente;
        private System.Windows.Forms.Button btn_preview;
        public System.Windows.Forms.RichTextBox rtx1;
        public System.Windows.Forms.CheckBox chk_tele;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TrackBar tr_speed;
        private System.Windows.Forms.Button btn_Default;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.OpenFileDialog openf;
        private System.Windows.Forms.ContextMenuStrip rtx_menu;
        private System.Windows.Forms.ToolStripMenuItem menu_copy;
        private System.Windows.Forms.ToolStripMenuItem menu_paste;
        private AForge.Controls.PictureBox pic_color_font;
        private AForge.Controls.PictureBox pic_back_color;
        private System.Windows.Forms.Button btn_fondo_color;
        private System.Windows.Forms.ColorDialog color2;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_clear_text;
        public System.Windows.Forms.CheckBox chk_delay;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown num_delay;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cb_loc;
        public System.Windows.Forms.CheckBox chk_trans;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown n_h_rtx;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.CheckBox chk_rem_lines;
        private System.Windows.Forms.Timer t_scroll;
        public System.Windows.Forms.NumericUpDown n_rem_timer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chk_max_obs;
        private System.Windows.Forms.Label lbl_sp_sc;
        private System.Windows.Forms.Label lbl_deck_prof;
        private System.Windows.Forms.Button btn_deck;
    }
}