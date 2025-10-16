using System;
using System.Windows.Forms;
using Kapuctagram;

namespace KAPUCTAgram
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запускаем сначала форму регистрации/входа
            Application.Run(new RegisterForm());
        }
    }
}