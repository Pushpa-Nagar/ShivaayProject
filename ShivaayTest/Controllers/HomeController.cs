using Common.ViewModels.AgreementModel;
using Common.ViewModels.CommonModel;
using Common.ViewModels.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using ShivaayTest.Areas.Identity.Data;
using ShivaayTest.Helpers;
using ShivaayTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShivaayTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        RestClient client { get; set; }
        private readonly ILogger<HomeController> _logger;
        private AppSettings _appSettings { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        UserData userDetail = new UserData();
        public HomeController(ILogger<HomeController> logger, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this._appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            client = new RestClient(this._appSettings.WebGatewayClient);
            //userDetail = GetCurrentUser();
        }

        public IActionResult Index()
        {
            userDetail = GetCurrentUser();
            return View();
        }

        public JsonResult GetAgreementList()
        {
            RecordsListResponseView<AgreementListView> lstAgreementDetails = new RecordsListResponseView<AgreementListView>();
            RecordInputView<AgreementFilterInputView> recordInputView = new RecordInputView<AgreementFilterInputView>();
            recordInputView.Records = new AgreementFilterInputView();

            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault(); 
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault(); 
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            recordInputView.Records.PageSize = length != null ? Convert.ToInt32(length) : 0;
            recordInputView.Records.SkipRecord = start != null ? Convert.ToInt32(start) : 0;
            recordInputView.Records.SearchText = searchValue;
            recordInputView.Records.SortColumn = sortColumn;
            recordInputView.Records.SortOrder = sortColumnDirection;

            var request = new RestRequest("/api/Product/GetAllAgreementList", Method.POST);
            request.AddJsonBody(recordInputView);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            if (!string.IsNullOrEmpty(content))
            {
                lstAgreementDetails = JsonConvert.DeserializeObject<RecordsListResponseView<AgreementListView>>(content);
            }
            var data = lstAgreementDetails.RecordsList;
            return Json(new { draw = draw, recordsFiltered = lstAgreementDetails.TotalRecordCount, recordsTotal = lstAgreementDetails.TotalRecordCount, data = data });
        }
        
        public ActionResult LoadaddAgreementPopup()
        {
            try
            {
                return PartialView("_AddAgreement");
            }
            catch (Exception ex)
            {
                return PartialView("_AddAgreement");
            }
        }

        public ActionResult LoadEditAgreementPopup(int agreementId)
        {
            AgreementDetailResponseView agreementDetails = new AgreementDetailResponseView();
            try
            {
                var request = new RestRequest("/api/Product/GetAgreementDetailsById", Method.GET);
                request.AddQueryParameter("agreementId", agreementId.ToString());
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                if (!string.IsNullOrEmpty(content))
                {
                    agreementDetails = JsonConvert.DeserializeObject<AgreementDetailResponseView>(content);
                }
                var data = agreementDetails;
                return PartialView("_UpdateAgreement", data);
            }
            catch (Exception ex)
            {
                return PartialView("_UpdateAgreement");
            }
        }

        public JsonResult AddAgreement(AddAgreementInputView recordView)
        {
            BaseResponseView result = new BaseResponseView();
            RecordInputView<AddAgreementInputView> recordInputView = new RecordInputView<AddAgreementInputView>();
            recordInputView.Records = new AddAgreementInputView();
            string status = string.Empty;
            try
            {
                userDetail = GetCurrentUser();
                recordInputView.Records.ProductGroupId = recordView.ProductGroupId;
                recordInputView.Records.ProductId = recordView.ProductId;
                recordInputView.Records.EffectiveDate = TimeZoneInfo.ConvertTimeFromUtc(recordView.EffectiveDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                recordInputView.Records.ExpirationDate = TimeZoneInfo.ConvertTimeFromUtc(recordView.ExpirationDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                recordInputView.Records.NewPrice = recordView.NewPrice;
                recordInputView.Records.UserId = userDetail.UserId;
                recordInputView.Records.UserName = userDetail.UserName;
                recordInputView.Records.Active = recordView.Active;

                var request = new RestRequest("/api/Product/SaveAgreement", Method.POST);
                request.AddJsonBody(recordInputView);
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                if (!string.IsNullOrEmpty(content))
                {
                    result = JsonConvert.DeserializeObject<BaseResponseView>(content);
                }
                var data = result;
                status = data.Message;
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return Json(status);
        }
        public JsonResult UpdateAgreement(EditAgreementInputView recordView)
        {
            BaseResponseView result = new BaseResponseView();
            RecordInputView<EditAgreementInputView> recordInputView = new RecordInputView<EditAgreementInputView>();
            recordInputView.Records = new EditAgreementInputView();
            string status = string.Empty;
            try
            {
                recordInputView.Records.AgreementId = recordView.AgreementId;
                recordInputView.Records.ProductGroupId = recordView.ProductGroupId;
                recordInputView.Records.ProductId = recordView.ProductId;
                recordInputView.Records.EffectiveDate = TimeZoneInfo.ConvertTimeFromUtc(recordView.EffectiveDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                recordInputView.Records.ExpirationDate = TimeZoneInfo.ConvertTimeFromUtc(recordView.ExpirationDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                recordInputView.Records.NewPrice = recordView.NewPrice;
                recordInputView.Records.Active = recordView.Active;

                var request = new RestRequest("/api/Product/EditAgreement", Method.POST);
                request.AddJsonBody(recordInputView);
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                if (!string.IsNullOrEmpty(content))
                {
                    result = JsonConvert.DeserializeObject<BaseResponseView>(content);
                }
                var data = result;
                status = data.Message;
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return Json(status);
        }
        public JsonResult DeleteAgreement(int agreementId)
        {
            BaseResponseView result = new BaseResponseView();
            string status = string.Empty;
            try
            {
                var request = new RestRequest("/api/Product/DeleteAgreement", Method.POST);
                request.AddQueryParameter("agreementId", agreementId.ToString());
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                if (!string.IsNullOrEmpty(content))
                {
                    result = JsonConvert.DeserializeObject<BaseResponseView>(content);
                }
                var data = result;
                status = data.Message;
            }
            catch (Exception ex)
            {
                status = ex.Message;

            }
            return Json(status);
        }

        public IActionResult GetAllProductGroup()
        {
            RecordsListResponseView<SelectionRecordView> lstProductGroup = new RecordsListResponseView<SelectionRecordView>();
            var request = new RestRequest("/api/Product/GetAllProductGroup", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            if (!string.IsNullOrEmpty(content))
            {
                lstProductGroup = JsonConvert.DeserializeObject<RecordsListResponseView<SelectionRecordView>>(content);
            }
            var data = lstProductGroup.RecordsList;
            return Json(data);
        }
        public IActionResult GetAllProductByGroupId(int productGroupId)
        {
            RecordsListResponseView<SelectionRecordView> lstProducts = new RecordsListResponseView<SelectionRecordView>();
            var request = new RestRequest("/api/Product/GetAllProductByGroupId", Method.GET);
            request.AddQueryParameter("productGroupId", productGroupId.ToString());
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            if (!string.IsNullOrEmpty(content))
            {
                lstProducts = JsonConvert.DeserializeObject<RecordsListResponseView<SelectionRecordView>>(content);
            }
            var data = lstProducts.RecordsList;
            return Json(data);
        }

        
        public UserData GetCurrentUser()
        {
            ApplicationUser usr = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
            userDetail.UserId = usr.Id;
            userDetail.UserName = usr.FirstName + ' ' + usr.LastName;
            return userDetail;
        }

       // private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
