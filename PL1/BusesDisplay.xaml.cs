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
using BlApi;

namespace PL1
{
    /// <summary>
    /// Interaction logic for BusesDisplay.xaml
    /// </summary>
    public partial class BusesDisplay : Page
    {
        static IBL bl;
        BO.User User;
        public BusesDisplay(IBL bl1, BO.User user)
        {
            InitializeComponent();
            bl = bl1;
            User = user;
        }
    }
}
