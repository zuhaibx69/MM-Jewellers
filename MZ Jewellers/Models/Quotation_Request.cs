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
    
    public partial class Quotation_Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quotation_Request()
        {
            this.Quotation_Response = new HashSet<Quotation_Response>();
        }
    
        public int req_id { get; set; }
        public Nullable<int> prd_id { get; set; }
        public string jeweller_id { get; set; }
        public int prd_quantity { get; set; }
        public System.DateTime order_deadline { get; set; }
    
        public virtual Jeweller Jeweller { get; set; }
        public virtual Product Product { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quotation_Response> Quotation_Response { get; set; }
    }
}
