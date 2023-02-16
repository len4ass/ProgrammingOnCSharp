using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NotepadPlus.Overrides;

namespace NotepadPlus
{
    /// <summary>
    /// Главный класс программы.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Главный метод.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OverrideForms.Run(new Form1());
        }
    }
}
