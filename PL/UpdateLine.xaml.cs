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
using System.Windows.Shapes;
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateLine.xaml
    /// </summary>
 
    
    public partial class UpdateLine : Window
    {
        static IBL bl;
        BusLine Line;
        public UpdateLine(BusLine line)
        {
            bl = BlFactory.GetBl();
            InitializeComponent();
            Line = line;
        }
    }
}
