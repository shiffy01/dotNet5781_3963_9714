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
//using Xceed.Wpf.Toolkit;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddDistances.xaml
    /// </summary>
    public partial class AddDistances : Window
    {
        //public AddDistances(List<string> distances)
        //{
        //   
        //InitializeComponent();
        //}
        static IBL bl;
        int Index;
        int Code1;
        int Code2;
        string askForDistance;
        string askForTime;
        int Days;
        int Hours;
        int Minutes;

        List<string> PairIds;
        public AddDistances(List<string> pairIds)
        {
            bl = BlFactory.GetBl();
            InitializeComponent();
            Index = 0;
            PairIds = pairIds;
            askForTime = "Please enter average drive time between these two stations";
            ask_for_driveTime_label.Content = askForTime;
            createDialogeContent();
           
        }
        private void splitStringTOTwoInts(string str, ref int num1, ref int num2, char splitHere)
        {
            string[] codes =str.Split(splitHere);
            try
            {
                num1 = Int32.Parse(codes[0]);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                num2 = Int32.Parse(codes[1]);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
          
           // txtAnswer.Text = null;
           
        }
        private void createDialogeContent()
        {
            splitStringTOTwoInts(PairIds[Index], ref Code1, ref Code2, '*');
            askForDistance = $"Please enter the distance between station:{Code1} and station:{Code2}";
            ask_for_distance_label.Content = askForDistance;
        }
        private bool correctTimeFormat()
        {
            splitStringTOTwoInts(averageDriveTimeMSB.Text, ref Hours, ref Minutes, ':');

            if (Minutes > 59)
            {
                //throw trigger to change text to red
                errorLabel.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (averageDriveTimeMSB.IsMaskFull)
            {
                if (!correctTimeFormat())
                    return;
                try
                {
                    bl.AddAdjacentStations(Code1, Code2, double.Parse(distanceMTB.Text), new TimeSpan(Hours, Minutes, 00));
                }
                catch (BO.PairAlreadyExistsException ex)
                {
                    MessageBoxResult result = MessageBox.Show(ex.Message, " Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Index++;
                if (Index == PairIds.Count)
                {
                    this.DialogResult = true;
                    return;

                }
                createDialogeContent();

                
            }
            else
            {

                //throw trigger to change text to red
                errorLabel.Visibility = Visibility.Visible;
                return;
            }

        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            distanceMTB.SelectAll();
            distanceMTB.Focus();
        }

        private void averageDriveTimeMSB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (averageDriveTimeMSB.IsMaskFull)
                correctTimeFormat();
        }

        private void cancel_btn_click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result = MessageBox.Show(@"             Are you sure you want to cancel?
                                                         By canceling, the remaining distances will be set to random values.", " Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                for (int i = Index; i < PairIds.Count; i++)
                {
                    try
                    {
                        bl.AddAdjacentStations(Code1, Code2, 200, new TimeSpan(00, 12, 00));
                    }
                    catch (BO.PairAlreadyExistsException ex)
                    {
                        MessageBoxResult result2 = MessageBox.Show(ex.Message, " Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                this.DialogResult = true;
            }
        }
    }
}
