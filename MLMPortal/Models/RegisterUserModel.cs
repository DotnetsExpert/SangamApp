using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MLMPortal.Models
{
    public class RegisterUserModel
    {
        public string name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string pincode { get; set; }

    }

    public class Datum
    {
        public int id { get; set; }
        public string number { get; set; }
        public string mobile { get; set; }
        public int provider_id { get; set; }
        public int api_id { get; set; }
        public int amount { get; set; }
        public int charge { get; set; }
        public int profit { get; set; }
        public int gst { get; set; }
        public int tds { get; set; }
        public object apitxnid { get; set; }
        public string txnid { get; set; }
        public string payid { get; set; }
        public string refno { get; set; }
        public string description { get; set; }
        public string remark { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public object udf4 { get; set; }
        public string udf5 { get; set; }
        public string udf6 { get; set; }
        public string status { get; set; }
        public int user_id { get; set; }
        public int credited_by { get; set; }
        public string rtype { get; set; }
        public string via { get; set; }
        public int adminprofit { get; set; }
        public int balance { get; set; }
        public int closing_balance { get; set; }
        public string trans_type { get; set; }
        public string product { get; set; }
        public object wid { get; set; }
        public int wprofit { get; set; }
        public object mdid { get; set; }
        public int mdprofit { get; set; }
        public object disid { get; set; }
        public int disprofit { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object fundbank { get; set; }
        public string apicode { get; set; }
        public string apiname { get; set; }
        public string username { get; set; }
        public string sendername { get; set; }
        public string providername { get; set; }
        public string agentname { get; set; }
        public string shopname { get; set; }
        public string phone { get; set; }
    }

    public class TransactionReportModel
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
        public int totalPage { get; set; }
    }




}