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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow1 : Window
    {
        IBl bl;
        int idS;
        location l = new location();
        station st =new BO.station();
        bool temp1 = false, temp2 = false, temp3 = false, temp4 = false,temp5=false;
        ObservableCollection<stationToList> stationPL = new ObservableCollection<stationToList>();
        public StationWindow1(IBl b, ObservableCollection<stationToList> obserList)
        {
            stationPL = obserList;
            InitializeComponent();
            bl = b;
            addGird.Visibility = Visibility.Visible;
        }
        #region StationWindow1 window act
        /// <summary>
        /// A constructor receives a station ID and displays its data and the
        /// actions that can be taken on it.
        /// </summary>
        public StationWindow1(IBl b, int i, ObservableCollection<stationToList> obserList)
        {
            try
            {
                stationPL = obserList;
                idS = i;
                InitializeComponent();
                bl = b;
                st.stationLocation = new location();
                st.drones = new List<droneInCharning>();
                st = bl.getStation(idS);
                upGird.DataContext = st;
                upGird.Visibility = Visibility.Visible;
                nemaT.IsEnabled = false;
                LongitudeN.IsEnabled = false;
                drone.ItemsSource = st.drones;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region StationWindow1 window add
        /// <summary>
        /// A station insert window opens
        /// </summary>
        /// <param name="b"> interface of BL </param>
        public StationWindow1(IBl b)
        {
            InitializeComponent();
            bl = b;
            addGird.Visibility = Visibility.Visible;
        }
        #endregion

        #region IsEnabledAdd
        /// <summary>
        /// Checks if all the necessary data for adding a drone has been entered
        /// </summary>
        private void IsEnabledAdd()
        {
            if (temp1 && temp2 && temp3 && temp4 && temp5)
            {
                add.IsEnabled = true;

            }
            else
                add.IsEnabled = false;
        }
        #endregion

        private void IDN_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp1 = true;
                IDNErrow.Visibility = Visibility.Hidden;
                st.ID = int.Parse(IDN.Text);
                updat.IsEnabled = true;
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

        private void LongitudeN_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp4 = true;
                LongitudeNErrow.Visibility = Visibility.Hidden;
                l.longitude = double.Parse(LongitudeN.Text);
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

        private void LatitudeN_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp3 = true;
                LatitudeNErrow.Visibility = Visibility.Hidden;
                l.latitude = double.Parse(LatitudeN.Text);
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

        private void numberOfStandsT_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp5 = true;
                updat.IsEnabled = true;
                st.numberOfStands = int.Parse(numberOfStandsT.Text);
                numberOfStandsTErrow.Visibility = Visibility.Hidden;
                IsEnabledAdd();
              
            }
            catch (Exception ex)
            {
                temp5 = false;
                numberOfStandsTErrow.Content = ex.Message;
                numberOfStandsTErrow.Visibility = Visibility.Visible;
                IsEnabledAdd();
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.droneToList d = (BO.droneToList)drone.SelectedItem;
            int i = d.ID;
            new WindowDrone_(bl, i).ShowDialog();
        }

        private void updat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                idS = st.ID;
                int index = 0;
                for (int i = 0; i < stationPL.Count(); i++)
                {
                    if (stationPL[i].ID == idS)                  
                        index = i;                   
                }
                bl.updateNameAndNumOfStands(st.ID, st.numberOfStands, st.name);                                          
                station t = bl.getStation(idS);
                stationToList tl = (new stationToList
                {
                    ID = t.ID,
                    name = t.name,
                    numberOfAvailableStands = t.numberOfStands,
                    numberOfOccupiedStands = t.drones.Count()
                });
                stationPL[index] = tl;
                MessageBox.Show("uccessful");//לא בטוח שיעבוד טוב
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }          
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                st.stationLocation = new location();
                st.stationLocation = l;
                st.drones = new List<droneInCharning>();
                bl.addStation(st);
                stationPL.Add(new stationToList
                {
                    ID = st.ID,
                    name = st.name,
                    numberOfAvailableStands = st.numberOfStands- st.drones.Count(),
                    numberOfOccupiedStands = st.drones.Count()
                });
                MessageBox.Show("successful");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void nemaT_TextChanged(object sender, TextChangedEventArgs e)
        {
            temp2 = true;
            st.name = nemaT.Text;
            IsEnabledAdd();
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
