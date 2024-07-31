using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPG223_Group22_Project
{
    class cPerson
    {
        /// <summary>
        /// public string variables declared. common attributes of employees and visitors.
        /// </summary>
        public string name;
        public string surname;
        public string contactNum;

        /// <summary>
        /// (same for: "getName", "getSurname", "getContactNumber")
        /// returns the variable that was declared and received in "set.." methods.
        /// It is then put into the insert-and update statements
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return name;
        }
        public string getSurname()
        {
            return surname;
        }
        public string getContactNum()
        {
            return contactNum;
        }

        /// <summary>
        /// Receives name, surname and contactNumber from mthods that were called
        /// </summary>
        /// <param name="name"></param>
        public void setName(string name)
        {
            this.name = name;
        }
        public void setSurname(string surname)
        {
            this.surname = surname;
        }
        public void setContactNum(string contactNum)
        {
            this.contactNum = contactNum;
        }

        //Not necessary
        public void addPerson()
        {

        }
        public void changePersonDetals()
        {

        }
        public void deletePerson()
        {

        }
        /////
    }
}
