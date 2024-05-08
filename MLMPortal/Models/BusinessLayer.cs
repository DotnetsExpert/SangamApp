using MLMPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MLMPortal.Models
{
    public class BusinessLayer
    {
        Master ObjP = new Master();
        Datalayer objdl = new Datalayer();
        string Msg = "", Flag = "";
        public string BindState(string CountryId)
        {
            FranRegistration objp = new FranRegistration();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<SelectListItem> lst = new List<SelectListItem>();
            try
            {


                objp.CountryId = Convert.ToInt32(CountryId);

                DataTable dt = objdl.BindStateFran(objp, "USP_BindState");

                lst = AllClasses.BindDDL(dt);


            }
            catch (Exception ex)
            {
                ObjP.Id = 0;
            }

            return js.Serialize(lst);
        }
        public string BindCity(string StateId)
        {
            FranRegistration objp = new FranRegistration();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<SelectListItem> lst = new List<SelectListItem>();
            try
            {


                objp.CountryId = Convert.ToInt32(StateId);

                DataTable dt = objdl.BindStateFran(objp, "USP_BindCity");

                lst = AllClasses.BindDDL(dt);


            }
            catch (Exception ex)
            {
                ObjP.Id = 0;
            }

            return js.Serialize(lst);
        }

        internal DataTable NewBindState(string CountryId=null)
        {
            FranRegistration objp = new FranRegistration();
            DataTable dt = new DataTable();
            try
            {
                objp.CountryId = Convert.ToInt32(CountryId);
                 dt = objdl.BindStateFran(objp, "USP_BindState");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal DataTable BindStateDropDown(string CountryId=null)
        {
            PropertyClass proObject = new PropertyClass();
            DataTable dt = new DataTable();
            try
            {
                proObject.CountryId =CountryId;
                proObject.Action = "1";
                dt = objdl.Bind(proObject, "Masters");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        internal DataTable BindSupplierDropDown(string CountryId = null)
        {
            PropertyClass proObject = new PropertyClass();
            DataTable dt = new DataTable();
            try
            {
                proObject.CompanyCode = "1";
                proObject.Action = "2";
                dt = objdl.Bind(proObject, "Masters");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal DataTable BindItemGroupDropDown(string CompanyCode = null)
        {
            PropertyClass proObject = new PropertyClass();
            DataTable dt = new DataTable();
            try
            {
                proObject.Action = "3";
                dt = objdl.Bind(proObject, "Masters");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        internal DataTable GetSupplierAccDetail(string AccountCode)
        {
            PropertyClass proObject = new PropertyClass();
            DataTable dt = new DataTable();
            try
            {
                proObject.Action = "4";
                proObject.CompanyCode = "1";
                proObject.AccountCode = AccountCode;
                dt = objdl.Bind(proObject, "Masters");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal DataTable GetItemHeadDetail(string ItemCode)
        {
            PropertyClass proObject = new PropertyClass();
            DataTable dt = new DataTable();
            try
            {
                proObject.Action = "5";
                proObject.CompanyCode = "1";
                proObject.ItemCode = ItemCode;
                dt = objdl.Bind(proObject, "Masters");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable InsertPurchaseOrder(PropertyClass Objp, string ProcName, DataTable dt)
        {

        DataTable dtt=    objdl.InsertPurchaseOrder(Objp, ProcName, dt);
            return dtt;
        }


        public DataTable InsertSuppliersAccount(PropertyClass Objp)
        {

            DataTable dtt = objdl.InsertSuppliersAccount(Objp, "proc_Accounthead");
            return dtt;
        }


        }
}