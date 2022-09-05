using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitianPositionApp
{
    public partial class Frm_Main : Form
    {
        #region Variabili

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();


        private DateTime Adesso = DateTime.Now;

        ProcessStartInfo PSI;
        Process P;

        #endregion

        #region Handles

        public Frm_Main()
        {
            InitializeComponent();

            ThisMe();

            Btn_Lbl_Versione.Text = "Version: " + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major;
        }

        private void Frm_Main_Shown(object sender, EventArgs e)
        {
            try
            {
                ReStarter();

                PSI = new ProcessStartInfo(Funzioni.AppPath)
                {
                    CreateNoWindow = true,
                    RedirectStandardInput = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false
                };

                P = Process.Start(PSI);

                //p.WaitForInputIdle();

                //Thread.Sleep(500);

                //if (!p.WaitForInputIdle(10000)) // 10 s timout
                //    throw new ApplicationException("Il processo impiega troppo ad avviarsi");

                int MaxCount = 10000;
                int Count = 0;
                
                while (Funzioni.HWnd == IntPtr.Zero || Count > MaxCount)
                {
                    P.WaitForInputIdle();
                    P.Refresh();
                    Funzioni.HWnd = P.MainWindowHandle;
                    Count++;
                }

                if (Funzioni.HWnd == IntPtr.Zero) throw new ApplicationException("The process is taking long to start");

                Funzioni.SetParent(Funzioni.HWnd, Pnl_Centrale.Handle);

                Funzioni.MoveWindow(Funzioni.HWnd, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, true);

                //PSI.WindowStyle = ProcessWindowStyle.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Funzioni.FormAltezza = this.Height;
                Funzioni.FormLarghezza = this.Width;
                Funzioni.FormPosX = this.Location.X;
                Funzioni.FormPosY = this.Location.Y;

                Funzioni.LeggiImpostazioni(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_About_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/RallyTuning/InitianPositionApp");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Impostazioni_Click(object sender, EventArgs e)
        {
            try
            {
                Form Frm_Impo = new Frm_Impostazioni();
                if (Frm_Impo.ShowDialog() == DialogResult.OK)
                {
                    ThisMe();

                    if (Funzioni.RestartTimerMin > 0)
                    {
                        ReStarter();
                    }    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Esci_Click(object sender, EventArgs e)
        {
            try
            {
                if (P != null) Funzioni.Killalo(P.ProcessName);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Riavvia_Click(object sender, EventArgs e)
        {
            try
            {
                if (P != null) Funzioni.Killalo(P.ProcessName);
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Txt_Restart_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar); //&& !char.IsControl(e.KeyChar);
        }

        private void Txt_Restart_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Txt_Restart.Text)) return;
                if (Int32.Parse(Txt_Restart.Text) <= 0) return;

                Funzioni.RestartTimerMin = Int32.Parse(Txt_Restart.Text);
                Adesso = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void ToolBarSotto_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, new IntPtr(HT_CAPTION), IntPtr.Zero);
            }
        }

        #endregion

        #region void

        private void ThisMe()
        {
            this.Location = new Point(
                Screen.AllScreens[Funzioni.Monitor].WorkingArea.X + Funzioni.FormPosX,
                Screen.AllScreens[Funzioni.Monitor].WorkingArea.Y + Funzioni.FormPosY
                );

            //Size S = Screen.AllScreens[Funzioni.Monitor].WorkingArea.Size;
            this.Size = new Size(Funzioni.FormLarghezza, Funzioni.FormAltezza);

            Txt_Restart.Text = Funzioni.RestartTimerMin.ToString();

            this.Focus();

            ToolBarSotto.Visible = Funzioni.MostraToolBar;

            if (Funzioni.MostraToolBar)
                Funzioni.AltezzaToolBar = ToolBarSotto.Height;
            else
                Funzioni.AltezzaToolBar = 0;
        }

        private async void ReStarter()
        {
            if (Funzioni.RestartTimerMin == 0) return;

            Adesso = DateTime.Now;

            await Task.Run(() =>
            {
                DateTime dopo = Adesso.AddMinutes(Funzioni.RestartTimerMin);
                TimeSpan t = Adesso.AddMinutes(Funzioni.RestartTimerMin) - DateTime.Now;

                while (t.Ticks >= 0)
                {
                    t = Adesso.AddMinutes(Funzioni.RestartTimerMin) - DateTime.Now;

                    this.Invoke((System.Action)(() =>
                    {
                        Lbl_Timer.Text = t.ToString(@"hh\:mm\:ss");
                    }));

                    Thread.Sleep(500);
                }
            });

            if (P != null) Funzioni.Killalo(P.ProcessName);
            Application.Restart();
        }

        #endregion

    }
}