
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace QuizMIS.Models {

    public class StudentStoreContext {
    
        public string ConnectionString {get;set;}

        public StudentStoreContext()  
        {  
            this.ConnectionString=null;  
        }   

        public StudentStoreContext(string connectionString) {
            this.ConnectionString=connectionString;
        }

        private MySqlConnection GetConnection() {
            return new MySqlConnection(ConnectionString);
        }

        public Student getStudentDetails (int username) {

            Student stu=new Student();
        
            MySqlConnection connection = GetConnection();
            connection.Open();
            string query = "select * from student where loginId=@val1 ";
            MySqlCommand cmd= new MySqlCommand(query,connection);

            cmd.Parameters.AddWithValue("@val1",username);
            cmd.Prepare();

            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()) {
                stu.StudentName=(string)reader["StudentName"];
                stu.TeacherId=(int)reader["TeacherId"];
                stu.CurrentYear=(int)reader["CurrentYear"];
                stu.Branch=(string)reader["Branch"];
            }
            return stu;
        }

        public List<Quiz> GetQuizes() {
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            List<Quiz> l=new List<Quiz>();

            string query="select * from quiz;";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Prepare();

            var reader=cmd.ExecuteReader();

            while(reader.Read()) {
                l.Add(new Quiz(){
                    QuizId=(int)reader["QuizId"],
                    TeacherId=(int)reader["TeacherId"],
                    QuizTitle=reader["QuizTitle"] == DBNull.Value ? " ":(string)reader["QuizTitle"],
                    StartTime=reader["StartTime"] == DBNull.Value ? " ":(string)reader["StartTime"],
                    EndTime=reader["EndTime"]==DBNull.Value ? " ":(string)reader["EndTime"]
                });
            }
            return l;
        }
    }
}