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
using System.Text.RegularExpressions; //via chatGPT to check for digits in a string

namespace CMPG223_Group22_Project
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// SQL components and relevant classes to access and work with databases, connection string also declared
        /// </summary>

        /// <relativepathnote>
        ///Attempt at creating a relative database path, but received error within terms of remote network, would sort out if given enough time :)
        ///string executable;
        ///string path;
        ///string connString = "Data Source=|DataDirectory|\\Database1.mdf";
        ///string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
        ///string path = System.IO.Path.GetDirectoryName(executable);
        ///AppDomain.CurrentDomain.SetData("DataDirectory", path);*/
        ///string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;Database=Databbase1.mdf;Integrated Security=True";
        /// </note>
        /// 
        //string connString = @"Data Source =(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
        //string connString = "Data Source=" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Database1.mdf";
        //C:\Users\chris\OneDrive\Documents\CMPG 223 - Project\Zoomazing Demo\Project-Zoomazing\CMPG223_Group22_Project\CMPG223_Group22_Project

        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\CMPG223\CMPG223_Group22_Project\CMPG223_Group22_Project\Database1.mdf;Integrated Security=True";
        SqlConnection conn;
        SqlCommand command;
        DataSet ds;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        int receive_id;



        /// <summary>
        /// Resets the relevant components and shows relevant components when called.
        /// </summary>
        private void show_animal_components()
        {
            lblAName.Visible = true;
            lblADOB.Visible = true;
            lblAGap1.Visible = true;
            lblAGap2.Visible = true;
            lblGender.Visible = true;
            lblWeight.Visible = true;
            lblVaccStatus.Visible = true;

            txtAName.Visible = true;
            nudADay.Visible = true;
            nudAMonth.Visible = true;
            nudAYear.Visible = true;
            pnlAGender.Visible = true;
            rdbMale.Visible = true;
            rdbFemale.Visible = true;
            txtWeight.Visible = true;
            pnlYesNo.Visible = true;

            txtAName.Text = "";
            nudADay.Value = 1;
            nudAMonth.Value = 1;
            nudAYear.Value = 1900;
            rdbMale.Checked = false;
            rdbFemale.Checked = false;
            txtWeight.Text = "";
            rdbTrue.Checked = false;
            rdbFalse.Checked = false;
        }
        private void show_visitor_components()
        {
            lblVFName.Visible = true;
            lblVLName.Visible = true;
            lblVDob.Visible = true;
            lblContactNumber.Visible = true;
            lblVGap1.Visible = true;
            lblVGap2.Visible = true;

            txtVName.Visible = true;
            txtVLName.Visible = true;
            nudVDay.Visible = true;
            nudVMonth.Visible = true;
            nudVYear.Visible = true;
            txtContactNumber.Visible = true;

            txtVName.Text = "";
            txtVLName.Text = "";
            nudVDay.Value = 1;
            nudVMonth.Value = 1;
            nudVYear.Value = 1900;
            txtContactNumber.Text = "";
        }


        /// <summary>
        /// Hides relevant(unnecessary) components when called
        /// </summary>
        private void hide_animal_components()
        {
            lblAName.Visible = false;
            lblADOB.Visible = false;
            lblAGap1.Visible = false;
            lblAGap2.Visible = false;
            lblGender.Visible = false;
            lblWeight.Visible = false;
            lblVaccStatus.Visible = false;

            txtAName.Visible = false;
            nudADay.Visible = false;
            nudAMonth.Visible = false;
            nudAYear.Visible = false;
            pnlAGender.Visible = false;
            rdbMale.Visible = false;
            rdbFemale.Visible = false;
            txtWeight.Visible = false;
            pnlYesNo.Visible = false;
        }
        private void hide_visitor_components()
        {
            lblVFName.Visible = false;
            lblVLName.Visible = false;
            lblVDob.Visible = false;
            lblContactNumber.Visible = false;
            lblVGap1.Visible = false;
            lblVGap2.Visible = false;

            txtVName.Visible = false;
            txtVLName.Visible = false;
            nudVDay.Visible = false;
            nudVMonth.Visible = false;
            nudVYear.Visible = false;
            txtContactNumber.Visible = false;
        }


        /// <summary>
        /// Creates new SQL components when called with an sql statement as parameter to use the command component with
        /// </summary>
        /// <param name="sql"></param>
        private void sql_showComponents(string sql)
        {
            conn = new SqlConnection(connString);
            command = new SqlCommand(sql, conn);
            adapter = new SqlDataAdapter();
            ds = new DataSet();
        }


        /// <summary>
        /// (for procedures "read_animal_id()" and "read_visitor_id()")
        /// (Used for the insert statement [to ensure [sequential addition?]])
        /// The SELECT SQL statement is declared using the received id [from the relevant database].
        /// The statement is then executed and read from the database.
        /// The id read, is then returned.
        /// </summary>
        /// <returns></returns>
        private int read_animal_id()
        {
            string sql_id = "SELECT Animal_Id FROM ANIMALS";
            sql_showComponents(sql_id);
            conn.Open();
            adapter.SelectCommand = command;
            adapter.Fill(ds, "ANIMALS");
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                receive_id = reader.GetInt32(0);
            }

            conn.Close();
            return receive_id;
        }
        private int read_visitor_id()
        {
            string sql_id = "SELECT Visitors_Id FROM VISITORS";
            sql_showComponents(sql_id);
            conn.Open();
            adapter.SelectCommand = command;
            adapter.Fill(ds, "VISITORS");
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                receive_id = reader.GetInt32(0);
            }

            conn.Close();
            return receive_id;
        }


        /// <summary>
        /// (for procedures "pop_Acbx" and "pop_Vcbx")
        /// SELECT SQL statement is declared with id field of relevant database to update relevant comboBoxes.
        /// This statment is then put into the "sql_showComponents()" procedure to initiate the relevant SQL components and create the correct demand.
        /// A comboBox is then populated with the records' id's for updating/ deleting records
        /// </summary>
        private void pop_Acbx()
        {
            string sql_pop = "SELECT Animal_Id FROM ANIMALS";
            sql_showComponents(sql_pop);

            conn.Open();

            adapter.SelectCommand = command;
            adapter.Fill(ds, "ANIMALS");

            cbxAId.DataSource = ds.Tables["ANIMALS"];
            cbxAId.DisplayMember = "Animal_Id";
            cbxAId.ValueMember = "Animal_Id";
        }
        private void pop_Vcbx()
        {
            string sql_pop = "SELECT Visitors_Id FROM VISITORS";
            sql_showComponents(sql_pop);

            conn.Open();

            adapter.SelectCommand = command;
            adapter.Fill(ds, "VISITORS");

            cbxVId.DataSource = ds.Tables["VISITORS"];
            cbxVId.DisplayMember = "Visitors_Id";
            cbxVId.ValueMember = "Visitors_Id";
        }


        /// <summary>
        /// (For procedures "sql_showAnimals()" and "sql_showVisitors" and "sql_showEmployees")
        /// SELECT SQL statement declared to select all the records from the relevant database to show the database to the user through the 'dataGridView' component.
        /// Also called after a new record has been inserted, record updated or record deleted.
        /// </summary>
        private void sql_showAnimals()
        {
            string sql = "SELECT * FROM ANIMALS";
            sql_showComponents(sql);
            conn.Open();
            adapter.SelectCommand = command;
            adapter.Fill(ds, "ANIMALS");

            dgvShowAnimals.DataSource = ds;
            dgvShowAnimals.DataMember = "ANIMALS";
        }
        private void sql_showVisitors()
        {
            string sql = "SELECT * FROM VISITORS";
            sql_showComponents(sql);
            conn.Open();
            adapter.SelectCommand = command;
            adapter.Fill(ds, "VISITORS");

            dgvShowVisitors.DataSource = ds;
            dgvShowVisitors.DataMember = "VISITORS";
        }



        /// <summary>
        /// Procedure to add an animal to the database(ANIMALS) and takes all the data/ values needed, as parameters.
        /// "sql_showComponents()" takes an sql statement as a parameter, thus the animal class with an "addAnimal" procedure is called because it returns an SQL statement with values received.
        /// The command is then executed and a new animal is added to the database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="weight"></param>
        /// <param name="vacc"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        private void sql_AddAnimal(string name, string gender, decimal weight, string vacc, int day, int month, int year)
        {
            cAnimals cAddA = new cAnimals();
            sql_showComponents(cAddA.addAnimal(read_animal_id(), name, gender, weight, vacc, day, month, year));

            conn.Open();
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Procedure to update an animal to the database(ANIMALS) and takes all the data/ values needed, as parameters.
        /// "sql_showComponents()" takes an sql statement as a parameter, thus the animal class with an "changeAnimalDetail" procedure is called because it returns an SQL statement with values received.
        /// The command is then executed and an animal's details are updated and posted on the database.
        /// </summary>
        /// <param name="animalId"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="weight"></param>
        /// <param name="vacc"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        private void sql_UpdateAnimal(string animalId, string name, string gender, decimal weight, string vacc, int day, int month, int year)
        {
            cAnimals cUpdateA = new cAnimals();
            sql_showComponents(cUpdateA.changeAnimalDetail(animalId, name, gender, weight, vacc, day, month, year));

            conn.Open();
            adapter.UpdateCommand = command;
            adapter.UpdateCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Procedure to delete an animal from the database(ANIMALS) and takes all the data/ values needed, as parameters.
        /// "sql_showComponents()" takes an sql statement as a parameter, thus the animal class with an "deleteAnimal" procedure is called because it returns an SQL statement with values received.
        /// The command is then executed and an animal is deleted from the database.
        /// </summary>
        /// <param name="animalId"></param>
        private void sql_DeleteAnimal(string animalId)
        {
            cAnimals cDeleteA = new cAnimals();
            sql_showComponents(cDeleteA.deleteAnimal(animalId));
            conn.Open();
            adapter.DeleteCommand = command;
            adapter.DeleteCommand.ExecuteNonQuery();
        }



        /// <summary>
        /// Procedure to add a Visitor to the database(VISITORS) and takes all the data/ values needed, as parameters.
        /// "sql_showComponents()" takes an sql statement as a parameter, thus the visitor class with an "newVisitor" procedure is called because it returns an SQL statement with values received.
        /// The command is then executed and a new visitor is added to the database.
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="contactNum"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        private void sql_AddVisitor(string surname, string name, string contactNum, int day, int month, int year)
        {
            cVisitors cAddV = new cVisitors();
            sql_showComponents(cAddV.newVisitor(read_visitor_id(), surname, name, contactNum, day, month, year));

            conn.Open();
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Procedure to update a visitor to the database(VISITORS) and takes all the data/ values needed, as parameters.
        /// "sql_showComponents()" takes an sql statement as a parameter, thus the visitor class with an "changeVisitorDetail" procedure is called because it returns an SQL statement with values received.
        /// The command is then executed and a visitor's details are updated and posted on the database.
        /// </summary>
        /// <param name="visitorId"></param>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="contactNum"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        private void sql_UpdateVisitor(string visitorId, string surname, string name, string contactNum, int day, int month, int year)
        {
            cVisitors cUpdateV = new cVisitors();
            sql_showComponents(cUpdateV.changeVisitorDetail(visitorId, surname, name, contactNum, day, month, year));

            conn.Open();
            adapter.UpdateCommand = command;
            adapter.UpdateCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Procedure to delete a visitor from the database(VISITORS) and takes all the data/ values needed, as parameters.
        /// "sql_showComponents()" takes an sql statement as a parameter, thus the visitor class with an "deleteVisitor" procedure is called because it returns an SQL statement with values received.
        /// The command is then executed and a visitor is deleted from the database.
        /// </summary>
        /// <param name="visitorId"></param>
        private void sql_DeleteVisitor(string visitorId)
        {
            cVisitors cDeleteV = new cVisitors();
            sql_showComponents(cDeleteV.deleteVisitor(visitorId));

            conn.Open();
            adapter.DeleteCommand = command;
            adapter.DeleteCommand.ExecuteNonQuery();
        }


        /// <summary>
        /// Validates the components used for date with the present date and tests accordingly
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private bool date_validate(int day, int month, int year)
        {
            bool valid = false;
            int daysInAMonth = DateTime.DaysInMonth(year, month);
            int dayVali = DateTime.Now.Day;
            int monthVali = DateTime.Now.Month;
            int yearVali = DateTime.Now.Year;


            if (((day > dayVali) && (month > monthVali) && (year > yearVali)) || (day > daysInAMonth))
            {
                MessageBox.Show("Invalid dates, please check dates again");
                return valid = false;
            }
            return valid = true;
        }


        /// <summary>
        /// (For procedures "total_ticket_qty" and "total_ticket_price")
        /// Reads data from TICKET entity to generate relevant reports such as:
        /// total ticket quantities
        /// total prices of tickets
        /// </summary>
        /// <returns></returns>
        private int total_ticket_qty()
        {
            int totalTQty = 0;
            int ticketQty = 0;
            bool isValid = false;
            string sql_receive = "SELECT isTicketValid FROM TICKETS";
            int receiveTicketId;
            sql_showComponents(sql_receive);
            conn.Open();
            adapter.SelectCommand = command;
            adapter.Fill(ds, "TICKETS");
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                receiveTicketId = reader.GetInt32(0);
                isValid = reader.GetBoolean(1);

                if (isValid == true)
                {
                    ticketQty++;
                }
            }

            conn.Close();
            totalTQty += ticketQty;
            return totalTQty;
        }
        private decimal total_ticket_price()
        {
            decimal totalTPrice = 0.00m;
            decimal ticketPrice = 0.00m;
            bool isValid = false;
            string sql_receive = "SELECT Price, isTicketValid FROM TICKETS";
            sql_showComponents(sql_receive);
            conn.Open();
            adapter.SelectCommand = command;
            adapter.Fill(ds, "TICKETS");
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                ticketPrice = (decimal)reader.GetValue(0);
                isValid = reader.GetBoolean(1);

                if (isValid == true)
                {
                    ticketPrice += ticketPrice;
                }
            }

            totalTPrice += ticketPrice;
            conn.Close();

            return totalTPrice;
        }


        /// <summary>
        /// (Same for "cbxAChooseAction_SelectedIndexChanged" and "bxVChooseAction_SelectedIndexChanged" with exceptions being animal = visitor in terminolgy sense)
        /// The actions that will occur when the index changes of the comboBox (comboBox asking what action would be taken), includes:
        /// ** A switch statement containing what would happen when a certain index is chosen:
        ///     * if 0 is chosen(which is for adding the animal): 
        ///         ~ The label and comboBox for ID, is hidden.
        ///         ~ "show_animal_components()" called, shows relevant components.
        ///         ~ Button that would execute the SQL statements, is renamed for appropriate action. ("Add..." in this case).
        ///     * If 1 is chosen(which is for updating the animal): 
        ///         ~ The label and comboBox for ID, is shown.
        ///         ~ "show_animal_components()" called, shows relevant components.
        ///         ~ Button that would execute the SQL statements, is renamed for appropriate action. ("Update..." in this case).
        ///     * If 2 is chosen(which is for deleting the animal): 
        ///         ~ The label and comboBox for ID, is shown.
        ///         ~ "show_animal_components()" called, shows relevant components.
        ///         ~ Button that would execute the SQL statements, is renamed for appropriate action. ("Delete..." in this case).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxAChooseAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxAChooseAction.SelectedIndex)
            {
                case 0:             //Adds animal
                    {
                        btnAnimalAction.Enabled = true;
                        pop_Acbx();
                        lblAID.Visible = false;
                        cbxAId.Visible = false;
                        show_animal_components();

                        btnAnimalAction.Text = "Add Animal";
                        break;
                    }
                case 1:             //Updates Animal
                    {
                        btnAnimalAction.Enabled = true;
                        pop_Acbx();
                        lblAID.Visible = true;
                        cbxAId.Visible = true;
                        show_animal_components();

                        btnAnimalAction.Text = "Update Animal Details";
                        break;
                    }
                case 2:             //Deletes Animal
                    {
                        btnAnimalAction.Enabled = true;
                        pop_Acbx();
                        lblAID.Visible = true;
                        cbxAId.Visible = true;
                        hide_animal_components();

                        btnAnimalAction.Text = "Remove Animal";
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
        }
        private void cbxVChooseAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxVChooseAction.SelectedIndex)
            {
                case 0:                     //Adds Visitor
                    {
                        btnVisitorAction.Enabled = true;
                        pop_Vcbx();
                        lblVID.Visible = false;
                        cbxVId.Visible = false;
                        show_visitor_components();

                        btnVisitorAction.Text = "Add Visitor";
                        break;
                    }
                case 1:                     //Updates Visitor
                    {
                        btnVisitorAction.Enabled = true;
                        pop_Vcbx();
                        lblVID.Visible = true;
                        cbxVId.Visible = true;
                        show_visitor_components();

                        btnVisitorAction.Text = "Update Visitor Details";
                        break;
                    }
                case 2:                     //Removes Visitor
                    {
                        btnVisitorAction.Enabled = true;
                        pop_Vcbx();
                        lblVID.Visible = true;
                        cbxVId.Visible = true;
                        hide_visitor_components();

                        btnVisitorAction.Text = "Delete Visitor";
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
        }



        /// <summary>
        /// The actions that will occur when the index changes of the comboBox (comboBox asking what action would be taken) and "action" button being clicked, includes:
        /// ** A switch statement containing what would happen when a certain index is chosen:
        ///     * if 0 is chosen(which is for adding the animal): 
        ///         ~ gender and vaccination variable declared and initialized. Validation and declarations also occurs for them as they have binary('2') availabilties.
        ///         ~ "sql_AddAnimal" called to get the values needed for the SQL statement and thus would be executed when button is pressed(Add/Insert).
        ///         ~ Connection is then closed. The connection would open again to show database after updating ("refresh").
        ///     * If 1 is chosen(which is for updating the animal): 
        ///         ~ gender and vaccination variable declared and initialized. Validation and declarations also occurs for them as they have binary('2') availabilties.
        ///         ~ "sql_UpdateAnimal" called to get the values needed for the SQL statement and thus would be executed when button is pressed(Update).
        ///         ~ Connection is then closed. The connection would open again to populate the comboBox (containing id's) and show database after updating ("refresh").
        ///     * If 2 is chosen(which is for deleting the animal):
        ///         ~ Animal record Id chosen through comboBox (populated, as described earlier, with id's of the records) and declared into variable.
        ///         ~ Variable is then called in the "sql_DeleteAnimal" procedure to be used in SQL statement, it would then be executed as button is pressed (Delete)
        ///         ~ Connection is then closed. The connection would open again to populate the comboBox (containing id's) and show database after deleting ("refresh").
        /// Format in log listbox for each option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnimalAction_Click(object sender, EventArgs e)
        {
            switch (cbxAChooseAction.SelectedIndex)
            {
                case 0:                     //Adds animal
                    {
                        try
                        {
                            string gender = "Neutral", vacci = "False";
                            if (rdbMale.Checked)
                            {
                                gender = rdbMale.Text;
                            }
                            else if (rdbFemale.Checked)
                            {
                                gender = rdbFemale.Text;
                            }

                            if (rdbTrue.Checked)
                            {
                                vacci = rdbTrue.Text;
                            }
                            else if (rdbFalse.Checked)
                            {
                                vacci = rdbFalse.Text;
                            }

                            if (Regex.IsMatch(txtAName.Text, @"\d"))
                            {
                                MessageBox.Show("Numbers found in the name entered");
                                txtAName.Focus();
                            }
                            else
                            {
                                int dayRec = (int)nudADay.Value;
                                int monthRec = (int)nudAMonth.Value;
                                int yearRec = (int)nudAYear.Value;

                                if (date_validate(dayRec, monthRec, yearRec) == true)
                                {
                                    sql_AddAnimal(txtAName.Text, gender, decimal.Parse(txtWeight.Text), vacci, dayRec, monthRec, yearRec);
                                    conn.Close();

                                    sql_showAnimals();
                                    conn.Close();

                                    lstbxAnimalLog.Items.Add("==================================================");
                                    lstbxAnimalLog.Items.Add("Record Added to ANIMAL table:");
                                    lstbxAnimalLog.Items.Add("");
                                    lstbxAnimalLog.Items.Add("Name:\t\t " + txtAName.Text);
                                    lstbxAnimalLog.Items.Add("Gender:\t\t " + gender.ToString());
                                    lstbxAnimalLog.Items.Add("Weight:\t\t " + decimal.Parse(txtWeight.Text));
                                    lstbxAnimalLog.Items.Add("Vaccinated:\t " + vacci.ToString());
                                    lstbxAnimalLog.Items.Add("Date of Birth:\t " + dayRec.ToString() + " / " + monthRec.ToString() + " / " + yearRec.ToString());
                                    lstbxAnimalLog.Items.Add("==================================================");

                                    ;
                                }
                            }
                            break;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case 1:                     //Updates Animal
                    {
                        try
                        {
                            string animalId = cbxAId.Text;
                            string gender = "Neutral", vacci = "False";
                            if (rdbMale.Checked)
                            {
                                gender = rdbMale.Text;
                            }
                            else if (rdbFemale.Checked)
                            {
                                gender = rdbFemale.Text;
                            }

                            if (rdbTrue.Checked)
                            {
                                vacci = rdbTrue.Text;
                            }
                            else if (rdbFalse.Checked)
                            {
                                vacci = rdbFalse.Text;
                            }

                            if (Regex.IsMatch(txtAName.Text, @"\d"))
                            {
                                MessageBox.Show("Numbers found in the name entered");
                                txtAName.Focus();
                            }
                            else
                            {
                                int dayRec = (int)nudADay.Value;
                                int monthRec = (int)nudAMonth.Value;
                                int yearRec = (int)nudAYear.Value;
                                if (date_validate(dayRec, monthRec, yearRec) == true)
                                {

                                    sql_UpdateAnimal(animalId, txtAName.Text, gender, decimal.Parse(txtWeight.Text), vacci, dayRec, monthRec, yearRec);
                                    conn.Close();

                                    sql_showAnimals();
                                    conn.Close();

                                    pop_Acbx();
                                    conn.Close();

                                    lstbxAnimalLog.Items.Add("==================================================");
                                    lstbxAnimalLog.Items.Add("Record Updated on ANIMAL table: (Full Details)");
                                    lstbxAnimalLog.Items.Add("");
                                    lstbxAnimalLog.Items.Add("ID used:\t\t " + animalId.ToString());
                                    lstbxAnimalLog.Items.Add("Name:\t\t " + txtAName.Text);
                                    lstbxAnimalLog.Items.Add("Gender:\t\t " + gender.ToString());
                                    lstbxAnimalLog.Items.Add("Weight:\t\t " + decimal.Parse(txtWeight.Text));
                                    lstbxAnimalLog.Items.Add("Vaccinated:\t " + vacci.ToString());
                                    lstbxAnimalLog.Items.Add("Date of Birth:\t " + dayRec.ToString() + " / " + monthRec.ToString() + " / " + yearRec.ToString());
                                    lstbxAnimalLog.Items.Add("==================================================");
                                }
                            }
                            break;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case 2:                     //Remove animal
                    {
                        try
                        {
                            string animalId = cbxAId.Text;
                            DialogResult userAnswer = MessageBox.Show($"Are you sure you want to delete record with ID number: {animalId} ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (userAnswer == DialogResult.Yes)
                            {
                                sql_DeleteAnimal(animalId);
                                conn.Close();

                                sql_showAnimals();
                                conn.Close();

                                pop_Acbx();
                                conn.Close();
                                lstbxAnimalLog.Items.Add("==================================================");
                                lstbxAnimalLog.Items.Add("Record Deleted on ANIMAL table:");
                                lstbxAnimalLog.Items.Add("");
                                lstbxAnimalLog.Items.Add("Record deleted with ID: " + animalId.ToString());
                                lstbxAnimalLog.Items.Add("==================================================");
                            }
                            break;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
        }

        /// <summary>
        /// The actions that will occur when the index changes of the comboBox (comboBox asking what action would be taken) and "action" button being clicked, includes:
        /// ** A switch statement containing what would happen when a certain index is chosen:
        ///     * if 0 is chosen(which is for adding the visitor): 
        ///         ~ "sql_AddVisitor" called to get the values needed for the SQL statement and thus would be executed when button is pressed(Add/Insert).
        ///         ~ Connection is then closed. The connection would open again to show database after adding ("refresh").
        ///     * If 1 is chosen(which is for updating the visitor): 
        ///         ~ "sql_UpdateVisitor" called to get the values needed for the SQL statement and thus would be executed when button is pressed(Update).
        ///         ~ Connection is then closed. The connection would open again to populate the comboBox (containing id's) and show database after updating ("refresh").
        ///     * If 2 is chosen(which is for deleting the visitor):
        ///         ~ Visitor record Id chosen through comboBox (populated, as described earlier, with id's of the records) and declared into variable.
        ///         ~ "sql_DeleteVisitor" called to get the values needed for the SQL statement and thus would be executed when button is pressed(Delete).
        ///         ~ Connection is then closed. The connection would open again to populate the comboBox (containing id's) and show database after deleting ("refresh").
        /// Format in log listbox for each option       
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVisitorAction_Click(object sender, EventArgs e)
        {
            switch (cbxVChooseAction.SelectedIndex)
            {
                case 0:                     //Adds Visitor      
                    {
                        try
                        {
                            if (Regex.IsMatch(txtVName.Text, @"\d"))
                            {
                                MessageBox.Show("Numbers found in the name entered");
                                txtVName.Focus();
                            }
                            else
                            {
                                if (Regex.IsMatch(txtVLName.Text, @"\d"))
                                {
                                    MessageBox.Show("Numbers found in the last name entered");
                                    txtVLName.Focus();
                                }
                                else
                                {
                                    if (Regex.IsMatch(txtContactNumber.Text, @"\D"))
                                    {
                                        MessageBox.Show("Letters found in the contact number entered");
                                        txtContactNumber.Focus();
                                    }
                                    else
                                    {
                                        int dayRec = (int)nudVDay.Value;
                                        int monthRec = (int)nudVMonth.Value;
                                        int yearRec = (int)nudVYear.Value;

                                        if (date_validate(dayRec, monthRec, yearRec) == true)
                                        {

                                            sql_AddVisitor(txtVName.Text, txtVLName.Text, txtContactNumber.Text, dayRec, monthRec, yearRec);
                                            conn.Close();

                                            sql_showVisitors();
                                            conn.Close();

                                            lstbxVisitorLog.Items.Add("==================================================");
                                            lstbxVisitorLog.Items.Add("Record Added to VISITOR table:");
                                            lstbxVisitorLog.Items.Add("");
                                            lstbxVisitorLog.Items.Add("Name:\t\t " + txtVName.Text);
                                            lstbxVisitorLog.Items.Add("Last Name:\t " + txtVLName.Text);
                                            lstbxVisitorLog.Items.Add("ContactNumber:\t " + txtContactNumber.Text);
                                            lstbxVisitorLog.Items.Add("Date of Birth:\t " + dayRec.ToString() + " / " + monthRec.ToString() + " / " + yearRec.ToString());
                                            lstbxVisitorLog.Items.Add("==================================================");
                                        }
                                    }
                                }
                            }
                            break;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case 1:                     //Updates Visitor         
                    {
                        try
                        {
                            string visitorId = cbxVId.Text;
                            if (Regex.IsMatch(txtVName.Text, @"\d"))
                            {
                                MessageBox.Show("Numbers found in the name entered");
                                txtVName.Focus();
                            }
                            else
                            {
                                if (Regex.IsMatch(txtVLName.Text, @"\d"))
                                {
                                    MessageBox.Show("Numbers found in the last name entered");
                                    txtVLName.Focus();
                                }
                                else
                                {
                                    if (Regex.IsMatch(txtContactNumber.Text, @"\D"))
                                    {
                                        MessageBox.Show("Letters found in the contact number entered");
                                        txtContactNumber.Focus();
                                    }
                                    else
                                    {
                                        int dayRec = (int)nudVDay.Value;
                                        int monthRec = (int)nudVMonth.Value;
                                        int yearRec = (int)nudVYear.Value;

                                        if (date_validate(dayRec, monthRec, yearRec) == true)
                                        {

                                            sql_UpdateVisitor(visitorId, txtVLName.Text, txtVName.Text, txtContactNumber.Text, dayRec, monthRec, yearRec);
                                            conn.Close();

                                            sql_showVisitors();
                                            conn.Close();

                                            pop_Vcbx();
                                            conn.Close();

                                            lstbxVisitorLog.Items.Add("==================================================");
                                            lstbxVisitorLog.Items.Add("Record Updated on VISITOR table: (Full Details)");
                                            lstbxVisitorLog.Items.Add("");
                                            lstbxVisitorLog.Items.Add("ID used:\t\t " + visitorId.ToString());
                                            lstbxVisitorLog.Items.Add("Name:\t\t " + txtVName.Text);
                                            lstbxVisitorLog.Items.Add("Last Name:\t " + txtVLName.Text);
                                            lstbxVisitorLog.Items.Add("ContactNumber:\t " + txtContactNumber.Text);
                                            lstbxVisitorLog.Items.Add("Date of Birth:\t " + dayRec.ToString() + " / " + monthRec.ToString() + " / " + yearRec.ToString());
                                            lstbxVisitorLog.Items.Add("==================================================");
                                        }
                                    }
                                }
                            }
                            break;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case 2:                     //Removes Visitor  
                    {
                        try
                        {
                            string visitorId = cbxVId.Text;
                            DialogResult userAnswer = MessageBox.Show($"Are you sure you want to delete record with ID number: {visitorId} ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (userAnswer == DialogResult.Yes)
                            {
                                sql_DeleteVisitor(visitorId);
                                conn.Close();

                                sql_showVisitors();
                                pop_Vcbx();
                                conn.Close();

                                lstbxVisitorLog.Items.Add("==================================================");
                                lstbxVisitorLog.Items.Add("Record Deleted on VISITOR table:");
                                lstbxVisitorLog.Items.Add("");
                                lstbxVisitorLog.Items.Add("Record deleted with ID: " + visitorId.ToString());
                                lstbxVisitorLog.Items.Add("==================================================");
                            }
                            break;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
        }

        /// <summary>
        /// Initiates form by hiding almost all [but relevant] components.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdminForm_Load(object sender, EventArgs e)
        {
            /// <relativepathnote>
            /// Attempt at creating a relative database path, but received error within terms of remote network, would sort out if given enough time :)
            /// connString = "Data Source=|DataDirectory|\\Database1.mdf";
            /// executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            /// path = System.IO.Path.GetDirectoryName(executable);
            /// AppDomain.CurrentDomain.SetData("DataDirectory", path);
            /// </note>


            lstbxAnimalLog.Items.Add("ANIMAL TABLE LOG");
            lstbxAnimalLog.Items.Add("");
            lstbxVisitorLog.Items.Add("VISITOR TABLE LOG");
            lstbxVisitorLog.Items.Add("");
            lstbxReport.Items.Add("REPORTS");
            lstbxVisitorLog.Items.Add("");

            lblAID.Visible = false;
            cbxAId.Visible = false;
            sql_showAnimals();
            hide_animal_components();
            nudAYear.Maximum = DateTime.Now.Year;

            lblVID.Visible = false;
            cbxVId.Visible = false;
            sql_showVisitors();
            hide_visitor_components();
            nudVYear.Maximum = DateTime.Now.Year;

            btnAnimalAction.Enabled = false;
            btnVisitorAction.Enabled = false;

        }


        /// <summary>
        /// Validation, disables user from entering values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxAId_TextChanged(object sender, EventArgs e)
        {
            cbxAId.Text = "";
        }
        private void cbxVId_TextChanged(object sender, EventArgs e)
        {
            cbxVId.Text = "";
        }


        /// <summary>
        /// Gives the report for the tickets with formatting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNumTickets_Click(object sender, EventArgs e)
        {
            //Quantity of tickets
            int totalTQty = total_ticket_qty();
            DateTime today = DateTime.Now;

            lstbxReport.Items.Add("==================================================");
            lstbxReport.Items.Add($" TOTAL number of tickets |\t\t\t({today})");
            lstbxReport.Items.Add("---------------------------");
            lstbxReport.Items.Add("");
            lstbxReport.Items.Add("Total Tickets: " + totalTQty.ToString());
            lstbxReport.Items.Add("==================================================");
        }
        private void btnTotalCost_Click(object sender, EventArgs e)
        {
            //Total money received from tickets
            decimal totalTPrice = total_ticket_price();
            DateTime today = DateTime.Now;

            lstbxReport.Items.Add("==================================================");
            lstbxReport.Items.Add($"TOTAL TICKET COST |\t\t\t({today})");
            lstbxReport.Items.Add("-----------------------");
            lstbxReport.Items.Add("");
            lstbxReport.Items.Add("Total Price of Tickets: R" + totalTPrice.ToString());
            lstbxReport.Items.Add("==================================================");

        }


        /// <summary>
        /// Exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnVExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnRExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        /// <summary>
        /// Quick help button for users
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button will only activate once the action on the top has been chosen", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnVHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button will only activate once the action on the top has been chosen", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnRHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reports are pre-determined via buttons above. Press a button to get the report(s)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
