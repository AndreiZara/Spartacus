using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spartacus.Web.Models
{
    public class Membership
    {
        public string Title { get; set; }
        public int[4] Prices { get; set; }
        public string Description { get; set; }
    }
}