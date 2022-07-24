using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class drone
        {
            public int ID { get; set; }
        public string model { get; set; }
        public enums.WeightCategories weight { get; set; }
        public double battery { get; set; }
        public enums.DroneStatuses statusOfDrone { get; set; }
        public parcelToTranser parcel { get; set; }
        public location droneLocation { get; set; }
        public override string ToString()
            {
                return "Id: " + ID + " Name: " + model + " weight: " + weight +
                 " battery: " + battery + "status:" + statusOfDrone + "parcel:"+ parcel+ "location:" + droneLocation;

            }
        }
    }

