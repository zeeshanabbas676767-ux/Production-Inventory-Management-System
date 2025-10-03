using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using DataBaseLibrary;

namespace EMB_App.Models
{
    public class GenDesigns
    {
        public int Id { get; set; }
        public Nullable<int> Customer_Id { get; set; }
        public string EntryType { get; set; }
        public string Fabric { get; set; }
        public string Remarks { get; set; }
        public string Extra_Service { get; set; }
        public Nullable<int> Code { get; set; }
        public Nullable<int> DNo { get; set; }
        public Nullable<int> Head_Limit { get; set; }
        public Nullable<int> Req_Repeats { get; set; }
        public Nullable<int> Head { get; set; }
        public Nullable<int> Stitches { get; set; }
        public Nullable<int> T_Stitches { get; set; }
        public Nullable<int> T_Seq { get; set; }
        public Nullable<int> W_Head { get; set; }
        public Nullable<int> Seq { get; set; }
        [Column("Seq_Id", TypeName = "NVARCHAR(50)")] 
        [StringLength(50)]
        public string Seq_Id { get; set; }

        [Column("T_Id", TypeName = "NVARCHAR(50)")]  
        [StringLength(50)]
        public string T_Id { get; set; }
        [Column("Seq_Usage", TypeName = "NVARCHAR(50)")] 
        [StringLength(50)]
        public string Seq_Usage { get; set; }
        [Column("T_Usage", TypeName = "NVARCHAR(50)")] 
        [StringLength(50)]
        public string T_Usage { get; set; }
        public Nullable<int> MD_Id { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> Colors { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> Component_Code { get; set; }
        public Nullable<int> DCode { get; set; }
        public string CollectionName { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

    }
}
