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
            PropertyClass objp = new PropertyClass();

            return View(objp);
        }

    }
}