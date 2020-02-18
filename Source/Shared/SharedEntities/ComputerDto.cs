using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEntities
{
    public class ComputerDto
    {
        public string Id { get; set; }

        public string TypeId { get; set; }

        public int Number { get; set; }

        public ComputerTypeDto Type { get; set; }

        public bool IsTerminated { get; set; }
    }

    public class ComputerSessionDto : ComputerDto
    {
        public DateTime NextAvailableTime { get; set; }
    }

    public class ComputerQueueDto : ComputerDto
    {

        public SessionDto Current { get; set; }
        public List<SessionDto> Queue { get; set; }
    }
}
