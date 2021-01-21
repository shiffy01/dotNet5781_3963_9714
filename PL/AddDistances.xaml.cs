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
        int Hour;
        int Minute;

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
        private void createDialogeContent()
        {
            string[] codes = PairIds[Index].Split('*');
            try
            {
                Code1 = Int32.Parse(codes[0]);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Code2 = Int32.Parse(codes[1]);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            askForDistance = $"Please enter the distance between station:{Code1} and station:{Code2}";
            ask_for_distance_label.Content = askForDistance;
           // txtAnswer.Text = null;
           
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddAdjacentStations(Code1, Code2, distanceTB.Text, new TimeSpan(Hour, Minute, 00));
            }
            if(Index==PairIds.Count)
            Index++;
            //try
            //{
            //    bl.AddAdjacentStations(Code1, Code2, new TimeSpan())
            //}
            //createDi
            this.DialogResult = true;

        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //txtAnswer.SelectAll();
            //txtAnswer.Focus();
        }

        //public string Answer
        //{
        //    get
        //    {
        //       // return txtAnswer.Text;
        //    }
        //}
    }
}
