using System;
using System.Windows.Forms;

namespace InitialPositionApp
{
    internal static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Per sicurezza
            if (System.IO.File.Exists("stop")) return;

            Funzioni.LeggiImpostazioni();

            Funzioni.Killalo(Funzioni.AppPath);

            Application.Run(new Frm_Main());

        }
    }
}
