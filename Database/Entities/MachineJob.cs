using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace statmathPostgreSample.Database.Entities
{
    public class MachineJob
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MachineID { get; set; }
        public Machine Machine { get; set; }
    }
}
