using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Inventralalab.Pages
{
    /// <summary>
    /// Interaction logic for Peminjaman_Alat.xaml
    /// </summary>
    public partial class PeminjamanAlat : UserControl
    {
        
        public PeminjamanAlat()
        {
            InitializeComponent();
            textbox_Nama.Text = "Masukkan nama";
            textbox_Nomor_HP.Text = "Masukkan Nomor HP";
            
            string query = "SELECT * FROM master_inventory_type";
            using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                try {
                    DataTable dataTable = new DataTable();
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd)) {
                        dataAdapter.Fill(dataTable);
                        comboBox_Jenis_Barang.DataContext = dataTable;
                        comboBox_Jenis_Barang.DisplayMemberPath = dataTable.Columns["nama"].ToString();
                        comboBox_Jenis_Barang.SelectedValuePath = dataTable.Columns["id"].ToString();
                    }
                }
                catch (MySqlException ex) {
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

        private void Button_Selesai_Click(object sender, RoutedEventArgs e)
        {

            /* Informasi Peminjam */
            string nama_peminjam = textbox_Nama.Text;
            string no_hp = textbox_Nomor_HP.Text;
            string jenis_peminjam = null;
            if (radioButton_Dosen.IsChecked != null && radioButton_Dosen.IsChecked == true)
                jenis_peminjam = (string)Application.Current.FindResource("jenisPeminjamDosen");
            if (radioButton_Mahasiswa.IsChecked != null && radioButton_Mahasiswa.IsChecked == true)
                jenis_peminjam = (string)Application.Current.FindResource("jenisPeminjamMahasiswa");
            if (radioButton_Pegawai.IsChecked != null && radioButton_Pegawai.IsChecked == true)
                jenis_peminjam = (string)Application.Current.FindResource("jenisPeminjamPegawai");

            string id_jenis = comboBox_Jenis_Barang.SelectedValue.ToString();
            DateTime? tanggal_mulai = Date_Peminjaman.SelectedDate;
            DateTime? tanggal_selesai = Date_Pengembalian.SelectedDate;

            string query = "SELECT * FROM inventory WHERE id_jenis = @id_jenis";
            using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id_jenis", id_jenis);

                using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd)) {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    
                }
            }
        }

        private void textbox_Jumlah_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text); 
        }
    }
}
