using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitialPositionApp
{
    public partial class Frm_Main : Form
    {
        #region Variabili

        ProcessStartInfo PSI;
        Process P;

        private IntPtr PnlHandle = IntPtr.Zero;
        private DateTime Adesso = DateTime.Now;

        private bool Freezattivo = false;

        private CancellationTokenSource CTS = new CancellationTokenSource();

        #endregion


        #region Handles

        public Frm_Main()
        {
            InitializeComponent();

            Btn_Lbl_Versione.Text = "Version: " + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major;
            this.Icon = Properties.Resources.Saki_NuoveXT_Apps_display;

            ThisMe();
        }

        private void Frm_Main_Shown(object sender, EventArgs e)
        {
            try
            {
                PnlHandle = Pnl_Centrale.Handle;

                Timerozzo();

                AvviaExe();

                Adesso = DateTime.Now;

                CTS = new CancellationTokenSource();
                new Task(() => RestartIfFreezedVoid(), CTS.Token, TaskCreationOptions.LongRunning).Start();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Funzioni.FormAltezza = this.Height;
                Funzioni.FormLarghezza = this.Width;
                Funzioni.FormPosX = this.Location.X - Screen.AllScreens[Funzioni.Monitor].WorkingArea.X;
                Funzioni.FormPosY = this.Location.Y - Screen.AllScreens[Funzioni.Monitor].WorkingArea.Y;
                Funzioni.RestartSeFreezed = Cmb_RestartIfFreezed.SelectedIndex == 1;

                Funzioni.LeggiImpostazioni(true);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Btn_About_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("MIT License\r\n\r\nCopyright (c) 2022 Gianluigi Capozzoli\r\n\r\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:\r\n\r\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\r\n\r\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.",
                    "About",MessageBoxButtons.OK,MessageBoxIcon.Information);

                Process.Start("https://github.com/RallyTuning/InitialPositionApp");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Btn_Impostazioni_Click(object sender, EventArgs e)
        {
            try
            {
                Form Frm_Impo = new Frm_Impostazioni();
                if (Frm_Impo.ShowDialog() == DialogResult.OK)
                {
                    ThisMe();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Btn_Esci_Click(object sender, EventArgs e)
        {
            try
            {
                Funzioni.Killalo(P);
                Application.Exit();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Btn_Riavvia_Click(object sender, EventArgs e)
        {
            try
            {
                Funzioni.Killalo(P);

                AvviaExe();

                Adesso = DateTime.Now;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Txt_Restart_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b'; //&& !char.IsControl(e.KeyChar);
        }

        private void Txt_Restart_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Txt_Restart.Text))
                {
                    Funzioni.RestartTimerMin = 0;
                }
                else
                {
                    Adesso = DateTime.Now;
                    Funzioni.RestartTimerMin = Int32.Parse(Txt_Restart.Text);
                }
                //if (Int32.Parse(Txt_Restart.Text) <= 0) return;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ToolBarSotto_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                APIs.ReleaseCapture();
                APIs.SendMessage(this.Handle, APIs.WM_NCLBUTTONDOWN, new IntPtr(APIs.HT_CAPTION), IntPtr.Zero);
            }
        }

        private void Cmb_RestartIfFreezed_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Freezattivo = Cmb_RestartIfFreezed.SelectedIndex == 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

            ToolBarSotto.Visible = Funzioni.MostraToolBar;

            Cmb_RestartIfFreezed.SelectedIndex = Funzioni.RestartSeFreezed ? 1 : 0;

            Funzioni.AltezzaToolBar = Funzioni.MostraToolBar ? ToolBarSotto.Height : 0;
        }

        private async void Timerozzo()
        {
            Lbl_Timer.ForeColor = Color.RoyalBlue;

            Adesso = DateTime.Now;

            await Task.Run(() =>
            {
                while (true)
                {
                    if (Funzioni.RestartTimerMin == 0)
                    {
                        this.Invoke((Action)(() => { Lbl_Timer.Text = "Disabled"; Lbl_Timer.ForeColor = Color.Firebrick; }));

                        Thread.Sleep(1000);

                        Adesso = DateTime.Now;

                        continue;
                    }

                    TimeSpan Fine = Adesso.AddMinutes(Funzioni.RestartTimerMin) - DateTime.Now;

                    Fine = Adesso.AddMinutes(Funzioni.RestartTimerMin) - DateTime.Now;

                    this.Invoke((Action)(() => { Lbl_Timer.Text = Fine.ToString(@"hh\:mm\:ss"); }));

                    Thread.Sleep(500);

                    if (Fine.Ticks <= 0)
                    {
                        Funzioni.Killalo(P);
                        Thread.Sleep(500);
                        AvviaExe();
                        Adesso = DateTime.Now;
                    }
                }
            });
        }

        private async void AvviaExe()
        {
            await Task.Run(() =>
            {
                Funzioni.HWnd = IntPtr.Zero;

                PSI = new ProcessStartInfo(Funzioni.AppPath)
                {
                    CreateNoWindow = true,
                    RedirectStandardInput = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                P = Process.Start(PSI);
                Thread.Sleep(800);

                int MaxCount = 10000;
                int Count = 0;
                while (Funzioni.HWnd == IntPtr.Zero || Count > MaxCount)
                {
                    P.WaitForInputIdle();
                    P.Refresh();

                    Funzioni.HWnd = Funzioni.EnumerateProcessWindowHandles(P.Id).First();
                    Count++;
                }

                if (Funzioni.HWnd == IntPtr.Zero) throw new ApplicationException("The process is taking long to start");

                Funzioni.SetParent(Funzioni.HWnd, PnlHandle);

                //Funzioni.MoveWindow(Funzioni.HWnd, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, true);
                //Funzioni.SetWindowPos(Funzioni.HWnd, IntPtr.Zero, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, SWP.HIDEWINDOW);
                Funzioni.SetWindowPos(Funzioni.HWnd, (IntPtr)APIs.SpecialWindowHandles.HWND_NOTOPMOST,
                    Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar,
                 (uint)APIs.SetWindowPosFlags.SWP_NOACTIVATE | (uint)APIs.SetWindowPosFlags.SWP_NOOWNERZORDER);


                PaintWindow(Funzioni.HWnd);
                //Thread.Sleep(2000);
                APIs.UpdateWindow(Funzioni.HWnd);

                Funzioni.ShowWindow(Funzioni.HWnd, 1);
                Funzioni.MoveWindow(Funzioni.HWnd, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, true);
                //P.Refresh();
            });
        }

        private void RestartIfFreezedVoid()
        {
            CTS.Token.WaitHandle.WaitOne(new TimeSpan(0, 0, 10));

            while (!CTS.IsCancellationRequested)
            {
                if (Freezattivo == false)
                {
                    this.Invoke((Action)(() => { Lbl_Stato.Text = "Disabled"; Lbl_Stato.ForeColor = Color.Firebrick; }));
                    CTS.Token.WaitHandle.WaitOne(new TimeSpan(0, 0, 1));
                    continue;
                }

                this.Invoke((Action)(() => { Lbl_Stato.ForeColor = Color.DarkViolet; Lbl_Stato.Text = "Capturing..."; }));

                List<bool> iHash1 = new List<bool>();
                List<bool> iHash2 = new List<bool>();


                iHash1 = Funzioni.GetHashDaControllo(Pnl_Centrale);

                if (Freezattivo == false) { continue; }
                CTS.Token.WaitHandle.WaitOne(new TimeSpan(0, 0, 30));
                if (Freezattivo == false) { continue; }

                iHash2 = Funzioni.GetHashDaControllo(Pnl_Centrale);

                int equalElements = iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq);


                this.Invoke((Action)(() => { Lbl_Stato.Text = "Match: " + equalElements.ToString() + " (" + (equalElements * 100) / Funzioni.PixelMassimi() + "%)"; }));


                if (equalElements == Funzioni.PixelMassimi())
                {
                    Funzioni.Killalo(P);

                    AvviaExe();

                    Adesso = DateTime.Now;
                }


                if (Freezattivo == false) { continue; }
                CTS.Token.WaitHandle.WaitOne(new TimeSpan(0, 0, 10));

                this.Invoke((Action)(() => { Lbl_Stato.ForeColor = Color.RoyalBlue; Lbl_Stato.Text = "Idle"; }));

                if (Freezattivo == false) { continue; }
                CTS.Token.WaitHandle.WaitOne(new TimeSpan(0, 1, 0));
            }
        }

        private bool PaintWindow(IntPtr hWnd)
        {
            Funzioni.InvalidateRect(hWnd, IntPtr.Zero, true);
            if (Funzioni.UpdateWindow(hWnd))
            {
                Application.DoEvents();
                return true;
            }

            return false;
        }

        #endregion

    }
}