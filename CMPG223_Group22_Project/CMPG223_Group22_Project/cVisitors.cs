using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPG223_Group22_Project
{
    class cVisitors
    {
        /// <summary>
        /// variable declared for use in SQL statements
        /// </summary>
        public DateTime dateOfBirth;

        /// <summary>
        /// returns the variable that was declared and received in "set.." methods. DateTime variable in this case
        /// It is then put into the insert-and update statements
        /// </summary>
        /// <returns></returns>
        public DateTime getDateOfBirth()
        {
            return dateOfBirth;
        }

        /// <summary>
        /// Receives variables from where procedure was called. 
        /// Variables are evaluated and formatted to correct datatype("string") for incorporation into relevant SQL statements.
        /// To satisfy the date format of the database, the day and month with single digits, need a "0" in front. 
        ///     This is done by adding a "0" in front of the "day"/ "month" string when the integer variables are bigger than '0' and smaller than '10' (2 digits)
        /// After the "day"/ "month" formatting, the day, month and year is formatted as date in the database in string datatype.
        /// The date string is then Parsed to a DateTime format for use in the database.
        /// The DateTime variable is then assigned to the local DateTime variable for the SQL statements
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        public void setDateOfBirth(int day, int month, int year)
        {
            string correctDay, correctMonth;

            if ((day < 10) && (day >= 1))
            {
                correctDay = "0" + day.ToString();
            }
            else
            {
                correctDay = day.ToString();
            }

            if ((month < 10) && (month >= 1))
            {
                correctMonth = "0" + month.ToString();
            }
            else
            {
                correctMonth = month.ToString();
            }
            string dateVar = year.ToString() + "/" + correctMonth + "/" + correctDay;

            DateTime dob = DateTime.ParseExact(dateVar, "yyyy/MM/dd", null);

            dateOfBirth = dob;
        }

        /// <summary>
        /// INSERT SQL statement is formed from variable assigned above as well as superclass "Person".
        /// "set" methods are called to initiate variables and do the validation/ evaluation as previously described.
        /// After the variables have been validated/ evaluated, the SQL statement is formed by calling the "get" methods.
        /// The visitor_Id is also inserted to ensure accuracy of insertion. Id is received from being read from database in "AdminForm.cs" via "read_visitor_id()"
        /// Returns INSERT SQL statement.
        /// </summary>
        /// <param name="visitorId"></param>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="contactNum"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public string newVisitor(int visitorId, string surname, string name, string contactNum, int day, int month, int year)
        {
            cPerson vPerson = new cPerson();
            vPerson.setSurname(surname);
            vPerson.setName(name);
            vPerson.setContactNum(contactNum);
            setDateOfBirth(day, month, year);
            string sql = $"INSERT INTO VISITORS VALUES({visitorId + 1},'{vPerson.getSurname()}', '{vPerson.getName()}', '{getDateOfBirth()}', '{vPerson.getContactNum()}')";

            return sql;
        }

        /// <summary>
        /// UPDATE SQL statement is formed from variables assigned above as well as superclass "Person".
        /// "set" methods are called to initiate variables and do the validation/ evaluation as previously described.
        /// After the variables have been validated/ evaluated, the SQL statement is formed by calling the "get" methods.
        /// The visitor_Id is as a condition as it is possibly the only unique field to work with.
        /// Id is received from being read from database in "AdminForm.cs" via "read_visitor_id()"
        /// Returns UPDATE SQL statement.
        /// </summary>
        /// <param name="visitorId"></param>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="contactNum"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public string changeVisitorDetail(string visitorId, string surname, string name, string contactNum, int day, int month, int year)
        {
            cPerson vPerson = new cPerson();
            vPerson.setSurname(surname);
            vPerson.setName(name);
            vPerson.setContactNum(contactNum);
            setDateOfBirth(day, month, year);

            string sql = $"UPDATE VISITORS SET Visitors_LName = '{vPerson.getSurname()}', Visitors_Name = '{vPerson.getName()}', Date_Of_Birth = '{getDateOfBirth()}', ContactNumber = '{vPerson.getContactNum()}' WHERE Visitors_ID = '{visitorId}'";

            return sql;
        }

        /// <summary>
        /// DELETE SQL statement is formed.
        /// The visitor_Id is used as a condition as it is possibly the only unique field to work with.
        /// Id is received from being read from database in "AdminForm.cs" via "read_visitor_id()"
        /// Returns DELETE SQL statement.
        /// </summary>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        public string deleteVisitor(string visitorId)
        {
            string sql = $"DELETE FROM VISITORS WHERE Visitors_ID = '{visitorId}'";

            return sql;
        }
    }
}
