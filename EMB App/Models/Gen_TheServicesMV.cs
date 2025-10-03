using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Gen_TheServicesMV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gen_TheServicesMV()
        {
            this.Gen_Machines = new HashSet<Gen_MachinesMV>();
            this.Gen_ServiceRates = new HashSet<Gen_ServiceRatesMV>();
        }

        public int Id { get; set; }
        public Nullable<int> Service_Type { get; set; }
        public string Service_Name { get; set; }
        public Nullable<int> Service_RateCharge { get; set; }
        public Nullable<int> Machine_Device { get; set; }
        public string Max_Range { get; set; }
        public Nullable<decimal> Max_Charges { get; set; }
        public Nullable<decimal> Min_Charges { get; set; }
        public Nullable<decimal> Field1 { get; set; }
        public Nullable<decimal> Field2 { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<System.DateTime> RecordTimeStamp { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gen_MachinesMV> Gen_Machines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gen_ServiceRatesMV> Gen_ServiceRates { get; set; }
        public virtual Gen_ServiceType Gen_ServiceType { get; set; }
    }
}