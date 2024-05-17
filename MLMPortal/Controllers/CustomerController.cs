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
    public class CustomerController : Controller
    {

        // GET: Franchise
        Master ObjP = new Master();
        Datalayer objdl = new Datalayer();
        string Msg = "", Flag = "";


        public JsonResult GetMasterDropDown(int Action, string id1, string id2, string id3)
        {
            List<SelectListItem> lst = AllClasses.BindDDL(objdl.GetMasterData(Action, id1, id2, id3));

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MemberTopup()
        {
            UserReport returnObj = new UserReport();
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            try
            {
                returnObj.lstPackage = AllClasses.BindDDL(objdl.GetMasterData(6));
            }
            catch (Exception ex)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }
            return View(returnObj);
        }
        [HttpPost]
        public ActionResult MemberTopup(UserReport returnObj)
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

                returnObj.CreatedBy = Session["username"].ToString();
                returnObj.Role = Session["Role"].ToString();
                objdt = objdl.Save_MemberTopup_By_Pin(returnObj);
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
                        //string message = "Dear " + objdt.Rows[0]["Name"].ToString() + ". Topup of products of order id " + objdt.Rows[0]["OrderId"].ToString() + " on " + returnObj.memberId + " of amount " + objdt.Rows[0]["Amount"].ToString() + " on date " + DateTime.Now.ToString("dd/MMM/yyyy") + " Keep Shopping with us ALL THE BEST";
                        //if (objdt.Rows[0]["Mobile"].ToString() != "")
                        //{
                        //}
                    }

                }
                //}
                returnObj.lstPackage = AllClasses.BindDDL(objdl.GetMasterData(6));
            }
            catch (Exception ex)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }
            return View(returnObj);
        }



        public JsonResult JsonMemberTopup(UserReport returnObj)
        {
            string flag = "0";
            string msg = "Server Not Responding";
            DataTable objdt = new DataTable();
            if (Session["UserName"] == null)
            {
                msg = "Please Re-login then try again";
                return Json(new { flag= flag, msg= msg },JsonRequestBehavior.AllowGet);
            }
            try
            {
                returnObj.memberId = Session["username"].ToString();
                returnObj.CreatedBy = Session["username"].ToString();
                returnObj.Role = Session["Role"].ToString();
                objdt = objdl.Save_MemberTopup_By_Pin(returnObj);
                if (objdt != null && objdt.Rows.Count > 0)
                {
                    if (objdt.Rows[0]["ID"].ToString() == "0")
                    {
                        flag = objdt.Rows[0]["ID"].ToString();
                        msg = objdt.Rows[0]["MSG"].ToString();
                    }
                    else
                    {
                        string Message = "Congratulations " + objdt.Rows[0]["Name"].ToString() + ", Your ID " + returnObj.memberId + " has been successfully activated.!!";
                        flag= "1";
                        msg = Message;
                    }

                }
            }
            catch (Exception ex)
            {
                
            }
            return Json(new { flag = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TopupHistory(PayoutReport objp)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                    objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.USP_GetClosingDateils("3", objp);
            }
            catch (Exception ex)
            {
            }
            return View(objp);
        }
        #region Associate Panel
        public ActionResult FundRequestReport(Registration cr)
        {

            if (Session["username"] != null)
            {
                cr.SessionId = Convert.ToString(Session["username"]);
                //cl.FromDate = DateTime.Now.AddDays(-365).ToString("dd-MMM-yyyy");
                //cl.ToDate = DateTime.Now.ToString("dd-MMM-yyyy");
                ////cl.MemberId = Convert.ToString(Session["username"]);

                cr.dt = objdl.GetFundRequestReport(cr);



            }
            else
            {
                Response.Redirect("Login");
            }

            return View(cr);
        }
        //member feild
        public ActionResult FundRequest()
        {
            cls_fundmgnt cls = new cls_fundmgnt();
            ViewBag.Username = Convert.ToString(Session["username"]);
            cls.Action = "1";
            cls.MemberId = Convert.ToString(Session["username"]);
            cls.dt = objdl.fundManageMent(cls);
            if (cls.dt != null && cls.dt.Rows.Count > 0)
            {
                ViewBag.name = cls.dt.Rows[0]["name"] + "";
            }
            cls.dtBank = objdl.BindBank(cls);
            cls.Action = "2";
            cls.dtDepo = objdl.fundManageMent(cls);

            cls.lstPackage = AllClasses.BindDDL(objdl.GetMasterData(7));


            return View(cls);
        }

        public JsonResult WalletMoneyTransfer(cls_fundmgnt cls)
        {
            string flag = "0";
            string msg = "Server Not Responding";
            cls.MemberId = Session["username"].ToString();

            if (cls != null && cls.File != null && cls.File.ContentLength > 0)
            {
                string Pic = Path.GetFileName(DateTime.Now.Ticks.ToString() + cls.File.FileName.Replace(" ", ""));
                cls.File.SaveAs(Server.MapPath("/Content/Fund_files/" + Pic));
                cls.fileUrl = "/Content/Fund_files/" + Pic;
            }


            DataTable dt = objdl.Sp_InsertMoneyTransfer(cls);
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = dt.Rows[0]["ID"].ToString();
                msg = dt.Rows[0]["Msg"].ToString();
            }
            return Json(new { flag= flag, msg= msg }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LevelIncomeCount(string Status)
        {
            PayoutReport cl = new PayoutReport();
            try
            {
                if (Status == "")
                {
                    cl.Status = null;
                }
                else
                {
                    cl.Status = Status;
                }
                cl.Action = "1";
                cl.MemberId = Session["username"].ToString();
                cl.dt = objdl.Levelmembercount("1", cl);
            }
            catch (Exception ex)
            {

            }
            return View(cl);
        }

        public ActionResult MyDirectTeam()
        {
            PayoutReport objR = new Models.PayoutReport();
            try
            {
                objR.MemberId = Convert.ToString(Session["username"]);
                objR.dt = objdl.Get_MyDirectTeam(objR);

            }
            catch (Exception ex)
            { }

            return View(objR);
        }
        [HttpPost]
        public ActionResult MyDirectTeam(PayoutReport objR)
        {
            try
            {
                objR.MemberId = Session["username"].ToString();
                objR.dt = objdl.Get_MyDirectTeam(objR);

            }
            catch (Exception ex)
            { }

            return View(objR);
        }
        public ActionResult RegistrationNew()
        {
            Registration mr = new Registration();

            if (Session["username"] != null)
            {
                mr.MemberId = Convert.ToString(Session["username"]);

                mr.PhoneCodeLst = AllClasses.BindDDL(objdl.BindPhoneCode());

                mr.StateDDLLst = AllClasses.BindDDL(objdl.BindState());
                mr.DistrictDDLLst = AllClasses.BindDDL(new DataTable());
                mr.CountryCode = 100;

                mr.dt = objdl.GetMemberRegistration(mr);
                if (mr.dt != null && mr.dt.Rows.Count > 0)
                {

                    //mr.MobileNo = mr.dt.Rows[0]["mobile"].ToString();
                    //mr.Address = mr.dt.Rows[0]["Address"].ToString();
                    //mr.Name = mr.dt.Rows[0]["Name"].ToString();
                    //mr.LandMark = mr.dt.Rows[0]["LandMark"].ToString();
                    //mr.FatherName = mr.dt.Rows[0]["f_name"].ToString();
                    //mr.Area = mr.dt.Rows[0]["Area"].ToString();
                    //mr.HouseNo = mr.dt.Rows[0]["HouseNo"].ToString();

                    //mr.AdharNo = mr.dt.Rows[0]["AdharNo"].ToString();
                    //mr.EmailId = mr.dt.Rows[0]["Email"].ToString();
                    mr.SponserId = Session["username"].ToString();
                    //mr.ZipCode = mr.dt.Rows[0]["ZipCode"].ToString();
                    mr.SponserName = mr.dt.Rows[0]["Name"].ToString();


                }


            }

            return View(mr);
        }
        [HttpPost]
        public ActionResult RegistrationNew(Registration mr)
        {
            TempData["msg"] = null;
            TempData["flag"] = null;

            if (Session["username"] != null)
            {
                mr.MemberId = Convert.ToString(Session["username"]);

                mr.PhoneCodeLst = AllClasses.BindDDL(objdl.BindPhoneCode());
                mr.StateDDLLst = AllClasses.BindDDL(objdl.BindState());
                mr.DistrictDDLLst = AllClasses.BindDDL(new DataTable());
                mr.Pass = AllClasses.CreateRandomPassword(5);
                mr.TranPass = AllClasses.CreateTransactPassword(4);
                DataTable dt = objdl.Asso_MemberReg(mr, "USP_InsertMembers");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["MSG"].ToString();
                    Flag = dt.Rows[0]["ID"].ToString();


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

            return View(mr);
        }
        public ActionResult MemberViewProfile()
        {
            Registration mr = new Registration();
            if (Session["username"] != null)
            {
                mr.MemberId = Convert.ToString(Session["username"]);


                mr.PhoneCodeLst = AllClasses.BindDDL(objdl.BindPhoneCode());
                mr.StateDDLLst = AllClasses.BindDDL(objdl.BindState());



                mr.dt = objdl.GetMemberDetails(mr);
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
                }


            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View(mr);
        }

        public ActionResult MemberEditProfile()

        {
            Registration mr = new Registration();
            if (Session["username"] != null)
            {
                mr.MemberId = Convert.ToString(Session["username"]);

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
                    mr.DOB = mr.dt.Rows[0]["MDob1"].ToString(); //mr.dt.Rows[0]["MDob"].ToString();
                    mr.CountryCode = CountryCode;
                    mr.Gender = mr.dt.Rows[0]["Gender"].ToString();

                    mr.PanFilePath = mr.dt.Rows[0]["PANCopy2"].ToString();
                    mr.AdharFilePath = mr.dt.Rows[0]["AADHARCopy2"].ToString();
                    mr.BankPassbookFilePath = mr.dt.Rows[0]["Passbookcopyy"].ToString();
                    mr.AdharbackFilePath = mr.dt.Rows[0]["AdharBackCopyy"].ToString();


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
                string ProfilePic1 = string.Empty;
                string ProfilePic2 = string.Empty;

                string ProfilePic3 = string.Empty;
                string ProfilePic4 = string.Empty;

                mr.MemberId = Convert.ToString(Session["username"]);

                mr.PhoneCodeLst = AllClasses.BindDDL(objdl.BindPhoneCode());
                mr.StateDDLLst = AllClasses.BindDDL(objdl.BindState());
                mr.DistrictDDLLst = AllClasses.BindDDL(new DataTable());
                if (PAN != null)
                {


                    System.Drawing.Image img = System.Drawing.Image.FromStream(PAN.InputStream);
                    int height = img.Height;
                    int width = img.Width;

                    // imageFilePath1 = Server.MapPath("/Images") + "/default1.png";
                    ProfilePic1 = Path.GetFileName(DateTime.Now + PAN.FileName);
                    PAN.SaveAs(Server.MapPath("/Content/KYC/" + ProfilePic1));
                    mr.PanFilePath = ProfilePic1;
                }
                if (Adhar != null)
                {

                    System.Drawing.Image img = System.Drawing.Image.FromStream(Adhar.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    ProfilePic2 = Path.GetFileName(DateTime.Now + Adhar.FileName);
                    Adhar.SaveAs(Server.MapPath("/Content/KYC/" + ProfilePic2));
                    mr.AdharFilePath = ProfilePic2;

                }

                if (BankPassbook != null)
                {

                    System.Drawing.Image img = System.Drawing.Image.FromStream(BankPassbook.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    ProfilePic3 = Path.GetFileName(DateTime.Now + BankPassbook.FileName);
                    BankPassbook.SaveAs(Server.MapPath("/Content/KYC/" + ProfilePic3));
                    mr.BankPassbookFilePath = ProfilePic3;

                }
                if (Adharback != null)
                {

                    System.Drawing.Image img = System.Drawing.Image.FromStream(Adharback.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    ProfilePic4 = Path.GetFileName(DateTime.Now + Adharback.FileName);
                    Adharback.SaveAs(Server.MapPath("/Content/KYC/" + ProfilePic4));
                    mr.AdharbackFilePath = ProfilePic4;
                }




                if (ProfilePic != null)
                {
                    string FName = "";
                    FName = "EmpProfile" + DateTime.Now.Ticks + ProfilePic.FileName;
                    string FileUrl = "/Content/ProfilePic/" + FName;
                    string Cpath = Path.Combine(Server.MapPath("~/Content/ProfilePic/"), FName);
                    ProfilePic.SaveAs(Cpath);
                    mr.ProfilePic = FName;

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

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(Registration mr)
        {

            if (Session["username"] != null)
            {
                string username = Convert.ToString(Session["username"]);
                mr.dt = objdl.ChangePassword("3", username, mr.OldPassword, mr.NewPassword, null);
                if (mr.dt != null & mr.dt.Rows.Count > 0)
                {
                    Msg = mr.dt.Rows[0]["MSG"].ToString();
                    Flag = mr.dt.Rows[0]["flag"].ToString();
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
        public ActionResult ForgetTransactionPassword()
        {

            if (Session["username"] != null)
            {
            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View();
        }
        [HttpPost]
        public ActionResult ForgetTransactionPassword(Registration mr)
        {
            string res = "";

            if (Session["username"] != null)
            {
                mr.dt = objdl.GetMemberEditDetails(mr);
                if (mr.dt != null && mr.dt.Rows.Count > 0)
                {
                    res = sendsms(mr.dt.Rows[0]["Name"].ToString(), mr.dt.Rows[0]["TransactPassword"].ToString(), Session["username"].ToString(), mr.dt.Rows[0]["Mobile"].ToString());
                    if (res == "success")
                        Msg = "Transaction Password has been sent on your registered mobile no.";
                    Flag = "1";

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
        public ActionResult ForgetLoginPassword()
        {

            if (Session["username"] != null)
            {
            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View();
        }
        [HttpPost]
        public ActionResult ForgetLoginPassword(Registration mr)
        {
            string res = "";

            if (Session["username"] != null)
            {
                mr.dt = objdl.GetMemberEditDetails(mr);
                if (mr.dt != null && mr.dt.Rows.Count > 0)
                {
                    res = SendLoginPsw(mr.dt.Rows[0]["Name"].ToString(), mr.dt.Rows[0]["psw"].ToString(), Session["username"].ToString(), mr.dt.Rows[0]["Mobile"].ToString());
                    if (res == "success")
                        Msg = "Login Password has been sent on your registered mobile no.";
                    Flag = "1";

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
        public ActionResult ChangeTransactionPassword()
        {

            if (Session["username"] != null)
            {


            }
            else
            {
                Response.Redirect("Account/login");
            }

            return View();
        }
        [HttpPost]
        public ActionResult ChangeTransactionPassword(Registration mr)
        {

            if (Session["username"] != null)
            {
                string username = Convert.ToString(Session["username"]);
                mr.dt = objdl.ChangePassword("4", username, mr.OldPassword, mr.NewPassword, null);
                if (mr.dt != null & mr.dt.Rows.Count > 0)
                {
                    Msg = mr.dt.Rows[0]["MSG"].ToString();
                    Flag = mr.dt.Rows[0]["flag"].ToString();
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


        protected string sendsms(string Name, string passwowd, string MemberId, string MobileNo)
        {
            string result = "";
            try
            {
                string message = "";
                message = "Dear " + Name + " ! Your Member ID is " + MemberId + " and transaction password: " + passwowd + " Thankyou,  ";



            }
            catch
            {
            }
            return result;
        }
        protected string SendLoginPsw(string Name, string passwowd, string MemberId, string MobileNo)
        {
            string result = "";
            try
            {
                string message = "Your Member ID/Username is " + MemberId + " and password: " + passwowd + " Thankyou";



            }
            catch
            {
            }
            return result;
        }
        #endregion

        public ActionResult UserDashboard()
        {
            Registration mr = new Registration();
            mr.Action = 1;
            mr.News = objdl.GetNews(mr);
            if (mr.News != null && mr.News.Rows.Count > 0)
            {
                mr.NewDetails = mr.News.Rows[0]["NewsPreference"].ToString();
            }

            if (Session["Isuserpop"] == null)
            {
                Session["Isuserpop"] = 1;
            }
            else
            {

                Session["Isuserpop"] = 2;
            }
            if (Session["username"] != null)
            {
                mr.SessionId = Convert.ToString(Session["username"]);
                mr.hdnValue = "/RegistrationForm?SPId=" + Session["username"].ToString();
                mr.dt = objdl.GetDashBoardDetails(mr);
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
                    //mr.ROIIncome = mr.dt.Rows[0]["ROIIncome"].ToString();
                    //mr.MatchingIncome = mr.dt.Rows[0]["MatchingIncome"].ToString();


                    mr.AllTeam = mr.dt.Rows[0]["OurTeam"].ToString();
                    mr.MyDirects = mr.dt.Rows[0]["DirectTeam"].ToString();
                    mr.DirectIncome = mr.dt.Rows[0]["DirectIncome"].ToString();
                    mr.LevelIncome = mr.dt.Rows[0]["LevelIncome"].ToString();
                    mr.FundWallet = mr.dt.Rows[0]["FundWallet"].ToString();
                    mr.SuperPerformancebonus = mr.dt.Rows[0]["SuperPerformanceIncome"].ToString();
                    mr.PerformerOfTheDayIncome = mr.dt.Rows[0]["PerformanceOftheDayIncome"].ToString();
                    mr.Performancebonus = mr.dt.Rows[0]["PerformanceIncome"].ToString();


                }
                mr.dt = null;

            }

            else
            {
                Response.Redirect("/Account/login");
            }


            //mr.dtoffer = objdl.OfferReportByAction("5");

            return View(mr);

        }
        [HttpPost]
        public ActionResult UserDashboard(HttpPostedFileBase ProfilePic)
        {
            Registration mr = new Registration();


            mr.Action = 1;
            mr.News = objdl.GetNews(mr);
            mr.NewDetails = mr.News.Rows[0]["NewsPreference"].ToString();
            if (Session["Isuserpop"] == null)
            {
                Session["Isuserpop"] = 1;
            }
            else
            {

                Session["Isuserpop"] = 2;
            }
            if (ProfilePic != null)
            {
                string FName = "";
                FName = "EmpProfile" + DateTime.Now.Ticks + ProfilePic.FileName;
                string FileUrl = "/ProfilePic/" + FName;
                string Cpath = Path.Combine(Server.MapPath("~/ProfilePic/"), FName);
                ProfilePic.SaveAs(Cpath);
                mr.ProfilePic = FName;
                Session["prodilepic"] = mr.ProfilePic;
            }
            mr.MemberId = Session["username"].ToString();

            DataTable dt = objdl.update_only_profile(mr);



            //mr.Action = 1;
            //mr.Catdt = objdl.GetImage(mr);
            //mr.ImageTitle = mr.Catdt.Rows[0]["ImageFile"].ToString();

            if (Session["username"] != null)
            {
                mr.SessionId = Convert.ToString(Session["username"]);
                //mr.hdnValue2 = "/RegistrationForm?SPId=" + Session["username"].ToString() + "&D=L";
                //mr.hdnValue = "/RegistrationForm?SPId=" + Session["username"].ToString() + "&D=R";
                mr.dt = objdl.GetDashBoardDetails(mr);
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
                    mr.TotalWalletAmnt = mr.dt.Rows[0]["TotalWalletAmnt"].ToString();
                    mr.CurrentWalletAmnt = mr.dt.Rows[0]["CurrentWalletAmnt"].ToString();

                }
                DataTable dt1 = objdl.Get_UserDashBoardDetails(mr);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    mr.AllTeam = dt1.Rows[0]["NewMember"].ToString();
                    mr.MyDirects = dt1.Rows[0]["DirectMember"].ToString();
                    mr.SelfBV = dt1.Rows[0]["SelfBV"].ToString();
                    mr.TeamBV = dt1.Rows[0]["TeamBV"].ToString();
                    mr.FundWallet = dt1.Rows[0]["MoneyWallet"].ToString();
                    mr.Current_LBV = dt1.Rows[0]["Current_LBV"].ToString();
                    mr.Current_RBV = dt1.Rows[0]["Current_RBV"].ToString();
                    mr.LeftTeam = dt1.Rows[0]["LeftTeam"].ToString();
                    mr.RightTeam = dt1.Rows[0]["RightTeam"].ToString();

                }

                PayoutReport pr = new PayoutReport();
                pr.MemberId = Session["username"].ToString();
                pr.Action = "4";

                DataTable dtoff = objdl.OfferReportByType(pr);
                if (dtoff.Rows.Count > 0)
                {
                    mr.DirectMember = dtoff.Rows[0]["DirectMem"].ToString();
                    mr.MatchingMember = dtoff.Rows[0]["MBusiness"].ToString();
                    mr.RewardName = dtoff.Rows[0]["Reward"].ToString();
                }

            }
            else
            {
                Response.Redirect("/Account/login");
            }

            return View(mr);

        }


        public ActionResult PendingFundRequestReport(PayoutReport objP)
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
                objp.dt = objdl.PayoutReport(objp, "USP_GETCustomerProductReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult ApprovedFundRequestReport(PayoutReport objP)

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
                objp.dt = objdl.CustomerPayoutReport(objp, "USP_GETCustomerProductReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult RejectFundRequestReport(PayoutReport objP)
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
                objp.Action = "3";

                objp.MemberName = objP.MemberName;
                objp.TransactionID = objP.TransactionID;
                objp.MemberId = objP.MemberId;
                objp.DepositType = objP.DepositType;

                //if (objP.MemberId == null)
                //{
                //    objp.MemberId = Session["username"].ToString();
                //}
                objp.dt = objdl.CustomerPayoutReport(objp, "USP_GETCustomerProductReport");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }


        public JsonResult ApproveCustumerFund(string MemberId, string Id, string Remark, string Amount, string status)
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

                DataTable dt = objdl.ApproveCustumerFund(objp, "sp_ApproveDisApproveFundReq");
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


        public ActionResult UserWalletUpdate()
        {
            if (Convert.ToString(Session["Role"]) != "1")
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult UserWalletUpdate(UserReport returnObj)
        {
            if (Convert.ToString(Session["Role"]) != "1")
            {
                return RedirectToAction("Index", "Admin");
            }
            TempData["flag"] = null;
            TempData["msg"] = null;
            DataTable objdt = new DataTable();

            try
            {

                returnObj.CreatedBy = Session["username"].ToString();
                returnObj.Role = Session["Role"].ToString();

                objdt = objdl.Save_AdminWalletTranfer(returnObj);
                if (objdt != null && objdt.Rows.Count > 0)
                {
                    TempData["flag"] = objdt.Rows[0]["flag"].ToString();
                    TempData["msg"] = objdt.Rows[0]["MSG"].ToString();
                }
                returnObj.lstPackage = AllClasses.BindDDL(objdl.GetMasterData(6));
            }
            catch (Exception ex)
            {
                ViewBag.PinAmt = new List<SelectListItem>() { new SelectListItem() { Text = "--none--", Value = "0" } };
            }
            return View(returnObj);
        }


        public ActionResult RankChart()
        {

            PayoutReport objp = new PayoutReport();
            if (Convert.ToString(Session["Role"]) == "2")
                objp.MemberId = Convert.ToString(Session["username"]);

            objp.dt = objdl.USP_GetClosingDateils("14", objp);


            return View(objp);
        }




    }
}
