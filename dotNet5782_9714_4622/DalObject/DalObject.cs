using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using System.Runtime.CompilerServices;


namespace Dal
{
    sealed class DalObject : IDal
    {
        #region singleton
        static readonly IDal instance = new DalObject();
        public static IDal Instance { get => instance; }
        static DalObject()
        {
            DataSource.Initialize();
        }
        public double[] felectric()
        {
            double[] arr = new double[5];
            arr[0] = DataSource.Config.available;
            arr[1] = DataSource.Config.light;
            arr[2] = DataSource.Config.medium;
            arr[3] = DataSource.Config.heavy;
            arr[4] = DataSource.Config.speed;
            return arr;

        }
        #endregion singleton

        //------------------------------------------------------Manager--------------------------------------------------
        #region checkManager
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool checkManager(int i, int p)//הפונקציה שהוספתי 
        {
            Manager myManager = DataSource.listManager.Find(x => x.ID == i);
            object obj = myManager;
            if (obj != null)
            {
                if (myManager.password == p)
                    return true;
                return false;
            }
            return false;
        }
        #endregion checkManager

        #region ListOfManager
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Manager> ListOfManager()
        {
            var list = from station in DataSource.listManager
                       select station;
            return list;
        }
        #endregion ListOfManager


        //------------------------------------------------------STATION--------------------------------------------------
        #region addStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addStation(DO.Station stationToAdd)
        {
            if (DataSource.listS.Exists(x => x.ID == stationToAdd.ID))
                throw new AlreadyExistException("The station already exist in the system");
            DataSource.listS.Add(stationToAdd);
        }
        #endregion addStation

        #region getStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station getStation(int stationID)
        {
            Station myStation = DataSource.listS.Find(x => x.ID == stationID);
            object obj = myStation;
            if (obj != null || !myStation.Deleted)
                return myStation;
            throw new DoesntExistException("The station does not exists");
        }
        #endregion getStation

