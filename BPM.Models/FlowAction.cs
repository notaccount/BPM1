using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
    public class FlowAction : BaseEntity
    {
        public Guid ActionId { get; set; }
        public string ActionName { get; set; }

        public string ActionCode { get; set; }


        

        public Guid StepId { get; set; }
    }
}
