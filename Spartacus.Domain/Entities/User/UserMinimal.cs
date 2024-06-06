using Spartacus.Domain.Enums;
using System;

namespace Spartacus.Domain.Entities.User
{
    public class UserMinimal
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public string LastIp { get; set; }
        public URole Role { get; set; }
    }
}
