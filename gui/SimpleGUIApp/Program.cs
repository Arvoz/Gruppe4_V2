using System;
using System.Windows.Forms;

namespace SimpleGUIApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());  // Make sure it references Form1
        }
    }
}
