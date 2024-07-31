using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CMPG223_Group22_Project
{
    public partial class Staff : Form
    {
        public String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\CMPG223\CMPG223_Group22_Project\CMPG223_Group22_Project\Database1.mdf;Integrated Security=True";
        public SqlConnection conn;
        public SqlCommand comm;
        public SqlDataReader reader;
        public SqlDataAdapter adapter;
        public DataSet ds;

        public String DOB { get; set; }
        public String ContactNo { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String date { get; set; }
        public int price { get; set; }

        public Staff()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        public const int APRICE = 120;
        public const int PPRICE = 70;
        public const int CPRICE = 65;

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart;

            int day = selectedDate.Day;
            int month = selectedDate.Month;
            int year = selectedDate.Year;

            lblDate.Text = $"Selected Date: {day}/{month}/{year}";
            date = lblDate.Text;
        }

        public void ShowDatabase()
        {
            try
            {
                string sql = "SELECT * FROM VISITORS";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();

                comm = new SqlCommand(sql, conn);

                adapter.SelectCommand = comm;
                adapter.Fill(ds, "Visitors");

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Visitors";

                // Closing the connection to the database
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void ShowDatabaseT()
        {
            try
            {
                string sql = "SELECT * FROM TICKETS";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();

                comm = new SqlCommand(sql, conn);

                adapter.SelectCommand = comm;
                adapter.Fill(ds, "Tickets");

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Tickets";

                // Closing the connection to the database
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DOB = txtAge.Text;
            ContactNo = txtContactNo.Text;
            Name = txtName.Text;
            Surname = txtSurname.Text;
            try
            {


                if (txtAge.Text != "" && txtContactNo.Text != "" && txtName.Text != "" && txtSurname.Text != "")
                {
                    String insert_query = $"INSERT INTO VISITORS VALUES('{Surname}','{Name}','{DOB}','{ContactNo}') ";
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    adapter = new SqlDataAdapter();
                    comm = new SqlCommand(insert_query, conn);
                    adapter.InsertCommand = comm;
                    adapter.InsertCommand.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Please enter all of the values");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR!! " + ex.Message);
            }
            ShowDatabase();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (txtDesc.Text == "Adult")
            {
                lblShowPrice.Text = (numericUpDown1.Value * APRICE).ToString();
            }
            else if (txtDesc.Text == "Pensioner")
            {
                lblShowPrice.Text = (numericUpDown1.Value * PPRICE).ToString();
            }
            else if (txtDesc.Text == "Child")
            {
                lblShowPrice.Text = (numericUpDown1.Value * CPRICE).ToString();
            }
            price = int.Parse(lblShowPrice.Text);
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            try
            {
                String insert_query = $"INSERT INTO TICKETS(Description,Price,isTicketValid) VALUES('{txtDesc.Text}',{lblShowPrice.Text},'{1}') ";
                if (txtDesc.Text != "" && lblShowPrice.Text != "")
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    adapter = new SqlDataAdapter();
                    comm = new SqlCommand(insert_query, conn);
                    adapter.InsertCommand = comm;
                    adapter.InsertCommand.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Please enter all of the values");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR!! " + ex.Message);
            }
            ShowDatabaseT();
        }

        private void Staff_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            MessageBox.Show("Please enter 'Adult' or 'Pensioner ' or 'Child'");
        }
    }
}
