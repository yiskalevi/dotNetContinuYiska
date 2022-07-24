using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        public class customerToList
        {
            public int ID { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int numberOfP { get; set; }//מספר חבילות שסופקו
        public int numberOfPNotDelivered { get; set; }//
        public int numberOfExpectedP { get; set; }//
        public int numberOfSentedP { get; set; }//
        public override string ToString()
            {
                return "Id: " + ID + " Name: " + name + "phone:" + phone + "numberOfP:" + numberOfP + "numberOfPNotDelivered:"
                    + numberOfPNotDelivered + "numberOfExpectedP:" + numberOfExpectedP + "numberOfSentedP:" + numberOfSentedP;

            }
        }
    }
   

