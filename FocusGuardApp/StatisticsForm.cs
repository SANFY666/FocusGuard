using System;
using System.Data;
using System.Windows.Forms;

namespace FocusGuardApp
{
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();
            RefreshStatistics(); // завантаження данних
        }

        // оновлення статистики
        private void RefreshStatistics()
        {
            try
            {
                DataTable data = DatabaseManager.GetAllSessions();
                dgvStatistics.DataSource = data;

                // загальний час
                int totalMinutes = 0;
                foreach (DataRow row in data.Rows)
                {
                    totalMinutes += Convert.ToInt32(row["Час (хв)"]);
                }
                this.Text = $"Історія активності  |  Загалом часу в фокусі: {totalMinutes} хв";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося завантажити статистику: " + ex.Message);
            }
        }

        // очистити історію
        private void очиститиІсторіюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // попередження
            DialogResult result = MessageBox.Show(
                "Ти точно хочеш видалити всю історію? Цю дію неможливо скасувати.",
                "Попередження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            // так
            if (result == DialogResult.Yes)
            {
                DatabaseManager.ClearAllSessions(); 
                RefreshStatistics(); 
                MessageBox.Show(
                    "Історію успішно видалено!",
                    "Успіх",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}