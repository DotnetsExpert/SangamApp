using MLMPortal.Models;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MLMPortal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Portfolio()
        {
            return View();
        }
        public ActionResult Team()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        
        public JsonResult Save_Registration(Registration objp)
        {
            string Msg = "Server Not Responding. try after some time";
            string Flag = "0";
            Datalayer objdl = new Datalayer();
            try
            {
                // objp.Pass = CreateRandomPassword(4);
                objp.TranPass = "1234";
                objp.Action = 1;
                DataTable dt = objdl.AddMember(objp, "USP_InsertMembers");
                if (dt != null & dt.Rows.Count > 0)
                {
                    Msg = dt.Rows[0]["MSG"].ToString();
                    Flag = dt.Rows[0]["ID"].ToString();
                    string msg = "Welcome to " + objp.Name + " ! You have successfully Registered on " + DateTime.Now + " . Your Username is " + dt.Rows[0]["ID"].ToString() + " , Psw: " + objp.Pass + " and Tran Psw: " + objp.TranPass + " Thankyou, ";  //here call regiter api method
                     CallRegisterAPI(objp);

                }
                else
                {
                    Flag = "0";

                }

            }
            catch (Exception ex)
            {
            }
            return Json(new {flag= Flag ,msg= Msg },JsonRequestBehavior.AllowGet);
        }

        private void CallRegisterAPI(Registration objp)
        {
            try
            {
                // Prepare the request data
                var requestData = new RegisterUserModel
                {
                    name = objp.Name,
                    mobile = objp.MobileNo,
                    email = objp.EmailId,
                    state = "UP",
                    city = "Gorkhpur",
                    address = "Gorakhpur",
                    pincode = "271305"
                };

                // Convert the request data to JSON
                var jsonRequestData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);

                // Prepare the HTTP request
                using (var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("https://login.missionpay.co.in/api/register/user"),
                        Content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json")
                    };

                    var response = httpClient.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // Optionally, read the response content
                        var responseData =  response.Content.ReadAsStringAsync();
                        Console.WriteLine("Registration successful: " + responseData);
                    }
                    else
                    {
                        Console.WriteLine("Error occurred while registering user. Status code: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }


        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();

        }
        public ActionResult Mission()
        {
            return View();

        }
        public ActionResult Vission()
        {
            return View();

        }


        
    }
}