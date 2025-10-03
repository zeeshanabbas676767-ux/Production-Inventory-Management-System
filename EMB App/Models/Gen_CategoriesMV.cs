using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Gen_CategoriesMV
    {
        public int Id { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string MeasureType { get; set; }
        public Nullable<decimal> MeasureValue { get; set; }
        public string Quantity { get; set; }
        public string Length { get; set; }
        public string Weight { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<System.DateTime> RecordTimeStamp { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public string Category_Name { get; set; }

        public virtual Gen_Brands Gen_Brands { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gen_ProductsMV> Gen_Products { get; set; }
    }
}
