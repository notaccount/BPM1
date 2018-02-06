using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BPM.Models
{
   public  class FlowStep :BaseEntity
    {
        public Guid FlowId { get; set; }
        public string StepName { get; set; }

        public int Sort { get; set; }

        public string StepType { get; set; }

        public string StepUser { get; set; }
    }
}