using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO    
{    
    public class enums
    {
        public enum WeightCategories { Light, Medium, heavy };
        public enum Priorities { Regular, Fast, Emergency };

        public enum DroneStatuses { Available, Maintenance, Shipping };

        public enum parcelStatus { Defined, Associated, Collected, Provided };
        public enum groping { sender, recipient };


    }
}

