using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BO;


namespace BLApi
{
	public interface IBl
	{
		#region add
		/// <summary>
		/// function to add a station to the system
		/// </summary>
		/// <param name="stationToAdd">the station we want to add with all his params</param>
		void addStation(station stationToAdd);

		/// <summary>
		/// function to add a drone to the system
		/// </summary>
		/// <param name="droneToAdd">the drone we want to add with all his params</param>
		void addDrone(drone droneToAdd, int s);

		/// <summary>
		/// function to add a customer to the system
		/// </summary>
		/// <param name="customerToAdd">the customer we want to add with all his params</param>
		void addCustomer(customer customerToAdd);

		/// <summary>
		/// function to add a parcel to the system
		/// </summary>
		/// <param name="parcelToAdd">the parcel we want to add with all his params</param>
		int addParcel(parcel parcelToAdd);

		#endregion

		#region update
		/// <summary>
		/// update the drone name
		/// </summary>
		/// <param name="id">id of drone</param>
		/// <param name="model">update model</param>
		void updateTheDroneName(int id, string model);

		/// <summary>
		/// update number of station name 
		/// </summary>
		/// <param name="id">id of station</param>
		/// <param name="names">update name</param>
		void updateTheStationName(int id, string names);//getting only the station name

		/// <summary>
		/// update number of stands in the station 
		/// </summary>
		/// <param name="id">id of station</param>
		/// <param name="numOfStands">new numOfStands</param>
		void updateNumberOfStands(int id, int numOfStands); //getting only the number of sations

		/// <summary>
		/// updare the station name and number of stands
		/// </summary>
		/// <param name="id">id of station</param>
		/// <param name="numOfStands">new numOfStands</param>
		/// <param name="names">new name</param>
		void updateNameAndNumOfStands(int id, int numOfStands, string names);//getting both 

		/// <summary>
		/// update the customer name
		/// </summary>
		/// <param name="id">id of customer</param>
		/// <param name="name">new name</param>
		void updateCustomerName(int id, string name);

		/// <summary>
		/// update the customer name
		/// </summary>
		/// <param name="d">id of customer</param>
		/// <param name="phone">new phone</param>
		/// <param name="x">help</param>
		void updateCustomerPhone(int d, string phone, int x); 

		/// <summary>
		/// update the customer name and phone
		/// </summary>
		/// <param name="d"> id of customer </param>
		/// <param name="phone"> new phone </param>
		/// <param name="name"> new name </param>
		void updateCustomerNameAndPhone(int d, string phone, string name);
		/// <summary>
		/// send Drone To Charge
		/// </summary>
		/// <param name="id"> id of drone</param>
		void sendDroneToCharge(int id);
		/// <summary>
		/// free Drone From Charge
		/// </summary>
		/// <param name="id">id of drone</param>
		void freeDroneFromCharge(int id);
		/// <summary>
		/// conect Parcel To Drone
		/// </summary>
		/// <param name="id">id of drone</param>
		void conectParcelToDrone(int id);
		/// <summary>
		/// collect Parcel By Drone
		/// </summary>
		/// <param name="id">id of drone</param>
		void collectParcelByDrone(int id);
		/// <summary>
		/// provide Parcel By Drone
		/// </summary>
		/// <param name="id">id of drone</param>
		void provideParcelByDrone(int id);
		#endregion

		#region exist
		/// <summary>
		/// return the station
		/// </summary>
		/// <param name="n">id of station</param>
		/// <returns></returns>
		station getStation(int n);
		/// <summary>
		/// return the StationToList
		/// </summary>
		/// <param name="n">id of station</param>
		/// <returns></returns>
		stationToList getStationToList(int n);
		/// <summary>
		/// return the drone
		/// </summary>
		/// <param name="n">id of drone</param>
		/// <returns></returns>
		drone getDrone(int n);
		/// <summary>
		///  return the customer
		/// </summary>
		/// <param name="n">id of customer</param>
		/// <returns></returns>
		customer getCustomer(int n);
		/// <summary>
		/// return the parcel
		/// </summary>
		/// <param name="n">id of parcel</param>
		/// <returns></returns>
		parcel getParcel(int n);
		#endregion

		#region view
		/// <summary>
		/// Returns a list of all stations
		/// </summary>
		/// <returns></returns>
		IEnumerable<stationToList> viewListOfStations();
		/// <summary>
		/// Returns a list of all drone
		/// </summary>
		/// <returns></returns>
		IEnumerable<droneToList> viewListOfDrone();
		/// <summary>
		/// Returns a list of all customer
		/// </summary>
		/// <returns></returns>
		IEnumerable<customerToList> viewListOfCustomer();
		/// <summary>
		/// Returns a list of all parcel
		/// </summary>
		/// <returns></returns>
		IEnumerable<parcelToList> viewListOfParcel();
		/// <summary>
		/// Returns a list of all Drone that meet the condition
		/// </summary>
		/// <param name="match"> the condition </param>
		/// <returns></returns>
		IEnumerable<droneToList> ListSortingDrone(Predicate<droneToList> match);
		/// <summary>
		/// Returns a list of all Drone that meet the Station
		/// </summary>
		/// <param name="predicate">the condition</param>
		/// <returns></returns>
		IEnumerable<stationToList> ListSortingStation(Func<stationToList, bool> predicate);
		/// <summary>
		/// Returns a list of all Drone that meet the parcel
		/// </summary>
		/// <param name="predicate">the condition</param>
		/// <returns></returns>
		IEnumerable<parcelToList> ListSortingParcel(Func<parcelToList, bool> predicate);
		/// <summary>
		/// Gets a list and returns a list of all Drone that meet the parcel
		/// </summary>
		/// <param name="match"> the condition</param>
		/// <param name="v">The given list</param>
		/// <returns></returns>
		List<parcelToList> ListSorting(Predicate<parcelToList> match,List<parcelToList> v);
		#endregion

		/// <summary>
		/// The function checks if the customer exists in the system
		/// </summary>
		/// <param name="n">the customer's name</param>
		/// <param name="i">the customer's id</param>
		/// <returns></returns>
		bool checkCustomer(string n, int i);
		/// <summary>
		/// The function checks if the administrator exists in the system
		/// </summary>
		/// <param name="i">the manager's id</param>
		/// <param name="p"></param>
		/// <returns></returns>
		bool checkManager(int i, int p);
		/// <summary>
		/// Returns the position of the drone in the list
		/// </summary>
		/// <param name="id">the drone id</param>
		/// <returns></returns>
		int indexDrone(int id);
		/// <summary>
		/// start Simolator
		/// </summary>
		/// <param name="droneId">the drone id</param>
		/// <param name="a"> Update function</param>
		/// <param name="stoop">Stop function</param>
		void startSimolator(int droneId, Action a, Func<bool> stoop);
		/// <summary>
		/// Conversion function
		/// </summary>
		/// <param name="customerToConvert"></param>
		/// <returns></returns>
		customerToList converToCListObj(customer customerToConvert);
	}
}