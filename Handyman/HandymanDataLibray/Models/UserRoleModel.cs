﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.Models
{
    public class UserRoleModel:IUserRoleModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
