using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spartacus.Web.Models
{
    public class UFile
    {
        public HttpPostedFileBase FileModel { get; set; }
        public string Username { get; set; }
    }
}