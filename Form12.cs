using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plato_saga
{
    public partial class Form12 : Form
    {

        //Scroll text

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

        [DllImport("user32.dll")]
        static extern int SetScrollInfo(IntPtr hwnd, int fnBar, [In] ref SCROLLINFO lpsi, bool fRedraw);

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }

        enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        enum ScrollInfoMask
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x10,
            SIF_ALL = SIF_RANGE + SIF_PAGE + SIF_POS + SIF_TRACKPOS
        }

        const int WM_VSCROLL = 277;
        const int SB_LINEUP = 0;
        const int SB_LINEDOWN = 1;
        const int SB_THUMBPOSITION = 4;
        const int SB_THUMBTRACK = 5;
        const int SB_TOP = 6;
        const int SB_BOTTOM = 7;
        const int SB_ENDSCROLL = 8;

        // End scroll text


        public Form12()
        {
            InitializeComponent();        
        }

        void scroll(IntPtr handle, int pixels)
        {
            IntPtr ptrLparam = new IntPtr(0);
            IntPtr ptrWparam;
            // Get current scroller posion

            SCROLLINFO si = new SCROLLINFO();
            si.cbSize = (uint)Marshal.SizeOf(si);
            si.fMask = (uint)ScrollInfoMask.SIF_ALL;
            GetScrollInfo(handle, (int)ScrollBarDirection.SB_VERT, ref si);

            // Increase posion by pixles
            if (si.nPos < (si.nMax - si.nPage))
                si.nPos += pixels;
            else
            {
                ptrWparam = new IntPtr(SB_ENDSCROLL);
                t_scroll.Enabled = false;
                SendMessage(handle, WM_VSCROLL, ptrWparam, ptrLparam);
            }

            // Reposition scroller
            SetScrollInfo(handle, (int)ScrollBarDirection.SB_VERT, ref si, true);

            // Send a WM_VSCROLL scroll message using SB_THUMBTRACK as wParam
            // SB_THUMBTRACK: low-order word of wParam, si.nPos high-order word of wParam
            ptrWparam = new IntPtr(SB_THUMBTRACK + 0x10000 * si.nPos);
            SendMessage(handle, WM_VSCROLL, ptrWparam, ptrLparam);
        }      

            private void Form12_Load(object sender, EventArgs e)
            {
            Color trans_col = new Color();
            trans_col = plato_saga.Properties.Settings.Default.pr_back_color;
            this.BackColor = trans_col;
            if (plato_saga.Properties.Settings.Default.pr_transp == true) this.TransparencyKey = trans_col;
            rtx1.BackColor = trans_col;
            rtx1.Font =  plato_saga.Properties.Settings.Default.pr_font;
            rtx1.ForeColor =  plato_saga.Properties.Settings.Default.pr_color;

            if (plato_saga.Properties.Settings.Default.pr_location == 0)
            {
                this.Width = Screen.PrimaryScreen.Bounds.Width - 80;
                this.Height = Screen.PrimaryScreen.Bounds.Height - 80;
                this.Top = 40;
                this.Left = 40;
                rtx1.Width = this.Width + 15;
                rtx1.Height = this.Height;
            }

            if (plato_saga.Properties.Settings.Default.pr_location == 1)
            {
                this.Width = Screen.PrimaryScreen.Bounds.Width / 2 + Screen.PrimaryScreen.Bounds.Width / 4;
                this.Height = plato_saga.Properties.Settings.Default.rtx_height;
                this.Top = 1;
                this.Left = Screen.PrimaryScreen.Bounds.Width / 2 - Screen.PrimaryScreen.Bounds.Width / 3 - 55;
                rtx1.Width = this.Width + 15;
                rtx1.Height = plato_saga.Properties.Settings.Default.rtx_height;
            }

            if (plato_saga.Properties.Settings.Default.pr_location == 2)
            {
                this.Width = Screen.PrimaryScreen.Bounds.Width / 2 + Screen.PrimaryScreen.Bounds.Width / 4;
                this.Height = plato_saga.Properties.Settings.Default.rtx_height;
                this.Top = Screen.PrimaryScreen.Bounds.Height - plato_saga.Properties.Settings.Default.rtx_height - 15;
                this.Left = Screen.PrimaryScreen.Bounds.Width / 2 - Screen.PrimaryScreen.Bounds.Width / 3 - 55;
                rtx1.Width = this.Width + 15;
                rtx1.Height = plato_saga.Properties.Settings.Default.rtx_height;
            }

            if (plato_saga.Properties.Settings.Default.pr_location == 3)
            {
                this.Width = Screen.PrimaryScreen.Bounds.Width / 3 + 15;
                this.Height = this.Height = plato_saga.Properties.Settings.Default.rtx_height + 100;
                this.Top = ((Screen.PrimaryScreen.Bounds.Height - plato_saga.Properties.Settings.Default.rtx_height) / 2) - 20;
                this.Left = 10;
                rtx1.Width = this.Width + 15;                
                rtx1.Height = plato_saga.Properties.Settings.Default.rtx_height + 100;
            }

            if (plato_saga.Properties.Settings.Default.pr_location == 4)
            {
                this.Width = Screen.PrimaryScreen.Bounds.Width / 3 + 15;
                this.Height = this.Height = plato_saga.Properties.Settings.Default.rtx_height + 100;
                this.Top = ((Screen.PrimaryScreen.Bounds.Height - plato_saga.Properties.Settings.Default.rtx_height) / 2) - 20;
                this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 10;
                rtx1.Width = this.Width + 15;
                rtx1.Height = plato_saga.Properties.Settings.Default.rtx_height + 100;
            }
            
            rtx1.SelectAll();
            rtx1.SelectionAlignment = HorizontalAlignment.Center;
            rtx1.DeselectAll();
            this.TopMost = true;
            t_scroll.Interval = plato_saga.Properties.Settings.Default.prompt_sp;
            if (plato_saga.Properties.Settings.Default.rem_lines == true)
            {
                t_scroll.Interval = plato_saga.Properties.Settings.Default.remove_timer;
            }
            t_scroll.Enabled = true;
        }

        private void t_scroll_Tick(object sender, EventArgs e)
        {
            t_scroll.Interval = plato_saga.Properties.Settings.Default.prompt_sp;
            rtx1.Font = Properties.Settings.Default.pr_font;
            rtx1.ForeColor = Properties.Settings.Default.pr_color;
            rtx1.BackColor = Properties.Settings.Default.pr_back_color;
            if (plato_saga.Properties.Settings.Default.pr_transp == true)
            {
                this.AllowTransparency = true;
                this.TransparencyKey = Properties.Settings.Default.pr_back_color;
            }
            else this.AllowTransparency = false;

            if (plato_saga.Properties.Settings.Default.rem_lines == false) scroll(rtx1.Handle, 1);
            else
            {
                bool continueProcess = true;
                int i = 1; //Zero Based So Start from 1
                int j = 0;
                List<string> lines = new List<string>();
                while (continueProcess)
                {
                    var index = rtx1.GetFirstCharIndexFromLine(i);
                    if (index != -1)
                    {
                        lines.Add(rtx1.Text.Substring(j, index - j));
                        j = index;
                        i++;
                    }
                    else
                    {
                        lines.Add(rtx1.Text.Substring(j, rtx1.Text.Length - j));
                        continueProcess = false;
                    }
                }
                int loc = rtx1.Find(lines[0]);
                try
                {
                    rtx1.Text = rtx1.Text.Remove(loc, lines[0].Length);
                    int textHeight;
                    using (Graphics g = rtx1.CreateGraphics())
                    {
                        textHeight = TextRenderer.MeasureText(g, rtx1.Text, rtx1.Font).Height;
                        rtx1.Height = rtx1.Height - textHeight;
                        this.Height = this.Height - textHeight;
                        rtx1.SelectAll();
                        rtx1.SelectionAlignment = HorizontalAlignment.Center;
                        rtx1.DeselectAll();
                    }
                }
                catch { }
            }
        }

        private void Form12_FormClosed(object sender, FormClosedEventArgs e)
        {
            t_scroll.Enabled = false;
        }

        private void close_item_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menu_Opening(object sender, CancelEventArgs e)
        {
            close_item.Text = Properties.Resources.close_w;
        }

        private void rtx1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Dispose();
        }
    }
}
