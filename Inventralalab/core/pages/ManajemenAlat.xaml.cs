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
    /// Interaction logic for Manajemen_Alat.xaml
    /// </summary>
    public partial class ManajemenAlat : UserControl
    {
        List<Alat> items;
        public ManajemenAlat()
        {
            InitializeComponent();
            RebindItems();
        }

        private void RebindItems() {
            string query = "SELECT *, (CASE WHEN(kondisi_alat = 1) THEN \"Baik\" ELSE \"Rusak\" END) as kondisi FROM inventory JOIN master_inventory_type ON inventralalab.inventory.id_jenis = inventralalab.master_inventory_type.id";
            try {
                using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        items = new List<Alat>();
                        foreach (DataRow row in dataTable.Rows) {
                            Alat alat = new Alat(
                                (string)row["id"],
                                (string)row["id_jenis"],
                                (string)row["nama"],
                                (bool)row["kondisi_alat"],
                                (string)row["lokasi"]
                                );
                            items.Add(alat);
                        }
                        listBox_Alat.ItemsSource = items;
                    }
                }
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteItem(string id) {
            try {
                string query = "DELETE FROM inventory WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delete Successful");
                }
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
            }
            RebindItems();
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

        private void Button_Ubah_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dataContext = button.DataContext;
            int index = listBox_Alat.Items.IndexOf(dataContext);

            Alat alat = items[index];
            Switcher.Switch(new EditAlat(alat));
        }

        private void Button_Hapus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dataContext = button.DataContext;
            int index = listBox_Alat.Items.IndexOf(dataContext);

            string inventory_id = items[index].Id;
            MessageBoxResult messageBoxResult = MessageBox.Show("Apakah anda yakin?", "Konfirmasi Penghapusan", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
                DeleteItem(inventory_id);
        }

        private void Button_Tambah_Alat_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TambahAlat());
        }
        private void Button_Tambah_Jenis_Alat_Click(object sender, RoutedEventArgs e) {
            Switcher.Switch(new TambahJenisAlat());
        }
    }
}
