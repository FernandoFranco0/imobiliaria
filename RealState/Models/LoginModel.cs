using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealState.Models
{
    public class LoginModel : UserModel
    {
        public bool IsAuthenticated { get; set; }
    }
}