﻿namespace WPFClient.Model
{
    public class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string ProfileType {  get; set; }
        public Company Company { get; set; }
    }
}