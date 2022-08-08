﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models
{
    public enum Access
    {
        Admin,
        User
    }
    public class User
    {
        public int UserId { get; set; }

        public Access ?Access { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
