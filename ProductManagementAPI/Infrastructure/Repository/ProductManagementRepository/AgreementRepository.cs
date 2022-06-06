using Common.ViewModels.AgreementModel;
using Common.ViewModels.ResponseModel;
using ProductManagementAPI.Extensions;
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

        public Task<AgreementDetailResponseView> GetAgreementDetails(int agreementId)
        {
            AgreementDetailResponseView agreementDetails = new AgreementDetailResponseView();
            try
            {
                var agreementData = (from agree in _context.Agreements
                                     join grpProduct in _context.ProductGroup on agree.ProductGroupId equals grpProduct.ProductGroupId
                                     join product in _context.Products on agree.ProductId equals product.ProductId
                                     where agree.AgreementId == agreementId
                                     select new AgreementDetailResponseView
                                     {
                                         AgreementId = agree.AgreementId,
                                         ProductGroupId = agree.ProductGroupId,
                                         ProductId = agree.ProductId,
                                         EffectiveDate = agree.EffectiveDate,
                                         ExpirationDate = agree.ExpirationDate,
                                         NewPrice = agree.NewPrice,
                                         Active = agree.Active
                                     }).FirstOrDefault();

                return Task.FromResult(agreementData);
            }
            catch
            {
                throw;
            }
        }

        public Task<RecordsListResponseView<AgreementListView>> GetAgreementList(AgreementFilterInputView agreementFilterInputView)
        {
            RecordsListResponseView<AgreementListView> lstAgreementDetails = new RecordsListResponseView<AgreementListView>();
            try
            {
                var recordsList = (from agree in _context.Agreements
                                   join grpProduct in _context.ProductGroup on agree.ProductGroupId equals grpProduct.ProductGroupId
                                   join product in _context.Products on agree.ProductId equals product.ProductId
                                   select new AgreementListView
                                   {
                                       AgreementId = agree.AgreementId,
                                       UserId = agree.UserId,
                                       UserName = agree.UserName,
                                       ProductGroupId = agree.ProductGroupId,
                                       GroupDescription = grpProduct.GroupDescription,
                                       GroupCode = grpProduct.GroupCode,
                                       ProductId = agree.ProductId,
                                       ProductDescription = product.ProductDescription,
                                       ProductNumber = product.ProductNumber,
                                       EffectiveDate = agree.EffectiveDate,
                                       ExpirationDate = agree.ExpirationDate,
                                       ProductPrice = agree.ProductPrice,
                                       NewPrice = agree.NewPrice
                                   }).ToList();


                if (agreementFilterInputView.SortColumn == null)
                    recordsList = recordsList.OrderByDescending(s => s.AgreementId).ToList();
                else if (agreementFilterInputView.SortOrder == "asc")
                {
                    if (agreementFilterInputView.SortColumn == "User Name")
                        recordsList = recordsList.OrderBy(s => s.UserName).ToList();
                    else if (agreementFilterInputView.SortColumn == "Group Code")
                        recordsList = recordsList.OrderBy(s => s.GroupCode).ToList();
                    else if (agreementFilterInputView.SortColumn == "Product Number")
                        recordsList = recordsList.OrderBy(s => s.ProductNumber).ToList();
                    else if (agreementFilterInputView.SortColumn == "Effective Date")
                        recordsList = recordsList.OrderBy(s => s.EffectiveDate).ToList();
                    else if (agreementFilterInputView.SortColumn == "Expiration Date")
                        recordsList = recordsList.OrderBy(s => s.ExpirationDate).ToList();
                    else if (agreementFilterInputView.SortColumn == "Product Price")
                        recordsList = recordsList.OrderBy(s => s.ProductPrice).ToList();
                    else if (agreementFilterInputView.SortColumn == "New Price")
                        recordsList = recordsList.OrderBy(s => s.NewPrice).ToList();
                }
                else
                {
                    if (agreementFilterInputView.SortColumn == "User Name")
                        recordsList = recordsList.OrderByDescending(s => s.UserName).ToList();
                    else if (agreementFilterInputView.SortColumn == "Group Code")
                        recordsList = recordsList.OrderByDescending(s => s.GroupCode).ToList();
                    else if (agreementFilterInputView.SortColumn == "Product Number")
                        recordsList = recordsList.OrderByDescending(s => s.ProductNumber).ToList();
                    else if (agreementFilterInputView.SortColumn == "Effective Date")
                        recordsList = recordsList.OrderByDescending(s => s.EffectiveDate).ToList();
                    else if (agreementFilterInputView.SortColumn == "Expiration Date")
                        recordsList = recordsList.OrderByDescending(s => s.ExpirationDate).ToList();
                    else if (agreementFilterInputView.SortColumn == "Product Price")
                        recordsList = recordsList.OrderByDescending(s => s.ProductPrice).ToList();
                    else if (agreementFilterInputView.SortColumn == "New Price")
                        recordsList = recordsList.OrderByDescending(s => s.NewPrice).ToList();
                }

                if (!string.IsNullOrEmpty(agreementFilterInputView.SearchText))
                {
                    // Replacing extra spacing between words.
                    string searchStringCleaned = string.Join(" ", agreementFilterInputView.SearchText.Trim().Split(' ').Where(s => !string.IsNullOrEmpty(s)));
                    recordsList = recordsList.Where(x => (x.UserName != null && x.UserName.ToLower().Contains(searchStringCleaned.ToLower())) || (x.GroupCode != null && x.GroupCode.ToLower().Contains(searchStringCleaned.ToLower())) || (x.ProductNumber != null && x.ProductNumber.ToLower().Contains(searchStringCleaned.ToLower()))).ToList();
                }
                lstAgreementDetails.TotalRecordCount = recordsList.Count;
                lstAgreementDetails.RecordsList = recordsList.GetCurrentPageList(agreementFilterInputView.PageSize, agreementFilterInputView.SkipRecord);
                return Task.FromResult(lstAgreementDetails);
            }
            catch
            {
                throw;
            }
        }

    }
}
