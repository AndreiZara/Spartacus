using System;

namespace Spartacus.Domain.Entities.User
{
    public class ULoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
