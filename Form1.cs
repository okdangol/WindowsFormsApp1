using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        // 데이터를 DataTable에 채웁니다.
                        adapter.Fill(dt);
                    }

                    if (dt != null)
                    {
                        DataRow dr = dt.Rows[0];
                        if (dr != null)
                        {
                            Console.WriteLine(dr["column1"].ToString());
                        }
                    }
                    DataTable dataTable = dt;

                    dataGridView1.DataSource = dataTable;

                    DataRow dataRow = dt.Rows[0];

                    string col1 = dataRow["column1"].ToString();

                    textBox1.Text = col1;
                }

            }

        }

    }
}
