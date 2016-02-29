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
using Inventralalab.core;

namespace Inventralalab.Pages
{
    /// <summary>
    /// Interaction logic for EditAlat.xaml
    /// </summary>
    public partial class EditAlat : UserControl
    {
        public  class KV {
            public KV(string key, int value) {
                Key = key;
                Value = value;
            }
            public int Value { get; set; }
            public string Key { get; set; }
        }
        public List<KV> status;
        private Alat alat;
        public EditAlat(Alat alat)
        {
            InitializeComponent();
            this.alat = alat;

            status = new List<KV>();
            status.Add(new KV("Baik", 1));
            status.Add(new KV("Rusak", 0));
            comboBox_Status.ItemsSource = status;
            comboBox_Status.DisplayMemberPath = "Key";
            comboBox_Status.SelectedValuePath = "Value";
            comboBox_Status.SelectedValue = (alat.KondisiAlat) ? 1 : 0;

            textbox_Laboratorium.Text = alat.Lokasi;

            string query = "SELECT * FROM master_inventory_type";
            using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                try {
                    DataTable dataSet = new DataTable();
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd)) {
                        dataAdapter.Fill(dataSet);
                        comboBox_Jenis_Barang.ItemsSource = dataSet.DefaultView;
                        comboBox_Jenis_Barang.DisplayMemberPath = dataSet.Columns["nama"].ToString();
                        comboBox_Jenis_Barang.SelectedValuePath = dataSet.Columns["id"].ToString();
                        comboBox_Jenis_Barang.SelectedValue = alat.IdJenis;
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
            string jenisBarang = comboBox_Jenis_Barang.SelectedValue.ToString();
            string lokasi = textbox_Laboratorium.Text;
            int kondisiBarang = int.Parse(comboBox_Status.SelectedValue.ToString());

            Console.WriteLine(jenisBarang);
            Console.WriteLine(lokasi);
            Console.WriteLine(kondisiBarang);

            string query = "UPDATE inventory SET id_jenis=@id_jenis, lokasi=@lokasi, kondisi_alat=@kondisi_alat WHERE id=@id";
            using (MySqlCommand cmd = new MySqlCommand(query, db.ConnectionManager.Connection)) {
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", alat.Id);
                cmd.Parameters.AddWithValue("@id_jenis", jenisBarang);
                cmd.Parameters.AddWithValue("@lokasi", lokasi);
                cmd.Parameters.AddWithValue("@kondisi_alat", kondisiBarang);

                try {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Pengubahan Alat Berhasil");
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
