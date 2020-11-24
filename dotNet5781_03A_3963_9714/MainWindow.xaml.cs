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

namespace dotNet5781_03A_3963_9714
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Random rand = new Random(DateTime.Now.Millisecond);
            int num = rand.Next(0, dotNet5781_02_3963_9714.Bus_line_list.Count);
        }

        private void ___cbBusLines__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
