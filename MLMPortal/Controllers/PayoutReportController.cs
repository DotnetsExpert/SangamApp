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
    public class PayoutReportController : Controller
    {
        // GET: PayoutReport
        Master ObjP = new Master();
        Datalayer objdl = new Datalayer();
        string Msg = "", Flag = "";
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LevelIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("4", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult N0_LevelIncome(string MemberId, string UID)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                objp.MemberId = MemberId;
                objp.FromDate = UID;
                objp.dt = objdl.USP_GetClosingDateils("5", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult DifffrenceIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                // objp.MemberId = objP.MemberId;

                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "1";
                objp.dt = objdl.PayoutReport(objp, "SP_PERFORMANCE");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult N0_DiffrenceIncome(string MemberId, string UID)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                objp.MemberId = MemberId;
                objp.FromDate = UID;
                ViewBag.Id = MemberId;
                objp.Action = "111";
                objp.dt = objdl.PayoutReport(objp, "SP_PERFORMANCE");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult RepurchaseIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "2";
                objp.dt = objdl.PayoutReport(objp, "SP_PERFORMANCE");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult LeadershipFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "Leadership";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetBussinessReportData");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult BikeFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "BikeFund";
                objp.dt = objdl.PayoutReport(objp, "Sp_GETALLRECORD");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult CarFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "CarFund";
                objp.dt = objdl.PayoutReport(objp, "Sp_GETALLRECORD");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult HouseFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "HouseFund";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetBussinessReportData");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult VillaFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                // objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "VillaFund";
                objp.dt = objdl.PayoutReport(objp, "Sp_GETALLRECORD");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult RoyalityFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "Royalty";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetBussinessReportData");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult AccumulatedIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                DateTime lastlastFriday = DateTime.Now.AddDays(-1);
                while (lastlastFriday.DayOfWeek != DayOfWeek.Friday)
                    lastlastFriday = lastlastFriday.AddDays(-1);
                //objp.FromDate = lastlastFriday.AddDays(-6).ToString("yyyy-MM-dd");
                // objp.ToDate = lastlastFriday.ToString("yyyy-MM-dd");

                // objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "1";
                objp.dt = objdl.PayoutReport(objp, "sp_BankStatementReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult NewAccumulatedIncome()
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                DateTime lastlastFriday = DateTime.Now.AddDays(-1);
                while (lastlastFriday.DayOfWeek != DayOfWeek.Friday)
                    lastlastFriday = lastlastFriday.AddDays(-1);
                //objp.FromDate = lastlastFriday.AddDays(-60).ToString("dd-MMM-yyyy");
                //objp.ToDate = DateTime.Now.ToString("dd-MMM-yyyy");
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.MemberId = Convert.ToString(Session["username"]);
                }
                objp.Action = "1";


                objp.dt = objdl.PayoutReport(objp, "sp_BankStatementReportNew");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [HttpPost]
        public ActionResult NewAccumulatedIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                DateTime lastlastFriday = DateTime.Now.AddDays(-1);
                while (lastlastFriday.DayOfWeek != DayOfWeek.Friday)
                    lastlastFriday = lastlastFriday.AddDays(-1);
                // objP.FromDate = lastlastFriday.AddDays(-6).ToString("dd-MM-yyyy");
                //objP.ToDate = lastlastFriday.ToString("dd-MM-yyyy");
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "1";
                objp.dt = objdl.PayoutReport(objp, "sp_BankStatementReportNew");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult MonthlyClosingReport(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {


                objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "1";
                objp.dt = objdl.PayoutReport(objp, "Usp_GetMonthlyIncomeDetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult BankStateMentReport(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {


                objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                // objp.Action = "1";
                objp.dt = objdl.PayoutReport(objp, "USP_FranchiseBankStatement");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult SelfIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "1";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }


        public ActionResult ROIIncome(PayoutReport objp)
        {
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("1", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult ROIDateWise(PayoutReport objp)
        {
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("2", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult DirectIncome(PayoutReport objP)

        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "14";// "2";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult PoolIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "15";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult BinaryIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "4";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        //Account Statement
        public ActionResult AccountStatement(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "13";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult NewLeadershipFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "6";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult NewBikeFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "3";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult NewCarFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "5";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult NewHouseFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "7";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult NewVillaFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "8";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult NewRoyalityFund(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                //objp.MemberId = objP.MemberId;
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "9";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult PurchaseRepurchaseReport(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                if (Request.QueryString["T"] != null && Request.QueryString["T"].ToString() != "")
                {
                    objp.UID = Request.QueryString["T"].ToString();
                }
                if (objP.UID != null)
                {
                    objp.UID = objP.UID;
                }

                if (objp.UID != "T")
                {
                    objp.Action = "1";
                    if (objp.UID == "N")
                    {
                        ViewBag.Heading = "Purchase Collection Report";
                    }
                    else if (objp.UID == "R")
                    {
                        ViewBag.Heading = "Re-Purchase Collection Report";
                    }
                }
                else
                {
                    objp.Action = "2";
                    ViewBag.Heading = "Purchase and Re-Purchase Collection Report";
                }

                objp.dt = objdl.BusinessReport(objp, "sp_getPurchaseAndRepurchaseBussiness_Report");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult GoldMatching(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {

                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "10";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult SilverMatching(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {

                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "11";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult DiamondMatching(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {

                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);
                else
                    objp.MemberId = objP.MemberId;
                objp.FromDate = objP.FromDate;
                objp.ToDate = objP.ToDate;
                objp.Action = "12";
                objp.dt = objdl.PayoutReport(objp, "Sp_GetAllReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }





        public ActionResult PerformanceIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);


                objp.dt = objdl.USP_GetClosingDateils("9", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }


        public ActionResult SuperPerformanceIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("10", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult BestPerformerOfTheDayIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("13", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
            
        public ActionResult RankIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("16", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }




        public ActionResult BronzeTTOIncome(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("15", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }



        public ActionResult Reward(PayoutReport objP)
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("17", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }




    }

}