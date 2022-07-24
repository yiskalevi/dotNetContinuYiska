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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using BO;
using BLApi;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for WindowListOfDrone.xaml
    /// </summary>
    public partial class WindowListOfDrone : Window
    {
        IBl bn;
        ObservableCollection<droneToList> dronePL = new ObservableCollection<droneToList>();
        public WindowListOfDrone(BL.BL b)
        {
            try
            {
                InitializeComponent();
                bn = b;
                DataContext = upList();
                WeigtSelector.ItemsSource = Enum.GetValues(typeof(BO.enums.WeightCategories));
                StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.enums.DroneStatuses));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private ObservableCollection<droneToList> upList()
        {
            try
            {
                IEnumerable<droneToList> l = bn.viewListOfDrone();
                foreach (droneToList item in l)
                {
                    droneToList d = new droneToList();
                    d.droneLocation = new location();
                    d = item;
                    dronePL.Add(d);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dronePL;
        }      
        private void WeigtSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.enums.WeightCategories x = (BO.enums.WeightCategories)WeigtSelector.SelectedItem;
             DroneListView.DataContext = bn.ListSortingDrone(y => y.weight == x);

        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.enums.DroneStatuses x = (BO.enums.DroneStatuses)StatusSelector.SelectedItem;
            DroneListView.DataContext = bn.ListSortingDrone(y => y.statusOfDrone == x);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new WindowDrone_(bn, dronePL).ShowDialog();
        }
        private void DroneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            droneToList d = (BO.droneToList)DroneListView.SelectedItem;
            if (d != null)
            {
                new WindowDrone_(bn, d.ID, dronePL).ShowDialog();
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void up()
        {
           upList();
        }
        private void updatS_Click(object sender, RoutedEventArgs e)
        {
            up();
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
