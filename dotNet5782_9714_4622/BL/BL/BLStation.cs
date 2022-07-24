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
        #region addStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addStation(station s)
        {
            lock (dal)
            {
                try
                {
                    DO.Station stationIDL = new DO.Station();
                    stationIDL.ID = s.ID;
                    stationIDL.Name = s.name;
                    stationIDL.Longitude = s.stationLocation.longitude;
                    stationIDL.Latitude = s.stationLocation.latitude;
                    stationIDL.ChargeSlots = s.numberOfStands;
                    dal.addStation(stationIDL);
                }
                catch (Exception ex)
                {
                    throw new AddingProblemException("can not add a station", ex);
                }
            }
        }
        #endregion addStation

        #region convertFromDrone
        droneInCharning convertFromDrone(DO.DroneCharge droneToConvert)
        {
            lock (dal)
            {
                drone dr = getDrone(droneToConvert.DroneID);
                return new droneInCharning
                {
                    ID = droneToConvert.DroneID,
                    battery = dr.battery
                };
            }
        }
        #endregion convertFromDrone

        #region convertToListSObj
        stationToList convertToListSObj(DO.Station stationToConvert)
        {
            lock (dal)
            {
                stationToList convertedStation = new stationToList();
                station sta = new station();
                sta = getStation(stationToConvert.ID);
                convertedStation.ID = stationToConvert.ID;
                convertedStation.name = stationToConvert.Name;
                convertedStation.numberOfAvailableStands = sta.numberOfStands;
                if (sta.drones != null)
                    convertedStation.numberOfOccupiedStands = sta.drones.Count();
                else
                    convertedStation.numberOfOccupiedStands = 0;
                return convertedStation;
            }
        }
        #endregion convertToListSObj

        #region getStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public station getStation(int n)
        {
            lock (dal)
            {
                try
                {
                    station stationIBL = new station();
                    DO.Station stationIDL = dal.getStation(n);
                    stationIBL.ID = stationIDL.ID;
                    stationIBL.name = stationIDL.Name;
                    stationIBL.numberOfStands = stationIDL.ChargeSlots;
                    stationIBL.stationLocation = new location
                    {
                        latitude = stationIDL.Latitude,
                        longitude = stationIDL.Longitude
                    };
                    // IEnumerable<DO.Drone> sortedList = dal.ListOfDroneInCharge();

                    IEnumerable<DO.DroneCharge> sortedList = dal.ListOfDroneInCharge1(stationIBL.ID);

                    if (sortedList.Count() > 0)
                    {
                        stationIBL.drones = sortedList.Select(convertFromDrone);
                    }
                    else
                        stationIBL.drones = null;
                    return stationIBL;
                }
                catch (Exception ex)
                {
                    throw new GetDetailsProblemException("can not get the drone", ex);
                }
            }
        }
        #endregion getStation

        #region getStationToList
        public stationToList getStationToList(int n)
        {
            try
            {
                stationToList stationIBL = new stationToList();
                station t = getStation(n);
;                DO.Station stationIDL = dal.getStation(n);
                stationIBL.ID = stationIDL.ID;
                stationIBL.name = stationIDL.Name;
                stationIBL.numberOfAvailableStands = stationIDL.ChargeSlots;
                stationIBL.numberOfOccupiedStands = t.drones.Count();
               
                // IEnumerable<DO.Drone> sortedList = dal.ListOfDroneInCharge();

               
                return stationIBL;
            }
            catch (Exception ex)
            {
                throw new GetDetailsProblemException("can not get the drone", ex);
            }
        }
        #endregion

        #region closeStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int closeStation(double longtit, double latit)
        {
            lock (dal)
            {
                return dal.closeStation(longtit, latit);
            }
        }
        #endregion closeStation

        #region updateTheStationName
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateTheStationName(int id, string names)
        {
            lock (dal)
            {
                try
                {
                    dal.updateTheStationName(id, names);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion updateTheStationName

        #region updateNumberOfStands
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateNumberOfStands(int id, int numOfStands)
        {
            lock (dal)
            {
                try
                {
                    dal.updateNumberOfStands(id, numOfStands);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion updateNumberOfStands

        #region updateNameAndNumOfStands
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateNameAndNumOfStands(int id, int numOfStands, string names)
        {
            lock (dal)
            {
                try
                {
                    dal.updateNameAndNumOfStands(id, numOfStands, names);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion updateNameAndNumOfStands

        #region viewListOfStations
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<stationToList> viewListOfStations()
        {
            lock (dal)
            {
                IEnumerable<stationToList> listStat = new List<stationToList>();
                IEnumerable<DO.Station> sortedLost = new List<DO.Station>();
                sortedLost = dal.viewListOfStations();
                listStat = sortedLost.Select(convertToListSObj);
                return listStat;
            }
        }
        #endregion viewListOfStations

        #region ListSortingStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<stationToList> ListSortingStation(Func<stationToList, bool> predicate)
        {
            var v = viewListOfStations();
            var list = v.Where(predicate);
            return list;
        }
         
        #endregion ListSortingStation

    }
}