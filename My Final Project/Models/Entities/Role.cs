﻿namespace My_Final_Project.Models.Entities
{
    public class Role : BaseEntity 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public IList<UserRole> UserRoles { get; set; }
    }
}