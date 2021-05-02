
namespace QuizMIS.Models  
{  
    public class Student
    {  
        private StudentStoreContext context;  
    
        public int StudentId { get; set;}

        public int loginId {get;set;}

        public string StudentName { get; set; }

        public int TeacherId { get;set; }

        public int CurrentYear { get; set; }

        public string Branch {get;set;}
    }  
}