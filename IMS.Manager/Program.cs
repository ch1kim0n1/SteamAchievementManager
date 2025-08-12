using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace IMS.Manager
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            string profileName = "Default";

            if (args.Length == 0)
            {
                Process.Start("IMS.Picker.exe");
                return;
            }

            if (args.Length > 0)
            {
                profileName = args[0];
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InventoryManager(profileName));
        }
    }
}
