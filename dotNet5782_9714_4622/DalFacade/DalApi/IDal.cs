using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
	public interface IDal
	{
		//------------------------------------------------------electric--------------------------------------------------

		/// <summary>
		/// drone charging time, and how much battery it wastes
		/// </summary>
		/// <returns> How much is charged per minute / how much battery he spends per minute depending on the situation.</returns>
		double[] felectric();


		// ------------------------------ All the functions Add -------------------------
		/// <summary>
		/// Function that add a station to the system
		/// </summary>
		/// <param name="s"> is the station that we add</param> 
		void addStation(Station s);
		/// <summary>
		/// Function that add a drone to the system
		/// </summary>
		/// <param name="d"> is the drone that we add</param> 
		void addDrone(Drone d);
		/// <summary>
		/// Function that add a customer to the system
		/// </summary>
		/// <param name="c"> is the customer that we add</param> 
		void addCustomer(Customer c);
		/// <summary>
		/// Function that add a parcel to the system
		/// </summary>
		/// <param name="p"> is the parcel that we add</param> 
		int addParcel(Parcel p);
		/// <summary>
		/// Function that add a drone In Charge to the system
		/// </summary>
		/// <param name="id"> is the drone In Charge that we add</param> 
		void addDroneInCharge(DroneCharge id);

		//--------------------- All the functions of update -------------------
		/// <summary>
		/// Function that updates the name of the drone in the system
		/// </summary>
		/// <param name="id"> id of drone</param>
		/// <param name="name">The new name</param>
		void updateTheDroneName(int id, string name);
		/// <summary>
		/// Function that updates the name of the station in the system
		/// </summary>
		/// <param name="id"> id of station</param>
		/// <param name="names">The new name</param>
		void updateTheStationName(int id, string names);
		/// <summary>
		/// Function that updates the number of stands of the station in the system
		/// </summary>
		/// <param name="id">id of station</param>
		/// <param name="d">new number of stands</param>
		void updateNumberOfStands(int id, int d);
		/// <summary>
		///  Function that updates the number of stands and name in the station in the system
		/// </summary>
		/// <param name="id">id of station</param>
		/// <param name="numOfStands">new number of stands</param>
		/// <param name="names">The new name</param>
		void updateNameAndNumOfStands(int id, int numOfStands, string names);
		/// <summary>
		///  Function that updates the name of the customer in the system
		/// </summary>
		/// <param name="id">id of customer</param>
		/// <param name="name"></param>
		void updateCustomerName(int id, string name);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="phone"></param>
		/// <param name="x"></param>
		void updateCustomerPhone(int id, string phone, int x); //getting only the phone
		void updateCustomerNameAndPhone(int id, string phone, string name); //getting both 
		void connectPrcelToDrone(int d, int p);
		void collectParcelByDrone(int p);
		void DeliverTheParcel(int p);
		void sendTheDroneToCharge(int d, int s);
		void freeDroneFromCharge(int d);
		bool checkManager(int i, int p);

		//------------------------------------------------------get--------------------------------------------------
		Station getStation(int n);
		Drone getDrone(int n);
		Customer GetCustomer(int n);
		Parcel getParcel(int n);
		DroneCharge GetDroneCharge(int n);
		Parcel getParcelBelongsDrone(int droneID);

		
		//------------------------------------------------------view list--------------------------------------------------
		IEnumerable<Station> viewListOfStations();
		IEnumerable<Drone> viewListOfDrones();
		IEnumerable<Customer> viewListOfCustomers();
		IEnumerable<Parcel> viewListOfParcels();
		IEnumerable<Parcel> viewListOfFreeParcels();
		IEnumerable<Station> viewListOfAvailableStations();
		/// <summary>
		/// Returns a list of drone loaded with the station number to be received
		/// </summary>
		/// <param name="stationI"> id station</param>
		/// <returns></returns>
		IEnumerable<DroneCharge> ListOfDroneInCharge1(int stationI);
		IEnumerable<DroneCharge> ListOfDroneInCharge();
		//IEnumerable<Manager> ListOfManager();
		int closeStationToCharge(double droneLongitude, double droneLatitude, ref double minimLocation);
		int closeStation(double longtit, double latit);


	}
}