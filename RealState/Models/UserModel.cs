using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealState.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public int CpfCnpj { get; set; }
        public int RoleId { get; set; }
        public List<PropertyModel> PropertyList { get; set; }
        //public List<int> FavoritePropertyList { get; set; }
    }
}