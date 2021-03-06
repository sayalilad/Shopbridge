//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace attempt_api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Shopbridge_inventory_1
    {
        [StringLength(10), Required]
        public string Name_ { get; set; }
        [StringLength(1000), Required]
        public string description_ { get; set; }
        [RegularExpression(@"\b(\d+(?:\.(?:[^0]\d|\d[^0]))?)\b")]
        public Nullable<decimal> price { get; set; }
        [StringLength(200)]
        public string image_name { get; set; }
        [StringLength(200)]
        public string thumb_name { get; set; }
    }
}
