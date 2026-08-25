using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        NpgsqlConnection conn = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //postgresqlLib.SelectDB();
            string connString = string.Format("Host={0};Database={1};Username ={2};Password={3};", "127.0.0.1",
    "postgres", "postgres", "postgres");

            DataTable dt = new DataTable();

            // 테스트용 테이블 생성 및 데이터 조회 쿼리
            // (실제 사용 시에는 기존에 존재하는 테이블명을 넣으시면 됩니다)
            string query = "SELECT column1, column2, column3 FROM schema1.table1";

            conn = new NpgsqlConnection(connString);
            var cmd = new NpgsqlCommand(query, conn);
            var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.DataSource is DataTable dt)
            {
                int rowCnt = dt.Rows.Count;
            }
            else
            {
                return;
            }

            DataRow dr = null;
            string x = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                if (dr == null)
                {
                    return;
                }
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    x = GetRowString(dr, j);
                    textBox1.Text += x + ", ";
                }
                textBox1.Text += "\r\n";
            }
        }

        private string GetRowString(DataRow row, int colIdx)
        {
            if (row == null) return string.Empty;
            if (colIdx < 0 || colIdx >= row.ItemArray.Length)
            { return string.Empty; }

            object val = row[colIdx];
            if (val == null || val == DBNull.Value)
            { return string.Empty; }

            return val.ToString().Trim();

        }
    }
}
