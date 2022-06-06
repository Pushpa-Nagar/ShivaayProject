using ProductManagementAPI.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository.IProductManagementRepository
{
    public interface IProductRepository : IRepositoryBase<Products>
    {
        Task<List<Products>> GetAllProductByGroupId(int productGroupId);
        Task<Products> GetProductDetailById(int productId);
    }
}
