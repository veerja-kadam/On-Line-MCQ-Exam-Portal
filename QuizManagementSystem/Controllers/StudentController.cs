
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizMIS.Models;

namespace QuizMIS.Controllers  
{  
    public class StudentController : Controller     
    {  
        [HttpGet]
        public IActionResult Index(int username)
        {
            
            StudentStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.StudentStoreContext)) as StudentStoreContext;  
            ViewData["Message"]="Student";
            dynamic mymodel = new ExpandoObject();
            mymodel.Quiz= context.GetQuizes();
            mymodel.Student = context.getStudentDetails(username);
            return View(mymodel);
        }   
    }  
}