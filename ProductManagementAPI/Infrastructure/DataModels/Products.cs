using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProductManagementAPI.Infrastructure.DataModels
{
    public partial class Products
    {
        public Products()
        {
            Agreements = new HashSet<Agreements>();
        }

        public int ProductId { get; set; }
        public int ProductGroupId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductNumber { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
        public virtual ICollection<Agreements> Agreements { get; set; }
    }
}
