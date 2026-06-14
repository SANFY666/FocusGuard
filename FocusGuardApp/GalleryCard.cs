using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FocusGuardApp
{
    public partial class GalleryCard : UserControl
    {
        private string archivePath;

        public GalleryCard()
        {
            InitializeComponent();
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            Color hoverColor = Color.FromArgb(70, 70, 70); // картка світлішає
            this.BackColor = hoverColor;
            lblTitle.BackColor = hoverColor; 
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            Point mousePos = this.PointToClient(Cursor.Position);
            if (!this.ClientRectangle.Contains(mousePos))
            {
                Color normalColor = Color.FromArgb(40, 40, 40); 
                this.BackColor = normalColor;
                lblTitle.BackColor = normalColor; 
            }
        }

        public void SetData(string title, Image coverImage, string path)
        {
            lblTitle.Text = title.ToUpper();
            pbCover.Image = coverImage;
            archivePath = path;
        }

        private void Card_Click(object sender, EventArgs e)
        {
            using (GalleryForm gallery = new GalleryForm(archivePath))
            {
                Form parentForm = this.FindForm();
                parentForm.Hide(); // Ховаємо головне вікно Бібліотеки
                gallery.ShowDialog();
                parentForm.Show(); // Повертаємо після закриття перегляду
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int radius = 12;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(0, 0, radius, radius, 180, 90); // Верхній лівий
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90); // Верхній правий
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90); // Нижній правий
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90); // Нижній лівий
            path.CloseFigure();

            this.Region = new Region(path);
        }
    }
}