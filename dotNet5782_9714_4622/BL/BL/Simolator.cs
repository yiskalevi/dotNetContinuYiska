using System;
using BO;
using System.Threading;
using static BL.BL;
using System.Linq;

namespace BL
{
    class Simolator
    {
        public Simolator(BL bl, int droneId, Action update,  Func<bool> checkStop)
        {
            drone myDrone = new drone();
            myDrone.droneLocation = new location();
            myDrone.parcel = new parcelToTranser();
            int DELAY = 500;
            bool temp = true;
            while(!checkStop()&&temp)
            {
                lock (bl)
                {
                    myDrone = bl.getDrone(droneId);
                    update();
                    switch (myDrone.statusOfDrone)
                    {
                        case enums.DroneStatuses.Available:
                            {
                                lock (bl)
                                {
                                    try
                                    {
                                        Thread.Sleep(DELAY);
                                        bl.conectParcelToDrone(droneId);
                                        myDrone = bl.getDrone(droneId);
                                        update();
                                    }
                                    catch (NoBatteryOrParcelCloseException)
                                    {
                                        try
                                        {
                                            if (myDrone.battery < 100)
                                            {
                                                Thread.Sleep(DELAY);
                                                bl.sendDroneToCharge(myDrone.ID);
                                                myDrone = bl.getDrone(droneId);
                                                update();
                                            }
                                        }
                                        catch (Exception)// אם אין עמדות טעינה פנויות מוסיפים עמדת טעינה לתחנה בשביל למנוע לולאה אינסופית
                                        {
                                            int id = bl.closeStation(myDrone.droneLocation.longitude, myDrone.droneLocation.latitude);
                                            station stat = new station();
                                            stat = bl.getStation(id);
                                            bl.updateNumberOfStands(id, stat.numberOfStands++);
                                            myDrone = bl.getDrone(droneId);
                                            update();
                                        }
                                    }
                                    catch (NoMoreParcelException)
                                    {
                                        //temp=false;
                                        if (myDrone.battery < 100)
                                        {
                                            Thread.Sleep(DELAY);
                                            bl.sendDroneToCharge(myDrone.ID);
                                            myDrone = bl.getDrone(droneId);
                                            update();
                                        }
                                        else
                                            temp = false;
                                    }
                                    break;
                                }                                
                            }
                        case enums.DroneStatuses.Maintenance:
                            {
                                lock (bl)
                                {
                                    double time = 0;
                                    var timeInCharge =bl.dal.GetDroneCharge(myDrone.ID).TimeCharning;
                                    TimeSpan t = DateTime.Now - timeInCharge;
                                    time = t.Minutes /*+ (t.Days * 24 * 60) + (t.Hours * 60) + (t.Seconds * (1 / 60))*/;
                                    myDrone.battery = myDrone.battery + bl.dal.felectric()[4] * time;
                                    while (myDrone.battery<100)
                                    {
                                        Thread.Sleep(DELAY);
                                        myDrone.battery = myDrone.battery + bl.dal.felectric()[4];
                                        droneToList d = new droneToList();
                                        d.droneLocation = new location();
                                        int index = bl.indexDrone(myDrone.ID);
                                        d = bl.droneToList[index];
                                        d.battery = myDrone.battery;
                                        bl.droneToList[index]=d;
                                        update();
                                    }
                                    bl.freeDroneFromCharge(myDrone.ID);
                                    myDrone = bl.getDrone(droneId);
                                    update();
                                    break;
                                }
                            }
                        case enums.DroneStatuses.Shipping:
                            {
                                lock (bl)
                                {
                                    Thread.Sleep(DELAY);
                                    parcel p = bl.getParcel(myDrone.parcel.ID);
                                    if (p.collictionTime == null)
                                    {
                                        bl.collectParcelByDrone(myDrone.ID);
                                        myDrone = bl.getDrone(droneId);
                                    }
                                    else if (p.collictionTime != null)
                                    {
                                        bl.provideParcelByDrone(myDrone.ID);
                                        myDrone = bl.getDrone(droneId);
                                    }
                                    update();
                                    break;
                                }
                            }
                    }
                }

            }

        }

    }
}
