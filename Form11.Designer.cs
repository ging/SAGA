
namespace plato_saga
{
    partial class Form11
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form11));
            this.gr_settings = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chk_validate = new System.Windows.Forms.CheckBox();
            this.chk_auto_close_obs = new System.Windows.Forms.CheckBox();
            this.lbl_lang = new System.Windows.Forms.Label();
            this.combo_lang = new System.Windows.Forms.ComboBox();
            this.chk_show_keep = new System.Windows.Forms.CheckBox();
            this.chk_updates = new System.Windows.Forms.CheckBox();
            this.chk_crono = new System.Windows.Forms.CheckBox();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_blackm = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_restore = new System.Windows.Forms.Button();
            this.timer_cam = new System.Windows.Forms.Timer(this.components);
            this.btn_settings = new System.Windows.Forms.Button();
            this.btn_joiner = new System.Windows.Forms.Button();
            this.gr_settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr_settings
            // 
            resources.ApplyResources(this.gr_settings, "gr_settings");
            this.gr_settings.Controls.Add(this.button1);
            this.gr_settings.Controls.Add(this.chk_validate);
            this.gr_settings.Controls.Add(this.chk_auto_close_obs);
            this.gr_settings.Controls.Add(this.lbl_lang);
            this.gr_settings.Controls.Add(this.combo_lang);
            this.gr_settings.Controls.Add(this.chk_show_keep);
            this.gr_settings.Controls.Add(this.chk_updates);
            this.gr_settings.Controls.Add(this.chk_crono);
            this.gr_settings.Name = "gr_settings";
            this.gr_settings.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.chk_auto_close_obs.Checked = true;
            this.chk_auto_close_obs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_auto_close_obs.Name = "chk_auto_close_obs";
            this.chk_auto_close_obs.UseVisualStyleBackColor = true;
            this.chk_auto_close_obs.CheckedChanged += new System.EventHandler(this.chk_auto_close_obs_CheckedChanged);
            // 
            // lbl_lang
            // 
            resources.ApplyResources(this.lbl_lang, "lbl_lang");
            this.lbl_lang.Name = "lbl_lang";
            // 
            // combo_lang
            // 
            resources.ApplyResources(this.combo_lang, "combo_lang");
            this.combo_lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_lang.FormattingEnabled = true;
            this.combo_lang.Items.AddRange(new object[] {
            resources.GetString("combo_lang.Items"),
            resources.GetString("combo_lang.Items1")});
            this.combo_lang.Name = "combo_lang";
            this.combo_lang.SelectedIndexChanged += new System.EventHandler(this.combo_lang_SelectedIndexChanged);
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
            this.chk_updates.Checked = true;
            this.chk_updates.CheckState = System.Windows.Forms.CheckState.Checked;
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
            // btn_update
            // 
            resources.ApplyResources(this.btn_update, "btn_update");
            this.btn_update.FlatAppearance.BorderSize = 0;
            this.btn_update.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_update.Name = "btn_update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_blackm
            // 
            resources.ApplyResources(this.btn_blackm, "btn_blackm");
            this.btn_blackm.FlatAppearance.BorderSize = 0;
            this.btn_blackm.Name = "btn_blackm";
            this.btn_blackm.UseVisualStyleBackColor = true;
            this.btn_blackm.Click += new System.EventHandler(this.btn_blackm_Click);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_restore
            // 
            resources.ApplyResources(this.btn_restore, "btn_restore");
            this.btn_restore.FlatAppearance.BorderSize = 0;
            this.btn_restore.Name = "btn_restore";
            this.btn_restore.UseVisualStyleBackColor = true;
            this.btn_restore.Click += new System.EventHandler(this.btn_restore_Click);
            // 
            // btn_settings
            // 
            resources.ApplyResources(this.btn_settings, "btn_settings");
            this.btn_settings.FlatAppearance.BorderSize = 0;
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.UseVisualStyleBackColor = true;
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
            // 
            // btn_joiner
            // 
            resources.ApplyResources(this.btn_joiner, "btn_joiner");
            this.btn_joiner.FlatAppearance.BorderSize = 0;
            this.btn_joiner.Name = "btn_joiner";
            this.btn_joiner.UseVisualStyleBackColor = true;
            this.btn_joiner.Click += new System.EventHandler(this.btn_joiner_Click_1);
            // 
            // Form11
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_joiner);
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btn_restore);
            this.Controls.Add(this.gr_settings);
            this.Controls.Add(this.btn_blackm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form11";
            this.Load += new System.EventHandler(this.Form11_Load);
            this.gr_settings.ResumeLayout(false);
            this.gr_settings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gr_settings;
        public System.Windows.Forms.CheckBox chk_validate;
        public System.Windows.Forms.CheckBox chk_auto_close_obs;
        public System.Windows.Forms.CheckBox chk_show_keep;
        public System.Windows.Forms.CheckBox chk_updates;
        public System.Windows.Forms.CheckBox chk_crono;
        private System.Windows.Forms.Button btn_blackm;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_restore;
        private System.Windows.Forms.Timer timer_cam;
        public System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_settings;
        private System.Windows.Forms.Label lbl_lang;
        public System.Windows.Forms.ComboBox combo_lang;
        private System.Windows.Forms.Button btn_joiner;
        private System.Windows.Forms.Button button1;
    }
}