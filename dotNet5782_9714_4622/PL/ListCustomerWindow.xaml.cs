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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for ListCustomerWindow.xaml
    /// </summary>
    public partial class ListCustomerWindow : Window
    {
        IBl bn;
        ObservableCollection<customerToList> customerPL = new ObservableCollection<customerToList>();
        public ListCustomerWindow(BL.BL b)
        {
            try
            {
                InitializeComponent();
                bn = b;
                DataContext=upList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private ObservableCollection<customerToList> upList()
        {
            try
            {
                IEnumerable<customerToList> l = bn.viewListOfCustomer();
                foreach (customerToList item in l)
                {
                    customerToList d = new customerToList();
                    d = item;
                    customerPL.Add(d);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return customerPL;
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void listCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.customerToList c = (BO.customerToList)listCustomer.SelectedItem;
            if (c != null)
            {
                new CustomerWindow1(bn, c.ID, customerPL).ShowDialog();
            }
        }

        private void updatS_Click(object sender, RoutedEventArgs e)
        {
            listCustomer.Visibility = Visibility.Collapsed;
            upList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow1(bn, customerPL).ShowDialog();
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

      
    }
}
