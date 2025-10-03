using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Gen_MachineParametersMV
    {
        public int Id { get; set; }
        public string HeadToHead { get; set; }
        public Nullable<decimal> FrameHeight { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
