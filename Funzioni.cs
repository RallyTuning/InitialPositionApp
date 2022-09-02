using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace InitianPositionApp
{
    internal static class Funzioni
    {
        internal static string AppPath = "C:\\Windows\\notepad.exe";

        internal static int Monitor = 0;
        internal static int FormLarghezza = 300;
        internal static int FormAltezza = 300;
        internal static int PosX = 0;
        internal static int PosY = 0;
        internal static int ExeLarghezza = 0;
        internal static int ExeAltezza = 0;
        internal static int RestartTimerMin = 0;
        internal static bool MostraToolBar = true;

        internal static void Killalo(string P)
        {
            if (P != null)
            {
                Process.GetProcesses()
                            .Where(PR => PR.ProcessName == P).ToList()
                            .ForEach(PRR => PRR.Kill());
            }
        }

        internal static void LeggiImpostazioni()
        {
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

                                case "posx":
                                    PosX = Int32.Parse(Valor);
                                    break;

                                case "posy":
                                    PosY = Int32.Parse(Valor);
                                    break;

                                case "exewidth":
                                    ExeLarghezza = Int32.Parse(Valor);
                                    break;

                                case "exeheight":
                                    ExeAltezza = Int32.Parse(Valor);
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
                        SB.WriteLine("*  File di configurazione di InitianPositionApp  *");
                        SB.WriteLine("**************************************************");
                        SB.WriteLine("");
                        SB.WriteLine("* The exe app to start");
                        SB.WriteLine(@"ExePath = C:\Windows\notepad.exe");
                        SB.WriteLine("");
                        SB.WriteLine("* ID of the monitor where the app will run");
                        SB.WriteLine("Monitor = 0");
                        SB.WriteLine("");
                        SB.WriteLine("* The width of the container app");
                        SB.WriteLine("AppWidth = 500");
                        SB.WriteLine("");
                        SB.WriteLine("* The height of the container app");
                        SB.WriteLine("AppHeight = 500");
                        SB.WriteLine("");
                        SB.WriteLine("* The position X of the nested exe");
                        SB.WriteLine("PosX = 0");
                        SB.WriteLine("");
                        SB.WriteLine("* The position Y of the nested exe");
                        SB.WriteLine("PosY = 0");
                        SB.WriteLine("");
                        SB.WriteLine("* The width of the nested exe");
                        SB.WriteLine("ExeWidth = 400");
                        SB.WriteLine("");
                        SB.WriteLine("* The height of the nested exe");
                        SB.WriteLine("ExeHeight = 400");
                        SB.WriteLine("");
                        SB.WriteLine("* Auto-restart the app after X minutes");
                        SB.WriteLine("RestartTimerMin = 0");
                        SB.WriteLine("");
                        SB.WriteLine("* The tollbar on the bottom");
                        SB.WriteLine("ShowToolBar = true");
                    }
                }
            }
        }
    }
}
