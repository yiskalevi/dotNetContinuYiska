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
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace PL
{
    /// <summary>
    /// Interaction logic for act.xaml
    /// </summary>
    public partial class act : Window
    {
        BL.BL b = new BL.BL();
        public act(BL.BL d)//פותח את חלון הפעולות של המנהל
        {
            InitializeComponent();
            b = d;
        }

        private void dronView_Click(object sender, RoutedEventArgs e)
        {
            new WindowListOfDrone(b).ShowDialog();
        }

        private void stationView_Click(object sender, RoutedEventArgs e)
        {
            new ListStationWindow(b).ShowDialog();
        }

        private void customerView_Click(object sender, RoutedEventArgs e)
        {
            new ListCustomerWindow(b).ShowDialog();
        }

        private void parcelView_Click(object sender, RoutedEventArgs e)
        {
            new ListParcelWindow(b).ShowDialog();
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
    }
}
