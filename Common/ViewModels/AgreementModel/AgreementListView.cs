using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels.AgreementModel
{
    public class AgreementListView
    {
        public int AgreementId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ProductGroupId { get; set; }
        public string GroupDescription { get; set; }
        public string GroupCode { get; set; }
        public int ProductId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal NewPrice { get; set; }
    }
}
