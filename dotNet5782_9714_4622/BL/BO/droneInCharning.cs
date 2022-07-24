using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class droneInCharning
        {
            public int ID { get; set; }
        public double battery { get; set; }
        public DateTime TimeCharning { get; set; }
        public override string ToString()
            {
                return "Id: " + ID + " battery: " + battery; 

            }
        }
    }

