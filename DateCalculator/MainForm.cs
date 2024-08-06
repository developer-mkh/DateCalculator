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

        private void Button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = startDatePicker.Value.Date;
            DateTime endDate = endDatePicker.Value.Date;
 
            int days = endDate.Subtract(startDate).Days + 1;

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

            List<Holiday> holidays = dBAccess.GetHolidays(Holiday);
            List<Holiday> paidHolidays = dBAccess.GetHolidays(PaidHoliday);

            for(int i = holidays.Count - 1; i >= 0; i--)
            {
                if (!IsWorkDay(holidays[i].date, startDate, endDate))
                {
                    holidays.RemoveAt(i);
                }
            }
            for (int i = paidHolidays.Count - 1; i >= 0; i--)
            {
                if (!IsWorkDay(paidHolidays[i].date, startDate, endDate))
                {
                    paidHolidays.RemoveAt(i);
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

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 与えられた日が、与えられた期間内の平日かどうかを判定する。
        /// 期間の両端は期間内に含む。
        /// </summary>
        /// <param name="target">判定したい日</param>
        /// <param name="startDate">期間の開始日</param>
        /// <param name="endDate">期間の終了日</param>
        /// <returns>判定したい日が期間内の平日であればTrue</returns>
        private bool IsWorkDay(DateTime target, DateTime startDate, DateTime endDate)
        {
            return (startDate <= target && target <= endDate && target.DayOfWeek != DayOfWeek.Saturday && target.DayOfWeek != DayOfWeek.Sunday);
        }
    }
}
