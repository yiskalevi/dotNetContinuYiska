using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class station
        {
        /// <summary>
        /// station id
        /// </summary>
            public int ID { get; set; }
        /// <summary>
        /// station name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// station Location
        /// </summary>
        public location stationLocation { get; set; }
        /// <summary>
        /// station number Of Stands
        /// </summary>
        public int numberOfStands { get; set; }
        /// <summary>
        /// station list drone in charge
        /// </summary>
        public IEnumerable<droneInCharning> drones { get; set; }
        public override string ToString()
            {
                return "Id: " + ID + " name: " + name +
                 " location: " + stationLocation + " numberOfStands: " + numberOfStands; 
            }
        }
    }

