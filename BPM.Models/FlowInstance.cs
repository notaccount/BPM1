using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
    public class FlowInstance : BaseEntity
    {
        public Guid FlowId { get; set; }
        public string ApplyId { get; set; }

        public Guid CurrentStep { get; set; }

        public Guid NextStep { get; set; }

        public int FlowStatus { get; set; }

        public int FlowFruit { get; set; }

        public string Xmllfo { get; set; }


    }
}
