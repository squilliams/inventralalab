using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventralalab.db {
    public class Connection {
        private Connection() {
            string connString = "server=127.0.0.1; port=5715; database=inventralalab;";
            string username = Properties.Settings.Default.db_user;
            string password = Properties.Settings.Default.db_pass;
            if (username != null) {
                connString += "uid=" + username + ";";
                if (password != null) connString += "pwd=" + password + ";";
                try {
                    conn = new MySql.Data.MySqlClient.MySqlConnection();
                    conn.ConnectionString = connString;
                    conn.Open();
                }
                catch (MySql.Data.MySqlClient.MySqlException ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
            }
        }
        private MySql.Data.MySqlClient.MySqlConnection conn;

        private static Connection instance;
        public static Connection Instance {
            get {
                if (instance == null) Connection.instance = new Connection();
                return Connection.instance;
            }
            private set { 
                Connection.instance = value; 
            }
        }
    }
}
