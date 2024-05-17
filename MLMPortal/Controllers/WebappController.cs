using MLMPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MLMPortal.Controllers
{
    public class WebappController : Controller
    {
        Datalayer objL = new Datalayer();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return View("Login");
        }
        
        public JsonResult LoginUser(PropertyClass p)
        {
            string msg = "";
            try
            {
                Login login = new Login();
                login.UserName = p.UserName;
                login.Password = p.Password;
                login.Action = 1;

                DataTable dt = objL.getLoginDetails(login, "Proc_GetLoginDetails");

                if (dt != null && dt.Rows.Count > 0)
                {
                   
                    Session["MUserMobile"] = dt.Rows[0]["MobileNo"].ToString();
                    Session["MUserName"] = dt.Rows[0]["username"].ToString();
                    Session["MIntroID"] = dt.Rows[0]["IntroID"].ToString();
                    Session["RegDate"] = dt.Rows[0]["RegDate"].ToString();
                    Session["Name"] = dt.Rows[0]["Name"].ToString();


                    Session["username"] = dt.Rows[0]["UserName"].ToString();
                    Session["Role"] = dt.Rows[0]["Role"].ToString();
                    Session["usercode"] = dt.Rows[0]["user_code"].ToString();
                    Session["prodilepic"] = dt.Rows[0]["profileimgpath"].ToString();

                    // Session["group_name"] = dt.Rows[0]["group_name"].ToString();

                    // Session["Secretkey"] = dt.Rows[0]["Secretkey"].ToString();
                    p.Id = 1;
                    p.Msg = "";
                }
                else
                {
                    p.Id = 2;
                    p.Msg = "Invalid user name or password";
                }
            }
            catch (Exception ex)
            {
                p.Id = 0;
                p.Msg = "Some Error Occured!";
            }
            return Json(p, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Registration()
        {
            PropertyClass objp = new PropertyClass();
            if (Request.QueryString["uid"] != null)
            {
                string rlink = Request.QueryString["uid"].ToString();
                if (Request.QueryString["uid"].Contains("/share"))
                {
                    rlink = Request.QueryString["uid"].Replace("/share", "");
                }
                objp.SponsorId = rlink;
            }

            return View(objp);
        }






        public ActionResult Dashboard()
        {
            if (Session["username"]==null)
            {
                return RedirectToAction("login","webApp");
            }

            Registration mr = new Registration();
            mr.SessionId = Convert.ToString(Session["username"]);
            mr.hdnValue = "/RegistrationForm?SPId=" + Convert.ToString(Session["username"]);
            mr.dt = objL.GetDashBoardDetails(mr);
            if (mr.dt != null && mr.dt.Rows.Count > 0)
            {
                mr.Name = mr.dt.Rows[0]["NAME"].ToString();
                mr.MemberId = mr.dt.Rows[0]["RegistrationID"].ToString();
                mr.SponserId = mr.dt.Rows[0]["SponsId"].ToString();
                mr.RegDate = mr.dt.Rows[0]["DateOfJoin"].ToString();
                mr.Rank = mr.dt.Rows[0]["Rank"].ToString();
                mr.Distributor = mr.dt.Rows[0]["RankName"].ToString();
                mr.Package = mr.dt.Rows[0]["JoinPkge"].ToString();
                mr.Status = mr.dt.Rows[0]["Status"].ToString();
                mr.UpgradeStatus = mr.dt.Rows[0]["USta"].ToString();
                mr.ProfilePic = mr.dt.Rows[0]["ProfilePic"].ToString();
                mr.PaymentDate = mr.dt.Rows[0]["ActivationDate"].ToString();
                mr.TotalWithdraw = mr.dt.Rows[0]["TotalWithdraw"].ToString();
                mr.LedgerBalance = mr.dt.Rows[0]["CurrentWalletAmnt"].ToString();



                //mr.ROIWallet = mr.dt.Rows[0]["RoiWallet"].ToString();                   
                
                //mr.MatchingIncome = mr.dt.Rows[0]["MatchingIncome"].ToString();


                mr.AllTeam = mr.dt.Rows[0]["OurTeam"].ToString();
                mr.MyDirects = mr.dt.Rows[0]["DirectTeam"].ToString();
                mr.DirectIncome = mr.dt.Rows[0]["DirectIncome"].ToString();
                mr.LevelIncome = mr.dt.Rows[0]["LevelIncome"].ToString();
                mr.FundWallet = mr.dt.Rows[0]["FundWallet"].ToString();
                mr.SuperPerformancebonus = mr.dt.Rows[0]["SuperPerformanceIncome"].ToString();
                mr.PerformerOfTheDayIncome = mr.dt.Rows[0]["PerformanceOftheDayIncome"].ToString();
                mr.Performancebonus = mr.dt.Rows[0]["PerformanceIncome"].ToString();
                mr.ROIIncome = mr.dt.Rows[0]["ROIIncome"].ToString();


            }
            mr.dt = null;

            return View(mr);
        }




        public ActionResult Topup()
        {
            UserReport returnObj = new UserReport();

            returnObj.SmemberId = Convert.ToString(Session["username"]);
            DataTable dt = objL.USP_GetAppData("1", returnObj);
            if (dt.Rows.Count > 0)
            {

                returnObj.Amount = dt.Rows[0]["MoneyWallet"].ToString();
                returnObj.TopupAmt = dt.Rows[0]["InvestAmt"].ToString();
            }
            else
            {
                returnObj.Amount = "0";
                returnObj.TopupAmt = "0";
            }

            returnObj.Table1 = objL.USP_GetAppData("2", returnObj);
            returnObj.Table2 = objL.USP_GetAppData("3", returnObj);

            return View(returnObj);
        }


        public ActionResult AddFund()
        {
           

            return View();
        }

        public ActionResult TeamReports()
        {


            return View();
        }
        public ActionResult IncomeReports()
        {


            return View();
        }



        public ActionResult ViewProfile(profile p)
        {

            profile Objp = new profile();
            try
            {
                Objp.Action = 1;
                Objp.Username = Convert.ToString(Session["MUserName"]);
                DataTable dt = objL.UserProfile(Objp, "Proc_UserProfile");
                if (dt.Rows.Count > 0)
                {
                    Objp.Name = dt.Rows[0]["Name"].ToString();
                    Objp.Mobile = dt.Rows[0]["mobile"].ToString();
                    Objp.Email = dt.Rows[0]["Email"].ToString();
                    Objp.Password = dt.Rows[0]["psw"].ToString();
                    Objp.ImgProfile = dt.Rows[0]["profileimgpath"].ToString();
                }
                else
                {

                }
            }
            catch (Exception exc)
            {
                Response.Write("<script>alert('" + exc.Message + "')</script>");
                //throw exc;
            }
            return View(Objp);
        }


        public ActionResult ChangePassword()
        {
            return View();
        }



        public ActionResult ROIIncomeReport()
        {
            return View();
        }


        public ActionResult Withdroll()
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
                DataTable dt = objL.GetWalletDetails(obj);
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






    }
}