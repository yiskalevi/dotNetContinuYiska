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
using BLApi;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for parcelWindow.xaml
    /// </summary>
    public partial class parcelWindow : Window
    {
        IBl bl;
        parcel parcelPL=new BO.parcel();
        bool temp1 = false, temp2 = false, temp3 = false, temp4 = false;
        ObservableCollection<parcelToList> parcelPLL = new ObservableCollection<parcelToList>();
        public parcelWindow(IBl b,  ObservableCollection<parcelToList> obserList)//act parcel
        {
            try
            {
                parcelPLL = obserList;
                InitializeComponent();
                parcelPL = new parcel();
                parcelPL.sender = new customerInP();
                parcelPL.recipient = new customerInP();
                bl = b;
                GirAdd.Visibility = Visibility.Visible;
                WeightE.ItemsSource = Enum.GetValues(typeof(BO.enums.WeightCategories));
                priority.ItemsSource = Enum.GetValues(typeof(BO.enums.Priorities));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public parcelWindow(IBl b)//add parcel
        {
            InitializeComponent();
            parcelPL = new parcel();
            parcelPL.sender = new customerInP();
            parcelPL.recipient = new customerInP();
            bl = b;
            GirAdd.Visibility = Visibility.Visible;
            WeightE.ItemsSource = Enum.GetValues(typeof(BO.enums.WeightCategories));
            priority.ItemsSource = Enum.GetValues(typeof(BO.enums.Priorities));

        }
        public parcelWindow(IBl b,int i)//act parcel
        {
            try
            {
                InitializeComponent();
                parcelPL.sender = new customerInP();
                parcelPL.recipient = new customerInP();
                parcelPL.droneInPar = new droneInParcel();
                bl = b;
                GirAct.Visibility = Visibility.Visible;
                parcelPL = bl.getParcel(i);
                GirAct.DataContext = parcelPL;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }
        public parcelWindow(IBl b, int i,int t)//parcel
        {
            try
            {
                InitializeComponent();
                parcelPL.sender = new customerInP();
                parcelPL.recipient = new customerInP();
                parcelPL.droneInPar = new droneInParcel();
                WeightE.ItemsSource = Enum.GetValues(typeof(BO.enums.WeightCategories));
                priority.ItemsSource = Enum.GetValues(typeof(BO.enums.Priorities));
                bl = b;
                GirAdd.Visibility = Visibility.Visible;
                IDSender.IsEnabled = false;
                string s = Convert.ToString(i);
                IDSender.Text = s;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      

        #region addParcel

        private void IsEnabledAdd()//Checks if all the necessary data for adding a drone has been entered
        {
            if (temp1 && temp2 && temp3 && temp4)
                add.IsEnabled = true;
            else
                add.IsEnabled = false;
        }

        private void IDSender_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp1 = true;
                IDNESrrow.Visibility = Visibility.Hidden;
                customer t = bl.getCustomer(int.Parse(IDSender.Text));
                parcelPL.sender.ID = int.Parse(IDSender.Text);
                customerInP pc = new customerInP();
                pc.ID = int.Parse(IDSender.Text);
                pc.name = t.name;
                parcelPL.sender = pc;
                IsEnabledAdd();
            }
            catch (Exception ex)
            {
                temp1 = false;
                IDNESrrow.Content = ex.Message;
                IDNESrrow.Visibility = Visibility.Visible;
                IsEnabledAdd();
            }
        }

        private void IDRecipient_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp2 = true;
                IDRErrow.Visibility = Visibility.Hidden;
                parcelPL.recipient.ID = int.Parse(IDRecipient.Text);
                customer t = bl.getCustomer(int.Parse(IDRecipient.Text));
                customerInP pc = new customerInP();
                pc.ID = int.Parse(IDRecipient.Text);
                pc.name = t.name;
                parcelPL.recipient = pc;
                IsEnabledAdd();
            }
            catch (Exception ex)
            {
                temp2 = false;
                IDRErrow.Content = ex.Message;
                IDRErrow.Visibility = Visibility.Visible;
                IsEnabledAdd();
            }
        }

        private void updating_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WeightE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            temp3 = true;
            parcelPL.weight = (BO.enums.WeightCategories)WeightE.SelectedItem;
            IsEnabledAdd();
        }

        private void priority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            temp4 = true;
            parcelPL.priority = (BO.enums.Priorities)priority.SelectedItem;
            IsEnabledAdd();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                int g=bl.addParcel(parcelPL);
                g--;
                parcelToList par = new parcelToList();
                par.ID = g;
                par.sender = new customerInP();
                par.sender = parcelPL.sender;
                par.recipient = new customerInP();
                par.recipient = parcelPL.recipient;
                par.weight = parcelPL.weight;
                par.status = enums.parcelStatus.Defined;
                parcelPLL.Add(par);              
                MessageBox.Show("successful");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

    }

}
