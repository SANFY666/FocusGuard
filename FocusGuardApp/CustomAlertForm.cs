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

            lblMessage.AutoSize = false;
            lblMessage.Size = new Size(340, 130);
            lblMessage.Location = new Point((this.ClientSize.Width - lblMessage.Width) / 2, 15);
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            lblMessage.Text = message;
            
            btnOk.BackColor = themeColor;
            btnOk.Location = new Point((this.ClientSize.Width - btnOk.Width) / 2, lblMessage.Bottom + 5);
            btnOk.DialogResult = DialogResult.OK;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}