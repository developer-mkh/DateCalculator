using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using DateCalculator.db;
using System.Linq.Expressions;

namespace DateCalculator
{
    public partial class HolidaySetting : Form
    {
        public HolidaySetting()
        {
            InitializeComponent();

            DBAccess dBAccess = new DBAccess();
            dataGridView.DataSource = dBAccess.GetHolidayData();

            dataGridView.Columns["id"].Visible = false;
            dataGridView.Columns["date"].HeaderText = "年月日";
            dataGridView.Columns["name"].HeaderText = "休日名";
            dataGridView.Columns["category"].Visible = false;

            // カテゴリー部分はコンボボックスで表示する
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.DataPropertyName = dataGridView.Columns["category"].DataPropertyName;
            column.DataSource = dBAccess.GetCategoryData();
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
    }
}
