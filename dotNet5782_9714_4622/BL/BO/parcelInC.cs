using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace BO
    {
        public class parcelInC
        {
            public int ID;
            public enums.WeightCategories weight { get; set; }
        public enums.Priorities priority { get; set; }
        public enums.parcelStatus status { get; set; }
        public customerInP desOrsourc { get; set; }
        public override string ToString()
            {
                return "Id: " + ID + "weight:" + weight + "priority:" + priority
                    + "status:" + status + "desOrsourc:" + desOrsourc;
            }

        }
    }