        #region updateTheStationName
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateTheStationName(int id, string names) //getting only the station name
        {
            try
            {
                Station myStation = getStation(id);
                myStation.Name = names;
                DataSource.listS.Remove(getStation(id));
                DataSource.listS.Add(myStation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion updateTheStationName

        #region updateNumberOfStands
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateNumberOfStands(int id, int numOfStands) //getting only the number of stations
        {
            try
            {
                Station myStation = getStation(id);
                myStation.ChargeSlots = numOfStands;
                DataSource.listS.Remove(getStation(id));
                DataSource.listS.Add(myStation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion updateNumberOfStands

        #region updateNameAndNumOfStands
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateNameAndNumOfStands(int id, int numOfStands, string names) //getting both 
        {
            try
            {
                Station myStation = getStation(id);
                myStation.ChargeSlots = numOfStands;
                myStation.Name = names;
                DataSource.listS.Remove(getStation(id));
                DataSource.listS.Add(myStation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion updateNameAndNumOfStands

        #region viewListOfStations
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> viewListOfStations()
        {
            var list = from station in DataSource.listS
                       select station;
            return list;
        }
        #endregion viewListOfStations

        #region viewListOfAvailableStations
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> viewListOfAvailableStations()
        {
            var list = from x in DataSource.listS
                       where x.ChargeSlots > 0
                       select x;
            return list;
        }
        #endregion viewListOfAvailableStations

        #region closeStationToCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int closeStationToCharge(double droneLongitude, double droneLatitude, ref double minimLocation)
        {
            Station st = new Station();
            double minLocation = Math.Sqrt((Math.Pow(droneLongitude - DataSource.listS[0].Longitude, 2)
                                    + Math.Pow(droneLatitude - DataSource.listS[0].Latitude, 2)));
            foreach (DO.Station itemS in DataSource.listS)
            {
                if (itemS.ChargeSlots > 0)
                {
                    double dis = Math.Sqrt((Math.Pow(droneLongitude - itemS.Longitude, 2)
                        + Math.Pow(droneLatitude - itemS.Latitude, 2)));
                    if (dis <= minLocation)
                    {
                        minLocation = dis;
                        st = itemS;
                    }
                }
            }
            object obj = st;
            if (obj != null)
                return st.ID;
            else
                return 0;
        }
        #endregion closeStationToCharge

        #region closeStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int closeStation(double longtit, double latit)
        {
            Station st = new Station();
            double minLocation = Math.Sqrt((Math.Pow(longtit - DataSource.listS[0].Longitude, 2)
                                    + Math.Pow(latit - DataSource.listS[0].Latitude, 2)));
            foreach (DO.Station itemS in DataSource.listS)
            {
                double dis = Math.Sqrt((Math.Pow(longtit - itemS.Longitude, 2)
                    + Math.Pow(latit - itemS.Latitude, 2)));
                if (dis <= minLocation)
                {
                    minLocation = dis;
                    st = itemS;
                }
            }
            object obj = st;
            if (obj != null)
                return st.ID;
            else
                return 0;
        }
        #endregion closeStation
        //------------------------------------------------------DRONE-------------------------------------------------------------
        #region addDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDrone(Drone droneToAdd)
        {
            if (DataSource.listD.Exists(x => x.ID == droneToAdd.ID))
                throw new AlreadyExistException("the drone is already exists");
            DataSource.listD.Add(droneToAdd);
        }
        #endregion addDrone

        #region addDroneInCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDroneInCharge(DroneCharge droneToCharge)
        {
            if (DataSource.listDC.Exists(x => x.DroneID == droneToCharge.DroneID))
                throw new AlreadyExistException("the drone is already exists");
            DataSource.listDC.Add(droneToCharge);
        }
        #endregion addDroneInCharge

        #region getDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone getDrone(int droneID)
        {
            Drone myDrone = DataSource.listD.Find(x => x.ID == droneID);
            object obj = myDrone;
            if (obj != null || !myDrone.Deleted)
                return myDrone;
            throw new DoesntExistException("The drone does not exists");
        }
        #endregion getDrone

        #region updateTheDroneName
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateTheDroneName(int id, string name)
        {
            try
            {
                Drone myDrone = getDrone(id);
                myDrone.Model = name;
                DataSource.listD.Remove(getDrone(id));
                DataSource.listD.Add(myDrone);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion updateTheDroneName

        #region sendTheDroneToCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void sendTheDroneToCharge(int droneID, int stationID)
        {
            DroneCharge myDroneCharge = new DroneCharge();
            myDroneCharge.DroneID = droneID;
            myDroneCharge.StationID = stationID;
            myDroneCharge.TimeCharning = DateTime.Now;
            DataSource.listDC.Add(myDroneCharge);
        }
        #endregion sendTheDroneToCharge

        #region freeDroneFromCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void freeDroneFromCharge(int droneID)
        {
            DataSource.listDC.Remove(GetDroneCharge(droneID));
        }
        #endregion freeDroneFromCharge

        #region GetDroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneCharge(int droneID)
        {
            DroneCharge myDroneCharge = DataSource.listDC.Find(x => x.DroneID == droneID);
            object obj = myDroneCharge;
            if (obj != null)
                return myDroneCharge;
            throw new DoesntExistException("The drone does not exists");
        }
        #endregion GetDroneCharge

        #region viewListOfDrones
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> viewListOfDrones()
        {
            var list = from x in DataSource.listD
                       select x;
            return list;
        }
        #endregion viewListOfDrones

        #region ListOfDroneInCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> ListOfDroneInCharge()
        {
            var list = from x in DataSource.listDC
                       select GetDroneCharge(x.DroneID);
            return list;
        }
        #endregion ListOfDroneInCharge

        #region ListOfDroneInCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> ListOfDroneInCharge1(int stationI)
        {
            var list = DataSource.listDC.FindAll(x => x.StationID == stationI);
            var list2 = from x in list
                       select GetDroneCharge(x.DroneID);
            return list2;
        }
        #endregion ListOfDroneInCharge

        #region distance
        double distance(double lon1, double lat1, double lon2, double lat2)
        {
            return Math.Sqrt((Math.Pow(lon1 - lon2, 2)
                                    + Math.Pow(lat1 - DataSource.listS[0].Latitude, 2)));
        }
        #endregion distance
        //----------------------------------------------------CUSTOMER----------------------------------------------------------
        #region addCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(Customer customerToAdd)
        {
            if (DataSource.listC.Exists(x => x.ID == customerToAdd.ID))
                throw new AlreadyExistException("the customer is already exists");
            DataSource.listC.Add(customerToAdd);
        }
        #endregion addCustomer

        #region GetCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int customerID)
        {
            Customer myCustomer = DataSource.listC.Find(x => x.ID == customerID);
            object obj = myCustomer;
            if (obj != null || !myCustomer.Deleted)
                return myCustomer;
            throw new DoesntExistException("The customer does not exists");
        }
        #endregion GetCustomer

        #region viewListOfCustomers
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> viewListOfCustomers()
        {
            var list = from x in DataSource.listC
                       select x;
            return list;
        }
        #endregion viewListOfCustomers

        #region updateCustomerName
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomerName(int id, string name)
        {
            try
            {
                Customer myCustomer = GetCustomer(id);
                myCustomer.Name = name;
                DataSource.listC.Remove(GetCustomer(id));
                DataSource.listC.Add(myCustomer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion updateCustomerName

        #region updateCustomerPhone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomerPhone(int id, string phone, int x)
        {
            try
            {
                Customer myCustomer = GetCustomer(id);
                myCustomer.Phone = phone;
                DataSource.listC.Remove(GetCustomer(id));
                DataSource.listC.Add(myCustomer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion updateCustomerPhone

        #region updateCustomerNameAndPhone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomerNameAndPhone(int id, string phone, string name)
        {
            try
            {
                Customer myCustomer = GetCustomer(id);
                myCustomer.Name = name;
                myCustomer.Phone = phone;
                DataSource.listC.Remove(GetCustomer(id));
                DataSource.listC.Add(myCustomer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion updateCustomerNameAndPhone
        //----------------------------------------------------PARCEL-------------------------------------------------------------

        #region addParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int addParcel(Parcel parcelToAdd)
        {
            if (DataSource.listP.Exists(x => x.ID == parcelToAdd.ID))
                throw new AlreadyExistException("the parcel is already exists");
            parcelToAdd.ID = DataSource.Config.number++;
            DataSource.listP.Add(parcelToAdd);
            return DataSource.Config.number;
        }
        #endregion addParcel

        #region getParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel getParcel(int parcelID)
        {
            Parcel myParcel = DataSource.listP.Find(x => x.ID == parcelID);
            object obj = myParcel;
            if (obj != null || !myParcel.Deleted)
                return myParcel;
            throw new DoesntExistException("The parcel does not exists");
        }
        #endregion getParcel

        #region getParcelBelongsDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel getParcelBelongsDrone(int droneID)
        {
            Parcel myParcel = DataSource.listP.Find(x => x.DroneID == droneID);
            object obj = myParcel;
            if (obj != null || !myParcel.Deleted)
                return myParcel;
           throw new DoesntExistException("There is no package associated with the drone");
        }
        #endregion getParcelBelongsDrone

        #region connectPrcelToDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void connectPrcelToDrone(int droneID, int parcelID)
        {
            try
            {
                Parcel myParcel = getParcel(parcelID);
                myParcel.DroneID = droneID;
                myParcel.Scheduled = DateTime.Now;
                DataSource.listP.Remove(getParcel(parcelID));
                DataSource.listP.Add(myParcel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion connectPrcelToDrone

        #region collectParcelByDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void collectParcelByDrone(int parcelID)
        {
            try
            {
                Parcel myParcel = getParcel(parcelID);
                myParcel.PickedUp = DateTime.Now;
                DataSource.listP.Remove(getParcel(parcelID));
                DataSource.listP.Add(myParcel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion collectParcelByDrone


        #region DeliverTheParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliverTheParcel(int parcelID)
        {
            try
            {
                Parcel myParcel = getParcel(parcelID);
                myParcel.Delivered = DateTime.Now;
                DataSource.listP.Remove(getParcel(parcelID));
                DataSource.listP.Add(myParcel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion DeliverTheParcel


        #region viewListOfParcels
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> viewListOfParcels()
        {
            var list = from x in DataSource.listP
                       select x;
            return list;
        }
        #endregion viewListOfParcels

        #region viewListOfFreeParcels
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> viewListOfFreeParcels()
        {
            var list = from x in DataSource.listP
                       where x.Scheduled == null
                       select x;
            return list;
        }
        #endregion viewListOfFreeParcels		

        #region addDroneInCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDroneInCharge(int id)
        {
            throw new NotImplementedException();
        }
        #endregion addDroneInCharge		

    }
}
