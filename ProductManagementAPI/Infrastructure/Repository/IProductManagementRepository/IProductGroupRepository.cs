using ProductManagementAPI.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository.IProductManagementRepository
{
    public interface IProductGroupRepository : IRepositoryBase<ProductGroup>
    {
        Task<List<ProductGroup>> GetAllProductGroup();
    }
}
