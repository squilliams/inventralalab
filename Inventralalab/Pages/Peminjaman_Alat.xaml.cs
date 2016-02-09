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

namespace Inventralalab.Pages
{
    /// <summary>
    /// Interaction logic for Peminjaman_Alat.xaml
    /// </summary>
    public partial class Peminjaman_Alat : UserControl
    {
        public Peminjaman_Alat()
        {
            InitializeComponent();
            textbox_Nama.Text = "Masukkan nama";
            textbox_Nomor_HP.Text = "Masukkan Nomor HP";
        }

        private void Button_Peminjaman_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Peminjaman_Alat());
        }

        private void Button_Cek_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Cek_Peminjaman());
        }

        private void Button_Manajemen_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Manajemen_Alat());
        }

        private void Button_Statistik_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Statistik_Peminjaman());
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

        private void Button_Selesai_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
