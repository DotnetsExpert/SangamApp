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
    public class MemberTeamController : Controller
    {
        Master ObjP = new Master();
        Datalayer objdl = new Datalayer();
        string Msg = "", Flag = "";
        // GET: MemberTeam
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DirectTeam()
        {
            Registration objp = new Registration();
            try
            {
                if (Request.QueryString["Id"] != null)
                {
                    objp.MemberId = Request.QueryString["Id"].ToString();
                }
                else
                {

                    objp.MemberId = "MP00001";
                }
                objp.dt = objdl.DirectTeam(objp, "Sp_GetRightTeamMemberDetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult MyTeam()
        {
            Registration objp = new Registration();
            try
            {
                if (Request.QueryString["Id"] != null)
                {
                    objp.MemberId = Request.QueryString["Id"].ToString();
                }
                else
                {
                    if (Convert.ToString(Session["Username"]).ToLower() == "admin")
                    {
                        objp.MemberId = "MP00001";
                    }
                    else
                    {
                        objp.MemberId = Convert.ToString(Session["Username"]);
                    }


                }
                objp.dt = objdl.DirectTeam(objp, "Sp_GetMemberDetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult LevelTree(string memberid)
        {
            Registration objp = new Registration();
            try
            {
                if (memberid != null)
                {
                    objp.SessionId = memberid;
                    objp.MemberId = memberid;
                }
                else
                {
                    if (Convert.ToString(Session["Role"])== "2")
                    {
                        objp.SessionId = Convert.ToString(Session["username"]);
                        objp.MemberId = Convert.ToString(Session["username"]);
                    }
                    else
                    {
                        objp.SessionId = "MP00001";// "MP00001";
                        objp.MemberId = "MP00001";// "MP00001";
                    }
                  
                }


                DataTable dt123 = objdl.LevelTree(objp, "USP_BindSponsoreTree");
                objp.treeData = dt123.Rows[0]["Tree1"].ToString() + dt123.Rows[0]["Tree"].ToString() + dt123.Rows[0]["Tree2"].ToString();
                //objp.treeData = ViewBag.tree;
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [HttpPost]
        public ActionResult LevelTree(Registration objp, string memberid)
        {
            try
            {
                if (memberid != null)
                {
                    objp.SessionId = memberid;
                    objp.MemberId = memberid;
                }
                else
                {
                    objp.SessionId = objp.MemberId;
                    objp.MemberId = objp.MemberId;
                }
                DataTable dt123 = objdl.LevelTree(objp, "USP_BindSponsoreTree");
                objp.treeData = dt123.Rows[0]["Tree1"].ToString() + dt123.Rows[0]["Tree"].ToString() + dt123.Rows[0]["Tree2"].ToString();
                //objp.treeData = ViewBag.tree;
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }


        public ActionResult BinaryTree(string memberid)
        {
            Registration objp = new Registration();
            try
            {
                if (memberid != null)
                {
                    objp.SessionId = memberid;
                    objp.MemberId = memberid;
                }
                else
                {
                    if (Convert.ToString(Session["Username"]).ToLower() == "admin")
                    {
                        objp.MemberId = "MP00001";
                        objp.SessionId = "MP00001";
                    }
                    else
                    {
                        objp.SessionId = Convert.ToString(Session["username"]);
                        objp.MemberId = Convert.ToString(Session["username"]);
                    }



                }
                //USP_BindBinaryTree_testing
                //DataTable dt123 = objdl.LevelTree(objp, "USP_BindBinaryTree");
                DataTable dt123 = objdl.LevelTree(objp, "USP_BindBinaryTree");
                objp.treeData = dt123.Rows[0]["Tree1"].ToString() + dt123.Rows[0]["Tree"].ToString() + dt123.Rows[0]["Tree2"].ToString();
                //objp.treeData = ViewBag.tree;
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [HttpPost]
        public ActionResult BinaryTree(Registration objp)
        {
            try
            {
                if (objp.MemberId != null)
                {
                    objp.SessionId = objp.MemberId;
                    objp.MemberId = objp.MemberId;
                }
                else
                {
                    objp.SessionId = Session["username"].ToString();
                    objp.MemberId = Session["username"].ToString();
                }


                string MemberId = Session["username"].ToString();
                string NewMemberId = objp.MemberId;
                DataTable dtcheck = objdl.CheckMemberdetailbyIdExists(MemberId, NewMemberId, "proc_checkMemberId");

                if (dtcheck.Rows.Count > 0)
                {
                    DataTable dt123 = objdl.LevelTree(objp, "USP_BindBinaryTree");
                    //DataTable dt123 = objdl.LevelTree(objp, "USP_BindBinaryTree_testing");
                    objp.treeData = dt123.Rows[0]["Tree1"].ToString() + dt123.Rows[0]["Tree"].ToString() + dt123.Rows[0]["Tree2"].ToString();
                }

                //objp.treeData = ViewBag.tree;
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult MemberBankDetails()
        {
            Registration cr = new Registration();
            if (Session["username"] != null)
            {
                if (Request.QueryString["Id"] != null && Request.QueryString["Id"].ToString() != "")
                {
                    cr.SessionId = Request.QueryString["Id"].ToString();
                }
                else
                {
                    cr.SessionId = Convert.ToString(Session["username"]);
                }
                //cl.FromDate = DateTime.Now.AddDays(-365).ToString("dd-MMM-yyyy");
                //cl.ToDate = DateTime.Now.ToString("dd-MMM-yyyy");
                ////cl.MemberId = Convert.ToString(Session["username"]);

                cr.dt = objdl.GetMemberBankDetails(cr);
                if (cr.dt != null && cr.dt.Rows.Count > 0)
                {
                    cr.Name = cr.dt.Rows[0]["Name"].ToString();
                    cr.AccountNo = cr.dt.Rows[0]["BankaccNo"].ToString();
                    cr.BankName = cr.dt.Rows[0]["BankName"].ToString();
                    cr.BankBranch = cr.dt.Rows[0]["BankBranch"].ToString();
                    cr.IFSCCode = cr.dt.Rows[0]["BankIfscCode"].ToString();
                    cr.PanNo = cr.dt.Rows[0]["Panno"].ToString();
                    cr.PhonePayNo = cr.dt.Rows[0]["PhonePayNo"].ToString();
                    cr.MemberId = cr.SessionId;
                    cr.Id = 1;
                }
                else
                {
                    cr.Id = 0;
                }


            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(cr);
        }
        [HttpPost]
        public ActionResult MemberBankDetails(Registration mr)
        {

            if (Session["username"] != null)
            {
                if (Request.QueryString["Id"] != null && Request.QueryString["Id"].ToString() != "")
                {
                    mr.SessionId = Request.QueryString["Id"].ToString();
                }
                else
                {
                    mr.SessionId = Convert.ToString(Session["username"]);
                }
                //cl.UseId = Convert.ToString(Session["username"]);
                //cl.FromDate = DateTime.Now.AddDays(-365).ToString("dd-MMM-yyyy");
                //cl.ToDate = DateTime.Now.ToString("dd-MMM-yyyy");
                ////cl.MemberId = Convert.ToString(Session["username"]);

                //cl.dt = objdl.GetTeamRepurchaseDetails(cl);
                mr.dt = objdl.Sp_UpdatebankDetails(mr);
                if (mr.dt != null & mr.dt.Rows.Count > 0)
                {
                    Msg = mr.dt.Rows[0]["MSG"].ToString();
                    Flag = mr.dt.Rows[0]["ID"].ToString();
                    ModelState.Clear();
                }
                else
                {
                    Msg = "Something Went Wrong.!!";
                    Flag = "0";

                }
                TempData["msg"] = Msg;
                TempData["flag"] = Flag;

            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(mr);
        }
        public ActionResult ManageLeftRight()
        {
            Registration cr = new Registration();
            if (Session["username"] != null)
            {
                //cl.UseId = Convert.ToString(Session["username"]);
                //cl.FromDate = DateTime.Now.AddDays(-365).ToString("dd-MMM-yyyy");
                //cl.ToDate = DateTime.Now.ToString("dd-MMM-yyyy");
                ////cl.MemberId = Convert.ToString(Session["username"]);

                //cl.dt = objdl.GetTeamRepurchaseDetails(cl);

            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(cr);
        }
        [HttpPost]
        public ActionResult ManageLeftRight(Registration mr)
        {

            if (Session["username"] != null)
            {
                mr.MemberId = Session["username"].ToString();
                mr.dt = objdl.sp_ManageLeftRight(mr);
                if (mr.dt != null & mr.dt.Rows.Count > 0)
                {
                    Msg = mr.dt.Rows[0]["MSG"].ToString();
                    Flag = mr.dt.Rows[0]["ID"].ToString();
                    ModelState.Clear();
                }
                else
                {
                    Msg = "Something Went Wrong.!!";
                    Flag = "0";

                }
                TempData["msg"] = Msg;
                TempData["flag"] = Flag;

            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(mr);
        }

        public ActionResult LevelWiseTeamReport()
        {
            PayoutReport objp = new PayoutReport();
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("6", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);

          
        }

        public ActionResult LevelWiseInconeTeamReport(PayoutReport objp)
        {
            
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("7", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);


        }

        public ActionResult TransactionHistory(PayoutReport objp)
        {

            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("8", objp);
            }
            catch (Exception ex)
            {

            }
            return View(objp);


        }



    }
}