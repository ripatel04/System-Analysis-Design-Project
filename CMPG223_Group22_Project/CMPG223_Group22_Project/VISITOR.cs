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
    
    public partial class VISITOR
    {
        public int Visitors_ID { get; set; }
        public string Visitors_LName { get; set; }
        public string Visitors_Name { get; set; }
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        public string ContactNumber { get; set; }
    
        public virtual VISITORS_ON_TICKET VISITORS_ON_TICKET { get; set; }
    }
}
