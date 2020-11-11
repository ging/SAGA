using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plato_saga
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static Mutex mutex = new Mutex(true, "{856c1800-9211-4bac-9bd9-21e6c32c8304}");
        [STAThread]
        
        static void Main(string[] args)
        {            
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());                
                mutex.ReleaseMutex();
            }
            else
            {
                if (plato_saga.Properties.Settings.Default.app_lang == "es")
                MessageBox.Show("El programa ya se está ejecutando.");
                if (plato_saga.Properties.Settings.Default.app_lang == "en")
                    MessageBox.Show("Application is already running.");
            }
        }
    }
}
