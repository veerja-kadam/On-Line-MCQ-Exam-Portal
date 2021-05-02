
namespace QuizMIS.Models  
{  
    public class Question
    {  
        private QuestionStoreContext context;  
    
        public int QuestionId { get; set;}

        public int QuizId {get; set;}

        public string Title { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }
        
        public string Option3 { get; set; }
        
        public string Option4 { get; set; }

        public string CorrectAnswer { get;set; }

        public string QuestionImage { get; set; }

        public int Points { get; set; }
    }  
}