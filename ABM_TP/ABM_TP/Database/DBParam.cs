using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace ABM_TP.Database
{
    class DBParam
    {
        private string server = "localhost";
        private string port = "3306";
        private string dbname = "tp_zara";
        private string uid = "root";
        private string password = "";


        public string genStrConn()
        {

            string connString = "SERVER=" + server + ";" + "DATABASE=" +
            dbname + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            return connString;

        }

        // TODO: Check empty fields

    }

}
