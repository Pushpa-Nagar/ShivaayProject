using System;

namespace Common.ViewModels.CommonModel
{
    public class RecordContentView<T>
    {
        public long UserId { get; set; }
        public T Records { get; set; }
    }
}
