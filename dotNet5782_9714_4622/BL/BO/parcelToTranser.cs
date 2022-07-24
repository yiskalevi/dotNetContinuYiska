using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace BO
    {
        public class parcelToTranser
        {
            public int ID { get; set; }
        public bool isDelivered { get; set; }
        public enums.Priorities priority { get; set; }
        public enums.WeightCategories weight { get; set; }
        public customerInP sender { get; set; }
        public customerInP recipient { get; set; }
        public location collection { get; set; }
        public location destination { get; set; }
        public double distance { get; set; }
        public override string ToString()
            {
                return "Id: " + ID + "isDelivered:" + isDelivered + "priority:" + priority
                    + "weight:" + weight + "sender:" + sender + "recipient:" + recipient + "collection:"
                    + collection + "destination:" + destination + "distance:" + distance;
            }
        }
    }

