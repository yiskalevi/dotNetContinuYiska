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
        #region addParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int addParcel(parcel parcelToAdd)
        {
            lock (dal)
            {
                DO.Parcel parcelDal = new DO.Parcel();
                try
                {
                    parcelDal.ID = DataSource.Config.number++;
                    parcelDal.SenderID = parcelToAdd.sender.ID;
                    parcelDal.TargetID = parcelToAdd.recipient.ID;
                    parcelDal.Weight = (DO.WeightCategories)parcelToAdd.weight;
                    parcelDal.Priority = (DO.Priorities)parcelToAdd.priority;
                    parcelDal.DroneID = 0;
                    parcelDal.PickedUp = null;
                    parcelDal.created = DateTime.Now;
                    parcelDal.Scheduled = null;
                    parcelDal.Delivered = null;
                    return dal.addParcel(parcelDal);
                }
                catch (Exception ex)
                {
                    throw new AddingProblemException("can not add a parcel", ex);
                }
            }
        }
        #endregion addParcel

        #region getParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public parcel getParcel(int n)
        {
            lock (dal)
            {
                try
                {
                    parcel parcelBL = new parcel();
                    DO.Parcel parcelDL = dal.getParcel(n);     //Exception if does not exists
                    parcelBL.ID = parcelDL.ID;
                    parcelBL.weight = (enums.WeightCategories)parcelDL.Weight;
                    parcelBL.priority = (enums.Priorities)parcelDL.Priority;
                    parcelBL.creatingTime = parcelDL.created;
                    parcelBL.assignmintTime = parcelDL.Scheduled;
                    parcelBL.collictionTime = parcelDL.PickedUp;
                    parcelBL.deliveryTime = parcelDL.Delivered;
                    parcelBL.sender = new customerInP { ID = getCustomer(parcelDL.SenderID).ID, name = getCustomer(parcelDL.SenderID).name };
                    parcelBL.recipient = new customerInP { ID = getCustomer(parcelDL.TargetID).ID, name = getCustomer(parcelDL.TargetID).name };
                    if (parcelDL.DroneID != 0)
                    {
                        droneToList Dr = droneToList.Find(x => x.ID == parcelDL.DroneID);
                        parcelBL.droneInPar = new droneInParcel
                        {
                            ID = parcelDL.DroneID,
                            battery = Dr.battery,
                            droneLocation = new location
                            {
                                longitude = Dr.droneLocation.longitude,
                                latitude = Dr.droneLocation.latitude
                            }
                        };
                    }
                    return parcelBL;
                }
                catch (Exception ex)
                {
                    throw new GetDetailsProblemException("can not get the parcel", ex);
                }
            }
        }
        #endregion getParcel

        #region convertToPListObj
        [MethodImpl(MethodImplOptions.Synchronized)]
        parcelToList convertToPListObj(DO.Parcel parcelToConvert)
        {
            lock (dal)
            {
                parcelToList covertedParcal = new parcelToList();
                covertedParcal.ID = parcelToConvert.ID;
                covertedParcal.priority = (enums.Priorities)parcelToConvert.Priority;
                if (parcelToConvert.Scheduled == null)
                    covertedParcal.status = enums.parcelStatus.Defined;
                else
                {
                    if (parcelToConvert.PickedUp == null)
                        covertedParcal.status = enums.parcelStatus.Associated;
                    else
                    {
                        if (parcelToConvert.Delivered == null)
                            covertedParcal.status = enums.parcelStatus.Collected;
                        else
                            covertedParcal.status = enums.parcelStatus.Provided;
                    }
                }
                covertedParcal.weight = (enums.WeightCategories)parcelToConvert.Weight;
                covertedParcal.sender = new customerInP { ID = parcelToConvert.SenderID, name = getCustomer(parcelToConvert.SenderID).name };
                covertedParcal.recipient = new customerInP { ID = parcelToConvert.TargetID, name = getCustomer(parcelToConvert.TargetID).name };
                return covertedParcal;
            }
        }
        #endregion convertToPListObj

        #region viewListOfParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<parcelToList> viewListOfParcel()
        {
            lock (dal)
            {
                IEnumerable<DO.Parcel> sertedList = new List<DO.Parcel>();
                sertedList = dal.viewListOfParcels();
                IEnumerable<parcelToList> parcelList = new List<parcelToList>();
                parcelList = sertedList.Select(convertToPListObj);
                return parcelList;
            }
        }
        #endregion viewListOfParcel

        #region ListSortingParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<parcelToList> ListSortingParcel(Func<parcelToList, bool> b)
        {
            lock (dal)
            {
                IEnumerable<DO.Parcel> sertedList = new List<DO.Parcel>();
                sertedList = dal.viewListOfParcels();
                IEnumerable<parcelToList> parcelList = new List<parcelToList>();
                parcelList = sertedList.Select(convertToPListObj);
                parcelList = parcelList.Where(b);
                return parcelList;
            }
        }

        #endregion ListSortingParcel
    }
}