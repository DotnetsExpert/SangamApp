using MLMPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MLMPortal.Controllers
{
    public class AccountController : Controller
    {
        
        Login objp = new Login();
        Datalayer objdl = new Datalayer();
        public ActionResult login()
        {
            Session.Abandon();
            if (HttpContext.Request.Cookies["userInfo"] != null)
            {
                HttpCookie aCookie = HttpContext.Request.Cookies["userInfo"];
                aCookie.Expires = DateTime.Now.AddDays(-10);
                aCookie.Value = "";
                HttpContext.Response.Cookies.Add(aCookie);
            }

            //    Master cl = new Master();
            //    try
            //    {
            //        cl.Action = 1;
            //        cl.Catdt = objdl.GetImage(cl);
            //        cl.ImageTitle = cl.Catdt.Rows[0]["ImageFile"].ToString();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //    return View(cl);


            return View();
        }

        public ActionResult Error()
        {


            return View();
        }


        public JsonResult AuthenticateUser(string UserName, string Password)
        {
            string[] msg = new string[2];
            try
            {
                objp.UserName = UserName;
                objp.Password = Password;
                objp.Action = 1;
                DataTable dt = objdl.getLoginDetails(objp, "Proc_GetLoginDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    HttpCookie aCookie = new HttpCookie("userInfo");
                    aCookie.Values["username"] = dt.Rows[0]["UserName"].ToString();
                    aCookie.Values["Role"] = dt.Rows[0]["Role"].ToString();
                    aCookie.Values["usercode"] = dt.Rows[0]["user_code"].ToString();
                    aCookie.Values["prodilepic"] = dt.Rows[0]["profileimgpath"].ToString();
                    aCookie.Expires = DateTime.Now.AddDays(365);
                    Response.Cookies.Add(aCookie);


                    Session["username"] = dt.Rows[0]["UserName"].ToString();
                    Session["Role"] = dt.Rows[0]["Role"].ToString();
                    Session["usercode"] = dt.Rows[0]["user_code"].ToString();
                    Session["prodilepic"] = dt.Rows[0]["profileimgpath"].ToString();
                    Session["Name"] = dt.Rows[0]["Name"].ToString();
                    if (dt.Rows[0]["Role"].ToString() == "1" || dt.Rows[0]["Role"].ToString() == "5")
                    {

                        msg[0] = dt.Rows[0]["Role"].ToString();
                        msg[1] = dt.Rows[0]["UserName"].ToString();

                    }
                    else if (dt.Rows[0]["Role"].ToString() == "2")
                    {

                        msg[0] = dt.Rows[0]["Role"].ToString();
                        msg[1] = dt.Rows[0]["UserName"].ToString();
                    }


                    var res = AllClasses.GetIP();
                    Master ms = new Master();
                    ms.Name = dt.Rows[0]["UserName"].ToString();
                    ms.Address = res;
                    ms.Type = Convert.ToString(dt.Rows[0]["group_name"]);
                    ms.Action = 1;
                    DataTable dt1 = Datalayer.setIPAddress(ms, "Proc_setIPAddress");
                   
                }
                else
                {
                    msg[0] = "0";
                }
            }
            catch (Exception ex)
            {
                msg[0] = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "Account");
        }

        //public ActionResult BinaryTree()
        //{
        //    Registration objp = new Registration();
        //    try
        //    {
        //        if (Session["username"] != null)
        //        {
        //            objp.SessionId = Convert.ToString(Session["username"]);

        //            objp.MemberId = Convert.ToString(Session["username"]);
        //        }
        //        DataTable dt123 = objdl.LevelTree(objp, "USP_BindSponsoreTree");
        //        objp.treeData = dt123.Rows[0]["Tree1"].ToString() + dt123.Rows[0]["Tree"].ToString() + dt123.Rows[0]["Tree2"].ToString();
        //        //objp.treeData = ViewBag.tree;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View(objp);
        //}

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
                    objp.SessionId = "MP00001";
                    objp.MemberId = "MP00001";
                }
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
                    objp.SessionId = "MP00001";
                    objp.MemberId = "MP00001";
                }
                DataTable dt123 = objdl.LevelTree(objp, "USP_BindBinaryTree");
                objp.treeData = dt123.Rows[0]["Tree1"].ToString() + dt123.Rows[0]["Tree"].ToString() + dt123.Rows[0]["Tree2"].ToString();
                //objp.treeData = ViewBag.tree;
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public string CallJob()
        {
            string Res = "Server Not Connect";
            DataTable dt123 = objdl.CallJob();

            if (dt123!=null && dt123.Rows.Count>0)
            {
                Res = Convert.ToString(dt123.Rows[0]["msg"]);
            }

            return Res;
        }


       


    }
}