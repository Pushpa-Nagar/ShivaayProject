using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels.AgreementModel
{
    public class AgreementFilterInputView
    {
        public int PageSize { get; set; }
        public int SkipRecord { get; set; }
        public string SearchText { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}
