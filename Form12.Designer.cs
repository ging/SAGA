
namespace plato_saga
{
    partial class Form12
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
            this.rtx1 = new System.Windows.Forms.RichTextBox();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.close_item = new System.Windows.Forms.ToolStripMenuItem();
            this.t_scroll = new System.Windows.Forms.Timer(this.components);
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtx1
            // 
            this.rtx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtx1.ContextMenuStrip = this.menu;
            this.rtx1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtx1.Location = new System.Drawing.Point(0, 1);
            this.rtx1.Name = "rtx1";
            this.rtx1.ReadOnly = true;
            this.rtx1.Size = new System.Drawing.Size(802, 119);
            this.rtx1.TabIndex = 0;
            this.rtx1.Text = "";
            this.rtx1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtx1_KeyDown);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.close_item});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(149, 26);
            this.menu.Opening += new System.ComponentModel.CancelEventHandler(this.menu_Opening);
            // 
            // close_item
            // 
            this.close_item.Name = "close_item";
            this.close_item.Size = new System.Drawing.Size(148, 22);
            this.close_item.Text = "Close window";
            this.close_item.Click += new System.EventHandler(this.close_item_Click);
            // 
            // t_scroll
            // 
            this.t_scroll.Tick += new System.EventHandler(this.t_scroll_Tick);
            // 
            // Form12
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 120);
            this.ContextMenuStrip = this.menu;
            this.Controls.Add(this.rtx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form12";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Teleprompter";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form12_FormClosed);
            this.Load += new System.EventHandler(this.Form12_Load);
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.RichTextBox rtx1;
        public System.Windows.Forms.Timer t_scroll;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem close_item;
    }
}