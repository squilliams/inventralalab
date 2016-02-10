using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventralalab.db {
    public class ConnectionManager {
        private ConnectionManager() {
            string connString = "server=127.0.0.1; port=5715; database=inventralalab;";
            // TODO: input username and password
            string username = null;
            string password = null;
            if (username != null) {
                connString += "uid=" + username + ";";
                if (password != null) connString += "pwd=" + password + ";";
                try {
                    this.connection = new MySql.Data.MySqlClient.MySqlConnection();
                    this.connection.ConnectionString = connString;
                    this.connection.Open();
                }
                catch (MySql.Data.MySqlClient.MySqlException ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
            }
        }
        

        private static ConnectionManager instance;
        private MySql.Data.MySqlClient.MySqlConnection connection;

        public static MySql.Data.MySqlClient.MySqlConnection Connection {
            get {
                if (instance == null) ConnectionManager.instance = new ConnectionManager();
                return ConnectionManager.instance.connection;
            }
        }
    }
}
