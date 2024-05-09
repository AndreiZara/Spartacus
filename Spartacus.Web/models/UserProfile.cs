using System;

namespace Spartacus.Web.Models
{
    public class UserProfile
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime RemainingTime { get; set; }
        // and something related to category
        public string Title { get; set; }
        public string Description { get; set; }
    }
}