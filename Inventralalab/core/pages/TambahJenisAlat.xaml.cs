using MySql.Data.MySqlClient;
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
    /// Interaction logic for TambahJenisAlat.xaml
    /// </summary>
    public partial class TambahJenisAlat : UserControl {
        public TambahJenisAlat() {
            InitializeComponent();
        }

        private void Button_Simpan_Click(object sender, RoutedEventArgs e) {
            string invId = textbox_Id.Text;
            string nama = textbox_Jenis_Barang.Text;

            string query = "INSERT INTO master_inventory_type(id, nama) VALUES(@id, @nama)";
            using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", invId);
                cmd.Parameters.AddWithValue("@nama", nama);
                try {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Penambahan Alat Berhasil");
                }
                catch (MySqlException ex) {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void Button_Peminjaman_Click(object sender, RoutedEventArgs e) {
            Switcher.Switch(new PeminjamanAlat());
        }

        private void Button_Cek_Click(object sender, RoutedEventArgs e) {
            Switcher.Switch(new CekPeminjaman());
        }

        private void Button_Manajemen_Click(object sender, RoutedEventArgs e) {
            Switcher.Switch(new ManajemenAlat());
        }

        private void Button_Statistik_Click(object sender, RoutedEventArgs e) {
            Switcher.Switch(new StatistikPeminjaman());
        }

    }
}
