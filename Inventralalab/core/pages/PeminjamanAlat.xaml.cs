﻿using System;
using System.Data;
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
            MySql.Data.MySqlClient.MySqlDataAdapter dataAdapter =
                new MySql.Data.MySqlClient.MySqlDataAdapter(query, db.ConnectionManager.Connection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            comboBox_Jenis_Barang.ItemsSource = dataSet.Tables[0].DefaultView;
            comboBox_Jenis_Barang.DisplayMemberPath = dataSet.Tables[0].Columns["nama"].ToString();
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

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void radioButton_Mahasiswa_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioButton_Dosen_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioButton_Pegawai_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
