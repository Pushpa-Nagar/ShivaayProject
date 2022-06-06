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
                recordContentView.Records.ProductGroupId = recordInputView.Records.ProductGroupId;
                recordContentView.Records.ProductId = recordInputView.Records.ProductId;
                recordContentView.Records.EffectiveDate = recordInputView.Records.EffectiveDate;
                recordContentView.Records.ExpirationDate = recordInputView.Records.ExpirationDate;
                recordContentView.Records.NewPrice = recordInputView.Records.NewPrice;
                recordContentView.Records.UserId = recordInputView.Records.UserId;
                recordContentView.Records.UserName = recordInputView.Records.UserName;
                recordContentView.Records.Active = recordInputView.Records.Active;

                var contentData = new StringContent(JsonConvert.SerializeObject(recordContentView), System.Text.Encoding.UTF8, "application/json");
                var data = await _httpClient.PostAsync(_urls.ProductManagement + ProductManagementOperations.SaveAgreement(), contentData);
                if (data.IsSuccessStatusCode)
                {
                    result = (data != null) ? JsonConvert.DeserializeObject<BaseResponseView>(data.Content.ReadAsStringAsync().Result) : null;
                }
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
                recordContentView.Records.AgreementId = recordInputView.Records.AgreementId;
                recordContentView.Records.ProductGroupId = recordInputView.Records.ProductGroupId;
                recordContentView.Records.ProductId = recordInputView.Records.ProductId;
                recordContentView.Records.EffectiveDate = recordInputView.Records.EffectiveDate;
                recordContentView.Records.ExpirationDate = recordInputView.Records.ExpirationDate;
                recordContentView.Records.NewPrice = recordInputView.Records.NewPrice;
                recordContentView.Records.Active = recordInputView.Records.Active;

                var contentData = new StringContent(JsonConvert.SerializeObject(recordContentView), System.Text.Encoding.UTF8, "application/json");
                var data = await _httpClient.PostAsync(_urls.ProductManagement + ProductManagementOperations.EditAgreement(), contentData);
                if (data.IsSuccessStatusCode)
                {
                    result = (data != null) ? JsonConvert.DeserializeObject<BaseResponseView>(data.Content.ReadAsStringAsync().Result) : null;
                }
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
                var contestContent = new StringContent(JsonConvert.SerializeObject(null), System.Text.Encoding.UTF8, "application/json");
                var data = await _httpClient.PostAsync(_urls.ProductManagement + ProductManagementOperations.DeleteAgreement(agreementId), contestContent);
                if (data.IsSuccessStatusCode)
                {
                    result = (data != null) ? JsonConvert.DeserializeObject<BaseResponseView>(data.Content.ReadAsStringAsync().Result) : null;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Agreement Details Get API
        public async Task<AgreementDetailResponseView> GetAgreementDetailsById(int agreementId)
        {
            AgreementDetailResponseView result = new AgreementDetailResponseView();
            try
            {
                var data = await _httpClient.GetAsync(_urls.ProductManagement + ProductManagementOperations.GetAgreementDetailsById(agreementId));               
                if (data.IsSuccessStatusCode)
                {
                    result = (data != null) ? JsonConvert.DeserializeObject<AgreementDetailResponseView>(data.Content.ReadAsStringAsync().Result) : null;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RecordsListResponseView<AgreementListView>> GetAllAgreementList(RecordInputView<AgreementFilterInputView> recordInputView)
        {
            RecordsListResponseView<AgreementListView> lstAgreementDetails = new RecordsListResponseView<AgreementListView>();
            RecordContentView<AgreementFilterInputView> recordFilterView = new RecordContentView<AgreementFilterInputView>();
            recordFilterView.Records = new AgreementFilterInputView();
            try
            {
                recordFilterView.Records.PageSize = recordInputView.Records.PageSize;
                recordFilterView.Records.SkipRecord = recordInputView.Records.SkipRecord;
                recordFilterView.Records.SearchText = recordInputView.Records.SearchText;
                recordFilterView.Records.SortColumn = recordInputView.Records.SortColumn;
                recordFilterView.Records.SortOrder = recordInputView.Records.SortOrder;

                var productData = new StringContent(JsonConvert.SerializeObject(recordFilterView), System.Text.Encoding.UTF8, "application/json");
                var data = await _httpClient.PostAsync(_urls.ProductManagement + ProductManagementOperations.GetAllAgreementList(), productData);
                if (data.IsSuccessStatusCode)
                {
                    lstAgreementDetails = (data != null) ? JsonConvert.DeserializeObject<RecordsListResponseView<AgreementListView>> (data.Content.ReadAsStringAsync().Result) : null;
                }
                return lstAgreementDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
