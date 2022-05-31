using ProductManagementAPI.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository.IProductManagementRepository
{
    public interface IAgreementRepository : IRepositoryBase<Agreements>
    {
        Task<bool> SaveAgreementDetails(Agreements agreementDetails);
        Task<bool> UpdateAgreementDetails(Agreements agreementDetails);
        Task<bool> DeleteAgreementDetails(Agreements agreementDetails);
        Task<Agreements> GetAgreementDetailById(int agreementId);
    }
}
