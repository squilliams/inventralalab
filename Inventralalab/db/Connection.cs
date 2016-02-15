using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using System.IO;

namespace Inventralalab.db {
    public class ConnectionManager {
        private ConnectionManager() {}
            
        private MySql.Data.MySqlClient.MySqlConnection OpenConnection(string username, string password) {
            string connString = "server=" + Properties.Settings.Default.db_server + ";"
                                + "uid=" + username + ";"
                                + "pwd=" + password + ";"
                                + "; port=5715; database=inventralalab;";

            try {
                connection = new MySql.Data.MySqlClient.MySqlConnection();
                connection.ConnectionString = connString;
                connection.Open();
                Properties.Settings.Default.Save();
                string[] files = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory + "../../db/tables");
                foreach (string file in files) {
                    MySqlScript script = new MySqlScript(connection, File.ReadAllText(file));
                    script.Execute();
                }
                
                return connection;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private static ConnectionManager instance = null;
        private MySqlConnection connection;

        public static MySqlConnection Connection {
            get {
                if (instance == null) ConnectionManager.instance = new ConnectionManager();
                if (instance.connection == null) {
                    instance.connection = instance.OpenConnection(Properties.Settings.Default.db_user,
                        Properties.Settings.Default.db_pass);
                }
                return instance.connection;
            }
        }
    }
}
