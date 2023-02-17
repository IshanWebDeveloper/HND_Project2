using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HND_Project2
{
    public partial class Form1 : Form

    {

        static readonly string con_string = "Data Source=ISHAN-PC\\SQLEXPRESS;Initial Catalog=ESOFT;Integrated Security=true";
        SqlConnection con = new SqlConnection(con_string);
        public Form1()
        {



            InitializeComponent();
            fillComboBox();
        }

        private void fillComboBox()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Category FROM Category", con_string);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            category.DataSource = dt;
            category.DisplayMember = "Category";
            category.ValueMember = "Category";
        }

        private void form_clear()
        {
            eNo.Text = "";
            eName.Text = "";
            category.ResetText();
            dob.ResetText();
            male.Checked = false;
            female.Checked = false;
            salary.Text = string.Empty;
            addBtn.Text = "Add";
        }

        private void addEmployee()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Emp_Table VALUES(@eNo,@eName, @Category )", con);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
