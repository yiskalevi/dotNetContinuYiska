using System;
using BO;
using BLApi;
using DalApi;
using Dal;
using DO;


using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class BL_ConsoleUI
    {
        static void Main(string[] args)
        {

            BL.BL b = new BL.BL();
            //Console.WriteLine("please enter ID, Name, Longitude, Latitude,chargeslots");
            //int id = int.Parse(Console.ReadLine());
            //string name = Console.ReadLine();
            //double longitude = double.Parse(Console.ReadLine());
            //double latitude = double.Parse(Console.ReadLine());
            //int chargeslots = int.Parse(Console.ReadLine());
            //location h = new() { longitude = longitude, latitude = latitude };
            //station s = new() { ID = id, name = name, stationLocation = h, numberOfStands = chargeslots };
            //b.addStation(s);
            //b.viewListOfStations();
            Console.WriteLine("please enter ID, Name, phone, Longitude, Latitude");

            int id = int.Parse(Console.ReadLine());
            string name = Console.ReadLine();
            string phone = Console.ReadLine();
            double longitude = double.Parse(Console.ReadLine());
            double latitude = double.Parse(Console.ReadLine());
            location h1 = new() { longitude = longitude, latitude = latitude };
            customer c = new() { ID = id, name = name, phone = phone, locationC = h1 };
            b.addCustomer(c);
            IEnumerable<customerToList> dr;
            dr = b.viewListOfCustomer();
            foreach (BO.customerToList item in dr)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("please enter customer number, name");
            int pid = int.Parse(Console.ReadLine());
            string d = Console.ReadLine();
            b.updateCustomerName(pid, d);
        }
    }
}
//            //foreach(droneToList item in b.viewListOfDrone())
//            //            {
//            //    Console.WriteLine(item);
//            //};
//            //foreach (parcelToList item in b.viewListOfParcel())
//            //{
//            //    Console.WriteLine(item);
//            //};
//            //foreach (customerToList item in b.viewListOfCustomer())
//            //{
//            //    Console.WriteLine(item);
//            //};
//            //foreach (stationToList item in b.viewListOfStations())
//            //{
//            //    Console.WriteLine(item);
//            //};
//            options1 o;
//            int id;
//            string name;
//            string sf;
//            int num;
//            double longitude;
//            double latitude;
//            int chargeslots;
//            bool temp = true;
//            string model;
//            enums.WeightCategories maxweight;
//            while (temp)
//            {
//                Console.WriteLine
//        (@"Insert options:
//                        0 = Adding
//                        1 =updating
//                        2 =View an object
//                        3 = View a list
//                        5 = Exit
//                        ");
//                string sd = Console.ReadLine();
//                o = (options1)int.Parse(sd);
//                switch (o)
//                {
//                    case options1.add:
//                        Console.WriteLine
//(@"Insert options:
//                        0 = station
//                        1 = drone
//                        2 = customer
//                        3 = parcel
//                        4 = back
//                        ");
//                        string a = Console.ReadLine();
//                        Add add;
//                        add = (Add)int.Parse(a);
//                        switch (add)
//                        {

//                            case Add.station:
//                                try
//                                {
//                                    Console.WriteLine("please enter ID, Name, Longitude, Latitude,chargeslots");
//                                    id = int.Parse(Console.ReadLine());
//                                    name = Console.ReadLine();
//                                    longitude = double.Parse(Console.ReadLine());
//                                    latitude = double.Parse(Console.ReadLine());
//                                    chargeslots = int.Parse(Console.ReadLine());
//                                    location h = new() { longitude = longitude, latitude = latitude };
//                                    station s = new() { ID = id, name = name, stationLocation = h, numberOfStands = chargeslots };
//                                    b.addStation(s);
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Add.drone:
//                                try
//                                {
//                                    Console.WriteLine("please enter ID, model, maxweight and a station number to put the drone in");
//                                    id = int.Parse(Console.ReadLine());
//                                    model = Console.ReadLine();
//                                    int e = int.Parse(Console.ReadLine());
//                                    maxweight = (enums.WeightCategories)e;
//                                    int idS = int.Parse(Console.ReadLine());
//                                    drone d = new() { ID = id, model = model, weight = maxweight };
//                                    d.battery = 0;
//                                    d.droneLocation = null;
//                                    d.statusOfDrone = 0;
//                                    b.addDrone(d, idS);
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Add.customer:
//                                try
//                                {
//                                    Console.WriteLine("please enter ID, Name, phone, Longitude, Latitude");
//                                    id = int.Parse(Console.ReadLine());
//                                    name = Console.ReadLine();
//                                    string phone = Console.ReadLine();
//                                    longitude = double.Parse(Console.ReadLine());
//                                    latitude = double.Parse(Console.ReadLine());
//                                    location h1 = new() { longitude = longitude, latitude = latitude };
//                                    customer c = new() { ID = id, name = name, phone = phone, locationC = h1 };
//                                    b.addCustomer(c);
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Add.parcel:
//                                try
//                                {
//                                    //מה אם הID של החבילה
//                                    Console.WriteLine("please enter ID, senderID, targetID, weight, priority,drone ID");
//                                    id = int.Parse(Console.ReadLine());
//                                    int senderID = int.Parse(Console.ReadLine());
//                                    int targetID = int.Parse(Console.ReadLine());
//                                    sf = Console.ReadLine();
//                                    maxweight = (enums.WeightCategories)int.Parse(sf);
//                                    enums.Priorities priority;
//                                    sf = Console.ReadLine();
//                                    priority = (enums.Priorities)int.Parse(sf);
//                                    //יש מצב שצריך לשנות איך שהוא לבנות בתוך ה add
//                                    customer cs = new customer();
//                                    cs = b.getCustomer(senderID);
//                                    customerInP se = new() { ID = cs.ID, name = cs.name };
//                                    cs = b.getCustomer(targetID);
//                                    customerInP t = new() { ID = cs.ID, name = cs.name };
//                                    int droneID = int.Parse(Console.ReadLine());

