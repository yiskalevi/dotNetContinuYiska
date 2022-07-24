using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
using System.Runtime.CompilerServices;



namespace Dal
{
    sealed class DalXml : IDal
    {
        #region singlton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }
        DalXml() { }
        public static DalXml Instance { get => instance; }
        #endregion singlton
        static string dir = @"data\";

        string stationPath = @"dalStation.xml";
        string customerPath = @"dalCustomer.xml";
        string parcelPath = @"dalParcels.xml";
        string dronePath = @"dalDrone.xml";
        string droneCharge = @"‏‏‫dalDroneChargee.xml";
        string Manager = @"dalManger.xml";

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
        #region checkManager
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool checkManager(int i, int p)//הפונקציה שהוספתי 
        {
            List<Manager> ListManager = XMLTools.LoadListFromXMLSerializer<Manager>(Manager);
            Manager myManager = ListManager.Find(x => x.ID == i);
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
            List<Manager> ListManager = XMLTools.LoadListFromXMLSerializer<Manager>(Manager);
            var list = from station in ListManager
                       select station;
            return list;
        }
        #endregion ListOfManager
        //------------------------------------------------------STATION--------------------------------------------------
        #region addStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addStation(DO.Station stationToAdd)
        {
            List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            if (ListStudents.Exists(x => x.ID == stationToAdd.ID))
                throw new AlreadyExistException("The station already exist in the system");
            ListStudents.Add(stationToAdd);
            XMLTools.SaveListToXMLSerializer<Station>(ListStudents, stationPath);
        }
        #endregion addStation

        #region getStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station getStation(int stationID)
        {
            List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Station myStation = ListStudents.Find(x => x.ID == stationID);
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
            List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            try
            {
                Station myStation = getStation(id);
                myStation.Name = names;
                ListStudents.Remove(getStation(id));
                ListStudents.Add(myStation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            XMLTools.SaveListToXMLSerializer<Station>(ListStudents, stationPath);
        }
        #endregion updateTheStationName

        #region updateNumberOfStands
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateNumberOfStands(int id, int numOfStands) //getting only the number of stations
        {
            List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            try
            {
                Station myStation = getStation(id);
                myStation.ChargeSlots = numOfStands;
                ListStudents.Remove(getStation(id));
                ListStudents.Add(myStation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            XMLTools.SaveListToXMLSerializer<Station>(ListStudents, stationPath);
        }
        #endregion updateNumberOfStands

        #region updateNameAndNumOfStands
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateNameAndNumOfStands(int id, int numOfStands, string names) //getting both 
        {
            List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            try
            {
                Station myStation = getStation(id);
                myStation.ChargeSlots = numOfStands;
                myStation.Name = names;
                ListStudents.Remove(getStation(id));
                ListStudents.Add(myStation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            XMLTools.SaveListToXMLSerializer<Station>(ListStudents, stationPath);
        }
        #endregion updateNameAndNumOfStands

        #region viewListOfStations
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> viewListOfStations()
        {
            List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            var list = from station in ListStudents
                       select station;
            return list;
        }
        #endregion viewListOfStations

        #region viewListOfAvailableStations
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> viewListOfAvailableStations()
        {
            List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            var list = from x in ListStudents
                       where x.ChargeSlots > 0
                       select x;
            return list;
        }
        #endregion viewListOfAvailableStations

        #region closeStationToCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int closeStationToCharge(double droneLongitude, double droneLatitude, ref double minimLocation)
        {
            List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Station st = new Station();
            double minLocation = Math.Sqrt((Math.Pow(droneLongitude - ListStudents[0].Longitude, 2)
                                    + Math.Pow(droneLatitude - ListStudents[0].Latitude, 2)));
            foreach (DO.Station itemS in ListStudents)
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
            List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Station st = new Station();
            double minLocation = Math.Sqrt((Math.Pow(longtit - ListStudents[0].Longitude, 2)
                                    + Math.Pow(latit - ListStudents[0].Latitude, 2)));
            foreach (DO.Station itemS in ListStudents)
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
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            if (ListDrones.Exists(x => x.ID == droneToAdd.ID))
                throw new AlreadyExistException("the drone is already exists");
            ListDrones.Add(droneToAdd);
            XMLTools.SaveListToXMLSerializer<Drone>(ListDrones, dronePath);
        }
        #endregion addDrone

        #region addDroneInCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDroneInCharge(DroneCharge droneToCharge)
        {
            List<DroneCharge> ListDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneCharge);
            if (ListDronesCharge.Exists(x => x.DroneID == droneToCharge.DroneID))
                throw new AlreadyExistException("the drone is already exists");
            ListDronesCharge.Add(droneToCharge);
            XMLTools.SaveListToXMLSerializer<DroneCharge>(ListDronesCharge, droneCharge);
        }
        #endregion addDroneInCharge

        #region getDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone getDrone(int droneID)
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            Drone myDrone = ListDrones.Find(x => x.ID == droneID);
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
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            try
            {
                Drone myDrone = getDrone(id);
                myDrone.Model = name;
                ListDrones.Remove(getDrone(id));
                ListDrones.Add(myDrone);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            XMLTools.SaveListToXMLSerializer<Drone>(ListDrones, dronePath);
        }
        #endregion updateTheDroneName

        #region sendTheDroneToCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void sendTheDroneToCharge(int droneID, int stationID)
        {
            List<DroneCharge> ListDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneCharge);
            DroneCharge myDroneCharge = new DroneCharge();
            myDroneCharge.DroneID = droneID;
            myDroneCharge.StationID = stationID;
            myDroneCharge.TimeCharning = DateTime.Now;
            ListDronesCharge.Add(myDroneCharge);
            XMLTools.SaveListToXMLSerializer<DroneCharge>(ListDronesCharge, droneCharge);
        }
        #endregion sendTheDroneToCharge

        #region freeDroneFromCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void freeDroneFromCharge(int droneID)
        {
            List<DroneCharge> ListDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneCharge);
            ListDronesCharge.Remove(GetDroneCharge(droneID));
            XMLTools.SaveListToXMLSerializer<DroneCharge>(ListDronesCharge, droneCharge);
        }
        #endregion freeDroneFromCharge

        #region GetDroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneCharge(int droneID)
        {
            List<DroneCharge> ListDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneCharge);
            DroneCharge myDroneCharge = ListDronesCharge.Find(x => x.DroneID == droneID);
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
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            var list = from x in ListDrones
                       select x;
            return list;
        }
        #endregion viewListOfDrones

        #region ListOfDroneInCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> ListOfDroneInCharge()
        {
            List<DroneCharge> ListDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneCharge);
            var list = from x in ListDronesCharge
                       select GetDroneCharge(x.DroneID);
            return list;
        }
        #endregion ListOfDroneInCharge

        #region ListOfDroneInCharge1
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> ListOfDroneInCharge1(int stationI)
        {
            List<DroneCharge> ListDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneCharge);
            var list = ListDronesCharge.FindAll(x => x.StationID == stationI);
            var list2 = from x in list
                        select GetDroneCharge(x.DroneID);
            return list2;
        }
        #endregion ListOfDroneInCharge1

