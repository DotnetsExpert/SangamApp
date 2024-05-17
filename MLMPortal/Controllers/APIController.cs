using MLMPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MLMPortal.Controllers
{
    public class APIController : Controller
    {
        
        Login objp = new Login();
        Datalayer objdl = new Datalayer();
        private readonly HttpClient _client;
        public APIController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://login.missionpay.co.in/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(RegisterUserModel model)
        {
            HttpResponseMessage response = await _client.PostAsync("api/register/user", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                
                string responseData = await response.Content.ReadAsStringAsync();
                
                return Content("Registration successful");
            }
            else
            {
                return Content("Error occurred while registering user");
            }
        }

        [HttpPost]
        public async Task<ActionResult> TransactionReport(TransactionReportModel model)
        {
            HttpResponseMessage response = await _client.PostAsync("api/external/transaction/user/report", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                
                string responseData = await response.Content.ReadAsStringAsync();                
                return Content("Transaction report successful");
            }
            else
            {
                return Content("Error occurred while fetching transaction report");
            }
        }















    }
}