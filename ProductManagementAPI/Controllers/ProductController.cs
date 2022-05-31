using Common.ViewModels.CommonModel;
using Common.ViewModels.AgreementModel;
using Common.ViewModels.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.ServiceWorker.IProductManagementServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        #region Variables

        private readonly IProductServices _productServices;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        #endregion

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
                lstProductGroup = await _productServices.GetAllProductGroup();
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
                lstProducts = await _productServices.GetAllProductByGroupId(productGroupId);
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
        public async Task<IActionResult> SaveAgreement([FromBody] RecordContentView<AddAgreementInputView> recordContentView)
        {
            return await Execute(async () =>
            {
                var response = await _productServices.SaveAgreement(recordContentView);
                return Ok(response);
            });
        }

        [Route("EditAgreement")]
        [HttpPost]
        /// <summary>
        /// Edit Agreement
        /// </summary>
        /// <returns>BaseResponseView</returns>
        public async Task<IActionResult> EditAgreement([FromBody] RecordContentView<EditAgreementInputView> recordContentView)
        {
            return await Execute(async () =>
            {
                var response = await _productServices.EditAgreement(recordContentView);
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
                var response = await _productServices.DeleteAgreement(agreementId);
                return Ok(response);
            });
        }

        #endregion

        //#region Content Details Get API

        //[Route("GetContentDetailsById")]
        //[HttpPost]
        ///// <summary>
        ///// Get ContentDetails By Id
        ///// </summary>
        ///// <param name="contentId"></param>
        ///// <returns></returns>
        //public async Task<IActionResult> GetContentDetailsById([FromBody] RecordContentView<ContentDetailInputView> recordContentView)
        //{
        //    return await Execute(async () =>
        //    {
        //        var contentData = await _contentServices.GetContentDetailsById(recordContentView.Records.ContentId, recordContentView.Records.ContentTypeId, recordContentView.Records.BlockedMemberIds, recordContentView.MemberId, Convert.ToInt64(recordContentView.ContentOwnerId));
        //        return Ok(contentData);
        //    });
        //}

        //[Route("GetAllContents")]
        //[HttpPost]
        ///// <summary>
        /////  Get all Contents
        ///// </summary>
        ///// <returns></returns>
        //public async Task<IActionResult> GetAllContents([FromBody] RecordContentView<ContentFilterView> contentFilterView)
        //{
        //    return await Execute(async () =>
        //    {
        //        var result = await _contentServices.GetAllContents(contentFilterView);
        //        return Ok(result);
        //    });
        //}

        //#endregion

    }
}
