using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
namespace MLMPortal.Models
{
    public class Datalayer
    {


        dbHelper db = new dbHelper();



        #region PKC

        #region Login
        internal DataTable Wr_get_login(clsLg objT)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objT.Action),
                new SqlParameter("@UserId",objT.UserId),
                new SqlParameter("@Pswd",objT.Pswd)
            };
            dt = db.ExecProcDataTable("Wr_get_login", parm);
            return dt;
        }
        #endregion

        #region Dashboard
        internal DataTable Wr_get_Dash(string DipoId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@DipoId",DipoId),
            };
            dt = db.ExecProcDataTable("GetDashboardDepo", parm);
            return dt;
        }
        internal DataTable get_Dash_admin()
        {
            DataTable dt = new DataTable();
            dt = db.ExecProcDataTable("GetDashboardAdmin");
            return dt;
        }
        internal DataTable Wr_get_Dash_dipo(string DipoId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@DipoId",DipoId),
            };
            dt = db.ExecProcDataTable("GetDashboardDepo_dipo1", parm);
            return dt;
        }
        internal DataTable Wr_get_Dash_ledger_wallet(string DipoId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@DipoId",DipoId),
            };
            dt = db.ExecProcDataTable("GetDashboardDepo_ledger", parm);
            return dt;
        }
        internal DataTable Wr_get_news(int Action)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
            };
            dt = db.ExecProcDataTable("Wr_get_news", parm);
            return dt;
        }

        #endregion

        #region Change Password

        internal DataTable ChangePassword(string action, string MemberId, string oldPassword, string newPassword, string conformPassword)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@action",action),
                new SqlParameter("@MemberId",MemberId),
                new SqlParameter("@oldPassword",oldPassword),
                new SqlParameter("@newPassword",newPassword),
                new SqlParameter("@conformPassword",conformPassword),
            };


            return db.ExecProcDataTable("Proc_ChangePassword", parm);
        }



        internal DataTable ChangePassword_minimart(string action, string MemberId, string oldPassword, string newPassword, string conformPassword)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@action",action),
                new SqlParameter("@MemberId",MemberId),
                new SqlParameter("@oldPassword",oldPassword),
                new SqlParameter("@newPassword",newPassword),
                new SqlParameter("@conformPassword",conformPassword),
            };


            return db.ExecProcDataTable("Proc_ChangePassword_minimart", parm);
        }


        #endregion

        #region View Profile

        internal DataTable wr_get_profile(int Action, string DipoId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@DipoId",DipoId)
            };
            dt = db.ExecProcDataTable("wr_get_profile", parm);
            return dt;
        }

        #endregion

        #region Franchise Product Request

        internal DataTable wr_Fr_Pd_Req(cls_rpt_pack objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@FromDate",objPack.FromDate),
                new SqlParameter("@ToDate",objPack.ToDate),
                new SqlParameter("@AccountCode",objPack.p_Code),
            };
            dt = db.ExecProcDataTable("wr_Fr_Pd_Req", parm);
            return dt;
        }

        internal DataTable sp_ApproveDisApproveProductReq_By_Dipo(cls_rpt_pack objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@MemberId",objPack.p_Code),
                new SqlParameter("@Date",null),
                new SqlParameter("@Remark",objPack.p_name),
                new SqlParameter("@amount",objPack.Amount),
                new SqlParameter("@status",objPack.Hsn),
                new SqlParameter("@ApprovedBy",objPack.Dipo_Code),
                new SqlParameter("@id",objPack.EntryBy),
            };
            dt = db.ExecProcDataTable("sp_ApproveDisApproveProductReq_By_Dipo", parm);
            return dt;
        }

        #endregion

        #region Report

        #region Package
        internal DataTable wr_rpt_pack(cls_rpt_pack objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@p_Code",objPack.p_Code),
                new SqlParameter("@p_name",objPack.p_name),
                new SqlParameter("@Hsn",objPack.Hsn),
            };
            dt = db.ExecProcDataTable("wr_rpt_pack", parm);
            return dt;
        }
        #endregion

        #region Retailer Details
        internal DataTable wr_rpt_RetDtls(cls_rpt_pack objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@FromDate",objPack.FromDate),
                new SqlParameter("@ToDate",objPack.ToDate),
                new SqlParameter("@Mobile",objPack.p_name),
                new SqlParameter("@Name",objPack.p_Code),
            };
            dt = db.ExecProcDataTable("wr_rpt_RetDtls", parm);
            return dt;
        }
        #endregion

        #region Monthly Closing Report
        internal DataTable wr_rpt_MthCls(cls_rpt_pack objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Member_id",objPack.Dipo_Code),
                new SqlParameter("@FromDate",objPack.FromDate),
                new SqlParameter("@ToDate",objPack.ToDate)
            };
            dt = db.ExecProcDataTable("Usp_GetMonthlyIncomeDetails", parm);
            return dt;
        }
        #endregion

        #region Report
        internal DataTable wr_rpt(cls_rpt_pack objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@FromDate",objPack.FromDate),
                new SqlParameter("@ToDate",objPack.ToDate),
                new SqlParameter("@Item_Code",objPack.p_Code),
                new SqlParameter("@Hsn",objPack.Hsn)
            };
            dt = db.ExecProcDataTable("wr_rpt", parm);
            return dt;
        }
        #endregion

        public DataTable GetDipoStockDetails(cls_rpt_pack obj)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Comapanyid","1"),
                 new SqlParameter("@DateFrom", obj.FromDate),
                 new SqlParameter("@ItemCode",obj.p_Code),
                 new SqlParameter("@DipoId",obj.Dipo_Code),

            };
            dt = db.ExecProcDataTable("DSR", parm);
            return dt;


        }


        #region Other
        internal DataTable BindDropdown(int Action, string Id)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@Id",Id),
            };
            dt = db.ExecProcDataTable("BindDropdown", parm);
            return dt;
        }
        #endregion




        #endregion

        #region Product Request

        internal DataTable SaveUpdate_Req(cls_pd_req objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@dtProduct",objPack.dtProduct),
            };
            dt = db.ExecProcDataTable("SaveUpdate_Req", parm);
            return dt;
        }

        #endregion

        #region Sales & Survices

        internal DataTable SaveUpdate_Sales(cls_sales objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@MemberId",objPack.MemberId),
                new SqlParameter("@dtProduct",objPack.dtProduct),
                new SqlParameter("@InvoiceNo",objPack.InvNo),
                   new SqlParameter("@CPWallet",objPack.CPWallet),
                  new SqlParameter("@BagAmount",objPack.BagAmount),
            };
            dt = db.ExecProcDataTable("SaveUpdate_Sales", parm);
            return dt;
        }


        internal DataTable SaveUpdate_SalesMinimart(cls_sales objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@MemberId",objPack.MemberId),
                new SqlParameter("@dtProduct",objPack.dtProduct),
                new SqlParameter("@InvoiceNo",objPack.InvNo),
                 new SqlParameter("@CPWallet",objPack.CPWallet),
                  new SqlParameter("@BagAmount",objPack.BagAmount),
            };
            dt = db.ExecProcDataTable("SaveUpdate_Sales_Minimart", parm);
            return dt;
        }

        #endregion

        #region Retailer Stock

        internal DataTable SaveUpdate_Stock(cls_Ret objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@MemberId",objPack.MemberId),
                new SqlParameter("@dtProduct",objPack.dtProduct),
                new SqlParameter("@InvoiceNo",objPack.InvNo),
                new SqlParameter("@DemandNo",objPack.DemandNo),
            };
            dt = db.ExecProcDataTable("SaveUpdate_Stock", parm);
            return dt;
        }

        #endregion


        #endregion

        #region Prahlad SIngh
        #region Bind Address Proof
        internal DataTable Bind_Adr_Proof()
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

            };
            dt = db.ExecProcDataTable("SP_S_ADDRESS_proof_franchise", parm);
            return dt;
        }
        #endregion

        #region Bind Id Proof

        internal DataTable Bind_Id_Proof()
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

            };
            dt = db.ExecProcDataTable("SP_S_name_proof_franchise", parm);
            return dt;
        }
        #endregion

        #region BindKyc
        internal DataTable BindKyc(string MemberId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
               new SqlParameter("@Action",2),
            new SqlParameter("@MemberId",MemberId),
            };
            dt = db.ExecProcDataTable("USP_DIPOUpdateKYCData", parm);
            return dt;
        }

        internal DataTable saveKycData(cls_dipo_kyc cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@Action",1),
            new SqlParameter("@MemberId",cls.MemberId),
            new SqlParameter("@CompanyNameProofAttachment",cls.CompanyNameProofAttachment),
            new SqlParameter("@PanCardAttachment",cls.PanCardAttachment),
            new SqlParameter("@CompanyAddressProofAttachment",cls.CompanyAddressProofAttachment),
            new SqlParameter("@PanCard",cls.PanCard),
            new SqlParameter("@CompanyNameProof",cls.CompanyNameProof),
            new SqlParameter("@CompanyAddressProof",cls.CompanyAddressProof),
            new SqlParameter("@Status",0),
            };
            dt = db.ExecProcDataTable("USP_DIPOUpdateKYCData", parm);
            return dt;
        }
        #endregion


        #region fund management

        internal DataTable fundManageMent(cls_fundmgnt cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
               new SqlParameter("@Action",cls.Action),
            new SqlParameter("@MemberId",cls.MemberId),
            };
            dt = db.ExecProcDataTable("Proc_FundMgnt", parm);
            return dt;
        }
        internal DataTable BindBank(cls_fundmgnt cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
            //   new SqlParameter("@Action",cls.Action),
            //new SqlParameter("@MemberId",cls.MemberId),
            };
            dt = db.ExecProcDataTable("Sp_GetBankDetailsBind", parm);
            return dt;
        }


        internal DataTable Sp_InsertMoneyTransfer(cls_fundmgnt cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
             //new SqlParameter("@Action",cls.Action),
           new SqlParameter("@PaymentType","BankWallet"),
           new SqlParameter("@BankName",cls.bankid),
           new SqlParameter("@BankACNo",cls.AccNo),
           new SqlParameter("@iFSCCOde",cls.Ifsc),
           new SqlParameter("@BankBranch",cls.Branch),
           new SqlParameter("@BankAccountHolderName",cls.AccHolderName),
           new SqlParameter("@MoneyAmount",cls.Amount),
           new SqlParameter("@TransactionId",cls.tranID),
           new SqlParameter("@PaymentDate",cls.PayDate),
           new SqlParameter("@MemberID",cls.MemberId),
           new SqlParameter("@DipositType",cls.DepositType),
           new SqlParameter("@ChequeNo",cls.ChecqueNo),
           new SqlParameter("@ReferenceNo",cls.RefNo),
           new SqlParameter("@NEFT",cls.RefNo),
           new SqlParameter("@Time",DateTime.Now.ToString("HH:mm")),
           new SqlParameter("@MobileNo",cls.MobileNo),
           new SqlParameter("@Remarks",cls.Remark),
           new SqlParameter("@Files",cls.fileUrl)
            };
            dt = db.ExecProcDataTable("Sp_InsertFundRequest", parm);
            return dt;
        }

        internal DataTable Sp_FundtransferHistory(cls_fundmgnt cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
               new SqlParameter("@Action",cls.Action),
            new SqlParameter("@Member_Id",cls.MemberId),
            new SqlParameter("@Fromdate",cls.FromDate),
            new SqlParameter("@Todate",cls.Todate),
            };
            dt = db.ExecProcDataTable("Sp_FundtransferHistory", parm);
            return dt;
        }
        internal DataTable Franchise_Transaction(cls_fundmgnt cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
               new SqlParameter("@flag","F"),
            new SqlParameter("@Franchise_id",cls.MemberId),
            new SqlParameter("@status","F"),
            new SqlParameter("@Date","A"),
            new SqlParameter("@dateto",cls.Todate==null?DateTime.Now.ToShortDateString():cls.Todate),
            new SqlParameter("@datefrom",cls.FromDate==null?DateTime.Now.ToShortDateString():cls.FromDate),
            new SqlParameter("@balance","0.00"),
            };
            dt = db.ExecProcDataTable("SP_S_GET_MEMBER_INCOME_REPORT_Franchise", parm);
            return dt;
        }
        internal DataTable Franchise_Dipo_Ledger(cls_fundmgnt cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
               new SqlParameter("@flag","F"),
            new SqlParameter("@Franchise_id",cls.MemberId),
            new SqlParameter("@status","F"),
            new SqlParameter("@Date","A"),
            new SqlParameter("@dateto",cls.Todate==null?DateTime.Now.ToShortDateString():cls.Todate),
            new SqlParameter("@datefrom",cls.FromDate==null?DateTime.Now.ToShortDateString():cls.FromDate),
            new SqlParameter("@balance","0.00"),
            };
            dt = db.ExecProcDataTable("sp_franchise_and_dipo_ledger_report", parm);
            return dt;
        }
        #endregion


        #region Center Login
        internal DataTable center_get_login(clsLg objT)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objT.Action),
                new SqlParameter("@UserId",objT.UserId),
                new SqlParameter("@Pswd",objT.Pswd)
            };
            dt = db.ExecProcDataTable("center_get_login", parm);
            return dt;
        }
        #endregion


        #region center view profile
        internal DataTable center_get_profile(int Action, string DipoId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@DipoId",DipoId)
            };
            dt = db.ExecProcDataTable("center_get_profile", parm);
            return dt;
        }


        internal DataTable minimart_get_profile(int Action, string DipoId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@DipoId",DipoId)
            };
            dt = db.ExecProcDataTable("minimart_get_profile", parm);
            return dt;
        }


        #endregion

        #region Center KYC
        internal DataTable BindCenterKyc(string MemberId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
               new SqlParameter("@Action",2),
            new SqlParameter("@MemberId",MemberId),
            };
            dt = db.ExecProcDataTable("USP_FranciseUpdateKYCData", parm);
            return dt;
        }


        internal DataTable BindMinimartKyc(string MemberId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
               new SqlParameter("@Action",2),
            new SqlParameter("@MemberId",MemberId),
            };
            dt = db.ExecProcDataTable("USP_MinimartUpdateKYCData", parm);
            return dt;
        }



        internal DataTable savecenterKycData(cls_dipo_kyc cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@Action",1),
            new SqlParameter("@MemberId",cls.MemberId),
            new SqlParameter("@CompanyNameProofAttachment",cls.CompanyNameProofAttachment),
            new SqlParameter("@PanCardAttachment",cls.PanCardAttachment),
            new SqlParameter("@CompanyAddressProofAttachment",cls.CompanyAddressProofAttachment),
            new SqlParameter("@PanCard",cls.PanCard),
            new SqlParameter("@CompanyNameProof",cls.CompanyNameProof),
            new SqlParameter("@CompanyAddressProof",cls.CompanyAddressProof),
            new SqlParameter("@Status",0),
            };
            dt = db.ExecProcDataTable("USP_FranciseUpdateKYCData", parm);
            return dt;
        }

        internal DataTable saveminimartKycData(cls_dipo_kyc cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@Action",1),
            new SqlParameter("@MemberId",cls.MemberId),
            new SqlParameter("@CompanyNameProofAttachment",cls.CompanyNameProofAttachment),
            new SqlParameter("@PanCardAttachment",cls.PanCardAttachment),
            new SqlParameter("@CompanyAddressProofAttachment",cls.CompanyAddressProofAttachment),
            new SqlParameter("@PanCard",cls.PanCard),
            new SqlParameter("@CompanyNameProof",cls.CompanyNameProof),
            new SqlParameter("@CompanyAddressProof",cls.CompanyAddressProof),
            new SqlParameter("@Status",0),
            };
            dt = db.ExecProcDataTable("USP_MinimartUpdateKYCData", parm);
            return dt;
        }





        #endregion

        #region
        internal DataTable Sp_InsertMoneyTransfer1(cls_fundmgnt cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
             //new SqlParameter("@Action",cls.Action),
           new SqlParameter("@PaymentType","BankWallet"),
           new SqlParameter("@BankName",cls.bankid),
           new SqlParameter("@BankACNo",cls.AccNo),
           new SqlParameter("@iFSCCOde",cls.Ifsc),
           new SqlParameter("@BankBranch",cls.Branch),
           //new SqlParameter("@BankAccountHolderName",cls.AccHolderName),
           new SqlParameter("@MoneyAmount",cls.Amount),
           new SqlParameter("@TransactionId",cls.tranID),
           new SqlParameter("@PaymentDate",cls.PayDate),
           new SqlParameter("@MemberID",cls.MemberId),
           new SqlParameter("@DipositType",cls.DepositType),
           new SqlParameter("@ChequeNo",cls.ChecqueNo),
           new SqlParameter("@ReferenceNo",cls.RefNo),
           new SqlParameter("@NEFT",cls.RefNo),
           new SqlParameter("@Time",DateTime.Now.ToString("HH:mm")),
           new SqlParameter("@MobileNo",cls.MobileNo),
           new SqlParameter("@Remarks",cls.Remark),
           new SqlParameter("@Files",cls.fileUrl),
           new SqlParameter("@msg","test"),
            };
            dt = db.ExecProcDataTable("Sp_InsertMoneyTransfer1", parm);
            return dt;
        }
        #endregion
        #endregion



        internal DataTable USP_User_Login(string username, string Pass, string OTP)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Employee_UserName",username),
                new SqlParameter("@Employee_Password",Pass),
                new SqlParameter("@OTP",OTP)
            };
            dt = db.ExecProcDataTable("USP_User_Login", parm);
            return dt;
        }



        internal DataTable GetSellerDetails(string User_Id)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action","1"),
                new SqlParameter("@User_Id",User_Id)
            };
            dt = db.ExecProcDataTable("Proc_SellerDetails", parm);
            return dt;
        }


        internal DataTable ResetPassword(string oldPassword, string newPassword, string employeeCode)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@oldPassword",oldPassword),
                new SqlParameter("@newPassword",newPassword),
                new SqlParameter("@employeeCode",employeeCode)
            };
            dt = db.ExecProcDataTable("USP_ResetPassword", parm);
            return dt;
        }

        internal DataTable GetMasterData(int Action, string id1 = null, string id2 = null, string id3 = null)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@id1",id1),
                new SqlParameter("@id2",id2),
                new SqlParameter("@id3",id3)
            };
            dt = db.ExecProcDataTable("GetMasterData", parm);
            return dt;
        }


        internal DataTable frnachisedayclose(int Action)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),

            };
            dt = db.ExecProcDataTable("GetMasterData", parm);
            return dt;
        }

        internal DataTable Admindayclose(int Action, string FranchiseId, string Pincode)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@franchiseid",FranchiseId),
                  new SqlParameter("@Pincode",Pincode),
            };
            dt = db.ExecProcDataTable("proc_dayclosefranchise", parm);
            return dt;
        }

        internal DataTable DeleteMasterData(int Action, string id1 = null, string id2 = null)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@id1",id1),
                new SqlParameter("@id2",id2)
            };
            dt = db.ExecProcDataTable("DeleteMasterData", parm);
            return dt;
        }

        internal DataTable GetExcelData(int Action, string id1 = null, string id2 = null)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@id1",id1),
                new SqlParameter("@id2",id2)
            };
            dt = db.ExecProcDataTable("GetExcelData", parm);
            return dt;
        }


        ///Aditya
        #region Aditya 
        public DataTable getLoginDetails(Login Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName",Objp.UserName),
                new SqlParameter("@Password",Objp.Password),
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable AddCategory(Master Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@Category_Id",Objp.CategoryId),
                new SqlParameter("@Category_Name",Objp.CategoryName),
                new SqlParameter("@Image",Objp.FilePath),
                new SqlParameter("@CreatedBy",Objp.EntryBy),
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable CompanyMaster(Master Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@Company_Id",Objp.CategoryId),
                new SqlParameter("@Company_Name",Objp.Name),
                new SqlParameter("@Address",Objp.Address),
                new SqlParameter("@Email",Objp.EmailId),
                new SqlParameter("@Phone",Objp.Contact),
                new SqlParameter("@Website",Objp.Website),
                new SqlParameter("@gstNo",Objp.GSTNo),
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable BankMaster(Master Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@ID",Objp.CategoryId),
                new SqlParameter("@BankName",Objp.Name),
                new SqlParameter("@BankAcNo",Objp.AccountNo),
                new SqlParameter("@IFSCCOde",Objp.IFSCCode),
                new SqlParameter("@Branch",Objp.BankBranch),
                new SqlParameter("@Name",Objp.CategoryName),

                new SqlParameter("@Action",Objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable NewsMaster(Master Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@ID",Objp.CategoryId),
                new SqlParameter("@NewsHeading",Objp.NewsHeading),
                new SqlParameter("@NewsPrefer",Objp.NewsPrefer),
                new SqlParameter("@NewsFor",Objp.NewsFor),
                new SqlParameter("@Enterdby",Objp.EntryBy),
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable BannerMaster(Master Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@ID",Objp.CategoryId),
                new SqlParameter("@Image",Objp.FilePath),
                new SqlParameter("@CraetedBy",Objp.EntryBy),
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable BindCategory(int Action, int Id)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action),
                new SqlParameter("@Category_Id",Id)
            };
            dtt = db.ExecProcDataTable("saveUpdateCategory", param);
            return dtt;
        }

        //radhe
        //public DataTable BindCountry(int Action, int Id)
        //{
        //    DataTable dtt = new DataTable();
        //    SqlParameter[] param = new SqlParameter[]
        //    {
        //        new SqlParameter("@Action",Action),
        //        new SqlParameter("@Category_Id",Id)
        //    };
        //    dtt = db.ExecProcDataTable("saveUpdateCategory", param);
        //    return dtt;
        //}
        //radhe
        public DataTable AddSubCategory(Master Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@SubCategory_Id",Objp.SubCategoryId),
                  new SqlParameter("@Category_Id",Objp.CategoryId),
                new SqlParameter("@SubCategory_Name",Objp.SubCategoryName),
                new SqlParameter("@CreatedBy",Objp.EntryBy),
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable AddProduct(Master Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@Id",Objp.Id),
                 new SqlParameter("@KitName",Objp.Name),
                 //new SqlParameter("@KitAmount",Objp.SaleRate),
                   new SqlParameter("@KitAmount",Objp.MRP),
                 new SqlParameter("@MRP",Objp.MRP),
                 new SqlParameter("@DP",Objp.DP),
                  //  new SqlParameter("@DP",Objp.SaleRate),
                      new SqlParameter("@NewDP",Objp.DP),
                 new SqlParameter("@BV",Objp.BV),
                 new SqlParameter("@GST",Objp.GST),
                 new SqlParameter("@CGST",Objp.CGST),
                 new SqlParameter("@SGST",Objp.SGST),
                 new SqlParameter("@PayableAmount",Objp.PurchaseRate),
                 new SqlParameter("@BatchaNo",Objp.BatchNo),
                 new SqlParameter("@PurchaseRate",Objp.PurchaseRate),
                 new SqlParameter("@MfgDate",Objp.MfgDate),
                 new SqlParameter("@ExpDate",Objp.ExpDate),
                 new SqlParameter("@PStatus",Objp.PurchaseGst),
                 new SqlParameter("@SStatus",Objp.SaleGst),
                 new SqlParameter("@HSNCode",Objp.HSNCode),
                 new SqlParameter("@Image",Objp.FilePath),
                 new SqlParameter("@CategoryId",Objp.CategoryId),
                 new SqlParameter("@SubCategoryId",Objp.SubCategoryId),
                 new SqlParameter("@Dis",Objp.Description),
                 new SqlParameter("@CreatedBy",Objp.EntryBy),
                 new SqlParameter("@APP",Objp.APP),
                 new SqlParameter("@RP",Objp.RP),
                 new SqlParameter("@CP",Objp.CP),
                   new SqlParameter("@DiscountPer",Objp.DiscountPer),
                 new SqlParameter("@Action",Objp.Action),
                 new SqlParameter("@Isoffer",Objp.Isoffer)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable AddMember(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Name",objp.Name),
                new SqlParameter("@f_name",objp.FatherName),
                new SqlParameter("@mobile",objp.MobileNo),
                new SqlParameter("@Email",objp.EmailId),
                new SqlParameter("@Intro_id",objp.SponserId),
                //new SqlParameter("@Direction",objp.Direction),
                new SqlParameter("@UserId",objp.EntryBy),
                new SqlParameter("@TransPassword",objp.TranPass),
                new SqlParameter("@gender",objp.Gender),
                new SqlParameter("@Pass",objp.Pass),
                new SqlParameter("@ParentId",objp.ParentId)

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable MemberBank(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@payeeName",objp.Name),
                new SqlParameter("@MemberID",objp.MemberId),
                new SqlParameter("@BankaccNo",objp.AccountNo),
                new SqlParameter("@BankName",objp.BankName),
                new SqlParameter("@BankBranch",objp.BankBranch),
                new SqlParameter("@BankIfscCode",objp.IFSCCode),
                new SqlParameter("@panno",objp.PanNo),
                new SqlParameter("@UpdatedBy",objp.EntryBy)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable BlockMember(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@MemberId",objp.MemberId),
                new SqlParameter("@ChkValue",objp.Id),
                new SqlParameter("@Remark",objp.Remark!=""?objp.Remark:null)

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable MemberReport(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable MemberPassword(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@MemberId",objp.MemberId),
                 new SqlParameter("@ConfirmPassword",objp.ConPass)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable AdminPassword(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@MemberId",objp.MemberId),
                 new SqlParameter("@ConfirmPassword",objp.ConPass),
                   new SqlParameter("@OldPassword",objp.TranPass)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable ActiveMemberReport(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Action",objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable DirectTeam(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@MemberID",objp.MemberId!=""?objp.MemberId:null)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetIDforDipo(DipoRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_Id",objp.MemberId)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable BindCountry(int Action)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action)

            };
            dtt = db.ExecProcDataTable("USP_BindCountry", param);
            return dtt;
        }

        //radhe
        public DataTable BindCountry(Registration mr)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                //new SqlParameter("@Member_Id",mr.MemberId)

            };
            dtt = db.ExecProcDataTable("USP_BindCountry", param);
            return dtt;
        }
        public DataTable BindPhoneCode()
        {
            DataTable dtt = new DataTable();
            dtt = db.ExecProcDataTable("Sp_GetBindCountry");
            return dtt;
        }
        public DataTable BindMonth(Registration mr)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Member_Id",mr.MemberId)

            };
            dtt = db.ExecProcDataTable("USP_BindCountry", param);
            return dtt;
        }
        public DataTable BindYear(Registration mr)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Member_Id",mr.MemberId)

            };
            dtt = db.ExecProcDataTable("USP_BindCountry", param);
            return dtt;
        }
        public DataTable BindGender(Registration mr)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Member_Id",mr.MemberId)

            };
            dtt = db.ExecProcDataTable("USP_BindCountry", param);
            return dtt;
        }
        //radhe

        public DataTable BindState(DipoRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@country_id",objp.CountryId)

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable AddDipo(DipoRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Name",objp.Name),
                new SqlParameter("@mobile",objp.MemberId),
                new SqlParameter("@MobileNo",objp.MobileNo),
                new SqlParameter("@EmailId",objp.EmailId),
                new SqlParameter("@GST",objp.Gst),
                new SqlParameter("@Branch",objp.Branch),
                new SqlParameter("@TPass",objp.TranPass),
                new SqlParameter("@Pass",objp.Pass),
                 new SqlParameter("@PanNo",objp.PanNo),
                new SqlParameter("@Addresss",objp.Address),
                new SqlParameter("@GSTno",objp.GSTNo),
                new SqlParameter("@BankName",objp.BankName),
                new SqlParameter("@AccountNo",objp.AccountNo),
                new SqlParameter("@IfscCode",objp.IFSCCode),
                new SqlParameter("@AccountHolderName",objp.AccName),
                new SqlParameter("@entryid",objp.Id),
                new SqlParameter("@District_Id",objp.CityId),
                 new SqlParameter("@State_Id",objp.StateId) ,
                new SqlParameter("@Country_Id",objp.CountryId),
                  new SqlParameter("@BankBranch",objp.BankBranch),
                  new SqlParameter("@Action",objp.Action)

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable DipoReport(DipoRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Action",objp.Action),
                 new SqlParameter("@Id",objp.Id),
                   new SqlParameter("@AprooveBy",objp.EntryBy)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable DirectTeam(DipoRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@MemberID",objp.MemberId!=""?objp.MemberId:null)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable BindStateFran(FranRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@country_id",objp.CountryId)

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable Bind(PropertyClass objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",objp.Action),
                new SqlParameter("@country_id",objp.CountryId),
                new SqlParameter("@CompanyCode",objp.CompanyCode),
                 new SqlParameter("@AccountCode",objp.AccountCode),
                 new SqlParameter("@ItemCode",objp.ItemCode),

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable FranReport(FranRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Action",objp.Action),
                 new SqlParameter("@Id",objp.Id),
                   new SqlParameter("@AprooveBy",objp.EntryBy)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        internal DataTable insertSalesPerson(Delivery del, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@Action",del.Action),
                new SqlParameter("@Name",del.Name),
                new SqlParameter("@ContactNo",del.ContactNo),
                new SqlParameter("@Address",del.Address),
                new SqlParameter("@AadharNo",del.AadharNo),
                new SqlParameter("@EmailId",del.Emailid),
                new SqlParameter("@UserName",del.userName),
                new SqlParameter("@Password",del.Password),
                new SqlParameter("@Pincode",del.Pincode),
                new SqlParameter("@VolunteerId",del.VolunteerId),
                new SqlParameter("@Id",del.SpCode),
            };
            dt = db.ExecProcDataTable(ProcName, sp);
            return dt;
        }
        public DataTable GetIDforFran(FranRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_Id",objp.MemberId)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable AddFran(FranRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Name",objp.Name),
                new SqlParameter("@mobile",objp.MemberId),
                new SqlParameter("@MobileNo",objp.MobileNo),
                new SqlParameter("@EmailId",objp.EmailId),
                new SqlParameter("@GST",objp.Gst),
                new SqlParameter("@Branch",objp.Branch),
                new SqlParameter("@TPass",objp.TranPass),
                new SqlParameter("@Pass",objp.Pass),
                 new SqlParameter("@PanNo",objp.PanNo),
                new SqlParameter("@Addresss",objp.Address),
                new SqlParameter("@GSTno",objp.GSTNo),
                new SqlParameter("@BankName",objp.BankName),
                new SqlParameter("@AccountNo",objp.AccountNo),
                new SqlParameter("@IfscCode",objp.IFSCCode),
                new SqlParameter("@AccountHolderName",objp.AccName),
                new SqlParameter("@CityId",objp.CityId),
                 new SqlParameter("@StateId",objp.StateId) ,
                new SqlParameter("@CountryId",objp.CountryId),
                 new SqlParameter("@DIPO_Code",objp.DipoCode),
                  new SqlParameter("@BankBranch",objp.BankBranch),
                   new SqlParameter("@TinNo",objp.TinNo),
                   new SqlParameter("@Pincode",objp.PinCode),
                   new SqlParameter("@Action",objp.Action),
                   new SqlParameter("@Id",objp.Id),
                   new SqlParameter("@Fraichaise_name",objp.Franchise_Name)

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable FranchiseReport(FranRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Action",objp.Action),
                 new SqlParameter("@Id",objp.Id),
                   new SqlParameter("@AprooveBy",objp.EntryBy)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable BindDipo(int Action)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action)

            };
            dtt = db.ExecProcDataTable("USP_BindDipo", param);
            return dtt;
        }
        ///End Aditya
        #endregion Aditya
        internal DataTable BindProductOnDashboard(string action, string Category, string ProductTitle)
        {
            string[] sp = new string[2];

            if (Category != null)
            {
                sp = Category.Split('|');
                Category = sp[0];
                if (sp.Length > 1)
                {
                    ProductTitle = sp[1];
                }
            }

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",(!string.IsNullOrEmpty(action)?action:null)),
                new SqlParameter("@Category",(!string.IsNullOrEmpty(Category)?Category:null)),
                 new SqlParameter("@ProductTitle",(!string.IsNullOrEmpty(ProductTitle)?ProductTitle:null))
            };
            dt = db.ExecProcDataTable("proc_BindProductItemsOnDashboard", parm);
            return dt;
        }
        internal DataTable AddToCart(HomeDashboardModel obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",obj.Action),
                new SqlParameter("@CustomerID",obj.CustomerID),
                 new SqlParameter("@ProdID",obj.ProdID),
                 new SqlParameter("@UserId",obj.UserId),
                 new SqlParameter("@Password",obj.Password),
                 new SqlParameter("@ProdPrice",obj.ProdPrice),
                  new SqlParameter("@Quantity",obj.Quantity)

            };
            dt = db.ExecProcDataTable("proc_AddToCart", parm);
            return dt;
        }
        internal DataTable ShowCart(HomeDashboardModel obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action","2"),
                new SqlParameter("@CustomerID",obj.CustomerID)
            };
            dt = db.ExecProcDataTable("proc_AddToCart", parm);
            return dt;
        }
        internal DataTable BindAllProduct(string query, string Action, string subcatid, string cateId = null)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@ProductTitle",subcatid),
                new SqlParameter("@Category",cateId),
                new SqlParameter("@query",query)
            };
            dt = db.ExecProcDataTable("proc_BindProductItemsOnDashboard", parm);
            return dt;
        }
        public DataTable getDashboardLoginDetails(HomeDashboardModel Objp)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserId",Objp.UserId),
                new SqlParameter("@Password",Objp.Password),
                new SqlParameter("@CustomerID",Objp.CustomerID),
                new SqlParameter("@Action",4)
            };
            dtt = db.ExecProcDataTable("proc_AddToCart", param);
            return dtt;
        }

        public DataTable BindStateForHome()
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",8)
            };
            dtt = db.ExecProcDataTable("proc_AddToCart", param);
            return dtt;
        }

        public DataTable PayoutReport(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),
                   new SqlParameter("@Action",objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable PaymentReport(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),
                   new SqlParameter("@Action",objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable Payoutdetails(Payoutdetails objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable CustomerPayoutReport(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),
                   new SqlParameter("@Action",objp.Action),
                   new SqlParameter("@MemberName",objp.MemberName),
                   new SqlParameter("@ApprovedDate",objp.ApprovedDate),
                   new SqlParameter("@RequestDate",objp.RequestDate),
                   new SqlParameter("@DepositType",objp.DepositType),
                   new SqlParameter("@TransactionID",objp.TransactionID)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable DipoPayoutReport(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),
                   new SqlParameter("@Action",objp.Action),
                   new SqlParameter("@MemberName",objp.MemberName),
                   new SqlParameter("@ApprovedDate",objp.ApprovedDate),
                   new SqlParameter("@RequestDate",objp.RequestDate),
                   new SqlParameter("@DepositType",objp.DepositType),
                   new SqlParameter("@TransactionID",objp.TransactionID)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable FranPayoutReport(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),
                   new SqlParameter("@Action",objp.Action),
                   new SqlParameter("@MemberName",objp.MemberName),
                   new SqlParameter("@ApprovedDate",objp.ApprovedDate),
                   new SqlParameter("@RequestDate",objp.RequestDate),
                   new SqlParameter("@DepositType",objp.DepositType),
                   new SqlParameter("@TransactionID",objp.TransactionID)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable GetProductModal(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@DemandNo",objp.Id),
                   new SqlParameter("@Action",objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable PayoutClickFuncton(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId),
                new SqlParameter("@UID",objp.UID),
                new SqlParameter("@Action",objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable BusinessReport(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),
                   new SqlParameter("@Action",objp.Action),
                    new SqlParameter("@PromoterStatus",objp.UID)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable ApproveProductReport(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),
                   new SqlParameter("@Action",objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable ApproveProduct(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@Date",null),
                new SqlParameter("@MemberId",objp.MemberId),
                new SqlParameter("@Remark",objp.Remark),
                new SqlParameter("@amount",objp.Amount),
                new SqlParameter("@status",objp.Status),
                new SqlParameter("@ApprovedBy",objp.EntryBy),
                new SqlParameter("@id",objp.Id)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable ApproveCustumerFund(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@Date",null),
                new SqlParameter("@MemberId",objp.MemberId),
                new SqlParameter("@Remark",objp.Remark),
                new SqlParameter("@amount",objp.Amount),
                new SqlParameter("@status",objp.Status),
                new SqlParameter("@ApprovedBy",objp.EntryBy),
                new SqlParameter("@id",objp.Id)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable ApprovAndRecjectDipoFund(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@Date",null),
                new SqlParameter("@MemberId",objp.MemberId),
                new SqlParameter("@Remark",objp.Remark),
                new SqlParameter("@amount",objp.Amount),
                new SqlParameter("@status",objp.Status),
                new SqlParameter("@ApprovedBy",objp.EntryBy),
                new SqlParameter("@id",objp.Id)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable ApproveDisapproveLuxFundRequest(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@Date",null),
                new SqlParameter("@MemberId",objp.MemberId),
                new SqlParameter("@Remark",objp.Remark),
                new SqlParameter("@amount",objp.Amount),
                new SqlParameter("@status",objp.Status),
                new SqlParameter("@ApprovedBy",objp.EntryBy),
                new SqlParameter("@id",objp.Id)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable ApprovAndRecjectFranFund(PayoutReport objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@Date",null),
                new SqlParameter("@MemberId",objp.MemberId),
                new SqlParameter("@Remark",objp.Remark),
                new SqlParameter("@amount",objp.Amount),
                new SqlParameter("@status",objp.Status),
                new SqlParameter("@ApprovedBy",objp.EntryBy),
                new SqlParameter("@id",objp.Id)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertPurchaseOrder(PropertyClass Objp, string ProcName, DataTable dt)
        {
            DateTime? chqDate = null;
            DateTime? BanktxnDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                chqDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                BanktxnDate = Convert.ToDateTime(Objp.eDate);
            }


            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyId","1"),
                new SqlParameter("@branchcode",Objp.BranchCode),
                new SqlParameter("@supplieraccountcode",Objp.SupplierAccCode),
                new SqlParameter("@invoicedate",Objp.InvoiceDate),
                new SqlParameter("@shipmentpreference",Objp.ShipmentPref),
                new SqlParameter("@stateid",Objp.StateId),
                new SqlParameter("@purchasefile",Objp.PurchaseFile),
                new SqlParameter("@deliverto",Objp.DeliveryTo),
                new SqlParameter("@termscondiotions",Objp.TermsCond),
                new SqlParameter("@notes",Objp.notes),
                new SqlParameter("@discountpersentage",Objp.DiscPer),
                new SqlParameter("@discountamount",Objp.DiscAmt),
                new SqlParameter("@cgstamount",Objp.CgstAmt),
                new SqlParameter("@sgstamount",Objp.SgstAmt),
                new SqlParameter("@igstamount",Objp.IgstAmt),
                new SqlParameter("@payableamount",Objp.PayableAmt),
                new SqlParameter("@grosspayable",Objp.GrossPayable),
                new SqlParameter("@nettotal",Objp.NetTotal),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@totalpayablegst",Objp.Payablegst),
                new SqlParameter("@isfreightinclude",Objp.IsFrieghtInclude),
                new SqlParameter("@freightcharges",Objp.FrieghtCharges),
                new SqlParameter("@gstin",Objp.GstNo),
                new SqlParameter("@billingaddress",Objp.Address),
                new SqlParameter("@invoiceno",Objp.InvoiceNo),
                new SqlParameter("@ewaybillno",Objp.EwayBillNo),
                new SqlParameter("@roundoffamt",Objp.RoundOffAmt),
                new SqlParameter("@PaymentMode",Objp.PayMode),
                new SqlParameter("@typePurchaseOrderItemInsert",dt),

                new SqlParameter("@GroupCode",Objp.GroupCode),
                new SqlParameter("@AccountCode",Objp.AccountCode),
                new SqlParameter("@ChequeNo",Objp.ChqNo),
                new SqlParameter("@ChequeDate",chqDate),
                new SqlParameter("@CheqBank",Objp.Bankname),
                new SqlParameter("@BankTxnNo",Objp.BankTransId),
                new SqlParameter("@BankTxnDate",BanktxnDate),
                new SqlParameter("@PaidAmount",Objp.PaidAmount),
                new SqlParameter("@VoucherNo",Objp.VoucherNo),
                new SqlParameter("@DueOn",Objp.DueOn),
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable InsertSuppliersAccount(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@account_name",Objp.SSName),
               new SqlParameter("@mobile",Objp.ContactNo),
                new SqlParameter("@address",Objp.Address),
                new SqlParameter("@City",Objp.CityName),
              //  new SqlParameter("@StateId",Objp.StateId),
              //  new SqlParameter("@PinCode",Objp.PinCode),
                new SqlParameter("@GSTNo",Objp.GstNo),
              //  new SqlParameter("@PanNo",Objp.PanNo),
                new SqlParameter("@TINNo",Objp.TINNo),
             new SqlParameter("@AccountCode","0"),
                new SqlParameter("@comapanyid",Objp.CompanyCode),
                new SqlParameter("@userid",Objp.UserName),
                 new SqlParameter("@GSTExpiryDate",Objp.GSTExpiryDate),
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        // Akash start 

        internal DataTable AddCustomerAddress(HomeDashboardModel obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",obj.Action),
                new SqlParameter("@CustomerID",obj.CustomerID),
                new SqlParameter("@FullName",obj.FullName),
                 new SqlParameter("@ContactNo",obj.ContactNo),
                 new SqlParameter("@EmailID",obj.EmailID),
                 new SqlParameter("@AddressType",obj.AddressType),
                 new SqlParameter("@Address",obj.Address),
                 new SqlParameter("@Landmark",obj.Landmark),
                 new SqlParameter("@City",obj.City),
                 new SqlParameter("@State",obj.State),
                 new SqlParameter("@PinCode",obj.PinCode),
                  new SqlParameter("@AddressID",obj.AddressID),
                    new SqlParameter("@AreaId",obj.AreaId)
            };
            dt = db.ExecProcDataTable("proc_AddToCart", parm);
            return dt;
        }
        internal DataTable GetPruductDeatkl(HomeDashboardModel obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",obj.Action),
                new SqlParameter("@ProductID",obj.ProdID)
            };
            dt = db.ExecProcDataTable("proc_GetProductDeatls", parm);
            return dt;
        }


        internal DataTable PlaceOrdercheckout(HomeDashboardModel obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",obj.Action),
                new SqlParameter("@CustomerID",obj.CustomerID),

                  new SqlParameter("@AddressID",obj.AddressID),
                  new SqlParameter("@Username",obj.UserId),
                  new SqlParameter("@TransactionId",obj.TransactionId),
                  new SqlParameter("@AccountName",obj.FullName)
            };
            dt = db.ExecProcDataTable("proc_Place_Order", parm);
            return dt;
        }


        public DataSet GetInvoiceDetails(HomeDashboardModel obj, string InvoiceId)
        {
            DataSet dtt = new DataSet();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@Action",obj.Action),
                new SqlParameter("@CustomerID",obj.CustomerID),
                  new SqlParameter("@InvoiceNo",InvoiceId)
            };
            dtt = db.ExecProcDataSet("proc_Salesbilloncheckout", param);
            return dtt;
        }
        // Akash end 


        //prahlad singh
        #region for wishlist
        internal DataTable saveupdatewishlist(string ProductId, string UseriD, string Action, string Id)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@ProId",ProductId),
                 new SqlParameter("@customerId",UseriD),
                 new SqlParameter("@id",Id),

            };
            dt = db.ExecProcDataTable("proc_wishlist", parm);
            return dt;
        }
        #endregion
        //

        #region prahlad singh 
        //code for save update multiple image
        public DataTable saveupdatemultiIMG(DataTable dtimg, string Action, string Productid)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@MultiImg",dtimg),
                 new SqlParameter("@Productid",Productid),


            };
            dt = db.ExecProcDataTable("proc_multiImg", parm);
            return dt;
        }

        #endregion

        #region for Orderlist
        internal DataTable CustomewrOrderList(string Action, string CustomerID, string OrderNo, string type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@CustomerId",CustomerID),
                 new SqlParameter("@OrderNo",OrderNo),
                 new SqlParameter("@type",type),

            };
            dt = db.ExecProcDataTable("proc_orderReport", parm);
            return dt;
        }
        #endregion

        #region for updatememberdetails
        internal DataTable updatememberdetails(Registration cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@MemberId",cls.MemberId),
                 new SqlParameter("@Name",cls.Name),
                 new SqlParameter("@FatherName",cls.FatherName),
                 new SqlParameter("@EmailId",cls.EmailId),
                 new SqlParameter("@MobileNo",cls.MobileNo),
                 new SqlParameter("@ZipCode",cls.ZipCode),
                 new SqlParameter("@Action",cls.Action),
                 new SqlParameter("@oldpass",cls.Pass),
                 new SqlParameter("@newpass",cls.ConPass),

            };
            dt = db.ExecProcDataTable("proc_updateMemberDetails", parm);
            return dt;
        }
        #endregion


        #region getDistributerDetails

        internal DataTable getDistributerDetails(Registration cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",cls.Action),
                 new SqlParameter("@MemberId",cls.MemberId),
                 new SqlParameter("@UserName",cls.Name),
                 new SqlParameter("@Remarks",cls.Remark),
                 new SqlParameter("@InviceNo",cls.GSTNo),

            };
            dt = db.ExecProcDataTable("USP_VerfyCustomerOrder", parm);
            return dt;
        }
        #endregion

        #region Verify Order
        internal DataTable VeriFyOrder(Registration cls)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",cls.MemberId),
                 new SqlParameter("@InviceNo",cls.GSTNo),
                 new SqlParameter("@UserName",cls.Name),

            };
            dt = db.ExecProcDataTable("USP_VerfyOrder", parm);
            return dt;
        }
        #endregion


        #region[radhe]
        public DataTable GetFundRequestReport(Registration cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Member_Id",cl.SessionId),

            };
            dt = db.ExecProcDataTable("Sp_BindPending", parm);
            return dt;

        }
        public DataTable USP_VerfyCustomerOrder(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@MemberId",cl.AccountCode),
                 new SqlParameter("@Sdate",cl.FromDate),
                 new SqlParameter("@LDate",cl.ToDate),
                 new SqlParameter("@Status",cl.Status),
                 new SqlParameter("@UserName",cl.MemberId),
                 new SqlParameter("@Remarks",null),
                 new SqlParameter("@OrderType",cl.OrderType),

            };
            dt = db.ExecProcDataTable("USP_VerfyCustomerOrder", parm);
            return dt;



            //SqlConnection con = new SqlConnection(obj.str);
            //SqlCommand cmd = new SqlCommand("USP_VerfyCustomerOrder", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Action", 2);
            //cmd.Parameters.AddWithValue("@MemberId", txtInviceNo.Text == "" ? null : txtInviceNo.Text);
            //cmd.Parameters.AddWithValue("@Sdate", txtDateFrom.Text == "" ? null : txtDateFrom.Text);
            //cmd.Parameters.AddWithValue("@LDate", txtDateTo.Text == "" ? null : txtDateTo.Text);
            //cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue.Trim());
            //cmd.Parameters.AddWithValue("@UserName", null);
            //cmd.Parameters.AddWithValue("@Remarks", null);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();

            //da.Fill(dt);
        }

        public DataTable GetAdminStockPosition()
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {


            };
            dt = db.ExecProcDataTable("GetAdminStockDetails", parm);
            return dt;

            //SqlConnection con = new SqlConnection(obj.str);

            //con.Open();
            //cmd = new SqlCommand("exec GetAdminStockDetails  ", con);
            //dr = cmd.ExecuteReader();
            //GvDsrStock.DataSource = dr;
            //GvDsrStock.DataBind();
            //con.Close();


        }
        public DataTable LevelTree(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Session_ID",objp.MemberId),
               new SqlParameter("@Member_Id",objp.SessionId)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable CallJob()
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
            };
            dtt = db.ExecProcDataTable("PKC_Shedular", param);
            return dtt;
        }
        public DataTable GetAdminStockDetails(PayoutReport obj)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Comapanyid","1"),
                 new SqlParameter("@UserName",obj.MemberId),
                 new SqlParameter("@DateFrom", obj.FromDate),//DateTime.Now.ToString("dd-MMM-yyyy")),
                 new SqlParameter("@ItemCode", ""),

            };
            dt = db.ExecProcDataTable("DSR3", parm);
            return dt;

            //SqlConnection con = new SqlConnection(obj.str);

            //con.Open();
            //cmd = new SqlCommand("exec GetAdminStockDetails  ", con);
            //dr = cmd.ExecuteReader();
            //GvDsrStock.DataSource = dr;
            //GvDsrStock.DataBind();
            //con.Close();


        }
        public DataTable Get_franchiseStockDetails(cls_rpt_pack obj)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Comapanyid","1"),
                 new SqlParameter("@UserName",obj.Dipo_Code),
                 new SqlParameter("@DateFrom", obj.FromDate),//DateTime.Now.ToString("dd-MMM-yyyy")),
                 new SqlParameter("@ItemCode", obj.p_Code),

            };
            dt = db.ExecProcDataTable("DSR3", parm);
            return dt;

        }
        public DataTable GetAdminDipoStockDetails(Report obj)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Comapanyid","1"),
                 new SqlParameter("@DateFrom",obj.FromDate),// DateTime.Now.ToString("dd-MMM-yyyy")),
                 new SqlParameter("@ItemCode",obj.ProductCode),
                 new SqlParameter("@DipoId",obj.DepoCode),

            };
            dt = db.ExecProcDataTable("DSR", parm);
            return dt;

            //SqlConnection con = new SqlConnection(obj.str);

            //con.Open();
            //cmd = new SqlCommand("exec GetAdminStockDetails  ", con);
            //dr = cmd.ExecuteReader();
            //GvDsrStock.DataSource = dr;
            //GvDsrStock.DataBind();
            //con.Close();


        }

        public DataTable GetSelfRepurchaseDetails(PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@SMemberID",cl.UseId),
                 new SqlParameter("@FromDate",cl.FromDate),
                 new SqlParameter("@ToDate",cl.ToDate),
                 new SqlParameter("@MemberId",cl.MemberId),


            };
            dt = db.ExecProcDataTable("Proc_TeamRepurchase", parm);
            return dt;
        }
        public DataTable GetTeamRepurchaseDetails(PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@SMemberID",cl.UseId),
                 new SqlParameter("@FromDate",cl.FromDate),
                 new SqlParameter("@ToDate",cl.ToDate),
                 new SqlParameter("@MemberId",cl.MemberId),


            };
            dt = db.ExecProcDataTable("Proc_TeamRepurchase", parm);
            return dt;
        }

        public DataTable GetMemberRegistration(Registration cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                 new SqlParameter("@MemberId",cl.MemberId),


            };
            dt = db.ExecProcDataTable("sp_getMemberDetails_ById", parm);
            return dt;
        }

        public DataTable GetMemberDetails(Registration cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                 new SqlParameter("@MemberId",cl.MemberId),
                 new SqlParameter("@AddressId",cl.AddressId),


            };
            dt = db.ExecProcDataTable("sp_getMemberDetails_ById", parm);
            return dt;
        }

        public DataTable GetMemberEditDetails(Registration cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                 new SqlParameter("@MemberId",cl.MemberId),


            };
            dt = db.ExecProcDataTable("sp_getMemberDetails_ById", parm);
            return dt;
        }
        public DataTable GetMemberBankDetails(Registration cd)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@Member_Id",cd.SessionId)
            };
            dt = db.ExecProcDataTable("Sp_GetName", parm);
            return dt;
        }
        public DataTable Sp_UpdatebankDetails(Registration cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@MemberID",cl.SessionId),
                    new SqlParameter("@UpdatedBy",cl.SessionId),
                    new SqlParameter("@BankName",cl.BankName),
                    new SqlParameter("@BankaccNo",cl.AccountNo),
                    new SqlParameter("@BankIfscCode",cl.IFSCCode),
                    new SqlParameter("@panno",cl.PanNo),
                    new SqlParameter("@BankBranch",cl.BankBranch),
                    new SqlParameter("@payeeName",cl.Name),
                    new SqlParameter("@PhonePayNo",cl.PhonePayNo),

            };
            dt = db.ExecProcDataTable("Sp_UpdatebankDetails", parm);
            return dt;
            //cmd.Parameters.AddWithValue("@MemberID", Session["username"].ToString());
            //cmd.Parameters.AddWithValue("@BankName", txtBankName.Text.Trim());
            //cmd.Parameters.AddWithValue("@BankaccNo", txtBankAcNo.Text.Trim());
            //cmd.Parameters.AddWithValue("@BankIfscCode", txtIFSCCode.Text.Trim());
            //cmd.Parameters.AddWithValue("@panno", txtPanNo.Text.Trim());
            //cmd.Parameters.AddWithValue("@BankBranch", txtBankBranch.Text.Trim());
            //cmd.Parameters.AddWithValue("@UpdatedBy", Session["username"].ToString());
            //cmd.Parameters.AddWithValue("@payeeName", txtPayeeName.Text.Trim());
        }
        public DataTable GetDashBoardDetails(Registration cd)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@MemberId",cd.SessionId)
            };
            dt = db.ExecProcDataTable("GetDashBoard", parm);
            return dt;
        }
        public DataTable GetFundRequest(Registration cd)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@MemberId",cd.SessionId)
            };
            dt = db.ExecProcDataTable("sp_getMemberDetails_ById", parm);
            return dt;
        }
        #endregion
        #region Ajay Pal 20.11.2021

        public DataTable GetWebsite_ProductReq_Data(string MemberId, string InvoiceNo)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                 new SqlParameter("@MemberId",MemberId),
                 new SqlParameter("@InviceNo",InvoiceNo),


            };
            dt = db.ExecProcDataTable("USP_GetWebSiteProductReq_Data", parm);
            return dt;
        }
        public DataTable sp_getWebsite_PeoductReq_data_item(string InvoiceNo)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                 new SqlParameter("@InvoiceNo",InvoiceNo),
            };
            dt = db.ExecProcDataTable("sp_getWebsite_PeoductReq_data_item", parm);
            return dt;
        }

        public DataTable BindState()
        {
            DataTable dtt = new DataTable();
            dtt = db.ExecProcDataTable("sp_GetState_H");
            return dtt;
        }
        public DataTable BindDistrict(int StateId)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
             {
                new SqlParameter("@Member_Id",StateId)
             };
            dtt = db.ExecProcDataTable("Sp_GetDistrict_H", parm);
            return dtt;
        }
        public DataTable Asso_MemberReg(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Countrycode", objp.CountryCode),
                new SqlParameter("@mobile",objp.MobileNo),
                new SqlParameter("@Name",objp.Name),
                new SqlParameter("@f_name",objp.FatherName),
                new SqlParameter("@Mdob",objp.DOB),
                new SqlParameter("@gender",objp.Gender),
                new SqlParameter("@Email",objp.EmailId),
                 new SqlParameter("@Zipcode",objp.ZipCode),
                  new SqlParameter("@States",objp.StateId),
                new SqlParameter("@City",objp.District),
                new SqlParameter("@Address",objp.Address),
                new SqlParameter("@Landmark",objp.LandMark),
                new SqlParameter("@Area",objp.Area),
                new SqlParameter("@HouseNo",objp.HouseNo),
                new SqlParameter("@AdharNo",objp.AdharNo),
                new SqlParameter("@Intro_id",objp.SponserId),
              //  new SqlParameter("@Direction",objp.Direction),
                new SqlParameter("@NomName",objp.NomName),
                new SqlParameter("@NomRelation",objp.NomRelationName),
                new SqlParameter("@UserId",objp.EntryBy),
                new SqlParameter("@TransPassword",objp.TranPass),
                new SqlParameter("@Pass",objp.Pass),
                new SqlParameter("@ParentId",objp.ParentId),
                new SqlParameter("@panno",objp.PanNo),


            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable Asso_Member_Update(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Member_Id", objp.MemberId),
                new SqlParameter("@Countrycode", objp.CountryCode),
                new SqlParameter("@mobile",objp.MobileNo),
                new SqlParameter("@Name",objp.Name),
                new SqlParameter("@f_name",objp.FatherName),
                new SqlParameter("@Mdob",objp.DOB),
                new SqlParameter("@gender",objp.Gender),
                new SqlParameter("@Email",objp.EmailId),
                new SqlParameter("@Zipcode",objp.ZipCode),
                new SqlParameter("@StateName",objp.StateId),
                new SqlParameter("@DistrictName",objp.District),
                new SqlParameter("@City",objp.City),
                new SqlParameter("@Address",objp.Address),
                new SqlParameter("@Landmark",objp.LandMark),

                new SqlParameter("@ProfilePic1",objp.ProfilePic),
                new SqlParameter("@BankIfscCode",objp.IFSCCode),
                new SqlParameter("@BankName",objp.BankName),
                new SqlParameter("@BankBranch",objp.BankBranch),
                new SqlParameter("@BankaccNo",objp.AccountNo),
                new SqlParameter("@panno",objp.PanNo),
                new SqlParameter("@AdharNo",objp.AdharNo),

               // new SqlParameter("@Intro_id",objp.SponserId),
                 new SqlParameter("@NomName",objp.NomName),
                 new SqlParameter("@NomRelation",objp.NomRelationName),
                 new SqlParameter("@NomAddress",objp.NomAddressName),
                 new SqlParameter("@NomFahter",objp.NomFatherName),
                new SqlParameter("@UpdatedBy",objp.MemberId),
                new SqlParameter("@PANCopy", objp.PanFilePath),
            new SqlParameter("@AAdharCopy", objp.AdharFilePath),
            new SqlParameter("@Bank", objp.BankPassbookFilePath),
            new SqlParameter("@AAdharBackCopy", objp.AdharbackFilePath)

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable sp_ManageLeftRight(Registration cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Member_Id",cl.MemberId),
                    new SqlParameter("@Intro_id",cl.SponserId),
                    new SqlParameter("@Direction",cl.Direction),
            };
            dt = db.ExecProcDataTable("B_UpdateLeftRight", parm);
            return dt;

        }
        internal DataTable SaveUpdate_CustomerSales(cls_sales objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@MemberId",objPack.MemberId),
                new SqlParameter("@SessionId",objPack.hdnMemberId),
                new SqlParameter("@dtProduct",objPack.dtProduct),
                new SqlParameter("@InvoiceNo",objPack.InvNo),
                new SqlParameter("@PaymentType",objPack.PaymentType),
                new SqlParameter("@Address",objPack.Addr),
                new SqlParameter("@TxnID",objPack.TxnID),
            };
            dt = db.ExecProcDataTable("SaveUpdate_customerSales", parm);
            return dt;
        }
        public DataTable Get_MyDirectTeam(PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@MemberId",cl.MemberId),
                 new SqlParameter("@Status",cl.Status),
                 //new SqlParameter("@Leg",cl.Direction)

            };
            dt = db.ExecProcDataTable("Sp_GetRightTeamMemberDetails", parm);
            return dt;

        }
        public DataTable Get_MyLevelTeam(PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 //new SqlParameter("@MemberId",null),
                 //new SqlParameter("@Status",cl.Status),
                 //new SqlParameter("@Leg",cl.MemberId)
                 new SqlParameter("@MemberId", cl.MemberId),
            new SqlParameter("@Position", cl.Status != "" ? cl.Status : null),
            new SqlParameter("@FromDate", cl.FromDate != "" ? cl.FromDate : null),
            new SqlParameter("@ToDate", cl.ToDate != "" ? cl.ToDate : null),
            new SqlParameter("@Leg", cl.Direction != "" ? cl.Direction : null)

        };
            dt = db.ExecProcDataTable("Sp_GetMemberDetails", parm);
            return dt;

        }
        public DataTable GetKYCUPdate(KYCUpdate cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                    new SqlParameter("@MemberID",cl.MemberId)
            };
            dt = db.ExecProcDataTable("USP_BindMemberDetails", parm);
            return dt;
        }
        public DataTable InsertKYCUPdate(KYCUpdate Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
        new SqlParameter("@Member_ID",Objp.MemberId),
            new SqlParameter("@PANCopy", Objp.PanFilePath),
            new SqlParameter("@AAdharCopy", Objp.AdharFilePath),
            new SqlParameter("@Bank", Objp.BankPassbookFilePath),
            new SqlParameter("@AAdharBackCopy", Objp.AdharbackFilePath),
            // new SqlParameter("@Action",Objp.Action)
        };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;

        }
        internal DataTable customer_Sales_byAdmin(cls_sales objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@MemberId",objPack.MemberId),
                new SqlParameter("@dtProduct",objPack.dtProduct),
                new SqlParameter("@InvoiceNo",objPack.InvNo),
            };
            dt = db.ExecProcDataTable("SaveUpdate_Sales_customer_byAd", parm);
            return dt;
        }
        // Dipo Product Sale By Admin
        internal DataTable DipoProductSaleByAdmin(cls_pd_req objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@dtProduct",objPack.dtProduct),
            };
            dt = db.ExecProcDataTable("dipo_productsale_byAdmin", parm);
            return dt;
        }
        // Fran Product Sale
        internal DataTable fran_product_sale(cls_Ret objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@MemberId",objPack.MemberId),
                new SqlParameter("@dtProduct",objPack.dtProduct),
                new SqlParameter("@InvoiceNo",objPack.InvNo),
                new SqlParameter("@DemandNo",objPack.DemandNo),
            };
            dt = db.ExecProcDataTable("fran_productsale_byAdmin", parm);
            return dt;
        }
        internal DataTable get_cust_Wall_bal(cls_sales objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                new SqlParameter("@Member_Id",objPack.MemberId),
            };
            dt = db.ExecProcDataTable("GetMemberDetailsById", parm);
            return dt;
        }
        public DataTable Get_UserDashBoardDetails(Registration cd)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@MemberCode",cd.SessionId),
                new SqlParameter("@SessionID",cd.SessionId)
            };
            dt = db.ExecProcDataTable("USP_Dashboard", parm);
            return dt;
        }
        internal DataTable Save_ProductPurchase_by_Franchise(cls_pd_req objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@username",objPack.username),
                new SqlParameter("@dtProduct",objPack.dtProduct),
            };
            dt = db.ExecProcDataTable("sp_product_purchase_by_franchise", parm);
            return dt;
        }
        public DataTable ProductWiseSale(Report cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                    new SqlParameter("@ProductId",cl.ProductCode)
            };
            dt = db.ExecProcDataTable("sp_ProductWiseSale", parm);
            return dt;
        }
        public DataTable InvoiceReportByType(PayoutReport cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                    new SqlParameter("@Action",cl.Action),
                    new SqlParameter("@Member_id",cl.MemberId)
            };
            dt = db.ExecProcDataTable("sp_GetGSTBill_Invoice", parm);
            return dt;
        }
        public DataTable SaveUpdate_PaymentGateway(string Req_Id, string TranxId, HomeDashboardModel obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                    new SqlParameter("@Action",obj.Action),
                    new SqlParameter("@Member_id",obj.UserId),
                    new SqlParameter("@txnid",TranxId),
                    new SqlParameter("@mode",obj.mode),
                    new SqlParameter("@status",obj.status),
                    new SqlParameter("@mkey",obj.key),
                    new SqlParameter("@amount",obj.totAmt),
                    new SqlParameter("@productinfo",obj.productinfo)
            };
            dt = db.ExecProcDataTable("Sp_saveupdate_payment_gateway", parm);
            return dt;
        }

        #region Support 18.02.2022
        public DataTable Save_SendMessage(Support obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                    new SqlParameter("@Action",obj.Action),
                    new SqlParameter("@MsgType",obj.MsgType),
                    new SqlParameter("@MsgToUserId",obj.ToMemberId),
                    new SqlParameter("@ToUserName",obj.ToName),
                    new SqlParameter("@Msg_Subject",obj.Subject),
                    new SqlParameter("@Message",obj.Message),
                    new SqlParameter("@CreatedBy",obj.CreatedBy),
                    new SqlParameter("@UserType",obj.UserType),
            };
            dt = db.ExecProcDataTable("USP_SendSupportMessage", parm);
            return dt;
        }

        public DataTable InboxMessageReport(Support objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@Action",objp.Action),
                   new SqlParameter("@fromDate",objp.FromDate!=""?objp.FromDate:""),
                   new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:""),
                   new SqlParameter("@User_Id",objp.MemberId !="" ? objp.MemberId:null)
            };
            dt = db.ExecProcDataTable(ProcName, param);
            return dt;
        }

        public DataTable InboxMessageBy_ID(Support objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@Action",objp.Action),
                   new SqlParameter("@User_Id",objp.MsgId)
            };
            dt = db.ExecProcDataTable(ProcName, param);
            return dt;
        }

        public DataTable Save_SendReply(Support obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                    new SqlParameter("@Action",obj.Action),
                    new SqlParameter("@Msg_Id",obj.MsgId),
                    new SqlParameter("@ToUserId",obj.ToMemberId),
                    new SqlParameter("@Message",obj.Message),
                    new SqlParameter("@CreatedBy",obj.CreatedBy),
                    new SqlParameter("@UserType",obj.UserType)
            };
            dt = db.ExecProcDataTable("SP_SendSupportAnsMessage", parm);
            return dt;
        }

        #endregion support



        #endregion

        #region[GUNJA]




        public DataTable GetAllMaster(Stucommon Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                 new SqlParameter("@SessionId",Objp.SessionId),
                 new SqlParameter("@ClassId",Objp.ClassId)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable GetMainMenuData(Itemsdata Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@flag",Objp.Action),
                new SqlParameter("@assignrole",Objp.UserType),
                new SqlParameter("@username",Objp.Username)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable GetSubMenuData(Itemsdata Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@flag",Objp.Action),
                new SqlParameter("@assignrole",Objp.UserType),
                new SqlParameter("@username",Objp.Username),
                new SqlParameter("@id",Objp.mainmenuid)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable GetThirdMenuData(Itemsdata Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@flag",Objp.Action),
                new SqlParameter("@assignrole",Objp.UserType),
                new SqlParameter("@username",Objp.Username),
                new SqlParameter("@subid",Objp.Submenuid)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable DeleteMenuper(Itemsdata Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@flag",Objp.Action),
                new SqlParameter("@assignrole",Objp.UserType),
                new SqlParameter("@username",Objp.Username)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public int InsertMenuPermission(Itemsdata p, string ProcName)
        {
            int r = 0;
            try
            {
                string sql = ProcName;
                SqlParameter[] sqlParams = {
                new SqlParameter("@flag",SqlDbType.VarChar),
                new SqlParameter("@assignrole",SqlDbType.VarChar),
                new SqlParameter("@username",SqlDbType.VarChar),
                new SqlParameter("@MainMenuid",SqlDbType.VarChar),
                new SqlParameter("@id",SqlDbType.VarChar),
                new SqlParameter("@activestatus",SqlDbType.VarChar),
                new SqlParameter("@thirdid",SqlDbType.VarChar),
                //new SqlParameter("@userid",SqlDbType.VarChar),
            };
                sqlParams[0].Value = p.Action;
                sqlParams[1].Value = p.UserType;
                sqlParams[2].Value = p.Username;
                sqlParams[3].Value = p.mainmenuid;
                sqlParams[4].Value = p.Submenuid;
                sqlParams[5].Value = p.ActvStatus;
                sqlParams[6].Value = p.thirdid;
                //sqlParams[7].Value = p.userid;

                r = db.ExecuteNonQueryProc(sql, sqlParams);
                return r;
            }
            catch (Exception ex)
            {
                return r;
            }
        }

        public DataTable BindAllUsers(Itemsdata Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@UserType",Objp.UserType)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataSet getMenuConfiguration(Stucommon Objp, string ProcName)
        {
            DataSet dtt = new DataSet();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@RoleId",Objp.Role),
                new SqlParameter("@UserName",Objp.EntryBy)

            };
            dtt = db.ExecProcDataSet(ProcName, param);
            return dtt;
        }




        public DataTable AddUserDetails(int Action, Registration cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Action",Action),
                     new SqlParameter("@id",cl.user_code),
                      new SqlParameter("@UserName",cl.Name),
                     new SqlParameter("@UserPassword",cl.Pass),
                      new SqlParameter("@UserContactNo",cl.MobileNo),

            };
            dt = db.ExecProcDataTable("Sp_UserDetails", parm);
            return dt;

        }

        public DataTable EditUserDetails(int Action, Registration cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                   new SqlParameter("@Action",Action),
                     new SqlParameter("@id",cl.user_code),


            };
            dt = db.ExecProcDataTable("Sp_UserDetails", parm);
            return dt;

        }



        #endregion
        #region[radhe]
        public DataTable AllCustomersFund(Registration cl, string Proce)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                new SqlParameter("@Action",cl.Action),
            };
            dt = db.ExecProcDataTable("Sp_FundRequestReport", parm);
            return dt;
        }
        public DataTable FranchiseWallet(Registration cl, string Proce)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                new SqlParameter("@Action",cl.Action),
                 new SqlParameter("@Member_Id",cl.MemberId),
            };
            dt = db.ExecProcDataTable("Sp_FundRequestReport", parm);
            return dt;
        }
        public DataTable FranchiseDepoStock(Registration cl, string Proce)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                new SqlParameter("@Action",cl.Action),
            };
            dt = db.ExecProcDataTable("Sp_FundRequestReport", parm);
            return dt;
        }


        public DataTable SMSFranchiseDetails()
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
            };
            dt = db.ExecProcDataTable("Sp_SMSFranchiseDetails", parm);
            return dt;
        }
        public DataTable SMSDepoDetails()
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
            };
            dt = db.ExecProcDataTable("Sp_SMSDipoDetails", parm);
            return dt;
        }
        public DataTable SMSMemberDetails(cls_Ret objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
            };
            dt = db.ExecProcDataTable("[dbo].[Sp_SMSMemberDetails]", parm);
            return dt;
        }
        public DataTable AddUpdateVideoMaster(Master cl, string ProcName, string VideoFile)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@VideoId",cl.Videoid),
                  new SqlParameter("@VideoTitle",cl.VideoTitle),
                 new SqlParameter("@VideoFile",VideoFile),
                 new SqlParameter("@EnterBy",cl.EntryBy==""?"Admin":cl.EntryBy),
                 new SqlParameter("@Action",cl.Action),
                 new SqlParameter("@Type",cl.Type),
                };
            dt = db.ExecProcDataTable(ProcName, param);
            return dt;
        }

        public DataTable GetVideodata(string ID)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Videoid",ID)
            };
            dt = db.ExecProcDataTable("GetVideodata", parm);
            return dt;
        }
        public DataTable GetVideoMasterList()
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
            };
            dt = db.ExecProcDataTable("Sp_GetVideoMasterList", parm);
            return dt;
        }
        public DataTable DeleteVideoMaster(string ID)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Videoid",ID)
            };
            dt = db.ExecProcDataTable("DeleteVideoMaster", parm);
            return dt;
        }
        public DataTable GetMemberUploadVideoList(string type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Type",type)
            };
            dt = db.ExecProcDataTable("GetMemberUploadVideoList", parm);
            return dt;
        }
        public DataTable GetHowtouse_video(string type, int Action)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Type",type),
                new SqlParameter("@Action",Action)
            };
            dt = db.ExecProcDataTable("Get_howtouse_VideoList", parm);
            return dt;
        }
        #endregion
        #region How To Use 
        public DataTable GetHowToUseMasterList()
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
            };
            dt = db.ExecProcDataTable("Sp_GetHowToUseMasterList", parm);
            return dt;
        }
        public DataTable AddUpdateHowToUseMaster(Master cl, string ProcName, string VideoFile)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@Entryid",cl.Entryid),
                  new SqlParameter("@VideoTitle",cl.VideoTitle),
                 new SqlParameter("@VideoFile",VideoFile == "" ? null : VideoFile),
                 new SqlParameter("@EnterBy",cl.EntryBy==""?"Admin":cl.EntryBy),
                 new SqlParameter("@Action",cl.Action),
                 new SqlParameter("@Type",cl.Type),
                };
            dt = db.ExecProcDataTable(ProcName, param);
            return dt;
        }
        public DataTable GetHowToUsedata(string ID)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@entryId",ID)
            };
            dt = db.ExecProcDataTable("GetHowToUsedata", parm);
            return dt;
        }
        public DataTable DeleteHowToUseMaster(string ID)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@EntryId",ID)
            };
            dt = db.ExecProcDataTable("DeleteHowToUseMaster", parm);
            return dt;
        }
        #endregion
        #region Popup Upload

        public DataTable GetPopupUploadList()
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
            };
            dt = db.ExecProcDataTable("GetPopupUploadList", parm);
            return dt;
        }
        public DataTable AddUpdatePopupUpload(Master cl, string ProcName, string IamgeFile)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@Title",cl.ImageTitle),
                 new SqlParameter("@Type",cl.Type),
                 new SqlParameter("@Image",IamgeFile == "" ? null : IamgeFile),
                 new SqlParameter("@EnterdBy",cl.EntryBy==""?"Admin":cl.EntryBy),
                 new SqlParameter("@Action",cl.Action),
                 new SqlParameter("@EntryId",cl.Entryid),
                };
            dt = db.ExecProcDataTable(ProcName, param);
            return dt;
        }
        public DataTable GetPopupUploadDataForEdit(string ID)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@EntryId",ID)
            };
            dt = db.ExecProcDataTable("GetPopupUploadDataForEdit", parm);
            return dt;
        }
        public DataTable DeletePopupUploadMaster(string ID)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@EntryId",ID)
            };
            dt = db.ExecProcDataTable("DeletePopupUploadMaster", parm);
            return dt;
        }
        #endregion
        public DataTable rank_wise_report(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@MemberID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),
                   new SqlParameter("@Rank",objp.Rank)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        #region  GetImage
        //public DataTable GetImage(Master cl)
        //{
        //    DataTable dt = new DataTable();
        //    SqlParameter[] parm = new SqlParameter[] {
        //         new SqlParameter("@Action",cl.Action)
        //    };
        //    dt = db.ExecProcDataTable("GetImage", parm);
        //    return dt;
        //}

        public DataTable GetImage(Registration cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@Action",cl.Action)
            };
            dt = db.ExecProcDataTable("GetImage", parm);
            return dt;
        }
        public DataTable GetNews(Registration cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@Action",cl.Action)
            };
            dt = db.ExecProcDataTable("BindNews", parm);
            return dt;
        }


        public DataTable GetImageforwarehous(clsDash cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@Action",cl.Action)
            };
            dt = db.ExecProcDataTable("GetImage", parm);
            return dt;
        }



        public DataTable sp_BankStatementReportPaidUnpaid(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",cl.Action),
                 new SqlParameter("@MemberId",cl.MemberId),
                 new SqlParameter("@FromDate",cl.FromDate),
                 new SqlParameter("@ToDate",cl.ToDate),


            };
            dt = db.ExecProcDataTable("sp_BankStatementReportPaidUnpaid", parm);
            return dt;
        }
        public static DataTable setIPAddress(Master Objp, string ProcName)
        {
            dbHelper db = new dbHelper();
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Name",Objp.Name),
                new SqlParameter("@Address",Objp.Address),
                new SqlParameter("@TypeName",Objp.Type),
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        //public int Save_paid_pending_payoutreport(List<PayoutReport> List)
        //{
        //    int iResult = 0;

        //    try
        //    {
        //        if (objConnection.State == ConnectionState.Closed)
        //        {
        //            objConnection.Open();
        //        }
        //        foreach (var item in List)
        //        {
        //            using (SqlCommand _SqlCommand = new SqlCommand("Sp_UpdateWeeklyData", objConnection))
        //            {
        //                _SqlCommand.CommandType = CommandType.StoredProcedure;
        //                _SqlCommand.CommandTimeout = 0;

        //                _SqlCommand.Parameters.Add("@MemberId", SqlDbType.VarChar).Value = item.MemberId;
        //                _SqlCommand.Parameters.Add("@Dis", SqlDbType.VarChar).Value = item.Remark;
        //                _SqlCommand.Parameters.Add("@Action", SqlDbType.Int).Value = 1;


        //                iResult = Convert.ToInt32(_SqlCommand.ExecuteNonQuery());
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //    finally
        //    {
        //        objConnection.Close();
        //    }
        //    return iResult;
        //}
        public DataTable GetCustomerLedger(Registration cl)
        {
            DataTable dt = new DataTable();
            //SqlParameter @Wallet = new SqlParameter("@Wallet", SqlDbType.Decimal);
            //@Wallet.Direction = ParameterDirection.Output;
            //
            //SqlParameter[] parm = new SqlParameter[] {
            //     new SqlParameter("@MemberID",cl.MemberId),
            //     new SqlParameter("@FromDate",cl.FromDate),
            //     new SqlParameter("@ToDate",cl.ToDate),
            //     new SqlParameter("@Action",cl.Action),
            //    new SqlParameter("@Wallet",SqlDbType.Decimal,ParameterDirection.Output,cl.Wallet)

            SqlParameter @Wallet = new SqlParameter("@Wallet", SqlDbType.Decimal);
            @Wallet.Direction = ParameterDirection.Output;

            SqlParameter[] Para = new SqlParameter[5];
            Para[0] = new SqlParameter("@Member_ID", cl.MemberId);
            Para[1] = new SqlParameter("@FromDate", cl.FromDate != "" ? cl.FromDate : null);
            Para[2] = new SqlParameter("@ToDate", cl.ToDate != "" ? cl.ToDate : null);
            Para[3] = new SqlParameter("@Action", 1);
            Para[4] = @Wallet;
            dt = db.FetchData1Proc("USP_CustomerLadger", Para);
            return dt;
        }
        public DataTable save_changeSponser(Registration cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@Member_Id",cl.MemberId),
                 new SqlParameter("@Intro_id",cl.SponserId),
                 new SqlParameter("@NewIntro_id",cl.New_SponserId),


            };
            dt = db.ExecProcDataTable("USP_ChangeSponserId", parm);
            return dt;
        }
        public DataTable MemberKYCReport(DipoRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Action",objp.Action),
                 new SqlParameter("@MemberId",objp.MemberId),

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        internal double CheckAvalability(cls_sales objPack, string UserId)
        {
            DataTable dt = new DataTable();
            double Qty = 0;
            try
            {
                string Query = "";
                Query = "GetDepoStockCheck";
                SqlParameter[] Para = new SqlParameter[2];

                Para[0] = new SqlParameter("@KitAppCode", objPack.Id_C);
                Para[1] = new SqlParameter("@DipoId", UserId);
                dt = db.FetchData1Proc(Query, Para);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Qty = int.Parse(dt.Rows[0]["DIPOSTOCK"].ToString());
                }
            }
            catch (Exception ex)
            {
                dt = null;
                // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + ex.Message.ToString() + "');", true);
            }

            return Qty;

        }
        public DataTable Getpassword(Registration cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                 new SqlParameter("@MemberId",cl.UserId),

            };
            dt = db.ExecProcDataTable("sp_ForgatePassword", parm);
            return dt;
        }
        public DataTable SaveTrackingDetails(PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Member_Id",cl.MemberId),
                 new SqlParameter("@IncoiveNo",cl.InviceNo),
                 new SqlParameter("@CourierName",cl.CourierName),
                 new SqlParameter("@TrackingNo",cl.TrackingNo),
                 new SqlParameter("@NoOfBox",cl.NoOfBox),
                 new SqlParameter("@DispatchDate",cl.DispatchDate),
                 new SqlParameter("@Action",cl.Action),

            };
            dt = db.ExecProcDataTable("Sp_saveupdate_TrackingOrder", parm);
            return dt;
        }
        #endregion
        public DataTable update_only_profile(Registration cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Member_Id",cl.MemberId),
                    new SqlParameter("@ProfilePic1",cl.ProfilePic),
            };
            dt = db.ExecProcDataTable("usp_update_only_profile", parm);
            return dt;

        }
        public DataTable PlanMaster(Master Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@ID",Objp.CategoryId),
                new SqlParameter("@Pdf",Objp.FilePath),
                new SqlParameter("@Title",Objp.ImageTitle),
                new SqlParameter("@CraetedBy",Objp.EntryBy),
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        #region PinGenerate


        internal DataTable Proc_GetPinDetailsFilter(string Action, string mCode, string PinNo, string fDate)
        {
            DataTable dt = new DataTable();
            DateTime? date = null;
            string pin1no = "";
            if (!string.IsNullOrEmpty(fDate))
            {
                date = Convert.ToDateTime(fDate);
            }
            if (!string.IsNullOrEmpty(PinNo))
            {
                pin1no = PinNo;
            }

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@MemberID",mCode),
                new SqlParameter("@Action",Action),
                new SqlParameter("@PinNumber",pin1no),
                new SqlParameter("@GenDate",date),
            };
            dt = db.ExecProcDataTable("Proc_GetPinDetails", parm);
            return dt;

        }

        internal DataTable insertPinDetailsNew(string MemberId, string PackageId, Int64 NoOfpins, string Remarks, string EntryBy)
        {
            DataTable dt = new DataTable();
            SqlParameter[] Para = new SqlParameter[5];
            Para[0] = new SqlParameter("@memberId", MemberId);
            Para[1] = new SqlParameter("@PackageId", PackageId);
            Para[2] = new SqlParameter("@NoofPins", NoOfpins);
            Para[3] = new SqlParameter("@Remarks", Remarks);
            Para[4] = new SqlParameter("@EntryBy", EntryBy);
            dt = db.ExecProcDataTable("Proc_GeneratePins", Para);
            return dt;
        }


        //internal DataTable getPinAmtList()
        //{
        //    DataTable dt = new DataTable();
        //    string Query = "SELECT '0' as PackgeId,'Select' as PackgeName union all SELECT KitCode as PackgeId,Cast(KitAmount as varchar) as PackgeName  FROM Kit_Master WHERE  KitCode in(1,2,3,4)";
        //    dt = db.ExecAdaptorDataTable(Query);
        //    return dt;
        //}



        internal DataTable getPinAmtList()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '0' as PackgeId,'Select' as PackgeName union all SELECT KitCode as PackgeId,KitName as PackgeName  FROM Kit_Master ";
            //where BusinessID = 1
            dt = db.ExecAdaptorDataTable(Query);
            return dt;
        }




        internal int GeneratePin(string txtLoginID, string txtPinAmt, string txtNoOfPins, string txtRemark)
        {
            int numberofpin = Convert.ToInt16(txtNoOfPins);
            int generatepin = 0;
            try
            {
            g:
                if (generatepin < numberofpin)
                {
                    string PIN = GeneratePin();
                    SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@LoginId",txtLoginID),
                new SqlParameter("@PinAmount",txtPinAmt),
                new SqlParameter("@PinNumber",PIN),
                new SqlParameter("@Remark",txtRemark)
               };
                    DataTable dt = db.ExecProcDataTable("Proc_InserPin", parm);
                    if (dt != null && Convert.ToString(dt.Rows[0][0]) == "1")
                    {
                        generatepin++;
                        goto g;
                    }

                }
            }
            catch (Exception exec)
            {
                return 0;
            }
            return 1;
        }
        public string GeneratePin()
        {
            string PIN = string.Empty;
            int amt = 3500;
            string stramt = amt.ToString();
            int leng = stramt.Length;
            int remainingleng = 12 - leng;

            string numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string otp = string.Empty;
            for (int i = 0; i < remainingleng; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, numbers.Length);
                    character = numbers.ToCharArray()[index].ToString();
                }
                while (otp.IndexOf(character) != -1);
                otp += character;
            }
            PIN = stramt + otp;
            return PIN;
        }


        #endregion


        #region Pindetails [Gunja]

        internal DataTable Proc_GetPinDetails(string Action, string mCode, string genDate)
        {
            DataTable dt = new DataTable();
            DateTime? date = null;
            if (!string.IsNullOrEmpty(genDate))
            {
                date = Convert.ToDateTime(genDate);
            }
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@MemberID",mCode),
                new SqlParameter("@Action",Action),
                new SqlParameter("@GenDate",date),
            };
            dt = db.ExecProcDataTable("Proc_GetPinDetails", parm);
            return dt;
        }

        internal DataTable Proc_DeletePinNew(string PinNumber, string UserName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@PinNumber",PinNumber),
                new SqlParameter("@EntryBy",UserName),
            };
            dt = db.ExecProcDataTable("Proc_DeletePins", parm);
            return dt;
        }

        #endregion

        #region
        internal DataTable Proc_TransferPinLogAdmin(string mCode, string trDate)
        {
            DataTable dt = new DataTable();
            DateTime? date = null;
            if (!string.IsNullOrEmpty(trDate))
            {
                date = Convert.ToDateTime(trDate);
            }
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@MemberID",mCode),
                new SqlParameter("@TrDate",date)

            };
            dt = db.ExecProcDataTable("USP_PINtransactionlogAdmin", parm);
            return dt;
        }

        internal DataTable Proc_GetPinBalance(string Action, string mCode, string PackId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@MemberCode",mCode),
                new SqlParameter("@Action",Action),
                new SqlParameter("@PackId",PackId)
            };
            dt = db.ExecProcDataTable("Proc_GetPinBalance", parm);
            return dt;
        }




        internal DataTable PinTransfer(string transferFromId, string transferToId, string txtPackage, string pinBalance, string transferNoOfPin)
        {
            DataTable dt = new DataTable();
            SqlParameter[] Para = new SqlParameter[4];
            Para[0] = new SqlParameter("@Trans_MemberId", transferFromId);
            Para[1] = new SqlParameter("@Trans_RecieveMemberId", transferToId);
            Para[2] = new SqlParameter("@Pins", transferNoOfPin);
            Para[3] = new SqlParameter("@PackageID", txtPackage);
            dt = db.ExecProcDataTable("USP_TransferPINOneIDToAnother", Para);
            return dt;
        }

        internal DataTable getMemberDetails(string MemberId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] parm = {
                new SqlParameter("@MemberId",SqlDbType.VarChar)
            };
            parm[0].Value = MemberId;
            dt = db.ExecProcDataTable("usp_getdetails", parm);
            return dt;
        }
        internal DataTable getDetails_forPin(string MemberId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] parm = {
                new SqlParameter("@MemberId",SqlDbType.VarChar)
            };
            parm[0].Value = MemberId;
            dt = db.ExecProcDataTable("GetName_Pin_AllType", parm);
            return dt;
        }
        #endregion
        #region Ajay Pal 05.01.2022
        internal DataTable sp_getPackageList(int Action)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
            };
            dt = db.ExecProcDataTable("Sp_A01_AdminGetPinTypeAll", parm);
            return dt;
        }
        public DataTable GetMember_details_By_Id(string MemberId)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                 new SqlParameter("@MemberId",MemberId),
            };
            dt = db.ExecProcDataTable("sp_getMemberDetails_ById", parm);
            return dt;
        }
        public DataTable Save_MemberTopup_By_Pin(UserReport obj)
        {
            try
            {
                DataTable ds = new DataTable();
                SqlParameter[] Para = new SqlParameter[4];
                Para[0] = new SqlParameter("@Member_ID", obj.memberId);
                Para[1] = new SqlParameter("@PackageID", obj.PackageId);
                Para[2] = new SqlParameter("@UserID", obj.CreatedBy);
                Para[3] = new SqlParameter("@Role", obj.Role);
                ds = db.ExecProcDataTable("Sp_MemberActivation", Para);
                return ds;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DataTable Save_AdminWalletTranfer(UserReport obj)
        {
            try
            {
                DataTable ds = new DataTable();
                SqlParameter[] Para = new SqlParameter[4];
                Para[0] = new SqlParameter("@memberId", obj.memberId);
                Para[1] = new SqlParameter("@PayMode", obj.PayMode);
                Para[2] = new SqlParameter("@Nature", obj.Nature);
                Para[3] = new SqlParameter("@Amount", obj.Amount);
                ds = db.ExecProcDataTable("Sp_Save_AdminWalletTranfer", Para);
                return ds;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DataTable Get_AdminDashBoardDetails()
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@Action",1)
            };
            dt = db.ExecProcDataTable("GetDashboardDetails", parm);
            return dt;
        }
        #endregion
        #region[Withdrawl Request]
        public DataTable GetWalletDetails(Registration cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                new SqlParameter("@Action",cl.Action),
                new SqlParameter("@UserId",cl.SessionId),
            };
            dt = db.ExecProcDataTable("Proc_GetWalletDetails", parm);
            return dt;
        }


        public DataTable InsertWalletRequest(Registration cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                //new SqlParameter("@Action",cl.Action),
                new SqlParameter("@Member_Id",cl.MemberId),
                new SqlParameter("@Member_Name",cl.Name),
                new SqlParameter("@Address",cl.Address),
                new SqlParameter("@MobileNo",cl.MobileNo),
                new SqlParameter("@CurrentWallet",cl.CurrentWalletAmnt),
                new SqlParameter("@ReqWallet",cl.RequestWalletAmt),
                 new SqlParameter("@TransactionPass",cl.TranPass),
            };
            dt = db.ExecProcDataTable("USP_InsertPayoutRequest", parm);
            return dt;
        }
        public DataTable USP_InsertROIPayoutRequest(Registration cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                //new SqlParameter("@Action",cl.Action),
                new SqlParameter("@Member_Id",cl.MemberId),
                new SqlParameter("@Member_Name",cl.Name),
                new SqlParameter("@Address",cl.Address),
                new SqlParameter("@MobileNo",cl.MobileNo),
                new SqlParameter("@CurrentWallet",cl.CurrentWalletAmnt),
                new SqlParameter("@ReqWallet",cl.RequestWalletAmt),
            };
            dt = db.ExecProcDataTable("USP_InsertROIPayoutRequest", parm);
            return dt;
        }
        public DataTable PendingWithdrawlRequestsReport(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable ApprovedWithdrawlRequestsReport(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable RejectedWithdrawlRequestsReport(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable ApproveRejectWithdrawlRequests(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@MemberId",objp.MemberId),
                new SqlParameter("@Date",null),
                new SqlParameter("@Remark",objp.Remark),
                new SqlParameter("@amount",objp.Amount),
                new SqlParameter("@status",objp.Status),
                new SqlParameter("@ApprovedBy",objp.EntryBy),
                new SqlParameter("@id",objp.Id)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable WithdrawalHistoryReport(Registration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Member_ID",objp.MemberId!=""?objp.MemberId:null),
                 new SqlParameter("@FromDate",objp.FromDate!=""?objp.FromDate:null),
                  new SqlParameter("@ToDate",objp.ToDate!=""?objp.ToDate:null),

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }



        #endregion[Withdrawl Request]


        #region
        public DataTable ShowOrderedList(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@PinCode",cl.PinCode),
                 new SqlParameter("@UserId",cl.MemberId)

            };
            dt = db.ExecProcDataTable("Proc_FranchiseWiseOrder", parm);
            return dt;

        }


        public DataTable SetDelBoy(PropertyClass obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
        {
                new SqlParameter("@DelBoyId",obj.DeliveryTo),
                new SqlParameter("@InvoiceNo",obj.InvoiceNo),

        };
            dt = db.ExecProcDataTable("SetDeliveryBoy", param);


            return dt;
        }

        public DataTable BindDeliveryBoy(Delivery del)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",del.Action),
                new SqlParameter("@VolunteerId",del.VolunteerId)


            };
            dtt = db.ExecProcDataTable("USP_DeliveryBoy", param);
            return dtt;
        }

        public DataTable BindDeliveryBoyminimart(Delivery del)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",del.Action),
                new SqlParameter("@VolunteerId",del.VolunteerId)


            };
            dtt = db.ExecProcDataTable("USP_DeliveryBoy", param);
            return dtt;
        }



        #endregion


        #region Mohd Nadeem

        public DataTable GetAssginorderlist(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@DeliveryboyId",cl.MemberId),

            };
            dt = db.ExecProcDataTable("Proc_DeliveryboyWiseOrder", parm);
            return dt;

        }

        public DataTable GetAssginorderDeliverdlist(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@DeliveryboyId",cl.MemberId),

            };
            dt = db.ExecProcDataTable("Proc_DeliveryboyWiseOrder", parm);
            return dt;

        }


        internal DataTable get_Dash_Delivery(string Member_Id)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {

                 new SqlParameter("@Member_Id",Member_Id),

            };
            dt = db.ExecProcDataTable("GetDashboardDelivery", parm);
            return dt;

        }



        public DataTable Levelmembercount(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",cl.Action),
                 new SqlParameter("@MemberId",cl.MemberId),
                 new SqlParameter("@FromDate",cl.FromDate),
                 new SqlParameter("@ToDate",cl.ToDate),
                 new SqlParameter("@Level",cl.Status)


            };
            dt = db.ExecProcDataTable("proc_getlevelmembercount", parm);
            return dt;
        }


        internal DataTable ItemList(string Action, string OrderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@OrderNo",OrderNo),
            };
            dt = db.ExecProcDataTable("proc_orderReport", parm);
            return dt;
        }


        public DataTable Franchisewiseincomereportadmin(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@MemberId",cl.AccountCode),
                 new SqlParameter("@Sdate",cl.FromDate),
                 new SqlParameter("@LDate",cl.ToDate),
                 new SqlParameter("@Status",cl.Status),
                 new SqlParameter("@UserName",cl.MemberId),
                 new SqlParameter("@Remarks",null),
                 new SqlParameter("@OrderType",cl.OrderType),

            };
            dt = db.ExecProcDataTable("USP_franchisewiseincome", parm);
            return dt;

        }

        public DataTable Minimartincomereportadmin(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@MemberId",cl.AccountCode),
                 new SqlParameter("@Sdate",cl.FromDate),
                 new SqlParameter("@LDate",cl.ToDate),
                 new SqlParameter("@Status",cl.Status),
                 new SqlParameter("@UserName",cl.MemberId),
                 new SqlParameter("@Remarks",null),
                 new SqlParameter("@OrderType",cl.OrderType),

            };
            dt = db.ExecProcDataTable("USP_minimartwiseincome", parm);
            return dt;

        }





        public DataTable FranchiseInvoiceRequewest(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@MemberId",cl.AccountCode),
                 new SqlParameter("@Sdate",cl.FromDate),
                 new SqlParameter("@LDate",cl.ToDate),
                 new SqlParameter("@Status",cl.Status),
                 new SqlParameter("@UserName",cl.MemberId),
                 new SqlParameter("@Remarks",null),
                 new SqlParameter("@OrderType",cl.OrderType),

            };
            dt = db.ExecProcDataTable("USP_franchisewiseincome", parm);
            return dt;

        }



        public DataTable addreceiptforrequest(Requestmoneycls cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@action",cl.Action),
                 new SqlParameter("@InvoiceNo",cl.InvoiceNo),
                 new SqlParameter("@ReqAmount",cl.Amount),
                 new SqlParameter("@TransactionId",cl.TransactionId),
                 new SqlParameter("@MemberId",cl.MemberId),
                 new SqlParameter("@Imagepath",cl.ImagePath),
                 new SqlParameter("@Remark",cl.Remark),


            };
            dt = db.ExecProcDataTable("proc_requestmoneyfran", parm);
            return dt;

        }


        public DataTable addreceiptforrequestminimart(Requestmoneycls cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@action",cl.Action),
                 new SqlParameter("@InvoiceNo",cl.InvoiceNo),
                 new SqlParameter("@ReqAmount",cl.Amount),
                 new SqlParameter("@TransactionId",cl.TransactionId),
                 new SqlParameter("@MemberId",cl.MemberId),
                 new SqlParameter("@Imagepath",cl.ImagePath),
                 new SqlParameter("@Remark",cl.Remark),


            };
            dt = db.ExecProcDataTable("proc_requestmoneyminimart", parm);
            return dt;

        }



        public DataTable ViewRequestresponsefranchise(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@InvoiceNo",cl.InviceNo),


            };
            dt = db.ExecProcDataTable("proc_requestmoneyfran", parm);
            return dt;

        }


        public DataTable ViewRequestresponseminimart(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@InvoiceNo",cl.InviceNo),
                   new SqlParameter("@MemberId",cl.MemberId),

            };
            dt = db.ExecProcDataTable("proc_requestmoneyminimart", parm);
            return dt;

        }



        public DataTable GetPincodedetail(HomeDashboardModel Objp)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@action",Objp.Action),
                new SqlParameter("@Pincode",Objp.PinCode),

            };
            dtt = db.ExecProcDataTable("proc_getpincodefranchise", param);
            return dtt;
        }

        public DataTable CheckMemberdetailbyIdExists(string MemberId, string NewMemberId, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Action","1"),
               new SqlParameter("@MemberId",MemberId),
                new SqlParameter("@NewMemberId",NewMemberId)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }



        public DataTable Updatecancelreason(string Action, string InvoiceNo, string CancelReason)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@action",Action),
                new SqlParameter("@InvoiceNo",InvoiceNo),
                   new SqlParameter("@Cancelreason",CancelReason),
            };
            dtt = db.ExecProcDataTable("proc_updateInvoice", param);
            return dtt;
        }


        #endregion


        #region Nadeem



        public DataTable BindStateMini(MinimartRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@country_id",objp.CountryId)

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable BindFranchiselist(int Action)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action)

            };
            dtt = db.ExecProcDataTable("USP_BindDipo", param);
            return dtt;
        }




        public DataTable Bindformartfranchiselist(int Action)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@action",Action)

            };
            dtt = db.ExecProcDataTable("proc_getretailerlist", param);
            return dtt;
        }




        public DataTable AddMinimart(MinimartRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Name",objp.Name),
                new SqlParameter("@mobile",objp.MemberId),
                new SqlParameter("@MobileNo",objp.MobileNo),
                new SqlParameter("@EmailId",objp.EmailId),
                new SqlParameter("@GST",objp.Gst),
                new SqlParameter("@Branch",objp.Branch),
                new SqlParameter("@TPass",objp.TranPass),
                new SqlParameter("@Pass",objp.Pass),
                 new SqlParameter("@PanNo",objp.PanNo),
                new SqlParameter("@Addresss",objp.Address),
                new SqlParameter("@GSTno",objp.GSTNo),
                new SqlParameter("@BankName",objp.BankName),
                new SqlParameter("@AccountNo",objp.AccountNo),
                new SqlParameter("@IfscCode",objp.IFSCCode),
                new SqlParameter("@AccountHolderName",objp.AccName),
                new SqlParameter("@CityId",objp.CityId),
                 new SqlParameter("@StateId",objp.StateId) ,
                new SqlParameter("@CountryId",objp.CountryId),
                 new SqlParameter("@FranchiseId",objp.FranchiseId),
                  new SqlParameter("@BankBranch",objp.BankBranch),
                   new SqlParameter("@TinNo",objp.TinNo),
                   new SqlParameter("@Pincode",objp.PinCode),
                   new SqlParameter("@Action",objp.Action),
                   new SqlParameter("@Id",objp.Id),
                   new SqlParameter("@Minimart_Name",objp.Minimart_Name),
                      new SqlParameter("@AreaId",objp.AreaId)

            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable MinimartReport(MinimartRegistration objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@Action",objp.Action),
                 new SqlParameter("@Id",objp.Id),
                   new SqlParameter("@AprooveBy",objp.EntryBy)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }



        public DataTable PincodewiseareaMaster(Master Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                  new SqlParameter("@Id",Objp.Id),
                new SqlParameter("@PinCode",Objp.PinCode),
                new SqlParameter("@AreaName",Objp.AreaName),

                new SqlParameter("@action",Objp.Action)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }



        public DataTable Bindareapincodebysearch(string Pincode, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@action","5"),
                 new SqlParameter("@PinCode",Pincode),


            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }



        public DataTable CheckMemberPackage(string MemberId, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@action","1"),
                 new SqlParameter("@MemberId",MemberId),


            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }



        internal DataTable Wr_get_Minimart(string DipoId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@DipoId",DipoId),
            };
            dt = db.ExecProcDataTable("GetDashboardMinimart", parm);
            return dt;
        }



        internal DataTable UserCheckOrderalreadyadded(string Action, string UserId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@action",Action),
                new SqlParameter("@UserId",UserId),

            };
            dt = db.ExecProcDataTable("proc_getuserdetailpackage", parm);
            return dt;
        }


        public DataTable MinimartInvoiceRequewest(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@MemberId",cl.AccountCode),
                 new SqlParameter("@Sdate",cl.FromDate),
                 new SqlParameter("@LDate",cl.ToDate),
                 new SqlParameter("@Status",cl.Status),
                 new SqlParameter("@UserName",cl.MemberId),
                 new SqlParameter("@Remarks",null),
                 new SqlParameter("@OrderType",cl.OrderType),

            };
            dt = db.ExecProcDataTable("USP_minimartwiseincome", parm);
            return dt;

        }


        internal DataTable wr_rptminimart(cls_rpt_pack objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@FromDate",objPack.FromDate),
                new SqlParameter("@ToDate",objPack.ToDate),
                new SqlParameter("@Item_Code",objPack.p_Code),
                new SqlParameter("@Hsn",objPack.Hsn)
            };
            dt = db.ExecProcDataTable("wr_rpt", parm);
            return dt;
        }


        public DataTable Centerreportofminimart(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@MemberId",cl.AccountCode),
                 new SqlParameter("@Sdate",cl.FromDate),
                 new SqlParameter("@LDate",cl.ToDate),
                 new SqlParameter("@Status",cl.Status),
                 new SqlParameter("@UserName",cl.MemberId),
                 new SqlParameter("@Remarks",null),
                 new SqlParameter("@OrderType",cl.OrderType),

            };
            dt = db.ExecProcDataTable("USP_franchisewiseincome", parm);
            return dt;

        }


        public DataTable allincomefranchise(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@action",Action),
                 new SqlParameter("@MemberId",cl.MemberId),


            };
            dt = db.ExecProcDataTable("proc_unionalldatafranchis", parm);
            return dt;

        }


        public DataTable Franchisewithmartincome(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@action",Action),
                 new SqlParameter("@MemberId",cl.MemberId),


            };
            dt = db.ExecProcDataTable("proc_unionalldatafranchis", parm);
            return dt;

        }




        public DataTable OfferReportByType(PayoutReport cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                    new SqlParameter("@action",cl.Action),
                    new SqlParameter("@MemberId",cl.MemberId),
                      new SqlParameter("@Startdate",cl.FromDate),
                      new SqlParameter("@Enddate",cl.ToDate)
            };
            dt = db.ExecProcDataTable("proc_getofferreport", parm);
            return dt;
        }


        public DataTable OfferReportByAction(string Action)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                    new SqlParameter("@action",Action),

            };
            dt = db.ExecProcDataTable("proc_getofferreport", parm);
            return dt;
        }


        public DataTable ViewRequestresponsecompany(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@InvoiceNo",cl.InviceNo),


            };
            dt = db.ExecProcDataTable("proc_requestmoneyfran", parm);
            return dt;

        }


        public DataTable Centerreportofminimartviewadmin(string Action, PayoutReport cl)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                 new SqlParameter("@MemberId",cl.AccountCode),
                 new SqlParameter("@Sdate",cl.FromDate),
                 new SqlParameter("@LDate",cl.ToDate),
                 new SqlParameter("@Status",cl.Status),
                 new SqlParameter("@UserName",cl.MemberId),
                 new SqlParameter("@Remarks",null),
                 new SqlParameter("@OrderType",cl.OrderType),

            };
            dt = db.ExecProcDataTable("USP_franchisewiseincome", parm);
            return dt;

        }


        public DataTable Chartlistdetail(PayoutReport cl)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {
                    new SqlParameter("@action",cl.Action),
                    new SqlParameter("@MemberId",cl.MemberId),
                      new SqlParameter("@Startdate",cl.FromDate),
                      new SqlParameter("@Enddate",cl.ToDate)
            };
            dt = db.ExecProcDataTable("proc_getofferreport", parm);
            return dt;
        }



        #endregion

        #region Shani 26/04/2023
        public DataTable searchallRecord(string Prefix)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[]
            {

                      new SqlParameter("@Prefix",Prefix)
            };
            dt = db.ExecProcDataTable("sp_Autocomplete", parm);
            return dt;
        }
        internal DataTable AddToCart2(HomeDashboardModel obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",obj.Action),
                new SqlParameter("@CustomerID",obj.CustomerID),
                 new SqlParameter("@ProdID",obj.ProdID),
                 new SqlParameter("@UserId",obj.UserId),
                 new SqlParameter("@Password",obj.Password),
                 new SqlParameter("@ProdPrice",obj.ProdPrice),
                  new SqlParameter("@Quantity",obj.Quantity)

            };
            dt = db.ExecProcDataTable("sp_AddToCart2", parm);
            return dt;
        }
        #endregion


        internal DataTable SaveUpdate_Req_minimart(cls_pd_req objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@dtProduct",objPack.dtProduct),
            };
            dt = db.ExecProcDataTable("SaveUpdate_Req_minimart", parm);
            return dt;
        }


        internal DataTable Save_ProductPurchase_by_Minimart(cls_pd_req objPack)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objPack.Action),
                new SqlParameter("@Dipo_Code",objPack.Dipo_Code),
                new SqlParameter("@username",objPack.username),
                new SqlParameter("@dtProduct",objPack.dtProduct),
            };
            dt = db.ExecProcDataTable("sp_product_purchase_by_minimart", parm);
            return dt;
        }
        internal DataTable CheckAvailbility(int ProductId, int Qty, string username)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action","1"),
                new SqlParameter("@ProductId", ProductId),
                new SqlParameter("@qty", Qty),
                new SqlParameter("@UserName", username),
            };
            dt = db.ExecProcDataTable("Proc_checkQuantity", parm);
            return dt;
        }
        public DataTable GetMinimartStockDetails(PayoutReport obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@Username",obj.MemberId),
                 new SqlParameter("@Fromdate", obj.FromDate),
                 new SqlParameter("@Todate", obj.ToDate),
            };
            dt = db.ExecProcDataTable("Proc_MiniMartStok", parm);
            return dt;
        }

        
        public DataTable USP_GetClosingDateils(string Action, PayoutReport objp)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@Action",Action),
                 new SqlParameter("@MemberID",objp.MemberId),
                 new SqlParameter("@Sdate", objp.FromDate),
                 new SqlParameter("@Ldate", objp.ToDate),
                 new SqlParameter("@Level", objp.Level),
                 new SqlParameter("@Status", objp.Status)
            };
            dt = db.ExecProcDataTable("USP_GetClosingDateils", parm);
            return dt;
        }

        internal DataTable USP_P2P_Transfer(string Action,PayoutReport objP)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@Action",Action),
                 new SqlParameter("@UserId",objP.UseId),
                 new SqlParameter("@MemberID",objP.MemberId),
                 new SqlParameter("@Status", objP.Status),
                 new SqlParameter("@TranferAmt",objP.TranferAmt),
                  new SqlParameter("@TransactionPass",objP.TransactionID),
                   new SqlParameter("@DepositType",objP.DepositType),
            };
            dt = db.ExecProcDataTable("USP_P2P_Transfer", parm);
            return dt;
        }

        internal DataTable USP_UpdateLevelIncomePer(string Action,Master objP)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                 new SqlParameter("@Action",Action),
                 new SqlParameter("@Level",objP.Level),
                 new SqlParameter("@PackageId",objP.PackageId),
                 new SqlParameter("@LevelPer",objP.LevelPer)                 
            };
            dt = db.ExecProcDataTable("USP_UpdateLevelIncomePer", parm);
            return dt;
        }

        public DataTable UserProfile(profile p, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
          {
              new SqlParameter("@Action",p.Action),
              new SqlParameter("@Member_ID",p.Username),
          };
            dt = db.ExecProcDataTable(ProcName, param);
            return dt;
        }

        public DataTable USP_GetAppData(string Action,UserReport Requ)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
          {
              new SqlParameter("@Action",Action),
              new SqlParameter("@memberId",Requ.memberId),
              new SqlParameter("@SmemberId",Requ.SmemberId),
              new SqlParameter("@FromDate",Requ.FromDate),
              new SqlParameter("@ToDate",Requ.ToDate),
              new SqlParameter("@Role",Requ.Role),
          };
            dt = db.ExecProcDataTable("USP_GetAppData", param);
            return dt;
        }
    }
}

