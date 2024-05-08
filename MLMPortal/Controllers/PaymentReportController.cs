using MLMPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace MLMPortal.Controllers
{
    public class PaymentReportController : Controller
    {

        Master ObjP = new Master();
        Datalayer objdl = new Datalayer();
        string Msg = "", Flag = "";

        public ActionResult PendingWithdrwalRequests(PayoutReport objP)
        {

            PayoutReport objp = new PayoutReport();
            try
            {
                if (objP.FromDate == null && objP.ToDate == null)
                {
                    DateTime lastlastFriday = DateTime.Now.AddDays(-1);
                    while (lastlastFriday.DayOfWeek != DayOfWeek.Friday)
                        lastlastFriday = lastlastFriday.AddDays(-1);
                    objp.FromDate = lastlastFriday.AddDays(-6).ToString("yyyy-MM-dd");
                    objp.ToDate = lastlastFriday.ToString("yyyy-MM-dd");
                }
                else
                {
                    objp.FromDate = objP.FromDate;
                    objp.ToDate = objP.ToDate;
                }
                objp.Action = "1";
                objp.MemberId = objP.MemberId;
                //if (objP.MemberId == null)
                //{
                //    objp.MemberId = Session["username"].ToString();
                //}
                objp.dt = objdl.PaymentReport(objp, "USP_GETPaymentReport");
            }
            catch (Exception ex)
            {
            }
            return View(objp);
        }


        public ActionResult Payoutdetails(Payoutdetails objP)
        {

            Payoutdetails objp = new Payoutdetails();
            try
            {
                objp.dt = objdl.Payoutdetails(objp, "GetPayoutdetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult ApprovedWithdrwalRequests(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                ViewBag.DepositType = AllClasses.BindDDLDepositType();
                //if (objP.FromDate == null && objP.ToDate == null)
                //{
                //    DateTime lastlastFriday = DateTime.Now.AddDays(-1);
                //    while (lastlastFriday.DayOfWeek != DayOfWeek.Friday)
                //        lastlastFriday = lastlastFriday.AddDays(-1);
                //    objp.FromDate = lastlastFriday.AddDays(-6).ToString("yyyy-MM-dd");
                //    objp.ToDate = lastlastFriday.ToString("yyyy-MM-dd");
                //}
                //else
                //{
                //}
                if (!string.IsNullOrEmpty(objP.ApprovedDate))
                {
                    objp.ApprovedDate = Convert.ToDateTime(objP.ApprovedDate).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrEmpty(objP.RequestDate))
                {
                    objp.RequestDate = Convert.ToDateTime(objP.RequestDate).ToString("yyyy-MM-dd");
                }
                objp.Action = "2";

                objp.MemberName = objP.MemberName;
                objp.TransactionID = objP.TransactionID;
                objp.MemberId = objP.MemberId;
                objp.DepositType = objP.DepositType;

                //if (objP.MemberId == null)
                //{
                //    objp.MemberId = Session["username"].ToString();
                //}
                objp.dt = objdl.DipoPayoutReport(objp, "USP_GETPaymentReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult TDSMonthlyReport()
        {
            return View();
        }

        public JsonResult ApproveDisapproveLuxFundRequest(string MemberId, string Id, string Remark, string Amount, string status)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                objp.EntryBy = Convert.ToString(Session["username"]);
                objp.MemberId = MemberId;
                objp.Status = status;
                objp.Remark = Remark;
                objp.Amount = Amount;
                objp.Id = Id;
                objp.Status = status;
                DataTable dt = objdl.ApproveDisapproveLuxFundRequest(objp, "sp_ApproveDisApproveShareLuxuryReq");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.Id = Convert.ToString(dt.Rows[0]["id"]);
                    objp.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    objp.Id = "0";
                }
            }
            catch (Exception ex)
            {
                objp.Id = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }



        #region [Withdrawl Request]
        public ActionResult WithdrawalRequestROI()
        {
            Registration obj = new Registration();
            try
            {
                DateTime dNow = DateTime.Now;
                obj.Date = (dNow.ToString("dd-MMM-yyyy"));
                if (Session["username"] != null)
                {
                    obj.SessionId = Convert.ToString(Session["username"]);
                }
                obj.Action = 1;
                DataTable dt = objdl.GetWalletDetails(obj);
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj.MemberId = dt.Rows[0]["Member_Id"].ToString();
                    obj.Name = dt.Rows[0]["Name"].ToString();
                    obj.Address = dt.Rows[0]["address"].ToString();
                    obj.MobileNo = dt.Rows[0]["mobile"].ToString();
                    obj.CurrentWalletAmnt = dt.Rows[0]["ROIWallet"].ToString();

                }
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }
        public ActionResult WithdrawalRequestNEFT()
        {
            Registration obj = new Registration();
            try
            {
                DateTime dNow = DateTime.Now;
                obj.Date = (dNow.ToString("dd-MMM-yyyy"));
                if (Session["username"] != null)
                {
                    obj.SessionId = Convert.ToString(Session["username"]);
                }
                obj.Action = 1;
                DataTable dt = objdl.GetWalletDetails(obj);
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj.MemberId = dt.Rows[0]["Member_Id"].ToString();
                    obj.Name = dt.Rows[0]["Name"].ToString();
                    obj.Address = dt.Rows[0]["address"].ToString();
                    obj.MobileNo = dt.Rows[0]["mobile"].ToString();
                    obj.CurrentWalletAmnt = dt.Rows[0]["EWallet1"].ToString();

                }
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }

        public ActionResult WithdrawalHistoryNEFT(Registration obj)
        {

            try
            {
                obj.MemberId = Convert.ToString(Session["username"]);
                //obj.MemberId = obj.SessionId;
                obj.FromDate = obj.FromDate;
                obj.ToDate = obj.ToDate;

                obj.dt = objdl.WithdrawalHistoryReport(obj, "USP_WithdrawalHistoryNEFT");
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }

        public ActionResult PendingWithdrawlRequests(Registration obj)
        {

            try
            {
                if (Session["username"] != null)
                {
                    obj.SessionId = Convert.ToString(Session["username"]);
                }
                obj.MemberId = obj.SessionId;
                obj.FromDate = obj.FromDate;
                obj.ToDate = obj.ToDate;
                //obj.Action = 1;
                obj.dt = objdl.PendingWithdrawlRequestsReport(obj, "USP_WithdrawlRequest_Pendding");
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }

        public JsonResult ApproveWithdrawlRequests(string MemberId, string Id, string Remark, string Amount)
        {
            Registration obj = new Registration();
            try
            {
                obj.EntryBy = Convert.ToString(Session["username"]);
                obj.MemberId = MemberId;
                obj.Status = "1";
                obj.Remark = Remark;
                obj.Amount = Amount;
                obj.Id = Convert.ToInt32(Id);

                DataTable dt = objdl.ApproveRejectWithdrawlRequests(obj, "sp_ApproveWithdroll");
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                    obj.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    obj.Id = 0;
                }
            }
            catch (Exception ex)
            {
                obj.Id = 0;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RejectWithdrawlRequests(string MemberId, string Id, string Remark, string Amount)
        {
            Registration obj = new Registration();
            try
            {
                obj.EntryBy = Convert.ToString(Session["username"]);
                obj.MemberId = MemberId;
                obj.Status = "0";
                obj.Remark = Remark;
                obj.Amount = Amount;
                obj.Id = Convert.ToInt32(Id);

                DataTable dt = objdl.ApproveRejectWithdrawlRequests(obj, "sp_ApproveWithdroll");
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                    obj.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    obj.Id = 0;
                }
            }
            catch (Exception ex)
            {
                obj.Id = 0;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApprovedWithdrawlRequests(Registration obj)
        {

            try
            {

                obj.MemberId = obj.MemberId;
                obj.FromDate = obj.FromDate;
                obj.ToDate = obj.ToDate;

                obj.dt = objdl.ApprovedWithdrawlRequestsReport(obj, "USP_WithdrawlRequest_Approve");
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }

        public ActionResult RejectedWithdrawlRequests(Registration obj)
        {

            try
            {

                obj.MemberId = obj.SessionId;
                obj.FromDate = obj.FromDate;
                obj.ToDate = obj.ToDate;

                obj.dt = objdl.RejectedWithdrawlRequestsReport(obj, "USP_WithdrawlRequest_Rejected");
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }

        public JsonResult SaveInvoiceDetail(Registration obj)
        {

            DataTable dt = objdl.InsertWalletRequest(obj);
            if (dt != null && dt.Rows.Count > 0)
            {

                obj.msg = dt.Rows[0]["MSG"].ToString();

                if (dt.Rows[0]["ID"].ToString() == "0")
                {
                    obj.RequestWalletAmt = "0";
                    //return false;
                }
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertROIPayoutRequest(Registration obj)
        {

            DataTable dt = objdl.USP_InsertROIPayoutRequest(obj);
            if (dt != null && dt.Rows.Count > 0)
            {

                obj.msg = dt.Rows[0]["MSG"].ToString();

                if (dt.Rows[0]["ID"].ToString() == "0")
                {
                    obj.RequestWalletAmt = "0";
                    //return false;
                }
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region P2P

        public ActionResult P2P_Transfer()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("login", "Account");
            }


            PayoutReport objP = new PayoutReport();
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objP.UseId = Convert.ToString(Session["username"]);

                DataTable dt = objdl.USP_P2P_Transfer("1", objP);

                if (dt != null && dt.Rows.Count > 0)
                {
                    objP.UseId = dt.Rows[0]["Member_Id"].ToString();
                    objP.FullName = dt.Rows[0]["Name"].ToString();
                    objP.Amount = dt.Rows[0]["MainWallet"].ToString();

                }


            }
            catch (Exception ex)
            {

            }
            return View(objP);
        }
        [HttpPost]
        public ActionResult P2P_Transfer(PayoutReport objP)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            TempData["flag"] = "0";
            TempData["msg"] = "Server Not Responding";
            try
            {
                //if (Convert.ToString(Session["Role"]) == "2")
                objP.UseId = Convert.ToString(Session["username"]);

                DataTable dt = objdl.USP_P2P_Transfer("2", objP);

                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["flag"] = dt.Rows[0]["flag"].ToString();
                    TempData["msg"] = dt.Rows[0]["msg"].ToString();
                }


            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("P2P_Transfer");
        }

        public JsonResult BindUserWallet(PayoutReport objP)
        {
            Payoutdetails objp = new Payoutdetails();
            try
            {
                objP.UseId = Convert.ToString(Session["username"]);
                DataTable dt = objdl.USP_P2P_Transfer("1", objP);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (objP.DepositType == "M")
                    {
                        objp.newWallet = Convert.ToString(dt.Rows[0]["MainWallet"]);
                    }
                    else if (objP.DepositType == "F")
                    {
                        objp.newWallet = Convert.ToString(dt.Rows[0]["MoneyWallet"]);
                    }


                }

            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }



        public ActionResult P2P_TransferHistory(PayoutReport objP)
        {

            Payoutdetails objp = new Payoutdetails();
            try
            {
                objP.dt = objdl.USP_P2P_Transfer("1", objP);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }






        #endregion





    }
}