using System;

namespace Common.ViewModels.ResponseModel
{
    public class BaseResponseView
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public int Code { get; set; }
    }
}
