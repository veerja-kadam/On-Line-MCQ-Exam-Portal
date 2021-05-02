
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using QuizMIS.Models;

namespace QuizMIS.Models {

    public class QuizStoreContext {
    
        public string ConnectionString {get;set;}

        public QuizStoreContext() {  
            this.ConnectionString=null;  
        }   

        public QuizStoreContext(string connectionString) {
            this.ConnectionString=connectionString;
        }

        private MySqlConnection GetConnection() {
            return new MySqlConnection(ConnectionString);
        }

        public List<Question> getQuestions(int QuizId) {
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            string query="select * from question where QuizId=@v1";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",QuizId);
            cmd.Prepare();

            var reader=cmd.ExecuteReader();
            List<Question>q=new List<Question>();

            while(reader.Read()) {
                q.Add(new Question(){
                    QuestionId=(int)reader["QuestionId"],
                    Title=(string)reader["Title"],
                    Option1=(string)reader["Option1"],
                    Option2=(string)reader["Option2"],
                    Option3=(string)reader["Option3"],
                    Option4=(string)reader["Option4"],
                    CorrectAnswer=(string)reader["CorrectAnswer"],
                    Points=(int)reader["Points"]
                });
            }

            return q;
        }

        public void GetQuizTimings(int QuizId,ref string StartTime,ref string EndTime, ref string QuizTitle) {
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            string query="select QuizTitle,StartTime,EndTime from quiz where QuizId=@v1";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",QuizId);
            cmd.Prepare();

            var reader=cmd.ExecuteReader();
            while(reader.Read()) {
                if(reader["StartTime"]!=DBNull.Value)
                    StartTime=(string)reader["StartTime"];
                if(reader["EndTime"]!=DBNull.Value)
                    EndTime=(string)reader["EndTime"];
                if(reader["QuizTitle"]!=DBNull.Value)
                    QuizTitle=(string)reader["QuizTitle"];
            }
        }
        public void UpdateTiming(int QuizId,string StartTime,string EndTime, string QuizTitle) {
          
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            string query="update quiz set StartTime=@v1 , EndTime=@v2 , QuizTitle=@v4 where QuizId=@v3";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",StartTime);
            cmd.Parameters.AddWithValue("@v2",EndTime);
            cmd.Parameters.AddWithValue("@v3",QuizId);
            cmd.Parameters.AddWithValue("@v4",QuizTitle);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        public void AddQuestion(int QuizId,string Title,string Option1,string Option2,string Option3,string Option4,string CorrectAnswer,int Points) {
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            string query="insert into question (Title,Option1,Option2,Option3,Option4,CorrectAnswer,Points,QuizId) values(@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8)";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",Title);
            cmd.Parameters.AddWithValue("@v2",Option1);
            cmd.Parameters.AddWithValue("@v3",Option2);
            cmd.Parameters.AddWithValue("@v4",Option3);
            cmd.Parameters.AddWithValue("@v5",Option4);
            cmd.Parameters.AddWithValue("@v6",CorrectAnswer);
            cmd.Parameters.AddWithValue("@v7",Points);
            cmd.Parameters.AddWithValue("@v8",QuizId);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void DeleteQuestion(int QuestionId) {
            MySqlConnection Conn = GetConnection();
            Conn.Open();

            string query="delete from question where QuestionId=@v1";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",QuestionId);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public Question GetQuestionDetails(int QuestionId) {
            MySqlConnection Conn = GetConnection();
            Conn.Open();

            string query="select * from question where QuestionId=@v1";
            MySqlCommand cmd=new MySqlCommand(query,Conn);
            cmd.Parameters.AddWithValue("@v1",QuestionId);

            cmd.Prepare();
            var reader=cmd.ExecuteReader();

            Question q=new Question();
            while(reader.Read()) {
                q.QuestionId=(int)reader["QuestionId"];
                q.Title=(string)reader["Title"];
                q.Option1=(string)reader["Option1"];
                q.Option2=(string)reader["Option2"];
                q.Option3=(string)reader["Option3"];
                q.Option4=(string)reader["Option4"];
                q.CorrectAnswer=(string)reader["CorrectAnswer"];
                q.Points=(int)reader["Points"];
            }
            return q;
        }

         public void UpdateQuestion(int QuestionId,string Title,string Option1,string Option2,string Option3,string Option4,string CorrectAnswer,int Points) {
             Console.WriteLine(QuestionId);
             Console.WriteLine(Title);
             MySqlConnection Conn = GetConnection();
            Conn.Open();

            string query="update question set Title=@v1 ,  Option1=@v2 ,  Option2=@v3 ,  Option3=@v4 ,  Option4=@v5 ,  CorrectAnswer=@v6 ,  Points=@v8 where QuestionId=@v7";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",Title);
            cmd.Parameters.AddWithValue("@v2",Option1);
            cmd.Parameters.AddWithValue("@v3",Option2);
            cmd.Parameters.AddWithValue("@v4",Option3);
            cmd.Parameters.AddWithValue("@v5",Option4);
            cmd.Parameters.AddWithValue("@v6",CorrectAnswer);
            cmd.Parameters.AddWithValue("@v7",QuestionId);
            cmd.Parameters.AddWithValue("@v8",Points);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public int GetMarks(int QuizId,int StudentId,string answers) {
            MySqlConnection conn=GetConnection();
            conn.Open();
            int totalMarks=0;
            dynamic json=JsonConvert.DeserializeObject(answers);

            foreach(var item in json) {
                
                MySqlCommand cmd=new MySqlCommand("select CorrectAnswer,Points from question where QuestionId=@v1",conn);
                cmd.Parameters.AddWithValue("@v1",item.QuestionId);
                var reader=cmd.ExecuteReader();

                string cAns="";
                int points=0;

                while(reader.Read()) {
                    cAns=(string)reader["CorrectAnswer"];
                    points=(int)reader["Points"];
                }

                if(JsonConvert.ToString(JsonConvert.SerializeObject(cAns))==JsonConvert.ToString(JsonConvert.SerializeObject(item.selectedAnswer)))
                    totalMarks+=points;

                reader.Close();
            }
            return totalMarks;
        }

        public Student getStudentDetails (int username) {

            Student stu=new Student();
        
            MySqlConnection connection = GetConnection();
            connection.Open();
            string query = "select * from student where StudentId=@val1 ";
            MySqlCommand cmd= new MySqlCommand(query,connection);

            cmd.Parameters.AddWithValue("@val1",username);
            cmd.Prepare();

            MySqlDataReader reader = cmd.ExecuteReader(); 
            if(reader.Read()) {
                stu.StudentId=(int)reader["StudentId"];
                stu.StudentName=(string)reader["StudentName"];
                stu.TeacherId=(int)reader["TeacherId"];
                stu.CurrentYear=(int)reader["CurrentYear"];
                stu.Branch=(string)reader["Branch"];
            }
            return stu;
        }

        public Quiz GetQuizDetails(int QuizId) {
            MySqlConnection conn=GetConnection();
            conn.Open();

            Quiz q=new Quiz();

            MySqlCommand cmd=new MySqlCommand("select * from quiz where QuizId=@v1",conn);
            cmd.Parameters.AddWithValue("@v1",QuizId);
            cmd.Prepare();
            var reader=cmd.ExecuteReader();

            while(reader.Read()) {
                q.QuizId=(int)reader["QuizId"];
                q.QuizTitle=(string) reader["QuizTitle"];
            }
            return q;
        }

        public void UpdateAnswerTable(int QuizId,int StudentId,int marks) {
            MySqlConnection conn=GetConnection();
            conn.Open();

            MySqlCommand cmd=new MySqlCommand("Insert into answer (QuizId,StudentId,marks) values(@v1,@v2,@v3)",conn);
            cmd.Parameters.AddWithValue("@v1",QuizId);
            cmd.Parameters.AddWithValue("@v2",StudentId);
            cmd.Parameters.AddWithValue("@v3",marks);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}