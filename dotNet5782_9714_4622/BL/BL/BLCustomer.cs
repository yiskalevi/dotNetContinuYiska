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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool checkCustomer(string n, int i)//יסכה הוסיפה
        {
            lock (dal)
            {
                try
                {
                    DO.Customer c = new DO.Customer();
                    c = dal.GetCustomer(i);
                    if (c.Name == n)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {

                    throw new AddingProblemException("can not find a customer", ex);
                }
            }
        }

        #region addCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(customer customerToADD)
        {
            lock (dal)
            {
                DO.Customer dalcust = new DO.Customer();
                try
                {
                    dalcust.ID = customerToADD.ID;
                    dalcust.Name = customerToADD.name;
                    dalcust.Phone = customerToADD.phone;
                    dalcust.Longitude = customerToADD.locationC.longitude;
                    dalcust.Latitude = customerToADD.locationC.latitude;

                    dal.addCustomer(dalcust);
                }
                catch (Exception ex)
                {

                    throw new AddingProblemException("can not add a customer", ex);
                }
            }
        }
        #endregion addCustomer

        #region converToCListObj
        customerToList converToCListObj(DO.Customer customerToConvert)
        {
            customerToList cust = new customerToList();
            cust.ID = customerToConvert.ID;
            cust.name = customerToConvert.Name;
            cust.phone = customerToConvert.Phone;
            cust.numberOfP = 0;
            cust.numberOfPNotDelivered = 0;
            cust.numberOfExpectedP = 0;
            cust.numberOfSentedP = 0;
            var t = from j in getCustomer(cust.ID).listFromCus
                    where j.status == enums.parcelStatus.Provided
                    select j;
            cust.numberOfSentedP = t.Count();
            cust.numberOfPNotDelivered = getCustomer(cust.ID).listFromCus.Count() - cust.numberOfSentedP;
            var r = from j in getCustomer(cust.ID).listToCus
                    where j.status == enums.parcelStatus.Provided
                    select j;
            cust.numberOfP = r.Count();
            cust.numberOfExpectedP = getCustomer(cust.ID).listToCus.Count() - cust.numberOfP;
            return cust;
        }
        #endregion converToCListObj
        #region converToCListObj
        public customerToList converToCListObj(customer customerToConvert)
        {
            customerToList cust = new customerToList();
            cust.ID = customerToConvert.ID;
            cust.name = customerToConvert.name;
            cust.phone = customerToConvert.phone;
            cust.numberOfP = 0;
            cust.numberOfPNotDelivered = 0;
            cust.numberOfExpectedP = 0;
            cust.numberOfSentedP = 0;
            var t = from j in getCustomer(cust.ID).listFromCus
                    where j.status == enums.parcelStatus.Provided
                    select j;
            cust.numberOfSentedP = t.Count();
            cust.numberOfPNotDelivered = getCustomer(cust.ID).listFromCus.Count() - cust.numberOfSentedP;
            var r = from j in getCustomer(cust.ID).listToCus
                    where j.status == enums.parcelStatus.Provided
                    select j;
            cust.numberOfP = r.Count();
            cust.numberOfExpectedP = getCustomer(cust.ID).listToCus.Count() - cust.numberOfP;
            return cust;
        }
        #endregion converToCListObj

        #region getCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public customer getCustomer(int customerID)
        {
            try
            {
                lock (dal)
                {
                    customer custt = new customer();
                    custt.listToCus = new List<parcelToList>();
                    custt.listFromCus = new List<parcelToList>();
                    IEnumerable<DO.Parcel> sortedList = new List<DO.Parcel>();
                    DO.Customer dalcust = dal.GetCustomer(customerID); //Exception when customer doesnt exist
                    custt.ID = dalcust.ID;
                    custt.name = dalcust.Name;
                    custt.phone = dalcust.Phone;
                    location locC = new location();
                    locC.latitude = dalcust.Latitude;
                    locC.longitude = dalcust.Longitude;
                    custt.locationC = locC;
                    sortedList = dal.viewListOfParcels();
                    sortedList = sortedList.Where(x => x.SenderID == custt.ID);
                    custt.listFromCus = sortedList.Select(convertToPListObj);
                    sortedList = dal.viewListOfParcels();
                    sortedList = sortedList.Where(x => x.TargetID == custt.ID);
                    custt.listToCus = sortedList.Select(convertToPListObj);
                    return custt;
                }
            }
            catch (Exception ex)
            {
                throw new GetDetailsProblemException("can not get the customer", ex);
            }
        }
        #endregion getCustomer

        #region updateCustomerName
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomerName(int id, string name)
        {
            lock (dal)
            {
                try
                {
                    dal.updateCustomerName(id, name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion updateCustomerName

        #region updateCustomerPhone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomerPhone(int d, string phone, int x)
        {
            lock (dal)
            {
                try
                {
                    dal.updateCustomerPhone(d, phone, x);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion updateCustomerPhone

        #region updateCustomerNameAndPhone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomerNameAndPhone(int d, string phone, string name)
        {
            lock (dal)
            {
                try
                {
                    dal.updateCustomerNameAndPhone(d, phone, name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion updateCustomerNameAndPhone

        #region viewListOfCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<customerToList> viewListOfCustomer()
        {
            lock (dal)
            {
                IEnumerable<DO.Customer> sortedList = new List<DO.Customer>();
                sortedList = dal.viewListOfCustomers();
                IEnumerable<customerToList> customerList = new List<customerToList>();
                customerList = sortedList.Select(converToCListObj);
                return customerList;
            }
        }
        #endregion viewListOfCustomer
    }
}