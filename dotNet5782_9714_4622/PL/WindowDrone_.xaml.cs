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
    /// Interaction logic for WindowDrone_.xaml
    /// </summary>
    public partial class WindowDrone_ : Window
    {
        //BO.drone pld = new BO.drone();
        //int num = 0;
        IBl bl;
        BO.drone a;
        string ModeleHelp;
        BO.drone drl = new BO.drone();
        int statNum;
        bool temp1 = false, temp2 = false, temp3 = false, temp4 = false;
        ObservableCollection<droneToList> dronePL = new ObservableCollection<droneToList>();

        public WindowDrone_()
        {
            InitializeComponent();
        }
        public WindowDrone_(IBl b, ObservableCollection<droneToList> obserList)//פותח חלון הוספה
        {
            dronePL = obserList;
            InitializeComponent();
            bl = b;
            GirAdd.Visibility = Visibility.Visible;
            WeightE.ItemsSource = Enum.GetValues(typeof(BO.enums.WeightCategories));
        }
        public WindowDrone_(IBl b, int i, ObservableCollection<droneToList> obserList)// פותח חלון פעולות
        {
            try
            {
                dronePL = obserList;
                InitializeComponent();
                bl = b;
                a = new BO.drone();
                a.droneLocation = new location();
                a.parcel = new parcelToTranser();
                actain.Visibility = Visibility.Visible;
                GirAct.Visibility = Visibility.Visible;
                a = bl.getDrone(i);
                upupdatWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public WindowDrone_(IBl b, int i)// פותח חלון פעולות
        {
            try
            {
                InitializeComponent();
                bl = b;
                a = new BO.drone();
                a.droneLocation = new location();
                a.parcel = new parcelToTranser();
                actain.Visibility = Visibility.Visible;
                GirAct.Visibility = Visibility.Visible;
                a = bl.getDrone(i);
                GirAct.DataContext = a;
                //DataContext = a;
                upupdatWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }     
        private void ModelN_TextChanged(object sender, TextChangedEventArgs e)//איך אפשר להגביל את המידע
        {
            try
            {
                temp2 = true;
                ModelNErrow.Visibility = Visibility.Hidden;
                drl.model = ModelN.Text;
                IsEnabledAdd();
            }
            catch (Exception ex)
            {
                temp2 = false;
                ModelNErrow.Content = ex.Message;
                ModelNErrow.Visibility = Visibility.Visible;
                IsEnabledAdd();
            }
        }
        private void updating_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                bl.updateTheDroneName(a.ID, ModeleHelp);
                upupdatWindow();
                MessageBox.Show("The update was successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void IsEnabledAdd()//Checks if all the necessary data for adding a drone has been entered
        {
            if (temp1 && temp2 && temp3 && temp4)
                add.IsEnabled = true;
            else
                add.IsEnabled = false;
        }
        private void IDN_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp1 = true;
                IDNErrow.Visibility = Visibility.Hidden;
                drl.ID = int.Parse(IDN.Text);
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
        private void WeightE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            temp3 = true;
            drl.weight = (BO.enums.WeightCategories)WeightE.SelectedItem;
            IsEnabledAdd();
        }
        private void StationN_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                temp4 = true;
                StationNErrow.Visibility = Visibility.Hidden;
                statNum = int.Parse(StationN.Text);
                IsEnabledAdd();
            }
            catch (Exception ex)
            {
                temp4 = false;
                StationNErrow.Content = ex.Message;
                StationNErrow.Visibility = Visibility.Visible;
                IsEnabledAdd();
            }
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addDrone(drl, statNum);
                drone d = new drone();
                d = bl.getDrone(drl.ID);
                droneToList dls = new droneToList();
                dls.ID = d.ID;
                dls.model = d.model;
                dls.parcelNumber = d.parcel.ID;
                dls.statusOfDrone = d.statusOfDrone;
                dls.weight = d.weight;
                dls.droneLocation = new location();
                dls.droneLocation = d.droneLocation;
                dls.battery = d.battery;
                dronePL.Add(dls);                
                MessageBox.Show("successful");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void upupdatWindow()
        {
            try
            {
                //a = bl.getDrone(a.ID);
                //GirAct.DataContext = a;
                a = bl.getDrone(a.ID);
                GirAct.DataContext = a;

                ModelN2.IsEnabled = true;
                //LatitudeN.Content = Convert.ToString(a.droneLocation.latitude);
                //LongitudeN.Content = Convert.ToString(a.droneLocation.longitude);
                //parcelTOtranferT.Content = Convert.ToString(a.parcel.ID);
                freeFromCharning.IsEnabled = false;
                sendDroneCharn.IsEnabled = false;
                sendDroneToShip.IsEnabled = false;
                updating.IsEnabled = false;
                provide.IsEnabled = false;
                Package.IsEnabled = false;
                //windowList.upList();
                if (a.parcel.ID !=0)
                {
                    GirPaecel.Visibility = Visibility.Visible;
                    GirPaecel.DataContext = a.parcel;
                }
                else
                {
                    GirPaecel.Visibility = Visibility.Hidden;
                }

                if (a.statusOfDrone == BO.enums.DroneStatuses.Maintenance)
                    freeFromCharning.IsEnabled = true;
                if (a.statusOfDrone == BO.enums.DroneStatuses.Available)
                {
                    sendDroneCharn.IsEnabled = true;
                    sendDroneToShip.IsEnabled = true;
                }
                if (a.statusOfDrone == BO.enums.DroneStatuses.Shipping)
                {
                    parcel p = bl.getParcel(a.parcel.ID);
                    if (p.collictionTime == null)
                        Package.IsEnabled = true;
                    else if (p.collictionTime != null)
                        provide.IsEnabled = true;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #region Action buttons on drone
        private void freeFromCharning_Click(object sender, RoutedEventArgs e)//משחרר רחפן מטעינה
        {
            try
            {
                int index = 0;
                for (int i = 0; i < dronePL.Count(); i++)
                {
                    if (dronePL[i].ID == a.ID)
                        index = i;
                }
                bl.freeDroneFromCharge(a.ID);//צריך לראות איך אפשר לחשב את הזמן שהוא היה בטעינה כנראה להוסף שדה BL
                drone d = new drone();
                d = bl.getDrone(a.ID);
                droneToList dls = new droneToList();
                dls.ID = d.ID;
                dls.model = d.model;
                dls.parcelNumber = d.parcel.ID;
                dls.statusOfDrone = d.statusOfDrone;
                dls.weight = d.weight;
                dls.droneLocation = new location();
                dls.droneLocation = d.droneLocation;
                dls.battery = d.battery;
                dronePL[index] = dls;
                upupdatWindow();
                MessageBox.Show("successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Package_Click(object sender, RoutedEventArgs e)//איסוף חבילה על ידי רחפן
        {           
            try
            {
                int index = 0;
                for (int i = 0; i < dronePL.Count(); i++)
                {
                    if (dronePL[i].ID == a.ID)
                        index = i;
                }
                bl.collectParcelByDrone(a.ID);
                drone d = new drone();
                d = bl.getDrone(a.ID);
                droneToList dls = new droneToList();
                dls.ID = d.ID;
                dls.model = d.model;
                dls.parcelNumber = d.parcel.ID;
                dls.statusOfDrone = d.statusOfDrone;
                dls.weight = d.weight;
                dls.droneLocation = new location();
                dls.droneLocation = d.droneLocation;
                dls.battery = d.battery;
                dronePL[index] = dls;
                upupdatWindow();
                MessageBox.Show("successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void provide_Click(object sender, RoutedEventArgs e)//אספקת חבילה שרחפן כבר אסף
        {
            try
            {           
                int index = 0;
                for (int i = 0; i < dronePL.Count(); i++)
                {
                    if (dronePL[i].ID == a.ID)
                        index = i;
                }
                bl.provideParcelByDrone(a.ID);
                drone d = new drone();
                d = bl.getDrone(a.ID);
                droneToList dls = new droneToList();
                dls.ID = d.ID;
                dls.model = d.model;
                dls.parcelNumber = d.parcel.ID;
                dls.statusOfDrone = d.statusOfDrone;
                dls.weight = d.weight;
                dls.droneLocation = new location();
                dls.droneLocation = d.droneLocation;
                dls.battery = d.battery;
                dronePL[index] = dls;
                upupdatWindow();
                MessageBox.Show("successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void sendDroneToShip_Click(object sender, RoutedEventArgs e)//משייך רחפן לחבילה
        {          
            try
            {
                int index = 0;
                for (int i = 0; i < dronePL.Count(); i++)
                {
                    if (dronePL[i].ID == a.ID)
                        index = i;
                }
                bl.conectParcelToDrone(a.ID);
                drone d = new drone();
                d = bl.getDrone(a.ID);
                droneToList dls = new droneToList();
                dls.ID = d.ID;
                dls.model = d.model;
                dls.parcelNumber = d.parcel.ID;
                dls.statusOfDrone = d.statusOfDrone;
                dls.weight = d.weight;
                dls.droneLocation = new location();
                dls.droneLocation = d.droneLocation;
                dls.battery = d.battery;
                dronePL[index] = dls;
                upupdatWindow();
                MessageBox.Show("successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void sendDroneCharn_Click(object sender, RoutedEventArgs e)
        {          
            try
            {
                int index = 0;
                for (int i = 0; i < dronePL.Count(); i++)
                {
                    if (dronePL[i].ID == a.ID)
                        index = i;
                }
                bl.sendDroneToCharge(a.ID);
                drone d = new drone();
                d = bl.getDrone(a.ID);
                droneToList dls = new droneToList();
                dls.ID = d.ID;
                dls.model = d.model;
                dls.parcelNumber = d.parcel.ID;
                dls.statusOfDrone = d.statusOfDrone;
                dls.weight = d.weight;
                dls.droneLocation = new location();
                dls.droneLocation = d.droneLocation;
                dls.battery = d.battery;
                dronePL[index] = dls;
                upupdatWindow();
                MessageBox.Show("successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ModelN2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ModeleHelp = ModelN2.Text;
                updating.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }      
        bool Auto;
        BackgroundWorker worker;
        private void updateDrone() => worker.ReportProgress(0);
        private bool checkStop() => worker.CancellationPending;
        private void simolator_Click(object sender, RoutedEventArgs e)
        {
            actain.Visibility = Visibility.Hidden;
            stop.Visibility = Visibility.Visible;
            Auto = true;
            worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker.DoWork += (sender, args) => bl.startSimolator((int)args.Argument, updateDrone, checkStop);
            worker.RunWorkerCompleted += (sender, args) => Auto = false;
            worker.ProgressChanged += (sender, args) => upupdatWindow();
            worker.RunWorkerAsync(a.ID);
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
        }
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
                worker.CancelAsync();
            GirAct.Visibility = Visibility.Hidden;
            stop.Visibility = Visibility.Hidden;
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
