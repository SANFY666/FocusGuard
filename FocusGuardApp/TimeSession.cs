using System;

namespace FocusGuardApp
{
    public class TimeSession
    {
        public string ActivityName { get; private set; }
        public int PlannedMinutes { get; private set; }
        public int ElapsedSeconds { get; private set; }

        public TimeSession(string activityName, int plannedMinutes)
        {
            ActivityName = activityName;
            PlannedMinutes = plannedMinutes;
            ElapsedSeconds = 0;
        }

        // збільшує час на 1 секунду
        public void Tick()
        {
            ElapsedSeconds++;
        }

        // ліміт
        public bool IsOverdue()
        {
            return ElapsedSeconds > (PlannedMinutes * 60);
        }

        public string GetElapsedFormatted()
        {
            TimeSpan time = TimeSpan.FromSeconds(ElapsedSeconds);
            if (time.TotalHours >= 1)
            {
                return $"{(int)time.TotalHours} год {time.Minutes} хв {time.Seconds} сек";
            }
            return $"{time.Minutes} хв {time.Seconds} сек";
        }

        public string GetSessionSummary()
        {
            string status = $"Активність: {ActivityName}\nПройшло: {GetElapsedFormatted()}\nПланувалось: {PlannedMinutes} хв.";

            if (IsOverdue())
            {
                TimeSpan overdueTime = TimeSpan.FromSeconds(ElapsedSeconds - (PlannedMinutes * 60));
                status += $"\n⚠️ ПЕРЕВИЩЕНО НА: {(int)overdueTime.TotalMinutes} хв {overdueTime.Seconds} сек!";
            }

            return status;
        }
    }
}