//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class ORDERLISTITEM
    {
        public decimal ORDERLISTITEMID { get; set; }
        public Nullable<decimal> SUBTOTAL { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public Nullable<decimal> PRODUCT { get; set; }
        public Nullable<decimal> ORDERID { get; set; }
    
        public virtual ORDER ORDER { get; set; }
        public virtual PRODUCT PRODUCT1 { get; set; }
    }
}
