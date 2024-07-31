using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMPG223_Group22_Project
{
    public partial class Receipt : Form
    {
        public Receipt()
        {
            InitializeComponent();
        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            Staff frmStaff = new Staff();

            lbReceipt.Items.Add(frmStaff.Name + " " + frmStaff.Surname + " your date of birth is: " + frmStaff.DOB + " with contact info: " + frmStaff.ContactNo + " on the date of " + frmStaff.DOB + " with the price being" + frmStaff.price);

        }
    }
}
