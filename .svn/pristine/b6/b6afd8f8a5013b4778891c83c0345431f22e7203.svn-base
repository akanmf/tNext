using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using tNext.Common.Core.Helpers;
using tNext.Common.Model.Enums;
using tNext.Microservices.Environment.Api.Enums;
using tNext.Microservices.Environment.Api.Models;

namespace tNext.Microservices.Environment.Api.Service
{
    public class BannerService
    {
        public List<tblBanner> GetBannersByCategoryIdAndApplicationType(BannerCategory category, ApplicationType appType)
        {
            var newBannerEnable = ConfigurationHelper.GetConfiguration("NewBannerEnable");
            var constr = ConfigurationHelper.GetConfiguration("TeknosaDeveloperConStr");
            Database db = new SqlDatabase(constr);
            var command = db.GetSqlStringCommand($@"SELECT * FROM dbo.tblBanner b WHERE 
                    b.IsActive=1
                    AND b.IsDeleted=0
                    AND b.BannerApplicationId IS NOT NULL
                    AND b.StatusId = {(int)SystemStatus.Published}
                    AND b.StartDate <= @Now
                    AND (b.EndDate IS NULL OR b.EndDate >=@Now )
                    AND (b.BannerCategoryId ) =  @BannerCategoryId
                    AND (b.BannerApplicationId & @BannerApplicationId)<>0");

            db.AddInParameter(command, "@BannerCategoryId", DbType.Int32, category);
            db.AddInParameter(command, "@BannerApplicationId", DbType.Int32, appType);
            db.AddInParameter(command, "@Now", DbType.DateTime, DateTime.Now);

            var dataset = db.ExecuteDataSet(command);

            var result = dataset.Tables[0].CreateListFromTable<tblBanner>();

            return result;
        }

        public List<AdvertisementsResponseItem> GetAdvertisementsByTypeForBannerWithArea(BannerCategory bannerCategory, ApplicationType appType)
        {
            var response = new List<AdvertisementsResponseItem>();

            var newBannerEnable = bool.Parse(ConfigurationHelper.GetConfiguration("NewBannerEnable"));


            var banners = GetBannersByCategoryIdAndApplicationType(bannerCategory, appType);

            foreach (var item in banners.OrderBy(o => o.DisplayOrder))
            {
                var a = new AdvertisementsResponseItem();
                a.ImagePath = string.Empty;
                switch (item.BannerMobileParameterType)
                {
                    case 1:
                        a.libParameterCode = "ToProduct";
                        item.BannerMobileParameterValue = item.BannerMobileParameterValue.Substring(item.BannerMobileParameterValue.Length - 9);
                        break;
                    case 2:
                        a.libParameterCode = "ToCatalog";
                        break;
                    case 3:
                        a.libParameterCode = "ToCategory";
                        break;
                    case 4:
                        a.libParameterCode = "ToSearch";
                        break;
                    case 5:
                        a.libParameterCode = "ToLink";
                        item.BannerMobileParameterValue = item.BannerUrl;
                        break;
                    default:
                        a.libParameterCode = string.Empty;
                        break;
                }

                a.mobileForwardParameter = item.BannerMobileParameterValue;

                switch (tNextExecutionContext.Current.RequestPlatform)
                {
                    case PlatformType.UNDEFINED:
                    case PlatformType.IPHONE:
                    case PlatformType.ANDROID:
                    case PlatformType.WINDOWS_PHONE_8:
                    case PlatformType.MOBILE_SITE:
                        if (!string.IsNullOrWhiteSpace(item.BannerMobileImage))
                            a.ImagePath = item.BannerMobileImage;
                        break;
                    case PlatformType.WEB:
                    case PlatformType.IPAD:
                    case PlatformType.ANDROID_TABLET:
                    case PlatformType.WINDOWS_8:
                        if (!string.IsNullOrWhiteSpace(item.BannerTabletImage))
                            a.ImagePath = item.BannerTabletImage;
                        break;
                }
                if (!a.ImagePath.StartsWith("http"))
                    a.ImagePath = "http:/" + a.ImagePath.Replace("//", "/");
                a.WebLink = item.BannerUrl;
                response.Add(a);
            }

            return response;

        }
    }
}