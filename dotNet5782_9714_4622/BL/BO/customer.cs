using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace BO
    {
        public class customer
        {
            public int ID { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public location locationC { get; set; }
        public IEnumerable<parcelToList> listFromCus { get; set; }
        public IEnumerable<parcelToList> listToCus { get; set; }
        public override string ToString()
            {
                return "Id: " + ID + " Name: " + name + " Phone: " + phone +
                 " location: " + locationC ;
                
            }
        }
    }

