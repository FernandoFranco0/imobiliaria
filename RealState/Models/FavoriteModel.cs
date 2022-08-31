using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealState.Models
{
    public class FavoriteModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
    }
}