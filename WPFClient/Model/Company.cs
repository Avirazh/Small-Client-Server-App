﻿using System.Collections.Generic;

namespace WPFClient.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}