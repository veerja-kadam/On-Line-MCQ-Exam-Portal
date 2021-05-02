
namespace QuizMIS.Models  
{  
    public class Quiz
    {  
        private QuizStoreContext context;  
    
        public int QuizId { get; set;}

        public string QuizTitle { get;set;}

        public int TeacherId { get; set; }

        public string StartTime { get;set; }

        public string EndTime { get; set; }

    }  
}