
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
// using System.Web.Mvc;
using System.Web;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizMIS.Models;
using Newtonsoft.Json;

namespace QuizMIS.Controllers  
{  
    
    public class QuizController : Controller     
    {  
        [HttpGet]
        public IActionResult Create(int id) {
            QuizStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.QuizStoreContext)) as QuizStoreContext;  
            ViewData["Message"]="Quiz";
            ViewData["QuizId"]=id;

            string StartTime="Start Time Not Specified";
            string EndTime="End Time Not Specified";
            string QuizTitle="ADS";

            context.GetQuizTimings(id,ref StartTime,ref EndTime, ref QuizTitle);

            ViewData["StartTime"]=StartTime;
            ViewData["EndTime"]=EndTime;
            ViewData["QuizTitle"]=QuizTitle;

            dynamic mymodel = new ExpandoObject();  
            mymodel.Question = context.getQuestions(id);
            return View(mymodel);  
        }

        public IActionResult Display(int id,int StudentId) {
            QuizStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.QuizStoreContext)) as QuizStoreContext;  
            ViewData["Message"]="Quiz";
            ViewData["QuizId"]=id;

            string StartTime="Start Time Not Specified";
            string EndTime="End Time Not Specified";
            string QuizTitle="ADS";

            context.GetQuizTimings(id,ref StartTime,ref EndTime, ref QuizTitle);

            ViewData["StartTime"]=StartTime;
            ViewData["EndTime"]=EndTime;
            ViewData["QuizTitle"]=QuizTitle;
            ViewData["StudentId"]=StudentId;

            dynamic mymodel = new ExpandoObject();  
            mymodel.Question = context.getQuestions(id);
            return View(mymodel);  
        }   

        public IActionResult UpdateTime(string StartTime,string EndTime,int QuizId, string QuizTitle) {
            QuizStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.QuizStoreContext)) as QuizStoreContext;  
            context.UpdateTiming(QuizId,StartTime,EndTime,QuizTitle);
            return RedirectToAction("Create", new { id=QuizId});
        }

        [HttpPost]
        public IActionResult Create(string Title,string Option1,string Option2,string Option3,string Option4,string CorrectAnswer,int Points,int QuizId) {
            QuizStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.QuizStoreContext)) as QuizStoreContext;  
            context.AddQuestion(QuizId,Title,Option1,Option2,Option3,Option4,CorrectAnswer,Points);
            return RedirectToAction("Create", new { id=QuizId});
        }

        public IActionResult Delete(int QuizId,int QuestionId) {
            QuizStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.QuizStoreContext)) as QuizStoreContext;  
            context.DeleteQuestion(QuestionId);
            return RedirectToAction("Create",new {id=QuizId});
        }

        public IActionResult Edit(int QuestionId,int QuizId) {
            QuizStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.QuizStoreContext)) as QuizStoreContext;  
            ViewData["QuestionId"]=QuestionId;
            ViewData["QuizId"]=QuizId;
            return View(context.GetQuestionDetails(QuestionId));
        }

        public IActionResult UpdateQuestion(int QuizId,int QuestionId,string Title,string Option1,string Option2,string Option3,string Option4,string CorrectAnswer,int Points) {
            QuizStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.QuizStoreContext)) as QuizStoreContext;  
            context.UpdateQuestion(QuestionId,Title,Option1,Option2,Option3,Option4,CorrectAnswer,Points);
            return RedirectToAction("Create",new {id=QuizId});
        }

        public IActionResult SubmitQuiz(int id,int StudentId,string answers) {

            QuizStoreContext context = HttpContext.RequestServices.GetService(typeof(QuizMIS.Models.QuizStoreContext)) as QuizStoreContext;  
            int marks=(int) context.GetMarks(id,StudentId,answers);
            Console.WriteLine(marks);

            context.UpdateAnswerTable(id,StudentId,marks);

            //display total marks to student  with student details,quiz details,
            // store TotalMarks,QuizId,StudentId to Result table
            // create Result table and model
            ViewData["TotalMarks"]=marks;
             dynamic mymodel = new ExpandoObject();
            mymodel.Quiz= context.GetQuizDetails(id);
            mymodel.Student = context.getStudentDetails(StudentId);
            return View("DisplayResult",mymodel);
        }
    }  
}