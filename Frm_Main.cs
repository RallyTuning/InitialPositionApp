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

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        readonly DateTime adesso = DateTime.Now;

        ProcessStartInfo PSI;
        Process P;

        #endregion

        #region Handles

        public Frm_Main()
        {
            InitializeComponent();

            this.Location = Screen.AllScreens[Funzioni.Monitor].WorkingArea.Location;

            //Size S = Screen.AllScreens[Funzioni.Monitor].WorkingArea.Size;
            this.Size = new Size(Funzioni.FormLarghezza,  Funzioni.FormAltezza);

            Txt_Restart.Text = Funzioni.RestartTimerMin.ToString();

            this.Focus();

            ToolBarSotto.Visible = Funzioni.MostraToolBar;
        }

        private void Frm_Main_Shown(object sender, EventArgs e)
        {
            try
            {
                ReStarter();

                PSI = new ProcessStartInfo(Funzioni.AppPath)
                {
                    CreateNoWindow = false,
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
                IntPtr HWnd = IntPtr.Zero;

                while (HWnd == IntPtr.Zero || Count > MaxCount)
                {
                    P.WaitForInputIdle();
                    P.Refresh();
                    HWnd = P.MainWindowHandle;
                    Count++;
                }

                if (HWnd == IntPtr.Zero) throw new ApplicationException("Il processo impiega troppo ad avviarsi");

                SetParent(HWnd, Pnl_Centrale.Handle);
                
                int AltezzaToolBar = 0;
                if (Funzioni.MostraToolBar) AltezzaToolBar = ToolBarSotto.Height;

                MoveWindow(HWnd, Funzioni.PosX, Funzioni.PosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - AltezzaToolBar, true);

                //PSI.WindowStyle = ProcessWindowStyle.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Chiudi_Click(object sender, EventArgs e)
        {
            try
            {
                Funzioni.Killalo(P.ProcessName);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Riavvia_Click(object sender, EventArgs e)
        {
            try
            {
                Funzioni.Killalo(P.ProcessName);
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region void

        private async void ReStarter()
        {
            if (Funzioni.RestartTimerMin == 0) return;

            await Task.Run(() =>
            {
                DateTime dopo = adesso.AddMinutes(Funzioni.RestartTimerMin);
                TimeSpan t = adesso.AddMinutes(Funzioni.RestartTimerMin) - DateTime.Now;

                while (t.Ticks >= 0)
                {
                    t = adesso.AddMinutes(Funzioni.RestartTimerMin) - DateTime.Now;

                    this.Invoke((System.Action)(() =>
                    {
                        Lbl_Timer.Text = t.ToString(@"hh\:mm\:ss");
                    }));


                    Thread.Sleep(500);
                }
            });

            Funzioni.Killalo(P.ProcessName);
            Application.Restart();
        }

        #endregion

    }
}