
namespace QuizMIS.Models  
{  
    public class Login
    {  
        private LoginStoreContext context;  
    
        public int loginId { get; set;}

        public string username { get; set; }

        public string password { get;set; }

        public string role { get; set; }
    }  
}