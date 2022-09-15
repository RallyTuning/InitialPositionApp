using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

                AvviaExe();
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
                Funzioni.FormPosX = this.Location.X - Screen.AllScreens[Funzioni.Monitor].WorkingArea.X;
                Funzioni.FormPosY = this.Location.Y - Screen.AllScreens[Funzioni.Monitor].WorkingArea.Y;

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
                Funzioni.Killalo(P);
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
                Funzioni.Killalo(P);
                //Application.Restart();
                AvviaExe();
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

            ToolBarSotto.Visible = Funzioni.MostraToolBar;

            if (Funzioni.MostraToolBar)
                Funzioni.AltezzaToolBar = ToolBarSotto.Height;
            else
                Funzioni.AltezzaToolBar = 0;
        }

        private void AvviaExe()
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

            int MaxCount = 10000;
            int Count = 0;
            Thread.Sleep(500);
            while (Funzioni.HWnd == IntPtr.Zero || Count > MaxCount)
            {
                P.WaitForInputIdle();
                P.Refresh();
                //Funzioni.HWnd = P.MainWindowHandle;
                Funzioni.HWnd = Funzioni.EnumerateProcessWindowHandles(P.Id).First();
                Count++;
            }

            if (Funzioni.HWnd == IntPtr.Zero) throw new ApplicationException("The process is taking long to start");

            Funzioni.SetParent(Funzioni.HWnd, Pnl_Centrale.Handle);

            //Funzioni.MoveWindow(Funzioni.HWnd, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, true);
            //Funzioni.SetWindowPos(Funzioni.HWnd, IntPtr.Zero, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, SWP.HIDEWINDOW);
            Funzioni.SetWindowPos(Funzioni.HWnd, (IntPtr)SpecialWindowHandles.HWND_NOTOPMOST,
                Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar,
             (uint)SetWindowPosFlags.SWP_NOACTIVATE | (uint)SetWindowPosFlags.SWP_NOOWNERZORDER);


            Funzioni.MoveWindow(Funzioni.HWnd, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, true);
            P.Refresh();
            PaintWindow(Funzioni.HWnd);
            UpdateWindow(Funzioni.HWnd);
            Funzioni.ShowWindow(Funzioni.HWnd, 1);


        }

        [DllImport("user32.dll")]
        static extern bool UpdateWindow(IntPtr hWnd);


        public bool PaintWindow(IntPtr hWnd)
        {
            Funzioni.InvalidateRect(hWnd, IntPtr.Zero, true);
            if (Funzioni.UpdateWindow(hWnd))
            {
                Application.DoEvents();
                return true;
            }

            return false;
        }


        /// <summary>
        ///     Special window handles
        /// </summary>
        public enum SpecialWindowHandles
        {
            // ReSharper disable InconsistentNaming
            /// <summary>
            ///     Places the window at the top of the Z order.
            /// </summary>
            HWND_TOP = 0,
            /// <summary>
            ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            /// </summary>
            HWND_BOTTOM = 1,
            /// <summary>
            ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
            /// </summary>
            HWND_TOPMOST = -1,
            /// <summary>
            ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            /// </summary>
            HWND_NOTOPMOST = -2
            // ReSharper restore InconsistentNaming
        }

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            // ReSharper disable InconsistentNaming

            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,

            // ReSharper restore InconsistentNaming
        }

        public static class SWP
        {
            public static readonly int
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000;
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

            Funzioni.Killalo(P);
            AvviaExe();

            ReStarter();
        }

        #endregion

    }
}