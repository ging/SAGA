using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plato_saga
{
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }
        public String prompt_text = String.Empty;
        public Boolean check_rem_lines = false;
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

        private void btn_fuente_Click(object sender, EventArgs e)
        {
            font1.Font = rtx1.Font;
            rtx1.SelectAll();
            font1.ShowDialog();
            rtx1.Font = font1.Font;
            Properties.Settings.Default.pr_font = font1.Font;
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            color1.ShowDialog();
            rtx1.ForeColor = color1.Color;
            pic_color_font.BackColor = color1.Color;
            Properties.Settings.Default.pr_color = color1.Color;
            if (color1.Color == pic_back_color.BackColor)
            {
                MessageBox.Show("El color del texto es igual al del fondo.");
            }
        }

        private void localize()
        {
            chk_tele.Text = Properties.Resources.enable_tel;
            chk_delay.Text = Properties.Resources.start_delay;
        }


        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture);
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }
        private void Form13_Load(object sender, EventArgs e)
        {            
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(plato_saga.Properties.Settings.Default.app_lang);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form13));
            RefreshResources(this, resources);
            lbl_deck_prof.Text = Properties.Resources.stream_prf_pr;
    
            //localize();
            openf.Filter = Properties.Resources.txt_filter;
            pic_color_font.BackColor = plato_saga.Properties.Settings.Default.pr_color;
            pic_back_color.BackColor = plato_saga.Properties.Settings.Default.pr_back_color;
            rtx1.Font = plato_saga.Properties.Settings.Default.pr_font;
            rtx1.ForeColor = plato_saga.Properties.Settings.Default.pr_color;
            rtx1.BackColor = plato_saga.Properties.Settings.Default.pr_back_color;
            rtx1.Text = plato_saga.Properties.Settings.Default.prompt_txt;
            t_scroll.Interval = plato_saga.Properties.Settings.Default.prompt_sp;
            num_delay.Value = plato_saga.Properties.Settings.Default.pr_delay_val;
            tr_speed.Value = t_scroll.Interval;
            if (plato_saga.Properties.Settings.Default.rem_lines == true)
            {
                plato_saga.Properties.Settings.Default.remove_timer = (int)n_rem_timer.Value * 1000;
                plato_saga.Properties.Settings.Default.Save();                
                t_scroll.Interval = (int)n_rem_timer.Value * 1000;
            }
            chk_max_obs.Checked = plato_saga.Properties.Settings.Default.max_obs;
            chk_tele.Checked = plato_saga.Properties.Settings.Default.to_prompt;
            chk_delay.Checked = plato_saga.Properties.Settings.Default.pr_delay;
            cb_loc.SelectedIndex = plato_saga.Properties.Settings.Default.pr_location;            
            if ((int)plato_saga.Properties.Settings.Default.rtx_height < n_h_rtx.Minimum) n_h_rtx.Value = 129;
            else n_h_rtx.Value = (int)plato_saga.Properties.Settings.Default.rtx_height;

            if (chk_tele.Checked)
            {
                tr_speed.Enabled = true;
                chk_delay.Enabled = true;
                cb_loc.Enabled = true;
            }
            else
            {
                tr_speed.Enabled = false;
                chk_delay.Enabled = false;
                cb_loc.Enabled = false;
            }            
        }

        private void tr_speed_Scroll(object sender, EventArgs e)
        {
            t_scroll.Interval = tr_speed.Value;
            Properties.Settings.Default.prompt_sp = tr_speed.Value;
            Properties.Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            if (rtx1.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.add_text);
                return;
            }
           
                    Form12 frm_tele = new Form12();

                    try
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            if (plato_saga.Properties.Settings.Default.rem_lines == false)
                            {
                                frm_tele.rtx1.Text = Environment.NewLine + Environment.NewLine + rtx1.Text;
                            }
                            else frm_tele.rtx1.Text = rtx1.Text;
                            frm_tele.Show(this);
                        }));
                    }
                    catch
                    {
                        if (plato_saga.Properties.Settings.Default.rem_lines == false)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                frm_tele.rtx1.Text = Environment.NewLine + Environment.NewLine + rtx1.Text;
                            }));
                        }
                        else
                            this.Invoke(new MethodInvoker(delegate
                            {
                                frm_tele.rtx1.Text = rtx1.Text;
                            }));
                            frm_tele.Show(this);
                    }                    
              
         }    

        private void t_scroll_Tick(object sender, EventArgs e)
        {
            scroll(rtx1.Handle, 1);
        }

        private void Form13_FormClosed(object sender, FormClosedEventArgs e)
        {
            t_scroll.Enabled = false;
            prompt_text = rtx1.Text;
        }

        private void chk_tele_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tele.Checked == true)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                tr_speed.Enabled = true;
                chk_delay.Enabled = true;
                num_delay.Enabled = true;
                cb_loc.Enabled = true;
                lbl_deck_prof.Visible = true;
                btn_deck.Visible = true;
            }
            else
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                tr_speed.Enabled = false;
                chk_delay.Enabled = false;
                num_delay.Enabled = false;
                cb_loc.Enabled = false;
                lbl_deck_prof.Visible = false;
                btn_deck.Visible = false;
            }
        }

        private void btn_Default_Click(object sender, EventArgs e)
        {
            Font f = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
            rtx1.Font = f;
            rtx1.ForeColor = Color.White;
            rtx1.BackColor = Color.Black;
            pic_back_color.BackColor = Color.Black;
            pic_color_font.BackColor = Color.White;
            tr_speed.Value = 120;
            chk_rem_lines.Checked = false;
            Properties.Settings.Default.pr_font = rtx1.Font;
            Properties.Settings.Default.pr_color = rtx1.ForeColor;
            Properties.Settings.Default.pr_back_color = rtx1.BackColor;
            Properties.Settings.Default.prompt_sp = tr_speed.Value;
            Properties.Settings.Default.rem_lines = false;
            Properties.Settings.Default.Save();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            openf.ShowDialog();
        }

        private void openf_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                rtx1.Text = File.ReadAllText(openf.FileName);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error al cargar el fichero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menu_paste_Click(object sender, EventArgs e)
        {
            rtx1.Text = Clipboard.GetText();
        }

        private void menu_copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtx1.Text);
        }

        private void pic_color_font_Click(object sender, EventArgs e)
        {
            color1.ShowDialog();
            rtx1.ForeColor = color1.Color;
            pic_color_font.BackColor = color1.Color;
            Properties.Settings.Default.pr_color = color1.Color;
            if (color1.Color == pic_back_color.BackColor)
            {
                MessageBox.Show("El color del texto es igual al del fondo.");
            }
        }

        private void btn_fondo_color_Click(object sender, EventArgs e)
        {
            color2.ShowDialog();
            rtx1.BackColor = color2.Color;
            pic_back_color.BackColor = color2.Color;
            Properties.Settings.Default.pr_back_color = color2.Color;
            if (color2.Color == pic_color_font.BackColor)
            {
                MessageBox.Show("El color del fondo es igual al del texto.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            color2.ShowDialog();
            rtx1.BackColor = color2.Color;
            pic_back_color.BackColor = color2.Color;
            Properties.Settings.Default.pr_back_color = color2.Color;
            if (color2.Color == pic_color_font.BackColor)
            {
                MessageBox.Show("El color del fondo es igual al del texto.");
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            rtx1.Text = String.Empty;
        }

        private void chk_delay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_delay.Checked) num_delay.Enabled = true;
            else num_delay.Enabled = false;
        }

        private void cb_loc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_loc.SelectedIndex != 0) n_h_rtx.Enabled = true;
            else n_h_rtx.Enabled = false;

            if (cb_loc.SelectedIndex == 0 || cb_loc.SelectedIndex == 1 || cb_loc.SelectedIndex == 2)
            {
                rtx1.Width = 760;
                rtx1.Left = 17;
            }
            if (cb_loc.SelectedIndex == 3 || cb_loc.SelectedIndex == 4)
            {
                rtx1.Width = 416;
                rtx1.Left = this.Width / 4 - 30;
            }
            plato_saga.Properties.Settings.Default.pr_location = cb_loc.SelectedIndex;
            plato_saga.Properties.Settings.Default.Save();            
        }

        private void n_h_rtx_ValueChanged(object sender, EventArgs e)
        {
            plato_saga.Properties.Settings.Default.rtx_height = (int)n_h_rtx.Value;
            plato_saga.Properties.Settings.Default.Save();
            //if (n_h_rtx.Value < 119) n_h_rtx.Value = 119;
            //if (n_h_rtx.Value > 1999) n_h_rtx.Value = 2000;
        }

        private void rtx1_TextChanged(object sender, EventArgs e)
        {
            if (chk_rem_lines.Checked)
            {
                String rem_lines = Regex.Replace(rtx1.Text, @"\t|\n|\r", "");
                rtx1.Text = rem_lines;
            }
        }

        private void chk_rem_lines_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_rem_lines.Checked)
            {
                n_rem_timer.Enabled = true;
                plato_saga.Properties.Settings.Default.rem_lines = true;
            }
            else
            {
                n_rem_timer.Enabled = false;
                plato_saga.Properties.Settings.Default.rem_lines = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void n_rem_timer_ValueChanged(object sender, EventArgs e)
        {
            plato_saga.Properties.Settings.Default.remove_timer =(int)n_rem_timer.Value * 1000;
            plato_saga.Properties.Settings.Default.Save();
            t_scroll.Interval = (int)n_rem_timer.Value * 1000;
        }

        private void chk_trans_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_trans.Checked == true)
            {
                plato_saga.Properties.Settings.Default.pr_transp = true;
            }
            else plato_saga.Properties.Settings.Default.pr_transp = false;
            plato_saga.Properties.Settings.Default.Save();
        }

        private void chk_max_obs_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_max_obs.CheckState == CheckState.Checked)
            {
                plato_saga.Properties.Settings.Default.max_obs = true;
            }
            else
            {
                plato_saga.Properties.Settings.Default.max_obs = false;
            }
            plato_saga.Properties.Settings.Default.Save();
        }

        private void Form13_Shown(object sender, EventArgs e)
        {
            if (chk_tele.Checked == true)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                tr_speed.Enabled = true;
                chk_delay.Enabled = true;
                num_delay.Enabled = true;
                cb_loc.Enabled = true;
                lbl_deck_prof.Visible = true;
                btn_deck.Visible = true;
            }
            else
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                tr_speed.Enabled = false;
                chk_delay.Enabled = false;
                num_delay.Enabled = false;
                cb_loc.Enabled = false;
                lbl_deck_prof.Visible = false;
                btn_deck.Visible = false;
            }
        }

        private void btn_deck_Click(object sender, EventArgs e)
        {
            String elgato = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\"  + "Elgato" + "\\" + "StreamDeck" + "\\" + "StreamDeck.exe";            
            String tele_profile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Plato_Teleprompter.streamDeckProfile");
            
            if (File.Exists(elgato))
            {
                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                proc.StartInfo.FileName = elgato;
                if (File.Exists(tele_profile))
                {
                    if (plato_saga.Properties.Settings.Default.tele_prof == false)
                    {
                        proc.StartInfo.Arguments = tele_profile;
                        proc.Start();
                        plato_saga.Properties.Settings.Default.tele_prof = true;
                        plato_saga.Properties.Settings.Default.Save();
                        MessageBox.Show(Properties.Resources.tele_prof_ins);
                        proc.StartInfo.Arguments = String.Empty;
                    }
                }
                else
                {
                    plato_saga.Properties.Settings.Default.tele_prof = false;
                    plato_saga.Properties.Settings.Default.Save();
                }
                proc.Start();
            }
            else
            {
                Form14 frm14 = new Form14();
                frm14.ShowDialog();
            }
        }
    }
}
