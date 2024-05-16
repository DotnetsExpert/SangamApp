using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MLMPortal.Models
{
    public class HomeDashboardModel
    {
        public DataTable Table1 { get; set; }
        public DataTable DtIMg { get; set; }
        public DataTable CategoryList { get; set; }
        public DataTable SubCateList { get; set; }
        public DataTable dtoffer { get; set; }
        public string CustomerID { get; set; }
        public string ProdID { get; set; }
        public string ProdPrice { get; set; }
        public string Action { get; set; }
        public string totAmt { get; set; }
        public string CountItem { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public DataTable AddCartRes { get; set; }
        public string Quantity { get; set; }
        public string msg { get; set; }
        public string IPAddress { get; set; }
        public string FullName { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public string AddressType { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public DataTable AddAddressRes { get; set; }
        public string AddressID { get; set; }
        public DataTable AddressRes { get; set; }

        public string JSONData { get; set; }
        public string flag { get; set; }
        public string surl { get; set; }
        public string furl { get; set; }
        public string productinfo { get; set; }
        public string TransactionId { get; set; }

        public string mode { get; set; }
        public string status { get; set; }
        public string key { get; set; }
        public string BagAmount { get; set; }
        public DataTable TableP1 { get; set; }
        public DataTable TableP2 { get; set; }
        public DataTable TableP3 { get; set; }
        public DataTable Table2 { get; set; }
        public string ProductCP { get; set; }

        public string AreaId { get; set; }

    }
    public class UserReport
    {
        public DataTable Table1 { get; set; }
        public DataTable Table2 { get; set; }
        public DataTable Table3 { get; set; }
        public DataTable Table4 { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string Position { get; set; }
        public int TMemberCount { get; set; }
        public int TActiveMemberCount { get; set; }
        public int TDeActiveMemberCount { get; set; }

        public decimal TotalRewardIncome { get; set; }
        public decimal TotalSponcerIncome { get; set; }

        public string TxnId { get; set; }
        public string Amount { get; set; }
        public string status { get; set; }
        public string memberId { get; set; }
        public string Name { get; set; }
        public string ActiveDate { get; set; }
        public string PayMode { get; set; }
        public string DelAddress { get; set; }
        public string Password { get; set; }
        public string TranPassword { get; set; }
        public string PackageId { get; set; }
        public string Pin { get; set; }
        public List<SelectListItem> lstPackage { get; set; }
        public List<SelectListItem> lstPin { get; set; }
        public string JsonModel { get; set; }
        public string CreatedBy { get; set; }
        public string Role { get;  set; }
        public string Nature { get; set; }
        public string SmemberId { get;  set; }
        public string TopupAmt { get;  set; }
    }
    public class PinDetails
    {
        public string Pin { get; set; }
        public string PinNumber { get; set; }
    }






}