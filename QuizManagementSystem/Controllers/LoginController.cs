
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizMIS.Models;

namespace QuizMIS.Controllers  
{  
    public class LoginController : Controller     
    {  
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Message"]="Login";
            return View();
        }
 
        [HttpPost]
        public  IActionResult Index(string username,string password)  
        {  
            LoginStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.LoginStoreContext)) as LoginStoreContext;  
              int loginId=-1;
              string role="Invalid";
              
           
              context.ValidateLogin(username,password,ref loginId,ref role);
            // KeyValuePair<string,int>pair=new KeyValuePair<string,int>(str);


            if(role!="Invalid") {
                if(role=="student") 
                    Response.Redirect($"/Student?username={loginId}");
                else if(role=="teacher")
                    Response.Redirect($"/Teacher/Profile/{loginId}");
                else if (role=="admin")
                    Response.Redirect($"/Admin?username={loginId}");   
            }else 
                ViewData["Message"]="Invalid Username or Password";
            return View();  
        }     
    }  
}