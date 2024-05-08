using MLMPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MLMPortal.Controllers
{
    public class PinManagementController : Controller
    {
        Datalayer ex = new Datalayer();
        // GET: PinManagement
        public ActionResult PinGenerate(string MemberCode, string PinNumber, string fDate)

        {
            UserReport returnObj = new UserReport();
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            try
            {
                ViewBag.PinAmt = AllClasses.BindDDL(ex.getPinAmtList());
                string mCode = null;
                if (!string.IsNullOrEmpty(MemberCode))
                {
                    mCode = MemberCode;
                }
                if (PinNumber == "")
                {
                    PinNumber = null;
                }
                returnObj.Table1 = ex.Proc_GetPinDetailsFilter("4", mCode, PinNumber, fDate);
            }
            catch (Exception exec)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }


            return View(returnObj);
        }


        [HttpPost]
        public ActionResult PinGenerate(string txtLoginID, string txtPinAmt, string txtNoOfPins, string txtRemark)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["flag"] = null;
            TempData["msg"] = null;
            try
            {
                int a = ex.GeneratePin(txtLoginID, txtPinAmt, txtNoOfPins, txtRemark);
                if (a > 0)
                {
                    TempData["flag"] = "1";
                    TempData["msg"] = "PIN Generated Successfully";
                }
                else
                {
                    TempData["flag"] = "0";
                    TempData["msg"] = "Error";
                }
            }
            catch (Exception exec)
            {
                TempData["flag"] = "0";
                TempData["msg"] = exec.Message;
            }


            return RedirectToAction("PinGenerate");
        }

        public JsonResult GeneratePins(string MemberId, string PackageId, string NoofPins, string Remarks)
        {
            string msg = "";
            try
            {

                DataTable dt = ex.insertPinDetailsNew(MemberId, PackageId, Convert.ToInt64(NoofPins), Remarks, Convert.ToString(Session["UserName"]));
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["MSG"].ToString();
                }
                else
                {
                    msg = "Pin can not be Generated. ";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #region PinDetails
        public ActionResult Pindetails()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            UserReport returnObj = new UserReport();
            try
            {
                string mCode = Convert.ToString(Session["UserName"]);
                returnObj.Table1 = ex.Proc_GetPinDetails("1", mCode, null);
            }
            catch (Exception exec)
            {
            }


            return View(returnObj);
        }

        #region DeletePinDetails
        public ActionResult DeletePinDetails(string PinId, string mode, string MemberCode, string genDate)
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (mode != null && PinId != null && mode == "D")
            {
                DataTable dt = ex.Proc_DeletePinNew(PinId, Convert.ToString(Session["UserName"]));
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["flag"] = dt.Rows[0]["ID"].ToString();
                    TempData["msg"] = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    TempData["flag"] = "0";
                    TempData["msg"] = "Error";
                }

                return RedirectToAction("DeletePinDetails");
            }


            UserReport returnObj = new UserReport();
            try
            {
                string mCode = MemberCode != "" ? MemberCode : null;
                returnObj.Table1 = ex.Proc_GetPinDetails("1", mCode, genDate);
            }
            catch (Exception exec)
            {
            }


            return View(returnObj);

       
        }

        #endregion

        #region DeletePins
        public ActionResult DeletePINs(string MemberCode, string genDate)
        {
            UserReport returnObj = new UserReport();
            try
            {
                string mCode = MemberCode != "" ? MemberCode : null;
                returnObj.Table1 = ex.Proc_GetPinDetails("2", mCode, genDate);
            }
            catch (Exception exec)
            {
            }
            return View(returnObj);
        }
        #endregion
        public ActionResult BindUserPindetails()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            UserReport returnObj = new UserReport();
            try
            {
                string mCode = Convert.ToString(Session["UserName"]);
                returnObj.Table1 = ex.Proc_GetPinDetails("3", mCode, null);
            }
            catch (Exception exec)
            {
            }


            return View(returnObj);
        }

        public JsonResult BindPins(string MemberId)
        {
            List<PinDetails> PinDetails = new List<PinDetails>();
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "[dbo].[Proc_getPinsforActivation]";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@MemberId", MemberId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            PinDetails.Add(new PinDetails
                            {
                                Pin = sdr["Pin"].ToString(),
                                PinNumber = sdr["PinNumber"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return Json(PinDetails, JsonRequestBehavior.AllowGet);
        }

     
        #endregion

        #region PinTransfer

        public ActionResult PinTransfer(string MemberId, string tDate)
        {
            UserReport returnObj = new UserReport();
            try
            {
                ViewBag.PinAmt = AllClasses.BindDDL(ex.getPinAmtList());
               
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                try
                {
                    string mCode = MemberId != "" ? MemberId : null;
                    returnObj.Table1 = ex.Proc_TransferPinLogAdmin(mCode, tDate);
                }
                catch (Exception exec)
                {
                }
            }
            catch (Exception exec)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }
            return View(returnObj);
        }
        public JsonResult getPinBalance(string PackId, string TransferMemberId)
        {
            int Balance = 0;
            try
            {
                DataTable dt = ex.Proc_GetPinBalance("1", TransferMemberId, PackId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Balance = dt.Rows[0]["cnt"].ToString() != "" ? Convert.ToInt32(dt.Rows[0]["cnt"].ToString()) : 0;
                }
            }
            catch (Exception ex)
            {

            }
            return Json(Balance, JsonRequestBehavior.AllowGet);
        }

        public JsonResult admininsertPintransfer(string TransferFromId, string TransferToId, string txtPackage, string PinBalance, string TransferNoOfPin)
        {
            string msg = "";
            try
            {
                DataTable dt = ex.PinTransfer(TransferFromId, TransferToId, txtPackage, PinBalance, TransferNoOfPin);
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["MSG"].ToString();
                }
                else
                {
                    msg = "Error";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMemberDetails(string MemberCode)
        {
            string MemName = "";
            try
            {
                DataTable dt = ex.getMemberDetails(MemberCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MemName = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(MemName, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetName_Pin_AllType(string MemberCode)
        {
            string MemName = "";
            try
            {
                DataTable dt = ex.getDetails_forPin(MemberCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MemName = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(MemName, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PinTransfer(string TransferFromId, string TransferToId, string txtPackage, string PinBalance, string TransferNoOfPin)
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            try
            {

                DataTable dt = ex.PinTransfer(TransferFromId, TransferToId, txtPackage, PinBalance, TransferNoOfPin);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["flag"] = dt.Rows[0]["ID"];
                    TempData["msg"] = dt.Rows[0]["MSG"];
                }
                else
                {
                    TempData["flag"] = "0";
                    TempData["msg"] = "Error";
                }

            }
            catch (Exception exec)
            {
                TempData["flag"] = "0";
                TempData["msg"] = exec.Message;
            }




            return RedirectToAction("PinTransfer");
        }
        public ActionResult EPinTransferReport(string memberCode, string fDate)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "adminlogin");
            }
            UserReport ReturnObj = new UserReport();
            try
            {
                string username = null;
                if (!string.IsNullOrEmpty(memberCode))
                {
                    username = memberCode;
                }
                else
                {
                    username = Session["username"].ToString();
                }
                //else
                //{
                //    username = Convert.ToString(Session["mCode"]);
                //}
                ReturnObj.Table1 = ex.Proc_TransferPinLogAdmin(username, fDate);
                //ReturnObj.Table1 = ex.MLM_getPayoutDetails(username, fDate, "1");

            }
            catch (Exception exec)
            {

                throw;
            }
            return View(ReturnObj);
        }



        public ActionResult AdminPinDetails(string MemberCode, string PinNumber, string fDate)
        {
            UserReport returnObj = new UserReport();
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                ViewBag.PinAmt = AllClasses.BindDDL(ex.getPinAmtList());
                string mCode = null;
                if (!string.IsNullOrEmpty(MemberCode))
                {
                    mCode = MemberCode;
                }
                if (PinNumber == "")
                {
                    PinNumber = null;
                }
                returnObj.Table1 = ex.Proc_GetPinDetailsFilter("4", mCode, PinNumber, fDate);
            }
            catch (Exception exec)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }


            return View(returnObj);
        }


        #endregion


        #region [RAHUL Topup_By_Franchise]
        public ActionResult MemberTopup_ByFranPin()
        {
            UserReport returnObj = new UserReport();
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            try
            {
                returnObj.lstPackage = AllClasses.BindDDL(ex.sp_getPackageList(1));
                returnObj.lstPin = AllClasses.BindDDL(new DataTable());
            }
            catch (Exception ex)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }
            return View(returnObj);
        }
        [HttpPost]
        public ActionResult MemberTopup_ByFranPin(UserReport returnObj)
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            DataTable objdt = new DataTable();
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            try
            {
                if (CheckActivatedId(returnObj.memberId) == true)
                {
                    TempData["flag"] = "0";
                    TempData["msg"] = "Member Id Already Activated.";
                }
                else
                {
                    returnObj.CreatedBy = Session["username"].ToString();
                    objdt = ex.Save_MemberTopup_By_Pin(returnObj);
                    if (objdt != null && objdt.Rows.Count > 0)
                    {

                        if (objdt.Rows[0]["ID"].ToString() == "0")
                        {
                            TempData["flag"] = "0";
                            TempData["msg"] = objdt.Rows[0]["MSG"].ToString();
                        }
                        else
                        {

                            string Message = "Congratulations " + objdt.Rows[0]["Name"].ToString() + ", Your ID " + returnObj.memberId + " has been successfully activated. Happy Association.!!";
                            TempData["flag"] = "1";
                            TempData["msg"] = Message;
                            string message = "Dear " + objdt.Rows[0]["Name"].ToString() + ". Topup of products of order id " + objdt.Rows[0]["OrderId"].ToString() + " on " + returnObj.memberId + " of amount " + objdt.Rows[0]["Amount"].ToString() + " on date " + DateTime.Now.ToString("dd/MMM/yyyy") + " Keep Shopping with us ALL THE BEST";
                            if (objdt.Rows[0]["Mobile"].ToString() != "")
                            {
                            }
                        }

                    }
                }
                returnObj.lstPackage = AllClasses.BindDDL(ex.sp_getPackageList(1));
                returnObj.lstPin = AllClasses.BindDDL(new DataTable());
            }
            catch (Exception ex)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }
            return View(returnObj);
        }

#endregion


        public ActionResult MemberTopup_ByPin()
        {
            UserReport returnObj = new UserReport();
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            try
            {
                returnObj.lstPackage = AllClasses.BindDDL(ex.sp_getPackageList(1));
                returnObj.lstPin = AllClasses.BindDDL(new DataTable());
            }
            catch (Exception ex)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }
            return View(returnObj);
        }
        [HttpPost]
        public ActionResult MemberTopup_ByPin(UserReport returnObj)
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            DataTable objdt = new DataTable();
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            try
            {
                //if (CheckActivatedId(returnObj.memberId) == true)
                //{
                //    TempData["flag"] = "0";
                //    TempData["msg"] = "Member Id Allready Activated.";
                //}
                //else
                //{
                    returnObj.CreatedBy = Session["username"].ToString();
                    objdt = ex.Save_MemberTopup_By_Pin(returnObj);
                    if (objdt!=null && objdt.Rows.Count > 0)
                    {
                        if (objdt.Rows[0]["ID"].ToString() == "0")
                        {
                            TempData["flag"] = "0";
                            TempData["msg"] = objdt.Rows[0]["MSG"].ToString();
                        }
                        else
                        {
                            string Message = "Congratulations " + objdt.Rows[0]["Name"].ToString() + ", Your ID " + returnObj.memberId + " has been successfully activated. Happy Association.!!";
                            TempData["flag"] = "1"; 
                             TempData["msg"] = Message;
                            string message = "Dear "+ objdt.Rows[0]["Name"].ToString() + ". Topup of products of order id "+ objdt.Rows[0]["OrderId"].ToString() + " on "+ returnObj.memberId + " of amount " + objdt.Rows[0]["Amount"].ToString() + " on date " + DateTime.Now.ToString("dd/MMM/yyyy") + " Keep Shopping with us ALL THE BEST";
                            if (objdt.Rows[0]["Mobile"].ToString() != "")
                            {
                            }
                        }

                    }
                //}
                returnObj.lstPackage = AllClasses.BindDDL(ex.sp_getPackageList(1));
                returnObj.lstPin = AllClasses.BindDDL(new DataTable());
            }
            catch (Exception ex)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }
            return View(returnObj);
        }
        public object GetMemberDetails_ById(string MemberId)
        {
            UserReport returnObj = new UserReport();
            returnObj.Table1 = ex.GetMember_details_By_Id(MemberId);
            returnObj.JsonModel = AllClasses.ConvertDataTabletoString(returnObj.Table1);
            //return Json(returnObj, JsonRequestBehavior.AllowGet);
            return returnObj.JsonModel;
        }
        public JsonResult GetMasterDropDown(int Action, string id1, string id2, string id3)
        {
            id1=Session["username"].ToString()=="admin"? "UJ00001" : Session["username"].ToString();

            List<SelectListItem> lst = AllClasses.BindDDL(ex.GetMasterData(Action, id1, id2, id3));

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPinFor_Franchise(int Action, string id1, string id2, string id3)
        {
            id1 = Session["username"].ToString();

            List<SelectListItem> lst = AllClasses.BindDDL(ex.GetMasterData(Action, id1, id2, id3));

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public bool CheckActivatedId(string MemberId)
        {
            bool res = false;
            try
            {

                DataTable dt = ex.GetMember_details_By_Id(MemberId);
                if(dt!=null && dt.Rows.Count>0)
                {
                    if(dt.Rows[0]["Status"].ToString()=="True")
                    {
                        res = true;
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                return res;
            }
        }
        public ActionResult User_PinTransfer(string MemberId, string tDate)
        {
            UserReport returnObj = new UserReport();
            try
            {
                ViewBag.PinAmt = AllClasses.BindDDL(ex.getPinAmtList());
                if (Session["username"] != null)
                {
                    returnObj.Table1 = ex.GetMember_details_By_Id(Session["username"].ToString());
                    if(returnObj.Table1!=null && returnObj.Table1.Rows.Count>0)
                    {
                        returnObj.Name = returnObj.Table1.Rows[0]["Name"].ToString();
                    }
                    returnObj.memberId = Session["username"].ToString();
                    
                }
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                try
                {
                    string mCode = Session["username"].ToString();
                    returnObj.Table1 = ex.Proc_TransferPinLogAdmin(mCode, tDate);
                }
                catch (Exception exec)
                {
                }
            }
            catch (Exception exec)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }
            return View(returnObj);
        }
        [HttpPost]
        public ActionResult User_PinTransfer(string TransferFromId, string TransferToId, string txtPackage, string PinBalance, string TransferNoOfPin)
        {
            TempData["flag"] = null;
            TempData["msg"] = null;
            try
            {

                DataTable dt = ex.PinTransfer(TransferFromId, TransferToId, txtPackage, PinBalance, TransferNoOfPin);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["flag"] = dt.Rows[0]["ID"];
                    TempData["msg"] = dt.Rows[0]["MSG"];
                }
                else
                {
                    TempData["flag"] = "0";
                    TempData["msg"] = "Error";
                }

            }
            catch (Exception exec)
            {
                TempData["flag"] = "0";
                TempData["msg"] = exec.Message;
            }




            return RedirectToAction("PinTransfer");
        }
    }
}