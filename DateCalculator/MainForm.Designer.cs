namespace DateCalculator
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.calcButton = new System.Windows.Forms.Button();
            this.daysLabel = new System.Windows.Forms.Label();
            this.businessDays = new System.Windows.Forms.Label();
            this.daysText = new System.Windows.Forms.Label();
            this.businessDaysText = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.休日設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.businessDayWithPaidHoliday = new System.Windows.Forms.Label();
            this.businessDayWithHoliday = new System.Windows.Forms.Label();
            this.businessDayWithHolidayText = new System.Windows.Forms.Label();
            this.businessDayWithPaidHolidayText = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(172, 30);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(142, 19);
            this.startDatePicker.TabIndex = 0;
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(172, 55);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(142, 19);
            this.endDatePicker.TabIndex = 1;
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Location = new System.Drawing.Point(123, 35);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(41, 12);
            this.startDateLabel.TabIndex = 8;
            this.startDateLabel.Text = "開始日";
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Location = new System.Drawing.Point(123, 60);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(41, 12);
            this.endDateLabel.TabIndex = 9;
            this.endDateLabel.Text = "終了日";
            // 
            // calcButton
            // 
            this.calcButton.Location = new System.Drawing.Point(172, 91);
            this.calcButton.Name = "calcButton";
            this.calcButton.Size = new System.Drawing.Size(75, 23);
            this.calcButton.TabIndex = 2;
            this.calcButton.Text = "計算";
            this.calcButton.UseVisualStyleBackColor = true;
            this.calcButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // daysLabel
            // 
            this.daysLabel.AutoSize = true;
            this.daysLabel.Location = new System.Drawing.Point(135, 134);
            this.daysLabel.Name = "daysLabel";
            this.daysLabel.Size = new System.Drawing.Size(29, 12);
            this.daysLabel.TabIndex = 10;
            this.daysLabel.Text = "日数";
            // 
            // businessDays
            // 
            this.businessDays.AutoSize = true;
            this.businessDays.Location = new System.Drawing.Point(42, 162);
            this.businessDays.Name = "businessDays";
            this.businessDays.Size = new System.Drawing.Size(122, 12);
            this.businessDays.TabIndex = 11;
            this.businessDays.Text = "営業日（土日のみ考慮）";
            // 
            // daysText
            // 
            this.daysText.AutoSize = true;
            this.daysText.Location = new System.Drawing.Point(170, 134);
            this.daysText.Name = "daysText";
            this.daysText.Size = new System.Drawing.Size(52, 12);
            this.daysText.TabIndex = 3;
            this.daysText.Text = "daysText";
            // 
            // businessDaysText
            // 
            this.businessDaysText.AutoSize = true;
            this.businessDaysText.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.businessDaysText.Location = new System.Drawing.Point(170, 158);
            this.businessDaysText.Name = "businessDaysText";
            this.businessDaysText.Size = new System.Drawing.Size(126, 16);
            this.businessDaysText.TabIndex = 4;
            this.businessDaysText.Text = "businessDaysText";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.設定ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修了ToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // 修了ToolStripMenuItem
            // 
            this.修了ToolStripMenuItem.Name = "修了ToolStripMenuItem";
            this.修了ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.修了ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.修了ToolStripMenuItem.Text = "終了";
            this.修了ToolStripMenuItem.Click += new System.EventHandler(this.修了ToolStripMenuItem_Click);
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.休日設定ToolStripMenuItem});
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.設定ToolStripMenuItem.Text = "設定";
            // 
            // 休日設定ToolStripMenuItem
            // 
            this.休日設定ToolStripMenuItem.Name = "休日設定ToolStripMenuItem";
            this.休日設定ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.休日設定ToolStripMenuItem.Text = "休日設定";
            this.休日設定ToolStripMenuItem.Click += new System.EventHandler(this.休日設定ToolStripMenuItem_Click);
            // 
            // businessDayWithPaidHoliday
            // 
            this.businessDayWithPaidHoliday.AutoSize = true;
            this.businessDayWithPaidHoliday.Location = new System.Drawing.Point(7, 206);
            this.businessDayWithPaidHoliday.Name = "businessDayWithPaidHoliday";
            this.businessDayWithPaidHoliday.Size = new System.Drawing.Size(157, 12);
            this.businessDayWithPaidHoliday.TabIndex = 13;
            this.businessDayWithPaidHoliday.Text = "営業日（さらに他の休日も考慮）";
            // 
            // businessDayWithHoliday
            // 
            this.businessDayWithHoliday.AutoSize = true;
            this.businessDayWithHoliday.Location = new System.Drawing.Point(42, 184);
            this.businessDayWithHoliday.Name = "businessDayWithHoliday";
            this.businessDayWithHoliday.Size = new System.Drawing.Size(122, 12);
            this.businessDayWithHoliday.TabIndex = 12;
            this.businessDayWithHoliday.Text = "営業日（祝祭日も考慮）";
            // 
            // businessDayWithHolidayText
            // 
            this.businessDayWithHolidayText.AutoSize = true;
            this.businessDayWithHolidayText.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.businessDayWithHolidayText.Location = new System.Drawing.Point(170, 180);
            this.businessDayWithHolidayText.Name = "businessDayWithHolidayText";
            this.businessDayWithHolidayText.Size = new System.Drawing.Size(196, 16);
            this.businessDayWithHolidayText.TabIndex = 5;
            this.businessDayWithHolidayText.Text = "businessDayWithHolidayText";
            // 
            // businessDayWithPaidHolidayText
            // 
            this.businessDayWithPaidHolidayText.AutoSize = true;
            this.businessDayWithPaidHolidayText.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.businessDayWithPaidHolidayText.Location = new System.Drawing.Point(170, 202);
            this.businessDayWithPaidHolidayText.Name = "businessDayWithPaidHolidayText";
            this.businessDayWithPaidHolidayText.Size = new System.Drawing.Size(225, 16);
            this.businessDayWithPaidHolidayText.TabIndex = 6;
            this.businessDayWithPaidHolidayText.Text = "businessDayWithPaidHolidayText";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.businessDayWithPaidHolidayText);
            this.Controls.Add(this.businessDayWithHolidayText);
            this.Controls.Add(this.businessDayWithHoliday);
            this.Controls.Add(this.businessDayWithPaidHoliday);
            this.Controls.Add(this.businessDaysText);
            this.Controls.Add(this.daysText);
            this.Controls.Add(this.businessDays);
            this.Controls.Add(this.daysLabel);
            this.Controls.Add(this.calcButton);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "DateCalculator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.Button calcButton;
        private System.Windows.Forms.Label daysLabel;
        private System.Windows.Forms.Label businessDays;
        private System.Windows.Forms.Label daysText;
        private System.Windows.Forms.Label businessDaysText;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 休日設定ToolStripMenuItem;
        private System.Windows.Forms.Label businessDayWithPaidHoliday;
        private System.Windows.Forms.Label businessDayWithHoliday;
        private System.Windows.Forms.Label businessDayWithHolidayText;
        private System.Windows.Forms.Label businessDayWithPaidHolidayText;
    }
}

