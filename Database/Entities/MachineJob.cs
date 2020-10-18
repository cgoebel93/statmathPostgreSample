using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace statmathPostgreSample.Database.Entities
{
    public class MachineJob : Entity
    {
        public int id { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int machineid { get; set; }
        public Machine Machine { get; set; }
    }
}
