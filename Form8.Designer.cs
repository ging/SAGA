namespace plato_saga
{
    partial class Form8
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
            this.lbl_elapsed = new System.Windows.Forms.Label();
            this.timer_recorded = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lbl_elapsed
            // 
            this.lbl_elapsed.AutoSize = true;
            this.lbl_elapsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_elapsed.ForeColor = System.Drawing.Color.White;
            this.lbl_elapsed.Location = new System.Drawing.Point(8, 9);
            this.lbl_elapsed.Name = "lbl_elapsed";
            this.lbl_elapsed.Size = new System.Drawing.Size(104, 25);
            this.lbl_elapsed.TabIndex = 0;
            this.lbl_elapsed.Text = "00:00:00";
            // 
            // timer_recorded
            // 
            this.timer_recorded.Interval = 1000;
            this.timer_recorded.Tick += new System.EventHandler(this.timer_recorded_Tick);
            // 
            // Form8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(147, 43);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_elapsed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form8";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Crono";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form8_FormClosing);
            this.Load += new System.EventHandler(this.Form8_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_elapsed;
        public System.Windows.Forms.Timer timer_recorded;
    }
}