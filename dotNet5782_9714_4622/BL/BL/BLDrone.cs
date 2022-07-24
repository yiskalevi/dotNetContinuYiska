using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using Dal;
using BLApi;
using DalApi;
using System.Runtime.CompilerServices;


namespace BL

{
    public partial class BL : IBl
    {
        #region addDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDrone(drone droneToAdd, int stat)
        {
            lock (dal)
            {
                try
                {
                    DO.Drone droneDal = new DO.Drone();
                    droneDal.ID = droneToAdd.ID;
                    droneDal.Model = droneToAdd.model;
                    droneDal.MaxWeight = (DO.WeightCategories)droneToAdd.weight;
                    dal.addDrone(droneDal);
                    DO.DroneCharge droneDalc = new DO.DroneCharge();
                    droneDalc.DroneID = droneToAdd.ID;
                    droneDalc.StationID = stat;
                    dal.addDroneInCharge(droneDalc);
                    droneToList drl = new droneToList();
                    drl.ID = droneToAdd.ID;
                    drl.model = droneToAdd.model;
                    drl.weight = droneToAdd.weight;
                    drl.statusOfDrone = enums.DroneStatuses.Maintenance;
                    drl.parcelNumber = 0;
                    DO.Station sta = new DO.Station();
                    sta = dal.getStation(stat);
                    location locS = new location();
                    locS.longitude = sta.Longitude;
                    locS.latitude = sta.Latitude;
                    drl.droneLocation = locS;
                    Random r = new Random();
                    int x = r.Next(19, 41);
                    drl.battery = x;
                    droneToList.Add(drl);
                }
                catch (Exception ex)
                {
                    throw new AddingProblemException("can not add a drone", ex);
                }
            }
        }
        #endregion addDrone

        #region getDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public drone getDrone(int n)
        {
            lock (dal)
            {
                droneToList drlppp = droneToList.Find(x => x.ID == n);
                drone droneToGet = new drone();
                droneToGet.ID = n;
                droneToGet.model = drlppp.model;
                droneToGet.battery = drlppp.battery;
                droneToGet.droneLocation = drlppp.droneLocation;
                droneToGet.weight = drlppp.weight;
                droneToGet.statusOfDrone = drlppp.statusOfDrone;
                parcelToTranser parcToTrans = new parcelToTranser();
                parcel pp = getParcel(drlppp.parcelNumber);
                droneToGet.parcel = new parcelToTranser();
                if (drlppp.parcelNumber != 0)
                {
                    parcToTrans.ID = drlppp.parcelNumber;
                    if (drlppp.statusOfDrone == enums.DroneStatuses.Available)
                        parcToTrans.isDelivered = true;
                    else
                        parcToTrans.isDelivered = false;
                    parcToTrans.priority = pp.priority;
                    parcToTrans.weight = pp.weight;
                    parcToTrans.sender = pp.sender;
                    parcToTrans.recipient = pp.recipient;
                    parcToTrans.collection = new location
                    {
                        longitude = getCustomer(pp.sender.ID).locationC.longitude,
                        latitude = getCustomer(pp.sender.ID).locationC.latitude
                    };
                    parcToTrans.destination = new location
                    {
                        longitude = getCustomer(pp.recipient.ID).locationC.longitude,
                        latitude = getCustomer(pp.recipient.ID).locationC.latitude
                    };
                    parcToTrans.distance = Math.Sqrt((Math.Pow(parcToTrans.collection.longitude - parcToTrans.destination.longitude, 2)
                                      + Math.Pow(parcToTrans.collection.latitude - parcToTrans.destination.latitude, 2)));
                    droneToGet.parcel = parcToTrans;
                }
                return droneToGet;
            }
        }
        #endregion getDrone

