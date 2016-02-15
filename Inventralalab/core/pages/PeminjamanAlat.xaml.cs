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

        private void Button_Selesai_Click(object sender, RoutedEventArgs e) {
            string id_jenis = comboBox_Jenis_Barang.SelectedValue.ToString();
            int jumlah = int.Parse(textbox_Jumlah.Text);
            
            List<Inventory> inventory = GetInventory(id_jenis);
            if (inventory.Count < jumlah) {
                MessageBox.Show("Jumlah barang yang tersedia tidak mencukupi");
            }
            else {
                /* Informasi Peminjaman */
                string id_peminjam = textbox_Id.Text;
                string nama_peminjam = textbox_Nama.Text;
                string no_hp = textbox_Nomor_HP.Text;

                string jenis_peminjam = null;
                if (radioButton_Dosen.IsChecked != null && radioButton_Dosen.IsChecked == true)
                    jenis_peminjam = (string)Application.Current.FindResource("jenisPeminjamDosen");
                if (radioButton_Mahasiswa.IsChecked != null && radioButton_Mahasiswa.IsChecked == true)
                    jenis_peminjam = (string)Application.Current.FindResource("jenisPeminjamMahasiswa");
                if (radioButton_Pegawai.IsChecked != null && radioButton_Pegawai.IsChecked == true)
                    jenis_peminjam = (string)Application.Current.FindResource("jenisPeminjamPegawai");

                DateTime? tanggal_mulai = Date_Peminjaman.SelectedDate;
                DateTime? tanggal_selesai = Date_Pengembalian.SelectedDate;

                Borrower borrower = new Borrower(id_peminjam, nama_peminjam, jenis_peminjam, no_hp);
                for (var index = 0; index < jumlah; ++index) {
                    Inventory item = inventory[index];
                    BookItem(item, borrower, tanggal_mulai, tanggal_selesai);
                }
            }
        }

        private bool CheckBorrowerExist(Borrower borrower) {
            try {
                string query = "SELECT Count(*) FROM borrowers WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@id", borrower.Id);
                    long result = (long)cmd.ExecuteScalar();
                    return result > 0;
                }
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void BookItem(Inventory item, Borrower borrower, DateTime? tanggal_mulai, DateTime? tanggal_selesai) {
            if (!CheckBorrowerExist(borrower)) {
                try {
                    string query = "INSERT INTO borrowers(id, nama, jenis, no_telp) VALUES(@id, @nama, @jenis, @no_telp)";
                    using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                        cmd.Prepare();
                        cmd.Parameters.AddWithValue("@id", borrower.Id);
                        cmd.Parameters.AddWithValue("@nama", borrower.Nama);
                        cmd.Parameters.AddWithValue("@jenis", borrower.Jenis);
                        cmd.Parameters.AddWithValue("@no_telp", borrower.NomorTelp);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex) {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            try {
                string query = "UPDATE inventory SET id_peminjam = @id_peminjam, tanggal_mulai = @tanggal_mulai, tanggal_selesai = @tanggal_selesai";
                using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@id_peminjam", borrower.Id);
                    cmd.Parameters.AddWithValue("@tanggal_mulai", tanggal_mulai);
                    cmd.Parameters.AddWithValue("@tanggal_selesai", tanggal_selesai);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        class Inventory {
            public Inventory(string id) {
                Id = id;
            }
            public string Id { get; set; }
        }

        class Borrower {
            public Borrower(string id, string nama, string jenis, string nomorTelp) {
                Id = id;
                Nama = nama;
                Jenis = jenis;
                NomorTelp = nomorTelp;
            }
            public string Id { get; set; }
            public string Nama { get; set; }
            public string Jenis { get; set;  }
            public string NomorTelp { get; set; }
        }

        private List<Inventory> GetInventory(string id_jenis) {
            string query = "SELECT * FROM inventory WHERE id_jenis = @id_jenis AND id_peminjam IS NULL AND kondisi_alat = 1";
            List<Inventory> ret = new List<Inventory>();
            try {
                using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@id_jenis", id_jenis);

                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        foreach (DataRow row in dataTable.Rows) {
                            Inventory inventory = new Inventory(row["id"].ToString());
                            ret.Add(inventory);
                        }
                    }
                }
            }
            catch (MySqlException ex) {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }

        private void textbox_Jumlah_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text); 
        }
    }
}
