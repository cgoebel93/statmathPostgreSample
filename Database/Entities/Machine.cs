using System;
using System.Collections.Generic;
using System.Text;

namespace statmathPostgreSample.Database.Entities
{
    public class Machine
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<MachineJob> MachineJobs { get; set; }
    }
}
