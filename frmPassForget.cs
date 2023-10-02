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
    public partial class frmPassForget : Form
    {
        public frmPassForget()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtNewPass.Text == txtConfPass.Text)
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=userregcs;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand(@"update tblregister set password=' " + txtNewPass.Text + " ' where phone=' " + txtPhone.Text + " ' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                DialogResult r;
                r = MessageBox.Show("Your Password Changed Successfuly", "Informing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (r == DialogResult.OK)
                {
                    frmLogin f = new frmLogin();
                    f.Show();
                    this.Hide();
                }
            }
            else
            {
                DialogResult result;
                result = MessageBox.Show("Password is not Correct", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    txtConfPass.Text = "";
                    txtNewPass.Text = "";
                    txtPhone.Text = "";
                    txtPhone.Focus();
                }
                else
                {
                    this.Close();
                }
            }       
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtNewPass.PasswordChar='\0';
                txtConfPass.PasswordChar = '\0';
            }
            else
            {
                txtNewPass.PasswordChar ='*';
                txtConfPass.PasswordChar = '*';
            }
        }
    }
}
