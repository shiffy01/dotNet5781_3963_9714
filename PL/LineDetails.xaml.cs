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

namespace PL
{
    /// <summary>
    /// Interaction logic for LineDetails.xaml
    /// </summary>
    public partial class LineDetails : Window
    {
        public LineDetails()
        {
            InitializeComponent();
        }

        private void update_line_Click(object sender, RoutedEventArgs e)
        {
            UpdateLine updateLine = new UpdateLine();
            updateLine.Show();
        }
    }
}
