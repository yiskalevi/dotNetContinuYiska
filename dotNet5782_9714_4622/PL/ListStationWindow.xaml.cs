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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class ListStationWindow : Window //לשנות את שם המחלקה ל ListStationWindow
    {
        IBl bn;

        ObservableCollection<stationToList> stationPL = new ObservableCollection<stationToList>();

        public ListStationWindow(BL.BL b)
        {
            InitializeComponent();
            bn = b;
            DataContext = upList();           
        }
        private void avilableCharning_Click(object sender, RoutedEventArgs e)//להראות רק את התחנות שיש להם עמדות טעינה פנויות
        {
            bool f(stationToList x){ return x.numberOfAvailableStands>0; }
            listStation.ItemsSource = bn.ListSortingStation(f); ;
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
          this.Close();
        }

        private void updatS_Click(object sender, RoutedEventArgs e)
        {
            //לכדכן את רישמת הרחפנים ואם יש כפתור מופעל לעדכן גם אותו
            upList();//Updating the list of stations
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {       
            new StationWindow1(bn, stationPL).ShowDialog();
        }

        private void listStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stationToList s  = (stationToList)listStation.SelectedItem;
            if (s != null)
            {              
                new StationWindow1(bn, s.ID, stationPL).ShowDialog();
            }
        }
        private ObservableCollection<stationToList> upList()
        {
            try
            {
                IEnumerable<stationToList> l = bn.viewListOfStations();
                foreach (stationToList item in l)
                {
                    stationToList d = new stationToList();
                    d = item;
                    stationPL.Add(d);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return stationPL;
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
