using System;

namespace Spartacus.Domain.Entities.User
{
    public class GUID //Global Unique Identifier 
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsUsed { get; set; }
    }
}
