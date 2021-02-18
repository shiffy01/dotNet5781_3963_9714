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
using BlApi;

namespace PL1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        static IBL bl;
        BO.User User;
        public LoginWindow()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (username_tb.Text != null && pasword_box.Password != null)
            {
                BO.User user;
                try
                {
                    user = bl.GetUser(username_tb.Text, pasword_box.Password);
                }
                catch (BO.UserDoesNotExistException)
                {
                    //make the box red...or just say that the password or user name is incorrect
                    return;
                }
                if (Manager_RadioBtn.IsChecked == true)
                    popUp.IsOpen = true;
                else
                {
                    MainWindow main = new MainWindow(bl, User, false);
                    main.ShowDialog();
                }
            }
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (SignUp_Username_Tb.Text == null)
            {
                //maybe make it red.. this is a required field
                return;
            }
            if (SignUp_PasswordBx.Password != SignUp_ConfirmPasswordBx.Password)
            {
                //make it red... passwords dont match
                return;
            }
            bool isManager = false;
            if (Manager_RadioBtn.IsChecked == true)
                isManager = true;

            try
            {
                bl.AddUser(SignUp_Username_Tb.Text, SignUp_PasswordBx.Password, isManager);
                User = bl.GetUser(SignUp_Username_Tb.Text, SignUp_PasswordBx.Password);
            }
            catch (BO.UserNameAlreadyExistsException)
            {
                //ERROR!!!!!!!!!!!!!!!!!!!!!
                return;
            }
            if (Manager_RadioBtn.IsChecked == true)
                popUp.IsOpen = true;
            else
            {
                MainWindow main = new MainWindow(bl, User, false);
                main.ShowDialog();
            }
        }
        private void GoClick(object sender, RoutedEventArgs e)
        {
            popUp.IsOpen = false;
            if (Manager_RadioBtn1.IsChecked == true)
            {
                MainWindow mainw = new MainWindow(bl, User, true);
                mainw.ShowDialog();
            }
            else
            {
                MainWindow mainw = new MainWindow(bl, User, false);
                mainw.ShowDialog();
            }

        }

    }
}
