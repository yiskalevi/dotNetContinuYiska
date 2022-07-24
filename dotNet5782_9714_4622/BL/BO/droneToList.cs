using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;
namespace BO
{
    public class droneToList//: DependencyObject
    {
       public int ID { get; set; }
        public string model { get; set; } //was not clear 
        public enums.WeightCategories weight { get; set; }
        public double battery { get; set; }
            public enums.DroneStatuses statusOfDrone { get; set; }
            public location droneLocation { get; set; }
            public int parcelNumber { get; set; }
            public override string ToString()
            {
                return "Id: " + ID + " model: " + model + " weight: " + weight + " battery: " + battery
                    + " statusOfDrone: " + statusOfDrone + " location: " + droneLocation + " parcelNumber: " + parcelNumber;
            }

        }
 }

