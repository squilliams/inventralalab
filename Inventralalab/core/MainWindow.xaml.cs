﻿using System;
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
using System.Windows.Shapes;
using Inventralalab.Pages;

namespace Inventralalab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Switcher.pageSwitcher = this;
            if (db.ConnectionManager.Connection != null) {
                Switcher.Switch(new PeminjamanAlat());
            }
            else {
                Switcher.Switch(new DatabaseLogin());
            }
        }

        public void Navigate(UserControl nextPage)
        {
            Content = nextPage;
        }
    }
}
