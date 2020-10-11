using System;
using System.Collections.Generic;
using System.Text;

namespace statmathPostgreSample.Database.Entities
{
    public class Machine
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<MachineJob> MachineJobs { get; set; }
    }
}
