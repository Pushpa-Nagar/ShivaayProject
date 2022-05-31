using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels.AgreementModel
{
    public class AgreementFilterInputView
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SearchText { get; set; }
    }
}