//                                    parcel p = new() { ID = id, sender = se, recipient = t, weight = maxweight, priority = priority };
//                                    drone drlp = new drone();
//                                    drlp = b.getDrone(droneID);
//                                    droneInParcel drp = new() { ID = drlp.ID, battery = drlp.battery, droneLocation = drlp.droneLocation };
//                                    p.droneInPar = drp;
//                                    b.addParcel(p);
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Add.back:
//                                break;
//                        }
//                        break;
//                    case options1.update:
//                        Console.WriteLine
//(@"Insert options:
//                                Choose what you want to update
//                                1 = drone
//                                2 = station
//                                3 = customer
//                                4 = Sending a skimmer for charging
//                                5 = Release skimmer from charging
//                                6 = Assigning a package to a glider
//                                7 = Collection of a package by drone
//                                8 = Package satisfaction by drone
//                                9 = back
//                                ");
//                        string u = Console.ReadLine();
//                        Update up;
//                        up = (Update)int.Parse(u);
//                        switch (up)
//                        {
//                            case Update.uDrone:
//                                try
//                                {
//                                    Console.WriteLine("please enter a drone id and model");
//                                    num = int.Parse(Console.ReadLine());
//                                    string pid = Console.ReadLine();
//                                    b.updateTheDroneName(num, pid);
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Update.uStation:
//                                try
//                                {
//                                    Console.WriteLine("What to update: 1 = station name 2 = total amount of charging stations or years = 3");
//                                    int p = int.Parse(Console.ReadLine());
//                                    if (p == 1)
//                                    {
//                                        Console.WriteLine("please enter Station number Station name");
//                                        string d = Console.ReadLine();
//                                        int pid = int.Parse(Console.ReadLine());

//                                        b.updateTheStationName(pid, d);
//                                    }
//                                    if (p == 2)
//                                    {
//                                        Console.WriteLine("please enter total amount of charging stations.");
//                                        int pid = int.Parse(Console.ReadLine());
//                                        int d = int.Parse(Console.ReadLine());
//                                        b.updateNumberOfStands(pid, d);
//                                    }
//                                    if (p == 3)
//                                    {
//                                        Console.WriteLine("please enter Station number Station name, total amount of charging stations.");
//                                        int pid = int.Parse(Console.ReadLine());
//                                        int d = int.Parse(Console.ReadLine());
//                                        string dp = Console.ReadLine();
//                                        b.updateNameAndNumOfStands(pid, d, dp);
//                                    }
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Update.uCustomer:
//                                try
//                                {
//                                    Console.WriteLine("What to update: 1 = name 2 = phone number or both = 3");
//                                    int p = int.Parse(Console.ReadLine());
//                                    if (p == 1)
//                                    {
//                                        Console.WriteLine("please enter customer number, name");
//                                        int pid = int.Parse(Console.ReadLine());
//                                        string d = Console.ReadLine();
//                                        b.updateCustomerName(pid, d);
//                                    }
//                                    if (p == 2)
//                                    {
//                                        Console.WriteLine("please enter customer number fone.");
//                                        int pid = int.Parse(Console.ReadLine());
//                                        string d = Console.ReadLine();
//                                        //b.updateCustomerPhone(pid, d);
//                                    }
//                                    if (p == 3)
//                                    {
//                                        Console.WriteLine("please enter customer number, name, fone.");
//                                        int pid = int.Parse(Console.ReadLine());
//                                        string d = Console.ReadLine();
//                                        string dp = Console.ReadLine();
//                                        //b.uCustomer(pid, d, dp);
//                                    }
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }

