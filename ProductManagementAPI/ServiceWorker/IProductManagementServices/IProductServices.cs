using Common.ViewModels.CommonModel;
using Common.ViewModels.AgreementModel;
using Common.ViewModels.ResponseModel;
using System;
using System.Threading.Tasks;

namespace ProductManagementAPI.ServiceWorker.IProductManagementServices
{
    public interface IProductServices
    {
        #region Master APIs
        Task<RecordsListResponseView<SelectionRecordView>> GetAllProductGroup();
        Task<RecordsListResponseView<SelectionRecordView>> GetAllProductByGroupId(int productGroupId);

        #endregion

        #region
        Task<BaseResponseView> SaveAgreement(RecordContentView<AddAgreementInputView> recordContentView);
        Task<BaseResponseView> EditAgreement(RecordContentView<EditAgreementInputView> recordContentView);
        Task<BaseResponseView> DeleteAgreement(int agreementId);

        #endregion

        #region
        Task<AgreementDetailResponseView> GetAgreementDetailsById(int agreementId);
        Task<RecordsListResponseView<AgreementListView>> GetAllAgreementList(RecordContentView<AgreementFilterInputView> recordContentView);
        #endregion
    }
}
