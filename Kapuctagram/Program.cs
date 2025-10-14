using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kapuctagram
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Сначала показываем регистрацию как модальное окно
            using (var registerForm = new RegisterForm())
            {
                var result = registerForm.ShowDialog();

                // Если пользователь нажал "Зарегистрироваться" (а не просто закрыл окно)
                if (result == DialogResult.OK)
                {
                    // Запускаем основную форму чата
                    Application.Run(new KAPUCTAgram.KAPUCTAgram("User"));
                }
                // Если DialogResult != OK — приложение просто завершится
            }
        }
    }
}
