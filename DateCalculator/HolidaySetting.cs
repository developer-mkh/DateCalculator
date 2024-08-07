using DateCalculator.db;
using DateCalculator.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DateCalculator
{
    public partial class HolidaySetting : Form
    {
        public HolidaySetting()
        {
            InitializeComponent();

            DBAccess dBAccess = new DBAccess();
            dataGridView.DataSource = dBAccess.GetHolidays();

            dataGridView.Columns["id"].Visible = false;
            dataGridView.Columns["date"].HeaderText = "年月日";
            dataGridView.Columns["name"].HeaderText = "休日名";
            dataGridView.Columns["category"].Visible = false;

            // カテゴリー部分はコンボボックスで表示する
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.DataPropertyName = dataGridView.Columns["category"].DataPropertyName;
            column.DataSource = dBAccess.GetCategories();
            column.ValueMember = "value";
            column.DisplayMember = "name";
            column.HeaderText = "カテゴリー";
            dataGridView.Columns.Add(column);
        }
        private void ok_Click(object sender, EventArgs e)
        {
            DBAccess dBAccess = new DBAccess();
            dBAccess.SetData((DataTable)dataGridView.DataSource);
            Close();
        }

        private void copy_Click(object sender, EventArgs e)
        {
            DBAccess dBAccess = new DBAccess();

            List<Holiday> holidays = new List<Holiday>();
            DataGridViewSelectedRowCollection selectedRows = dataGridView.SelectedRows;
            foreach (DataGridViewRow row in selectedRows) {
                DataGridViewCellCollection cells = row.Cells;
                Holiday holiday = new Holiday();
                holiday.name = cells["name"].Value.ToString();
                holiday.date = DateTime.ParseExact(cells["date"].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                holiday.category = cells["category"].Value.ToString();
                holidays.Add(holiday);
            }

            dBAccess.SetData((DataTable)dataGridView.DataSource);
            dBAccess.AddHolidays(holidays);
            dataGridView.DataSource = dBAccess.GetHolidays();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DBAccess dBAccess = new DBAccess();

            List<Holiday> holidays = new List<Holiday>();
            DataGridViewSelectedRowCollection selectedRows = dataGridView.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                DataGridViewCellCollection cells = row.Cells;
                Holiday holiday = new Holiday();
                holiday.id = int.Parse(cells["id"].Value.ToString());
                holidays.Add(holiday);
            }

            dBAccess.SetData((DataTable)dataGridView.DataSource);
            dBAccess.DeleteHolidays(holidays);
            dataGridView.DataSource = dBAccess.GetHolidays();
        }
    }
}
