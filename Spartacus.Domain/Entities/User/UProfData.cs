using Spartacus.Domain.Enums;
using System;
using System.Web;

namespace Spartacus.Domain.Entities.User
{
    public class UProfData
    {
        public URole Role { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public string Activity { get; set; }
        public string InstagramUrl { get; set; }
        public string FacebookUrl { get; set; }

        // readonly
        public string FileName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? CatId { get; set; }
        // on post only
        public HttpPostedFileBase Image { get; set; }
    }
}
