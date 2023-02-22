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
                SqlCommand command = new SqlCommand("INSERT INTO Employee VALUES(@eNo,@eName, @Category,@dob, @Gender, @Salary )", con);
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
                MessageBox.Show("Record updated", "Database Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                form_clear();
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void load_employye()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Employee WHERE EmpNo=@eNo", con);
                command.Parameters.AddWithValue("@eNo", eNo.Text);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    eName.Text = reader[0].ToString();
                    category.Text = reader[2].ToString();
                    dob.Text = reader[3].ToString();
                    if (reader[4].Equals("M"))
                    {
                        male.Checked = false;
                    }
                    else
                    {
                        female.Checked = true;

                    }
                    salary.Text = reader[5].ToString();
                    addBtn.Text = "Edit";
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Employee Not Found");

                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void eNo_Leave(object sender, EventArgs e)
        {
            load_employye();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            form_clear();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (addBtn.Text == "Add")
            {
                addEmployee();
            }
            else
            {
                edit_Employee();
            }
        }
    }
}
