using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
    public class InstanceDetail:BaseEntity
    {
        public Guid InstanceId { get; set; }
        public Guid StepId { get; set; }

        public string UserId { get; set; }

        public int IsOpen { get; set; }
        
        public string Comment { get; set; }

        public int HandleFruit { get; set; }

        public DateTime HandleTime { get; set; }


    }
}
