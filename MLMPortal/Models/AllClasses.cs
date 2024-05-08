using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MLMPortal.Models
{
    public class AllClasses
    {
        public static string apicall(string url)
        {
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

                StreamReader sr = new StreamReader(httpres.GetResponseStream());

                string results = sr.ReadToEnd();

                sr.Close();
                return results;

            }
            catch
            {
                return "0";
            }
        }
        public static List<SelectListItem> BindDDL(DataTable dt, string value = null)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                lst.Add(new SelectListItem() { Text = "--select--", Value = "" });
                foreach (DataRow item in dt.Rows)
                {
                    if (Convert.ToString(item[0]) == value)
                    {
                        lst.Add(new SelectListItem()
                        {
                            Text = Convert.ToString(item[1]),
                            Value = Convert.ToString(item[0]),
                            Selected = true
                        });
                    }
                    else
                    {
                        lst.Add(new SelectListItem()
                        {
                            Text = Convert.ToString(item[1]),
                            Value = Convert.ToString(item[0])
                        });
                    }
                }
            }
            else
            {
                lst.Add(new SelectListItem() { Text = "--none--", Value = "" });
            }
            return lst;
        }

        public static List<SelectListItem> BindDDLDepositType()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            lst.Add(new SelectListItem() { Text = "All", Value = "0" });
            lst.Add(new SelectListItem() { Text = "Request Transfer", Value = "1" });
            lst.Add(new SelectListItem() { Text = "Credit Transfer", Value = "2" });
            lst.Add(new SelectListItem() { Text = "Debit Transfer", Value = "3" });

            return lst;
        }

        public static string ConvertTableToList(DataTable dt)
        {


            JavaScriptSerializer js = new JavaScriptSerializer();
            if (dt != null && dt.Rows.Count > 0)
            {


                Hashtable[] pr = new Hashtable[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Hashtable ch = new Hashtable();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string columnName = Convert.ToString(dt.Columns[j]).Replace("'", "").Replace('"', ' ');
                        string columnValue = Convert.ToString(dt.Rows[i][columnName]).Replace("'", "").Replace('"', ' '); ;
                        ch.Add(columnName, columnValue);
                    }
                    pr[i] = ch;
                }
                js.MaxJsonLength = 999999999;
                return js.Serialize(pr);
            }


            return "False";
        }
        public static string CreateTransactPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public static string ConvertDataTabletoString(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }


        public static string GetIP()
        {
            string IP = "";
            string ipAdd = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAdd))
            {
                ipAdd = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                IP = ipAdd;
            }
            else
            {
                IP = ipAdd;
            }
            return IP;
        }
    }
    //aditya
    #region aditya / Mohd Nadeem
    public class Master
    {
        public DataTable dt { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }

        public List<Master> Catgory = new List<Master>();

        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int KitCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string EmailId { get; set; }
        public string Website { get; set; }
        public string GSTNo { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string BankBranch { get; set; }
        public string BatchNo { get; set; }
        public string HSNCode { get; set; }
        public decimal DP { get; set; }
        public decimal BV { get; set; }

        public decimal MRP { get; set; }

        public decimal GST { get; set; }

        public decimal CGST { get; set; }

        public decimal SGST { get; set; }

        public decimal PurchaseRate { get; set; }
        public decimal SaleRate { get; set; }
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public HttpPostedFileBase ProductFile { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string NewsHeading { get; set; }
        public int PurchaseGst { get; set; }
        public int SaleGst { get; set; }
        public string NewsPrefer { get; set; }
        public string NewsFor { get; set; }
        public string EntryBy { get; set; }
        public string msg { get; set; }
        public int Action { get; set; }
        public string Type { get; set; }
        public string VideoFile { get; set; }
        public string VideoTitle { get; set; }
        public DataTable Catdt { get; set; }
        public int Videoid { get; set; }
        public int Entryid { get; set; }
        public string ImageTitle { get; set; }
        public decimal APP { get; set; }
        public decimal CP { get; set; }
        public decimal RP { get; set; }

        public decimal DiscountPer { get; set; }
        public bool Isoffer { get; set; }

        public string PinCode { get; set; }
        public string AreaName { get; set; }
        public string Level { get;  set; }
        public string Level1Per { get;  set; }
        public string LevelPer { get;  set; }
        public string PackageId { get;  set; }
    }
    public class Registration
    {
        public string NewDetails { get; set; }
        public DataTable News { get; set; }
        public DataTable Catdt { get; set; }
        public string ImageTitle { get; set; }
        public DataTable dt { get; set; }
        public List<Registration> State = new List<Registration>();
        public string SessionId { get; set; }
        public int CountryCode { get; set; }
        public int StateId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CenterId { get; set; }
        public string StateName { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
        public string Wallet { get; set; }
        public string InvoiceNo { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string MemberId { get; set; }
        public string RegDate { get; set; }
        public string Pass { get; set; }

        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public string HouseNo { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string TranPass { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string ConPass { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string ZipCode { get; set; }
        public string MobileNo { get; set; }
        public string PaymentDate { get; set; }
        public string DepositType { get; set; }
        public string EmailId { get; set; }
        public string District { get; set; }
        public string GSTNo { get; set; }
        public string SponserId { get; set; }
        public string New_SponserId { get; set; }
        public string Rate { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string SponserName { get; set; }
        public string Direction { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string BankBranch { get; set; }
        public string PanNo { get; set; }
        public string PhonePayNo { get; set; }
        public string AdharNo { get; set; }
        public string NomName { get; set; }
        public string NomFatherName { get; set; }
        public string NomAddressName { get; set; }
        public string NomRelationName { get; set; }
        public string EntryBy { get; set; }
        public string Remark { get; set; }
        public string LandMark { get; set; }
        public string KYCStatus { get; set; }
        public string msg { get; set; }
        public int Action { get; set; }
        public string ProfilePic { get; set; }

        public string Rank { get; set; }
        public string Distributor { get; set; }
        public string Package { get; set; }
        public string Status { get; set; }
        public string UpgradeStatus { get; set; }
        public string TotalWalletAmnt { get; set; }
        public string CurrentWalletAmnt { get; set; }
        public string hdnValue { get; set; }
        public string hdnValue2 { get; set; }
        public string treeData { get; set; }

        public string AllTeam { get; set; }
        public string LeftTeam { get; set; }
        public string RightTeam { get; set; }
        public string MyDirects { get; set; }
        public string SelfBV { get; set; }
        public string TeamBV { get; set; }
        public string FundWallet { get; set; }
        public string Current_LBV { get; set; }
        public string Current_RBV { get; set; }
        public string GridHtml { get; set; }
        public string Productpurchaseornot { get; set; }
        public string PackageType { get; set; }
        public string CP { get; set; }
        public List<SelectListItem> StateDDLLst = new List<SelectListItem>();
        public List<SelectListItem> DistrictDDLLst = new List<SelectListItem>();
        public List<SelectListItem> PhoneCodeLst = new List<SelectListItem>();
        public List<SelectListItem> RankLst = new List<SelectListItem>();
        public string UserId { get; set; }

        public string user_code { get; set; }
        public HttpPostedFileBase PAN { get; set; }

        public string PanFilePath { get; set; }
        public HttpPostedFileBase Adhar { get; set; }
        public string AdharFilePath { get; set; }
        public HttpPostedFileBase BankPassbook { get; set; }
        public string BankPassbookFilePath { get; set; }
        public HttpPostedFileBase Adharback { get; set; }

        public string AdharbackFilePath { get; set; }
        public string AddressId { get; set; }

        //16Jul2022
        public string TotalWithdraw { get; set; }
        public string LedgerBalance { get; set; }
        public string DirectIncome { get; set; }
        public string LevelIncome { get; set; }
        public string MatchingIncome { get; set; }
        public string ROIIncome { get; set; }
        public string AutoPoolIncome { get; set; }
        //16Jul2022

        public string RequestWalletAmt { get; set; }

        public string LeftTeamActive { get; set; }
        public string RightTeamActive { get; set; }

        public string Levelcount { get; set; }

        public string AvailableCP { get; set; }

        public string RepurchaseIncome { get; set; }

        public string DirectMember { get; set; }
        public string MatchingMember { get; set; }
        public string RewardName { get; set; }

        public DataTable dtoffer { get; set; }

        public string OfferDirectMember { get; set; }
        public string OfferMatchingMember { get; set; }
        public string ROIWallet { get;  set; }




        public string Performancebonus { get; set; } = "0";
        public string SuperPerformancebonus { get; set; } = "0";
        public string PerformerOfTheDayIncome { get; set; } = "0";
        public string BronzeIncome { get; set; } = "0";
        public string SilverIncome { get; set; } = "0";
        public string StarsilverIncome { get; set; } = "0";
        public string GoldIncome { get; set; } = "0";
        public string PlatinumIncome { get; set; } = "0";
        public string DiamondIncome { get; set; } = "0";
        public string AmbassadorIncome { get; set; } = "0";
        public string CrownAmbassadorIncome { get; set; } = "0";
    }
    public class Login
    {

        public string UserName { get; set; }
        public string Password { get; set; }

        public int Action { get; set; }
    }
    public class DipoRegistration
    {
        public DataTable dt { get; set; }
        public List<Registration> State = new List<Registration>();
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string entryid { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string MemberId { get; set; }
        public string Pass { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string GSTNo { get; set; }
        public string Gst { get; set; }
        public string TranPass { get; set; }
        public string PanNo { get; set; }
        public string AccName { get; set; }
        public string Branch { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string BankBranch { get; set; }
        public string EntryBy { get; set; }
        public string msg { get; set; }
        public int Action { get; set; }
    }
    public class FranRegistration
    {
        public DataTable dt { get; set; }
        public List<Registration> State = new List<Registration>();
        public string entryid { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string TinNo { get; set; }
        public string DipoCode { get; set; }
        public string DepositType { get; set; }

        public string FranchiseId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string MemberId { get; set; }
        public string Pass { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string GSTNo { get; set; }
        public string Gst { get; set; }
        public string TranPass { get; set; }
        public string PanNo { get; set; }
        public string AccName { get; set; }
        public string Branch { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string BankBranch { get; set; }
        public string EntryBy { get; set; }
        public string msg { get; set; }
        public int Action { get; set; }
        public string PinCode { get; set; }

        public string Franchise_Name { get; set; }

        #region Vinay Maurya
        #region Delvery Boy

        public string ContactNo { get; set; }
        public string AadharNo { get; set; }
       // public string Emailid { get; set; }
        public string userName { get; set; }
        public string VolunteerId { get; set; }
        public string SpCode { get; set; }  
        #endregion
        #endregion


    }
    public class PayoutReport
    {
        public string UseId { get; set; }
        public DataTable dt { get; set; }
        public DataTable dt1 { get; set; }
        public string Id { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string TransactionID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string RequestDate { get; set; }
        public string DepositType { get; set; }


        public string ApprovedDate { get; set; }
        public string msg { get; set; }
        public string UID { get; set; }
        public string Action { get; set; }
        public string Remark { get; set; }
        public string EntryBy { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
        public string TranferAmt { get; set; }
        public string InviceNo { get; set; }
        public string Direction { get; set; }
        public string OrderType { get; set; }
        public string AccountCode { get; set; }

        public string CourierName { get; set; }
        public string TrackingNo { get; set; }
        public string NoOfBox { get; set; }
        public string DispatchDate { get; set; }


        #region
        public string PinCode { get; set; }
        #endregion

        #region  Vinay
        public string Del_Address { get; set; }
        public string FullName { get; set; }
        public string Franchise_Name { get; set; }
        public string Delivered_Date { get; set; }
        public string JsonObject { get; set; }
        public string Level { get;  set; }
        #endregion

    }

    public class Payoutdetails
    {
       
        public DataTable dt { get; set; }
        
        public int Member_Id { get; set; }        
        public string Name { get; set; }
        public string CurrentWallet { get; set; }
        public string Autoincome { get; set; }
        public string newWallet { get; set; }
       
    }
    #endregion aditya


    #region rahul
    public class PropertyClass
    {

        public string TDS_Per { get; set; }
        public string TDS_Amt { get; set; }

        public string CountryId { get; set; }

        public List<PropertyClass> BundleList { get; set; }

        public static List<SelectListItem> BindDDL(DataTable dt)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                lst.Add(new SelectListItem() { Text = "--Select--", Value = "" });
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add(new SelectListItem()
                    {
                        Text = Convert.ToString(item[1]),
                        Value = Convert.ToString(item[0])
                    });
                }
            }
            else
            {
                lst.Add(new SelectListItem() { Text = "--none--", Value = "" });
            }
            return lst;
        }
        public string AccountCode { get; set; }
        public string AccountType { get; set; }
        public string billno { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string Action { get; set; }
        public string EntryBy { get; set; }
        public int Id { get; set; }
        public string MenuTitle { get; set; }
        public string Url { get; set; }
        public string iconClass { get; set; }
        public int Priority { get; set; }
        public string SubMenuTitle { get; set; }
        public int MainMenuId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public string HSNCode { get; set; }
        public decimal PuchaseRate { get; set; }
        public decimal MRP { get; set; }
        public decimal GSTPer { get; set; }
        public decimal CGSTPer { get; set; }
        public decimal SGSTPer { get; set; }
        public decimal PV { get; set; }
        public decimal BV { get; set; }



        public DateTime? MfgDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Rate { get; set; }
        public decimal Quantity { get; set; }
        public string mDate { get; set; }
        public string eDate { get; set; }
        public decimal PendingQuantity { get; set; }
        public decimal TrfQuantity { get; set; }
        public decimal CoupenAmount { get; set; }
        public string iscoupenapplied { get; set; }
        public decimal deliverycharges { get; set; }

        //---------- Parameters for SS

        public string OutLetCode { get; set; }
        public string SSCode { get; set; }
        public string SSName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string PinCode { get; set; }
        public string Address { get; set; }
        public string hidCreditDays { get; set; }
        public string Phone { get; set; }
        public string GSTExpiryDate { get; set; }


        public string CityName { get; set; }
        public string GstNo { get; set; }
        public string PanNo { get; set; }
        public string GstDoc { get; set; }
        public string PanDoc { get; set; }
        public string StCode { get; set; }
        public string TINNo { get; set; }
        public string accountno { get; set; }
        public string ifsccode { get; set; }
        public string branchname { get; set; }
        public string AccountName { get; set; }
        public string DeleteBy { get; set; }
        public string EmployeeClosed { get; set; }
        public string RenewalDate { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string ProductSpacification { get; set; }
        public string CompanyCode { get; set; }
        public string UOM { get; set; }
        public string OrderId { get; set; }
        public string PartyStateCode { get; set; }
        public string Mtype { get; set; }
        public string Status { get; set; }
        public string ActiveStatus { get; set; }
        public string StockistName { get; set; }
        public string ItemBarCode { get; set; }
        public decimal PurchaseRate_Bulk { get; set; }
        public decimal PurchaseRate_Loose { get; set; }
        public decimal SaleRate_Bulk { get; set; }
        public decimal SaleRate_Loose { get; set; }
        public decimal StorePrice { get; set; }
        public decimal OnlinePrice { get; set; }
        public string BulkUOM { get; set; }
        public string LooseUOM { get; set; }
        public decimal BulkUOMQty { get; set; }
        public string CustomerId { get; set; }
        public string CardType { get; set; }
        public string msg { get; set; }
        public string WalletBalance { get; set; }
        public string strId { get; set; }
        public string FatherName { get; set; }

        //-------- DashBoard Count Parameters
        public string TotStokist { get; set; }
        public string TotOutlets { get; set; }
        public string TotPurchase { get; set; }
        public string TotDemands { get; set; }
        public string todayPo { get; set; }
        public string todayPoAmt { get; set; }
        public string todaySaleAmt { get; set; }
        public string todaySaleInv { get; set; }
        public string WalletBal { get; set; }
        public string CashBackBalance { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string parent_groupcode { get; set; }
        public string close_to { get; set; }
        public string Relation { get; set; }

        public string OfferTitle { get; set; }
        public string OfferType { get; set; }
        public decimal OnPurchaseAmount { get; set; }
        public decimal CashBackAmount { get; set; }
        public DateTime ValidStartDate { get; set; }
        public DateTime ValidEndDate { get; set; }
        public decimal Points { get; set; }
        public decimal AmountPerPoint { get; set; }
        public HttpPostedFileWrapper multipleImages { get; set; }
        public string locality { get; set; }
        public string landmark { get; set; }
        public string altmobileno { get; set; }
        public string IsFreeItem { get; set; }
        public string FreeItemCode { get; set; }
        public string FreeQty { get; set; }
        public string PurchaseBy { get; set; }
        public string IsCashbackApplied { get; set; }
        public string OpenBal { get; set; }
        public string CloseBal { get; set; }
        public string CrBal { get; set; }
        public string DrBal { get; set; }

        public string narration { get; set; }
        public string AccCode { get; set; }
        public string AccName { get; set; }

        #region  Parameters for Purchase Order
        public string BranchCode { get; set; }
        public string SupplierAccCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ShipmentPref { get; set; }
        public string PurchaseFile { get; set; }
        public string DeliveryTo { get; set; }
        public string TermsCond { get; set; }
        public string notes { get; set; }
        public decimal DiscPer { get; set; }
        public decimal DiscAmt { get; set; }
        public decimal CgstAmt { get; set; }
        public decimal SgstAmt { get; set; }
        public decimal IgstAmt { get; set; }
        public decimal PayableAmt { get; set; }
        public decimal GrossPayable { get; set; }
        public decimal NetTotal { get; set; }
        public decimal Payablegst { get; set; }
        public decimal totalPAmt { get; set; }
        public decimal totalBAmt { get; set; }
        public string IsFrieghtInclude { get; set; }
        public decimal FrieghtCharges { get; set; }
        public string InvoiceNo { get; set; }
        public string EwayBillNo { get; set; }
        public decimal RoundOffAmt { get; set; }
        public string PayMode { get; set; }
        public string ChqNo { get; set; }
        public DateTime ChqDate { get; set; }
        public string BanKAccName { get; set; }
        public string BankTransId { get; set; }
        public decimal PaidAmount { get; set; }

        public string VoucherNo { get; set; }
        public string DueOn { get; set; }

        public string txnId { get; set; }
        public string Purchase_taxIncludeExclude { get; set; }
        public string Sale_taxIncludeExclude { get; set; }

        public string PaytmTransId { get; set; }
        public string RespoCode { get; set; }
        public string RespMsg { get; set; }
        public string GateWayname { get; set; }
        public string Bankname { get; set; }

        public string cdate { get; set; }
        public string txndate { get; set; }

        #endregion

        #region DayBookDashboard Proprty

        public string CashOpeningBal { get; set; }
        public string CashClosingBal { get; set; }
        public string CashRecived { get; set; }
        public string CashPayment { get; set; }

        public string BankOpeningBal { get; set; }
        public string BankClosingBal { get; set; }
        public string BankRecived { get; set; }
        public string BankPayment { get; set; }
        public string FinancialYear { get; set; }

        #endregion
        public DataTable dt { get; set; }
        public DataTable dt1 { get; set; }
        public DataTable dt2 { get; set; }
        public List<PropertyClass> poList = new List<PropertyClass>();

        public PropertyClass()
        {
            this.ItemList = new List<SelectListItem>();
        }
        public List<SelectListItem> ItemList { get; set; }
        public List<PropertyClass> customerList { get; set; }
        public string EntryDate { get; set; }
        public string InitialQty { get; set; }
        public string LowStock { get; set; }
        public string Servicetype { get; set; }
        public string CompanyLogo { get; set; }
        public string ChallanNo { get; set; }
        public string AadhaarNo { get; set; }
        public string InvoicePrefix { get; set; }
        public bool IsLatterPad { get; set; }
        public string notetype { get; set; }
        public DateTime notedate { get; set; }
        public string invoicetype { get; set; }
        public decimal totalcessamount { get; set; }
        public string Estatus { get; set; }
        public decimal TotalGST { get; set; }
        public decimal TotalGSTAmt { get; set; }
        public string Msg { get;  set; }
        public string SponsorId { get;  set; }
    }

    #endregion rahul

    public class Report
    {
        public DataTable dt { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string DepoCode { get; set; }
        public string DepoName { get; set; }
        public string FromDate { get; set; }
        public List<SelectListItem> ProductLst = new List<SelectListItem>();

    }
    public class KYCUpdate
    {

        public string MemberId { get; set; }
        public DataTable dt { get; set; }
        public HttpPostedFileBase PAN { get; set; }

        public string PanFilePath { get; set; }
        public HttpPostedFileBase Adhar { get; set; }
        public string AdharFilePath { get; set; }
        public HttpPostedFileBase BankPassbook { get; set; }
        public string BankPassbookFilePath { get; set; }
        public HttpPostedFileBase Adharback { get; set; }

        public string AdharbackFilePath { get; set; }

    }
    public class Support
    {

        public string MsgId { get; set; }
        public string MsgType { get; set; }
        public string MemberId { get; set; }
        public string ToMemberId { get; set; }
        public string From_MemberId { get; set; }
        public string From_MemberName { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> MsgTypeList = new List<SelectListItem>();
        public int Action { get; set; }
        public string CreatedBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataTable dt { get; set; }
        public DataTable dt_Dipo { get; set; }
        public DataTable dt_Franchise { get; set; }
        public DataTable dt_AdminReply { get; set; }

        public string UserType { get; set; }

    }

    #region [Gunja]

    public class Stucommon
    {
        public int Id { get; set; }
        public int TotalCount { get; set; }

        public string ClassId { get; set; }
        public string FeeHeadId { get; set; }
        public string Conveyance { get; set; }
        public string SessionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string aff_no { get; set; }
        public string filepath { get; set; }
        public string Action { get; set; }
        public string EntryBy { get; set; }
        public string Role { get; set; }
        public string strId { get; set; }
        public DataTable dtSession { get; set; }
        public DataTable dt { get; set; }
        public DataTable dt1 { get; set; }
        public DataTable dt2 { get; set; }

        public string classname { get; set; }
        public string Partyname { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string housename { get; set; }
        public string sectionname { get; set; }
        public string stopname { get; set; }

        public string admclass { get; set; }
        public string lastclass { get; set; }
        public string RecieptNo { get; set; }
        public string Rolename { get; set; }
        public string userlist { get; set; }

        public DataTable Dtmainlist { get; set; }
        public DataTable Dtsubidlist { get; set; }
        public DataTable DtThirdlist { get; set; }
        public string ConvertDataTabletoString(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
    }

    public class Itemsdata : Stucommon
    {
        public string flag { get; set; }

        public string Username { get; set; }
        public string UserType { get; set; }
        public string password { get; set; }
        public string userid { get; set; }
        public string itemname { get; set; }
        public string itemtype { get; set; }
        public string price { get; set; }
        public string opqty { get; set; }
        public string curqty { get; set; }
        public string Itemid { get; set; }
        public string mainmenuid { get; set; }
        public string Submenuid { get; set; }
        public string thirdid { get; set; }
        public string ActvStatus { get; set; }
        public string status { get; set; }


        public string billno { get; set; }
        public string partyno { get; set; }
        public string totalAmount { get; set; }
        public string discountamount { get; set; }
        public string netamount { get; set; }
        public string purchaseno { get; set; }
        public string quantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string Purchasedate { get; set; }
        public string voucherno { get; set; }
        public string Bundleid { get; set; }
        public string Bundlename { get; set; }
        public string Saledate { get; set; }
        public string regno { get; set; }
        public string saleno { get; set; }
        public string bundleQty { get; set; }
        public string delsaleno { get; set; }
        public List<ItemRow> ItemRowList { get; set; }

    }

    public class ItemRow
    {

        public string Itemid { get; set; }
        public string qty { get; set; }
        public string rate { get; set; }
        public string amount { get; set; }
    }





    public class MinimartRegistration
    {
        public DataTable dt { get; set; }
        public List<Registration> State = new List<Registration>();

        public string FranchiseId { get; set; }

        public string entryid { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string TinNo { get; set; }
        public string DipoCode { get; set; }
        public string DepositType { get; set; }


        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string MemberId { get; set; }
        public string Pass { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string GSTNo { get; set; }
        public string Gst { get; set; }
        public string TranPass { get; set; }
        public string PanNo { get; set; }
        public string AccName { get; set; }
        public string Branch { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string BankBranch { get; set; }
        public string EntryBy { get; set; }
        public string msg { get; set; }
        public int Action { get; set; }
        public string PinCode { get; set; }

        public string Minimart_Name { get; set; }

      
        public string ContactNo { get; set; }
        public string AadharNo { get; set; }
        // public string Emailid { get; set; }
        public string userName { get; set; }
        public string VolunteerId { get; set; }
        public string SpCode { get; set; }
       
        public string AreaId { get; set; }

    }



    #endregion



    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class ProductModel
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }

    }
    #region Divanshu Shakya
    public class clsWr
    {
        public int Action { get; set; }
        public string EntryBy { get; set; }
        public string Dipo_Code { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public bool Status { get; set; }
        public string msg { get; set; }
        public int Id { get; set; }
        public string Bill_No { get; set; }
        public string username { get; set; }
    }

    #region Registration

    public class clsLg : clsWr
    {
        public string UserId { get; set; }
        public string Pswd { get; set; }

    }




    #endregion

    #region Dashboard

    public class clsDash : clsWr
    {


        public DataTable Catdt { get; set; }
        public string ImageTitle { get; set; }
        public DataTable dtNews { get; set; }
        public string TotalJoining { get; set; }
        public string TotalActiveMember { get; set; }
        public string TotalInactiveMember { get; set; }
        public string CustomerSale { get; set; }
        public string FranchiseSale { get; set; }
        public string DipoCommition { get; set; }
        public string WalletBal { get; set; }
        public string FundBal { get; set; }


        public string TotalWithdroll { get; set; }
        public string PendingWithdroll { get; set; }

        public string FranchiseIncome { get; set; }

        public string FranchiseApp { get; set; }

        public string FranchiseCP { get; set; }

        public string TotalIncome { get; set; }

        public string FranchiseSaleToday { get; set; }
        public string FranchiseSaleMonthly { get; set; }
        public string FranchiseSaleAll { get; set; }
        public string CompanyIncome { get; set; }
        #region Vinay 11-Feb-2023
        public string TotalPendingOrder { get; set; }
        public string TotalDeliveredOrder { get; set; }
        public string MonthlyPendingOrder { get; set; }
        public string MonthlyDeliveredOrder { get; set; }
        #endregion

        public string FranchisePendingAll { get; set; }

        public string FraIncfromminimart { get; set; }
    }

    #endregion

    #region Profile

    public class clsUser : clsWr
    {
        public string mobileno { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Addresss { get; set; }
        public string IfscCode { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string panno { get; set; }
        public string TinNo { get; set; }
        public string GSTNo { get; set; }
        public string BankBranch { get; set; }

    }

    #endregion

    #region Report

    #region package details

    public class cls_rpt_pack : clsWr
    {
        public DataTable dtPack { get; set; }
        public string p_Code { get; set; }
        public string p_name { get; set; }
        public string Hsn { get; set; }
        public decimal Amount { get; set; }

    }

    #endregion

    #endregion

    #region Product Request

    public class cls_pd_req : clsWr
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Req { get; set; }
        public string FundWallet { get; set; }
        public string Addr { get; set; }
        public string Rate { get; set; }
        public string PdList { get; set; }
        public DataTable dtProduct { get; set; }
        public string InvoiceNo { get; set; }
        public string BV { get; set; }
        public string PV { get; set; }
        public string txtReq { get; set; }
        public string CPAmount { get; set; }
        public List<SelectListItem> ReqLst = new List<SelectListItem>();

    }

    public class cls_Pd
    {
        public string ProductId { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }

    }


    #endregion

    #region Sales And Survices

    public class cls_sales : clsWr
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string InvNo { get; set; }
        public string MobileNo { get; set; }
        public string Addr { get; set; }
        public string StateId { get; set; }
        public string PdList { get; set; }
        public DataTable dtProduct { get; set; }
        public string Amount { get; set; }
        public string InvDetails { get; set; }
        public string PaymentType { get; set; }
        public string Wallet { get; set; }
        public string hdnMemberId { get; set; }
        public string Id_C { get; set; }
        public double Stock { get; set; }
        public string TxnID { get; set; }
        public string CPWallet { get; set; }
        public string BagAmount { get; set; }

    }

    public class cls_sl
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string BV { get; set; }

    }


    #endregion

    #region Retailer Stock

    public class cls_Ret : clsWr
    {
        public string MemberId { get; set; }
        public string Franchise_Id { get; set; }
        public string Address { get; set; }
        public string Statuss { get; set; }
        public string Name { get; set; }
        public string InvNo { get; set; }
        public string MobileNo { get; set; }
        public string Addr { get; set; }
        public string psw { get; set; }
        public string StateId { get; set; }
        public string PdList { get; set; }
        public DataTable dtProduct { get; set; }
        public string Amount { get; set; }
        public string InvDetails { get; set; }

        public string DemandNo { get; set; }
    }

    public class cls_slSt
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string BV { get; set; }
        public string PV { get; set; }

    }


    #endregion


    #endregion

    #region Prahlad singh
    #region
    public class cls_dipo_kyc
    {
        public DataTable adrprooof { get; set; }
        public DataTable idproof { get; set; }

        public HttpPostedFileBase id_proof { get; set; }
        public string idproofurl { get; set; }
        public string MemberId { get; set; }
        public string CompanyNameProofAttachment { get; set; }
        public string PanCardAttachment { get; set; }
        public string CompanyAddressProofAttachment { get; set; }
        public string PanCard { get; set; }
        public string CompanyNameProof { get; set; }
        public string CompanyAddressProof { get; set; }

    }
    #endregion fund management
    public class cls_fundmgnt
    {
        public string MemberId { get; set; }
        public string Action { get; set; }
        public DataTable dt { get; set; }
        public DataTable dtBank { get; set; }
        public DataTable dtDepo { get; set; }

        //-----------18/11/021
        public string Amount { get; set; }
        public string BankName { get; set; }
        public string DepositType { get; set; }
        public string PayDate { get; set; }
        public string MobileNo { get; set; }
        public string Remark { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string fileUrl { get; set; }
        public string Ifsc { get; set; }
        public string Branch { get; set; }
        public string AccNo { get; set; }
        public string AccHolderName { get; set; }
        public string bankid { get; set; }
        public string tranID { get; set; }
        public string RefNo { get; set; }
        public string ChecqueNo { get; set; }
        public string DepostBy { get; set; }
        public string FromDate { get; set; }
        public string Todate { get; set; }
        public List<SelectListItem> lstPackage { get; set; }
    }
    #region

    #endregion

    #endregion


    #region Request Franchise

    public class Responsecls
    {
        public string Msg { get; set; }
        public string Id { get; set; }
    }

    #endregion

    public class Delivery
    {
        public int Action { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string AadharNo { get; set; }
        public string Emailid { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public string Pincode { get; set; }
        public string VolunteerId { get; set; }
        public string StrId { get; set; }
        public string msg { get; set; }
        public DataTable dt { get; set; }
        public string SpCode { get; set; }
        public string AssignOrder { get; set; }
        public string DeliveredOrder { get; set; }


    }
    public class Requestmoneycls
    {
        public string Action { get; set; }
        public string InvoiceNo { get; set; }
        public string MemberId { get; set; }
        public string Amount { get; set; }
        public string TransactionId { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase Receiptimg { get; set; }
        public string Remark { get; set; }
        public string Id { get; set; }
        public string msg { get; set; }
    }
}