using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Model.Abstracts;

namespace tNext.Common.Model.Errors
{
    public class ProductNotFoundError : tNextErrorBase
    {
        public ProductNotFoundError(string productId)
            : base("PRODUCT_NOT_FOUND", $"{productId} Id li ürün bulunamadı", "Aradığınız ürün bulanamadı")
        {

        }
    }
}
