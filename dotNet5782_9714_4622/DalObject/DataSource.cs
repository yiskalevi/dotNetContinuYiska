using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{
    public static class DataSource
    {
        //needs to be internal
        internal static List<DO.Drone> listD = new List<DO.Drone>();
        internal static List<DO.Station> listS = new List<DO.Station>();
        internal static List<DO.Customer> listC = new List<DO.Customer>();
        internal static List<DO.Parcel> listP = new List<DO.Parcel>();
        internal static List<DO.DroneCharge> listDC = new List<DO.DroneCharge>();
        internal static List<DO.Manager> listManager = new List<DO.Manager>(); //יסכה הוסיפה בשביל שיהיה נתונים של מנהלים


        public class Config
        {
            public static double available = 0.25;
            public static double medium = 0.75;
            public static double light = 0.5;
            public static double heavy = 1;
            public static double speed =5;
            public static int number = 20;
        }
        internal static void Initialize()
        {

            DO.Station s1 = new Station() { ID = 1234, Name = "Avi", Longitude = 33, Latitude = 42, ChargeSlots = 3 };
            listS.Add(s1);
            s1 = new Station() { ID = 2345, Name = "Beni", Longitude = 35, Latitude = 50, ChargeSlots = 17 };
            listS.Add(s1);

            // ינוסיף מנהלים למערכת יבכה הוסיפה
            Manager m = new Manager()
            { ID = 4622, password = 4622, };
            listManager.Add(m);
            m = new Manager()
            { ID = 9714, password = 9714, };
            listManager.Add(m);

            //בין 1111-5555
            Drone d = new Drone()
            { ID = 1111, Model = "A", MaxWeight = WeightCategories.heavy/*, Status = DroneStatuses.Shipping, Battery = 97*/ };
            listD.Add(d);
            d = new Drone()
            { ID = 2222, Model = "B", MaxWeight = WeightCategories.Light/*, Status = DroneStatuses.Shipping, Battery = 97*/ };
            listD.Add(d);
            d = new Drone()
            { ID = 3333, Model = "C", MaxWeight = WeightCategories.heavy/*, Status = DroneStatuses.Shipping, Battery = 97*/ };
            listD.Add(d);
            d = new Drone()
            { ID = 4444, Model = "D", MaxWeight = WeightCategories.heavy/*, Status = DroneStatuses.Shipping, Battery = 97*/ };
            listD.Add(d);
            d = new Drone()
            { ID = 5555, Model = "E", MaxWeight = WeightCategories.heavy/*, Status = DroneStatuses.Shipping, Battery = 97*/ };
            listD.Add(d);


            //8888 - 1616
            Customer c = new Customer() { ID = 7777, Name = "a", Phone = "6543", Longitude = 43, Latitude = 34 };

            Customer e = new Customer() { ID = 8888, Name = "b", Phone = "3232", Longitude = 32, Latitude = 45 };
            listC.Add(e);
            c = new Customer() { ID = 9999, Name = "c", Phone = "4554", Longitude = 34, Latitude = 44 };
            listC.Add(c);
            c = new Customer() { ID = 1111, Name = "d", Phone = "6565", Longitude = 34, Latitude = 49 };
            listC.Add(c);
            c = new Customer() { ID = 5555, Name = "e", Phone = "7667", Longitude = 38, Latitude = 50 };
            listC.Add(c);
            c = new Customer() { ID = 1212, Name = "f", Phone = "8787", Longitude = 37, Latitude = 37 };
            listC.Add(c);
            c = new Customer() { ID = 1313, Name = "g", Phone = "3344", Longitude = 47, Latitude = 46 };
            listC.Add(c);
            c = new Customer() { ID = 1414, Name = "h", Phone = "8778", Longitude = 48, Latitude = 44 };
            listC.Add(c);
            c = new Customer() { ID = 1515, Name = "i", Phone = "2332", Longitude = 34, Latitude = 34 };
            listC.Add(c);
            c = new Customer() { ID = 1616, Name = "k", Phone = "6523", Longitude = 43, Latitude = 33 };
            listC.Add(c);

            Parcel p = new Parcel()
            {
                ID = Config.number++,
                SenderID = 1111,
                TargetID = 1212,
                Weight = WeightCategories.heavy,
                Priority = Priorities.Fast,
                created = new DateTime(2021, 1, 1),
                DroneID = 1111,
                Scheduled = new DateTime(2021, 1, 3),
                PickedUp = new DateTime(2021, 1, 6),
                Delivered = null
            };
            listP.Add(p);
            p = new Parcel()
            {
                ID = Config.number++,
                SenderID = 1515,
                TargetID = 8888,
                Weight = WeightCategories.Light,
                Priority = Priorities.Fast,
                created = new DateTime(2021, 2, 1),
                DroneID = 2222,
                Scheduled = new DateTime(2021, 2, 3),
                PickedUp = null,
                Delivered = null
            };
            listP.Add(p);
            p = new Parcel()
            {
                ID = Config.number++,
                SenderID = 1313,
                TargetID = 9999,
                Weight = WeightCategories.heavy,
                Priority = Priorities.Fast,
                created = new DateTime(2021, 3, 1),
                DroneID = 3333,
                Scheduled = new DateTime(2021, 3, 3),
                PickedUp = new DateTime(2021, 3, 6),
                Delivered = new DateTime(2021, 3, 9)
            };
            listP.Add(p);
            p = new Parcel()
            {
                ID = Config.number++,
                SenderID = 1313,
                TargetID = 1414,
                Weight = WeightCategories.heavy,
                Priority = Priorities.Fast,
                created = new DateTime(2021, 4, 1),
                DroneID = 1111,
                Scheduled = new DateTime(2021, 4, 3),
                PickedUp = new DateTime(2021, 4, 6),
                Delivered = new DateTime(2021, 4, 9)
            };
            listP.Add(p);
            p = new Parcel()
            {
                ID = Config.number++,
                SenderID = 1414,
                TargetID = 8888,
                Weight = WeightCategories.heavy,
                Priority = Priorities.Fast,
                created = new DateTime(2021, 5, 1),
                DroneID = 5555,
                Scheduled = new DateTime(2021, 5, 3),
                PickedUp = new DateTime(2021, 5, 6),
                Delivered = new DateTime(2021, 5, 9)
            };
            listP.Add(p);
            p = new Parcel()
            {
                ID = Config.number++,
                SenderID = 1515,
                TargetID = 1616,
                Weight = WeightCategories.heavy,
                Priority = Priorities.Fast,
                created = new DateTime(2021, 6, 1),
                DroneID = 2222,
                Scheduled = new DateTime(2021, 6, 3),
                PickedUp = new DateTime(2021, 6, 6),
                Delivered = new DateTime(2021, 6, 7)
            };
            listP.Add(p);
            p = new Parcel()
            {
                ID = Config.number++,
                SenderID = 8888,
                TargetID = 1313,
                Weight = WeightCategories.heavy,
                Priority = Priorities.Fast,
                created = new DateTime(2021, 7, 1),
                DroneID = 0,
                Scheduled = null,
                PickedUp = null,
                Delivered = null
            };
            listP.Add(p);
            p = new Parcel()
            {
                ID = Config.number++,
                SenderID = 9999,
                TargetID = 1414,
                Weight = WeightCategories.heavy,
                Priority = Priorities.Fast,
                created = new DateTime(2021, 8, 1),
                DroneID = 0,
                Scheduled = null,
                PickedUp = null,
                Delivered = null
            };
            listP.Add(p);
            p = new Parcel()
            {
                ID = Config.number++,
                SenderID = 1212,
                TargetID = 9999,
                Weight = WeightCategories.heavy,
                Priority = Priorities.Fast,
                created = new DateTime(2021, 8, 1),
                DroneID = 2222,
                Scheduled = new DateTime(2021, 8, 3), //new DateTime(2021, 8, 3),
                PickedUp = new DateTime(2021, 8, 6),
                Delivered = new DateTime(2021, 8, 8)
            };
        }


    }
}



