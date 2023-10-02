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
using System.IO;

namespace myLogin
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public void fdisable1()
        {
            txtcode.Enabled = true;
            txtname.Enabled = false;
            txtfamily.Enabled = false;  
            txtDOB.Enabled = false;
            txtbirthcity.Enabled = false;   
            txtaddress.Enabled = false;
            txtmobile.Enabled = false;
            txtemail.Enabled = false;
            txtfield.Enabled = false;
            cmbgender.Enabled = false;
            cmbedu.Enabled = false;
            gunaPictureBox1.Enabled = false;

        }
        public void fdisable2()
        {
            txtcode.Enabled = false;
            txtname.Enabled = true;
            txtfamily.Enabled = false;
            txtDOB.Enabled = false;
            txtbirthcity.Enabled = false;
            txtaddress.Enabled = false;
            txtmobile.Enabled = false;
            txtemail.Enabled = false;
            txtfield.Enabled = false;
            cmbgender.Enabled = false;
            cmbedu.Enabled = false;
            gunaPictureBox1.Enabled = false;
        }

        public void fdisable3()
        {
            txtcode.Enabled = false;
            txtname.Enabled = false;
            txtfamily.Enabled = false;
            txtDOB.Enabled = false;
            txtbirthcity.Enabled = false;
            txtaddress.Enabled = false;
            txtmobile.Enabled = true;
            txtemail.Enabled = false;
            txtfield.Enabled = false;
            cmbgender.Enabled = false;
            cmbedu.Enabled = false;
            gunaPictureBox1.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void refresh()
        {
            string cs = "Data Source=(local);Initial Catalog=userregcs;Integrated Security=True";
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            string com = "select * from tbluser";
            SqlDataAdapter cmd = new SqlDataAdapter(com, conn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            dataGV.DataSource = dt;
            conn.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'userregcsDataSet1.tbluser' table. You can move, or remove it, as needed.
            this.tbluserTableAdapter.Fill(this.userregcsDataSet1.tbluser);

        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog pic = new OpenFileDialog();
            pic.ShowDialog();
            gunaPictureBox1.Image = Image.FromFile(pic.FileName);
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtcode.Text == "" || txtname.Text == "" || txtfamily.Text == "" || txtDOB.Text == "" ||
               txtbirthcity.Text == "" || txtaddress.Text == "" || txtmobile.Text == "" || txtemail.Text == "" ||
                 cmbgender.Text == "" || cmbedu.Text == "") {
                MessageBox.Show("Please Fill in All Blanks!!", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                btnNew_Click(sender, e);
            }
            else
            {
                gunaPictureBox1.Image.Save(Application.StartupPath + "//picture//" + txtcode.Text + ".png");
                FileStream fs = File.OpenRead(Application.StartupPath + "//picture//" + txtcode.Text + ".png");
                byte[] arr = new byte[fs.Length];
                fs.Read(arr, 0, (int)arr.Length);
                fs.Close();

                string cs = "Data Source=(local);Initial Catalog=userregcs;Integrated Security=True";
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                string com = "insert into tbluser values (@c,@n,@fa,@db,@bc,@add,@m,@em,@g,@ed,@fi,@pic)";
                SqlCommand cmd = new SqlCommand(com, conn);
                cmd.Parameters.AddWithValue("@c", txtcode.Text);
                cmd.Parameters.AddWithValue("@n", txtname.Text);
                cmd.Parameters.AddWithValue("@fa", txtfamily.Text);
                cmd.Parameters.AddWithValue("@db", txtDOB.Text);
                cmd.Parameters.AddWithValue("@bc", txtbirthcity.Text);
                cmd.Parameters.AddWithValue("@add", txtaddress.Text);
                cmd.Parameters.AddWithValue("@m", txtmobile.Text);
                cmd.Parameters.AddWithValue("@em", txtemail.Text);
                cmd.Parameters.AddWithValue("@g", cmbgender.SelectedItem);
                cmd.Parameters.AddWithValue("@ed", cmbedu.SelectedItem);
                cmd.Parameters.AddWithValue("@fi", txtfield.Text);
                cmd.Parameters.AddWithValue("@pic", arr);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Information saved Successfully!", "Acclamation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNew_Click(sender, e);
                    refresh();
                }
                catch
                {
                    DialogResult res;
                    res = MessageBox.Show("Error in Saving Information!!!", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.OK)
                    {
                        btnNew_Click(sender, e);
                    }
                    else
                    {
                        btnNew_Click(sender, e);
                        this.Close();
                    }
                }
                conn.Close();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtcode.Text = "Code";
            txtname.Text = "Name";
            txtfamily.Text = "Family";
            txtDOB.Text = "Date of Birth";
            txtbirthcity.Text = "Birth City";
            txtaddress.Text = "Address";
            cmbgender.Text = "Male";
            cmbedu.Text = "B.Sc";
            txtmobile.Text = "Mobile";
            txtemail.Text = "Email";
            txtfield.Text = "Field of Study";
            txtcode.Focus();
            gunaPictureBox1.Image = Image.FromFile(Application.StartupPath + "//images//User.png");
        }

        private void dataGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtcode.Text = dataGV.SelectedRows[0].Cells[0].Value.ToString();
            txtname.Text = dataGV.SelectedRows[0].Cells[1].Value.ToString();
            txtfamily.Text = dataGV.SelectedRows[0].Cells[2].Value.ToString();
            txtDOB.Text = dataGV.SelectedRows[0].Cells[3].Value.ToString();
            txtbirthcity.Text = dataGV.SelectedRows[0].Cells[4].Value.ToString();
            txtaddress.Text = dataGV.SelectedRows[0].Cells[5].Value.ToString();
            txtmobile.Text = dataGV.SelectedRows[0].Cells[6].Value.ToString();
            txtemail.Text = dataGV.SelectedRows[0].Cells[7].Value.ToString();
            cmbgender.SelectedItem = dataGV.SelectedRows[0].Cells[8].Value.ToString();
            cmbedu.SelectedItem = dataGV.SelectedRows[0].Cells[9].Value.ToString();
            txtfield.Text = dataGV.SelectedRows[0].Cells[10].Value.ToString();
            gunaPictureBox1.Image = Image.FromFile(Application.StartupPath + "//picture//" + txtcode.Text + ".png");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string cs = "Data Source=(local);Initial Catalog=userregcs;Integrated Security=True";
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            if (radioButton1.Checked)
            {
                fdisable1();
                string com = "select * from tbluser where code='" + txtcode.Text + " ' ";
                SqlCommand cmd = new SqlCommand(com, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    r.Read();
                    txtcode.Text = r["code"].ToString();
                    txtname.Text = r["name"].ToString();
                    txtfamily.Text = r["family"].ToString();
                    txtDOB.Text = r["DOB"].ToString();
                    txtbirthcity.Text = r["birthcity"].ToString();
                    txtaddress.Text = r["address"].ToString();
                    txtmobile.Text = r["mobile"].ToString();
                    txtemail.Text = r["email"].ToString();
                    cmbgender.Text = r["gender"].ToString();
                    cmbedu.Text = r["education"].ToString();
                    txtfield.Text = r["field"].ToString();
                    gunaPictureBox1.Image = Image.FromFile(Application.StartupPath + "//picture//" + txtcode.Text + ".png");
                }
                else
                {
                    DialogResult res;
                    res = MessageBox.Show("There is No Person With This Code!!!", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.OK)
                    {
                        refresh();
                        btnNew_Click(sender, e);
                    }
                    else
                    {
                        refresh();
                        btnNew_Click(sender, e);
                        this.Close();
                    }
                }
                r.Close();
                conn.Close();
            }
            else if (radioButton2.Checked)
            {
                fdisable2();
                string com = "select * from tbluser where name='" + txtname.Text + " ' ";
                SqlCommand cmd = new SqlCommand(com, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    r.Read();
                    txtcode.Text = r["code"].ToString();
                    txtname.Text = r["name"].ToString();
                    txtfamily.Text = r["family"].ToString();
                    txtDOB.Text = r["DOB"].ToString();
                    txtbirthcity.Text = r["birthcity"].ToString();
                    txtaddress.Text = r["address"].ToString();
                    txtmobile.Text = r["mobile"].ToString();
                    txtemail.Text = r["email"].ToString();
                    cmbgender.Text = r["gender"].ToString();
                    cmbedu.Text = r["education"].ToString();
                    txtfield.Text = r["field"].ToString();
                    gunaPictureBox1.Image = Image.FromFile(Application.StartupPath + "//picture//" + txtcode.Text + ".png");
                }
                else
                {
                    DialogResult res;
                    res = MessageBox.Show("There is No Person With This Name!!!", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.OK)
                    {
                        refresh();
                        btnNew_Click(sender, e);
                    }
                    else
                    {
                        refresh();
                        btnNew_Click(sender, e);
                        this.Close();
                    }
                }
                r.Close();
                conn.Close();
            }
            else if (radioButton3.Checked)
            {
                fdisable3();
                string com = "select * from tbluser where mobile='" + txtmobile.Text + " ' ";
                SqlCommand cmd = new SqlCommand(com, conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    r.Read();
                    txtcode.Text = r["code"].ToString();
                    txtname.Text = r["name"].ToString();
                    txtfamily.Text = r["family"].ToString();
                    txtDOB.Text = r["DOB"].ToString();
                    txtbirthcity.Text = r["birthcity"].ToString();
                    txtaddress.Text = r["address"].ToString();
                    txtmobile.Text = r["mobile"].ToString();
                    txtemail.Text = r["email"].ToString();
                    cmbgender.Text = r["gender"].ToString();
                    cmbedu.Text = r["education"].ToString();
                    txtfield.Text = r["field"].ToString();
                    gunaPictureBox1.Image = Image.FromFile(Application.StartupPath + "//picture//" + txtcode.Text + ".png");
                }
                else
                {
                    DialogResult res;
                    res = MessageBox.Show("There is No Person With This Mobilenumber!!!", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.OK)
                    {
                        refresh();
                        btnNew_Click(sender, e);
                    }
                    else
                    {
                        refresh();
                        btnNew_Click(sender, e);
                        this.Close();
                    }
                }
                r.Close();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Please Choose one of Cases!!","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {


            string cs = "Data Source=(local);Initial Catalog=userregcs;Integrated Security=True";
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            string com = "update tbluser set name=@n , family=@fa, DOB=@db, birthcity=@bc , address=@add , mobile=@m, email=@em , gender=@g , education=@ed ,field=@fi   where code=@c ";
            SqlCommand cmd = new SqlCommand(com, conn);
            FileStream fs = File.OpenRead(Application.StartupPath + "//picture//" + txtcode.Text + ".png");
            byte[] arr = new byte[fs.Length];
            fs.Read(arr, 0, (int)arr.Length);
            fs.Close();

            cmd.Parameters.AddWithValue("@c", txtcode.Text);
            cmd.Parameters.AddWithValue("@n", txtname.Text);
            cmd.Parameters.AddWithValue("@fa", txtfamily.Text);
            cmd.Parameters.AddWithValue("@db", txtDOB.Text);
            cmd.Parameters.AddWithValue("@bc", txtbirthcity.Text);
            cmd.Parameters.AddWithValue("@add", txtaddress.Text);
            cmd.Parameters.AddWithValue("@m", txtmobile.Text);
            cmd.Parameters.AddWithValue("@em", txtemail.Text);
            cmd.Parameters.AddWithValue("@g", cmbgender.SelectedItem);
            cmd.Parameters.AddWithValue("@ed", cmbedu.SelectedItem);
            cmd.Parameters.AddWithValue("@fi", txtfield.Text);
            cmd.Parameters.AddWithValue("@pic", arr);
           
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Information Edited Successfully!", "Acclamation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresh();
                btnNew_Click(sender, e);
            }
            catch
            { 
                DialogResult res;
                res = MessageBox.Show("Error in Editing Information!!!", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (res == DialogResult.OK)
                {
                    refresh();
                    btnNew_Click(sender, e);
                }
                else
                {
                    refresh();
                    btnNew_Click(sender, e);
                    this.Close();
                }
            }
            conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string cs = "Data Source=(local);Initial Catalog=userregcs;Integrated Security=True";
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            string com = "delete  tbluser where code=@c ";
            SqlCommand cmd = new SqlCommand(com, conn);
            cmd.Parameters.AddWithValue("@c",txtcode.Text);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Information deleted successfully");
                refresh();  
                btnNew_Click(sender,e);
            }
            catch
            {
                MessageBox.Show("Error in deleting Information!");
                refresh();
                btnNew_Click(sender, e);
            }
            conn.Close();
        }

        private void txtcode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue ==13)
            {
                txtname.Focus();
            }
        }

        private void txtname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtfamily.Focus();
            }
        }

        private void txtfamily_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtDOB.Focus();
            }
        }

        private void txtDOB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtbirthcity.Focus();
            }
        }

        private void txtbirthcity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtaddress.Focus();
            }
        }

        private void txtaddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtmobile.Focus();
            }
        }

        private void txtmobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtemail.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtfield.Focus();
            }
        }

        private void txtfield_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                cmbgender.Focus();
            }
        }

        private void cmbgender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                cmbedu.Focus();
            }
        }

        private void cmbedu_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fdisable1();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fdisable2();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            fdisable3();

        }
    }
}