        #region updateTheDroneName
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateTheDroneName(int id, string model)
        {
            lock (dal)
            {
                try
                {
                    int indexD = 0;
                    for (int i = 0; i < droneToList.Count(); i++)
                    {
                        if (droneToList[i].ID == id)
                            indexD = i;
                    }
                    droneToList d1 = new droneToList();
                    d1.droneLocation = new location();
                    d1 = droneToList[indexD];
                    d1.model = model;
                    droneToList[indexD] = d1;
                    dal.updateTheDroneName(id, model);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion updateTheDroneName

        #region sendDroneToCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void sendDroneToCharge(int id)
        {
            lock (dal)
            {
                int index = indexDrone(id);
                double minLocation = 0;
                droneToList droneToCharge = new droneToList();
                droneToCharge.droneLocation = new location();
                droneToCharge = droneToList[index];
                if (droneToCharge.statusOfDrone != enums.DroneStatuses.Available)
                    throw new Exception();
                int idStation = dal.closeStation(droneToCharge.droneLocation.longitude, droneToCharge.droneLocation.latitude);
                if (idStation == 0)
                    throw new Exception();
                station statinToCharge = new station();
                statinToCharge.stationLocation = new location();
                statinToCharge = getStation(idStation);
                minLocation = distance(droneToCharge.droneLocation, statinToCharge.stationLocation);
                double power = dal.felectric()[0] * minLocation;
                if (getDrone(id).battery - power > 0)
                {
                    droneToCharge.battery = droneToCharge.battery - power;
                    droneToCharge.droneLocation = statinToCharge.stationLocation;
                    droneToCharge.statusOfDrone = enums.DroneStatuses.Maintenance;
                    updateNumberOfStands(statinToCharge.ID, statinToCharge.numberOfStands - 1);
                    dal.sendTheDroneToCharge(id, statinToCharge.ID);
                    droneToList[index] = droneToCharge;
                }
            }
        }
        #endregion sendDroneToCharge

        #region freeDroneFromCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void freeDroneFromCharge(int id)
        {
            lock (dal)
            {
                double timeToCharge = 0;
                int index = indexDrone(id);
                droneToList droneFromCharge = new droneToList();
                droneFromCharge.droneLocation = new location();
                droneFromCharge = droneToList[index];
                if (droneFromCharge.statusOfDrone != enums.DroneStatuses.Maintenance)
                    throw new Exception();//לזרוק חריגה מתאימה
                DO.DroneCharge droneCHarge = dal.GetDroneCharge(id);
                TimeSpan t = DateTime.Now - droneCHarge.TimeCharning;
                timeToCharge = t.Minutes + (t.Days * 24 * 60) + (t.Hours * 60) + (t.Seconds * (1 / 60));
                droneFromCharge.battery = droneFromCharge.battery + dal.felectric()[4] * timeToCharge;
                if (droneFromCharge.battery > 100)
                    droneFromCharge.battery = 100;
                droneFromCharge.statusOfDrone = enums.DroneStatuses.Available;
                station stationToCharge = getStation(droneCHarge.StationID);
                updateNumberOfStands(stationToCharge.ID, stationToCharge.numberOfStands + 1);
                dal.freeDroneFromCharge(id);//deleting the DroneCharge
                droneToList[index] = droneFromCharge;
            }
        }
        #endregion freeDroneFromCharge

        #region viewListOfDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<droneToList> viewListOfDrone()
        {
            return droneToList;
        }
        #endregion viewListOfDrone

        #region conectParcelToDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void conectParcelToDrone(int id)
        {
            lock (dal)
            {
                double x = 0;
                location par = new location();
                customer cus = new customer();
                drone dr = getDrone(id);
                if (dr.statusOfDrone == enums.DroneStatuses.Available)
                {
                    List<parcelToList> viewParcel = new List<parcelToList>();
                    List<parcelToList> viewParcelHelp = new List<parcelToList>();
                    double p = 0;
                    var v = viewListOfParcel();
                    foreach (var item in v)
                        viewParcel.Add(item);
                    viewParcel = viewParcel.FindAll(x => getParcel(x.ID).assignmintTime == null);
                    if (viewParcel.Count != 0)
                    {
                        foreach (parcelToList item in viewParcel)
                        {
                            cus = getCustomer(item.sender.ID);
                            x = isEnough(dr, cus.locationC);
                            dr.battery = x;//בטריה עד ללקוח
                            dr.droneLocation = cus.locationC;
                            cus = getCustomer(item.recipient.ID);
                            x = isEnough1(dr, cus.locationC, item.weight);
                            dr.battery = x;//בטריה עד ליעד
                            location statLoc = new location();
                            station stationClose = new station();
                            stationClose.stationLocation = new location();
                            int isStationClosedal = dal.closeStationToCharge(dr.droneLocation.longitude, dr.droneLocation.latitude, ref p);
                            if (isStationClosedal == 0)
                                throw new GetDetailsProblemException("can not get the drone");
                            stationClose = getStation(isStationClosedal);
                            statLoc.longitude = stationClose.stationLocation.longitude;
                            statLoc.latitude = stationClose.stationLocation.latitude;
                            dr.droneLocation = cus.locationC;
                            x = isEnough(dr, statLoc);
                            if (x > 0)
                                viewParcelHelp.Add(item);
                        }
                        if (viewParcelHelp.Count() == 0)
                            throw new NoBatteryOrParcelCloseException("There are no parcel close enough to the drone");
                        viewParcel = viewParcelHelp;
                        viewParcel.FindAll(x => x.priority == enums.Priorities.Emergency);
                        viewParcel = WeightMax(viewParcel, dr.weight);
                        if (viewParcel.Count() == 0)
                        {
                            viewParcel = viewParcelHelp;
                            viewParcel.FindAll(x => x.priority == enums.Priorities.Fast);
                            viewParcel = WeightMax(viewParcel, dr.weight);
                        }
                        if (viewParcel.Count() == 0)
                        {
                            viewParcel = viewParcelHelp;
                            viewParcel.FindAll(x => x.priority == enums.Priorities.Regular);
                            viewParcel = WeightMax(viewParcel, dr.weight);
                        }
                        parcelToList parcel1 = new parcelToList();
                        parcel1 = minPar(dr.droneLocation, viewParcel);
                        dal.connectPrcelToDrone(dr.ID, parcel1.ID);
                        int indexD = indexDrone(id);
                        droneToList d1 = new droneToList();
                        d1.droneLocation = new location();
                        d1 = droneToList[indexD];
                        d1.statusOfDrone = enums.DroneStatuses.Shipping;
                        d1.parcelNumber = parcel1.ID;
                        droneToList[indexD] = d1;
                    }
                    else//אין חבילה שממתינה לשיוך
                        throw new NoMoreParcelException("No More Parcel");
                }
                else//הרחפן לא במצב משלוח
                    throw new GetDetailsProblemException("No suitable parcel");
            }
        }
        #endregion

        #region indexDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int indexDrone(int id)
        {
            for (int i = 0; i < droneToList.Count; i++)
            {
                if (droneToList[i].ID == id)
                    return i;
            }
            return -1;
        }
        #endregion

        #region provideParcelByDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void provideParcelByDrone(int id)
        {
            lock (dal)
            {
                try
                {
                    parcel pr = new parcel();
                    pr.recipient = new customerInP();
                    droneToList dr;
                    int indexD = indexDrone(id);
                    dr = droneToList[indexD];
                    pr = getParcel(dr.parcelNumber);
                    if (dr.statusOfDrone == enums.DroneStatuses.Shipping && pr.deliveryTime == null)//בדיקה אם הוא אסף את החבילה לא בטוחה שזה מספיק
                    {
                        customer c = new customer();
                        c = getCustomer(pr.recipient.ID);
                        double x = distance(c.locationC, dr.droneLocation);
                        int j = (int)pr.weight + 1;
                        dr.battery = dr.battery - (x * dal.felectric()[j]);//צריכת הבטריה
                        dr.droneLocation = c.locationC;
                        dr.statusOfDrone = enums.DroneStatuses.Available;
                        droneToList[indexD] = dr;
                        dal.DeliverTheParcel(dr.parcelNumber);
                        dr.parcelNumber = 0;
                    }
                    else
                    {
                        throw new NotImplementedException();
                        //אי אפשר לספק את החבילה תיזרק חריגה מתאימה
                    }
                }
                catch (Exception ex)
                {
                    throw new NotImplementedException("Error", ex);
                }
            }

        }
        #endregion

        #region ListSortingDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<droneToList> ListSortingDrone(Predicate<droneToList> match)
        {
            lock (dal)
            {
                List<droneToList> list11 = new List<droneToList>();
                list11 = droneToList.FindAll(match);
                return list11;
            }
        }
        #endregion

        #region collectParcelByDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void collectParcelByDrone(int id)
        {
            lock (dal)
            {
                DO.Parcel pr = new DO.Parcel();
                droneToList dr = new droneToList();
                dr.droneLocation = new location();
                int indexD = 0;
                for (int i = 0; i < droneToList.Count; i++)//מציאת הרחפן
                {
                    if (droneToList[i].ID == id)
                        indexD = i;
                }
                dr = droneToList[indexD];
                if (dr.statusOfDrone == enums.DroneStatuses.Shipping)
                {
                    pr = dal.getParcelBelongsDrone(id);
                    if (pr.PickedUp == null)
                    {
                        customer c = new customer();
                        c.locationC = new location();
                        c = getCustomer(pr.SenderID);
                        double x = distance(c.locationC, droneToList[indexD].droneLocation);
                        dal.collectParcelByDrone(pr.ID);
                        dr.battery = dr.battery - (x * dal.felectric()[0]);//צריכת הבטריה
                        dr.droneLocation = c.locationC;
                        droneToList[indexD] = dr;
                    }
                    else
                        throw new NotImplementedException();
                }
                else
                {
                    throw new NotImplementedException();
                    //לזרוק שגיאה שהוא לא מבצע משלוח
                }
            }
        }
        #endregion
    }
}