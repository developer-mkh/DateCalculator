using DateCalculator.Db;
using DateCalculator.Entity;
using DateCalculator.Service;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DateCalculator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            daysText.Text = "";
            businessDaysText.Text = "";
            businessDayWithHolidayText.Text = "";
            businessDayWithPaidHolidayText.Text = "";

            DBAccess dBAccess = new DBAccess();
            dBAccess.CreateTable();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = startDatePicker.Value.Date;
            DateTime endDate = endDatePicker.Value.Date;

            DBAccess dBAccess = new DBAccess();
            CalcService service = new CalcService(dBAccess);

            int days = service.CalcDays(startDate, endDate);
            int businessDays = service.CalcBusinessDays(startDate, endDate, days);
            int businessDaysWithHolidays = service.CalcBusinessDaysWithHolidays(startDate, endDate, businessDays);
            int businessDaysWithPaidHolidays = service.CalcBusinessDaysWithPaidHolidays(startDate, endDate, businessDaysWithHolidays);

            daysText.Text = days.ToString();
            businessDaysText.Text = businessDays.ToString();
            businessDayWithHolidayText.Text = businessDaysWithHolidays.ToString();
            businessDayWithPaidHolidayText.Text = businessDaysWithPaidHolidays.ToString();
        }

        private void 休日設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new HolidaySetting().ShowDialog();
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
