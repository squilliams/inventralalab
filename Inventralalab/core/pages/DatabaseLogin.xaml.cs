using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inventralalab.Pages {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class DatabaseLogin : UserControl {
        public DatabaseLogin() {
            InitializeComponent();
        }

        private void button_Connect_Click(object sender, RoutedEventArgs e) {
            string username = textBox_Username.Text;
            string password = textBox_Password.Text;

            Properties.Settings.Default["db_user"] = username;
            Properties.Settings.Default["db_pass"] = password;

            if (db.ConnectionManager.Connection != null) {
                Properties.Settings.Default.Save();
                Switcher.Switch(new PeminjamanAlat());
            }
        }
    }
}
