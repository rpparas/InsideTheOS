using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyOS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form.CheckForIllegalCrossThreadCalls = false;
            try
            {
                Application.Run(new OperatingSystem());
            }
            catch (Exception e)
            {
                
            }
        }
    }
}