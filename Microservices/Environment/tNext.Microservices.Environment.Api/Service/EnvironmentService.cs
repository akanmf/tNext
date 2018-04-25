using System.Collections.Generic;
using tNext.Common.Core.Helpers;
using tNext.Common.Model.Enums;
using tNext.Microservices.Environment.Api.Models;

namespace tNext.Microservices.Environment.Api.Service
{
    public class EnvironmentService
    {
        public List<OrderType> GetOrderTypes(OrderTypePages orderType)
        {
            var orderList = new List<OrderType>();

            var orderTypes = ParameterHelper.GetParameter(orderType.ToString(), string.Empty);
            if (orderTypes != null && orderTypes.Count > 0)
            {
                foreach (var item in orderTypes)
                {
                    var ot = new OrderType();
                    ot.ID = item.Value;
                    ot.Name = item.Key;
                    orderList.Add(ot);
                }
            }
            return orderList;
        }
    }
}