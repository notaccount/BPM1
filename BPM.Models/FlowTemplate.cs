using System;

namespace BPM.Models
{
    public class FlowTemplate : BaseEntity
    {

        public string Name { get; set; }

        public bool IsUsed { get; set; }
    }
}
