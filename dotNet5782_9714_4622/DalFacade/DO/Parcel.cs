using System;
using System.Text;


    namespace DO
    {
        public struct Parcel
        {
            public int ID { get; set; }
            public int SenderID { get; set; }
            public int TargetID { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime? created { get; set; }
            public int DroneID { get; set; }//
            public bool Deleted { get; set; }
            public DateTime? Scheduled { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }
            public override string ToString()
            {
                return"Id: " + ID + " SenderID: " + SenderID + " TargetID: " + TargetID +
                 " Weight: " + Weight + " Priority: " + Priority + " Requested: " + created + " DroneID: " + DroneID
                 + " Scheduled: " + Scheduled + " PickedUp: " + PickedUp + " Delivered: " + Delivered;
                ;
            }
        }
    }

