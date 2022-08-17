using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealState.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string ByteCodeBase64 { get; set; }
    }
}