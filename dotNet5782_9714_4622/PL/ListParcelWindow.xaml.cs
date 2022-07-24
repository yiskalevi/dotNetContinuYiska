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
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for ListParcelWindow.xaml
    /// </summary>
    public partial class ListParcelWindow : Window
    {
        IBl bl;
        ObservableCollection<parcelToList> parcelPL = new ObservableCollection<parcelToList>();
        public ListParcelWindow(BL.BL b)
        {
            InitializeComponent();
            bl = b;
            DataContext = upList(); 
            statusPr.ItemsSource = Enum.GetValues(typeof(BO.enums.Priorities));
            statusWe.ItemsSource = Enum.GetValues(typeof(BO.enums.WeightCategories));
            
        }

        #region upList
        private ObservableCollection<parcelToList> upList()
        {
            try
            {
                IEnumerable<parcelToList> l = bl.viewListOfParcel();
                //parcelPL = new ObservableCollection<parcelToList>();
                foreach (parcelToList item in l)
                {
                    parcelToList d = new parcelToList();
                    d.recipient = new customerInP();
                    d.sender = new customerInP();
                    d = item;
                    parcelPL.Add(d);
                }                             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return parcelPL;
        }
        #endregion
        

        private void parcelV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            parcelToList p = (BO.parcelToList)parcelV.SelectedItem;
            new parcelWindow(bl, p.ID).ShowDialog();
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

       
        private void gropingChoce_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v= bl.viewListOfParcel();
            var groups = v.GroupBy(s => s.recipient);
            parcelV.DataContext = groups;

           
        }
        #region Sortings
        private void statusWe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.enums.WeightCategories x = (BO.enums.WeightCategories)statusWe.SelectedItem;
            parcelV.DataContext = bl.ListSortingParcel(y => y.weight == x);
        }
        private void statusPr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            enums.Priorities y = (enums.Priorities)statusPr.SelectedItem;
            parcelV.DataContext = bl.ListSortingParcel(x => x.priority == y);

        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new parcelWindow(bl, parcelPL).ShowDialog();
        }
    }

}