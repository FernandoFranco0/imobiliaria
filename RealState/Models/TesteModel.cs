using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealState.Models
{
    public class TesteModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsAuthenticated { get; set; }

        public void Login(string Name, int Id)
        {
            if (String.Equals(Name, "a") && Id == 2)
                IsAuthenticated = true;
            else
                IsAuthenticated = false;
        }
    }
}