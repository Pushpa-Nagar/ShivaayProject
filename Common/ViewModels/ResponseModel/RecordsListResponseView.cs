using System;
using System.Collections.Generic;

namespace Common.ViewModels.ResponseModel
{
    public class RecordsListResponseView<T> : BaseResponseView
    {
        public int TotalRecordCount { get; set; }
        public List<T> RecordsList { get; set; }
    }
}
