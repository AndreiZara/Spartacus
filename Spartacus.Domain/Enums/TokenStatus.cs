using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Domain.Enums
{
    public enum TokenStatus :int
    {
        Confirmed = 0,
        Unconfirmed = 1,
        Default = 2
    }
}
