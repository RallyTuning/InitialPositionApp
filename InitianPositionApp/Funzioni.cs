using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace InitianPositionApp
{
    internal static class Funzioni
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

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

        internal static void Killalo(string P)
        {
            if (P != null)
            {
                Process.GetProcesses()
                            .Where(PR => PR.ProcessName == P).ToList()
                            .ForEach(PRR => PRR.Kill());
            }
        }

        internal static void LeggiImpostazioni(bool Cancella = false)
        {
            if (Cancella) File.Delete("InitianPositionApp.cfg");

            if (File.Exists("InitianPositionApp.cfg"))
            {
                using (FileStream FS = new FileStream("InitianPositionApp.cfg", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
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

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                using (FileStream FS = new FileStream("InitianPositionApp.cfg", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    FS.Seek(0, SeekOrigin.Begin);
                    FS.SetLength(0);

                    using (StreamWriter SB = new StreamWriter(FS))
                    {
                        SB.WriteLine("**************************************************");
                        SB.WriteLine("*    Configuration file of InitianPositionApp    *");
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
                        SB.WriteLine("* Show the tollbar on the bottom");
                        SB.WriteLine("ShowToolBar = " + MostraToolBar.ToString());
                    }
                }
            }
        }
    }
}
