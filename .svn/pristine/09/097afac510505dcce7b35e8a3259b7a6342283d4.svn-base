using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using tNext.Common.Core.Helpers;
using tNext.Microservices.Order.Api.Model;
using tNext.Microservices.Store.Api.Models;

namespace tNext.Microservices.Order.Api.Service
{
    public class OrderService
    {
        public List<XXWW_SIPARIS_KARGO_BILGILER_V> getCargoInfo(string siparisno)
        {
            var conStr = ConfigurationHelper.GetConfiguration("TeknosaServicesDB");
            Database db = new SqlDatabase(conStr);
            var command = db.GetStoredProcCommand("[dbo].[tNext_sp_GetCargoStatus]");
            db.AddInParameter(command, "@ordernumber", DbType.String, siparisno);
            List<XXWW_SIPARIS_KARGO_BILGILER_V> resp = new List<XXWW_SIPARIS_KARGO_BILGILER_V>();
            var ds = db.ExecuteDataSet(command);

            if (ds != null && ds.Tables.Count > 0)
            {
                resp = ds.Tables[0].CreateListFromTable<XXWW_SIPARIS_KARGO_BILGILER_V>();
                if (resp != null && resp.Count > 0)
                    foreach (var item in resp)
                    {
                        resp[resp.IndexOf(item)].URUN_KODU = item.URUN_KODU.Replace(".", string.Empty);
                    }
            }
            return resp;
        }
        public tNext_sp_LN_GetPurchaseOrderV2 getPurchaseOrderV2(Guid OrderGroupId)
        {
            tNext_sp_LN_GetPurchaseOrderV2 response = new tNext_sp_LN_GetPurchaseOrderV2();
            var conStr = ConfigurationHelper.GetConfiguration("TeknosaTransactionServicesDB");
            Database db = new SqlDatabase(conStr);
            var command = db.GetStoredProcCommand("[dbo].[tNext_sp_LN_GetPurchaseOrderV2]");
            db.AddInParameter(command, "@OrderGroupId", DbType.Guid, OrderGroupId);

            var ds = db.ExecuteDataSet(command);
            response.purchaseOrdersList = ds.Tables[0].CreateListFromTable<LN_PurchaseOrders>();
            response.lineItemsList = ds.Tables[1].CreateListFromTable<LN_LineItems>();
            response.discountsAppliedList = ds.Tables[2].CreateListFromTable<LN_DiscountsApplied>();
            response.orderAddressesList = ds.Tables[3].CreateListFromTable<LN_OrderAddresses>();
            response.shipmentsList = ds.Tables[4].CreateListFromTable<LN_Shipments>();

            return response;
        }

        public List<tNext_sp_getTeknosaRaporAlanlari> getTeknosaRaporAlanları(string trackingNumber)
        {

            var conStr = ConfigurationHelper.GetConfiguration("TeknosaTransactionServicesDB");
            Database db = new SqlDatabase(conStr);
            var command = db.GetStoredProcCommand("[dbo].[tNext_sp_getTeknosaRaporAlanlari]");
            db.AddInParameter(command, "@trackingNumber", DbType.String, trackingNumber);

            var ds = db.ExecuteDataSet(command);
            var resp = ds.Tables[0].CreateListFromTable<tNext_sp_getTeknosaRaporAlanlari>();
            return resp;

        }

        public tNext_CC_sp_GetStoreInformation GetStoreInformations(string StoreId)
        {
            tNext_CC_sp_GetStoreInformation response = new tNext_CC_sp_GetStoreInformation();
            var conStr = ConfigurationHelper.GetConfiguration("TeknosaServicesDB");
            Database db = new SqlDatabase(conStr);
            var command = db.GetStoredProcCommand("[dbo].[tNext_CC_sp_GetStoreInformation]");
            db.AddInParameter(command, "@StoreId", DbType.String, StoreId);

            var ds = db.ExecuteDataSet(command);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                response = ds.Tables[0].Rows[0].CreateItemFromRow<tNext_CC_sp_GetStoreInformation>();

            return response;
        }

        public OrderContract GetOrderContractByOrderID(string OrderID)
        {
            OrderContract response = new OrderContract();

            var conStr = ConfigurationHelper.GetConfiguration("TeknosaServicesDB");
            Database db = new SqlDatabase(conStr);
            var command = db.GetStoredProcCommand("[dbo].[tNext_sp_GetOrderContractByOrderID]");
            db.AddInParameter(command, "@OrderId", DbType.String, OrderID);

            var ds = db.ExecuteDataSet(command);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                response = ds.Tables[0].Rows[0].CreateItemFromRow<OrderContract>();

            return response;
        }
    }
}