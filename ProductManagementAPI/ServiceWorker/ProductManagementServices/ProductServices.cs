using Common.CommonEnum;
using Common.ViewModels.CommonModel;
using Common.ViewModels.AgreementModel;
using Common.ViewModels.ResponseModel;
using Microsoft.Extensions.Options;
using ProductManagementAPI.Helpers;
using ProductManagementAPI.Infrastructure.DataModels;
using ProductManagementAPI.Infrastructure.Repository;
using ProductManagementAPI.Infrastructure.Repository.IProductManagementRepository;
using ProductManagementAPI.ServiceWorker.IProductManagementServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.ServiceWorker.ProductManagementServices
{
    public class ProductServices : IProductServices
    {
        #region Variables

        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAgreementRepository _agreementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        
        #endregion

        #region Constructor

        public ProductServices(IProductGroupRepository productGroupRepository, IProductRepository productRepository, IAgreementRepository agreementRepository,
            IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _productGroupRepository = productGroupRepository;
            _productRepository = productRepository;
            _agreementRepository = agreementRepository;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        #endregion

        #region Master APIs

        /// <summary>
        ///Get All ProductGroups
        /// </summary>
        public async Task<RecordsListResponseView<SelectionRecordView>> GetAllProductGroup()
        {
            RecordsListResponseView<SelectionRecordView> lstProductGroup = new RecordsListResponseView<SelectionRecordView>();
            try
            {
                var productGroupList = await _productGroupRepository.GetAllProductGroup();
                if (productGroupList != null && productGroupList.Count > 0)
                {
                    lstProductGroup.RecordsList = (from prodGroup in productGroupList
                                                   select new SelectionRecordView
                                                    {
                                                        SelectionRecordId = prodGroup.ProductGroupId,
                                                        SelectionRecordName = prodGroup.GroupCode
                                                    }).ToList();

                    lstProductGroup.TotalRecordCount = lstProductGroup.RecordsList.Count();
                    lstProductGroup.Message = "Success";
                    lstProductGroup.Type = _appSettings.IsSuccessType;
                    lstProductGroup.Code = Convert.ToInt32(ResponseCodeEnum.Success);
                }
                return lstProductGroup;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///Get All Products By GroupId
        /// </summary>
        public async Task<RecordsListResponseView<SelectionRecordView>> GetAllProductByGroupId(int productGroupId)
        {
            RecordsListResponseView<SelectionRecordView> lstProducts = new RecordsListResponseView<SelectionRecordView>();
            try
            {
                var productList = await _productRepository.GetAllProductByGroupId(productGroupId);
                if (productList != null && productList.Count > 0)
                {
                    lstProducts.RecordsList = (from product in productList
                                               select new SelectionRecordView
                                               {
                                                   SelectionRecordId = product.ProductId,
                                                   SelectionRecordName = product.ProductNumber
                                               }).ToList();

                    lstProducts.TotalRecordCount = lstProducts.RecordsList.Count();
                    lstProducts.Message = "Success";
                    lstProducts.Type = _appSettings.IsSuccessType;
                    lstProducts.Code = Convert.ToInt32(ResponseCodeEnum.Success);
                }
                return lstProducts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Agreement Crud Operations

        /// <summary>
        /// Save Agreement
        /// </summary>
        /// <returns>BaseResponseView</returns>
        public async Task<BaseResponseView> SaveAgreement(RecordContentView<AddAgreementInputView> recordContentView)
        {
            BaseResponseView responseResult = new BaseResponseView();
            Agreements agreementDetails = null;
            try
            {
                var productData = await _productRepository.GetProductDetailById(recordContentView.Records.ProductId);

                agreementDetails = new Agreements();
                agreementDetails.ProductGroupId = recordContentView.Records.ProductGroupId;
                agreementDetails.ProductId = recordContentView.Records.ProductId;
                agreementDetails.EffectiveDate = recordContentView.Records.EffectiveDate;
                agreementDetails.ExpirationDate = recordContentView.Records.ExpirationDate;
                agreementDetails.ProductPrice = productData != null ? productData.Price : 0;
                agreementDetails.NewPrice = recordContentView.Records.NewPrice;
                agreementDetails.UserId = recordContentView.Records.UserId;
                agreementDetails.UserName = recordContentView.Records.UserName;
                agreementDetails.Active = recordContentView.Records.Active;

                await _agreementRepository.SaveAgreementDetails(agreementDetails);
                _unitOfWork.Commit();

                responseResult.Message = "Agreement created successfully.";
                responseResult.Type = _appSettings.IsSuccessType;
                responseResult.Code = Convert.ToInt32(ResponseCodeEnum.Success);

                return responseResult;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Edit Agreement
        /// </summary>
        /// <returns>BaseResponseView</returns>
        public async Task<BaseResponseView> EditAgreement(RecordContentView<EditAgreementInputView> recordContentView)
        {
            BaseResponseView responseResult = new BaseResponseView();
            try
            {
                var agreementDetails = await _agreementRepository.GetAgreementDetailById(recordContentView.Records.AgreementId);
                if (agreementDetails != null)
                {
                    var productData = await _productRepository.GetProductDetailById(recordContentView.Records.ProductId);

                    agreementDetails.ProductGroupId = recordContentView.Records.ProductGroupId;
                    agreementDetails.ProductId = recordContentView.Records.ProductId;
                    agreementDetails.EffectiveDate = recordContentView.Records.EffectiveDate;
                    agreementDetails.ExpirationDate = recordContentView.Records.ExpirationDate;
                    agreementDetails.ProductPrice = productData != null ? productData.Price : 0;
                    agreementDetails.NewPrice = recordContentView.Records.NewPrice;
                    agreementDetails.Active = recordContentView.Records.Active;

                    await _agreementRepository.UpdateAgreementDetails(agreementDetails);
                    _unitOfWork.Commit();

                    responseResult.Message = "Agreement updated successfully.";
                    responseResult.Type = _appSettings.IsSuccessType;
                    responseResult.Code = Convert.ToInt32(ResponseCodeEnum.Success);
                }
                else
                {
                    responseResult.Message = "Content Not Found.";
                    responseResult.Type = _appSettings.IsErrorType;
                    responseResult.Code = Convert.ToInt32(ResponseCodeEnum.ContentNotFound);
                }
                return responseResult;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Agreement
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns>BaseResponseView</returns>
        public async Task<BaseResponseView> DeleteAgreement(int agreementId)
        {
            BaseResponseView responseResult = new BaseResponseView();
            try
            {
                var agreementDetails = await _agreementRepository.GetAgreementDetailById(agreementId);
                if (agreementDetails != null)
                {
                    await _agreementRepository.DeleteAgreementDetails(agreementDetails);
                    _unitOfWork.Commit();

                    responseResult.Message = "Agreement deleted successfully.";
                    responseResult.Type = _appSettings.IsSuccessType;
                    responseResult.Code = Convert.ToInt32(ResponseCodeEnum.Success);
                }
                else
                {
                    responseResult.Message = "Content Not Found.";
                    responseResult.Type = _appSettings.IsErrorType;
                    responseResult.Code = Convert.ToInt32(ResponseCodeEnum.ContentNotFound);
                }
                return responseResult;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Agreement Details Get API

        /// <summary>
        /// Get Agreement Details By ID
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        public async Task<AgreementDetailResponseView> GetAgreementDetailsById(int agreementId)
        {
            AgreementDetailResponseView productAgreementData = new AgreementDetailResponseView();
            try
            {
                var agreementDetails = await _agreementRepository.GetAgreementDetailById(agreementId);
                if (agreementDetails != null)
                {
                    productAgreementData = await _agreementRepository.GetAgreementDetails(agreementId);
                    productAgreementData.Message = "Success";
                    productAgreementData.Type = _appSettings.IsSuccessType;
                    productAgreementData.Code = Convert.ToInt32(ResponseCodeEnum.Success);
                }
                else
                {
                    productAgreementData.Message = "Content Not Found.";
                    productAgreementData.Type = _appSettings.IsErrorType;
                    productAgreementData.Code = Convert.ToInt32(ResponseCodeEnum.ContentNotFound);
                }
                return productAgreementData;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  Get all Agreements
        /// </summary>
        /// <returns></returns>
        public async Task<RecordsListResponseView<AgreementListView>> GetAllAgreementList(RecordContentView<AgreementFilterInputView> recordContentView)
        {
            try
            {
                RecordsListResponseView<AgreementListView> lstAgreementDetails = new RecordsListResponseView<AgreementListView>();
                lstAgreementDetails = await _agreementRepository.GetAgreementList(recordContentView.Records);
                lstAgreementDetails.Message = "Success";
                lstAgreementDetails.Type = _appSettings.IsSuccessType;
                lstAgreementDetails.Code = Convert.ToInt32(ResponseCodeEnum.Success);
                return lstAgreementDetails;
            }
            catch
            {
                throw;
            }
        }

        #endregion

    }
}
