using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReconciliationTool
{
    public class SqlClass
    {
        private SqlConnection sqlConn = new SqlConnection();
        private SqlCommand sqlCmd = new SqlCommand();
        private SqlTransaction sqlTrx;

        private SqlDataReader sqlDr;
        private SqlBulkCopy sqlBulk;

        public SqlClass()
        {

        }

        public void vdInitSqlDataReader()
        {
            if (sqlDr != null) sqlDr.Close();
        }
    }
}
