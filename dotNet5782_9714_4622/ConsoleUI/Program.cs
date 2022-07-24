using System;
using DalApi;
using DO;


namespace ConsoleUI
{
    enum options
    {
        aS, aD, aC, aP, ush, uisuf, uaspka, ushliha, ufree,
        vst, vD, vC, vP, vast, vad, vac, vap, vastn, vastb, exit
    };
    class Program
    {

        static void Main(string[] args)
        {

            IDal dalo = DalFactory.GetDal();
            bool flag = true;
            while (flag)
            {
                Console.WriteLine
                (@"Insert options:
0 = Adding a base station to the list of stations
1 = Add a skimmer to the list of existing skimmers
2 = Absorption of a new customer to the customer list
3 = Receipt of package for shipment.
Update options:
4 = Assigning a package to a glider
5 = Collection of a package by skimmer
6 = Delivery of package to customer
7 = Sending a skimmer for charging at a base station
8 = Release skimmer from charging at base station
Display options:
9 = Base station display
10 = skimmer display
11 = Customer view
12 = package display
List view options
13 = Displays a list of base stations
14 = Displays the skimmer list
15 = Displays the customer list
16 = Displays the list of packages
17 = Displays a list of packages that have not yet been assigned to the glider
18 = Display of base stations with available charging stations
19 = Exit
");
                options o;
                int id;
                string name;
                double longitude;
                double latitude;
                int chargeslots;
                string sd = Console.ReadLine();
                o = (options)int.Parse(sd);
                switch (o)
                {

                    case options.aS:
                        try
                        {
                            Console.WriteLine("please enter ID, Name, Longitude, Latitude,chargeslots");
                            id = int.Parse(Console.ReadLine());
                            name = Console.ReadLine();
                            longitude = double.Parse(Console.ReadLine());
                            latitude = double.Parse(Console.ReadLine());
                            chargeslots = int.Parse(Console.ReadLine());
                            Station s = new() { ID = id, Name = name, Longitude = longitude, Latitude = latitude, ChargeSlots = chargeslots };
                            //dalo.addStaton(s);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case options.aD:
                        Console.WriteLine("please enter ID, model, maxweight, status, battery");
                        id = int.Parse(Console.ReadLine());
                        string model = Console.ReadLine();

                        WeightCategories maxweight;
                        string sf = Console.ReadLine();
                        maxweight = (WeightCategories)int.Parse(sf);


                        double battery = double.Parse(Console.ReadLine());
                        Drone d = new() { ID = id, Model = model, MaxWeight = maxweight };
                        dalo.addDrone(d);
                        break;
                    case options.aC:
                        Console.WriteLine("please enter ID, Name, phone, Longitude, Latitude");
                        id = int.Parse(Console.ReadLine());
                        name = Console.ReadLine();
                        string phone = Console.ReadLine();
                        longitude = double.Parse(Console.ReadLine());
                        latitude = double.Parse(Console.ReadLine());
                        Customer c = new() { ID = id, Name = name, Phone = phone, Longitude = longitude, Latitude = latitude };
                        dalo.addCustomer(c);
                        break;
                    case options.aP:
                        Console.WriteLine("please enter ID, senderID, targetID, weight, priority");
                        id = int.Parse(Console.ReadLine());
                        int senderID = int.Parse(Console.ReadLine());
                        int targetID = int.Parse(Console.ReadLine());
                        sf = Console.ReadLine();
                        maxweight = (WeightCategories)int.Parse(sf);
                        Priorities priority;
                        sf = Console.ReadLine();
                        priority = (Priorities)int.Parse(sf);
                        Parcel p = new() { ID = id, SenderID = senderID, TargetID = targetID, Weight = maxweight, Priority = priority };
                        p.created = DateTime.Now;
                        dalo.addParcel(p);
                        break;
                    case options.ush:
                        Console.WriteLine("please enter a drone id and a parcel id");
                        int did = int.Parse(Console.ReadLine());
                        int pid = int.Parse(Console.ReadLine());
                        dalo.connectPrcelToDrone(did, pid);
                        break;
                    case options.uisuf:
                        Console.WriteLine("please enter a drone id and a parcel id");
                        did = int.Parse(Console.ReadLine());
                        pid = int.Parse(Console.ReadLine());
                        dalo.collectParcelByDrone(pid);//check
                        break;
                    case options.uaspka:
                        Console.WriteLine("please enter a drone id and a parcel id");
                        did = int.Parse(Console.ReadLine());
                        pid = int.Parse(Console.ReadLine());
                        dalo.DeliverTheParcel(pid);//check
                        break;
                    case options.ushliha:
                        Console.WriteLine("please enter a drone id and a station id");
                        did = int.Parse(Console.ReadLine());
                        pid = int.Parse(Console.ReadLine());
                        dalo.sendTheDroneToCharge(did, pid);
                        break;
                    case options.ufree:
                        Console.WriteLine("please enter a drone id and a station id");
                        did = int.Parse(Console.ReadLine());
                        pid = int.Parse(Console.ReadLine());
                        dalo.freeDroneFromCharge(pid);//check
                        break;
                    case options.vst:
                        Console.WriteLine("please enter a station id");
                        pid = int.Parse(Console.ReadLine());
                        dalo.getStation(pid);
                        break;
                    case options.vD:
                        Console.WriteLine("please enter a drone id");
                        pid = int.Parse(Console.ReadLine());
                        dalo.getDrone(pid);
                        break;
                    case options.vC:
                        Console.WriteLine("please enter a customer id");
                        pid = int.Parse(Console.ReadLine());
                        dalo.GetCustomer(pid);
                        break;
                    case options.vP:
                        Console.WriteLine("please enter a parcel id");
                        pid = int.Parse(Console.ReadLine());
                        dalo.getParcel(pid);
                        break;
                    case options.vast:
                        foreach (Station item in dalo.viewListOfStations())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case options.vad:
                        foreach (Drone item in dalo.viewListOfDrones())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case options.vac:
                        foreach (Customer item in dalo.viewListOfCustomers())
                        {
                            Console.WriteLine(item);
                        };
                        break;
                    case options.vap:
                        foreach (Parcel item in dalo.viewListOfParcels())
                        {
                            Console.WriteLine(item);
                        };
                        break;
                    case options.vastn:
                        dalo.viewListOfFreeParcels();
                        break;
                    case options.vastb:
                        dalo.viewListOfAvailableStations();
                        break;
                    case options.exit:
                        Console.WriteLine("bye bye");
                        flag = false;
                        break;
                    default:
                        break;

                }

            }
        }
    }
}



























