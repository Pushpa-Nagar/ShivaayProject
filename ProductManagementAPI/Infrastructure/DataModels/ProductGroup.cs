using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProductManagementAPI.Infrastructure.DataModels
{
    public partial class ProductGroup
    {
        public ProductGroup()
        {
            Agreements = new HashSet<Agreements>();
            Products = new HashSet<Products>();
        }

        public int ProductGroupId { get; set; }
        public string GroupDescription { get; set; }
        public string GroupCode { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Agreements> Agreements { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
