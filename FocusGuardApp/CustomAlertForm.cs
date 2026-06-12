using System;
using System.Drawing;
using System.Windows.Forms;

namespace FocusGuardApp
{
    public partial class CustomAlertForm : Form
    {
        public CustomAlertForm(string message, Color themeColor)
        {
            InitializeComponent();

            string[] parts = message.Split(new[] { "\n\n" }, StringSplitOptions.None);
            if (parts.Length > 1)
            {
                this.Text = parts[0];
                lblMessage.Text = parts[1];
            }
            else
            {
                this.Text = "Сповіщення FocusGuard";
                lblMessage.Text = message;
            }

            btnOk.BackColor = themeColor;
            btnOk.ForeColor = Color.FromArgb(33, 33, 33);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}