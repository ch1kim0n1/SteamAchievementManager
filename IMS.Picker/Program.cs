using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace IMS.Picker
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProfilePicker());
        }
    }
}
