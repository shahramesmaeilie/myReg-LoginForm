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
    public partial class frmreg : Form
    {
        public frmreg()
        {
            InitializeComponent();
        }

        private void btnSinUp_Click(object sender, EventArgs e)
        {
       
            if (txtPass.TextLength<8)
            {
                DialogResult result3;
                result3=MessageBox.Show("The Password should be more than 8 character!!","Warning",MessageBoxButtons.OKCancel);
                if (result3 == DialogResult.OK)
                {
                    txtPass.Text = "";
                    txtPass.Focus();
                }
                else
                {
                    txtFname.Text = "";
                    txtLname.Text = "";
                    txtGender.Text = "";
                    txtEmail.Text = "";
                    txtPhone.Text = "";
                    txtPass.Text = "";
                    txtConPass.Text = "";
                    txtUsername.Text = "";
                    txtAdd.Text = "";
                }
                
            }
            else
            {
                if (txtPass.Text == txtConPass.Text)
                {
                    SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=userregcs;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand(@" INSERT INTO [dbo].[tblregister]
           ([firstname]
           ,[lastname]
           ,[addres]
           ,[gender]
           ,[email]
           ,[phone]
           ,[username]
           ,[password])
     VALUES
           (' " + txtFname.Text + " ',' " + txtLname.Text + " ',' " + txtAdd.Text + " ',' " + txtGender.Text + " ', ' " + txtEmail.Text + " ', ' " + txtPhone.Text + " ', ' " + txtUsername.Text + " ',' " + txtPass.Text + " ') ", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    DialogResult result;
                    result = MessageBox.Show("Registeration done Successfuly", "Acclamation!!", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        frmMain f1 = new frmMain();
                        f1.Show();
                        this.Hide();
                    }
                    else
                    {
                        frmLogin frmLogin = new frmLogin();
                        frmLogin.Show();
                        this.Hide();
                    }
                }
                else
                {
                    DialogResult res;
                    res = MessageBox.Show("Password is not Correct", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.OK)
                    {
                        txtPass.Text = "";
                        txtConPass.Text = "";
                        txtPass.Focus();
                        DialogResult res2;
                        res2 = MessageBox.Show("Enter Your Correct Password Again!!", "Warning", MessageBoxButtons.RetryCancel);
                        if (res2 == DialogResult.Retry)
                        {
                            txtPass.Text = "";
                            txtConPass.Text = "";
                            txtPass.Focus();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtPass.PasswordChar = '\0';
                txtConPass.PasswordChar='\0';
            }
            else
            {
                txtPass.PasswordChar = '*';
                txtConPass.PasswordChar = '*';
            }
        }

        private void linkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPassForget ff=new frmPassForget();
            ff.Show();
            this.Hide();
        }
    }
}