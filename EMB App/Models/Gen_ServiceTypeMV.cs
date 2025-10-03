using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Gen_ServiceTypeMV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gen_ServiceTypeMV()
        {
            this.Gen_TheServices = new HashSet<Gen_TheServicesMV>();
        }

        public int Id { get; set; }
        public string Service_Type { get; set; }
        public string Service_TypeDescription { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<System.DateTime> RecordTimeStamp { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gen_TheServicesMV> Gen_TheServices { get; set; }
    }
}