//                                break;
//                            case Update.sendDroneToCharge:
//                                try
//                                {
//                                    Console.WriteLine("please enter a station id");
//                                    num = int.Parse(Console.ReadLine());
//                                    b.sendDroneToCharge(num);
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Update.freeDroneFromCharge:
//                                try
//                                {
//                                    Console.WriteLine("please enter a station id and time");
//                                    num = int.Parse(Console.ReadLine());
//                                    double x = double.Parse(Console.ReadLine());
//                                   // b.freeDroneFromCharge(num, x);
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Update.conectParcelToDrone:
//                                try
//                                {
//                                    Console.WriteLine("please enter a drone id");
//                                    num = int.Parse(Console.ReadLine());
//                                    b.conectParcelToDrone(num);
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Update.collectParcelByDrone:
//                                try
//                                {
//                                    Console.WriteLine("please enter a drone id");
//                                    num = int.Parse(Console.ReadLine());
//                                    b.collectParcelByDrone(num);
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Update.provideParcelByDrone:
//                                try
//                                {
//                                    Console.WriteLine("please enter a drone id");
//                                    num = int.Parse(Console.ReadLine());
//                                    b.provideParcelByDrone(num);
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case Update.back:
//                                break;
//                        }
//                        break;
//                    case options1.objectView:
//                        Console.WriteLine(@"Choose which object to display:
//                                    0 = station
//                                    1 = drone
//                                    2 = customer
//                                    3 = parcel
//                                    4 = back
//                                    ");
//                        string v = Console.ReadLine();
//                        ObjectView ob;
//                        ob = (ObjectView)int.Parse(v);
//                        switch (ob)
//                        {
//                            case ObjectView.existS:
//                                try
//                                {
//                                    Console.WriteLine("please enter a station id");
//                                    num = int.Parse(Console.ReadLine());
//                                    Console.WriteLine(b.getStation(num).ToString());
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case ObjectView.existD:
//                                try
//                                {
//                                    Console.WriteLine("please enter a drone id");
//                                    num = int.Parse(Console.ReadLine());
//                                    Console.WriteLine(b.getDrone(num).ToString());
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case ObjectView.existC:
//                                try
//                                {
//                                    Console.WriteLine("please enter a customer id");
//                                    num = int.Parse(Console.ReadLine());
//                                    Console.WriteLine(b.getCustomer(num).ToString());
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case ObjectView.existP:
//                                try
//                                {
//                                    Console.WriteLine("please enter a parcel id");
//                                    num = int.Parse(Console.ReadLine());
//                                    Console.WriteLine(b.getParcel(num).ToString());
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case ObjectView.back:
//                                break;
//                        }
//                        break;
//                    case options1.listView:
//                        Console.WriteLine(@"Choose which object to display:
//                        0 = station
//                        1 = drone
//                        2 = customer
//                        3 = parcel
//                        4 = packages not yet discussed
//                        5 = base stations with available charging stations
//                        6 = back");
//                        string l = Console.ReadLine();
//                        ListView li;
//                        li = (ListView)int.Parse(l);
//                        switch (li)
//                        {
//                            case ListView.viewS:
//                                try
//                                {
//                                    IEnumerable<stationToList> dr;
//                                    dr = b.viewListOfStations();
//                                    //  var  = dr.ToList();
//                                    foreach (BO.stationToList item in dr)
//                                    {
//                                        Console.WriteLine(item);
//                                    }
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case ListView.viewD:
//                                try
//                                {
//                                    IEnumerable<droneToList> dr;
//                                    dr = b.viewListOfDrone();
//                                    foreach (BO.droneToList item in dr)
//                                    {
//                                        Console.WriteLine(item);
//                                        Console.WriteLine('\n');
//                                    }
//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case ListView.viewC:
//                                try
//                                {
//                                    IEnumerable<customerToList> dr;
//                                    dr = b.viewListOfCustomer();
//                                    foreach (BO.customerToList item in dr)
//                                    {
//                                        Console.WriteLine(item.ToString());
//                                    }

