﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealState.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string ImageUrl { get; set; }
    }
}