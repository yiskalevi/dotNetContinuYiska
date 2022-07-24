using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class droneInParcel
        {
            public int ID { get; set; }
        public double battery { get; set; }
        public location droneLocation { get; set; }
        public override string ToString()
            {
                return "Id: " + ID + " battery: " + battery + "location:" + droneLocation;

            }
        }
    }

