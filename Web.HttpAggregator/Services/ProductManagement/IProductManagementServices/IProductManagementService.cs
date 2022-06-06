using Common.ViewModels.CommonModel;
using Common.ViewModels.AgreementModel;
using Common.ViewModels.ResponseModel;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Services.ProductManagement.IProductManagementServices
{
    public interface IProductManagementService
    {
        #region Master APIs
        Task<RecordsListResponseView<SelectionRecordView>> GetAllProductGroup();
        Task<RecordsListResponseView<SelectionRecordView>> GetAllProductByGroupId(int productGroupId);
        #endregion

        #region Agreement Crud Operations
        Task<BaseResponseView> SaveAgreement(RecordInputView<AddAgreementInputView> recordInputView);
        Task<BaseResponseView> EditAgreement(RecordInputView<EditAgreementInputView> recordInputView);
        Task<BaseResponseView> DeleteAgreement(int agreementId);

        #endregion

        #region Agreement Details Get API
        Task<AgreementDetailResponseView> GetAgreementDetailsById(int agreementId);
        Task<RecordsListResponseView<AgreementListView>> GetAllAgreementList(RecordInputView<AgreementFilterInputView> recordInputView);

        #endregion

    }
}
