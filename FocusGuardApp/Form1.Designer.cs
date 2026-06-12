namespace FocusGuardApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.sessionTimer = new System.Windows.Forms.Timer(this.components);
            this.cmbTheme = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPresetGame = new System.Windows.Forms.Button();
            this.btnPresetRead = new System.Windows.Forms.Button();
            this.btnPresetCode = new System.Windows.Forms.Button();
            this.btnOpenStatistics = new System.Windows.Forms.Button();
            this.roundedPanel1 = new FocusGuardApp.RoundedPanel();
            this.lblTimerStatus = new System.Windows.Forms.Label();
            this.btnStartSession = new System.Windows.Forms.Button();
            this.numPlannedMinutes = new System.Windows.Forms.NumericUpDown();
            this.cmbActivity = new System.Windows.Forms.ComboBox();
            this.panelProgressBg = new System.Windows.Forms.Panel();
            this.panelProgressFill = new System.Windows.Forms.Panel();
            this.roundedPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPlannedMinutes)).BeginInit();
            this.panelProgressBg.SuspendLayout();
            this.SuspendLayout();
            // 
            // sessionTimer
            // 
            this.sessionTimer.Interval = 1000;
            // 
            // cmbTheme
            // 
            this.cmbTheme.FormattingEnabled = true;
            this.cmbTheme.Items.AddRange(new object[] {
            "Зелена тема",
            "Синя тема",
            "Фіолетова тема",
            "Червона тема"});
            this.cmbTheme.Location = new System.Drawing.Point(307, 15);
            this.cmbTheme.Name = "cmbTheme";
            this.cmbTheme.Size = new System.Drawing.Size(118, 25);
            this.cmbTheme.TabIndex = 6;
            this.cmbTheme.SelectedIndexChanged += new System.EventHandler(this.cmbTheme_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(28, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "FocusGuard";
            // 
            // btnPresetGame
            // 
            this.btnPresetGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPresetGame.FlatAppearance.BorderSize = 0;
            this.btnPresetGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetGame.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPresetGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnPresetGame.Location = new System.Drawing.Point(35, 324);
            this.btnPresetGame.Margin = new System.Windows.Forms.Padding(4);
            this.btnPresetGame.Name = "btnPresetGame";
            this.btnPresetGame.Size = new System.Drawing.Size(124, 36);
            this.btnPresetGame.TabIndex = 6;
            this.btnPresetGame.Text = "🕹 Brawl Stars";
            this.btnPresetGame.UseVisualStyleBackColor = false;
            this.btnPresetGame.Click += new System.EventHandler(this.btnPresetGame_Click_1);
            // 
            // btnPresetRead
            // 
            this.btnPresetRead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPresetRead.FlatAppearance.BorderSize = 0;
            this.btnPresetRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetRead.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPresetRead.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnPresetRead.Location = new System.Drawing.Point(277, 324);
            this.btnPresetRead.Margin = new System.Windows.Forms.Padding(4);
            this.btnPresetRead.Name = "btnPresetRead";
            this.btnPresetRead.Size = new System.Drawing.Size(148, 36);
            this.btnPresetRead.TabIndex = 7;
            this.btnPresetRead.Text = "📚 Читати мангу";
            this.btnPresetRead.UseVisualStyleBackColor = false;
            this.btnPresetRead.Click += new System.EventHandler(this.btnPresetRead_Click);
            // 
            // btnPresetCode
            // 
            this.btnPresetCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPresetCode.FlatAppearance.BorderSize = 0;
            this.btnPresetCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetCode.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPresetCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnPresetCode.Location = new System.Drawing.Point(167, 324);
            this.btnPresetCode.Margin = new System.Windows.Forms.Padding(4);
            this.btnPresetCode.Name = "btnPresetCode";
            this.btnPresetCode.Size = new System.Drawing.Size(102, 36);
            this.btnPresetCode.TabIndex = 8;
            this.btnPresetCode.Text = "💻 Кодинг";
            this.btnPresetCode.UseVisualStyleBackColor = false;
            this.btnPresetCode.Click += new System.EventHandler(this.btnPresetCode_Click);
            // 
            // btnOpenStatistics
            // 
            this.btnOpenStatistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnOpenStatistics.FlatAppearance.BorderSize = 0;
            this.btnOpenStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenStatistics.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOpenStatistics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnOpenStatistics.Location = new System.Drawing.Point(35, 368);
            this.btnOpenStatistics.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenStatistics.Name = "btnOpenStatistics";
            this.btnOpenStatistics.Size = new System.Drawing.Size(390, 36);
            this.btnOpenStatistics.TabIndex = 9;
            this.btnOpenStatistics.Text = "📊 Статистика";
            this.btnOpenStatistics.UseVisualStyleBackColor = false;
            this.btnOpenStatistics.Click += new System.EventHandler(this.btnOpenStatistics_Click);
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.roundedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.roundedPanel1.BorderRadius = 25;
            this.roundedPanel1.BorderThickness = 3;
            this.roundedPanel1.Controls.Add(this.lblTimerStatus);
            this.roundedPanel1.Controls.Add(this.btnStartSession);
            this.roundedPanel1.Controls.Add(this.numPlannedMinutes);
            this.roundedPanel1.Controls.Add(this.cmbActivity);
            this.roundedPanel1.Controls.Add(this.panelProgressBg);
            this.roundedPanel1.ForeColor = System.Drawing.Color.White;
            this.roundedPanel1.Location = new System.Drawing.Point(35, 62);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Size = new System.Drawing.Size(390, 240);
            this.roundedPanel1.TabIndex = 6;
            // 
            // lblTimerStatus
            // 
            this.lblTimerStatus.AutoSize = true;
            this.lblTimerStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTimerStatus.Location = new System.Drawing.Point(29, 62);
            this.lblTimerStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimerStatus.Name = "lblTimerStatus";
            this.lblTimerStatus.Size = new System.Drawing.Size(152, 21);
            this.lblTimerStatus.TabIndex = 3;
            this.lblTimerStatus.Text = "Очікування старту...";
            // 
            // btnStartSession
            // 
            this.btnStartSession.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnStartSession.FlatAppearance.BorderSize = 0;
            this.btnStartSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartSession.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStartSession.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnStartSession.Location = new System.Drawing.Point(24, 173);
            this.btnStartSession.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartSession.Name = "btnStartSession";
            this.btnStartSession.Size = new System.Drawing.Size(344, 36);
            this.btnStartSession.TabIndex = 2;
            this.btnStartSession.Text = "Почати активність";
            this.btnStartSession.UseVisualStyleBackColor = false;
            // 
            // numPlannedMinutes
            // 
            this.numPlannedMinutes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numPlannedMinutes.Location = new System.Drawing.Point(303, 23);
            this.numPlannedMinutes.Margin = new System.Windows.Forms.Padding(4);
            this.numPlannedMinutes.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.numPlannedMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPlannedMinutes.Name = "numPlannedMinutes";
            this.numPlannedMinutes.Size = new System.Drawing.Size(65, 25);
            this.numPlannedMinutes.TabIndex = 1;
            this.numPlannedMinutes.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // cmbActivity
            // 
            this.cmbActivity.BackColor = System.Drawing.Color.White;
            this.cmbActivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActivity.FormattingEnabled = true;
            this.cmbActivity.Items.AddRange(new object[] {
            "Brawl Stars / Ігри",
            "Перегляд серіалів / YouTube",
            "Програмування / Навчання",
            "Відпочинок"});
            this.cmbActivity.Location = new System.Drawing.Point(24, 23);
            this.cmbActivity.Margin = new System.Windows.Forms.Padding(4);
            this.cmbActivity.Name = "cmbActivity";
            this.cmbActivity.Size = new System.Drawing.Size(271, 25);
            this.cmbActivity.TabIndex = 0;
            // 
            // panelProgressBg
            // 
            this.panelProgressBg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelProgressBg.Controls.Add(this.panelProgressFill);
            this.panelProgressBg.Location = new System.Drawing.Point(24, 154);
            this.panelProgressBg.Name = "panelProgressBg";
            this.panelProgressBg.Size = new System.Drawing.Size(344, 3);
            this.panelProgressBg.TabIndex = 5;
            // 
            // panelProgressFill
            // 
            this.panelProgressFill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.panelProgressFill.Location = new System.Drawing.Point(0, 0);
            this.panelProgressFill.Name = "panelProgressFill";
            this.panelProgressFill.Size = new System.Drawing.Size(344, 3);
            this.panelProgressFill.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(455, 423);
            this.Controls.Add(this.btnOpenStatistics);
            this.Controls.Add(this.btnPresetCode);
            this.Controls.Add(this.btnPresetRead);
            this.Controls.Add(this.btnPresetGame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbTheme);
            this.Controls.Add(this.roundedPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPlannedMinutes)).EndInit();
            this.panelProgressBg.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbActivity;
        private System.Windows.Forms.NumericUpDown numPlannedMinutes;
        private System.Windows.Forms.Button btnStartSession;
        private System.Windows.Forms.Label lblTimerStatus;
        private System.Windows.Forms.Timer sessionTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelProgressBg;
        private System.Windows.Forms.Panel panelProgressFill;
        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.ComboBox cmbTheme;
        private System.Windows.Forms.Button btnPresetGame;
        private System.Windows.Forms.Button btnPresetRead;
        private System.Windows.Forms.Button btnPresetCode;
        private System.Windows.Forms.Button btnOpenStatistics;
    }
}

