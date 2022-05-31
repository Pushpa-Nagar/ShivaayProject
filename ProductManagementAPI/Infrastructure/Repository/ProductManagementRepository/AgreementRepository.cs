using ProductManagementAPI.Infrastructure.DataModels;
using ProductManagementAPI.Infrastructure.Repository.IProductManagementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository.ProductManagementRepository
{
    public class AgreementRepository : RepositoryBase<Agreements>, IAgreementRepository
    {
        public AgreementRepository(ShivaayProductDBContext context) : base(context) { }

        public Task<bool> SaveAgreementDetails(Agreements agreementDetails)
        {
            try
            {
                base.Create(agreementDetails);
                return Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> UpdateAgreementDetails(Agreements agreementDetails)
        {
            try
            {
                base.Update(agreementDetails);
                return Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> DeleteAgreementDetails(Agreements agreementDetails)
        {
            try
            {
                base.Delete(agreementDetails);
                return Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }

        public Task<Agreements> GetAgreementDetailById(int agreementId)
        {
            try
            {
                var result = base.Single(x => x.AgreementId == agreementId);
                return Task.FromResult(result);
            }
            catch
            {
                throw;
            }
        }

    }
}
