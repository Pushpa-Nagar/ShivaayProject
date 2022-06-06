using ProductManagementAPI.Infrastructure.DataModels;
using ProductManagementAPI.Infrastructure.Repository.IProductManagementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository.ProductManagementRepository
{
    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductRepository(ShivaayProductDBContext context) : base(context) { }
        public Task<List<Products>> GetAllProductByGroupId(int productGroupId)
        {
            List<Products> productResult = new List<Products>();
            try
            {
                var productDetails =  base.Find(x => x.ProductGroupId == productGroupId && x.Active == true).ToList();
                productResult = productDetails.ToList();
                return Task.FromResult(productResult);
            }
            catch
            {
                throw;
            }
        }

        public Task<Products> GetProductDetailById(int productId)
        {
            Products productResult = new Products();
            try
            {
                var productDetails = base.Single(x => x.ProductId == productId && x.Active == true);
                productResult = productDetails;
                return Task.FromResult(productResult);
            }
            catch
            {
                throw;
            }
        }
    }
}
