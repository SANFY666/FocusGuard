using System;
using System.Drawing;
using System.Windows.Forms;

namespace FocusGuardApp
{
    public partial class Form1 : Form
    {
        private TimeSession currentSession;
        private bool isSessionActive = false;


        public Form1()
        {
            InitializeComponent();


            if (cmbActivity.Items.Count == 0)
            {
                cmbActivity.Items.AddRange(new string[] { "Brawl Stars / Ігри", "Серіали / YouTube", "Програмування / Навчання" });
            }
            cmbActivity.SelectedIndex = 0;

            btnStartSession.Click += BtnStartSession_Click;
            sessionTimer.Tick += SessionTimer_Tick;
        }

        private void BtnStartSession_Click(object sender, EventArgs e)
        {
            if (!isSessionActive)
            {
                // старт
                string activity = cmbActivity.SelectedItem.ToString();
                int plannedTime = (int)numPlannedMinutes.Value;

                // новий елемент
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

                // результат
                MessageBox.Show($"Активність завершено!\n\n{currentSession.GetSessionSummary()}", "Результати фокусу", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmbActivity.Enabled = true;
                numPlannedMinutes.Enabled = true;

                btnStartSession.Text = "Почати";
                btnStartSession.BackColor = Color.LightGreen;
                lblTimerStatus.Text = "Очікування старту...";
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
                    lblTimerStatus.ForeColor = Color.Red;

                    if (currentSession.ElapsedSeconds % 60 == 0)
                    {
                        TopMost = true;
                        MessageBox.Show($"Увага! Час для '{currentSession.ActivityName}' вичерпано! Планувалось {currentSession.PlannedMinutes} хв.", "FocusGuard Нагадування", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TopMost = false;
                    }
                }
                else
                {
                    lblTimerStatus.ForeColor = Color.Yellow;
                }

                // прогрес бар
                int totalSeconds = currentSession.PlannedMinutes * 60;
                int max_width = 300; 

                if (totalSeconds > 0 && currentSession.ElapsedSeconds <= totalSeconds)
                {
                    // відсоток завершення
                    double progressPercent = (double)currentSession.ElapsedSeconds / totalSeconds;

                    // ширина зеленої панелі
                    panelProgressFill.Width = (int)(max_width * progressPercent);
                }
            }
        }
    }
}