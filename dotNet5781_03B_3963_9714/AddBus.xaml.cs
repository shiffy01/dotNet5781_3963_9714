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
using dotNet5781_01_3963_9714;

namespace dotNet5781_03B_3963_9714
{
    /// <summary>
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        public Bus CurrentBus { get; set; }
       public bool AddIt { get; set; }

        public AddBus()
        {
            InitializeComponent();
            AddIt = false;
            CurrentBus = new Bus(1234567, DateTime.Now, 12300, 50, false, false, false);
            gridAdd.DataContext = CurrentBus;
        }

        private void finalAdd_Click(object sender, RoutedEventArgs e)
        {
           bool noPassengers=false;
            MessageBoxResult mbResult;
            if (CurrentBus.Number_of_passengers40 == false && CurrentBus.Number_of_passengers50 == false && CurrentBus.Number_of_passengers60 == false)
                noPassengers = true;
                if(!(CurrentBus.License<1000000|| CurrentBus.License >99999999||CurrentBus.Milage<0||noPassengers))//limit the date picker inside
            AddIt = true;

                else
            mbResult = MessageBox.Show("cannot add!");
            this.Close();
        }
    }
}

