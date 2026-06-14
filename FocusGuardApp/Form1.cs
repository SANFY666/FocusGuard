using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace FocusGuardApp
{
    public partial class Form1 : Form
    {
        private TimeSession currentSession;
        private bool isSessionActive = false;
        private Color currentThemeColor = Color.FromArgb(46, 204, 113);
        private bool isPatchUnlocked = false;

        public Form1()
        {
            InitializeComponent();

            SQLitePCL.Batteries.Init();

            try
            {
                DatabaseManager.InitializeDatabase();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\n\nСправжня причина:\n" + ex.InnerException.Message;
                }
                MessageBox.Show($"Помилка:\n{errorMessage}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cmbTheme.SelectedIndex = 0;

            if (cmbActivity.Items.Count == 0)
            {
                cmbActivity.Items.AddRange(new string[] { "Brawl Stars / Ігри", "Серіали / YouTube", "Програмування / Навчання" });
            }
            cmbActivity.SelectedIndex = 0;

            label1.Click += Label1_Click;
            btnStartSession.Click += BtnStartSession_Click;
            sessionTimer.Tick += SessionTimer_Tick;
        }

        private void CheckForPatch()
        {
            string patchFile = @"Mods\unlock.dat";

            if (File.Exists(patchFile))
            {
                isPatchUnlocked = true;
                UpdateModeUI(); // Викликаємо загальний метод оновлення інтерфейсу
            }
        }

        private void ApplyAccentColor(Color newColor)
        {
            currentThemeColor = newColor;
            roundedPanel1.BorderColor = newColor;
            panelProgressFill.BackColor = newColor;

            Button[] accentButtons = {
                btnStartSession,
                btnPresetGame,
                btnPresetRead,
                btnPresetCode,
                btnOpenStatistics
            };

            foreach (var btn in accentButtons)
            {
                btn.BackColor = newColor;
                btn.ForeColor = Color.FromArgb(33, 33, 33);
            }
        }

        private void BtnStartSession_Click(object sender, EventArgs e)
        {
            if (!isSessionActive)
            {
                // старт
                string activity = cmbActivity.Text;
                int plannedTime = (int)numPlannedMinutes.Value;

                currentSession = new TimeSession(activity, plannedTime);

                isSessionActive = true;
                sessionTimer.Start();

                cmbActivity.Enabled = false;
                numPlannedMinutes.Enabled = false;

                btnStartSession.Text = "Закінчити";
                btnStartSession.BackColor = Color.Tomato;
            }
            else
            {
                // стоп
                sessionTimer.Stop();
                isSessionActive = false;

                // час
                int actualMinutes = currentSession.ElapsedSeconds / 60;
                if (actualMinutes == 0 && currentSession.ElapsedSeconds > 0)
                {
                    actualMinutes = 1;
                }

                string rawActivity = currentSession.ActivityName;
                string category = "Загальне";
                string activityName = rawActivity;

                if (rawActivity.Contains(" / "))
                {
                    var parts = rawActivity.Split(new[] { " / " }, StringSplitOptions.RemoveEmptyEntries);
                    activityName = parts[0].Trim();
                    if (parts.Length > 1)
                    {
                        category = parts[1].Trim();
                    }
                }

                // інфо в базу 
                DatabaseManager.AddSession(category, activityName, actualMinutes);

                string alertText = $"Активність завершено!\n\n{currentSession.GetSessionSummary()}";
                using (CustomAlertForm alert = new CustomAlertForm(alertText, currentThemeColor))
                {
                    alert.ShowDialog();
                }

                cmbActivity.Enabled = true;
                numPlannedMinutes.Enabled = true;

                btnStartSession.Text = "Почати активність";

                ApplyAccentColor(currentThemeColor);

                lblTimerStatus.Text = "Очікування старту...";
                lblTimerStatus.ForeColor = Color.White;
                panelProgressFill.Width = 0;
            }
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            if (currentSession != null)
            {
                currentSession.Tick();
                lblTimerStatus.Text = currentSession.GetSessionSummary();

                if (currentSession.IsOverdue())
                {
                    lblTimerStatus.ForeColor = Color.Tomato;

                    if (currentSession.ElapsedSeconds % 60 == 0)
                    {
                        sessionTimer.Stop();
                        string message = $"Увага! Час для '{currentSession.ActivityName}' вичерпано!\nПланувалось {currentSession.PlannedMinutes} хв.";

                        TopMost = true;
                        using (CustomAlertForm alert = new CustomAlertForm(message, currentThemeColor))
                        {
                            alert.ShowDialog();
                        }
                        TopMost = false;
                        sessionTimer.Start();
                    }
                }
                else
                {
                    lblTimerStatus.ForeColor = currentThemeColor;
                }

                int totalSeconds = currentSession.PlannedMinutes * 60;
                int max_width = 300;

                if (totalSeconds > 0 && currentSession.ElapsedSeconds <= totalSeconds)
                {
                    double progressPercent = (double)currentSession.ElapsedSeconds / totalSeconds;
                    panelProgressFill.Width = (int)(max_width * progressPercent);
                }
            }
        }

        private void cmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbTheme.SelectedIndex)
            {
                case 0: ApplyAccentColor(Color.FromArgb(46, 204, 113)); break;
                case 1: ApplyAccentColor(Color.FromArgb(52, 152, 219)); break;
                case 2: ApplyAccentColor(Color.FromArgb(155, 89, 182)); break;
                case 3: ApplyAccentColor(Color.FromArgb(231, 76, 60)); break;
            }
        }

        private void btnPresetRead_Click(object sender, EventArgs e)
        {
            if (isPatchUnlocked)
            {
                cmbActivity.Text = "Секретне відео";
                numPlannedMinutes.Value = 45;
                if (!isSessionActive) btnStartSession.PerformClick();
            }
            else
            {
                cmbActivity.Text = "Серіали / YouTube";
                numPlannedMinutes.Value = 60;
                if (!isSessionActive) btnStartSession.PerformClick();
            }
        }

        private void btnPresetCode_Click(object sender, EventArgs e)
        {
            if (isPatchUnlocked)
            {
                // таймер
                cmbActivity.Text = "Організація модів";
                numPlannedMinutes.Value = 20;
                if (!isSessionActive) btnStartSession.PerformClick();

                // відкриття папки
                System.Diagnostics.Process.Start("explorer.exe", @"Mods\");
            }
            else
            {
                cmbActivity.Text = "Програмування / Навчання";
                numPlannedMinutes.Value = 25;
                if (!isSessionActive) btnStartSession.PerformClick();
            }
        }

        private void btnPresetGame_Click_1(object sender, EventArgs e)
        {
            if (isPatchUnlocked)
            {
                // таймер
                cmbActivity.Text = "Перегляд галереї";
                numPlannedMinutes.Value = 30;
                if (!isSessionActive) btnStartSession.PerformClick();

                // галерея
                using (LibraryForm library = new LibraryForm())
                {
                    library.ShowDialog();
                }
            }
            else
            {
                cmbActivity.Text = "Brawl Stars / Ігри";
                numPlannedMinutes.Value = 40;
                if (!isSessionActive) btnStartSession.PerformClick();
            }
        }

        private void btnOpenStatistics_Click(object sender, EventArgs e)
        {
            using (StatisticsForm statForm = new StatisticsForm())
            {
                statForm.ShowDialog();
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            string patchFile = @"Mods\unlock.dat";

            if (File.Exists(patchFile))
            {
                string secretWord = File.ReadAllText(patchFile).Trim();

                if (secretWord == "vM8oqS2sdY6ftI7xbEplX0qnL9biG5")
                {
                    if (isSessionActive) btnStartSession.PerformClick();

                    isPatchUnlocked = !isPatchUnlocked;
                    UpdateModeUI();
                }
            }
        }

        private void UpdateModeUI()
        {
            if (isPatchUnlocked)
            {
                // 2 режим
                string bgImage = @"Mods\secret_bg.jpg";
                if (File.Exists(bgImage))
                {
                    this.BackgroundImage = Image.FromFile(bgImage);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }

                label1.Text = "R18Mod";
                label1.BackColor = Color.DeepPink;
                label1.ForeColor = Color.White;
                if (label1.Location.X < 140) label1.Location = new Point(label1.Location.X + 130, label1.Location.Y);

                ApplyAccentColor(Color.DeepPink);
                cmbTheme.Visible = false;

                btnPresetGame.Text = "Галерея";
                btnPresetRead.Text = "Відео";
                btnPresetCode.Text = "Моди";

                //новий список активностей
                cmbActivity.Items.Clear();
                cmbActivity.Items.AddRange(new string[] { "Перегляд галереї", "Секретне відео", "Організація модів" });
                if (cmbActivity.Items.Count > 0) cmbActivity.SelectedIndex = 0;

                DatabaseManager.SetDatabaseMode(true);
            }
            else
            {
                // 1 режим
                this.BackgroundImage = null;

                label1.Text = "FocusGuard";
                label1.BackColor = Color.FromArgb(33, 33, 33);
                label1.ForeColor = Color.White;
                if (label1.Location.X > 130) label1.Location = new Point(label1.Location.X - 130, label1.Location.Y);

                ApplyAccentColor(Color.FromArgb(46, 204, 113));
                cmbTheme.Visible = true;

                btnPresetGame.Text = "🕹 Brawl Stars";
                btnPresetRead.Text = "📚 Читати мангу";
                btnPresetCode.Text = "💻 Кодинг";

                // звичайні активності
                cmbActivity.Items.Clear();
                cmbActivity.Items.AddRange(new string[] { "Brawl Stars / Ігри", "Серіали / YouTube", "Програмування / Навчання" });
                if (cmbActivity.Items.Count > 0) cmbActivity.SelectedIndex = 0;

                DatabaseManager.SetDatabaseMode(false);
            }
        }
    }
}