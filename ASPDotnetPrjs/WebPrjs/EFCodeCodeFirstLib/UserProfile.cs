﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeCodeFirstLib
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public User User { get; set; }
    }
}
