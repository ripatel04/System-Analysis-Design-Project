//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMPG223_Group22_Project
{
    using System;
    using System.Collections.Generic;
    
    public partial class VISITORS_ON_TICKET
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VISITORS_ON_TICKET()
        {
            this.VISITORs = new HashSet<VISITOR>();
            this.TICKETs = new HashSet<TICKET>();
        }
    
        public int Visitors_on_Ticket_ID { get; set; }
        public Nullable<int> Visitors_ID { get; set; }
        public Nullable<int> Ticket_ID { get; set; }
        public Nullable<System.DateTime> Ticket_Date { get; set; }
        public Nullable<int> Ticket_Amount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VISITOR> VISITORs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TICKET> TICKETs { get; set; }
    }
}
