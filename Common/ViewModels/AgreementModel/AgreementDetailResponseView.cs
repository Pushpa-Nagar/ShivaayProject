using Common.ViewModels.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels.AgreementModel
{
    public class AgreementDetailResponseView : BaseResponseView
    {
        public int AgreementId { get; set; }
        public int ProductGroupId { get; set; }
        public string GroupCode { get; set; }
        public int ProductId { get; set; }
        public string ProductNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal NewPrice { get; set; }
        public bool Active { get; set; }
    }
}
