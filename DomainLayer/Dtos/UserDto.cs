﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Dtos
{
    public class UserDto
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string email { get; set; }
    }
}
