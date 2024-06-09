using System;

namespace Spartacus.Domain.Entities.User
{
    public class URegData
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public DateTime RegTime { get; set; }
    }
}
