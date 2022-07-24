using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class stationToList
        {
            public int ID { get; set; }
            public string name { get; set; }
		    public int numberOfAvailableStands { get; set; }
		   public int numberOfOccupiedStands { get; set; }
		   public override string ToString()
			{
				return "Id: " + ID + " name: " + name + " numberOfAvailableStands: " + numberOfAvailableStands
					+ " numberOfOccupiedStands: " + numberOfOccupiedStands;
			}
		}
    }

/*
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;


namespace IBL
{ 
    public interface IBl
    {
		//#region add
		void addS(station s);
		void addD(drone d);
		void addC(customer c);
		void addP(parcel p);
		//#endregion
		
		//#region update
		void uDrone(int id, string model);//update the drone
		void uStation(int id, string names);//getting only the station name
		void uStation(int id, int numOfStands); //getting only the number of sations
		void uStation(int id, int numOfStands, string names);//getting both 
		void uCustomer(int id, string name);//getting only the name
		void uCustomer(int d, string phone, int x); //getting only the phone
		void uCustomer(int d, string phone,string name); //getting both 
		void sendDroneToCharge(int id);
		void freeDroneFromCharge(int id, double timeToCharge);
		void conectParcelToDrone(int id);
		void collectParcelByDrone(int id);
		void provideParcelByDrone(int id);
		//#endregion

		//#region exist
		station existS(int n);
		drone existD(int n);
		customer existC(int n);
		parcel existP(int n);
		//#endregion

		//#region view
		IEnumerable<station> viewS();
		IEnumerable<drone> viewD();
		IEnumerable<customer> viewC();
		IEnumerable<parcel> viewP();
		IEnumerable<parcel> viewFP();
		IEnumerable<station> viewSB();
		//#endregion
	}
}

*/