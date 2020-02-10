using SharedEntities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEntities
{
    public enum ApplicationRole
    {
        [IgnoreValue]
        Unset = 0,
        [Description("Tech")]
        Tech,
        [Description("Sales")]
        Sales,
        [Description("Manager")]
        Manager,
        [Description("Admin")]
        Admin
    }
}
