//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MZ_Jewellers.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inventory
    {
        public int order_id { get; set; }
        public string prd_name { get; set; }
        public string prd_description { get; set; }
        public int prd_quantity { get; set; }
        public string prd_unit { get; set; }
        public int total_amount { get; set; }
    
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
