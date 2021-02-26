using app.Tabaldi.PACT.App.AuthenticationModule;
using System;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AuthenticationForm());
        }
    }
}
