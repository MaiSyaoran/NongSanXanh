//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ordersdetail
    {
        public int ID { get; set; }
        public int orderid { get; set; }
        public int productid { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public int priceSale { get; set; }
        public double amount { get; set; }
    }
}
