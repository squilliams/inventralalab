using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Inventralalab.Pages
{
    /// <summary>
    /// Interaction logic for Cek_Peminjaman.xaml
    /// </summary>
    public partial class CekPeminjaman : UserControl
    {
        public CekPeminjaman()
        {
            InitializeComponent();
            RebindItems();
        }

        List<string> itemIds;
        private void RebindItems() {
            string query = "SELECT * FROM inventory JOIN borrowers WHERE id_peminjam IS NOT NULL";
            try {
                using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        itemIds = new List<string>();
                        foreach (DataRow row in dataTable.Rows) {
                            string id = row["id"].ToString();
                            itemIds.Add(id);
                        }
                        listbox_Peminjam.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Peminjaman_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PeminjamanAlat());
        }

        private void Button_Cek_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CekPeminjaman());
        }

        private void Button_Manajemen_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ManajemenAlat());
        }

        private void Button_Statistik_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new StatistikPeminjaman());
        }

        private void Button_Ubah_Status_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
