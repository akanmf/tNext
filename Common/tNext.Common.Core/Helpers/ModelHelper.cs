using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Model;

namespace tNext.Common.Core.Helpers
{
    public class ModelHelper
    {
        public static ListWithPaging<T> GetResponseWithPaging<T>(List<T> list, int defaultRowCount = int.MaxValue)
        {

            var PagingModel = UriHelper.GetPagingModel(defaultRowCount);

            if (PagingModel.RowCount == int.MaxValue || PagingModel.RowCount <= 0)
            {
                PagingModel.RowCount = list.Count;
            }

            if (PagingModel.StartIndex <= 0)
            {
                PagingModel.StartIndex = PagingModel.PageNumber * PagingModel.RowCount;
            }

            var response = new ListWithPaging<T>();
            response.TotalRowCount = list.Count();

            response.CurrentPageRows = list.Skip(PagingModel.StartIndex).Take(PagingModel.RowCount).ToList();

            return response;
        }
    }
}
