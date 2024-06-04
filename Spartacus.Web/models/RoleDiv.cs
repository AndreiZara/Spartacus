using Spartacus.Domain.Entities.Feedback;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spartacus.Web.Models
{
    public class RoleDiv //Class made for role division, in order to divide the profile of an simple comsumer and an mentor(trainer)
    {
        public tmpModel UModel { get; set; }
        public MenDetTable MenDet {get; set;}
    }
}