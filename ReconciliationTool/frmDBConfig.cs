using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReconciliationTool
{
    public partial class frmDBConfig : Form
    {
        public frmDBConfig()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Crypto cryptObj = new Crypto();
                Utility utilObj = new Utility();

                Properties.ReconciliationTool.Default["DBServer"] = txtDBServer.Text;
                Properties.ReconciliationTool.Default["DBCatalog"] = txtDBCatalog.Text;
                Properties.ReconciliationTool.Default["DBUsername"] = txtDBUsername.Text;
                Properties.ReconciliationTool.Default["DBPassword"] = txtDBPassword.Text;

                Properties.ReconciliationTool.Default.Save();
                lblSaveResult.Text = "Save configuration OK";
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
                lblSaveResult.Text = ex.Message;
            }
        }

        private void frmDBConfig_Load(object sender, EventArgs e)
        {
            Crypto cryptObj = new Crypto();
            Utility utilObj = new Utility();

            txtDBServer.Text = Properties.ReconciliationTool.Default["DBServer"].ToString();
            txtDBCatalog.Text = Properties.ReconciliationTool.Default["DBCatalog"].ToString();
            txtDBUsername.Text = Properties.ReconciliationTool.Default["DBUsername"].ToString();
            //txtDBPassword.Text = Properties.ReconciliationTool.Default["DBPassword"].ToString();
           
            String DBPassword = Properties.ReconciliationTool.Default["DBPassword"].ToString();
          
            txtDBPassword.Text = utilObj.szByteToHex(cryptObj.btEncryptConfig(DBPassword));
          
            lblSaveResult.Text = "";
        }

        private void txtDBServer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDBCatalog_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDBUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDBPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblSaveResult_Click(object sender, EventArgs e)
        {

        }
    }
}
