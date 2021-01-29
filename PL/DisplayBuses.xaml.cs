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
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayBuses.xaml
    /// </summary>
    public partial class DisplayBuses : Window
    {
        static IBL bl;
        void initialize()
        {
            bl = BlFactory.GetBl();
            busDataGrid.DataContext = bl.GetAllBuses().OrderBy(bus=>bus.License);
        }
        //string license_format()
        public DisplayBuses()
        {
            InitializeComponent();
            initialize();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
        private void fillGas(object sender, RoutedEventArgs e)
        {
            //DO THIS!!!
        }
        private void driveBus(object sender, RoutedEventArgs e)
        {
            //DO THIS!!!
        }
        private void deleteBus(object sender, RoutedEventArgs e)
        {
            //DO THIS!!!
        }
    }
}
