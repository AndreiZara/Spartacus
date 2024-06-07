using Spartacus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Domain.Entities.Membership
{
    public class MsData
    {
        public int? LocId { get; set; }
        public int? CatId { get; set; }
        public MsDuration? Period { get; set; }
    }
}
