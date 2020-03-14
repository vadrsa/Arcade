using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEntities
{
    public enum ActivityType
    {
        Unknown,
        Login,
        Logout
    }

    public class ActivityDto
    {
        public DateTime Date { get; set; }
        public ActivityType Type { get; set; }
    }
}
