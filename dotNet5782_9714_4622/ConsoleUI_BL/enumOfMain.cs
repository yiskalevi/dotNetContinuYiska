using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{

    enum options1
    {
        add, update, objectView, listView, exit
    };
    enum Add
    {
        station, drone, customer, parcel, back
    };
    enum Update
    {
        uDrone, uStation, uCustomer, sendDroneToCharge, freeDroneFromCharge, conectParcelToDrone, collectParcelByDrone, provideParcelByDrone, back
    };
    enum ObjectView
    {
        existS, existD, existC, existP, back
    };
    enum ListView
    {
        viewS, viewD, viewC, viewP, viewFP, viewSB, back
    };

}