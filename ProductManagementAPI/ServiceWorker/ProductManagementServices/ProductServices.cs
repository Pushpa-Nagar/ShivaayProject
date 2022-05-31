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
                agreementDetails = new Agreements();
                agreementDetails.ProductGroupId = recordContentView.Records.ProductGroupId;
                agreementDetails.ProductId = recordContentView.Records.ProductId;
                agreementDetails.UserId = "1";
                agreementDetails.EffectiveDate = recordContentView.Records.EffectiveDate;
                agreementDetails.ExpirationDate = recordContentView.Records.ExpirationDate;
                agreementDetails.ExpirationDate = recordContentView.Records.ExpirationDate;
                agreementDetails.ProductPrice = recordContentView.Records.ProductPrice;
                agreementDetails.NewPrice = recordContentView.Records.NewPrice;
                //agreementDetails.CreatedDate = DateTime.UtcNow;

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
                    agreementDetails.ProductId = recordContentView.Records.ProductId;
                    agreementDetails.EffectiveDate = recordContentView.Records.EffectiveDate;
                    agreementDetails.ExpirationDate = recordContentView.Records.ExpirationDate;
                    agreementDetails.ExpirationDate = recordContentView.Records.ExpirationDate;
                    agreementDetails.ProductPrice = recordContentView.Records.ProductPrice;
                    agreementDetails.NewPrice = recordContentView.Records.NewPrice;

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

        //#region Content Details Get API

        ///// <summary>
        ///// Get Content Details By ID
        ///// </summary>
        ///// <param name="contentId"></param>
        ///// <returns></returns>
        //public async Task<ContentDetailView> GetContentDetailsById(long contentId, long contentTypeId, List<long> blockedMemberIds, long memberId, long contentOwnerId)
        //{
        //    ContentDetailView contentData = new ContentDetailView();
        //    Contents content = new Contents();
        //    try
        //    {
        //        var contentDetail = await _contentDetailRepository.GetContentDetailByContentTypeId(contentId, contentTypeId);
        //        if (contentDetail != null)
        //        {
        //            if (!blockedMemberIds.Contains(contentDetail.ContentOwnerId))
        //            {
        //                await SaveContentViews(contentId, contentOwnerId);
        //                content = await _contentDetailRepository.GetContentDetailsById(contentId);
        //                if (content != null)
        //                {
        //                    contentData.ContentId = content.ContentId;
        //                    contentData.Title = content.Title;
        //                    contentData.Description = content.Description;
        //                    contentData.IncidentDate = content.IncidentDate;
        //                    contentData.EraIncidentDate = content.EraIncidentDate;
        //                    contentData.TotalViews = content.TotalViews;
        //                    contentData.TotalLikes = content.TotalLikes;
        //                    contentData.TotalComments = content.TotalComments;
        //                    contentData.TotalShares = content.TotalShares;
        //                    contentData.IsLiked = content.Likes != null && content.Likes.Where(x => x.CreatedBy == contentOwnerId).Count() == 1 ? true : false;
        //                    contentData.IsShared = content.Shares != null && content.Shares.Where(x => x.CreatedBy == contentOwnerId).Count() == 1 ? true : false;
        //                    contentData.CreatedBy = content.CreatedBy;
        //                    contentData.CreatedDate = content.CreatedDate;
        //                    contentData.ContentOwnerId = content.ContentOwnerId;

        //                    if (content.ContentCategoryMapping != null && content.ContentCategoryMapping.Count > 0)
        //                    {
        //                        contentData.ContentCategoryIds = content.ContentCategoryMapping.Where(x => x.IsDeleted == false).Select(x => x.ContentCategoryId).ToList();
        //                    }

        //                    if (content.ContentTimePeriodMapping != null && content.ContentTimePeriodMapping.Count > 0)
        //                    {
        //                        contentData.TimePeriodIds = content.ContentTimePeriodMapping.Where(x => x.IsDeleted == false).Select(x => x.TimePeriodId).ToList();
        //                    }

        //                    if (content.Likes != null && content.Likes.Count > 0)
        //                    {
        //                        contentData.Likes = (from likes in content.Likes
        //                                             where likes.IsDeleted == false && !blockedMemberIds.Contains(likes.CreatedBy)
        //                                             select new LikesView()
        //                                             {
        //                                                 ContentLikeId = likes.LikeId,
        //                                                 ContentId = likes.ContentId,
        //                                                 CreatedBy = likes.CreatedBy,
        //                                                 CreatedDate = likes.CreatedDate
        //                                             }).OrderByDescending(x => x.CreatedDate).Take(Convert.ToInt32(_appSettings.DefaultCount)).ToList();
        //                    }

        //                    if (content.Comments != null && content.Comments.Count > 0)
        //                    {
        //                        contentData.Comments = (from comments in content.Comments
        //                                                where comments.IsDeleted == false && !blockedMemberIds.Contains(comments.CreatedBy)
        //                                                select new CommentsView
        //                                                {
        //                                                    CommentId = comments.CommentId,
        //                                                    ContentId = comments.ContentId,
        //                                                    Comment = comments.Comment,
        //                                                    CreatedBy = comments.CreatedBy,
        //                                                    CreatedDate = comments.CreatedDate
        //                                                }).OrderByDescending(x => x.CreatedDate).Take(Convert.ToInt32(_appSettings.DefaultCount)).ToList();
        //                    }

        //                    if (content.ContentMediaInformations != null && content.ContentMediaInformations.Count > 0)
        //                    {
        //                        contentData.MediaContents = (from media in content.ContentMediaInformations
        //                                                     where media.IsDeleted == false
        //                                                     select new MediaDocumentResponseView
        //                                                     {
        //                                                         ContentMediaInformationId = media.ContentMediaInformationId,
        //                                                         MediaTypeId = media.MediaTypeId,
        //                                                         ImageHeight = media.ImageHeight,
        //                                                         ImageWidth = media.ImageWidth,
        //                                                         Sequence = media.Sequence,
        //                                                         MediaUrl = (!string.IsNullOrEmpty(media.MediaUrl) && (media.MediaTypeId == Convert.ToInt32(MediaTypeEnum.Image) || media.MediaTypeId == Convert.ToInt32(MediaTypeEnum.InternalVideo))) ? _appSettings.StorageRootPath + media.MediaUrl : (!string.IsNullOrEmpty(media.MediaUrl) && media.MediaTypeId == Convert.ToInt32(MediaTypeEnum.ExternalVideo)) ? media.MediaUrl : null,
        //                                                         ThumbnailUrl = !string.IsNullOrEmpty(media.ThumbnailUrl) ? _appSettings.StorageRootPath + media.ThumbnailUrl : (media.MediaTypeId == Convert.ToInt64(MediaTypeEnum.ExternalVideo) || media.MediaTypeId == Convert.ToInt32(MediaTypeEnum.InternalVideo)) ? _appSettings.StorageRootPath + _contentDefaultThumbImage : null  //!string.IsNullOrEmpty(media.ThumbnailUrl) ? _appSettings.StorageRootPath + media.ThumbnailUrl : null
        //                                                     }).OrderBy(x => x.Sequence).ToList();
        //                    }
        //                    contentData.Message = "Success";
        //                    contentData.Type = _appSettings.IsSuccessType;
        //                    contentData.Code = Convert.ToInt32(ResponseCodeEnum.Success);
        //                }
        //                else
        //                {
        //                    contentData.Message = "Content Not Found";
        //                    contentData.Type = _appSettings.IsErrorType;
        //                    contentData.Code = Convert.ToInt32(ResponseCodeEnum.ContentNotFound);
        //                }
        //            }
        //            else
        //            {
        //                contentData.Message = "You are not authorized for this.";
        //                contentData.Type = _appSettings.IsErrorType;
        //                contentData.Code = Convert.ToInt32(ResponseCodeEnum.ContentNotFound);
        //            }
        //        }
        //        else
        //        {
        //            contentData.Message = "Content Not Found";
        //            contentData.Type = _appSettings.IsErrorType;
        //            contentData.Code = Convert.ToInt32(ResponseCodeEnum.ContentNotFound);
        //        }
        //        return contentData;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        /////  Get all Contents
        ///// </summary>
        ///// <returns></returns>
        //public async Task<RecordsListResponseView<ContentListView>> GetAllContents(RecordContentView<ContentFilterView> contentFilterView)
        //{
        //    try
        //    {
        //        RecordsListResponseView<ContentListView> listContentView = new RecordsListResponseView<ContentListView>();
        //        listContentView = await _contentDetailRepository.GetContentList(contentFilterView, _contentDefaultThumbImage);
        //        listContentView.Message = "Success";
        //        listContentView.Type = _appSettings.IsSuccessType;
        //        listContentView.Code = Convert.ToInt32(ResponseCodeEnum.Success);
        //        return listContentView;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //#endregion

    }
}
