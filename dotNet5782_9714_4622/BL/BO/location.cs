using System;

 

    namespace BO
    {
        public class location
        {
            public double longitude { get; set; }
        public double latitude { get; set; }
        public override string ToString()
            {
                return " longitude: " + longitude + " latitude: " + latitude;
            }

        }
    }

