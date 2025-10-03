using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Gen_JobOrdersMV
    {
        public int Id { get; set; }
        public Nullable<int> Code { get; set; } 
        public Nullable<int> CompId { get; set; }
        public Nullable<int> ReqRepeats { get; set; }
        public Nullable<int> ReqHead { get; set; }
        public Nullable<int> Panni { get; set; }
        public Nullable<int> Solving { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
