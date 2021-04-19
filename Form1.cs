using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplikasi_Nadira
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtFullName.Focus();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtFullName.Focus();
                this.appData.datasiswaDira.AdddatasiswaDiraRow(this.appData.datasiswaDira.NewdatasiswaDiraRow());
                datasiswaDiraBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                datasiswaDiraBindingSource.ResetBindings(false);
            }
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                    dataGridView.DataSource = datasiswaDiraBindingSource;
                else
                {
                    var query = from o in this.appData.datasiswaDira
                                where o.FullName.Contains(txtSearch.Text) || o.PhoneNumber == txtSearch.Text || o.Email == txtSearch.Text || o.Address.Contains(txtSearch.Text)
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    datasiswaDiraBindingSource.RemoveCurrent();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            datasiswaDiraBindingSource.ResetBindings(false);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                datasiswaDiraBindingSource.EndEdit();
                datasiswaDiraTableAdapter.Update(this.appData.datasiswaDira);
                panel.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                datasiswaDiraBindingSource.ResetBindings(false);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.datasiswaDira' table. You can move, or remove it, as needed.
            this.datasiswaDiraTableAdapter.Fill(this.appData.datasiswaDira);
            datasiswaDiraBindingSource.DataSource = this.appData.datasiswaDira;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using(OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
