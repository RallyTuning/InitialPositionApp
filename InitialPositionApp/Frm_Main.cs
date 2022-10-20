using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitialPositionApp
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


        ProcessStartInfo PSI;
        Process P;

        private IntPtr PnlHandle = IntPtr.Zero;
        private DateTime Adesso = DateTime.Now;

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
                PnlHandle = Pnl_Centrale.Handle;

                Timerozzo();

                AvviaExe();

                Adesso = DateTime.Now;

                _cancelationTokenSource = new CancellationTokenSource();
                new Task(() => CheckReportRequestsAndGenerateReports(), _cancelationTokenSource.Token, TaskCreationOptions.LongRunning).Start();

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
                Process.Start("https://github.com/RallyTuning/InitialPositionApp");
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

                AvviaExe();

                Adesso = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                Thread.Sleep(5000);

                int MaxCount = 10000;
                int Count = 0;
                while (Funzioni.HWnd == IntPtr.Zero || Count > MaxCount)
                {
                    P.WaitForInputIdle();
                    P.Refresh();
                    //Funzioni.HWnd = P.MainWindowHandle;
                    Funzioni.HWnd = Funzioni.EnumerateProcessWindowHandles(P.Id).First();
                    Count++;
                    Console.WriteLine(Count.ToString());
                }

                if (Funzioni.HWnd == IntPtr.Zero) throw new ApplicationException("The process is taking long to start");

                Funzioni.SetParent(Funzioni.HWnd, PnlHandle);

                //Funzioni.MoveWindow(Funzioni.HWnd, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, true);
                //Funzioni.SetWindowPos(Funzioni.HWnd, IntPtr.Zero, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, SWP.HIDEWINDOW);
                Funzioni.SetWindowPos(Funzioni.HWnd, (IntPtr)SpecialWindowHandles.HWND_NOTOPMOST,
                    Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar,
                 (uint)SetWindowPosFlags.SWP_NOACTIVATE | (uint)SetWindowPosFlags.SWP_NOOWNERZORDER);


                PaintWindow(Funzioni.HWnd);
                //Thread.Sleep(2000);
                UpdateWindow(Funzioni.HWnd);

                Funzioni.ShowWindow(Funzioni.HWnd, 1);
                Funzioni.MoveWindow(Funzioni.HWnd, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, true);
                //P.Refresh();
            });
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



        private async void Timerozzo()
        {
            Adesso = DateTime.Now;

            await Task.Run(() =>
                {
                    while (true)
                    {
                        if (Funzioni.RestartTimerMin == 0)
                        {
                            this.Invoke((Action)(() => { Lbl_Timer.Text = "Disabled"; }));

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

        #endregion

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //PaintWindow(this.Handle );
            //Thread.Sleep(2000);
            //Application.DoEvents();
            //UpdateWindow(Funzioni.HWnd);

            //this.ForcePaint();
            SendMessage(Funzioni.HWnd, WmPaint, IntPtr.Zero, IntPtr.Zero);
        }

        private const int WmPaint = 0x000F;

        [DllImport("User32.dll")]
        public static extern Int64 SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SendMessage(Funzioni.HWnd, WmPaint, IntPtr.Zero, IntPtr.Zero);
        }

        [DllImport("user32.dll")]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InvalidateRect(Funzioni.HWnd, IntPtr.Zero, true);
        }



        private CancellationTokenSource _cancelationTokenSource = new CancellationTokenSource();

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                

                

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CheckReportRequestsAndGenerateReports( )
        {
            while (!_cancelationTokenSource.IsCancellationRequested)
            {

                //var reportRequestTask = Task.Factory.StartNew(() => noRequest = CheckReportRequestsAndGenerateReports(), _cancelationTokenSource.Token);

                //Bitmap bmp = new Bitmap(Pnl_Centrale.Width, Pnl_Centrale.Height);
                //Pnl_Centrale.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                //bmp.Save(@"MyPanelImage.bmp");
                List<Bitmap> Splittate1 = splitBitmap(TakeComponentScreenShot(Pnl_Centrale), 2, 2);
                List<Bitmap> Splittate2 = new List<Bitmap>();
                List<List<bool>> iHash1 = new List<List<bool>>();
                List<List<bool>> iHash2 = new List<List<bool>>();

                foreach (Bitmap bitmap in Splittate1)
                {
                    iHash1.Add(GetHash(bitmap));
                }

                //TakeComponentScreenShot(Pnl_Centrale).Save(@"screen1.bmp");

                //Task task = Task.Delay(30000)
                //.ContinueWith(t => TakeComponentScreenShot(Pnl_Centrale).Save(@"screen2.bmp"));
                //task.Wait();

                //Task task = Task.Delay(30000)
                //                .ContinueWith(t => Splittate2 = splitBitmap(TakeComponentScreenShot(Pnl_Centrale), 2, 2));
                //task.Wait();

                _cancelationTokenSource.Token.WaitHandle.WaitOne(new TimeSpan(0, 0, 30));

                Splittate2 = splitBitmap(TakeComponentScreenShot(Pnl_Centrale), 2, 2);

                foreach (Bitmap bitmap in Splittate2)
                {
                    iHash2.Add(GetHash(bitmap));
                }

                int equalElements = 0;
                for (int H = 0; H < iHash1.Count(); H++)
                {
                    equalElements += iHash1[H].Zip(iHash2[H], (i, j) => i == j).Count(eq => eq);
                }

                //List<bool> iHash1 = GetHash(new Bitmap(@"screen1.bmp"));
                //List<bool> iHash2 = GetHash(new Bitmap(@"screen2.bmp"));

                //determine the number of equal pixel (x of 256)
                //int equalElements = iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq);

                Console.WriteLine(equalElements.ToString());

                if (equalElements == 1024)
                {
                    Funzioni.Killalo(P);

                    AvviaExe();

                    Adesso = DateTime.Now;
                }


                _cancelationTokenSource.Token.WaitHandle.WaitOne(new TimeSpan(0,1,0));

            }
        }







        public static Bitmap TakeComponentScreenShot(Control   ctrl)
        {
            // find absolute position of the control in the screen.
            Rectangle rect = new Rectangle(Point.Empty, ctrl.Size);
            do
            {
                rect.Offset(ctrl.Location);
                ctrl = ctrl.Parent;
            }
            while (ctrl != null);

            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
                }
                return new Bitmap(bmp);
            }
        }

        public static List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            using (Bitmap bmpMin = new Bitmap(bmpSource, new Size(64, 64)))
            {
                for (int j = 0; j < bmpMin.Height; j++)
                {
                    for (int i = 0; i < bmpMin.Width; i++)
                    {
                        //reduce colors to true / false                
                        lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                    }
                }
                return lResult;
            }
        }

        public List<Bitmap> splitBitmap(Bitmap picture, int row, int col)
        {
            List<Bitmap> lResult = new List<Bitmap>();

            using (var originalImage = new Bitmap(picture))
            {
                var incX = originalImage.Width / row;
                var incY = originalImage.Height / col;
                var startX = 0;
                for (int i = 0; i < row; i++)
                {
                    var startY = 0;
                    for (int j = 0; j < col; j++)
                    {
                        var rect = new Rectangle(startX, startY, incX, incY);
                        using (var clonedImage = originalImage.Clone(rect, originalImage.PixelFormat))
                        {
                            lResult.Add(new Bitmap(clonedImage)); //clonedImage.Save(directory + $"\\PageImage{i + 1}{j + 1}.jpg");
                            clonedImage.Save($"PageImage{i + 1}{j + 1}.jpg");
                        }

                        startY += incY;
                    }
                    startX += incX;
                }
            }
            return lResult;
        }
    }

    public static class WindowsApi
    {
        private const int WmPaint = 0x000F;

        [DllImport("User32.dll")]
        public static extern Int64 SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public static void ForcePaint(this Form form)
        {
            SendMessage(form.Handle, WmPaint, IntPtr.Zero, IntPtr.Zero);
        }
    }
}