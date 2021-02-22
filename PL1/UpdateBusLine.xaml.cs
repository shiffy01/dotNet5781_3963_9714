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
    /// Interaction logic for UpdateBusLine.xaml
    /// </summary>
    public partial class UpdateBusLine : Page
    {
        public UpdateBusLine(IBL bl, BO.User user, int lineID)
        {
            InitializeComponent();
        }
    }
}
