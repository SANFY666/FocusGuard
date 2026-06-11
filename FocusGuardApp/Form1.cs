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
            btnStartSession.BackColor = newColor;
            btnStartSession.ForeColor = Color.FromArgb(33, 33, 33);
            panelProgressFill.BackColor = newColor;
        }

        private void BtnStartSession_Click(object sender, EventArgs e)
        {
            if (!isSessionActive)
            {
                // старт
                string activity = cmbActivity.SelectedItem.ToString();
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

                // лінія 0
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
    }
}