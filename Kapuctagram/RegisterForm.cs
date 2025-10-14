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

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            RegisterB.Enabled = false;
        }

        private void PasswordTB_TextChanged(object sender, EventArgs e)
        {
            string password = PasswordTB.Text;

            string resultOfTest = StringUtils.TestPassword(password);
            bool isPasswordOK = false;

            RegisterB.Enabled = false;
            if (resultOfTest == "Твой пароль херня" || resultOfTest == "хз пон")
            {
                isPasswordOK = true;
                RegisterB.Enabled = true;
            }
            ShowError(resultOfTest);
            

        }
        private void ShowError(string message)
        {
            Diff_of_PasswordL.Text = message;
            Diff_of_PasswordL.Visible = true;
        }

        private void RegisterB_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
