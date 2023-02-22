using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HND_Project2
{
    public partial class Form2 : Form
    {
        static readonly string con_string = "Data Source=ISHAN-PC\\SQLEXPRESS;Initial Catalog=ESOFT;Integrated Security=true";
        SqlConnection con = new SqlConnection(con_string);
        public Form2()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // when click on a row, full row will be selected
            dataGridView1.MultiSelect = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT EmpName, Day(EmpDob) as Day, Year(EmpDob) as Year FROM Employee WHERE Month(EmpDob)=@month ORDER BY Day ", con);
                command.Parameters.AddWithValue("@month", comboBox1.SelectedIndex + 1);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

                da.Fill(dt);
                dataGridView2.Rows.Clear();
                int count = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    dataGridView2.Rows.Add(count.ToString(), dr["EmpName"].ToString(), dr["Day"].ToString(), dr["Year"].ToString());
                    count++;
                }
                con.Close();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
