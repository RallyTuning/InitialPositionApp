using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        readonly string appPath = "C:\\Users\\rally\\AppData\\Local\\Programs\\Reolink\\Reolink.exe";
        readonly int sinistra = 380;
        readonly int barra = 270;
        int restartTimerMin = 1;

        readonly DateTime adesso = DateTime.Now;

        ProcessStartInfo psi;
        Process p;

        #endregion

        #region Handles

        public Frm_Main()
        {
            InitializeComponent();

            this.Location = Screen.AllScreens[0].WorkingArea.Location;
            Size s = Screen.AllScreens[0].WorkingArea.Size;
            this.Size = new Size(s.Width - sinistra, s.Height);

            this.Focus();
            toolStrip1.Focus();

            //Per sicurezza
            if (File.Exists("stop"))
            {
                Application.Exit();
            }
        }

        private void Frm_Main_Shown(object sender, EventArgs e)
        {
            try
            {
                ReStarter();

                psi = new ProcessStartInfo(appPath)
                {
                    CreateNoWindow = true,
                    RedirectStandardInput = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false
                };

                p = Process.Start(psi);
                p.WaitForInputIdle();

                Thread.Sleep(500);

                SetParent(p.MainWindowHandle, Pnl_Centrale.Handle);

                psi.WindowStyle = ProcessWindowStyle.Maximized;

                MoveWindow(p.MainWindowHandle, -barra, 0, this.Width + barra, this.Height - toolStrip1.Height, true);

                //CenterToScreen();
                psi.WindowStyle = ProcessWindowStyle.Maximized;
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
                p.Close();
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
                p.Close();
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
                if (string.IsNullOrEmpty(Txt_Restart.Text)) { return; }
                if (Int32.Parse(Txt_Restart.Text) <= 0) { return; }

                restartTimerMin = Int32.Parse(Txt_Restart.Text);
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
            await Task.Run(() =>
            {
                DateTime dopo = adesso.AddMinutes(restartTimerMin);
                TimeSpan t = adesso.AddMinutes(restartTimerMin) - DateTime.Now;

                while (t.Ticks >= 0)
                {
                    t = adesso.AddMinutes(restartTimerMin) - DateTime.Now;

                    this.Invoke((System.Action)(() =>
                    {
                        Lbl_Timer.Text = t.ToString(@"hh\:mm\:ss");
                    }));


                    Thread.Sleep(500);
                }
            });

            p.Close();
            Application.Restart();
        }

        #endregion

    }
}