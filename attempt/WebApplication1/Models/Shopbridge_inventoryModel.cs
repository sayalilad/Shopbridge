using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Shopbridge_inventoryModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please Enter Name of an item")]
        [StringLength(10,ErrorMessage ="Please Enter Name of length upto 10 characters")]
        public string Name_ { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please description of an item")]
        [StringLength(1000, ErrorMessage = "Please Enter description of length upto 10 characters")]
        public string description_ { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please price of an item")]
        [RegularExpression(@"\b(\d+(?:\.(?:[^0]\d|\d[^0]))?)\b",ErrorMessage ="Please Enter proper value of an item")]
        public Nullable<decimal> price { get; set; }
        
        [StringLength(200)]
        public string image_name { get; set; }

        [StringLength(200)]
        public string thumb_name { get; set; }
    }
}