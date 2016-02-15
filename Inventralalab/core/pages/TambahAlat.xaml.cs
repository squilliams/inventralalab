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
using MySql.Data.MySqlClient;
using System.Data;

namespace Inventralalab.Pages
{
    /// <summary>
    /// Interaction logic for TambahAlat.xaml
    /// </summary>
    public partial class TambahAlat : UserControl
    {
        public TambahAlat()
        {
            InitializeComponent();
            string query = "SELECT * FROM master_inventory_type";
            using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                try {
                    DataTable dataSet = new DataTable();
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd)) {
                        dataAdapter.Fill(dataSet);
                        comboBox_Jenis_Barang.ItemsSource = dataSet.DefaultView;
                        comboBox_Jenis_Barang.DisplayMemberPath = dataSet.Columns["nama"].ToString();
                        comboBox_Jenis_Barang.SelectedValuePath = dataSet.Columns["id"].ToString();
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException ex) {
                    MessageBox.Show(ex.Message);
                }
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

        private void Button_Simpan_Click(object sender, RoutedEventArgs e)
        {
            string invId = textbox_Id.Text;
            string jenisBarang = comboBox_Jenis_Barang.SelectedValue.ToString();
            string lokasi = textbox_Laboratorium.Text;

            string query = "INSERT INTO inventory(id, id_jenis, kondisi_alat, lokasi) VALUES(@id, @id_jenis, TRUE, @lokasi)";
            using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", invId);
                cmd.Parameters.AddWithValue("@id_jenis", jenisBarang);
                cmd.Parameters.AddWithValue("@lokasi", lokasi);

                try {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Penambahan Alat Berhasil");
                }
                catch (MySqlException ex) {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
