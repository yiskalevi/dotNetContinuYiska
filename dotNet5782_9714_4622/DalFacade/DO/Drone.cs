using System;
using System.Text;


    namespace DO
    {
        public struct Drone
        {
            public int ID { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public bool Deleted { get; set; }
        public override string ToString()
            {
                return "Id: " + ID + " Name: " + Model + " MaxWeight: " + MaxWeight;
            }
        }
    }
