using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPG223_Group22_Project
{
    class cAnimals
    {
        /// <summary>
        /// Relevant variables (for database) declared for use
        /// </summary>
        public string name;
        public char gender;
        public decimal weight;
        public bool isVacc;
        public DateTime dateOfBirth;


        /// <summary>
        /// (same for: "getName", "getGender", "getGender", "getWeight", "getVacc", "getDateOfBirth")
        /// returns the variable that was declared and received in "set.." methods.
        /// It is then put into the insert-and update statements
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return name;
        }
        public char getGender()
        {
            return gender;
        }
        public decimal getWeight()
        {
            return weight;
        }
        public bool getVacc()
        {
            return isVacc;
        }
        public DateTime getDateOfBirth()
        {
            return dateOfBirth;
        }


        /// <summary>
        /// Assigns called variable("name") to local variable("name")
        /// </summary>
        /// <param name="name"></param>
        public void setName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Evaluates called variables and re-assigns to a variable("gender") with correct datatype("char") for SQL statements for use in database.
        /// </summary>
        /// <param name="fullGender"></param>
        public void setGender(string fullGender)
        {
            if (fullGender == "Male")
            {
                gender = 'M';
            }
            else if (fullGender == "Female")
            {
                gender = 'F';
            }
        }

        /// <summary>
        /// Assigns called variable("weight") to local variable("weight")
        /// </summary>
        /// <param name="weight"></param>
        public void setWeight(decimal weight)
        {
            this.weight = weight;
        }

        /// <summary>
        /// Evaluates called variables and re-assigns to a variable("isVacc") with correct datatype("boolean") for SQL statements for use in database. 
        /// </summary>
        /// <param name="vaccination"></param>
        public void setVacc(string vaccination)
        {
            if (vaccination == "True")
            {
                isVacc = true;
            }
            else if (vaccination == "False")
            {
                isVacc = false;
            }
        }

        /// <summary>
        /// Receives variables from where procedure was called. 
        /// Variables are evaluated and formatted to correct datatype("string") for incorporation into relevant SQL statements.
        /// To satisfy the date format of the database, the day and month with single digits, need a "0" in front. 
        ///     This is done by adding a "0" in front of the "day"/ "month" string when the integer variables are bigger than 0 and smaller than "10" (2 digits)
        /// After the "day"/ "month" formatting, the day, month and year is formatted as date in the database in string datatype.
        /// The date string is then Parsed to a DateTime format for use in the database.
        /// The DateTime variable is then assigned to the local variable for the SQL statements
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
        /// INSERT SQL statement is formed from variables assigned as above.
        /// "set" methods are called to initiate variables and do the validation/ evaluation as previously described above.
        /// After the variables have been validated/ evaluated, the SQL statement is formed by calling the "get" methods.
        /// The animal_Id is also inserted to ensure accuracy of insertion. Id is received from being read from database in "AdminForm.cs" via "read_animal_id()"
        /// Returns INSERT SQL statement.
        /// </summary>
        /// <param name="animalId"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="weight"></param>
        /// <param name="vacc"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public string addAnimal(int animalId, string name, string gender, decimal weight, string vacc, int day, int month, int year)
        {
            setName(name);
            setGender(gender);
            setWeight(weight);
            setVacc(vacc);
            setDateOfBirth(day, month, year);

            //string sql = $"INSERT INTO ANIMALS(Animal_ID, Animal_Name, Date_Of_Birth, Gender, Weight, Vaccination) VALUES({animalId + 1}, '{getName()}', '{getDateOfBirth()}', '{getGender()}', {getWeight()}, '{getVacc()}')";
            string sql = $"INSERT INTO ANIMALS VALUES({animalId + 1}, '{getName()}', '{getDateOfBirth()}', '{getGender()}', {getWeight()}, '{getVacc()}')";

            return sql;
        }

        /// <summary>
        /// UPDATE SQL statement is formed from variables assigned as above.
        /// "set" methods are called to initiate variables and do the validation/ evaluation as previously described above.
        /// After the variables have been validated/ evaluated, the SQL statement is formed by calling the "get" methods.
        /// The animal_Id is as a condition as it is possibly the only unique field to work with.
        /// Id is received from being read from database in "AdminForm.cs" via "read_animal_id()"
        /// Returns UPDATE SQL statement.
        /// </summary>
        /// <param name="animalId"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="weight"></param>
        /// <param name="vacc"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public string changeAnimalDetail(string animalId, string name, string gender, decimal weight, string vacc, int day, int month, int year)
        {
            setName(name);
            setGender(gender);
            setWeight(weight);
            setVacc(vacc);
            setDateOfBirth(day, month, year);

            string sql = $"UPDATE ANIMALS SET Animal_Name = '{getName()}', Date_Of_Birth = '{getDateOfBirth()}', Gender = '{getGender()}', Weight = {getWeight()}, Vaccination = '{getVacc()}' WHERE Animal_ID = '{animalId}'";

            return sql;
        }

        /// <summary>
        /// DELETE SQL statement is formed.
        /// The animal_Id is used as a condition as it is possibly the only unique field to work with.
        /// Id is received from being read from database in "AdminForm.cs" via "read_animal_id()"
        /// Returns DELETE SQL statement.
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns></returns>
        public string deleteAnimal(string animalId)
        {
            string sql = $"DELETE FROM ANIMALS WHERE Animal_ID = '{animalId}'";

            return sql;
        }
    }
}
