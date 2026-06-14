using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace FocusGuardApp
{
    public partial class GalleryForm : Form
    {
        private ZipArchive archive;
        private int currentIndex = 0;
        private string selectedArchivePath;
        private bool isFullScreen = false;

        public GalleryForm(string filePath)
        {
            InitializeComponent();
            selectedArchivePath = filePath;
            this.MouseWheel += GalleryForm_MouseWheel;
        }

        private void GalleryForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(selectedArchivePath))
            {
                archive = ZipFile.OpenRead(selectedArchivePath);
                if (archive.Entries.Count > 0)
                {
                    ShowImage(currentIndex);
                }
            }
            else
            {
                MessageBox.Show("Файл кешу не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ShowImage(int index)
        {
            if (archive == null || archive.Entries.Count == 0) return;

            try
            {
                var entry = archive.Entries[index];
                this.Text = $"Секретна Бібліотека - Зображення {index + 1} з {archive.Entries.Count}";

                if (pictureBox1.Image != null)
                {
                    var oldImage = pictureBox1.Image;
                    pictureBox1.Image = null;
                    oldImage.Dispose();
                }

                using (Stream stream = entry.Open())
                {
                    pictureBox1.Image = Image.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося прочитати зображення.\nПричина: {ex.Message}", "Помилка читання", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToggleFullScreen()
        {
            if (!isFullScreen)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                isFullScreen = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                isFullScreen = false;
            }
        }

        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
            ToggleFullScreen();
        }

        private void GalleryForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (archive == null || archive.Entries.Count == 0) return;

            if (e.Delta < 0)
            {
                NextImage();
            }
            else if (e.Delta > 0)
            {
                PrevImage();
            }
        }

        private void NextImage()
        {
            currentIndex++;
            if (currentIndex >= archive.Entries.Count) currentIndex = 0;
            ShowImage(currentIndex);
        }

        private void PrevImage()
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = archive.Entries.Count - 1;
            ShowImage(currentIndex);
        }

        private void GalleryForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (archive == null || archive.Entries.Count == 0) return;

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                NextImage();
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                PrevImage();
            }
            else if (e.KeyCode == Keys.F11)
            {
                ToggleFullScreen();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (isFullScreen)
                {
                    ToggleFullScreen();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void GalleryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (archive != null) archive.Dispose();
            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
        }

        private void btnNext_Click(object sender, EventArgs e) => NextImage();
        private void btnPrev_Click(object sender, EventArgs e) => PrevImage();
    }
}