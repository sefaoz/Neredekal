using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Common.Domain.OutboxModel
{
    public enum OutboxMessageStatus
    {
        Pending = 1,
        Published = 2,
        Error =3
    }
}
