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
        private frmDBConfig frmDBConfig = new frmDBConfig();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            rtLoadForm();
            toolStripStatusLabel1.Text = "Ready";
            this.WindowState = FormWindowState.Maximized;
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
            textBox1.Text = "";
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
           
            label_total.Text = "Total : 0";
            label_found.Text = "Found : 0";
            label_not_found.Text = "Not Found : 0";

            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            {
                toolStripStatusLabel1.Text = "Reading File";
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
                        toolStripStatusLabel1.Text = "Uploaded " + numRows + " row(s)";

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = "Wrong file type";
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                    toolStripStatusLabel1.Text = "Ready";
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Processing. Please wait..";
            vdFillTermList();
            toolStripStatusLabel1.Text = "Done";
        }

        private void vdFillTermList()
        {
            
            int totalfound = 0;
            int totalnotfound = 0;
            int row = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
               
                string FTRX_TS = r.Cells[0].Value.ToString().Trim();
                string FTID = r.Cells[1].Value.ToString().Trim();
                string FMID = r.Cells[2].Value.ToString().Trim();
                string FSTORECODE = r.Cells[3].Value.ToString().Trim();
                string FAPPRCODE = r.Cells[4].Value.ToString().Trim();
                string FRRN = r.Cells[5].Value.ToString().Trim();
                string FPREPAIDCARDNUM = r.Cells[6].Value.ToString();
                string FCARDNUM = r.Cells[7].Value.ToString().Trim();
                string FSTATUS = r.Cells[8].Value.ToString().Trim();
                string FAMOUNT = r.Cells[9].Value.ToString().Trim();
                string FFOUND = r.Cells[10].Value.ToString().Trim();
                
              
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
                        String DBServer = Properties.ReconciliationTool.Default["DBServer"].ToString();
                        String DBCatalog = Properties.ReconciliationTool.Default["DBCatalog"].ToString();
                        String DBUsername = Properties.ReconciliationTool.Default["DBUsername"].ToString();
                        String DBPassword = Properties.ReconciliationTool.Default["DBPassword"].ToString();

                        conn.ConnectionString = "Data Source=" + DBServer + ";Initial Catalog=" + DBCatalog + ";User ID=" + DBUsername + ";Password=" + DBPassword;
                        conn.Open();

                        using (SqlCommand command = new SqlCommand("spVIDM_SearchRecon", conn))
                        {
                            row++;
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

                            //Log
                          
                            //Console.WriteLine("Excel row: " + row + ". " + FTRX_TS + "|" + FTID + "|" + FMID + "|" + FSTORECODE + "|" + FAPPRCODE + "|" + FRRN + "|" + FPREPAIDCARDNUM + "|" + FCARDNUM + "|" + FAMOUNT + "|" + FSTATUS);

                            StringBuilder sb = new StringBuilder();
                            sb.Append("Excel row: " + row + ". " + FTRX_TS + "|" + FTID + "|" + FMID + "|" + FSTORECODE + "|" + FAPPRCODE + "|" + FRRN + "|" + FPREPAIDCARDNUM + "|" + FCARDNUM + "|" + FAMOUNT + "|" + FSTATUS);
                            sb.Append(Environment.NewLine);

                            // flush every 20 seconds as you do it
                            string folderName = @"C://ReconciliationTool";
                            string pathString = System.IO.Path.Combine(folderName, "Log");
                            System.IO.Directory.CreateDirectory(pathString);

                            File.AppendAllText(pathString + "/log.txt", sb.ToString());
                            sb.Clear();

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
                                String statusLine = "";
                                //Console.WriteLine("FTRX_TS\tFTID\t\tFMID\t\tFSTORECODE\t\tFAPPRCODE\t\tFRRN\t\tFPREPAIDCARDNUM\t\tFCARDNUM\t\tFAMOUNT\t\tFSTATUS");
                                while (reader.Read())
                                {
                                    statusLine = String.Format("{0} \t | {1} \t | {2} \t | {3} \t | {4} \t | {5} \t | {6} \t | {7}\t | {8}\t | {9}",
                                        reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], reader[9]);
                                    //Console.WriteLine("Result from sp: " + statusLine);

               
                                }
                                // if the result set is not NULL
                                if(reader.HasRows && statusLine != "")
                                //if (statusLine != "")
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
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
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
            toolStripStatusLabel1.Text = "Exporting..";
            Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Excel._Worksheet worksheet = null;
            app.Visible = false;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet.Columns["G"].NumberFormat = "0";
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
                            worksheet.Cells[i + 2, j + 1] = "\t" + dataGridView1.Rows[i].Cells[j].Value.ToString();

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
                toolStripStatusLabel1.Text = "Done";
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

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDBConfig.ShowDialog();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
