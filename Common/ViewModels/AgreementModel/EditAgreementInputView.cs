using System;
using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.AgreementModel
{
    public class EditAgreementInputView
    {
        [Required]
        public int AgreementId { get; set; }
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "EffectiveDate is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; }

        [Required(ErrorMessage = "ExpirationDate is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        public decimal NewPrice { get; set; }
    }
}
