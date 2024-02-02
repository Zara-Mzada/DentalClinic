using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace DentalClinic
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }
        void populate()
        {
            MyPatient Pat = new MyPatient();
            string query = "select * from UserTbl";

            DataSet ds = Pat.ShowPatient(query);
            UserDGV.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into UserTbl values('" + UnameTb.Text + "','" + PasswordTb.Text + "','" + PhoneTb.Text + "')";
            MyPatient Pat = new MyPatient();
            try
            {
                Pat.AddPatient(query);
                MessageBox.Show("User Successfully Added");
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            populate();
        }
        int key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            PasswordTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            PhoneTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
          
            if (UnameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (key == 0)
            {
                MessageBox.Show("Select The User T Be Deleted");
            }
            else
            {
                try
                {
                    string query = "Delete from UserTbl where UID=" + key + "";

                    Pat.DeletePatient(query);
                    MessageBox.Show("User Successfully Deleted");
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (key == 0)
            {
                MessageBox.Show("Select The User");
            }
            else
            {
                try
                {
                    string query = "Update UserTbl set Uname = '" + UnameTb.Text + "', Upass='" + PasswordTb.Text + "',Phone='" + PhoneTb.Text + "' where UId=" + key + ";";


                    Pat.DeletePatient(query);
                    MessageBox.Show("User Successfully Updated");
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

    }
}
