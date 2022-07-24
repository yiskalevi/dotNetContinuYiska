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
using DalApi;
using Dal;
using DO;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow1.xaml
    /// </summary>
    public partial class CustomerWindow1 : Window
    {
        IBl bl;
        customer cust;
        location l;
        int idC;
        bool temp1 = false, temp2 = false, temp3 = false, temp4 = false,temp5=false;
        ObservableCollection<customerToList> customerPL = new ObservableCollection<customerToList>();

        public CustomerWindow1(IBl b, int i, ObservableCollection<customerToList> obserList )//window up
        {
            try
            {
                InitializeComponent();
                bl = b;
                customerPL = obserList;
                upGird.Visibility = Visibility.Visible;
                l = new location();
                cust = new BO.customer();
                cust = bl.getCustomer(i);
                IDN.Text = Convert.ToString(cust.ID);
                IDN.IsEnabled = false;
                nemaT.Text = Convert.ToString(cust.name);
                LongitudeN.Text = Convert.ToString(cust.locationC.longitude);
                LongitudeN.IsEnabled = false;
                LatitudeN.Text = Convert.ToString(cust.locationC.latitude);
                LatitudeN.IsEnabled = false;
                phoneT.Text = Convert.ToString(cust.phone);
                updat.IsEnabled = false;
                received.ItemsSource = cust.listFromCus;//חבילות שהתקבלו
                shipped.ItemsSource = cust.listToCus;//חבילות שנשלחו
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public CustomerWindow1(IBl b, int i)//window up
        {
            try
            {
                InitializeComponent();
                bl = b;
                upGird.Visibility = Visibility.Visible;
                l = new location();
                cust = new BO.customer();
                cust = bl.getCustomer(i);
                IDN.Text = Convert.ToString(cust.ID);
                IDN.IsEnabled = false;
                nemaT.Text = Convert.ToString(cust.name);
                LongitudeN.Text = Convert.ToString(cust.locationC.longitude);
                LongitudeN.IsEnabled = false;
                LatitudeN.Text = Convert.ToString(cust.locationC.latitude);
                LatitudeN.IsEnabled = false;
                phoneT.Text = Convert.ToString(cust.phone);
                updat.IsEnabled = false;
                received.ItemsSource = cust.listFromCus;
                shipped.ItemsSource = cust.listToCus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public CustomerWindow1(IBl b, ObservableCollection<customerToList> obserList )//add up
        {
                InitializeComponent();
                bl = b;
            l = new location();
            addGird.Visibility = Visibility.Visible;
                cust = new BO.customer();
            customerPL = obserList;

        }
        public CustomerWindow1(IBl b)//add up
        {
            InitializeComponent();
            bl = b;
            l = new location();
            addGird.Visibility = Visibility.Visible;
            cust = new BO.customer();
        }
        private void IsEnabledAdd()//Checks if all the necessary data for adding a drone has been entered
        {
            if (temp1 && temp2 && temp3 && temp4 && temp5)
                add.IsEnabled = true;
            else
                add.IsEnabled = false;
        }
        private void IDN_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp1 = true;
                cust.ID = int.Parse(IDN.Text);
                IDNErrow.Visibility = Visibility.Hidden;
                IsEnabledAdd();
            }
            catch (Exception ex)
            {
                temp1 = false;
                IDNErrow.Content = ex.Message;
                IDNErrow.Visibility = Visibility.Visible;
                IsEnabledAdd();
            }
        }
        private void nemaT_TextChanged(object sender, TextChangedEventArgs e)
        {
            temp2 = true;
            cust.name = nemaT.Text;
            updat.IsEnabled = true;
            IsEnabledAdd();
            bl.updateCustomerName(cust.ID,nemaT.Text);
        }

        private void LatitudeN_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp3 = true;
                l.latitude = double.Parse(LatitudeN.Text);
                LatitudeNErrow.Visibility = Visibility.Hidden;
                IsEnabledAdd();
            }
            catch (Exception ex)
            {
                temp3 = false;
                LatitudeNErrow.Content = ex.Message;
                LatitudeNErrow.Visibility = Visibility.Visible;
                IsEnabledAdd();
            }
        }

        private void LongitudeN_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp4 = true;
                l.longitude = double.Parse(LongitudeN.Text);
                LongitudeNErrow.Visibility = Visibility.Hidden;
                IsEnabledAdd();
            }
            catch (Exception ex)
            {
                temp4 = false;
                LongitudeNErrow.Content = ex.Message;
                LongitudeNErrow.Visibility = Visibility.Visible;
                IsEnabledAdd();
            }
        }

        private void phoneT_TextChanged(object sender, TextChangedEventArgs e)
        {
            temp5 = true;
            cust.phone = phoneT.Text;
            updat.IsEnabled = true;
            IsEnabledAdd();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cust.locationC = l;
                bl.addCustomer(cust);
                customerPL.Add(new customerToList
                {
                    ID = cust.ID,
                    name = cust.name,
                    phone = cust.phone,
                    numberOfP = 0,
                    numberOfPNotDelivered = 0,
                    numberOfExpectedP = 0,
                    numberOfSentedP = 0,
                });
                MessageBox.Show("successful");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                idC = cust.ID;
                int index = 0;
                for (int i = 0; i < customerPL.Count(); i++)
                {
                    if (customerPL[i].ID == idC)
                        index = i;
                }
                bl.updateCustomerNameAndPhone(idC, cust.phone, cust.name);
                customer c = bl.getCustomer(idC);
                customerToList tl = new customerToList();
                tl = bl.converToCListObj(c);           
                customerPL[index] = tl;
                MessageBox.Show("successful");
                this.Close();
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
    }
}
