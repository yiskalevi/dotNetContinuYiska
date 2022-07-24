using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Runtime.InteropServices;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using BLApi;
using System.Windows.Interop;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BL.BL b = new BL.BL();
        bool whoAreYou;//customer=false, manage=true
        string name;
        int id;
        int pas;
        int counter;
        bool right;
        public MainWindow()
        {
            InitializeComponent();
            up();
        }
        private void up()
        {
            counter = 0;
            right = false;
            enter.IsEnabled = false;
            managerGird.Visibility = Visibility.Hidden;
            customerGird.Visibility = Visibility.Hidden;
            whoAreYou = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (whoAreYou)
                {
                    //המשתמש שנכנס מנהל
                    //לבנות פונרציה שמקבלת סיסמא ושם בול שאומר אם קיים מנהל כזה
                    right = b.checkManager(id, pas);
                    if (right)
                        new act(b).ShowDialog();
                    else
                    {
                        MessageBox.Show("The data entered is incorrect");
                        up();
                    }
                    right = false;
                }
                else
                {
                    //המשתמש שנכנס לקוח
                    right = b.checkCustomer(name, id);
                    if (right)
                    {
                        new CustomerActivities(b, id).ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("The data entered is incorrect");
                        up();
                    }
                    up();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void passwordTe_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pas = int.Parse(passwordTe.Password);
            counter++;
            if (counter == 2)
                enter.IsEnabled = true;
        }

        private void manager_Click(object sender, RoutedEventArgs e)
        {
            managerGird.Visibility = Visibility.Visible;
            customerGird.Visibility = Visibility.Hidden;
            whoAreYou = true;
            counter = 0;
        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {
            managerGird.Visibility = Visibility.Hidden;
            customerGird.Visibility = Visibility.Visible;
            whoAreYou = false;
            counter = 0;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void updatS_Click(object sender, RoutedEventArgs e)
        {
            up();
        }

        private void newC_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow1(b).ShowDialog();
        }

        private void idTeC_TextChanged(object sender, TextChangedEventArgs e)
        {
            id = int.Parse(idTeC.Text);
            counter++;
            if (counter == 2)
                enter.IsEnabled = true;
        }

        private void nameTeC_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = nameTeC.Text;
            counter++;
            if(counter==2)
                enter.IsEnabled = true;
        }

     

        private void enterzmani_Click(object sender, RoutedEventArgs e)//למחוק בסוף
        {
            new act(b).ShowDialog();
        }

        private void nameTe_TextChanged(object sender, TextChangedEventArgs e)
        {
            id = int.Parse(nameTe.Text);
            counter++;
            if (counter == 2)
                enter.IsEnabled = true;
        }

        private void enterzmaniC_Click(object sender, RoutedEventArgs e)
        {
            int I = 8888;
            new CustomerActivities(b, I).ShowDialog(); ;
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
