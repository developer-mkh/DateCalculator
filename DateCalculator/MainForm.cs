using System;
using System.Windows.Forms;
using DateCalculator.db;
using DateCalculator.dto;
using System.Collections.Generic;

namespace DateCalculator
{
    public partial class MainForm : Form
    {
        private int Holiday = 1;
        private int PaidHoliday = 2;

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

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = startDatePicker.Value;
            DateTime endDate = endDatePicker.Value;

            int days = endDate.Subtract(startDate).Days;

            int businessDays = 1 + (days * 5 - (startDate.DayOfWeek - endDate.DayOfWeek) * 2) / 7;
            if (endDate.DayOfWeek == DayOfWeek.Saturday)
            {
                businessDays--;
            }
            if (startDate.DayOfWeek == DayOfWeek.Sunday)
            {
                businessDays--;
            }

            DBAccess dBAccess = new DBAccess();

            List<Holiday> holidays = dBAccess.getHolidaysCount(Holiday);
            List<Holiday> paidHolidays = dBAccess.getHolidaysCount(PaidHoliday);

            foreach(Holiday holiday in holidays)
            {
                if (startDate <= holiday.date && holiday.date <= endDate)
                {
                    holidays.Remove(holiday);
                }
            }
            foreach (Holiday holiday in paidHolidays)
            {
                if (startDate <= holiday.date && holiday.date <= endDate)
                {
                    paidHolidays.Remove(holiday);
                }
            }

            int businessDaysWithHolidays = businessDays - holidays.Count;
            int businessDaysWithPaidHolidays = businessDaysWithHolidays - paidHolidays.Count;

            daysText.Text = days.ToString();
            businessDaysText.Text = businessDays.ToString();     
            businessDayWithHolidayText.Text = businessDaysWithHolidays.ToString();
            businessDayWithPaidHolidayText.Text = businessDaysWithPaidHolidays.ToString();
        }

        private void 休日設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new HolidaySetting().ShowDialog();
        }

        private void 修了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
