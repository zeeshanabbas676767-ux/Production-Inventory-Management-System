using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class Gen_ProductsMV 
    {
        public int Id { get; set; }
        public Nullable<int> CatId { get; set; }
        public string Prod_Name { get; set; }
        public string Prod_Code { get; set; }
        public string Prod_Description { get; set; }
        public string Prod_Qty { get; set; }
        public string Prod_Length { get; set; }
        public string Prod_Weight { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Prod_DateIn { get; set; }
        public Nullable<System.DateTime> Prod_DateOut { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<decimal> Price { get; set; }
        public int BranchId { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public System.DateTime RecordTimeStamp { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string Category_Name { get; set; }
        public string Brand_Name { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public string Price_Method { get; set; }

        public virtual Gen_Categories Gen_Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gen_Stock> Gen_Stock { get; set; }
    }
}
