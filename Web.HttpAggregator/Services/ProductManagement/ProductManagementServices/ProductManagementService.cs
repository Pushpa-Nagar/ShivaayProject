using Common.ViewModels.CommonModel;
using Common.ViewModels.AgreementModel;
using Common.ViewModels.ResponseModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Web.HttpAggregator.Config;
using Web.HttpAggregator.Config.UrlsConfig.ProductManagement;
using Web.HttpAggregator.Services.ProductManagement.IProductManagementServices;

namespace Web.HttpAggregator.Services.ProductManagement.ProductManagementServices
{
    public class ProductManagementService : IProductManagementService
    {
        #region Private Variables

        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductManagementService> _logger;
        private readonly BaseUrlsConfig _urls;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ICommonService _commonService;

        #endregion

        #region Constructor
        public ProductManagementService(HttpClient httpClient, ILogger<ProductManagementService> logger,
            IOptions<BaseUrlsConfig> config) //, UserManager<ApplicationUser> userManager) //, ICommonService commonService)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = config.Value;
            //_userManager = userManager;
            //_commonService = commonService;
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
                //UserDetailView userDetails = await _commonService.GetUserByTokenForWeb();
                //if (userDetails != null)
                //{
                var lstContent = await _httpClient.GetAsync(_urls.ProductManagement + ProductManagementOperations.GetAllProductGroup());
                if (lstContent.IsSuccessStatusCode)
                {
                    lstProductGroup = (lstContent != null) ? JsonConvert.DeserializeObject<RecordsListResponseView<SelectionRecordView>>(lstContent.Content.ReadAsStringAsync().Result) : null;
                }
                //}
                //else
                //{
                //    lstAbuseCategory.Message = "InValid User";
                //    lstAbuseCategory.Type = "Error";
                //    lstAbuseCategory.Code = Convert.ToInt32(ResponseCodeEnum.UnauthorizedUser);
                //}
                return lstProductGroup;
            }
            catch (Exception)
            {
                throw;
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
                //UserDetailView userDetails = await _commonService.GetUserByTokenForWeb();
                //if (userDetails != null)
                //{
                var lstContent = await _httpClient.GetAsync(_urls.ProductManagement + ProductManagementOperations.GetAllProductByGroupId(productGroupId));
                if (lstContent.IsSuccessStatusCode)
                {
                    lstProducts = (lstContent != null) ? JsonConvert.DeserializeObject<RecordsListResponseView<SelectionRecordView>>(lstContent.Content.ReadAsStringAsync().Result) : null;
                }
                //}
                //else
                //{
                //    lstContentCategory.Message = "InValid User";
                //    lstContentCategory.Type = "Error";
                //    lstContentCategory.Code = Convert.ToInt32(ResponseCodeEnum.UnauthorizedUser);
                //}
                return lstProducts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Agreement Crud Operations
        public async Task<BaseResponseView> SaveAgreement(RecordInputView<AddAgreementInputView> recordInputView)
        {
            BaseResponseView result = new BaseResponseView();
            RecordContentView<AddAgreementInputView> recordContentView = new RecordContentView<AddAgreementInputView>();
            recordContentView.Records = new AddAgreementInputView();
            try
            {
                //UserDetailView userDetails = await _commonService.GetUserByTokenForWeb();
                //if (userDetails != null)
                //{

                recordContentView.Records.ProductGroupId = recordInputView.Records.ProductGroupId;
                recordContentView.Records.ProductId = recordInputView.Records.ProductId;
                recordContentView.Records.EffectiveDate = recordInputView.Records.EffectiveDate;
                recordContentView.Records.ExpirationDate = recordInputView.Records.ExpirationDate;
                recordContentView.Records.ProductPrice = recordInputView.Records.ProductPrice;
                recordContentView.Records.NewPrice = recordInputView.Records.NewPrice;

                var contentData = new StringContent(JsonConvert.SerializeObject(recordContentView), System.Text.Encoding.UTF8, "application/json");
                var data = await _httpClient.PostAsync(_urls.ProductManagement + ProductManagementOperations.SaveAgreement(), contentData);
                if (data.IsSuccessStatusCode)
                {
                    result = (data != null) ? JsonConvert.DeserializeObject<BaseResponseView>(data.Content.ReadAsStringAsync().Result) : null;
                }
                //}
                //else
                //{
                //    result.Message = "InValid User";
                //    result.Type = "Error";
                //    result.Code = Convert.ToInt32(ResponseCodeEnum.UnauthorizedUser);
                //}
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponseView> EditAgreement(RecordInputView<EditAgreementInputView> recordInputView)
        {
            BaseResponseView result = new BaseResponseView();
            RecordContentView<EditAgreementInputView> recordContentView = new RecordContentView<EditAgreementInputView>();
            recordContentView.Records = new EditAgreementInputView();
            try
            {
                //UserDetailView userDetails = await _commonService.GetUserByTokenForWeb();
                //if (userDetails != null)
                //{

                recordContentView.Records.AgreementId = recordInputView.Records.AgreementId;
                recordContentView.Records.ProductId = recordInputView.Records.ProductId;
                recordContentView.Records.EffectiveDate = recordInputView.Records.EffectiveDate;
                recordContentView.Records.ExpirationDate = recordInputView.Records.ExpirationDate;
                recordContentView.Records.ProductPrice = recordInputView.Records.ProductPrice;
                recordContentView.Records.NewPrice = recordInputView.Records.NewPrice;

                var contentData = new StringContent(JsonConvert.SerializeObject(recordContentView), System.Text.Encoding.UTF8, "application/json");
                var data = await _httpClient.PostAsync(_urls.ProductManagement + ProductManagementOperations.EditAgreement(), contentData);
                if (data.IsSuccessStatusCode)
                {
                    result = (data != null) ? JsonConvert.DeserializeObject<BaseResponseView>(data.Content.ReadAsStringAsync().Result) : null;
                }

                //}
                //else
                //{
                //    result.Message = "InValid User";
                //    result.Type = "Error";
                //    result.Code = Convert.ToInt32(ResponseCodeEnum.UnauthorizedUser);
                //}
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponseView> DeleteAgreement(int agreementId)
        {
            BaseResponseView result = new BaseResponseView();
            try
            {
                //UserDetailView userDetails = await _commonService.GetUserByTokenForWeb();
                //if (userDetails != null)
                //{

                var contestContent = new StringContent(JsonConvert.SerializeObject(null), System.Text.Encoding.UTF8, "application/json");
                var data = await _httpClient.PostAsync(_urls.ProductManagement + ProductManagementOperations.DeleteAgreement(agreementId), contestContent);
                if (data.IsSuccessStatusCode)
                {
                    result = (data != null) ? JsonConvert.DeserializeObject<BaseResponseView>(data.Content.ReadAsStringAsync().Result) : null;
                }

                //}
                //else
                //{
                //    result.Message = "InValid User";
                //    result.Type = "Error";
                //    result.Code = Convert.ToInt32(ResponseCodeEnum.UnauthorizedUser);
                //}
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        //#region Agreement Details Get API
        //public async Task<ContentDetailsWebResponseView> GetAgreementDetailsById(int agreementId)
        //{
        //    ContentDetailsWebResponseView result = new ContentDetailsWebResponseView();
        //    try
        //    {
        //        //UserDetailView userDetails = await _commonService.GetUserByTokenForWeb();
        //        //if (userDetails != null)
        //        //{
        //        var contentResult = await _httpClient.GetAsync(_urls.ProductManagement + ProductManagementOperations.GetAgreementDetailsById(agreementId));
        //        if (contentResult.IsSuccessStatusCode && contentResult != null)
        //        {
        //            ContentDetailWebView contentData = (contentResult != null) ? JsonConvert.DeserializeObject<ContentDetailWebView>(contentResult.Content.ReadAsStringAsync().Result) : null;
        //            if (contentData != null && contentData.Type == "Success")
        //            {
        //                result.ContentId = contentData.ContentId;
        //                result.Title = contentData.Title;
        //                result.Description = contentData.Description;
        //                result.ContentCategoryIds = contentData.ContentCategoryIds;
        //                result.TimePeriodIds = contentData.TimePeriodIds;
        //                result.IncidentDate = contentData.IncidentDate;
        //                result.EraIncidentDate = contentData.EraIncidentDate;
        //                result.CreatedBy = contentData.CreatedBy;
        //                result.ContentOwnerId = contentData.ContentOwnerId;
        //                result.Status = contentData.Status;
        //                result.Remarks = contentData.Remarks;
        //            }

        //            result.Message = contentData.Message;
        //            result.Type = contentData.Type;
        //            result.Code = contentData.Code;
        //        }

        //        //}
        //        //else
        //        //{
        //        //    result.Message = "InValid User";
        //        //    result.Type = "Error";
        //        //    result.Code = Convert.ToInt32(ResponseCodeEnum.UnauthorizedUser);
        //        //}
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task<RecordsListResponseView<ContentListWebView>> GetAllAgreementList(RecordInputView<AgreementFilterInputView> recordInputView)
        //{
        //    RecordsListResponseView<ContentListWebView> contentListdata = new RecordsListResponseView<ContentListWebView>();
        //    RecordContentView<AgreementFilterInputView> recordFilterView = new RecordContentView<AgreementFilterInputView>();
        //    recordFilterView.Records = new AgreementFilterInputView();
        //    try
        //    {
        //        //UserDetailView userDetails = await _commonService.GetUserByTokenForWeb();
        //        //if (userDetails != null)
        //        //{
        //        recordFilterView.Records.PageSize = recordInputView.Records.PageSize;
        //        recordFilterView.Records.PageIndex = recordInputView.Records.PageIndex;
        //        recordFilterView.Records.SearchText = recordInputView.Records.SearchText;

        //        var contentData = new StringContent(JsonConvert.SerializeObject(recordFilterView), System.Text.Encoding.UTF8, "application/json");
        //        var data = await _httpClient.PostAsync(_urls.ProductManagement + ProductManagementOperations.GetAllAgreementList(), contentData);
        //        if (data.IsSuccessStatusCode)
        //        {
        //            RecordsListResponseView<ContentListWebView> contentResult = (data != null) ? JsonConvert.DeserializeObject<RecordsListResponseView<ContentListWebView>>(data.Content.ReadAsStringAsync().Result) : null;
        //            if (contentResult != null && contentResult.RecordsList != null && contentResult.RecordsList.Count > 0)
        //            {
        //                var userInfo = await userListInfo;
        //                if (userInfo != null)
        //                {
        //                    contentListdata.RecordsList = (from contentList in contentResult.RecordsList
        //                                                   join users in userInfo on contentList.ContentOwnerId equals users.MemberId
        //                                                   select new ContentListWebView
        //                                                   {
        //                                                       ContentId = contentList.ContentId,
        //                                                       Title = contentList.Title,
        //                                                       Description = contentList.Description,
        //                                                       CategoryName = contentList.CategoryName,
        //                                                       TotalLikes = contentList.TotalLikes,
        //                                                       TotalComments = contentList.TotalComments,
        //                                                       TotalViews = contentList.TotalViews,
        //                                                       TotalShares = contentList.TotalShares,
        //                                                       IsAbuse = contentList.IsAbuse,
        //                                                       Status = contentList.Status,
        //                                                       CreatedBy = contentList.CreatedBy,
        //                                                       CreatedDate = contentList.CreatedDate,
        //                                                       ContentOwnerId = contentList.ContentOwnerId,
        //                                                       CreatorName = !string.IsNullOrEmpty(users.MiddleName) ? users.FirstName + ' ' + users.MiddleName + ' ' + users.LastName : users.FirstName + ' ' + users.LastName,
        //                                                       CreatorEmailId = users.Email,
        //                                                       ContactNumber = users.ContactNumber,
        //                                                       CountryCode = users.CountryCode,
        //                                                       GroupId = contentList.GroupId,
                                                               
        //                                                       TotalMedia = (contentList.MediaContents != null && contentList.MediaContents.Count > 0) ? contentList.MediaContents.Count() : 0
        //                                                   }).ToList();

        //                }
        //            }
        //            contentListdata.TotalRecordCount = contentResult.TotalRecordCount;
        //            contentListdata.Message = contentResult.Message;
        //            contentListdata.Type = contentResult.Type;
        //            contentListdata.Code = contentResult.Code;
        //        }

        //        //}
        //        //else
        //        //{
        //        //    contentListdata.Message = "InValid User";
        //        //    contentListdata.Type = "Error";
        //        //    contentListdata.Code = Convert.ToInt32(ResponseCodeEnum.UnauthorizedUser);
        //        //}
        //        return contentListdata;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //#endregion

    }
}
