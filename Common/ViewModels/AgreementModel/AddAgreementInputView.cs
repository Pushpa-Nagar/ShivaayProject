using System;
using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.AgreementModel
{
    public class AddAgreementInputView
    {
        [Required(ErrorMessage = "Product Group is Required")]
        public int ProductGroupId { get; set; }

        [Required(ErrorMessage = "Product is Required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "EffectiveDate is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; }

        [Required(ErrorMessage = "ExpirationDate is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "New Price is Required")]
        public decimal NewPrice { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
