using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class parcelToList
        {
            public int ID { get; set; }
        public customerInP sender { get; set; }
        public customerInP recipient { get; set; }
        public enums.WeightCategories weight { get; set; }
        public enums.Priorities priority { get; set; }
        public enums.parcelStatus status { get; set; }
        public override string ToString()
            {
                return "Id: " + ID + " sender: " + sender + " recipient: " + recipient
                    + " weight: " + weight + " priority: " + priority + " status: " + status;
            }
        }
    }