//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case ListView.viewP:
//                                try
//                                {
//                                    IEnumerable<parcelToList> dr;
//                                    dr = b.viewListOfParcel();
//                                    foreach (BO.parcelToList item in dr)
//                                    {
//                                        Console.WriteLine(item.ToString());
//                                    }

//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;

//                            case ListView.viewFP:
//                                try
//                                {
//                                    IEnumerable<parcelToList> dr;
//                                    //dr = b.viewFP();
//                                    //foreach (BO.parcelToList item in dr)
//                                    //{
//                                    //    Console.WriteLine(item.ToString());
//                                    //}

//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case ListView.viewSB:
//                                try
//                                {
//                                    IEnumerable<stationToList> dr;
//                                    //dr = b.viewSB();
//                                    //foreach (BO.stationToList item in dr)
//                                    //{
//                                    //    Console.WriteLine(item.ToString());
//                                    //}

//                                }
//                                catch (Exception ex)
//                                {
//                                    Console.WriteLine(ex.Message);
//                                }
//                                break;
//                            case ListView.back:
//                                break;
//                        }
//                        break;
//                    case options1.exit:
//                        temp = false;
//                        break;
//                }
//            }
//        }
//    }



//}


//            //            //foreach (stationToList item in b.viewListOfStations())
//            //            //{
//            //            //    Console.WriteLine(item);
//            //            //};
//            //            //Console.WriteLine("please enter ID, Name, Longitude, Latitude,chargeslots");
//            //            //int id = int.Parse(Console.ReadLine());
//            //            //string name = Console.ReadLine();
//            //            //double longitude = double.Parse(Console.ReadLine());
//            //            //double latitude = double.Parse(Console.ReadLine());
//            //            //int chargeslots = int.Parse(Console.ReadLine());
//            //            //location h = new() { longitude = longitude, latitude = latitude };
//            //            //station s = new() { ID = id, name = name, stationLocation = h, numberOfStands = chargeslots };
//            //            //b.addStation(s);

//            //            //Console.WriteLine(b.getStation(id));
//            //            //foreach (customerToList item in b.viewListOfCustomer())
//            //            //{
//            //            //    Console.WriteLine(item);
//            //            //};
//            //            //Console.WriteLine("please enter ID, Name, phone, Longitude, Latitude");
//            //            //int id = int.Parse(Console.ReadLine());
//            //            //string name = Console.ReadLine();
//            //            //string phone = Console.ReadLine();
//            //            //double longitude = double.Parse(Console.ReadLine());
//            //            //double latitude = double.Parse(Console.ReadLine());       
//            //            //location h1 = new() { longitude = longitude, latitude = latitude };
//            //            //customer c = new() { ID = id, name = name, phone = phone, locationC = h1 };
//            //            //b.addCustomer(c);
//            //            //Console.WriteLine(b.getCustomer(id));
//            //            //foreach (customerToList item in b.viewListOfCustomer())
//            //            //{
//            //            //    Console.WriteLine(item);
//            //            //};
//            //            //Console.WriteLine("please enter ID, senderID, targetID, weight, priority,drone ID");
//            //            //int id = int.Parse(Console.ReadLine());
//            //            //int senderID = int.Parse(Console.ReadLine());
//            //            //int targetID = int.Parse(Console.ReadLine());
//            //            //sf = Console.ReadLine();
//            //            //maxweight = (enums.WeightCategories)int.Parse(sf);
//            //            //enums.Priorities priority;
//            //            //sf = Console.ReadLine();
//            //            //priority = (enums.Priorities)int.Parse(sf);
//            //            ////יש מצב שצריך לשנות איך שהוא לבנות בתוך ה add
//            //            //customer cs = new customer();
//            //            //cs = b.getCustomer(senderID);
//            //            //customerInP se = new() { ID = cs.ID, name = cs.name };
//            //            //cs = b.getCustomer(targetID);
//            //            //customerInP t = new() { ID = cs.ID, name = cs.name };
//            //            //int droneID = int.Parse(Console.ReadLine());

//            //            //parcel p = new() { ID = id, sender = se, recipient = t, weight = maxweight, priority = priority };
//            //            //drone drlp = new drone();
//            //            //drlp = b.getDrone(droneID);
//            //            //droneInParcel drp = new() { ID = drlp.ID, battery = drlp.battery, droneLocation = drlp.droneLocation };
//            //            //p.droneInPar = drp;
//            //            //b.addParcel(p);
//            //            //Console.WriteLine(b.getCustomer(id));
//            //            //foreach (droneToList item in b.viewListOfDrone())
//            //            //{
//            //            //    Console.WriteLine(item);
//            //            //};





