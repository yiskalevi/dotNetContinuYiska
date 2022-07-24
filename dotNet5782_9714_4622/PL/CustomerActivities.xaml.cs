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
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerActivities.xaml
    /// </summary>
    public partial class CustomerActivities : Window
    {
        IBl bl;
        customer cust;
        public CustomerActivities(IBl b,int id)
        {
            InitializeComponent();
            cust = new customer();
            cust.locationC = new location();
            bl = b;
            cust=b.getCustomer(id);
            up();       
        }

        private void up()
        {
            user.Content = cust.name;
            GirdlistShipped.Visibility = Visibility.Hidden;
            GirdlistRecive.Visibility = Visibility.Hidden;

        }

        private void user_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow1(bl, cust.ID).ShowDialog();
            up();
        }

        private void newParcel_Click(object sender, RoutedEventArgs e)
        {
          new parcelWindow(bl, cust.ID, cust.ID).ShowDialog();
        }

        private void viewFrom_Click(object sender, RoutedEventArgs e)
        {
            GirdlistRecive.Visibility = Visibility.Visible;
            GirdlistShipped.Visibility = Visibility.Hidden;
            listRecive.ItemsSource = cust.listFromCus;
        }

        private void viewTo_Click(object sender, RoutedEventArgs e)
        {
            GirdlistShipped.Visibility = Visibility.Visible;
            GirdlistRecive.Visibility = Visibility.Hidden;
            listShipped.ItemsSource = cust.listToCus;
        }

        private void parcelCollection_Click(object sender, RoutedEventArgs e)
        {
            //עדכון זמן אספקה?
        }

        #region close
        //for hide the button close
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        #endregion

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void listShipped_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            parcelToList p = (BO.parcelToList)listShipped.SelectedItem;
            new parcelWindow(bl, p.ID).ShowDialog();
        }

        private void listRecive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            parcelToList p = (BO.parcelToList)listRecive.SelectedItem;
            new parcelWindow(bl, p.ID).ShowDialog();
        }
    }
}
