﻿using My_Final_Project.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Final_Project.Models.Entities
{
    public class Therapist : BaseEntity
    {
        public User User { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public string? Certificate { get; set; }
        public string? Credential { get; set; }
        public string? RegNo { get; set; }
        public string? Description { get; set; }
        public string?  ProfilePicture { get; set; }
        public IList<Booking>? Bookings { get; set; }
        public Aprrove? Status { get; set; } = Aprrove.Pending;
        public IList<TherapistIssue>? TherapistIssues { get; set; }
        public bool IsAvalaible { get; set; } = true;
        public bool IsActive { get; set; } = false;
        public IList<Client>? Clients { get; set; }
    }
}
