
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace QuizMIS.Models {

    public class LoginStoreContext {
    
        public string ConnectionString {get;set;}

        public LoginStoreContext()  
        {  
            this.ConnectionString=null;  
        }   

        public LoginStoreContext(string connectionString) {
            this.ConnectionString=connectionString;
        }

        private MySqlConnection GetConnection() {
            return new MySqlConnection(ConnectionString);
        }

        public  void ValidateLogin (string username,string password,ref int loginId,ref string role) {

        
            MySqlConnection connection = GetConnection();
            connection.Open();
            string query = "select loginId,role from login where username=@val1 and password=@val2 ";
            MySqlCommand cmd= new MySqlCommand(query,connection);

            cmd.Parameters.AddWithValue("@val1",username);
            cmd.Parameters.AddWithValue("@val2",password);
            cmd.Prepare();

            MySqlDataReader reader = cmd.ExecuteReader();
            if(!reader.HasRows){
                    role="Invalid";
                    loginId=-1;
            }else {
                // string role="student";
                // int loginId=0;
                while(reader.Read()){
                    loginId=(int)reader["loginId"];
                    role=reader["role"].ToString();
                }
                // role=role;
                // loginId=loginId;
            }
            // return pair.ToString();
        }
    }
}