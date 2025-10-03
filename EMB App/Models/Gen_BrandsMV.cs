using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Gen_BrandsMV
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public int BranchId { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
        public System.DateTime RecordTimeStamp { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gen_Categories> Gen_Categories { get; set; } 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gen_MachinesMV> Gen_Machines { get; set; }
    }
}

