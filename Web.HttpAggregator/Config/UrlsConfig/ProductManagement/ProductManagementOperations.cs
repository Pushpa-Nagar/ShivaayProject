using System;

namespace Web.HttpAggregator.Config.UrlsConfig.ProductManagement
{
    public class ProductManagementOperations
    {
        #region Master APIs

        public static string GetAllProductGroup() => $"/api/product/getallproductgroup";
        public static string GetAllProductByGroupId(int productGroupId) => $"/api/product/getallproductbygroupId?productGroupId=" + productGroupId;
        
        #endregion

        #region Content Crud Operations
        public static string SaveAgreement() => $"/api/product/saveagreement";
        public static string EditAgreement() => $"/api/product/editagreement";
        public static string DeleteAgreement(long agreementId) => $"/api/product/deleteagreement?agreementId=" + agreementId;
        
        #endregion

        #region Content Get API
        public static string GetAgreementDetailsById(int agreementId) => $"/api/product/getagreementdetailsbyid?agreementId=" + agreementId;
        public static string GetAllAgreementList() => $"/api/product/getallagreementList";

        #endregion

    }
}
