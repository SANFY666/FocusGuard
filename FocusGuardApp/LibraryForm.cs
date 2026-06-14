using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FocusGuardApp
{
    public partial class LibraryForm : Form
    {
        public LibraryForm()
        {
            InitializeComponent();
        }

        private async void LibraryForm_Load(object sender, EventArgs e)
        {
            string collectionsFolder = Path.Combine(Application.StartupPath, "Mods", "Collections");

            if (!Directory.Exists(collectionsFolder))
            {
                Directory.CreateDirectory(collectionsFolder);
                MessageBox.Show("Папку Mods/Collections створено. Закинь туди свої .dat архіви!", "Інфо");
                return;
            }

            string[] archiveFiles = Directory.GetFiles(collectionsFolder, "*.dat");

            flowLayoutPanel1.Controls.Clear();

            foreach (string file in archiveFiles)
            {
                await LoadCardAsync(file);
            }
        }

        // завантаження даних для картки асинхронно
        private async Task LoadCardAsync(string filePath)
        {
            string title = Path.GetFileNameWithoutExtension(filePath);
            Image cover = await Task.Run(() => GetThumbnailFromArchive(filePath));

            GalleryCard card = new GalleryCard();
            card.SetData(title, cover, filePath);

            card.Margin = new Padding(8);

            flowLayoutPanel1.Controls.Add(card);
        }

        // превью
        private Image GetThumbnailFromArchive(string zipPath)
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            entry.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            entry.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                        {
                            using (Stream stream = entry.Open())
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    stream.CopyTo(ms);
                                    ms.Position = 0;
                                    using (Image originalImage = Image.FromStream(ms))
                                    {
                                        return ResizeImage(originalImage, 200, 250);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            return null;
        }

        private Image ResizeImage(Image img, int maxWidth, int maxHeight)
        {
            double ratioX = (double)maxWidth / img.Width;
            double ratioY = (double)maxHeight / img.Height;
            double ratio = Math.Max(ratioX, ratioY);

            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);

            Bitmap newImage = new Bitmap(maxWidth, maxHeight);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, (maxWidth - newWidth) / 2, (maxHeight - newHeight) / 2, newWidth, newHeight);
            }
            return newImage;
        }
    }
}