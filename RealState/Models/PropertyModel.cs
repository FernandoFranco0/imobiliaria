using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealState.Models
{
    public class PropertyModel
    {
        public int Id { get; set; }
        public int BedroomNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string NeighboorHood { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int Area { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public int GarageSpace { get; set; }
        public List<string> ImagesUrl { get; set; }
        public List<int> ImagesId { get; set; }
    }
}