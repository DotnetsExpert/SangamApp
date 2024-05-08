using MLMPortal.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MLMPortal.Controllers
{

    public class AdminController : Controller
    {
        // GET: Admin
        //Registration objrp = new Registration();
        Master ObjP = new Master();
        Datalayer objdl = new Datalayer();
        string Msg = "", Flag = "";
        Stucommon objas = new Stucommon();
        public ActionResult Index()
        {
            clsDash objDash = new clsDash();
            try
            {
                if (Session["username"] != null)
                {
                    DataTable dtCount = objdl.get_Dash_admin();
                    if (dtCount != null && dtCount.Rows.Count > 0)
                    {
                        objDash.TotalJoining = Convert.ToString(dtCount.Rows[0]["TotalJoining"]);
                        objDash.TotalActiveMember = Convert.ToString(dtCount.Rows[0]["TotalActiveMember"]);
                        objDash.TotalInactiveMember = Convert.ToString(dtCount.Rows[0]["TotalInactiveMember"]);

                        //objDash.CustomerSale = Convert.ToString(dtCount.Rows[0]["CustomerSale"]);
                        //objDash.FranchiseSale = Convert.ToString(dtCount.Rows[0]["FranchiseSale"]);
                        //objDash.WalletBal = Convert.ToString(dtCount.Rows[0]["WalletBalance"]);
                        //objDash.TotalIncome = Convert.ToString(dtCount.Rows[0]["TotalIncome"]);
                        //objDash.FranchiseApp = Convert.ToString(dtCount.Rows[0]["FranchiseApp"]);
                        //objDash.TotalPendingOrder = Convert.ToString(dtCount.Rows[0]["TotalPendingOrder"]);
                        //objDash.TotalDeliveredOrder = Convert.ToString(dtCount.Rows[0]["TotalDeliveredOrder"]);
                        //objDash.MonthlyPendingOrder = Convert.ToString(dtCount.Rows[0]["MonthlyPendingOrder"]);
                        //objDash.MonthlyDeliveredOrder = Convert.ToString(dtCount.Rows[0]["MonthlyDeliveredOrder"]);


                        objDash.FundBal = Convert.ToString(dtCount.Rows[0]["FundWalletBal"]);
                        objDash.WalletBal = Convert.ToString(dtCount.Rows[0]["WalletBalance"]);
                        objDash.TotalWithdroll = Convert.ToString(dtCount.Rows[0]["WithdrollAmt"]);
                        objDash.PendingWithdroll = Convert.ToString(dtCount.Rows[0]["PendingWithdroll"]);



                    }

                }
                else
                {
                    Response.Redirect("../Account/login");
                }
            }
            catch (Exception ex)
            {
            }


            return View(objDash);
            //Registration mr = new Registration();
            //if (Session["username"] != null)
            //{


            //}
            //else
            //{
            //    Response.Redirect("../Account/login");
            //}

            //return View(mr);

        }

        public ActionResult FranchiseWiseReport()
        {

            PayoutReport cl = new PayoutReport();
            if (Session["username"] != null)
            {
                cl.MemberId = Session["username"].ToString() == "admin" ? null : Session["username"].ToString();
                cl.Status = null;
                cl.dt = objdl.Franchisewiseincomereportadmin("1", cl);

            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(cl);
        }


        public ActionResult FranchiseDayclose()
        {

            PayoutReport cl = new PayoutReport();
            if (Session["username"] != null)
            {
                cl.MemberId = Session["username"].ToString() == "admin" ? null : Session["username"].ToString();
                cl.Status = null;
                cl.dt = objdl.Franchisewiseincomereportadmin("3", cl);

            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(cl);
        }

        public JsonResult FranchiseDaycloseofminimart(string Franchise_Id, string Pincode)
        {

            string flag = "";
            DataTable dt = new DataTable();
            dt = objdl.Admindayclose(3, Franchise_Id, Pincode);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Id"].ToString() == "1")
                {
                    flag = dt.Rows[0]["Id"].ToString();
                }
            }
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewFranchiseRequestResponse(string InvoiceNo)
        {

            PayoutReport cl = new PayoutReport();
            if (Session["username"] != null)
            {
                cl.MemberId = Session["username"].ToString() == "admin" ? null : Session["username"].ToString();
                cl.Status = null;
                cl.InviceNo = InvoiceNo;
                cl.dt = objdl.ViewRequestresponsefranchise("3", cl);

            }
            else
            {
                Response.Redirect("../Account/login");
            }

            return View(cl);
        }

        public ActionResult Test()
        {
            Registration reg = new Registration();
            string ipAdd = "";
            ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAdd))
            {
                ipAdd = Request.ServerVariables["REMOTE_ADDR"];
                reg.msg = ipAdd;
            }
            else
            {
                reg.msg = ipAdd;
            }
            return View(reg);

        }
        public ActionResult CompanyMaster()
        {


            TempData["flag"] = null;
            TempData["msg"] = null;
            try
            {
                ObjP.Action = 3;
                ObjP.dt = objdl.CompanyMaster(ObjP, "USP_CompanyMasterNew");
                if (ObjP.dt != null)
                {
                    ObjP.Name = ObjP.dt.Rows[0]["Name"].ToString();
                    ObjP.Address = ObjP.dt.Rows[0]["Address"].ToString();
                    ObjP.EmailId = ObjP.dt.Rows[0]["emaill"].ToString();
                    ObjP.Contact = ObjP.dt.Rows[0]["contactnos"].ToString();
                    ObjP.Website = ObjP.dt.Rows[0]["Webcontact"].ToString();
                    ObjP.GSTNo = ObjP.dt.Rows[0]["GSTNo"].ToString();
                    ObjP.CategoryId = Convert.ToInt32(ObjP.dt.Rows[0]["PKID"].ToString());
                }
            }
            catch
            {

            }
            return View(ObjP);
        }
        public ActionResult UpdateLevelPer()
        {
            if (Convert.ToString(Session["Role"]) != "1")
            {
                return RedirectToAction("login", "Account");
            }
            try
            {
                ObjP.Action = 1;
                ObjP.dt = objdl.USP_UpdateLevelIncomePer("1", ObjP);
            }
            catch
            {

            }
            return View(ObjP);
        }
        public JsonResult UpdateLevelPercentage(Master ObjP)
        {
            string flag = "0";
            string msg = "Server Not Responding";

            if (Convert.ToString(Session["Role"]) != "1")
            {
                return Json(new { flag = flag, msg = msg }, JsonRequestBehavior.AllowGet);
            }

            DataTable dt = objdl.USP_UpdateLevelIncomePer("2", ObjP);

            if (dt!=null && dt.Rows.Count>0)
            {
                flag = Convert.ToString(dt.Rows[0]["flag"]);
                msg = Convert.ToString(dt.Rows[0]["msg"]);
            }


            return Json(new { flag = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public ActionResult CompanyMaster(Master objp, int CategoryId)
        {
            try
            {

                //ObjP.CategoryName = CategoryName;
                objp.EntryBy = Session["username"].ToString();
                // ObjP.CategoryId = CategoryId;
                if (objp.CategoryId != 0)
                {
                    objp.Action = 2;

                }
                else
                {
                    objp.Action = 1;
                }
                DataTable dt = objdl.CompanyMaster(objp, "USP_CompanyMasterNew");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["msg"].ToString();
                    Flag = dt.Rows[0]["Id"].ToString();

                }
                else
                {

                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;


            ObjP.Action = 3;
            ObjP.dt = objdl.CompanyMaster(ObjP, "USP_CompanyMasterNew");



            return View(ObjP);
        }

        public ActionResult BankMaster()
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            try
            {
                ObjP.Action = 2;
                ObjP.dt = objdl.BankMaster(ObjP, "Sp_InsertBankDetails");

            }
            catch (Exception ex)
            {

            }
            return View(ObjP);
        }
        [HttpPost]
        public ActionResult BankMaster(Master objp, int CategoryId)
        {
            try
            {


                objp.EntryBy = Session["username"].ToString();
                // ObjP.CategoryId = CategoryId;
                if (objp.CategoryId != 0)
                {
                    objp.Action = 4;

                }
                else
                {
                    objp.Action = 1;
                }
                DataTable dt = objdl.BankMaster(objp, "Sp_InsertBankDetails");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["msg"].ToString();
                    Flag = dt.Rows[0]["Id"].ToString();

                }
                else
                {

                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;


            ObjP.Action = 2;
            ObjP.dt = objdl.BankMaster(ObjP, "Sp_InsertBankDetails");



            return View(ObjP);
        }

        public JsonResult DeleteBankMaster(int CategoryId)
        {
            try
            {
                ObjP.EntryBy = Convert.ToString(Session["username"]);
                ObjP.CategoryId = CategoryId;
                ObjP.Action = 3;
                DataTable dt = objdl.BankMaster(ObjP, "Sp_InsertBankDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjP.CategoryId = Convert.ToInt32(dt.Rows[0]["id"]);
                    ObjP.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    ObjP.CategoryId = 0;
                }
            }
            catch (Exception ex)
            {
                //objp.strId = "0";
            }
            return Json(ObjP, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewsMaster()
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            try
            {
                ObjP.Action = 2;
                ObjP.dt = objdl.NewsMaster(ObjP, "USP_NewsMaster");

            }
            catch (Exception ex)
            {

            }
            return View(ObjP);
        }
        [HttpPost]
        public ActionResult NewsMaster(Master objp, int CategoryId)
        {

            try
            {
                objp.EntryBy = Session["username"].ToString();
                // ObjP.CategoryId = CategoryId;
                if (objp.CategoryId != 0)
                {
                    objp.Action = 4;
                }
                else
                {
                    objp.Action = 1;
                }
                DataTable dt = objdl.NewsMaster(objp, "USP_NewsMaster");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["msg"].ToString();
                    Flag = dt.Rows[0]["Id"].ToString();
                }
                else
                {
                    Flag = "0";
                }
            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            ObjP.Action = 2;
            ObjP.dt = objdl.NewsMaster(ObjP, "USP_NewsMaster");
            return View(ObjP);
        }

        public JsonResult DeleteNewsMaster(int CategoryId)
        {
            try
            {
                ObjP.EntryBy = Convert.ToString(Session["username"]);
                ObjP.CategoryId = CategoryId;
                ObjP.Action = 5;
                DataTable dt = objdl.NewsMaster(ObjP, "USP_NewsMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjP.CategoryId = Convert.ToInt32(dt.Rows[0]["id"]);
                    ObjP.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    ObjP.CategoryId = 0;
                }
            }
            catch (Exception ex)
            {
                //objp.strId = "0";
            }
            return Json(ObjP, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ADashboard()
        {
            clsDash objDash = new clsDash();
            try
            {
                DataTable dtCount = objdl.get_Dash_admin();
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    objDash.TotalJoining = Convert.ToString(dtCount.Rows[0]["TotalJoining"]);
                    objDash.TotalActiveMember = Convert.ToString(dtCount.Rows[0]["TotalActiveMember"]);
                    objDash.TotalInactiveMember = Convert.ToString(dtCount.Rows[0]["TotalInactiveMember"]);
                    objDash.CustomerSale = Convert.ToString(dtCount.Rows[0]["CustomerSale"]);
                    objDash.FranchiseSale = Convert.ToString(dtCount.Rows[0]["FranchiseSale"]);
                }
            }
            catch (Exception ex)
            {
            }


            return View(objDash);
        }

        public ActionResult BannerMaster()
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            try
            {
                ObjP.Action = 2;
                ObjP.dt = objdl.BannerMaster(ObjP, "Sp_Banner");

            }
            catch (Exception ex)
            {

            }
            return View(ObjP);
        }
        [HttpPost]
        public ActionResult BannerMaster(Master objp, int CategoryId, HttpPostedFileBase ProductFile)
        {
            try
            {
                string str = "";

                if (ProductFile != null)
                {
                    str = "Banner" + DateTime.Now.Ticks + DateTime.Now.ToString("ddMMMyyyyhhss") + Path.GetExtension(ProductFile.FileName);
                    ProductFile.SaveAs(Server.MapPath("/images/banner/" + str));

                    objp.FilePath = str;
                }
                objp.EntryBy = Session["username"].ToString();
                // ObjP.CategoryId = CategoryId;
                if (objp.CategoryId != 0)
                {
                    objp.Action = 5;

                }
                else
                {
                    objp.Action = 1;
                }
                DataTable dt = objdl.BannerMaster(objp, "Sp_Banner");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["msg"].ToString();
                    Flag = dt.Rows[0]["Id"].ToString();

                }
                else
                {

                    Flag = "0";
                    Flag = "Server not Response!.";
                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;


            ObjP.Action = 2;
            ObjP.dt = objdl.BannerMaster(ObjP, "Sp_Banner");



            return View(ObjP);
        }

        public JsonResult DeleteBannerMaster(int CategoryId)
        {
            try
            {
                ObjP.EntryBy = Convert.ToString(Session["username"]);
                ObjP.CategoryId = CategoryId;
                ObjP.Action = 4;
                DataTable dt = objdl.BannerMaster(ObjP, "Sp_Banner");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjP.CategoryId = Convert.ToInt32(dt.Rows[0]["id"]);
                    ObjP.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    ObjP.CategoryId = 0;
                    ObjP.msg = "Server not Response!.";
                }
            }
            catch (Exception ex)
            {
                //objp.strId = "0";
            }
            return Json(ObjP, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCategory()
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            try
            {
                ObjP.Action = 4;
                ObjP.dt = objdl.AddCategory(ObjP, "saveUpdateCategory");
            }
            catch
            {

            }
            return View(ObjP);
        }
        [HttpPost]
        public ActionResult AddCategory(string CategoryName, int CategoryId, HttpPostedFileBase ProductFile)
        {
            try
            {
                string str = "";
                if (ProductFile != null)
                {
                    str = "Category" + DateTime.Now.Ticks + DateTime.Now.ToString("ddMMMyyyyhhss") + Path.GetExtension(ProductFile.FileName);
                    ProductFile.SaveAs(Server.MapPath("~/images/products/" + str));

                    ObjP.FilePath = str;
                }
                ObjP.CategoryName = CategoryName;
                ObjP.EntryBy = Session["username"].ToString();
                ObjP.CategoryId = CategoryId;
                if (ObjP.CategoryId != 0)
                {
                    ObjP.Action = 3;

                }
                else
                {
                    ObjP.Action = 1;
                }
                DataTable dt = objdl.AddCategory(ObjP, "saveUpdateCategory");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["msg"].ToString();
                    Flag = dt.Rows[0]["Id"].ToString();

                }
                else
                {

                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;


            ObjP.Action = 4;
            ObjP.dt = objdl.AddCategory(ObjP, "saveUpdateCategory");



            return View(ObjP);
        }

        public JsonResult DeleteCategory(int CategoryId)
        {
            try
            {
                ObjP.EntryBy = Convert.ToString(Session["username"]);
                ObjP.CategoryId = CategoryId;
                ObjP.Action = 2;
                DataTable dt = objdl.AddCategory(ObjP, "saveUpdateCategory");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjP.CategoryId = Convert.ToInt32(dt.Rows[0]["id"]);
                    ObjP.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    ObjP.CategoryId = 0;
                    ObjP.msg = "Server not Response!.";
                }
            }
            catch (Exception ex)
            {
                //objp.strId = "0";
            }
            return Json(ObjP, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddSubCategory()
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            try
            {
                ViewBag.CategoryList = AllClasses.BindDDL(objdl.BindCategory(4, 0));
                ObjP.Action = 4;
                ObjP.dt = objdl.AddSubCategory(ObjP, "saveUpdate_SubCategory");
            }
            catch (Exception ex)
            {

            }
            return View(ObjP);
        }
        [HttpPost]
        public ActionResult AddSubCategory(string SubCategoryName, int CategoryId, int SubCategoryId)
        {
            try
            {

                ObjP.SubCategoryName = SubCategoryName;
                ObjP.EntryBy = Session["username"].ToString();
                ObjP.CategoryId = CategoryId;
                ObjP.SubCategoryId = SubCategoryId;
                if (ObjP.SubCategoryId != 0)
                {
                    ObjP.Action = 3;

                }
                else
                {
                    ObjP.Action = 1;
                }
                DataTable dt = objdl.AddSubCategory(ObjP, "saveUpdate_SubCategory");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["msg"].ToString();
                    Flag = dt.Rows[0]["Id"].ToString();
                    //Response.Write("<script>alert('" + dt.Rows[0]["msg"] + "')'</script>");
                }
                else
                {
                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;


            ViewBag.CategoryList = AllClasses.BindDDL(objdl.BindCategory(4, 0));
            ObjP.Action = 4;
            ObjP.dt = objdl.AddSubCategory(ObjP, "saveUpdate_SubCategory");



            return View(ObjP);

        }

        public JsonResult DeleteSubCategoryDetails(int SubCategoryId)
        {
            try
            {
                ObjP.EntryBy = Convert.ToString(Session["username"]);
                ObjP.SubCategoryId = SubCategoryId;
                ObjP.Action = 2;
                DataTable dt = objdl.AddSubCategory(ObjP, "saveUpdate_SubCategory");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjP.SubCategoryId = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                    ObjP.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    ObjP.SubCategoryId = 0;
                }
            }
            catch (Exception ex)
            {
                ObjP.SubCategoryId = 0;
            }
            return Json(ObjP, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductMaster()
        {
            try
            {
                ViewBag.CategoryList = AllClasses.BindDDL(objdl.BindCategory(4, 0));
                ObjP.Action = 2;
                ObjP.dt = objdl.AddProduct(ObjP, "USP_ManagePackage");
            }
            catch (Exception ex)
            {

            }
            return View(ObjP);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProductMaster(Master objp, HttpPostedFileBase ProductFile)
        {
            try
            {
                string str = "";
                if (ProductFile != null)
                {
                    str = "Product" + DateTime.Now.Ticks + DateTime.Now.ToString("ddMMMyyyyhhss") + Path.GetExtension(ProductFile.FileName);
                    ProductFile.SaveAs(Server.MapPath("~/images/products/" + str));
                    objp.FilePath = str;
                }
                objp.EntryBy = Session["username"].ToString();
                //objp.CategoryId = CategoryId;
                //objp.SubCategoryId = SubCategoryId;
                if (objp.Id != 0)
                {
                    objp.Action = 3;
                }
                else
                {
                    objp.Action = 1;
                }
                DataTable dt = objdl.AddProduct(objp, "USP_ManagePackage");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["msg"].ToString();
                    Flag = dt.Rows[0]["Id"].ToString();
                }
                else
                {
                    Flag = "0";
                    Msg = "Sever Not Response!";
                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return RedirectToAction("ProductMaster");
        }

        public JsonResult DeleteProductMasterDetails(int SubCategoryId)
        {
            try
            {
                ObjP.EntryBy = Convert.ToString(Session["username"]);
                ObjP.SubCategoryId = SubCategoryId;
                ObjP.Action = 2;
                DataTable dt = objdl.AddSubCategory(ObjP, "saveUpdate_SubCategory");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjP.SubCategoryId = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                    ObjP.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    ObjP.SubCategoryId = 0;
                }
            }
            catch (Exception ex)
            {
                ObjP.SubCategoryId = 0;
            }
            return Json(ObjP, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowHideProduct(int Action, int Id)
        {
            try
            {
                ObjP.EntryBy = Convert.ToString(Session["username"]);
                ObjP.Id = Id;
                ObjP.Action = Action;
                DataTable dt = objdl.AddProduct(ObjP, "USP_ManagePackage");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjP.Id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                    ObjP.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    ObjP.Id = 0;
                }
            }
            catch (Exception ex)
            {
                ObjP.Id = 0;
            }
            return Json(ObjP, JsonRequestBehavior.AllowGet);
        }
        public string BindSubCategory(string CategoryId)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<SelectListItem> lst = new List<SelectListItem>();
            try
            {

                ObjP.EntryBy = Convert.ToString(Session["username"]);
                ObjP.CategoryId = Convert.ToInt32(CategoryId);
                ObjP.Action = 6;
                DataTable dt = objdl.AddProduct(ObjP, "USP_ManagePackage");

                lst = AllClasses.BindDDL(dt);


            }
            catch (Exception ex)
            {
                ObjP.Id = 0;
            }

            return js.Serialize(lst);
        }
        public ActionResult MemberRegistration()
        {
            try
            {
                //ViewBag.CategoryList = AllClasses.BindDDL(objdl.BindCategory(4, 0));


                //ObjP.Action = 2;
                //ObjP.dt = objdl.AddProduct(ObjP, "USP_ManagePackage");
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult MemberRegistration(Registration objp)
        {
            try
            {
                // objp.Pass = CreateRandomPassword(4);
                objp.TranPass = CreateRandomPassword(5);
                objp.Action = 1;
                DataTable dt = objdl.AddMember(objp, "USP_InsertMembers");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["MSG"].ToString();
                    Flag = dt.Rows[0]["ID"].ToString();
                    string msg = "Welcome to " + objp.Name + " ! You have successfully Registered on " + DateTime.Now + " . Your Username is " + dt.Rows[0]["ID"].ToString() + " , Psw: " + objp.Pass + " and Tran Psw: " + objp.TranPass + " Thankyou, ";

                }
                else
                {
                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return RedirectToAction("MemberRegistration");
        }
        public JsonResult GetSposerId(string MemberId)
        {
            Registration objp = new Registration();
            try
            {
                objp.MemberId = MemberId;
                DataTable dt = objdl.MemberReport(objp, "USP_member_details");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.SponserName = dt.Rows[0]["Name"].ToString();

                    objp.Id = 1;
                }
                else
                {
                    objp.Id = 0;
                    objp.msg = "Member Id does not exists!";
                }
            }
            catch (Exception ex)
            {
                objp.Id = 0;
                objp.msg = "Server Not Response!";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
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

        public ActionResult MemberReport()
        {
            Registration objp = new Registration();
            try
            {


                objp.Action = 2;
                objp.dt = objdl.MemberReport(objp, "USP_MemberDetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult ActiveMemberReport()
        {
            Registration objp = new Registration();
            try
            {


                objp.Action = 1;
                objp.dt = objdl.ActiveMemberReport(objp, "USP_Active_MemberDetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult FreeMemberReport()
        {
            Registration objp = new Registration();
            try
            {


                objp.Action = 2;
                objp.dt = objdl.ActiveMemberReport(objp, "USP_Active_MemberDetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult MemberBank()
        {

            return View();
        }

        public JsonResult GetMemberBank(string MemberId)
        {
            Registration objp = new Registration();
            try
            {
                objp.MemberId = MemberId;
                DataTable dt = objdl.MemberReport(objp, "Sp_GetName");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.Name = dt.Rows[0]["payeeName"].ToString();
                    objp.AccountNo = dt.Rows[0]["BankaccNo"].ToString();
                    objp.BankName = dt.Rows[0]["BankName"].ToString();
                    objp.BankBranch = dt.Rows[0]["BankBranch"].ToString();
                    objp.IFSCCode = dt.Rows[0]["BankIfscCode"].ToString();
                    objp.PanNo = dt.Rows[0]["panno"].ToString();
                    objp.Id = 1;
                }
                else
                {
                    objp.Id = 0;
                    objp.msg = "Member Id does not exists!";
                }
            }
            catch (Exception ex)
            {
                objp.Id = 0;
                objp.msg = "Server Not Response!";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult MemberBank(Registration objp)
        {
            try
            {
                objp.EntryBy = Session["username"].ToString();


                DataTable dt = objdl.MemberBank(objp, "Sp_UpdatebankDetails");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["MSG"].ToString(); ;
                    Flag = dt.Rows[0]["ID"].ToString(); ;
                }
                else
                {
                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return RedirectToAction("MemberBank");
        }
        public ActionResult BlockMember()
        {
            Registration objp = new Registration();
            try
            {

                objp.dt = objdl.MemberReport(objp, "BlockMemberReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [HttpPost]
        public ActionResult BlockMember(Registration objp)
        {
            try
            {

                objp.Id = 1;
                DataTable dt = objdl.BlockMember(objp, "Sp_BlockedCustomer");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["MSG"].ToString(); ;
                    Flag = dt.Rows[0]["ID"].ToString(); ;
                }
                else
                {
                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return RedirectToAction("BlockMember");
        }
        public JsonResult GetMemberName(string MemberId)
        {
            Registration objp = new Registration();
            try
            {
                objp.MemberId = MemberId;
                DataTable dt = objdl.GetMemberBankDetails(objp);
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.Name = dt.Rows[0]["Name"].ToString();
                    objp.Id = 1;
                }
                else
                {
                    objp.Id = 0;
                    objp.msg = "Member Id does not exists!";
                }
            }
            catch (Exception ex)
            {
                objp.Id = 0;
                objp.msg = "Server Not Response!";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UnBlockMember()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UnBlockMember(Registration objp)
        {
            try
            {

                objp.Id = 1;
                DataTable dt = objdl.BlockMember(objp, "Sp_UNBlockedCustomer");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["MSG"].ToString(); ;
                    Flag = dt.Rows[0]["ID"].ToString(); ;
                }
                else
                {
                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return RedirectToAction("UnBlockMember");
        }
        public ActionResult LoginBlockMember()
        {
            Registration objp = new Registration();
            try
            {

                objp.dt = objdl.MemberReport(objp, "LoginBlockMemberReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [HttpPost]
        public ActionResult LoginBlockMember(Registration objp)
        {
            try
            {

                objp.Id = 1;
                DataTable dt = objdl.BlockMember(objp, "Sp_BlockedLogin");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["MSG"].ToString(); ;
                    Flag = dt.Rows[0]["ID"].ToString(); ;
                }
                else
                {
                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return RedirectToAction("LoginBlockMember");
        }

        public ActionResult LoginUnBlockMember()
        {

            return View();
        }
        [HttpPost]
        public ActionResult LoginUnBlockMember(Registration objp)
        {
            try
            {

                objp.Id = 1;
                DataTable dt = objdl.BlockMember(objp, "Sp_UNBlockedLogin");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["MSG"].ToString(); ;
                    Flag = dt.Rows[0]["ID"].ToString(); ;
                }
                else
                {
                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return RedirectToAction("LoginUnBlockMember");
        }

        public ActionResult MemberPasswordReport()
        {
            Registration objp = new Registration();
            try
            {


                objp.Action = 2;
                objp.dt = objdl.MemberReport(objp, "USP_ViewPassowrd");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult MemberPassword()
        {

            return View();
        }


        [HttpPost]
        public ActionResult MemberPassword(Registration objp)
        {
            try
            {
                if (objp.ConPass == objp.Pass)
                {
                    objp.EntryBy = Session["username"].ToString();


                    DataTable dt = objdl.MemberPassword(objp, "Sp_A05_UpdateLoginPassword");
                    if (dt != null & dt.Rows.Count > 0)
                    {
                        Msg = dt.Rows[0]["MSG"].ToString(); ;
                        Flag = dt.Rows[0]["ID"].ToString(); ;
                    }
                    else
                    {
                        Flag = "0";
                        Msg = "Server Not Response!";
                    }
                }
                else
                {
                    Flag = "1";
                    Msg = "Password and Confirm Password does not matched.";
                }
            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return RedirectToAction("MemberPassword");
        }
        public ActionResult AdminPassword()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AdminPassword(Registration objp)
        {
            try
            {
                if (objp.ConPass == objp.Pass)
                {
                    objp.MemberId = Session["username"].ToString();


                    DataTable dt = objdl.AdminPassword(objp, "Sp_A06_UpdateAdminLoginPassword");
                    if (dt != null & dt.Rows.Count > 0)
                    {
                        Msg = dt.Rows[0]["MSG"].ToString(); ;
                        Flag = dt.Rows[0]["ID"].ToString(); ;
                    }
                    else
                    {
                        Flag = "0";
                        Msg = "Server Not Response!";
                    }
                }
                else
                {
                    Flag = "1";
                    Msg = "Password and Confirm Password does not matched.";
                }
            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return RedirectToAction("AdminPassword");
        }
        public ActionResult MemberTranPassword()
        {

            return View();
        }
        [HttpPost]
        public ActionResult MemberTranPassword(Registration objp)
        {
            try
            {
                if (objp.ConPass == objp.Pass)
                {
                    // objp.MemberId = Session["username"].ToString();


                    DataTable dt = objdl.MemberPassword(objp, "Sp_A05_UpdateTranPassword");
                    if (dt != null & dt.Rows.Count > 0)
                    {
                        Msg = dt.Rows[0]["MSG"].ToString(); ;
                        Flag = dt.Rows[0]["ID"].ToString(); ;
                    }
                    else
                    {
                        Flag = "0";
                        Msg = "Server Not Response!";
                    }
                }
                else
                {
                    Flag = "1";
                    Msg = "Password and Confirm Password does not matched.";
                }
            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return RedirectToAction("MemberTranPassword");
        }
        /// <summary>
        /// ///      Prahlad singh
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="file1"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public JsonResult UploadMultiImg(string pid, HttpPostedFileBase[] file1)
        {
            string msg = "";
            try
            {
                HttpFileCollectionBase files = Request.Files;
                DataTable dt = new DataTable();
                dt.Columns.Add("ImageName");
                dt.Columns.Add("ProductId");
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];

                    string fileExt = Path.GetExtension(file.FileName);
                    var fileName = Path.GetFileName(file.FileName);
                    string fName = DateTime.Now.Ticks + fileExt;
                    string fname = DateTime.Today.ToString("ddmmyyyy") + "_" + new Random().Next() + Path.GetRandomFileName();

                    string ImagePathThumb = String.Format("/productImagesThumble/{0}{1}", fname, fileExt);

                    string path = Path.Combine(Server.MapPath("/productImagesThumble"), fname + fileExt);
                    file.SaveAs(path);
                    dt.Rows.Add(ImagePathThumb, pid);

                }
                DataTable dt1 = objdl.saveupdatemultiIMG(dt, "1", pid);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    msg = dt1.Rows[0]["msg"].ToString();
                }
                else
                {
                    msg = "Something Went wrong!";
                }
            }
            catch (Exception ex)
            {
                msg = "Something Went wrong!";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult sales_survices()
        {
            cls_sales objS = new cls_sales();
            try
            {

                ViewBag.ProductList = AllClasses.BindDDL(objdl.BindDropdown(2, ""));
            }
            catch (Exception ex)
            { }

            return View(objS);
        }
        public ActionResult StockTransfer()
        {
            cls_sales objS = new cls_sales();
            try
            {

                ViewBag.ProductList = AllClasses.BindDDL(objdl.BindDropdown(2, ""));
            }
            catch (Exception ex)
            { }

            return View(objS);
        }
        //

        // customer sales by admin
        public JsonResult save_sales(cls_sales objT, List<cls_sl> PdList)
        {
            clsWr objBase = new clsWr();
            try
            {
                objT.Dipo_Code = Convert.ToString(Session["UserName"]);
                JavaScriptSerializer js = new JavaScriptSerializer();
                DataTable dtProduct = new DataTable();

                dtProduct.Columns.Add("ProductId", typeof(string));
                dtProduct.Columns.Add("ProductName", typeof(string));
                dtProduct.Columns.Add("Qty", typeof(string));
                dtProduct.Columns.Add("Rate", typeof(string));
                dtProduct.Columns.Add("BV", typeof(string));
                dtProduct.Columns.Add("Amount", typeof(string));

                foreach (var itm in PdList)
                {
                    dtProduct.Rows.Add(itm.ProductId, itm.ProductName, itm.Qty, itm.Rate, itm.BV, itm.Amount);
                }


                objT.dtProduct = dtProduct;



                DataTable dt = objdl.customer_Sales_byAdmin(objT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    objBase.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    objBase.msg = Convert.ToString(dt.Rows[0]["msg"]);
                    objBase.Bill_No = Convert.ToString(dt.Rows[0]["InvoiceNo"]);

                    string msg = "Hello " + Convert.ToString(dt.Rows[0]["Name"]) + "Product Request has been raised from Admin on date " + DateTime.Now.ToString("dd/MMM/yyyy") + " of amount " + Convert.ToString(dt.Rows[0]["Amount"]) + ".Keep Shopping with us ALL THE BEST.";
                    if (Convert.ToString(dt.Rows[0]["Mobile"]) != "")
                    {
                    }


                }
                else
                {
                    objBase.Id = 0;
                    objBase.msg = "Something went wrong!!";
                }

            }
            catch (Exception ex)
            {
                objBase.Id = 0;
                objBase.msg = "Something went wrong!!";
            }
            return Json(objBase, JsonRequestBehavior.AllowGet);
        }
        // Dipo Product Sale
        public JsonResult save_dipo_product(cls_pd_req objT)
        {
            clsWr objBase = new clsWr();
            try
            {
                //objT.Dipo_Code = Convert.ToString(Session["UserName"]);

                JavaScriptSerializer js = new JavaScriptSerializer();
                DataTable dtProduct = new DataTable();

                dtProduct.Columns.Add("Qty", typeof(string));
                dtProduct.Columns.Add("ProductId", typeof(string));
                dtProduct.Columns.Add("Rate", typeof(string));
                dtProduct.Columns.Add("Amount", typeof(string));
                if (objT.PdList != null)
                {
                    List<cls_Pd> result = js.Deserialize<List<cls_Pd>>(objT.PdList);

                    if (result != null && result.Count > 0)
                    {
                        foreach (var itm in result)
                        {
                            dtProduct.Rows.Add(itm.Qty, itm.ProductId, itm.Rate, itm.Amount);
                        }
                    }

                }

                objT.dtProduct = dtProduct;

                DataTable dt = objdl.DipoProductSaleByAdmin(objT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    objBase.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    objBase.msg = Convert.ToString(dt.Rows[0]["msg"]);
                    objBase.Bill_No = Convert.ToString(dt.Rows[0]["InvoiceNo"]);

                    string msg = "Hello " + Convert.ToString(dt.Rows[0]["Name"]) + "Product Request has been raised from Admin on date " + DateTime.Now.ToString("dd/MMM/yyyy") + " of amount " + Convert.ToString(dt.Rows[0]["Amount"]) + ".Keep Shopping with us ALL THE BEST.";
                    if (Convert.ToString(dt.Rows[0]["Mobile"]) != "")
                    {

                    }
                }
                else
                {
                    objBase.Id = 0;
                    objBase.msg = "Something went wrong!!";
                }

            }
            catch (Exception ex)
            {
                objBase.Id = 0;
                objBase.msg = "Something went wrong!!";
            }
            return Json(objBase, JsonRequestBehavior.AllowGet);
        }
        // 
        public JsonResult save_fran_product(cls_Ret objT, List<cls_slSt> PdList)
        {
            clsWr objBase = new clsWr();
            try
            {
                objT.Dipo_Code = Convert.ToString(Session["UserName"]);
                JavaScriptSerializer js = new JavaScriptSerializer();

                DataTable dtProduct = new DataTable();

                dtProduct.Columns.Add("Qty", typeof(string));
                dtProduct.Columns.Add("ProductId", typeof(string));
                dtProduct.Columns.Add("Rate", typeof(string));
                dtProduct.Columns.Add("Amount", typeof(string));
                if (PdList != null)
                {
                    foreach (var itm in PdList)
                    {
                        dtProduct.Rows.Add(itm.Qty, itm.ProductId, itm.Rate, itm.Amount);
                    }

                }

                objT.dtProduct = dtProduct;



                DataTable dt = objdl.fran_product_sale(objT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    objBase.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    objBase.msg = Convert.ToString(dt.Rows[0]["msg"]);
                    objBase.Bill_No = Convert.ToString(dt.Rows[0]["InvoiceNo"]);
                }
                else
                {
                    objBase.Id = 0;
                    objBase.msg = "Something went wrong!!";
                }

            }
            catch (Exception ex)
            {
                objBase.Id = 0;
                objBase.msg = "Something went wrong!!";
            }
            return Json(objBase, JsonRequestBehavior.AllowGet);
        }
        public class Pics
        {
            public HttpPostedFileBase image { get; set; }
        }
        //Assignpermission 
        #region[GUNJA]
        public ActionResult AssignPermission()
        {
            objas.Action = "getrole";
            objas.SessionId = Convert.ToString(Session["SessionId"]);
            ViewBag.Rolelist = AllClasses.BindDDL(objdl.GetAllMaster(objas, "Proc_AllMaster"));

            return View(objas);
        }

        public string BindAllUsers(Itemsdata obj)
        {
            obj.Action = "getuser";
            objas.dt = objdl.BindAllUsers(obj, "Proc_AllMaster");

            string jsondata = objas.ConvertDataTabletoString(objas.dt);

            return jsondata;
        }
        public ActionResult GetMainMenuData(Itemsdata obj)
        {
            string html = "";
            if (obj.Username != null && obj.Username != "0")
            {
                obj.Action = "GetMenuList";
            }
            objas.Dtmainlist = objdl.GetMainMenuData(obj, "Proc_AssignMenu");
            if (objas.Dtmainlist.Rows.Count > 0)
            {
                for (int i = 0; i < objas.Dtmainlist.Rows.Count; i++)
                {
                    string mainchk = "";
                    if (objas.Dtmainlist.Rows[i]["IsActive"].ToString() == "True")
                    {
                        mainchk = "checked= '" + objas.Dtmainlist.Rows[i]["IsActive"].ToString() + "'";
                    }
                    html += "<ul id='u1'><li id='l1'><input type='checkbox' class='chkmainid'  " + mainchk + " value='" + objas.Dtmainlist.Rows[i]["Id"].ToString() + "' />" + objas.Dtmainlist.Rows[i]["MenuTitle"].ToString();

                    //Submenu
                    obj.mainmenuid = objas.Dtmainlist.Rows[i]["Id"].ToString();
                    obj.Action = "GetSubmenuview";
                    objas.Dtsubidlist = objdl.GetSubMenuData(obj, "Proc_AssignMenu");
                    if (objas.Dtsubidlist.Rows.Count > 0)
                    {
                        for (int j = 0; j < objas.Dtsubidlist.Rows.Count; j++)
                        {
                            string subchk = "";
                            if (objas.Dtsubidlist.Rows[j]["S"].ToString() == "True")
                            {
                                subchk = "checked= '" + objas.Dtsubidlist.Rows[j]["S"].ToString() + "'";
                            }
                            string subvalue = objas.Dtmainlist.Rows[i]["Id"].ToString() + "," + objas.Dtsubidlist.Rows[j]["SubmenuId"].ToString();

                            html += "<ul id='u2' class='mv'><li id='l2'><input type='checkbox' class='chksubid' " + subchk + " value='" + subvalue + "' />" + objas.Dtsubidlist.Rows[j]["Submenutitle"].ToString();

                            //Thirdmenu
                            obj.Submenuid = objas.Dtsubidlist.Rows[j]["SubmenuId"].ToString();
                            obj.Action = "Getthird";
                            objas.DtThirdlist = objdl.GetThirdMenuData(obj, "Proc_AssignMenu");
                            if (objas.DtThirdlist.Rows.Count > 0)
                            {
                                for (int K = 0; K < objas.DtThirdlist.Rows.Count; K++)
                                {
                                    string Thirdchk = "";
                                    if (objas.DtThirdlist.Rows[K]["S"].ToString() == "True")
                                    {
                                        Thirdchk = "checked= '" + objas.DtThirdlist.Rows[K]["S"].ToString() + "'";
                                    }
                                    string Thirdvalue = objas.Dtmainlist.Rows[i]["Id"].ToString() + "," + objas.Dtsubidlist.Rows[j]["SubmenuId"].ToString() + "," + objas.DtThirdlist.Rows[K]["thirdid"].ToString();

                                    html += "<ul id='u2' class='mv'><li id='l2'><input type='checkbox' class='chkthirdbid' " + Thirdchk + " value='" + Thirdvalue + "' />" + objas.DtThirdlist.Rows[K]["thirdtitle"].ToString() + "</li></ul>";
                                }
                            }

                            //End Thirdmenu
                            html += "</li></ul>";
                        }
                    }
                    //End submenu
                    html += "</li></ul>";
                }
            }

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertMenuPermission(Itemsdata obj, string[] multivalue1, string[] multivalue2, string[] multivalue3)
        {
            obj.SessionId = Convert.ToString(Session["SessionId"]);
            obj.Action = "Del";
            DataTable dt = objdl.DeleteMenuper(obj, "Proc_AssignMenu");
            string msg = "F";
            int count = 0;
            //Mainmenu
            if (multivalue1 != null)
            {
                if (multivalue1.Length > 0)
                {
                    foreach (string s in multivalue1)
                    {
                        obj.mainmenuid = s;
                        obj.ActvStatus = "1";

                        //Submenu
                        if (multivalue2 != null)
                        {
                            if (multivalue2.Length > 0)
                            {
                                foreach (string s2 in multivalue2)
                                {
                                    string[] valuereg = s2.Split(',');
                                    if (s == valuereg[0])
                                    {
                                        obj.Submenuid = valuereg[1];
                                        //Thirdmenu
                                        if (multivalue3 != null)
                                        {
                                            if (multivalue3.Length > 0)
                                            {
                                                foreach (string s3 in multivalue3)
                                                {
                                                    string[] valuereg3 = s3.Split(',');
                                                    if (obj.Submenuid == valuereg3[1])
                                                    {
                                                        obj.thirdid = valuereg3[2];
                                                        obj.Action = "InrtAssignRoleT";
                                                        int r2 = objdl.InsertMenuPermission(obj, "Proc_AssignMenu");
                                                        if (r2 > 0)
                                                        {
                                                            count = count + 1;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        obj.Action = "InrtAssignRoleS";
                                                        int r3 = objdl.InsertMenuPermission(obj, "Proc_AssignMenu");
                                                        if (r3 > 0)
                                                        {
                                                            count = count + 1;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            obj.Action = "InrtAssignRoleS";
                                            int r3 = objdl.InsertMenuPermission(obj, "Proc_AssignMenu");
                                            if (r3 > 0)
                                            {
                                                count = count + 1;
                                            }
                                        }
                                        //End third Main
                                    }
                                    else
                                    {
                                        obj.Action = "InrtAssignRoleM";
                                        int r1 = objdl.InsertMenuPermission(obj, "Proc_AssignMenu");
                                        if (r1 > 0)
                                        {
                                            count = count + 1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            obj.Action = "InrtAssignRoleM";
                            int r = objdl.InsertMenuPermission(obj, "Proc_AssignMenu");
                            if (r > 0)
                            {
                                count = count + 1;
                            }
                        }
                        //End sub Main
                    }
                }
            }
            //End Main
            if (count > 0)
            {
                msg = "S";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMenuList()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("login", "Account");

            }
            objas.Action = "1";
            objas.EntryBy = Convert.ToString(Session["usercode"]);


            objas.Role = Convert.ToString(Session["Role"]);
            DataSet ds = objdl.getMenuConfiguration(objas, "Proc_getMenuConfiguration");

            if (ds != null)
            {

                objas.dt = ds.Tables[0];
                objas.dt1 = ds.Tables[1];
                objas.dt2 = ds.Tables[2];
            }

            return View(objas);
        }


        public ActionResult CreateUser(string user_code)
        {
            Registration cr = new Registration();

            if (Session["username"] != null)
            {
                cr.SessionId = Convert.ToString(Session["username"]);


                cr.dt = objdl.AddUserDetails(2, cr);

            }

            else
            {
                Response.Redirect("Account/login");
            }
            if (user_code != null)
            {
                cr.SessionId = Convert.ToString(Session["username"]);
                cr.user_code = user_code;
                DataTable dt = objdl.EditUserDetails(3, cr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    cr.Name = dt.Rows[0]["username"].ToString();
                    cr.Pass = dt.Rows[0]["Pass"].ToString();
                    cr.MobileNo = dt.Rows[0]["MobileNo"].ToString();

                }
            }



            return View(cr);
        }

        [HttpPost]
        public ActionResult CreateUser(Registration cr)
        {

            if (Session["username"] != null)
            {
                cr.SessionId = Convert.ToString(Session["username"]);

                if (cr.user_code != null && cr.user_code != null)
                {
                    cr.dt = objdl.AddUserDetails(4, cr);
                    TempData["msg"] = "User Details Update Successfully";

                }
                else
                {
                    cr.dt = objdl.AddUserDetails(1, cr);
                    TempData["msg"] = "User Details Save Successfully";
                }


            }

            else
            {
                Response.Redirect("Account/login");
            }



            return RedirectToAction("CreateUser", new { });
        }

        #endregion
        #region[Radhe]
        public ActionResult AllCustomersFund()
        {
            Registration objp = new Registration();
            try
            {


                objp.Action = 3;
                objp.dt = objdl.AllCustomersFund(objp, "Sp_FundRequestReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [HttpPost]
        public ActionResult AllCustomersFund(Registration objp)
        {

            try
            {


                objp.Action = 7;
                objp.dt = objdl.AllCustomersFund(objp, "Sp_FundRequestReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult FranchiseWallet()
        {
            Registration objp = new Registration();
            try
            {

                objp.Action = 4;
                objp.dt = objdl.FranchiseWallet(objp, "Sp_FundRequestReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [HttpPost]
        public ActionResult FranchiseWallet(Registration objp)
        {

            try
            {

                objp.Action = 6;
                objp.dt = objdl.FranchiseWallet(objp, "Sp_FundRequestReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult FranchiseDepoStock()
        {
            Registration objp = new Registration();
            try
            {

                objp.Action = 5;
                objp.dt = objdl.FranchiseDepoStock(objp, "Sp_FundRequestReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        #endregion
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml, string Remark)
        {
            Registration objRpt = new Registration();
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", Remark + ".xls");
        }

        #region SMS Template
        public ActionResult SMSTemplate()
        {
            return View();
        }
        public JsonResult GetfranchiseeList()
        {
            List<cls_Ret> studentList = new List<cls_Ret>();
            DataTable dt = new DataTable();
            try
            {
                dt = objdl.SMSFranchiseDetails();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cls_Ret objBase = new cls_Ret();
                    objBase.Franchise_Id = dt.Rows[i]["Franchise_Id"].ToString();
                    objBase.Name = dt.Rows[i]["Name"].ToString();
                    objBase.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                    if (dt.Rows[i]["Pass"] == null)
                    {
                        objBase.psw = "";
                    }
                    else
                    {
                        objBase.psw = dt.Rows[i]["Pass"].ToString();
                    }
                    studentList.Add(objBase);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(studentList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDipoList()
        {
            List<cls_Ret> studentList = new List<cls_Ret>();
            DataTable dt = new DataTable();
            try
            {
                dt = objdl.SMSDepoDetails();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cls_Ret objBase = new cls_Ret();
                    objBase.Franchise_Id = dt.Rows[i]["DIPO_Code"].ToString();
                    objBase.Name = dt.Rows[i]["Name"].ToString();
                    objBase.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                    if (dt.Rows[i]["Pass"] == null)
                    {
                        objBase.psw = "";
                    }
                    else
                    {
                        objBase.psw = dt.Rows[i]["Pass"].ToString();
                    }
                    studentList.Add(objBase);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(studentList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSMSMemberDetails(cls_Ret objB)
        {
            List<cls_Ret> studentList = new List<cls_Ret>();
            DataTable dt = new DataTable();
            try
            {
                dt = objdl.SMSMemberDetails(objB);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cls_Ret objBase = new cls_Ret();
                    objBase.Franchise_Id = dt.Rows[i]["Member_Id"].ToString();
                    objBase.Name = dt.Rows[i]["Name"].ToString();
                    objBase.MobileNo = dt.Rows[i]["mobile"].ToString();
                    if (dt.Rows[i]["psw"] == null)
                    {
                        objBase.psw = "";
                    }
                    else
                    {
                        objBase.psw = dt.Rows[i]["psw"].ToString();
                    }
                    studentList.Add(objBase);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(studentList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendSms(string MemberId, string MemeberName, string MobileNo, string Password)
        {
            string message = " Dear " + MemeberName + ", Thank you for choosing SigmaIT for your recent purchase and trusting us with your online presence. We have received your order. For any support please login on  with username : " + MemberId + " and password : " + Password + ". Regards SigmaIT";
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public string apicall(string url)
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
        public JsonResult SendSmsDetail(string strrawdata)
        {
            cls_Ret objP = new cls_Ret();
            string msg = "";
            objP.EntryBy = Convert.ToString(Session["UserID"]);

            if (strrawdata != null)
            {
                Hashtable[] hs = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Hashtable[]>(strrawdata);
                DataTable dtnew = new DataTable();
                dtnew.Columns.Add("Member_Id", typeof(string));
                dtnew.Columns.Add("Name", typeof(string));
                dtnew.Columns.Add("mobile", typeof(string));
                dtnew.Columns.Add("psw", typeof(string));

                if (hs != null && hs.Length > 0)
                {
                    foreach (Hashtable item in hs)
                    {
                        dtnew.Rows.Add(item["memberid"], item["membername"], item["memberMobileNo"], item["memberpsw"]);
                        for (int i = 0; i < dtnew.Rows.Count; i++)
                        {
                            string MemberId = dtnew.Rows[i]["Member_Id"].ToString();
                            string MemeberName = dtnew.Rows[i]["Name"].ToString();
                            string MobileNo = dtnew.Rows[i]["mobile"].ToString();
                            string Password = dtnew.Rows[i]["psw"].ToString();
                            SendSms(MemberId, MemeberName, MobileNo, Password);
                        }
                    }
                    objP.dtProduct = dtnew;
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendDepoDetail(string strrawdata)
        {
            cls_Ret objP = new cls_Ret();
            string msg = "";
            objP.EntryBy = Convert.ToString(Session["UserID"]);

            if (strrawdata != null)
            {
                Hashtable[] hs = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Hashtable[]>(strrawdata);
                DataTable dtnew = new DataTable();
                dtnew.Columns.Add("Depoid", typeof(string));
                dtnew.Columns.Add("DepoName", typeof(string));
                dtnew.Columns.Add("DepoNameMobileNo", typeof(string));
                dtnew.Columns.Add("Dipopsw", typeof(string));

                if (hs != null && hs.Length > 0)
                {
                    foreach (Hashtable item in hs)
                    {
                        dtnew.Rows.Add(item["Depoid"], item["DepoName"], item["DepoNameMobileNo"], item["Dipopsw"]);
                        for (int i = 0; i < dtnew.Rows.Count; i++)
                        {
                            string MemberId = dtnew.Rows[i]["Depoid"].ToString();
                            string MemeberName = dtnew.Rows[i]["DepoName"].ToString();
                            string MobileNo = dtnew.Rows[i]["DepoNameMobileNo"].ToString();
                            string Password = dtnew.Rows[i]["Dipopsw"].ToString();
                            SendSms(MemberId, MemeberName, MobileNo, Password);
                        }
                    }
                    objP.dtProduct = dtnew;
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendFranchiseDetail(string strrawdata)
        {
            cls_Ret objP = new cls_Ret();
            string msg = "";
            objP.EntryBy = Convert.ToString(Session["UserID"]);

            if (strrawdata != null)
            {
                Hashtable[] hs = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Hashtable[]>(strrawdata);
                DataTable dtnew = new DataTable();
                dtnew.Columns.Add("Franchiseid", typeof(string));
                dtnew.Columns.Add("FranchiseName", typeof(string));
                dtnew.Columns.Add("FranchiseMobileNo", typeof(string));
                dtnew.Columns.Add("Franchisepsw", typeof(string));

                if (hs != null && hs.Length > 0)
                {
                    foreach (Hashtable item in hs)
                    {
                        dtnew.Rows.Add(item["Franchiseid"], item["FranchiseName"], item["FranchiseMobileNo"], item["Franchisepsw"]);
                        for (int i = 0; i < dtnew.Rows.Count; i++)
                        {
                            string MemberId = dtnew.Rows[i]["Franchiseid"].ToString();
                            string MemeberName = dtnew.Rows[i]["FranchiseName"].ToString();
                            string MobileNo = dtnew.Rows[i]["FranchiseMobileNo"].ToString();
                            string Password = dtnew.Rows[i]["Franchisepsw"].ToString();
                            SendSms(MemberId, MemeberName, MobileNo, Password);
                        }
                    }
                    objP.dtProduct = dtnew;
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        #region Video Master
        public ActionResult VideoMaster()
        {
            Master cl = new Master();
            DataTable dt = new DataTable();
            // TempData["flag"] = null;
            // TempData["msg"] = null;
            try
            {
                cl.Catdt = objdl.GetVideoMasterList();
            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }
        [HttpPost]
        public ActionResult VideoMaster(Master cl, HttpPostedFileBase VideoFile)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string myfilePack2 = "";
            try
            {
                if (cl.Videoid != 0)
                {
                    cl.Action = 2;
                    if (VideoFile != null)
                    {
                        HttpPostedFileBase packimg1 = VideoFile;
                        string pack1 = Path.GetExtension(packimg1.FileName);
                        var fileNamepack1 = Path.GetFileName(packimg1.FileName);
                        string fNamePack1 = DateTime.Now.Ticks.ToString();

                        string ImagePathPack11 = String.Format("/UploadFiles/{0}{1}", fNamePack1, pack1);
                        myfilePack2 = "/UploadFiles/" + fNamePack1 + pack1;
                        packimg1.SaveAs(Server.MapPath(ImagePathPack11));
                        cl.Catdt = dt1;
                    }
                    if (cl.Type != null && cl.VideoTitle != null)
                    {
                        dt = objdl.AddUpdateVideoMaster(cl, "Sp_AddMovieMaster", myfilePack2);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Msg = dt.Rows[0]["msg"].ToString();
                            Flag = dt.Rows[0]["Id"].ToString();
                        }
                        else
                        {
                            Msg = "Some Error Occured";
                        }
                    }
                }
                else
                {
                    cl.Action = 1;
                    if (VideoFile != null && cl.Type != null && cl.VideoTitle != null)
                    {
                        HttpPostedFileBase packimg1 = VideoFile;
                        string pack1 = Path.GetExtension(packimg1.FileName);
                        var fileNamepack1 = Path.GetFileName(packimg1.FileName);
                        string fNamePack1 = DateTime.Now.Ticks.ToString();

                        string ImagePathPack11 = String.Format("/UploadFiles/{0}{1}", fNamePack1, pack1);
                        myfilePack2 = "/UploadFiles/" + fNamePack1 + pack1;
                        packimg1.SaveAs(Server.MapPath(ImagePathPack11));
                        cl.Catdt = dt1;
                        dt = objdl.AddUpdateVideoMaster(cl, "Sp_AddMovieMaster", myfilePack2);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Msg = dt.Rows[0]["msg"].ToString();
                            Flag = dt.Rows[0]["Id"].ToString();
                        }
                        else
                        {
                            Msg = "Some Error Occured";
                        }
                    }
                    else
                    {
                        Msg = "Please Fill All Data";
                    }
                }
            }
            catch (Exception ex)
            {
            }
            cl.Catdt = objdl.GetVideoMasterList();
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return View(cl);
        }
        public JsonResult GetService(Master cl, string ID)
        {
            DataTable dt = new DataTable();
            dt = objdl.GetVideodata(ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                cl.Videoid = Convert.ToInt32(ID);
                cl.VideoTitle = dt.Rows[0]["Title"].ToString();
                cl.VideoFile = dt.Rows[0]["Video"].ToString();
                cl.Type = dt.Rows[0]["Type"].ToString();
            }
            return Json(cl, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteVideoMaster(Master cl, string ID)
        {
            DataTable dt = new DataTable();
            cl.Catdt = objdl.DeleteVideoMaster(ID);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region How To Use
        public ActionResult HowToUse()
        {
            Master cl = new Master();
            DataTable dt = new DataTable();
            TempData["flag"] = "";
            TempData["msg"] = "";
            try
            {
                cl.Catdt = objdl.GetHowToUseMasterList();
            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }
        [HttpPost]
        public ActionResult HowToUse(Master cl, HttpPostedFileBase VideoFile)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string myfilePack2 = "";
            try
            {
                if (cl.Entryid != 0)
                {
                    cl.Action = 2;
                    if (VideoFile != null)
                    {
                        HttpPostedFileBase packimg1 = VideoFile;
                        string pack1 = Path.GetExtension(packimg1.FileName);
                        var fileNamepack1 = Path.GetFileName(packimg1.FileName);
                        string fNamePack1 = DateTime.Now.Ticks.ToString();

                        string ImagePathPack11 = String.Format("/UploadFiles/{0}{1}", fNamePack1, pack1);
                        myfilePack2 = "/UploadFiles/" + fNamePack1 + pack1;
                        packimg1.SaveAs(Server.MapPath(ImagePathPack11));
                        cl.Catdt = dt1;
                    }
                    if (cl.Type != null && cl.VideoTitle != null)
                    {
                        dt = objdl.AddUpdateHowToUseMaster(cl, "Sp_AddHowToUseMaster", myfilePack2);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Msg = dt.Rows[0]["msg"].ToString();
                            Flag = dt.Rows[0]["Id"].ToString();
                        }
                        else
                        {
                            Msg = "Some Error Occured";
                        }
                    }
                }
                else
                {
                    cl.Action = 1;
                    if (VideoFile != null && cl.Type != null && cl.VideoTitle != null)
                    {
                        HttpPostedFileBase packimg1 = VideoFile;
                        string pack1 = Path.GetExtension(packimg1.FileName);
                        var fileNamepack1 = Path.GetFileName(packimg1.FileName);
                        string fNamePack1 = DateTime.Now.Ticks.ToString();

                        string ImagePathPack11 = String.Format("/UploadFiles/{0}{1}", fNamePack1, pack1);
                        myfilePack2 = "/UploadFiles/" + fNamePack1 + pack1;
                        packimg1.SaveAs(Server.MapPath(ImagePathPack11));
                        cl.Catdt = dt1;
                        dt = objdl.AddUpdateHowToUseMaster(cl, "Sp_AddHowToUseMaster", myfilePack2);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Msg = dt.Rows[0]["msg"].ToString();
                            Flag = dt.Rows[0]["Id"].ToString();
                        }
                        else
                        {
                            Msg = "Some Error Occured";
                        }
                    }
                    else
                    {
                        Msg = "Please Fill All Data";
                    }
                }

            }
            catch (Exception ex)
            {

            }
            cl.Catdt = objdl.GetHowToUseMasterList();
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return View(cl);
        }
        public JsonResult GetHowToUsedata(Master cl, string ID)
        {
            DataTable dt = new DataTable();
            dt = objdl.GetHowToUsedata(ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                cl.Videoid = Convert.ToInt32(ID);
                cl.VideoTitle = dt.Rows[0]["Title"].ToString();
                cl.VideoFile = dt.Rows[0]["Video"].ToString();
                cl.Type = dt.Rows[0]["Type"].ToString();
            }
            return Json(cl, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteHowToUseMaster(Master cl, string ID)
        {
            DataTable dt = new DataTable();
            cl.Catdt = objdl.DeleteHowToUseMaster(ID);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
        public ActionResult ProductWiseSaleReport()
        {
            Report cl = new Report();

            if (Session["username"] != null)
            {
                cl.ProductLst = AllClasses.BindDDL(objdl.BindDropdown(2, ""));

                cl.dt = objdl.ProductWiseSale(cl);

            }
            else
            {
                Response.Redirect("Authentication/Login");
            }

            return View(cl);
        }
        [HttpPost]
        public ActionResult ProductWiseSaleReport(Report cl)
        {
            if (Session["username"] != null)
            {
                cl.dt = objdl.ProductWiseSale(cl);
                cl.ProductLst = AllClasses.BindDDL(objdl.BindDropdown(2, ""));

            }
            else
            {
                Response.Redirect("Authentication/Login");
            }

            return View(cl);
        }
        [HttpGet]
        public ActionResult InvoiceReport()
        {
            PayoutReport cl = new PayoutReport();

            if (Session["username"] != null)
            {
                cl.Action = "2";
                cl.dt = objdl.InvoiceReportByType(cl);
                cl.msg = "Member Invoice Report.";
            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(cl);
        }
        [HttpPost]
        public ActionResult InvoiceReport(PayoutReport cl)
        {
            if (Session["username"] != null)
            {
                if (cl.Action == "3")
                {
                    cl.msg = "Franchise Invoice Report.";
                }
                if (cl.Action == "4")
                {
                    cl.msg = "Dipo Invoice Report.";
                }

                cl.dt = objdl.InvoiceReportByType(cl);
            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(cl);
        }
        #region popupImage Upload

        public ActionResult PopupUpload(Master cl)
        {
            try
            {
                cl.Catdt = objdl.GetPopupUploadList();
            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }
        [HttpPost]
        public ActionResult PopupUpload(Master cl, HttpPostedFileBase IamgeFile)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string myfilePack2 = "";
            try
            {
                if (cl.Entryid != 0)
                {
                    cl.Action = 2;
                    if (IamgeFile != null)
                    {
                        HttpPostedFileBase packimg1 = IamgeFile;
                        string pack1 = Path.GetExtension(packimg1.FileName);
                        var fileNamepack1 = Path.GetFileName(packimg1.FileName);
                        string fNamePack1 = DateTime.Now.Ticks.ToString();

                        string ImagePathPack11 = String.Format("/UploadFiles/{0}{1}", fNamePack1, pack1);
                        myfilePack2 = "/UploadFiles/" + fNamePack1 + pack1;
                        packimg1.SaveAs(Server.MapPath(ImagePathPack11));
                        cl.Catdt = dt1;
                    }
                    if (cl.ImageTitle != null)
                    {
                        dt = objdl.AddUpdatePopupUpload(cl, "AddPopupUploadMaster", myfilePack2);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Msg = dt.Rows[0]["msg"].ToString();
                            Flag = dt.Rows[0]["Id"].ToString();
                        }
                        else
                        {
                            Msg = "Some Error Occured";
                        }
                    }
                }
                else
                {
                    cl.Action = 1;
                    if (IamgeFile != null && cl.ImageTitle != null)
                    {
                        HttpPostedFileBase packimg1 = IamgeFile;
                        string pack1 = Path.GetExtension(packimg1.FileName);
                        var fileNamepack1 = Path.GetFileName(packimg1.FileName);
                        string fNamePack1 = DateTime.Now.Ticks.ToString();

                        string ImagePathPack11 = String.Format("/UploadFiles/{0}{1}", fNamePack1, pack1);
                        myfilePack2 = "/UploadFiles/" + fNamePack1 + pack1;
                        packimg1.SaveAs(Server.MapPath(ImagePathPack11));
                        cl.Catdt = dt1;
                        dt = objdl.AddUpdatePopupUpload(cl, "AddPopupUploadMaster", myfilePack2);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Msg = dt.Rows[0]["msg"].ToString();
                            Flag = dt.Rows[0]["Id"].ToString();
                        }
                        else
                        {
                            Msg = "Some Error Occured";
                        }
                    }
                    else
                    {
                        Msg = "Please Fill All Data";
                        Flag = "1";
                    }
                }

            }
            catch (Exception ex)
            {

            }
            cl.Catdt = objdl.GetPopupUploadList();
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;
            return View(cl);
        }

        public JsonResult GetPopupUploadDataForEdit(Master cl, string ID)
        {
            DataTable dt = new DataTable();
            dt = objdl.GetPopupUploadDataForEdit(ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                cl.Entryid = Convert.ToInt32(ID);
                cl.VideoTitle = dt.Rows[0]["Title"].ToString();
                cl.VideoFile = dt.Rows[0]["ImageFile"].ToString();
                cl.Type = dt.Rows[0]["Type"].ToString();
            }
            return Json(cl, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeletePopupUploadMaster(Master cl, string ID)
        {
            DataTable dt = new DataTable();
            cl.Catdt = objdl.DeletePopupUploadMaster(ID);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult MemberEditProfile()
        {
            Registration mr = new Registration();
            string Member_Id = "";
            if (Request.QueryString["Member_Id"] != null && Request.QueryString["Member_Id"].ToString() != "")
            {
                Member_Id = Request.QueryString["Member_Id"].ToString();
            }

            if (Session["username"] != null)
            {
                if (Member_Id != null && Member_Id != "")
                {
                    mr.MemberId = Member_Id;

                    mr.PhoneCodeLst = AllClasses.BindDDL(objdl.BindPhoneCode());
                    mr.StateDDLLst = AllClasses.BindDDL(objdl.BindState());



                    mr.dt = objdl.GetMemberEditDetails(mr);
                    if (mr.dt != null && mr.dt.Rows.Count > 0)
                    {
                        int StateId = 0, CountryCode = 0;
                        int.TryParse(Convert.ToString(mr.dt.Rows[0]["States"].ToString()), out StateId);
                        int.TryParse(Convert.ToString(mr.dt.Rows[0]["Countrycode"].ToString()), out CountryCode);
                        mr.MobileNo = mr.dt.Rows[0]["mobile"].ToString();
                        mr.IFSCCode = mr.dt.Rows[0]["BankIfscCode"].ToString();
                        mr.Name = mr.dt.Rows[0]["Name"].ToString();
                        mr.BankName = mr.dt.Rows[0]["BankName"].ToString();
                        mr.FatherName = mr.dt.Rows[0]["f_name"].ToString();
                        mr.BankBranch = mr.dt.Rows[0]["BankBranch"].ToString();
                        mr.RegDate = mr.dt.Rows[0]["regDate"].ToString();
                        mr.AccountNo = mr.dt.Rows[0]["BankaccNo"].ToString();
                        mr.PanNo = mr.dt.Rows[0]["panno"].ToString();
                        mr.AdharNo = mr.dt.Rows[0]["AdharNo"].ToString();
                        mr.EmailId = mr.dt.Rows[0]["Email"].ToString();
                        mr.SponserId = mr.dt.Rows[0]["Intro_Id"].ToString();
                        mr.ZipCode = mr.dt.Rows[0]["ZipCode"].ToString();
                        mr.SponserName = mr.dt.Rows[0]["SponserName"].ToString();
                        mr.NomName = mr.dt.Rows[0]["NomName"].ToString();
                        mr.StateId = StateId;
                        mr.DistrictDDLLst = AllClasses.BindDDL(objdl.GetMasterData(1, mr.StateId.ToString()));
                        mr.District = mr.dt.Rows[0]["District"].ToString();
                        mr.NomFatherName = mr.dt.Rows[0]["NomFahter"].ToString();
                        mr.City = mr.dt.Rows[0]["City"].ToString();
                        mr.NomAddressName = mr.dt.Rows[0]["NomAddress"].ToString();
                        mr.Address = mr.dt.Rows[0]["Address"].ToString();
                        mr.NomRelationName = mr.dt.Rows[0]["NomRelation"].ToString();
                        mr.LandMark = mr.dt.Rows[0]["LandMark"].ToString();
                        mr.KYCStatus = mr.dt.Rows[0]["KYCStatus"].ToString();
                        mr.DOB = mr.dt.Rows[0]["MDob"].ToString();
                        mr.CountryCode = CountryCode;
                        mr.Gender = mr.dt.Rows[0]["Gender"].ToString();
                        mr.PanFilePath = mr.dt.Rows[0]["PANCopy2"].ToString();
                        mr.AdharFilePath = mr.dt.Rows[0]["AADHARCopy2"].ToString();
                        mr.BankPassbookFilePath = mr.dt.Rows[0]["Passbookcopyy"].ToString();
                        mr.AdharbackFilePath = mr.dt.Rows[0]["AdharBackCopyy"].ToString();
                        mr.CountryCode = CountryCode;




                    }
                }



            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(mr);
        }
        [HttpPost]
        public ActionResult MemberEditProfile(Registration mr, HttpPostedFileBase ProfilePic, HttpPostedFileBase PAN, HttpPostedFileBase Adhar, HttpPostedFileBase BankPassbook, HttpPostedFileBase Adharback)
        {
            if (Session["username"] != null)
            {
                //mr.MemberId = Convert.ToString(Session["username"]);

                string ProfilePic1 = string.Empty;
                string ProfilePic2 = string.Empty;

                string ProfilePic3 = string.Empty;
                string ProfilePic4 = string.Empty;

                //mr.MemberId = Convert.ToString(Session["username"]);
                mr.PhoneCodeLst = AllClasses.BindDDL(objdl.BindPhoneCode());
                mr.StateDDLLst = AllClasses.BindDDL(objdl.BindState());
                mr.DistrictDDLLst = AllClasses.BindDDL(new DataTable());

                if (ProfilePic != null)
                {
                    string FName = "";
                    FName = "EmpProfile" + DateTime.Now.Ticks + ProfilePic.FileName;
                    string FileUrl = "/ProfilePic/" + FName;
                    string Cpath = Path.Combine(Server.MapPath("~/ProfilePic/"), FName);
                    ProfilePic.SaveAs(Cpath);
                    mr.ProfilePic = FName;
                    // dl.UpdateProfile(FileUrl, FileDetail_Id, objdata.FileNo, 1);
                    // ViewBag.Message = "File uploaded successfully.";
                }

                if (PAN != null)
                {


                    System.Drawing.Image img = System.Drawing.Image.FromStream(PAN.InputStream);
                    int height = img.Height;
                    int width = img.Width;

                    // imageFilePath1 = Server.MapPath("/Images") + "/default1.png";
                    ProfilePic1 = Path.GetFileName(DateTime.Now + PAN.FileName);
                    PAN.SaveAs(Server.MapPath("/KYC/" + ProfilePic1));
                    mr.PanFilePath = ProfilePic1;
                }
                if (Adhar != null)
                {

                    System.Drawing.Image img = System.Drawing.Image.FromStream(Adhar.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    ProfilePic2 = Path.GetFileName(DateTime.Now + Adhar.FileName);
                    Adhar.SaveAs(Server.MapPath("/KYC/" + ProfilePic2));
                    mr.AdharFilePath = ProfilePic2;

                }

                if (BankPassbook != null)
                {

                    System.Drawing.Image img = System.Drawing.Image.FromStream(BankPassbook.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    ProfilePic3 = Path.GetFileName(DateTime.Now + BankPassbook.FileName);
                    BankPassbook.SaveAs(Server.MapPath("/KYC/" + ProfilePic3));
                    mr.BankPassbookFilePath = ProfilePic3;

                }
                if (Adharback != null)
                {

                    System.Drawing.Image img = System.Drawing.Image.FromStream(Adharback.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    ProfilePic4 = Path.GetFileName(DateTime.Now + Adharback.FileName);
                    Adharback.SaveAs(Server.MapPath("/KYC/" + ProfilePic4));
                    mr.AdharbackFilePath = ProfilePic4;
                }


                DataTable dt = objdl.Asso_Member_Update(mr, "Sp_U01_UpdateUserDetails");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = "Profile Updated Successfully.";
                    Flag = "1";
                    //ModelState.Clear();
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
        public ActionResult rank_wise_report()
        {
            Registration objp = new Registration();
            try
            {
                objp.RankLst = AllClasses.BindDDL(objdl.BindDropdown(6, ""));
                objp.MemberId = "MP00001";
                objp.dt = objdl.rank_wise_report(objp, "sp_get_Rank_Details");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [HttpPost]
        public ActionResult rank_wise_report(Registration objp)
        {
            try
            {
                objp.dt = objdl.rank_wise_report(objp, "sp_get_Rank_Details");
                objp.RankLst = AllClasses.BindDDL(objdl.BindDropdown(6, ""));
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult Franchise_dipo_dp_bv()
        {
            Registration objp = new Registration();
            try
            {

                objp.Action = 6;
                objp.dt = objdl.FranchiseWallet(objp, "Sp_FundRequestReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [HttpPost]
        public ActionResult Franchise_dipo_dp_bv(Registration objp)
        {
            try
            {
                objp.Action = 6;
                objp.dt = objdl.FranchiseWallet(objp, "Sp_FundRequestReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult RejectedProductRequestList()
        {
            PayoutReport cl = new PayoutReport();
            if (Session["username"] != null)
            {
                cl.Status = "Rejected";
                cl.dt = objdl.USP_VerfyCustomerOrder("5", cl);

            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(cl);
        }
        [HttpPost]
        public ActionResult RejectedProductRequestList(PayoutReport cl)
        {
            if (Session["username"] != null)
            {
                cl.Status = "Rejected";
                cl.dt = objdl.USP_VerfyCustomerOrder("5", cl);

            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(cl);
        }
        public ActionResult WeeklyPaidUnPaidReport()
        {
            PayoutReport cl = new PayoutReport();
            try
            {

                //objp.Action = 6;
                //objp.dt = objdl.FranchiseWallet(objp, "Sp_FundRequestReport");
            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }
        [HttpPost]
        public ActionResult WeeklyPaidUnPaidReport(PayoutReport cl)
        {

            try
            {
                if (cl.Action == "Paid")
                {
                    cl.dt = objdl.sp_BankStatementReportPaidUnpaid("sp_BankStatementReportPaidUnpaid", cl);
                }

                if (cl.Action == "Unpaid")
                {
                    cl.dt = objdl.sp_BankStatementReportPaidUnpaid("sp_BankStatementReportPaidUnpaid", cl);
                }


            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }
        [HttpPost, ValidateInput(false)]
        public FileResult ExportPaidUnPaidReport(string GridHtml, string Remark)
        {
            PayoutReport objRpt = new PayoutReport();
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", Remark + ".xls");
        }
        public ActionResult PurchaseFromReplicatedWebsite()
        {
            PayoutReport cl = new PayoutReport();
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }
        [HttpPost]
        public ActionResult PurchaseFromReplicatedWebsite(PayoutReport cl)
        {

            try
            {
                if (Session["username"] != null)
                {
                    //cl.FromDate = DateTime.Now.ToString("dd-MMM-yyyy");
                    //cl.ToDate = DateTime.Now.ToString("dd-MMM-yyyy");
                    if (cl.OrderType == "E")
                    {
                        cl.dt = objdl.USP_VerfyCustomerOrder("6", cl);
                    }

                    if (cl.OrderType == "P")
                    {
                        cl.dt = objdl.USP_VerfyCustomerOrder("6", cl);
                    }


                }
            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }
        public ActionResult Report_IP()
        {
            Master ms = new Master();
            if (Session["username"] != null)
            {
                DataTable dt = new DataTable();
                ms.Action = 2;
                dt = Datalayer.setIPAddress(ms, "Proc_setIPAddress");
                ms.dt = dt;
            }
            else
            {
                Response.Redirect("Account/login");
            }
            return View(ms);
        }
        public ActionResult Replicated_web_details()
        {
            Registration objp = new Registration();
            try
            {

                objp.Action = 4;
                objp.FromDate = null;
                objp.ToDate = null;
                objp.dt = objdl.GetMasterData(7, objp.MemberId, objp.FromDate, objp.ToDate);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [HttpPost]
        public ActionResult Replicated_web_details(Registration objp)
        {

            try
            {

                objp.Action = 6;
                objp.dt = objdl.GetMasterData(7, objp.MemberId, objp.FromDate, objp.ToDate);
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        //public JsonResult Paid_unpaidPayout(string data)
        //{
        //    int Result = 0;
        //    try
        //    {
        //        JavaScriptSerializer js = new JavaScriptSerializer();

        //        if (data != null)
        //        {

        //            List<PayoutReport> lst = js.Deserialize<List<PayoutReport>>(data);

        //            if (lst.Count > 0)
        //            {
        //                Result = objdl.Save_paid_pending_payoutreport(lst);
        //            }

        //            if (Result > 0)
        //            {
        //                Result = 1;
        //            }


        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return Json(Result, JsonRequestBehavior.AllowGet);
        //}
        #region[radhe]
        public ActionResult UnPaidPayoutReport()
        {
            PayoutReport cl = new PayoutReport();
            try
            {

                //objp.Action = 6;
                //objp.dt = objdl.FranchiseWallet(objp, "Sp_FundRequestReport");
            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }

        [HttpPost]
        public ActionResult UnPaidPayoutReport(PayoutReport cl)
        {

            try
            {
                //if (cl.Action == "Paid")
                //{
                //    cl.dt = objdl.sp_BankStatementReportPaidUnpaid("sp_BankStatementReportPaidUnpaid", cl);
                //}

                if (cl.Action == "Unpaid1")
                {
                    cl.dt = objdl.sp_BankStatementReportPaidUnpaid("sp_BankStatementReportPaidUnpaid", cl);
                }


            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }

        public ActionResult ChangeSponser()
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            Registration obj = new Registration();
            if (Request.QueryString["Member_Id"] != null && Request.QueryString["Member_Id"].ToString() != "")
            {
                obj.MemberId = Request.QueryString["Member_Id"].ToString();
                obj.dt = objdl.GetMemberDetails(obj);
                if (obj.dt != null && obj.dt.Rows.Count > 0)
                {
                    obj.Name = obj.dt.Rows[0]["Name"].ToString();
                    obj.SponserId = obj.dt.Rows[0]["Intro_Id"].ToString();
                    obj.SponserName = obj.dt.Rows[0]["SponserName"].ToString();
                }
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult ChangeSponser(Registration cl)
        {
            TempData["msg"] = null;
            TempData["flag"] = null;
            try
            {
                cl.dt = objdl.save_changeSponser(cl);
                if (cl.dt != null && cl.dt.Rows.Count > 0)
                {
                    TempData["flag"] = cl.dt.Rows[0]["ID"].ToString();
                    TempData["msg"] = cl.dt.Rows[0]["MSG"].ToString();
                    // ModelState.Clear();
                }

            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }
        public JsonResult Check_Stock_Availability_admin(string ProductId)
        {
            cls_sales objStock = new cls_sales();
            try
            {

                DataTable dt = new DataTable();
                objStock.Action = 1;
                objStock.Id_C = ProductId;
                double stock = objdl.CheckAvalability(objStock, "admin");
                objStock.Stock = stock;

            }
            catch (Exception ex)
            {

            }
            return Json(objStock, JsonRequestBehavior.AllowGet);
        }
        #region [Radhe]
        public ActionResult PlanMaster()
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            try
            {
                ObjP.Action = 2;
                ObjP.dt = objdl.PlanMaster(ObjP, "Sp_PDFDetails");

            }
            catch (Exception ex)
            {

            }
            return View(ObjP);
        }
        [HttpPost]
        public ActionResult PlanMaster(Master objp, int CategoryId, HttpPostedFileBase ProductFile)
        {
            try
            {
                string str = "";

                if (ProductFile != null)
                {
                    str = "Pdf" + DateTime.Now.Ticks + DateTime.Now.ToString("ddMMMyyyyhhss") + Path.GetExtension(ProductFile.FileName);
                    ProductFile.SaveAs(Server.MapPath("/images/banner/" + str));

                    objp.FilePath = str;
                }
                objp.EntryBy = Session["username"].ToString();
                // ObjP.CategoryId = CategoryId;
                if (objp.CategoryId != 0)
                {
                    objp.Action = 5;

                }
                else
                {
                    objp.Action = 1;
                }
                DataTable dt = objdl.PlanMaster(objp, "Sp_PDFDetails");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["msg"].ToString();
                    Flag = dt.Rows[0]["Id"].ToString();

                }
                else
                {

                    Flag = "0";
                    Flag = "Server not Response!.";
                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;


            ObjP.Action = 2;
            ObjP.dt = objdl.PlanMaster(ObjP, "Sp_PDFDetails");



            return View(ObjP);
        }
        public JsonResult DeletePlanMaster(int CategoryId)
        {
            try
            {
                ObjP.EntryBy = Convert.ToString(Session["username"]);
                ObjP.CategoryId = CategoryId;
                ObjP.Action = 4;
                DataTable dt = objdl.PlanMaster(ObjP, "Sp_PDFDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjP.CategoryId = Convert.ToInt32(dt.Rows[0]["id"]);
                    ObjP.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    ObjP.CategoryId = 0;
                    ObjP.msg = "Server not Response!.";
                }
            }
            catch (Exception ex)
            {
                //objp.strId = "0";
            }
            return Json(ObjP, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Dounloadpdf()
        {
            try
            {
                ObjP.Action = 6;
                ObjP.dt = objdl.PlanMaster(ObjP, "Sp_PDFDetails");

            }
            catch (Exception ex)
            {

            }
            return View(ObjP);
        }
        #endregion
        #endregion




        public JsonResult Dayclosefranchise(string Action, string Franchise_Id, string Pincode)
        {
            string flag = "";
            DataTable dt = new DataTable();
            dt = objdl.Admindayclose(1, Franchise_Id, Pincode);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Id"].ToString() == "1")
                {
                    flag = dt.Rows[0]["Id"].ToString();
                }
            }
            return Json(flag, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PincodeWiseArea()
        {
            if (Session["username"] != null)
            {
                TempData["flag"] = null;
                TempData["msg"] = null;
                try
                {
                    ObjP.Action = 2;
                    ObjP.dt = objdl.PincodewiseareaMaster(ObjP, "proc_PinCodeWiseArea");

                }
                catch (Exception ex)
                {

                }


            }
            else
            {
                Response.Redirect("../Account/login");
            }
            return View(ObjP);
        }


        [HttpPost]
        public ActionResult PincodeWiseArea(Master objp, string Id)
        {
            try
            {


                objp.EntryBy = Session["username"].ToString();
                ObjP.Id = Convert.ToInt32(Id == "" ? "0" : Id);
                if (objp.Id != 0)
                {
                    objp.Action = 4;

                }
                else
                {
                    objp.Action = 1;
                }
                DataTable dt = objdl.PincodewiseareaMaster(objp, "proc_PinCodeWiseArea");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["msg"].ToString();
                    Flag = dt.Rows[0]["Id"].ToString();

                }
                else
                {

                    Flag = "0";

                }

            }
            catch (Exception ex)
            {

            }
            ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;


            ObjP.Action = 2;
            ObjP.dt = objdl.PincodewiseareaMaster(ObjP, "proc_PinCodeWiseArea");



            return View(ObjP);
        }



        public JsonResult DeletePincodeAreaMaster(int Id)
        {
            try
            {
                ObjP.EntryBy = Convert.ToString(Session["username"]);
                ObjP.Id = Id;
                ObjP.Action = 3;
                DataTable dt = objdl.PincodewiseareaMaster(ObjP, "proc_PinCodeWiseArea");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjP.Id = Convert.ToInt32(dt.Rows[0]["id"]);
                    ObjP.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    ObjP.Id = 0;
                }
            }
            catch (Exception ex)
            {
                //objp.strId = "0";
            }
            return Json(ObjP, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FranchiseIncomeallReport()
        {

            PayoutReport cl = new PayoutReport();
            if (Session["username"] != null)
            {
                cl.MemberId = Session["username"].ToString() == "admin" ? null : Session["username"].ToString();
                cl.Status = null;
                cl.dt = objdl.Franchisewithmartincome("2", cl);

            }
            else
            {
                Response.Redirect("../Account/login");
            }

            return View(cl);
        }



        public ActionResult OfferReport()
        {
            PayoutReport cl = new PayoutReport();

            if (Session["username"] != null)
            {
                cl.Action = "1";
                cl.dt = objdl.OfferReportByType(cl);
                cl.msg = "Member Offer Report.";
            }
            else
            {
                Response.Redirect("../Account/login");
            }

            return View(cl);
        }

        [HttpPost]
        public ActionResult OfferReport(PayoutReport cl)
        {

            if (Session["username"] != null)
            {
                cl.Action = "1";
                cl.dt = objdl.OfferReportByType(cl);
                cl.msg = "Member Offer Report.";
            }
            else
            {
                Response.Redirect("../Account/login");
            }

            return View(cl);
        }



        public ActionResult FranchiseMinimartReport()
        {

            PayoutReport cl = new PayoutReport();
            if (Session["username"] != null)
            {
                cl.MemberId = Session["username"].ToString() == "admin" ? null : Session["username"].ToString();
                cl.Status = null;
                cl.dt = objdl.Centerreportofminimartviewadmin("7", cl);

            }
            else
            {
                Response.Redirect("../Center/login");
            }

            return View(cl);
        }



        public ActionResult ViewFranchiseCompanyResponse(string InvoiceNo)
        {

            PayoutReport cl = new PayoutReport();
            if (Session["username"] != null)
            {
                cl.MemberId = Session["username"].ToString() == "admin" ? null : Session["username"].ToString();
                //cl.Status = null;
                cl.InviceNo = InvoiceNo;


                cl.dt = objdl.ViewRequestresponsecompany("5", cl);

            }
            else
            {
                Response.Redirect("../Center/login");
            }

            return View(cl);
        }



    }
}

