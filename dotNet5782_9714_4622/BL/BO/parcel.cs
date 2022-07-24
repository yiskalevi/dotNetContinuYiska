using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class parcel
        {
            public int ID { get; set; }
            public customerInP sender { get; set; }
        public customerInP recipient { get; set; }
        public enums.WeightCategories weight { get; set; }
        public enums.Priorities priority { get; set; }
        public droneInParcel droneInPar { get; set; }
        public DateTime? creatingTime { get; set; } //זמן יצירה
        public DateTime? assignmintTime { get; set; } //זמן שיוך
        public DateTime? collictionTime { get; set; } //זמן איסוף על ידי רחפן
        public DateTime? deliveryTime { get; set; } //זמן הגעת החבילה
        public override string ToString()
            {
                return "Id: " + ID + " weight: " + weight +
                 " priority: " + priority + "droneInPar:"+ droneInPar+ "creatingTime:" + creatingTime + "assignmintTime:" + assignmintTime +
                 "collictionTime:" + collictionTime + "deliveryTime:" + deliveryTime;
            }


        }
    }

