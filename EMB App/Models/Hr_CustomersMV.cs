using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Hr_CustomersMV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hr_CustomersMV()
        {
            this.Gen_ServiceRates = new HashSet<Gen_ServiceRatesMV>();
        }

        public int Id { get; set; }
        public string Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public string NTN_Number { get; set; }
        public string STRN_Number { get; set; }
        public string Customer_Address { get; set; }
        public string Customer_City { get; set; }
        public string Customer_Country { get; set; }
        public string Customer_Email { get; set; }
        public string Customer_Email2 { get; set; }
        public string Customer_Phone { get; set; }
        public string Customer_Phone2 { get; set; }
        public string Customer_Mobile { get; set; }
        public string Customer_Fax { get; set; }
        public Nullable<decimal> Customer_Credit_Limit_CAP { get; set; }
        public Nullable<int> Customer_Invoice_Period { get; set; }
        public int BranchId { get; set; }
        public System.DateTime RecordTimeStamp { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gen_ServiceRatesMV> Gen_ServiceRates { get; set; }
    }
}
