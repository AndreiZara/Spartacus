using System;

namespace Spartacus.Domain.Entities.User
{
    public class UProfData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? MembershipId { get; set; }
        //public DateTime LastLogin { get; set; }
        //public string LastIp { get; set; }
        //public URole Level { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
