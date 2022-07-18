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

namespace DateCalculator
{
    public partial class HolidaySetting : Form
    {
        public HolidaySetting()
        {
            InitializeComponent();

            DBAccess dBAccess = new DBAccess();
            dataGridView.DataSource = dBAccess.GetData();

            dataGridView.Columns["id"].Visible = false;
            dataGridView.Columns["date"].HeaderText = "年月日";
            dataGridView.Columns["name"].HeaderText = "休日名";
            dataGridView.Columns["category"].HeaderText = "カテゴリー値";
            dataGridView.Columns["name1"].HeaderText = "カテゴリー名称";
            dataGridView.Columns["name1"].ReadOnly = true;
        }
        private void ok_Click(object sender, EventArgs e)
        {
            DBAccess dBAccess = new DBAccess();
            dBAccess.SetData((DataTable)dataGridView.DataSource);
            Close();
        }
    }
}
