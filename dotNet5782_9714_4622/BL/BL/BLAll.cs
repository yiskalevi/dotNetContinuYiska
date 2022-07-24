using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using BLApi;
using DalApi;
using BO;
using System.Runtime.CompilerServices;


namespace BL

{
    public partial class BL : IBl
    {
        static readonly IBl instance = new BL();
        public static IBl Instance { get => instance; }

        static BL() { }

        internal IDal dal = DalFactory.GetDal();

        public List<droneToList> droneToList;

        #region checkManager
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool checkManager(int i, int p)
        {
            lock (dal)
            {
                return dal.checkManager(i, p);

            }
        }
        #endregion

        #region distance
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double distance(location x, location y)
        {
            return Math.Sqrt((Math.Pow(x.longitude - y.longitude, 2)
                                + Math.Pow(x.latitude - y.latitude, 2)));
        }
        #endregion

        #region isEnough
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double isEnough(drone locDrone, location locPrcel)
        {
            double minLocation = distance(locDrone.droneLocation, locPrcel);
            double power = dal.felectric()[0] * minLocation;

            return locDrone.battery - power;

        }
        #endregion

        #region isEnough1
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double isEnough1(drone locDrone, location locPrcel, enums.WeightCategories wei)
        {
            lock (dal)
            {
                double power = 0;

                double minLocation = distance(locDrone.droneLocation, locPrcel);
                if (wei == enums.WeightCategories.heavy)
                    power = dal.felectric()[3] * minLocation;
                if (wei == enums.WeightCategories.Medium)
                    power = dal.felectric()[1] * minLocation;
                if (wei == enums.WeightCategories.Light)
                    power = dal.felectric()[2] * minLocation;
                return locDrone.battery - power;
            }
        }
        #endregion

        #region minPar
        [MethodImpl(MethodImplOptions.Synchronized)]
        public parcelToList minPar(location drl, List<parcelToList> ls)
        {
            lock (dal)
            {
                customer cus = new customer();
                cus = getCustomer(ls[0].sender.ID);
                parcelToList par = new parcelToList();
                par.sender = new customerInP();
                par = ls[0];
                double max = distance(drl, cus.locationC);
                foreach (parcelToList item in ls)
                {
                    cus = getCustomer(item.sender.ID);
                    double x = distance(drl, cus.locationC);
                    if (x > max)
                    {
                        max = x;
                        par = item;
                    }
                }
                return par;
            }
        }
        #endregion

        #region ListSorting
        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<parcelToList> ListSorting(Predicate<parcelToList> match, List<parcelToList> v)
        {
            v = v.FindAll(match);
            return v;
        }
        #endregion
        
        #region WeightMax
        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<parcelToList>WeightMax(List<parcelToList>  viewParcel, enums.WeightCategories w)
        {
            List<parcelToList> list = new List<parcelToList>();
            if (w == enums.WeightCategories.heavy)
                list = viewParcel.FindAll(x => x.weight == enums.WeightCategories.heavy);
            if (w == enums.WeightCategories.Medium||list.Count()==0)
                list = viewParcel.FindAll(x => x.weight == enums.WeightCategories.Medium);
            if(list.Count() == 0)
                list = viewParcel.FindAll(x => x.weight == enums.WeightCategories.Light);
            return list;
        }
        #endregion

        #region weightList
        [MethodImpl(MethodImplOptions.Synchronized)]
        int weightList(enums.WeightCategories categories)
        {
            if (categories == enums.WeightCategories.heavy)
                return 3;
            if (categories == enums.WeightCategories.Light)
                return 2;
            if (categories == enums.WeightCategories.Medium)
                return 1;
            else
                return 0;
        }
        #endregion

