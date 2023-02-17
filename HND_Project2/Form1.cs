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
                string sex;

                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Emp_Table VALUES(@eNo,@eName, @Category,@dob, @Gender, @Salary )", con);
                command.Parameters.AddWithValue("@eNO", eNo.Text);
                command.Parameters.AddWithValue("@eName", eName.Text);
                command.Parameters.AddWithValue("@Category", category.Text);
                command.Parameters.AddWithValue("@dob", dob.Value);
                command.Parameters.AddWithValue("@Salary", salary.Text);
                if (male.Checked) sex = "M"; else sex = "F";
                command.Parameters.AddWithValue("@Gender", sex);
                command.ExecuteNonQuery();
                MessageBox.Show("New Record Added", "Database Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                form_clear();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void edit_Employee()
        {
            try
            {
                string sex;

                con.Open();
                SqlCommand command = new SqlCommand("UPDATE Employee SET EmpName=@Name, EmpCategory=@Category,EmpDob = @Dob, EmpGender=@Gender, EmpSalary=@Salary WHERE EmpNO=@eNo )", con);
                command.Parameters.AddWithValue("@eNO", eNo.Text);
                command.Parameters.AddWithValue("@eName", eName.Text);
                command.Parameters.AddWithValue("@Category", category.Text);
                command.Parameters.AddWithValue("@dob", dob.Value);
                command.Parameters.AddWithValue("@Salary", salary.Text);
                if (male.Checked) sex = "M"; else sex = "F";
                command.Parameters.AddWithValue("@Gender", sex);
                command.ExecuteNonQuery();
                MessageBox.Show("New Record Added", "Database Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                form_clear();
                con.Close();
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
