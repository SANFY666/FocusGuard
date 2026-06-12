using System;
using System.Drawing;
using System.Windows.Forms;

namespace FocusGuardApp
{
    public partial class Form1 : Form
    {
        private TimeSession currentSession;
        private bool isSessionActive = false;

        // колір зараз
        private Color currentThemeColor = Color.FromArgb(46, 204, 113);

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

                MessageBox.Show($"Помилка бази даних при старті:\n{errorMessage}", "Критична помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cmbTheme.SelectedIndex = 0; // зелена тема за замовчуванням

            if (cmbActivity.Items.Count == 0)
            {
                cmbActivity.Items.AddRange(new string[] { "Brawl Stars / Ігри", "Серіали / YouTube", "Програмування / Навчання" });
            }
            cmbActivity.SelectedIndex = 0;

            btnStartSession.Click += BtnStartSession_Click;
            sessionTimer.Tick += SessionTimer_Tick;
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

                // збереження данних у базу

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

                //інфо в базу
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

                // лінія
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
                case 0: // зелений
                    ApplyAccentColor(Color.FromArgb(46, 204, 113));
                    break;
                case 1: // синій
                    ApplyAccentColor(Color.FromArgb(52, 152, 219));
                    break;
                case 2: // фіолетовий
                    ApplyAccentColor(Color.FromArgb(155, 89, 182));
                    break;
                case 3: // червоний
                    ApplyAccentColor(Color.FromArgb(231, 76, 60));
                    break;
            }
        }

        private void btnPresetGame_Click_1(object sender, EventArgs e)
        {
            cmbActivity.Text = "Brawl Stars / Ігри";
            numPlannedMinutes.Value = 40; // 40 хвилин
            btnStartSession.PerformClick();
        }

        private void btnPresetRead_Click(object sender, EventArgs e)
        {
            cmbActivity.Text = "Серіали / YouTube";
            numPlannedMinutes.Value = 60;
            btnStartSession.PerformClick();
        }

        private void btnPresetCode_Click(object sender, EventArgs e)
        {
            cmbActivity.Text = "Програмування / Навчання";
            numPlannedMinutes.Value = 25; 
            btnStartSession.PerformClick();
        }

        private void btnOpenStatistics_Click(object sender, EventArgs e)
        {
            using (StatisticsForm statForm = new StatisticsForm())
            {
                statForm.ShowDialog();
            }
        }
    }
}