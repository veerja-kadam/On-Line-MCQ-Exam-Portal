
namespace QuizMIS.Models  
{  
    public class Teacher
    {  
        private TeacherStoreContext context;  
    
        public int TeacherId { get; set;}

        public int loginId {get;set;}

        public string FullName { get; set; }

        public int Semester {get;set;}
    }  
}