using MLMPortal.Models;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;


namespace MLMPortal.Controllers
{
    public class LetterController : Controller
    {
        Datalayer objdl = new Datalayer();
        BusinessLayer ObjBusi = new BusinessLayer();
        PropertyClass objp = new PropertyClass();
        string Msg = "", Flag = "";


        public ActionResult Index()
        {
            return View();
        }


        #region Gunja

        public ActionResult WelcomeLetter(string UId)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr;
            string Name = "", MemberId = "", DOB = "", Add = "", regDate = "", EmailId = "", MobNo = "", BankName = "", Branch = "", BankaccNo = "", BankIfscCode = "", PanNo = "", PName = "", PPrice = "";

            return View();
        }

        public ActionResult IDCard(string UId)
        {


            return View();
        }
        public ActionResult KYCUpdate()


        {

            KYCUpdate objp = new KYCUpdate();
            if (Session["username"] != null)
            {
                objp.MemberId = Convert.ToString(Session["username"]);

                objp.dt = objdl.GetKYCUPdate(objp);

                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {

                    objp.PanFilePath = objp.dt.Rows[0]["PANCopy2"].ToString();
                    objp.AdharFilePath = objp.dt.Rows[0]["AADHARCopy2"].ToString();
                    objp.AdharbackFilePath = objp.dt.Rows[0]["Passbookcopy"].ToString();
                    objp.BankPassbookFilePath = objp.dt.Rows[0]["AdharBackCopy"].ToString();


                }

            }
            else
            {
                Response.Redirect("../Account/login");
            }

            return View(objp);





        }


        [HttpPost]
        public ActionResult KYCUpdate(KYCUpdate objp, HttpPostedFileBase PAN, HttpPostedFileBase Adhar, HttpPostedFileBase BankPassbook, HttpPostedFileBase Adharback)
        {
            try
            {
                if (Session["username"] != null)
                {
                    objp.MemberId = Session["username"].ToString();
                    string ProfilePic1 = string.Empty;
                    string ProfilePic2 = string.Empty;

                    string ProfilePic3 = string.Empty;
                    string ProfilePic4 = string.Empty;


                    if (PAN != null)
                    {


                        System.Drawing.Image img = System.Drawing.Image.FromStream(PAN.InputStream);
                        int height = img.Height;
                        int width = img.Width;

                        // imageFilePath1 = Server.MapPath("/Images") + "/default1.png";
                        ProfilePic1 = Path.GetFileName(DateTime.Now + PAN.FileName);
                        PAN.SaveAs(Server.MapPath("/KYC/" + ProfilePic1));
                        objp.PanFilePath = ProfilePic1;
                    }
                    if (Adhar != null)
                    {

                        System.Drawing.Image img = System.Drawing.Image.FromStream(Adhar.InputStream);
                        int height = img.Height;
                        int width = img.Width;
                        ProfilePic2 = Path.GetFileName(DateTime.Now + Adhar.FileName);
                        Adhar.SaveAs(Server.MapPath("/KYC/" + ProfilePic2));
                        objp.AdharFilePath = ProfilePic2;

                    }

                    if (BankPassbook != null)
                    {

                        System.Drawing.Image img = System.Drawing.Image.FromStream(BankPassbook.InputStream);
                        int height = img.Height;
                        int width = img.Width;
                        ProfilePic3 = Path.GetFileName(DateTime.Now + BankPassbook.FileName);
                        BankPassbook.SaveAs(Server.MapPath("/KYC/" + ProfilePic3));
                        objp.BankPassbookFilePath = ProfilePic3;

                    }
                    if (Adharback != null)
                    {

                        System.Drawing.Image img = System.Drawing.Image.FromStream(Adharback.InputStream);
                        int height = img.Height;
                        int width = img.Width;
                        ProfilePic4 = Path.GetFileName(DateTime.Now + Adharback.FileName);
                        Adharback.SaveAs(Server.MapPath("/KYC/" + ProfilePic4));
                        objp.AdharbackFilePath = ProfilePic4;
                    }


                    //objp.Action = "1";
                    DataTable dt = objdl.InsertKYCUPdate(objp, "USP_UpdateKYCDetails");
                    if (dt != null & dt.Rows.Count > 0)
                    {
                        Msg = dt.Rows[0]["msg"].ToString();
                        Flag = "1";

                    }
                    else
                    {

                        Flag = "0";
                        Msg = "Server not Response!.";
                    }
                }
                else
                {
                    Response.Redirect("../Account/login");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //ModelState.Clear();
            TempData["msg"] = Msg;
            TempData["flag"] = Flag;

            return View(objp);
        }


        #endregion
        public ActionResult MemberKYCReport()
        {
            DipoRegistration objp = new DipoRegistration();
            try
            {
                objp.Action = 1;
                objp.dt = objdl.MemberKYCReport(objp, "USP_GetKYCDetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult ApproveMemberKyc(string Id)
        {
            DipoRegistration objp = new DipoRegistration();
            try
            {
                if (Session["username"] != null)
                {
                    objp.EntryBy = Convert.ToString(Session["username"]);
                    objp.MemberId = Id;
                    objp.Action = 4;
                    DataTable dt = objdl.MemberKYCReport(objp, "USP_GetKYCDetails");
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
                else
                {
                    Response.Redirect("../Account/login");
                }
            }
            catch (Exception ex)
            {
                //objp.strId = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MemberKYC_ApprovedReport()
        {
            DipoRegistration objp = new DipoRegistration();
            try
            {
                objp.Action = 2;
                objp.dt = objdl.MemberKYCReport(objp, "USP_GetKYCDetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

    }
}