        #region BL
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BL()
        {
            lock (dal)
            {
                droneToList = new List<droneToList>();
                foreach (DO.Drone itemD in dal.viewListOfDrones())
                {
                    droneToList myDrone = new droneToList();
                    myDrone.ID = itemD.ID;
                    myDrone.model = itemD.Model;
                    myDrone.weight = (enums.WeightCategories)itemD.MaxWeight;
                    IEnumerable<DO.Parcel> listOFParcels = new List<DO.Parcel>();
                    double mininmumBattery = 0;
                    double minLocation = 0;
                    Random r = new Random();
                    Random station = new Random();
                    listOFParcels = dal.viewListOfParcels();
                    listOFParcels = listOFParcels.Where(x => (x.DroneID == itemD.ID) && x.Delivered == null);
                    if (listOFParcels.Count() > 0) //במקרה שיש חבילה ששויכה אך עוד לא סופקה(מקסימום תהיה חבילה אחת, כי בודקים לפני שמשייכים אם הרחפן פנוי
                    {

                        myDrone.statusOfDrone = enums.DroneStatuses.Shipping;
                        if (listOFParcels.First().PickedUp == null)
                        {
                            myDrone.droneLocation = new location();
                            myDrone.droneLocation = getStation(dal.closeStation(getCustomer(listOFParcels.First().SenderID).locationC.longitude
                                , getCustomer(listOFParcels.First().SenderID).locationC.latitude)).stationLocation;
                            mininmumBattery = distance(myDrone.droneLocation, getCustomer(listOFParcels.First().SenderID).locationC)
                                * dal.felectric()[0] + distance(getCustomer(listOFParcels.First().SenderID).locationC, getCustomer(listOFParcels.First().TargetID).locationC)
                                * dal.felectric()[weightList((enums.WeightCategories)listOFParcels.First().Weight)];
                            dal.closeStationToCharge(getCustomer(listOFParcels.First().TargetID).locationC.longitude, getCustomer(listOFParcels.First().TargetID).locationC.latitude, ref minLocation);
                            mininmumBattery = mininmumBattery + minLocation * dal.felectric()[0];
                        }
                        else
                        {
                            myDrone.droneLocation = new location();
                            myDrone.droneLocation = getCustomer(listOFParcels.First().SenderID).locationC;
                            mininmumBattery = distance(getCustomer(listOFParcels.First().SenderID).locationC, getCustomer(listOFParcels.First().TargetID).locationC)
                                * dal.felectric()[weightList((enums.WeightCategories)listOFParcels.First().Weight)];
                            dal.closeStationToCharge(getCustomer(listOFParcels.First().TargetID).locationC.longitude, getCustomer(listOFParcels.First().TargetID).locationC.latitude, ref minLocation);
                            mininmumBattery = mininmumBattery + minLocation * dal.felectric()[0];
                        }
                        myDrone.battery = r.Next((int)mininmumBattery - 1, 101);
                        myDrone.parcelNumber = listOFParcels.First().ID;
                    }
                    else
                    {
                        int status = r.Next(2);
                        if (status == 0)
                        {
                            myDrone.statusOfDrone = enums.DroneStatuses.Maintenance;
                            IEnumerable<DO.Station> stationList = new List<DO.Station>();
                            stationList = dal.viewListOfAvailableStations();
                            int s = station.Next(stationList.Count() - 1);
                            myDrone.droneLocation = new location();
                            myDrone.droneLocation.longitude = stationList.First().Longitude;
                            myDrone.droneLocation.latitude = stationList.First().Latitude;
                            dal.sendTheDroneToCharge(myDrone.ID, stationList.First().ID);
                            myDrone.battery = station.Next(1, 20);
                        }
                        else
                        {
                            myDrone.statusOfDrone = enums.DroneStatuses.Available;
                            IEnumerable<customerToList> customerList = new List<customerToList>();
                            customerList = viewListOfCustomer();
                            customerList.Where(x => (x.numberOfP > 0));
                            if (customerList.Count() > 0)
                            {
                                int c = station.Next(1, customerList.Count() - 1);
                                customer cus = new customer();
                                cus = getCustomer(customerList.First().ID);
                                myDrone.droneLocation = new location();
                                myDrone.droneLocation = cus.locationC;
                            }
                            dal.closeStationToCharge(myDrone.droneLocation.longitude, myDrone.droneLocation.latitude, ref minLocation);
                            mininmumBattery = minLocation * dal.felectric()[0];
                            myDrone.battery = r.Next((int)mininmumBattery - 1, 101);
                        }
                    }
                    droneToList.Add(myDrone);
                }
            }
        }
        #endregion BL

        #region startSimolator
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void startSimolator(int droneId, Action a, Func<bool> stoop)
        {
            Simolator simolator = new Simolator(this, droneId, a, stoop);
        }
        #endregion startSimolator

    }
}


