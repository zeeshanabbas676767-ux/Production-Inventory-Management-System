using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Gen_MachinesMV
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public string HeadtoHead { get; set; } 
        public string FrameHeight { get; set; }
        public Nullable<int> HeadsNum { get; set; }
        public Nullable<int> NeedlesNum { get; set; }
        public string Serial_Number { get; set; }
        public string Calling_Number { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public Nullable<int> Warranty { get; set; }
        public Nullable<System.DateTime> ServiceEnd { get; set; }
        public Nullable<System.DateTime> ServiceNext { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public System.DateTime RecordTimeStamp { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }

        public virtual Gen_Brands Gen_Brands { get; set; }
        public virtual Gen_TheServicesMV Gen_TheServices { get; set; }
    }
}
