using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealState.Models
{
    public class SearchModel
    {
        public string Place { get; set; }
        public int? Area { get; set; }
        public int? Price { get; set; }
        public int? BedroomNumber { get; set; }
        public int? GarageSpace { get; set; }
    }
}