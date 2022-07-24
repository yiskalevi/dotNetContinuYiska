﻿using System;
using System.Text;


    namespace DO
    {
    public struct Station 
    {
            public int ID { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public int ChargeSlots { get; set; }
            public bool Deleted { get; set; }
            public override string ToString()
            {
                return "ID:"+ID + " Name:"+ Name+ " Longitude:"+ Longitude+ " Latitude:"+ Latitude+ " ChargeSlots:"+ ChargeSlots;
            }
        }
    }

