using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kapuctagram
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void PasswordTB_TextChanged(object sender, EventArgs e)
        {
            string password = Diff_of_PasswordL.Text;

            Diff_of_PasswordL.Text = "Твой пароль херня";
            if (password.Length < 20)
            {
                ShowError("Пароль слишком короткий!");
                Console.WriteLine(StringUtils.GetLength(password));
                return;
            }
            else if (StringUtils.GetUppercaseCount(password) < 5)
            {
                ShowError("Недостаточно заглавных букв!");
                Console.WriteLine(StringUtils.GetUppercaseCount(password));
                return;
            }
            else if (StringUtils.GetSpecialCharCount(password) < 5)
            {
                ShowError("Недостаточно специальных символов!");
                Console.WriteLine(StringUtils.GetSpecialCharCount(password));
                return;
            }
        }
        private void ShowError(string message)
        {
            Diff_of_PasswordL.Text = message;
            Diff_of_PasswordL.Visible = true;
        }

        private void RegisterB_Click(object sender, EventArgs e)
        {

        }
    }
}
