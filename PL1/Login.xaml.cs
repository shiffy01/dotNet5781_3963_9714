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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        static IBL bl;
        public Login()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();

        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (username_tb.Text != null&&pasword_box.Password!=null)
            {
                BO.User user;
                try
                {
                    user=bl.GetUser(username_tb.Text, pasword_box.Password);
                }
                catch (BO.UserDoesNotExistException)
                {
                    //make the box red...or just say that the password or user name is incorrect
                    return;
                }
                if(user.IsManager)
                    //open manager page
                else
                        //open search page;
            }
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
