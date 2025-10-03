using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Gen_ServiceRatesMV
    {
        public int Id { get; set; }
        public Nullable<int> Customer_Id { get; set; }
        public Nullable<int> Service_Id { get; set; }
        public string Charge_Fector { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> RecordTimeStamp { get; set; }

        public virtual Gen_TheServicesMV Gen_TheServices { get; set; }
        public virtual Hr_CustomersMV Hr_Customers { get; set; }
    }
}