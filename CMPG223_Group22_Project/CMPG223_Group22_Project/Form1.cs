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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

//===============================================
        //Use camelCase for variables            |
        //CAPITAL LETTERS FOR CONSTANTS          |
        //snake_case_for_tables_to_be_used       |
//===============================================

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login form2 = new Login();
            form2.Show();
        }
    }
}
