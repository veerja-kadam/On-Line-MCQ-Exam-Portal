
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
    public class TeacherController : Controller     
    {  
        [HttpGet]
        public IActionResult Profile(int id) {
            TeacherStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.TeacherStoreContext)) as TeacherStoreContext;  
            ViewBag.loginId=id;
            dynamic mymodel = new ExpandoObject();  
            mymodel.Teacher = context.getTeacherDetails(id);  
            mymodel.Quiz= context.GetQuizes(id);  
            return View(mymodel);  
        }   

        [HttpGet]
        public void Create(int TeacherId) {
            TeacherStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.TeacherStoreContext)) as TeacherStoreContext;  
            int id=(int)context.CreateQuiz(TeacherId);
            Response.Redirect($"/Quiz/Create/{id}");
        }

        public void Delete(int loginId,int QuizId) {
            TeacherStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.TeacherStoreContext)) as TeacherStoreContext;  
            context.DeleteQuiz(QuizId);
            Response.Redirect($"/Teacher/Profile/{loginId}");
        }

        public IActionResult ShowResults(int QuizId) {
            TeacherStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.TeacherStoreContext)) as TeacherStoreContext;  
            string QuizTitle=(string)context.GetQuizTitle(QuizId);
            ViewData["QuizTitle"]=QuizTitle;
            return View(context.GetQuizResults(QuizId));
        }
    }  
}