        #region distance
        [MethodImpl(MethodImplOptions.Synchronized)]
        double distance(double lon1, double lat1, double lon2, double lat2)
        {
            List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            return Math.Sqrt((Math.Pow(lon1 - lon2, 2)
                                    + Math.Pow(lat1 - ListStudents[0].Latitude, 2)));
        }
        #endregion distance
        //----------------------------------------------------CUSTOMER----------------------------------------------------------
        #region addCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(Customer customerToAdd)
        {
            XElement customerRoot = Dal.XMLTools.LoadData(customerPath);
            XElement custom;
            custom = (from p in customerRoot.Elements()
                      where Convert.ToInt32(p.Element("ID").Value) == customerToAdd.ID
                      select p).FirstOrDefault();
            if (custom == null)
            {
                XElement ID = new XElement("ID", customerToAdd.ID);
                XElement Name = new XElement("Name", customerToAdd.Name);
                XElement Phone = new XElement("Phone", customerToAdd.Phone);
                XElement Longitude = new XElement("Longitude", customerToAdd.Longitude);
                XElement Latitude = new XElement("Latitude", customerToAdd.Latitude);
                customerRoot.Add(new XElement("customer", ID, Name, Phone, Longitude, Latitude));
                customerRoot.Save(dir + customerPath);
            }
            else
            {
                Console.WriteLine("cannot adding test with id " + customerToAdd.ID + "...");
            }
        }
        #endregion addCustomer

