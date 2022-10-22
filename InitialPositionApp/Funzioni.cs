using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace InitialPositionApp
{
    internal static class Funzioni
    {

        #region WinApi

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        internal delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        internal static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId)
        {
            var handles = new List<IntPtr>();

            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
                EnumThreadWindows(thread.Id,
                    (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);

            return handles;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool InvalidateRect(IntPtr hWnd, IntPtr rect, bool bErase);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UpdateWindow(IntPtr hWnd);






        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);

        #endregion

        /// <summary>
        /// Handle of the nested exe
        /// </summary>
        internal static IntPtr HWnd = IntPtr.Zero;

        internal static string AppPath = @"C:\Windows\notepad.exe";

        internal static int Monitor = 0;

        internal static int FormLarghezza = 500;
        internal static int FormAltezza = 500;
        internal static int FormPosX = 0;
        internal static int FormPosY = 0;


        internal static int ExeLarghezza = 500;
        internal static int ExeAltezza = 500;
        internal static int ExePosX = 0;
        internal static int ExePosY = 0;

        internal static int RestartTimerMin = 0;

        internal static bool MostraToolBar = true;
        internal static int AltezzaToolBar = 0;

        internal static bool RestartSeFreezed = false;

        internal static string Cfg_Path = Path.Combine(Application.StartupPath, "InitialPositionApp.cfg");

        // For the resize
        private static readonly int RidimensionaA = 256;

        /// <summary>
        /// Kill a process by PID
        /// </summary>
        /// <param name="P">PID</param>
        internal static void Killalo(int P)
        {
            if (P <= 0)
            {
                Process.GetProcesses().Where(PR => PR.Id == P).ToList()
                    .ForEach(PRR => PRR.Kill());
            }
        }

        /// <summary>
        /// Kill a process by path exe
        /// </summary>
        /// <param name="P">The:\Full\Path\Of_The\ProcessName.exe</param>
        internal static void Killalo(string P)
        {
            if (P != null)
            {
                FileInfo FI = new FileInfo(P);
                Process.GetProcesses().Where(PR => PR.ProcessName.ToLower() == FI.Name.Replace(FI.Extension, string.Empty).ToLower()).ToList()
                    .ForEach(PRR => PRR.Kill());
            }
        }

        /// <summary>
        /// Kill a process by Process details
        /// </summary>
        /// <param name="P">The process</param>
        internal static void Killalo(Process P)
        {
            if (P != null && P.HasExited == false)
            {
                Process.GetProcesses().Where(PR => PR.ProcessName == P.ProcessName).ToList()
                    .ForEach(PRR => PRR.Kill());
            }
        }

        /// <summary>
        /// Take a screenshot of a control
        /// </summary>
        /// <param name="Ctrl">The control to use</param>
        /// <returns>A screenshot in Bitmap</returns>
        internal static Bitmap TakeComponentScreenShot(Control Ctrl)
        {
            // find absolute position of the control in the screen.
            Rectangle Rect = new Rectangle(Point.Empty, Ctrl.Size);
            do
            {
                Rect.Offset(Ctrl.Location);
                Ctrl = Ctrl.Parent;
            }
            while (Ctrl != null);

            using (Bitmap BMP = new Bitmap(Rect.Width, Rect.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics G = Graphics.FromImage(BMP))
                {
                    G.CopyFromScreen(Rect.Left, Rect.Top, 0, 0, BMP.Size, CopyPixelOperation.SourceCopy);
                }
                return new Bitmap(BMP);
            }
        }

        /// <summary>
        /// Give the list of true/false based on black/white of the input Bitmap
        /// </summary>
        /// <param name="BMPSource">The Bitmap to elaborate</param>
        /// <returns>A list of boolean</returns>
        internal static List<bool> GetHash(Bitmap BMPSource)
        {
            List<bool> ListaResult = new List<bool>();

            using (Bitmap BMPMin = new Bitmap(BMPSource, new Size(RidimensionaA, RidimensionaA))) // Resize for save memory, but less precise
            {
                for (int j = 0; j < BMPMin.Height; j++)
                {
                    for (int i = 0; i < BMPMin.Width; i++)
                    {
                        ListaResult.Add(BMPMin.GetPixel(i, j).GetBrightness() < 0.5f);//reduce colors to true / false
                    }
                }
                return ListaResult;
            }
        }

        /// <summary>
        /// Give the list of boolean based on black/white of the input control to take a screenshot
        /// </summary>
        /// <param name="Ctrl">The control to elaborate</param>
        /// <returns>A list of boolean</returns>
        internal static List<bool> GetHashDaControllo(this Control Ctrl)
        {
            return GetHash(TakeComponentScreenShot(Ctrl));
        }

        /// <summary>
        /// Give the total pixels of the resized image
        /// </summary>
        /// <returns>Int of pixels</returns>
        internal static int PixelMassimi()
        {
            return RidimensionaA * RidimensionaA;
        }

        /// <summary>
        /// Reset and read the file settings of the app
        /// </summary>
        /// <param name="Cancella">True for reset any setting</param>
        internal static void LeggiImpostazioni(bool Cancella = false)
        {
            if (Cancella) File.Delete(Cfg_Path);

            if (File.Exists(Cfg_Path))
            {
                using (FileStream FS = new FileStream(Cfg_Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamReader SR = new StreamReader(FS))
                    {
                        while (!SR.EndOfStream)
                        {
                            string Linea = SR.ReadLine().Trim();
                            if (Linea.StartsWith("*") || String.IsNullOrEmpty(Linea)) continue;

                            string Param = Linea.Split('=')[0].ToLower().Trim();
                            string Valor = Linea.Split('=')[1].ToLower().Trim();

                            switch (Param)
                            {
                                case "exepath":
                                    string Tmp = Valor;
                                    if (!string.IsNullOrEmpty(Tmp) && File.Exists(Tmp)) AppPath = Tmp;
                                    break;

                                case "monitor":
                                    Monitor = Int32.Parse(Valor);
                                    break;

                                case "appwidth":
                                    FormLarghezza = Int32.Parse(Valor);
                                    break;

                                case "appheight":
                                    FormAltezza = Int32.Parse(Valor);
                                    break;

                                case "appposx":
                                    FormPosX = Int32.Parse(Valor);
                                    break;

                                case "appposy":
                                    FormPosY = Int32.Parse(Valor);
                                    break;

                                case "exewidth":
                                    ExeLarghezza = Int32.Parse(Valor);
                                    break;

                                case "exeheight":
                                    ExeAltezza = Int32.Parse(Valor);
                                    break;

                                case "exeposx":
                                    ExePosX = Int32.Parse(Valor);
                                    break;

                                case "exeposy":
                                    ExePosY = Int32.Parse(Valor);
                                    break;

                                case "restarttimermin":
                                    RestartTimerMin = Int32.Parse(Valor);
                                    break;

                                case "showtoolbar":
                                    MostraToolBar = Convert.ToBoolean(Valor);
                                    break;

                                case "restartiffreezed":
                                    RestartSeFreezed = Convert.ToBoolean(Valor);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                using (FileStream FS = new FileStream(Cfg_Path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    FS.Seek(0, SeekOrigin.Begin);
                    FS.SetLength(0);

                    using (StreamWriter SB = new StreamWriter(FS))
                    {
                        SB.WriteLine("**************************************************");
                        SB.WriteLine("*    Configuration file of     *");
                        SB.WriteLine("**************************************************");
                        SB.WriteLine("");
                        SB.WriteLine("* The exe app to start");
                        SB.WriteLine(@"ExePath = " + AppPath);
                        SB.WriteLine("");
                        SB.WriteLine("* ID of the monitor where the app will run");
                        SB.WriteLine("Monitor = " + Monitor.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* The width of the container app");
                        SB.WriteLine("AppWidth = " + FormLarghezza.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* The height of the container app");
                        SB.WriteLine("AppHeight = " + FormAltezza.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* The position X of the app");
                        SB.WriteLine("AppPosX = " + FormPosX.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* The position Y of the app");
                        SB.WriteLine("AppPosY = " + FormPosY.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* The width of the nested exe");
                        SB.WriteLine("ExeWidth = " + ExeLarghezza.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* The height of the nested exe");
                        SB.WriteLine("ExeHeight = " + ExeAltezza.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* The position X of the nested exe");
                        SB.WriteLine("ExePosX = " + ExePosX.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* The position Y of the nested exe");
                        SB.WriteLine("ExePosY = " + ExePosY.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* Auto-restart the app after X minutes");
                        SB.WriteLine("RestartTimerMin = " + RestartTimerMin.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* Auto-restart the app if is freezed");
                        SB.WriteLine("RestartIfFreezed = " + RestartSeFreezed.ToString());
                        SB.WriteLine("");
                        SB.WriteLine("* Show the tollbar on the bottom");
                        SB.WriteLine("ShowToolBar = " + MostraToolBar.ToString());
                    }
                }
            }
        }
    }
}
