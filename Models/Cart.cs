
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using E_Market.Models;

namespace E_Market.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        [Key]
        [ForeignKey("Product")]
        public int product_id { get; set; }
        public Nullable<System.DateTime> added_at { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
