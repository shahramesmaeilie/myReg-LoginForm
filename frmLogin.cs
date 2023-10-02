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

namespace myLogin
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "" || txtUsername.Text == "")
            {
                DialogResult re;
                re = MessageBox.Show("Please Fill in the Textboxes!!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (re == DialogResult.OK)
                {
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
                else
                {
                    frmreg f = new frmreg();
                    f.ShowDialog();
                    this.Hide();
                }
            }
            else
            {
                SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=userregcs;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(@"select * from tblregister where username=' " + txtUsername.Text + " ' and password=' " + txtPassword.Text + " ' ", conn);
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    r.Read();
                    frmMain frmMain = new frmMain();
                    frmMain.Show();

                }
                else
                {
                    DialogResult res;
                    res = MessageBox.Show("Password or Username is not Correct!!", "Warning!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.OK)
                    {
                        txtPassword.Text = "";
                        txtUsername.Text = "";
                        txtUsername.Focus();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                r.Close();
                conn.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtPassword.PasswordChar= '\0';
            }
            else
            {
                txtPassword.PasswordChar= '*';
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmreg frmreg = new frmreg();
            frmreg.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPassForget f=new frmPassForget();
            f.Show();
            this.Hide();
        }
    }
}
