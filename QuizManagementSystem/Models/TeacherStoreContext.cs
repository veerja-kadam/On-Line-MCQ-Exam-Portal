
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace QuizMIS.Models {

    public class TeacherStoreContext {
    
        public string ConnectionString {get;set;}

        public TeacherStoreContext()  {  
            this.ConnectionString=null;  
        }   

        public TeacherStoreContext(string connectionString) {
            this.ConnectionString=connectionString;
        }

        private MySqlConnection GetConnection() {
            return new MySqlConnection(ConnectionString);
        }

        public Teacher getTeacherDetails (int username) {

            Teacher stu=new Teacher();
        
            MySqlConnection connection = GetConnection();
            connection.Open();
            string query = "select * from Teacher where loginId=@val1 ";
            MySqlCommand cmd= new MySqlCommand(query,connection);

            cmd.Parameters.AddWithValue("@val1",username);
            cmd.Prepare();

            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()) {
                stu.TeacherId=(int)reader["TeacherId"];
                stu.FullName=(string)reader["FullName"];
                stu.Semester=(int)reader["Semester"];
            }
            return stu;
        }

        public List<Quiz> GetQuizes(int username) {
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            List<Quiz> l=new List<Quiz>();

            string q1="select * from teacher where loginId=@v3";
            MySqlCommand c= new MySqlCommand(q1,Conn);
            c.Parameters.AddWithValue("@v3",username);

            var TeacherId=-1;
            var r=c.ExecuteReader();
            while(r.Read()) TeacherId=(int)r["TeacherId"];
            r.Close();

            string query="select * from quiz where TeacherId=@v1 AND StartTime IS NOT NULL";
            MySqlCommand cmd=new MySqlCommand(query,Conn);
            cmd.Parameters.AddWithValue("@v1",TeacherId);

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

        public int CreateQuiz(int TeacherId) {
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            string q="insert into quiz (TeacherId) values(@v1)";
            MySqlCommand c= new MySqlCommand(q,Conn);

            c.Parameters.AddWithValue("@v1",TeacherId);
            c.Prepare();
            c.ExecuteNonQuery();

            string query="select MAX(QuizId) as id from quiz";
            MySqlCommand cmd=new MySqlCommand(query,Conn);
            cmd.Prepare();

            var reader=cmd.ExecuteReader();
            int id=-1;
            while(reader.Read()) id=(int)reader["id"];

            return id;
        }

        public void DeleteQuiz(int QuizId) {
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            string query="delete from question where QuizId=@v1";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",QuizId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            MySqlCommand c3=new MySqlCommand("delete from quiz where QuizId=@v1",Conn);
            c3.Parameters.AddWithValue("@v1",QuizId);

            c3.Prepare();
            c3.ExecuteNonQuery();
            
        }
    }
}