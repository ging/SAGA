namespace plato_saga
{
    partial class Form7
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form7));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pic_prev = new AForge.Controls.PictureBox();
            this.pic_rec = new AForge.Controls.PictureBox();
            this.lbl_scene = new System.Windows.Forms.Label();
            this.pic_prog = new System.Windows.Forms.PictureBox();
            this.lbl_col = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_prev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_rec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_prog)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pic_prev);
            this.groupBox1.Controls.Add(this.pic_rec);
            this.groupBox1.Controls.Add(this.lbl_scene);
            this.groupBox1.Controls.Add(this.pic_prog);
            this.groupBox1.Controls.Add(this.lbl_col);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(2, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(630, 366);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // pic_prev
            // 
            this.pic_prev.Image = ((System.Drawing.Image)(resources.GetObject("pic_prev.Image")));
            this.pic_prev.Location = new System.Drawing.Point(285, 231);
            this.pic_prev.Name = "pic_prev";
            this.pic_prev.Size = new System.Drawing.Size(60, 60);
            this.pic_prev.TabIndex = 5;
            this.pic_prev.TabStop = false;
            this.pic_prev.Visible = false;
            // 
            // pic_rec
            // 
            this.pic_rec.Image = ((System.Drawing.Image)(resources.GetObject("pic_rec.Image")));
            this.pic_rec.Location = new System.Drawing.Point(285, 236);
            this.pic_rec.Name = "pic_rec";
            this.pic_rec.Size = new System.Drawing.Size(60, 57);
            this.pic_rec.TabIndex = 4;
            this.pic_rec.TabStop = false;
            this.pic_rec.Visible = false;
            // 
            // lbl_scene
            // 
            this.lbl_scene.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_scene.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_scene.Location = new System.Drawing.Point(7, 175);
            this.lbl_scene.Name = "lbl_scene";
            this.lbl_scene.Size = new System.Drawing.Size(617, 36);
            this.lbl_scene.TabIndex = 3;
            this.lbl_scene.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pic_prog
            // 
            this.pic_prog.Image = ((System.Drawing.Image)(resources.GetObject("pic_prog.Image")));
            this.pic_prog.Location = new System.Drawing.Point(298, 315);
            this.pic_prog.Name = "pic_prog";
            this.pic_prog.Size = new System.Drawing.Size(36, 36);
            this.pic_prog.TabIndex = 2;
            this.pic_prog.TabStop = false;
            // 
            // lbl_col
            // 
            this.lbl_col.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_col.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_col.Location = new System.Drawing.Point(7, 115);
            this.lbl_col.Name = "lbl_col";
            this.lbl_col.Size = new System.Drawing.Size(617, 55);
            this.lbl_col.TabIndex = 1;
            this.lbl_col.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 33.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.SteelBlue;
            this.textBox1.Location = new System.Drawing.Point(6, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(617, 58);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 367);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form7";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form7";
            this.Load += new System.EventHandler(this.Form7_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form7_Paint);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_prev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_rec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_prog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label textBox1;
        public System.Windows.Forms.Label lbl_col;
        private System.Windows.Forms.PictureBox pic_prog;
        public System.Windows.Forms.Label lbl_scene;
        public AForge.Controls.PictureBox pic_rec;
        public AForge.Controls.PictureBox pic_prev;
    }
}