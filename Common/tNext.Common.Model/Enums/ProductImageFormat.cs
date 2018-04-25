using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tNext.Common.Model.Enums
{
    /// <summary>
    /// Bir ürünün sahip olabileceği resim formatlarını ifade eder.
    /// </summary>
    public enum ProductImageFormat
    {
        UnSet = 0,

        /// <summary>
        /// Ana sayfada & ürün kategori sayfalarında product box gösterilen noktalarda kullanılan resim formatıdır.
        /// </summary>
        ProductBox = 1,

        /// <summary>
        /// Ana sayfada yatay olarak geniş olan product box için kullanılan resim formatıdır.
        /// </summary>
        ProductBoxWide = 2,

        /// <summary>
        /// Ana sayfada dikey olan product box için kullanılan resim formatıdır.
        /// </summary>
        ProductBoxTall = 3,

        /// <summary>
        /// Ana sayfada 4 tane normal product box büyüklüğünde olan product box için kullanılan resim formatıdır.
        /// </summary>
        ProductBoxDouble = 4,

        /// <summary>
        /// Kategori ekranında ürün karşılaştırma sepetinde kullanılan ufak resim formatıdır.
        /// </summary>
        ComparisonBasketThumbnail = 5,

        /// <summary>
        /// Sık ziyaret edilenler listelerinde kullanılan resim formatıdır.
        /// </summary>
        ProductListing = 6,

        /// <summary>
        /// Sitede en tepede arama kutusunda otomatik tamamlama alanında kullanılan resim formatıdır.
        /// </summary>
        AutoCompleteThumbnail = 8,

        /// <summary>
        /// Ürün Detay sayfasında büyük resim.
        /// </summary>
        ProductDetailBig = 9,

        /// <summary>
        /// Ürün Detay Sayfasında zoom yapılacak olan resim.
        /// </summary>
        ProductDetailZoom = 10,

        /// <summary>
        /// Ürün Detay Sayfasında Küçük Resim
        /// </summary>
        ProductDetailSmall = 11,

        /// <summary>
        /// Ürün Detay sayfasında orta boyutta olan resim.
        /// </summary>
        ProductDetailMedium = 12,

        /// <summary>
        /// Aksesuarların listelendiği sayfada 
        /// </summary>
        ProductDetailAccessory = 13,

        /// <summary>
        /// Ürün sepete eklendiğinde lightbox da önerilen resim.
        /// </summary>
        RecommendCampaignProduct = 14
    }
}
