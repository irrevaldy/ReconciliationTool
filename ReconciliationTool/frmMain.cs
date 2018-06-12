using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReconciliationTool
{
    public partial class frmMain : Form
    {
        
        private frmAbout frmAbout = new frmAbout();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            rtLoadForm();
        }

        public void rtLoadForm()
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("                   Are you sure?", "Close Application", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            String szExcelMSOLEDB = Properties.ReconciliationTool.Default["EXCELOLEDB"].ToString();
            conn = "Provider=" + szExcelMSOLEDB + ";Data Source=" +
                               fileName +
                               ";Extended Properties='Excel 12.0;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text';";

           /* if (fileExt.CompareTo(".xls") == 0)//compare the extension of the file
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';";//for below excel 2007
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";//for above excel 2007
            */
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con);//here we read data from sheet1
                    oleAdpt.Fill(dtexcel);//fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return dtexcel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label_total.Text = "Total : 0";
            label_found.Text = "Found : 0";
            label_not_found.Text = "Not Found : 0";

            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            {
                filePath = file.FileName; //get the path of the file  
                textBox1.Text = filePath;
                fileExt = Path.GetExtension(filePath); //get the file extension  
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt); //read excel file  
                        dataGridView1.Visible = true;
                        dataGridView1.DataSource = dtExcel;
                        int numRows = dataGridView1.Rows.Count;

                        label_total.Text = "Total : " + numRows.ToString();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vdFillTermList();
        }

        private void vdFillTermList()
        {
            int totalfound = 0;
            int totalnotfound = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                string FTRX_TS = r.Cells[0].Value.ToString();
                string FTID = r.Cells[1].Value.ToString();
                string FMID = r.Cells[2].Value.ToString();
                string FSTORECODE = r.Cells[3].Value.ToString();
                string FAPPRCODE = r.Cells[4].Value.ToString();
                string FRRN = r.Cells[5].Value.ToString();
                string FPREPAIDCARDNUM = r.Cells[6].Value.ToString();
                string FCARDNUM = r.Cells[7].Value.ToString();
                string FSTATUS = r.Cells[8].Value.ToString();
                string FAMOUNT = r.Cells[9].Value.ToString();
                string FFOUND = r.Cells[10].Value.ToString();
                
              
                /*string FGATEWAY_TS = "20180606143229";
                string FTID = "12001417";
                string FMID = "100012000014";
                string FSTORECODE = "TRIE";
                string FAPPRCODE = "";
                string FRRN = "";
                string FPREPAIDCARDNUM = "";
                string FAMOUNT = "2500";*/

                //MessageBox.Show("FGATEWAY_TS: " + FGATEWAY_TS + "\n" + "FTID: " + FTID + "\n" + "FMID: " + FMID + "\n" + "FSTORECODE: " + FSTORECODE + "\n" + "FAPPRCODE: " + FAPPRCODE + "\n" + "FRRN: " + FRRN + "\n" + "FPREPAIDCARDNUM: " + FPREPAIDCARDNUM + "\n" + "FAMOUNT: " + FAMOUNT);

                using (SqlConnection conn = new SqlConnection())
                {
                    try
                    {
                        conn.ConnectionString = "Data Source=192.168.202.102;Initial Catalog=DbWDGatewayIDM;User ID=sa;Password=pvs1909~";
                        conn.Open();

                        using (SqlCommand command = new SqlCommand("spVIDM_SearchRecon", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            SqlParameter paramFTRX_TS = new SqlParameter("@FTRX_TS", SqlDbType.VarChar, 50) { Value = FTRX_TS };
                            SqlParameter paramFTID = new SqlParameter("@FTID", SqlDbType.VarChar, 50) { Value = FTID };
                            SqlParameter paramFMID = new SqlParameter("@FMID", SqlDbType.VarChar, 50) { Value = FMID };
                            SqlParameter paramFSTORECODE = new SqlParameter("@FSTORECODE", SqlDbType.VarChar, 50) { Value = FSTORECODE };
                            SqlParameter paramFAPPRCODE = new SqlParameter("@FAPPRCODE", SqlDbType.VarChar, 50) { Value = FAPPRCODE };
                            SqlParameter paramFRRN = new SqlParameter("@FRRN", SqlDbType.VarChar, 50) { Value = FRRN };
                            SqlParameter paramFPREPAIDCARDNUM = new SqlParameter("@FPREPAIDCARDNUM", SqlDbType.VarChar, 50) { Value = FPREPAIDCARDNUM };
                            SqlParameter paramFCARDNUM = new SqlParameter("@FCARDNUM", SqlDbType.VarChar, 50) { Value = FCARDNUM };
                            SqlParameter paramFAMOUNT = new SqlParameter("@FAMOUNT", SqlDbType.VarChar, 50) { Value = FAMOUNT };
                            SqlParameter paramFSTATUS = new SqlParameter("@FSTATUS", SqlDbType.VarChar, 50) { Value = FSTATUS };

                            command.Parameters.Add(paramFTRX_TS);
                            command.Parameters.Add(paramFTID);
                            command.Parameters.Add(paramFMID);
                            command.Parameters.Add(paramFSTORECODE);
                            command.Parameters.Add(paramFAPPRCODE);
                            command.Parameters.Add(paramFRRN);
                            command.Parameters.Add(paramFPREPAIDCARDNUM);
                            command.Parameters.Add(paramFCARDNUM);
                            command.Parameters.Add(paramFAMOUNT);
                            command.Parameters.Add(paramFSTATUS);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                Console.WriteLine("FTRX_TS\tFTID\t\tFMID\t\tFSTORECODE\t\tFAPPRCODE\t\tFRRN\t\tFPREPAIDCARDNUM\t\tFCARDNUM\t\tFAMOUNT\t\tFSTATUS");
                                while (reader.Read())
                                {
                                    Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t | {4} \t | {5} \t | {6} \t | {7}\t | {8}\t | {9}",
                                        reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], reader[9]));
                                }

                                // if the result set is not NULL
                                if (reader.HasRows)
                                {
                                    r.DefaultCellStyle.BackColor = Color.SpringGreen;
                                    r.Cells[10].Value = "FOUND";
                                    totalfound++;
                                    this.label_found.Text = "Found : " + totalfound.ToString();
                                }
                                else
                                {
                                    r.DefaultCellStyle.BackColor = Color.Yellow;
                                    r.Cells[10].Value = "NOT FOUND";
                                    totalnotfound++;
                                    this.label_not_found.Text = "Not Found : " + totalnotfound.ToString();
                                }
                            }

                            /*object nullableValue = command.ExecuteScalar();

                            if (nullableValue == null || nullableValue == DBNull.Value)
                            {
                                r.DefaultCellStyle.BackColor = Color.Red;
                            }
                            else
                            {
                                r.DefaultCellStyle.BackColor = Color.Green;
                            }*/
                        }
                    }
                    finally
                    {
                        conn.Close();
                        
                     }
                }
            }
            //this.label_found.Text = "Found : " + totalfound.ToString();
            //this.label_not_found.Text = "Not Found : " + totalnotfound.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel files (*.xlsx)|*.xlsx|Excel Documents (*.xls)|*.xls";
            sfd.FileName = "export.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //ToCsV(dataGridView1, @"c:\export.xls");
                ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
            }*/

            Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Excel._Worksheet worksheet = null;
            app.Visible = false;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Records";

            try
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1] = "";
                        }
                    }
                }

                //Getting the location and file name of the excel to save from user. 
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                //app.Quit();
                workbook = null;
                worksheet = null;
                app.Visible = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout.ShowDialog();
        }
    }
}
