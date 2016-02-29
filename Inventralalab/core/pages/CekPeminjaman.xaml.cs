using Inventralalab.core;
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

        List<Alat> items;
        private void RebindItems() {
            string query = "SELECT * FROM inventory JOIN borrowers ON inventralalab.inventory.id_peminjam = inventralalab.borrowers.id WHERE id_peminjam IS NOT NULL";
            try {
                using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        items = new List<Alat>();
                        foreach (DataRow row in dataTable.Rows) {
                            Alat alat = new Alat(
                                (string)row["id"],
                                (string)row["nama"],
                                (DateTime?)row["tanggal_mulai"],
                                (DateTime?)row["tanggal_selesai"],
                                (bool)row["kondisi_alat"],
                                (string)row["lokasi"]
                                );
                            items.Add(alat);
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

        private void Button_Selesai_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dataContext = button.DataContext;
            int index = listbox_Peminjam.Items.IndexOf(dataContext);

            string inventory_id = items[index].Id;
            string query = "SELECT * from inventory WHERE id = @id";
            try {
                using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd)) {
                        DataTable dataRow = new DataTable();
                        dataAdapter.Fill(dataRow);
                    }
                }
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
