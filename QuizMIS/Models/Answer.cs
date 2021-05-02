
namespace QuizMIS.Models  
{  
    public class Answer
    {      
        public int AnswerID { get;set;}
        
        public int marks { get; set;}

        public string QuizId { get; set; }

        public string StudentId { get;set; }
    }  
}