        #region GetCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int customerID)
        {
            XElement customerRoot = Dal.XMLTools.LoadData(customerPath);
            Customer custom = new Customer();
            try
            {
                custom = (from p in customerRoot.Elements()
                          where Convert.ToInt32(p.Element("ID").Value) == customerID
                          select new Customer()
                          {
                              ID = Convert.ToInt32(p.Element("ID").Value),
                              Name = p.Element("Name").Value,
                              Phone = p.Element("Phone").Value,
                              Longitude = Convert.ToDouble(p.Element("Longitude").Value),
                              Latitude = Convert.ToDouble(p.Element("Latitude").Value),
                          }).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return custom;
        }
        #endregion GetCustomer

        #region viewListOfCustomers
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> viewListOfCustomers()
        {
            XElement customerRoot = Dal.XMLTools.LoadData(customerPath);
            List<Customer> allTcust;
            try
            {
                allTcust = (from p in customerRoot.Elements()
                            select new Customer()
                            {
                                ID = Convert.ToInt32(p.Element("ID").Value),
                                Name = p.Element("Name").Value,
                                Phone = p.Element("Phone").Value,
                                Longitude = Convert.ToDouble(p.Element("Longitude").Value),
                                Latitude = Convert.ToDouble(p.Element("Latitude").Value)
                            }).ToList();
            }
            catch
            {
                allTcust = null;
            }
            return allTcust;
        }
        #endregion viewListOfCustomers

        #region updateCustomerName
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomerName(int id, string name)
        {
            XElement customerRoot = Dal.XMLTools.LoadData(customerPath);
            XElement custom = (from p in customerRoot.Elements()
                               where Convert.ToInt32(p.Element("ID").Value) == id
                               select p).FirstOrDefault();
            custom.Element("Name").Value = name.ToString();
            customerRoot.Save(dir + customerPath);
        }
        #endregion updateCustomerName

        #region updateCustomerPhone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomerPhone(int id, string phone, int x)
        {
            XElement customerRoot = Dal.XMLTools.LoadData(customerPath);
            XElement custom = (from p in customerRoot.Elements()
                               where Convert.ToInt32(p.Element("ID").Value) == id
                               select p).FirstOrDefault();
            custom.Element("phone").Value = phone.ToString();
            customerRoot.Save(dir + customerPath);
        }
        #endregion updateCustomerPhone

        #region updateCustomerNameAndPhone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomerNameAndPhone(int id, string phone, string name)
        {
            XElement customerRoot = Dal.XMLTools.LoadData(customerPath);
            XElement custom = (from p in customerRoot.Elements()
                               where Convert.ToInt32(p.Element("ID").Value) == id
                               select p).FirstOrDefault();
            custom.Element("Name").Value = name.ToString();
            custom.Element("phone").Value = phone.ToString();
            customerRoot.Save(dir + customerPath);
        }
        #endregion updateCustomerNameAndPhone
        //----------------------------------------------------PARCEL-------------------------------------------------------------

        #region addParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int addParcel(Parcel parcelToAdd)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            if (ListParcels.Exists(x => x.ID == parcelToAdd.ID))
                throw new AlreadyExistException("the parcel is already exists");
            parcelToAdd.ID = DataSource.Config.number++;
            ListParcels.Add(parcelToAdd);
            XMLTools.SaveListToXMLSerializer<Parcel>(ListParcels, parcelPath);
            return DataSource.Config.number;           
        }
        #endregion addParcel

        #region getParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel getParcel(int parcelID)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel myParcel = ListParcels.Find(x => x.ID == parcelID);
            object obj = myParcel;
            if (obj != null || !myParcel.Deleted)
                return myParcel;
            throw new DoesntExistException("The parcel does not exists");
        }
        #endregion getParcel

        #region connectPrcelToDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void connectPrcelToDrone(int droneID, int parcelID)
        {             
            try
            {
                List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
                Parcel myParcel = getParcel(parcelID);
                myParcel.DroneID = droneID;
                myParcel.Scheduled = DateTime.Now;
                ListParcels.Remove(getParcel(parcelID));
                ListParcels.Add(myParcel);
                XMLTools.SaveListToXMLSerializer<Parcel>(ListParcels, parcelPath);
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
                List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
                Parcel myParcel = getParcel(parcelID);
                myParcel.PickedUp = DateTime.Now;
                ListParcels.Remove(getParcel(parcelID));
                ListParcels.Add(myParcel);
                XMLTools.SaveListToXMLSerializer<Parcel>(ListParcels, parcelPath);
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
                List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
                Parcel myParcel = getParcel(parcelID);
                myParcel.Delivered = DateTime.Now;
                ListParcels.Remove(getParcel(parcelID));
                ListParcels.Add(myParcel);
                XMLTools.SaveListToXMLSerializer<Parcel>(ListParcels, parcelPath);
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
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var list = from x in ListParcels
                       select x;
            return list;
        }
        #endregion viewListOfParcels

        #region viewListOfFreeParcels
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> viewListOfFreeParcels()
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var list = from x in ListParcels
                       where x.Scheduled == null
                       select x;
            return list;
        }
        #endregion viewListOfFreeParcels

        #region getParcelBelongsDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel getParcelBelongsDrone(int droneID)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel myParcel = ListParcels.Find(x => x.DroneID == droneID);
            object obj = myParcel;
            if (obj != null || !myParcel.Deleted)
                return myParcel;
            throw new DoesntExistException("The parcel does not exists");         
        }
        #endregion getParcelBelongsDrone
    }
}
