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
    /// Interaction logic for Manajemen_Alat.xaml
    /// </summary>
    public partial class ManajemenAlat : UserControl
    {
        public ManajemenAlat()
        {
            InitializeComponent();
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

        }

        private void Button_Hapus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Tambah_Alat_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TambahAlat());
        }
    }
}
