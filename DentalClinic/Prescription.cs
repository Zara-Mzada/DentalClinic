﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace DentalClinic
{
    public partial class Prescription : Form
    {
        public Prescription()
        {
            InitializeComponent();
        }
        ConnectionString MyCon = new ConnectionString();
        private void fillPatient()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Patient from AppointmentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Patient", typeof(string));
            dt.Load(rdr);
            PatId.ValueMember = "Patient";
            PatId.DataSource = dt;
            Con.Close();
        }
        private void fillTreatment()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TreatName from TreatmentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Patient", typeof(string));
            dt.Load(rdr);
            PatId.ValueMember = "Patient";
            PatId.DataSource = dt;
            Con.Close();
        }
        private void GetTreatment()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from AppointmentTbl where Patient='"+PatId.SelectedValue.ToString()+"'", Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                TreatmentTb.Text = dr["Treatment"].ToString();
            }
            Con.Close();
        }
        private void GetPrice()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from TreatmentTbl where TreatName='" + TreatmentTb.Text + "'", Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TreatCostTb.Text = dr["TreatCost"].ToString();
            }
            Con.Close();
        }
        private void Prescription_Load(object sender, EventArgs e)
        {
            fillPatient();
            populate();
        }

        private void PatId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTreatment();
        }

        private void TreatmentTb_TextChanged(object sender, EventArgs e)
        {
            GetPrice();
        }
        void populate()
        {
            MyPatient Pat = new MyPatient();
            string query = "select * from PrescriptionTbl";

            DataSet ds = Pat.ShowPatient(query);
            PrescriptionDGV.DataSource = ds.Tables[0];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into PrescriptionTbl values('" + PatId.SelectedValue.ToString() + "','" + TreatmentTb.Text + "','" + TreatCostTb.Text + "','" + MedicinesTb.Text + "','" + QtyTb.Text + "')";
            MyPatient Pat = new MyPatient();
            try
            {
                Pat.AddPatient(query);
                MessageBox.Show("Treatment Successfully Added");
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int key = 0;
        private void PrescriptionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatId.SelectedValue = PrescriptionDGV.SelectedRows[0].Cells[1].Value.ToString();
            TreatmentTb.Text = PrescriptionDGV.SelectedRows[0].Cells[2].Value.ToString();
            TreatCostTb.Text = PrescriptionDGV.SelectedRows[0].Cells[3].Value.ToString();
            MedicinesTb.Text = PrescriptionDGV.SelectedRows[0].Cells[4].Value.ToString();
            QtyTb.Text = PrescriptionDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (TreatmentTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PrescriptionDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (key == 0)
            {
                MessageBox.Show("Select The Prescription");
            }
            else
            {
                try
                {
                    string query = "Delete from PrescriptionTbl where PrescId=" + key + "";

                    Pat.DeletePatient(query);
                    MessageBox.Show("Prescription Successfully Deleted");
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
        Bitmap bitmap;
        private void button3_Click(object sender, EventArgs e)
        {
            int height = PrescriptionDGV.Height;
            PrescriptionDGV.Height = PrescriptionDGV.RowCount * PrescriptionDGV.RowTemplate.Height * 2;
            bitmap = new Bitmap(PrescriptionDGV.Width, PrescriptionDGV.Height);
            PrescriptionDGV.DrawToBitmap(bitmap, new Rectangle(0, 10, PrescriptionDGV.Width, PrescriptionDGV.Height));
            PrescriptionDGV.Height = height;
            printPreviewDialog1.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Patient Pat = new Patient();
            Pat.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Treatment Treat = new Treatment();
            Treat.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Appointment App = new Appointment();
            App.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            DashBoard dash = new DashBoard();
            dash.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
