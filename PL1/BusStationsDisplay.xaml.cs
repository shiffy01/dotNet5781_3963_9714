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
    /// Interaction logic for BusStationsDisplay.xaml
    /// </summary>
    public partial class BusStationsDisplay : Page
    {
        static IBL bl;
        BO.User User;
        public BusStationsDisplay(IBL bl1, BO.User user, bool Manage)
        {
            InitializeComponent();
            bl = bl1;
            User = user;

        }
    }
}
