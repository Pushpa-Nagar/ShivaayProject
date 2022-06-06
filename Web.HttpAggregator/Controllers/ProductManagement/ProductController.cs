using Common.ViewModels.CommonModel;
using Common.ViewModels.AgreementModel;
using Common.ViewModels.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.HttpAggregator.Services.ProductManagement.IProductManagementServices;

namespace Web.HttpAggregator.Controllers.ProductManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        #region Variables

        private readonly IProductManagementService _productManagementService;
        private readonly ILogger<ProductController> _logger;

        #endregion

        #region Constructor
        public ProductController(IProductManagementService productManagementService, ILogger<ProductController> logger)
        {
            _productManagementService = productManagementService;
            _logger = logger;
        }

        #endregion

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        #region Master APIs

        [Route("GetAllProductGroup")]
        [HttpGet]
        /// <summary>
        /// Get All ProductGroups
        /// </summary>
        /// <param name=""></param>
        /// <returns>ProductGroups</returns>
        public async Task<IActionResult> GetAllProductGroup()
        {
            return await Execute(async () =>
            {
                RecordsListResponseView<SelectionRecordView> lstProductGroup = new RecordsListResponseView<SelectionRecordView>();
                lstProductGroup = await _productManagementService.GetAllProductGroup();
                return Ok(lstProductGroup);
            });
        }

        [Route("GetAllProductByGroupId")]
        [HttpGet]
        /// <summary>
        /// Get All Products By GroupId
        /// </summary>
        /// <param name=""></param>
        /// <returns>Products Details</returns>
        public async Task<IActionResult> GetAllProductByGroupId([FromQuery] int productGroupId)
        {
            return await Execute(async () =>
            {
                RecordsListResponseView<SelectionRecordView> lstProducts = new RecordsListResponseView<SelectionRecordView>();
                lstProducts = await _productManagementService.GetAllProductByGroupId(productGroupId);
                return Ok(lstProducts);
            });
        }

        #endregion

        #region Agreement Crud Operations

        [Route("SaveAgreement")]
        [HttpPost]
        /// <summary>
        /// Save Agreement
        /// </summary>
        /// <returns>BaseResponseView</returns>
        public async Task<IActionResult> SaveAgreement([FromBody] RecordInputView<AddAgreementInputView> recordInputView)
        {
            return await Execute(async () =>
            {
                var response = await _productManagementService.SaveAgreement(recordInputView);
                return Ok(response);
            });
        }

        [Route("EditAgreement")]
        [HttpPost]
        /// <summary>
        /// Edit Agreement
        /// </summary>
        /// <returns>BaseResponseView</returns>
        public async Task<IActionResult> EditAgreement([FromBody] RecordInputView<EditAgreementInputView> recordInputView)
        {
            return await Execute(async () =>
            {
                var response = await _productManagementService.EditAgreement(recordInputView);
                return Ok(response);
            });
        }

        [Route("DeleteAgreement")]
        [HttpPost]
        /// <summary>
        /// Delete Agreement
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns>BaseResponseView</returns>
        public async Task<IActionResult> DeleteAgreement(int agreementId)
        {
            return await Execute(async () =>
            {
                var response = await _productManagementService.DeleteAgreement(agreementId);
                return Ok(response);
            });
        }

        #endregion

        #region Agreement Details Get API

        [Route("GetAgreementDetailsById")]
        [HttpGet]
        /// <summary>
        /// Get Agreement Details By Id
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetAgreementDetailsById([FromQuery] int agreementId)
        {
            return await Execute(async () =>
            {
                var agreementData = await _productManagementService.GetAgreementDetailsById(agreementId);
                return Ok(agreementData);
            });
        }

        [Route("GetAllAgreementList")]
        [HttpPost]
        /// <summary>
        ///  Get all Agreements
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllAgreementList([FromBody] RecordInputView<AgreementFilterInputView> recordInputView)
        {
            return await Execute(async () =>
            {
                var allAgreementData = await _productManagementService.GetAllAgreementList(recordInputView);
                return Ok(allAgreementData);
            });
        }

        #endregion

    }
}
