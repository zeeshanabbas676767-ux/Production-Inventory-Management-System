using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Gen_ComponentsMV
    {
        public int Id { get; set; }
        public Nullable<int> Code { get; set; }
        public Nullable<int> D_Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> HeadToHead { get; set; }
        public Nullable<int> Stitch { get; set; }
        public Nullable<int> Tilla { get; set; }
        public Nullable<int> Seq_A { get; set; }
        public Nullable<int> Seq_B { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
