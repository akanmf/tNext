using System;

namespace tNext.Common.Core.Model
{
    [Serializable]
    public class PagingModel
    {
        public int PageNumber { get; set; }
        public int StartIndex { get; set; }
        public int RowCount { get; set; }
    }
}
