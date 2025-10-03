using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMB_App.Models
{
    public partial class Gen_ThreadUsage
    {
        public int Id { get; set; }
        public Nullable<int> Code { get; set; }
        public Nullable<int> CompId { get; set; }
        public string ThreadId { get; set; }
        public Nullable<decimal> Usage { get; set; }
        public string Type { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}