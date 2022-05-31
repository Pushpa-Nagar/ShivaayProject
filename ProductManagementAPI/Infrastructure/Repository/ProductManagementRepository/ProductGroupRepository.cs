using ProductManagementAPI.Infrastructure.DataModels;
using ProductManagementAPI.Infrastructure.Repository.IProductManagementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository.ProductManagementRepository
{
    public class ProductGroupRepository : RepositoryBase<ProductGroup>, IProductGroupRepository
    {
        public ProductGroupRepository(ShivaayProductDBContext context) : base(context) { }

        public Task<List<ProductGroup>> GetAllProductGroup()
        {
            List<ProductGroup> resultGroup = new List<ProductGroup>();
            try
            {
                var productGroup = base.GetAll().Where(x => x.Active == true).ToList();
                resultGroup = productGroup.ToList();
                return Task.FromResult(resultGroup);
            }
            catch
            {
                throw;
            }
        }
    }
}
