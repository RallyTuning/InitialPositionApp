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
                if (P.HasExited == false) Funzioni.Killalo(P.ProcessName);
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
                if (P.HasExited == false) Funzioni.Killalo(P.ProcessName);
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
                RedirectStandardError = false
            };

            PSI.WindowStyle = ProcessWindowStyle.Hidden;

            P = Process.Start(PSI);



            //p.WaitForInputIdle();

            //Thread.Sleep(500);

            //if (!p.WaitForInputIdle(10000)) // 10 s timout
            //    throw new ApplicationException("Il processo impiega troppo ad avviarsi");

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

            Funzioni.MoveWindow(Funzioni.HWnd, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, true);

            //PSI.WindowStyle = ProcessWindowStyle.Normal;

            //SetWindowLong(Funzioni.HWnd, (int)WindowLongFlags.GWL_STYLE, (int)(WindowStyles.WS_VISIBLE | WindowStyles.WS_CHILD));

            //SetWindowPos(Funzioni.HWnd, IntPtr.Zero, 0, 0, 0, 0, 1);

            //Funzioni.ShowWindow(Funzioni.HWnd, 1);
            //Funzioni.ShowWindow(Funzioni.HWnd, 1);
            //PaintWindow(Funzioni.HWnd);
            //P.Refresh();
            //PSI.WindowStyle = ProcessWindowStyle.Normal;
            Funzioni.ShowWindow(Funzioni.HWnd, 1);

        }

        enum WindowStyles : uint
        {
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = 0x80000000,
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,

            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,

            WS_CAPTION = WS_BORDER | WS_DLGFRAME,
            WS_TILED = WS_OVERLAPPED,
            WS_ICONIC = WS_MINIMIZE,
            WS_SIZEBOX = WS_THICKFRAME,
            WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,

            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_CHILDWINDOW = WS_CHILD,

            //Extended Window Styles

            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_TRANSPARENT = 0x00000020,

            //#if(WINVER >= 0x0400)

            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,

            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,

            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,

            WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
            WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),
            //#endif /* WINVER >= 0x0400 */

            //#if(WIN32WINNT >= 0x0500)

            WS_EX_LAYERED = 0x00080000,
            //#endif /* WIN32WINNT >= 0x0500 */

            //#if(WINVER >= 0x0500)

            WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
            WS_EX_LAYOUTRTL = 0x00400000, // Right to left mirroring
                                          //#endif /* WINVER >= 0x0500 */

            //#if(WIN32WINNT >= 0x0500)

            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_NOACTIVATE = 0x08000000
            //#endif /* WIN32WINNT >= 0x0500 */

        }

        enum WindowLongFlags : int
        {
            GWL_EXSTYLE = -20,
            GWLP_HINSTANCE = -6,
            GWLP_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_USERDATA = -21,
            GWL_WNDPROC = -4,
            DWLP_USER = 0x8,
            DWLP_MSGRESULT = 0x0,
            DWLP_DLGPROC = 0x4

        }

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


        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);


        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool InvalidateRect(IntPtr hWnd, IntPtr rect, bool bErase);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UpdateWindow(IntPtr hWnd);

        public bool PaintWindow(IntPtr hWnd)
        {
            InvalidateRect(hWnd, IntPtr.Zero, true);
            if (UpdateWindow(hWnd))
            {
                Application.DoEvents();
                return true;
            }

            return false;
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

            if (P.HasExited == false) Funzioni.Killalo(P.ProcessName);
            AvviaExe();

            ReStarter();
        }

        #endregion

    }
}